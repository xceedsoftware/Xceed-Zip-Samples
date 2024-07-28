/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [ZipArchiveListViewItem.cs]
 * 
 * Special implementation if the FileListViewItem class that handles the treeview management
 * for Zip archives.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Xceed.GZip;
using Xceed.Tar;
using Xceed.FileSystem;
using Xceed.Zip;
using Xceed.FileSystem.Samples.Utils.TreeView;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
  public class ZipArchiveListViewItem : FileListViewItem
  {
    #region CONSTRUCTORS

    public ZipArchiveListViewItem( AbstractFile file, AbstractTreeViewNode parentNode )
      : this( file, parentNode, string.Empty )
    {
    }

    public ZipArchiveListViewItem( AbstractFile file, AbstractTreeViewNode parentNode, string displayText )
      : base( file, displayText )
    {
      m_parentNode = parentNode;
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    public override AbstractTreeViewNode ParentNode
    {
      get { return m_parentNode; }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    /// <summary>
    /// Create the item physically and add it to the FolderTree if applicable.
    /// </summary>
    public override void Create()
    {
      base.Create();

      // Add a matching folder in the treeview.
      AbstractTreeViewNode node = new ZipArchiveTreeViewNode( this.File, this.ListView );
      m_parentNode.Nodes.Add( node );
    }

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

      if( confirmDelete )
      {
        string message = string.Format( "Are you sure you want to delete '{0}'?", m_item.Name );
        string caption = "Confirm Zip Archive Delete";

        if( itemIsReadonly )
          message = string.Format( "The Zip archive '{0}' is a read-only file. Are you sure you want to delete it?", m_item.Name );

        if( MessageBox.Show( message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 ) == DialogResult.No )
          return;
      }

      // Find the related treeview node
      foreach( AbstractTreeViewNode node in m_parentNode.Nodes )
      {
        if( node.Text == this.Text )
          node.Remove();
      }

      if( m_item.Exists )
      {
        if( itemIsReadonly )
          m_item.Attributes &= ~System.IO.FileAttributes.ReadOnly;

        FileSystemEvents deleteEvents = new FileSystemEvents();
        deleteEvents.ItemException += new ItemExceptionEventHandler( Delete_ItemException );

        m_item.Delete( deleteEvents, null );

        deleteEvents.ItemException -= new ItemExceptionEventHandler( Delete_ItemException );
      }

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

      // Refresh the associated node.
      if( m_parentNode != null )
      {
        foreach( AbstractTreeViewNode node in m_parentNode.Nodes )
        {
          // The FileSystemItem is already renammed. We must search for the display text.
          if( node.Text == this.Text )
          {
            node.Refresh( item );
            break;
          }
        }
      }

      m_item = item;

      this.Text = m_item.Name;

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

      if( originalName != name )
      {
        // Rename the zip file.
        m_item.Name = name;

        // We need to refresh the associated TreeNode.
        if( m_parentNode != null )
        {
          foreach( AbstractTreeViewNode node in m_parentNode.Nodes )
          {
            if( node.Text == originalName )
            {
              node.Refresh( m_item );
              break;
            }
          }
        }

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
      // Name
      string name = this.File.Name;

      if( m_displayText.Length > 0 )
        name = m_displayText;

      this.Text = name;

      // Size
      string formattedSize = this.GetFormattedSize();
      this.SubItems.Add( formattedSize );

      // CompressedSize
      string formattedCompressedSize = this.GetFormattedCompressedSize();
      this.SubItems.Add( formattedCompressedSize );

      // Type
      string type = typeof( Xceed.Zip.ZipArchive) .ToString();
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

      AbstractFile file = this.File;

      // KB
      if( file.Exists && file.Size > 0 )
        formattedSize = ( ( long )( ( file.Size + 1023 ) / 1024 ) ).ToString( "#,##0" ) + " KB";

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

    #region PRIVATE FIELDS

    private AbstractTreeViewNode m_parentNode; //= null

    #endregion PRIVATE FIELDS
  }
}