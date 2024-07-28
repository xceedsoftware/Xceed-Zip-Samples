/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FileListViewItem.cs]
 * 
 * Implementation of the AbstractListViewItem class and represent an AbstractFile.
 * It expose methods to controls the action the user can make on this item. (copy, paste, delete...)
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Zip;  
using Xceed.GZip;
using Xceed.Tar;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
	public class FileListViewItem : AbstractListViewItem
	{
    #region CONSTRUCTORS

    public FileListViewItem( AbstractFile file )
      : this( file, string.Empty )
    {
    }

		public FileListViewItem( AbstractFile file, string displayText )
		{
      if( file == null )
        throw new ArgumentNullException( "file" );

      m_item = file;
      m_displayText = displayText;

      this.RefreshIcon( false );
      this.FillData();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    /// <summary>
    /// Gets the internal FileSystemItem object as an AbstractFile.
    /// </summary>
    public AbstractFile File
    {
      get { return m_item as AbstractFile; }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    /// <summary>
    /// Delete the item physically and remove it from the FolderTree if applicable.
    /// </summary>
    /// <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    public override void Delete( bool confirmDelete )
    {
      // Ensure that the attributes are up-to-date.
      m_item.Refresh();

      bool itemIsReadonly = 
        ( ( m_item.Attributes & System.IO.FileAttributes.ReadOnly ) == System.IO.FileAttributes.ReadOnly );

      // Confirm deletion.
      if( confirmDelete )
      {
        string message = string.Format( "Are you sure you want to delete '{0}'?", m_item.Name );
        string caption = "Confirm File Delete";

        if( itemIsReadonly )
          message = string.Format( "The file '{0}' is a read-only file. Are you sure you want to delete it?", m_item.Name );

        if( MessageBox.Show( message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 ) == DialogResult.No )
          return;
      }

      // Delete the physical item if existing.
      if( m_item.Exists )
      {
        if( itemIsReadonly )
          m_item.Attributes &= ~System.IO.FileAttributes.ReadOnly;

        FileSystemEvents deleteEvents = new FileSystemEvents();
        deleteEvents.ItemException += new ItemExceptionEventHandler( Delete_ItemException );

        m_item.Delete( deleteEvents, null );

        deleteEvents.ItemException -= new ItemExceptionEventHandler( Delete_ItemException );
      }

      // Remove ourself from the list.
      this.Remove();
    }

    /// <summary>
    /// Refresh the current item with this new FileSystemItem. A refresh to the matching
    /// FolderTree node will be made if applicable.
    /// </summary>
    /// <param name="item">The new FileSystemItem that this item represent.</param>
    public override void Refresh( FileSystemItem item )
    {
      if( item == null )
        throw new ArgumentNullException( "item" );

      if( item as AbstractFile == null )
        throw new ArgumentException( "The item must be an AbstractFile.", "item" );

      m_item = item;

      this.FillData();

      this.RefreshIcon( true );
    }

    /// <summary>
    /// Rename the item physically and the matching FolderTree node if applicable.
    /// </summary>
    /// <param name="name">The new name for the item.</param>
    public override void Rename( string name )
    {
      if( name == null )
        throw new ArgumentNullException( "name" );

      string originalName = m_item.Name;

      if( name != originalName )
      {
        // Rename the object.
        m_item.Name = name;

        // Make sure the text is ok.
        if( this.Text != name )
          this.Text = name;

        // Refresh the icon.
        this.RefreshIcon( true );
      }
    }

    #endregion PUBLIC METHODS

    #region FILESYSTEM EVENTS

    private void Delete_ItemException(object sender, ItemExceptionEventArgs e)
    {
      // The item was probably deleted by some other application. We just skip this one then.
      if( e.Exception as ItemDoesNotExistException != null )
      {
        e.Action = ItemExceptionAction.Ignore;
        return;
      }

      // At this point, we always want to delete read only items.
      if( e.Exception as ItemIsReadOnlyException != null )
      {
        e.CurrentItem.Attributes &= ~FileAttributes.ReadOnly;
        e.Action = ItemExceptionAction.Retry;
        return;
      }

      // Zipped file can be password protected. We need to ask the user to input that password.
      if( e.Exception as Xceed.Zip.InvalidDecryptionPasswordException != null )
      {
        using( InputPasswordForm passwordForm = new InputPasswordForm() )
        {
          if( passwordForm.ShowDialog( null, e.CurrentItem.FullName ) == DialogResult.OK )
          {
            if( ( e.CurrentItem as ZippedFile != null ) || ( e.CurrentItem as ZippedFolder != null ) )
            {
              ZipArchive archive = e.CurrentItem.RootFolder as ZipArchive;

              if( archive != null )
              {
                archive.DefaultDecryptionPassword = passwordForm.Password;
                e.Action = ItemExceptionAction.Retry;
                return;
              }

              // Set the last decryption password used for future use.
              Options.ZipLastDecryptionPasswordUsed = passwordForm.Password;
            }
          }
        }
      }

      // For any other exceptions, we show an error message and wait for the user action.
      string message = string.Format(
        "An error occured while deleting the item {0}.",
        e.CurrentItem.FullName );

      DialogResult result = MessageBox.Show( message, "Error Deleting Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error );

      switch( result )
      {
        case DialogResult.Abort:
          e.Action = ItemExceptionAction.Abort;
          break;

        case DialogResult.Retry:
          e.Action = ItemExceptionAction.Retry;
          break;

        case DialogResult.Ignore:
          e.Action = ItemExceptionAction.Ignore;
          break;
      }
    }

    #endregion FILESYSTEM EVENTS

    #region PROTECTED METHODS

    protected override void FillData()
    {
      this.SubItems.Clear();

      // Name
      string name = m_item.Name;

      if( m_displayText.Length > 0 )
        name = m_displayText;

      this.Text = name;

      // Size
      string formattedSize = this.GetFormattedSize();
      this.SubItems.Add( formattedSize );

      // CompressedSize
      string formattedcompressedSize = this.GetFormattedCompressedSize();
      this.SubItems.Add( formattedcompressedSize );

      // Type
      string type = m_item.GetType().ToString();
      this.SubItems.Add( type );

      // Modification date
      string modifiedDateFormatted = "N/A";
      
      if( m_item.HasLastWriteDateTime )
      {
        modifiedDateFormatted = m_item.LastWriteDateTime.ToString( 
          DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortTimePattern );
      }
      
      this.SubItems.Add( modifiedDateFormatted );

      // Encryption (ZIP)
      ZippedFile zippedFile = m_item as ZippedFile;

      if(  ( zippedFile != null )
        && ( zippedFile.Encrypted ) )
      {
        this.ForeColor = System.Drawing.Color.Green;
      }
      else
      {
        this.ForeColor = System.Drawing.SystemColors.WindowText;
      }
    }

    protected override string GetFormattedCompressedSize()
    {
      if( m_item.RootFolder.HostFile == null )
        return string.Empty;

      if( m_item is ZippedFile )
      {
        return this.GetZipCompressedSize();
      }
      else if( m_item is GZippedFile )
      {
        return this.GetGZipCompressedSize();
      }
      else if( m_item is TarredFile )
      {
        return this.GetTarCompressedSize();
      }
      else
      {
        return "0 KB";
      }
    }

    protected override string GetFormattedSize()
    {
      string formattedSize = "0 KB";

      // KB
      if( m_item.Exists && this.File.Size > 0 )
        formattedSize = ( ( long )( ( this.File.Size + 1023 ) / 1024 ) ).ToString( "#,##0" ) + " KB";

      return formattedSize;
    }

    #endregion PROTECTED METHODS

    #region PRIVATE METHODS

    private string GetZipCompressedSize()
    {
      ZippedFile zippedFile = m_item as ZippedFile;

      zippedFile.Refresh();

      // KB
      if( zippedFile.Exists && zippedFile.CompressedSize > 0 )
        return ( ( long )( ( zippedFile.CompressedSize + 1023 ) / 1024 ) ).ToString( "#,##0" ) + " KB";

      return "0 KB";
    }

    private string GetGZipCompressedSize()
    {
      // GZip does not expose CompressedSize.
      return "N/A";
    }

    private string GetTarCompressedSize()
    {
      TarredFile tarredFile = m_item as TarredFile;

      tarredFile.Refresh();

      // KB
      if( tarredFile.Exists && tarredFile.Size > 0 )
        return ( ( long )( ( tarredFile.Size + 1023 ) / 1024 ) ).ToString( "#,##0" ) + " KB";

      return "0 KB";
    }

    #endregion PRIVATE METHODS
  }
}
