/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FileListViewItem.cs]
 * 
 * Implementation of the AbstractListViewItem class and represent an AbstractFolder.
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
using Xceed.FileSystem.Samples.Utils.TreeView;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
	public class FolderListViewItem : AbstractListViewItem
	{
    #region CONSTRUCTORS

    public FolderListViewItem( AbstractFolder folder )
      : this( folder, null, string.Empty )
    {
    }

    public FolderListViewItem( AbstractFolder folder, AbstractTreeViewNode parentNode )
      : this( folder, parentNode, string.Empty )
    {
    }
    
		public FolderListViewItem( AbstractFolder folder, AbstractTreeViewNode parentNode, string displayText )
		{
      if( folder == null )
        throw new ArgumentNullException( "folder" );

      m_item = folder;
      m_parentNode = parentNode;
      m_displayText = displayText;

      this.RefreshIcon( false );
      this.FillData();
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    /// <summary>
    /// Gets the internal FileSystemItem object as an AbstractFolder.
    /// </summary>
    public AbstractFolder Folder
    {
      get { return m_item as AbstractFolder; }
    }

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
      // Let the base class create the item physically.
      base.Create();

      // Add a matching folder in the treeview.
      AbstractTreeViewNode node = new FolderTreeViewNode( this.Folder, this.ListView );
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

      AbstractFolder folder = this.Folder;
      
      if( folder != null && folder.IsRoot )
        throw new NotSupportedException( "Cannot delete a root folder." );

      bool itemIsReadonly = 
        ( ( m_item.Attributes & System.IO.FileAttributes.ReadOnly ) == System.IO.FileAttributes.ReadOnly );

      // Confirm deletion.
      if( confirmDelete )
      {
        string message = string.Format( "Are you sure you want to remove the folder '{0}' and all its contents?", m_item.Name );
        string caption = "Confirm Folder Delete";

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

      if( item as AbstractFolder == null )
        throw new ArgumentException( "The item must be an AbstractFolder." );

      // Refresh the associated node.
      if(  ( m_parentNode != null ) 
        && ( m_item is AbstractFolder ) )
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

      // Refresh ourself.
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

      if( !this.RenameToolEnabled )
        throw new ApplicationException( "Cannot rename a root folder." );

      string originalName = m_item.Name;

      if( name != originalName )
      {
        m_item.Name = name;

        if(  ( m_parentNode != null ) 
          && ( m_item is AbstractFolder ) )
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

      // Zipped folder can be password protected. We need to ask the user to input that password.
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

      if( this.Folder.IsRoot )
        name = m_item.FullName;

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
    }

    protected override string GetFormattedCompressedSize()
    {
      // Folders don't have a size.
      return string.Empty;
    }

    protected override string GetFormattedSize()
    {
      // Folders don't have a size.
      return string.Empty;
    }

    #endregion PROTECTED METHODS

    #region PRIVATE FIELDS

    private AbstractTreeViewNode m_parentNode; //= null

    #endregion PRIVATE FIELDS
	}
}
