/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [MainForm.cs]
 * 
 * This is the main form of the application. All the user interaction
 * is handled here.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Management;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.IO;
using Xceed.Compression;
using Xceed.FileSystem;
using Xceed.Ftp;
using Xceed.Tar;
using Xceed.GZip;
using Xceed.Zip;
using Xceed.FileSystem.Samples;
using Xceed.FileSystem.Samples.Utils;
using Xceed.FileSystem.Samples.Utils.API;
using Xceed.FileSystem.Samples.Utils.FileSystem;
using Xceed.FileSystem.Samples.Utils.Icons;
using Xceed.FileSystem.Samples.Utils.ListView;
using Xceed.FileSystem.Samples.Utils.TreeView;

namespace Xceed.FileSystem.Samples
{
	public class MainForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

		public MainForm()
		{
			InitializeComponent();

      // Set the image list on the controls.
      FileView.SmallImageList = IconCache.SmallIconList;
      FileView.LargeImageList = IconCache.LargeIconList;
      FolderTree.ImageList = IconCache.SmallIconList;
      TreeView.CheckForIllegalCrossThreadCalls = false;
      MainStatusBar.ItemsImageList = IconCache.SmallIconList;

      // Load the treeview with the "My Computer" node.
      FolderTree.Nodes.Add( new MyComputerTreeViewNode( new MyComputerFolder(), FileView ) );

      // Load the Ftp connections folder and add a connection to ftp.xceed.com
      FtpConnectionsFolder ftpConnections = new FtpConnectionsFolder();
      AbstractTreeViewNode node = new FtpConnectionsTreeViewNode( ftpConnections, FileView );
      FolderTree.Nodes.Add( node );

      try
      {
        FtpConnection ftpConnection = new FtpConnection( "ftp.xceed.com" );

        ftpConnection.CertificateReceived += new CertificateReceivedEventHandler( FtpConnection_CertificateReceived );
        ftpConnection.SynchronizingObject = this;

        ftpConnections.CreateConnectionFolder( ftpConnection );
      }
      catch {}

      // Let's also add a virtual RAM drive just for fun!
      // (and to show you how amasing are the MemoryFolder and MemoryFile classes!)
      MemoryFolder ramDrive = new MemoryFolder( "RAM", "\\" );
      FolderTree.Nodes.Add( new FolderTreeViewNode( ramDrive, FileView, "Memory folder" ) );

      // Let's add a user store drive
      IsolatedFolder isoDrive = new IsolatedFolder( "\\" );
      FolderTree.Nodes.Add( new FolderTreeViewNode( isoDrive, FileView, "User's folder" ) );

      // Set the second node as the starting point, to avoid exception on empty floppy      
      FolderTree.SelectedNode = FolderTree.Nodes[ 0 ]; 

      // Initialize the clipboard.
      m_clipBoard = new DataObject();
    }

    #endregion CONSTRUCTORS

    #region PRIVATE METHODS

    private void CreateFile()
    {
      string fileName = "New file";
      string fileExtension = ".txt";
      string fileNameTemplate = fileName + " ({0})" + fileExtension;
      int fileNameIndex = 2;

      // Get a reference on the selected node.
      AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;

      // The folder might not support file creation.
      AbstractFile file = null;

      try
      {
        file = parentNode.Folder.GetFile( fileName + fileExtension );

        // We want a file that does not already exists.
        while( file.Exists )
        {
          file = parentNode.Folder.GetFile( string.Format( fileNameTemplate, fileNameIndex++ ) );
        }
      }
      catch 
      {
        MessageBox.Show( this, "Cannot create files in this folder." );
        return;
      }

      // Create an AbstractListViewItem and put it in edit mode. (a flag must
      // be raised so that when edition is done, we actually create the file.
      m_creatingItem = true;
      
      AbstractListViewItem item = new FileListViewItem( file );
      FileView.Items.Add( item );
      
      item.BeginEdit();
    }

    private void CreateFolder()
    {
      string folderName = "New folder";
      string folderNameTemplate = folderName + " ({0})";
      int folderNameIndex = 2;

      // Get a reference on the selected node.
      AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;

      // The folder might not support folder creation.
      AbstractFolder folder = null;

      try
      {
        folder = parentNode.Folder.GetFolder( folderName );

        // We want a folder that does not already exists.
        while( folder.Exists )
        {
          folder = parentNode.Folder.GetFolder( string.Format( folderNameTemplate, folderNameIndex++ ) );
        }
      }
      catch 
      {
        MessageBox.Show( this, "Cannot create folders in this folder." );
        return;
      }

      // Create an AbstractListViewItem and put it in edit mode. (a flag must
      // be raised so that when edition is done, we actually create the folder.
      m_creatingItem = true;
      
      AbstractListViewItem item = new FolderListViewItem( folder, parentNode );
      FileView.Items.Add( item );

      item.BeginEdit();
    }

    private void CreateGZipArchive()
    {
      string archiveName = "New GZip archive";
      string archiveExtension = ".gz";
      string archiveNameTemplate = archiveName + " ({0})" + archiveExtension;
      int archiveNameIndex = 2;

      // Get a reference on the selected node.
      AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;

      // The folder might not support file creation.
      AbstractFile gzipFile = null;

      try
      {
        gzipFile = parentNode.Folder.GetFile( archiveName + archiveExtension );

        // We want a file that does not already exists.
        while( gzipFile.Exists )
        {
          gzipFile = parentNode.Folder.GetFile( string.Format( archiveNameTemplate, archiveNameIndex++ ) );
        }
      }
      catch 
      {
        MessageBox.Show( this, "Cannot create files in this folder." );
        return;
      }

      // Create an AbstractListViewItem and put it in edit mode. (a flag must
      // be raised so that when edition is done, we actually create the folder.
      m_creatingItem = true;
      
      AbstractListViewItem item = new GZipArchiveListViewItem( gzipFile, parentNode );
      FileView.Items.Add( item );

      item.BeginEdit();
    }

    private void CreateTarArchive()
    {
      string archiveName = "New Tar archive";
      string archiveExtension = ".tar";
      string archiveNameTemplate = archiveName + " ({0})" + archiveExtension;
      int archiveNameIndex = 2;

      // Get a reference on the selected node.
      AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;

      // The folder might not support file creation.
      AbstractFile tarFile = null;

      try
      {
        tarFile = parentNode.Folder.GetFile( archiveName + archiveExtension );

        // We want a file that does not already exists.
        while( tarFile.Exists )
        {
          tarFile = parentNode.Folder.GetFile( string.Format( archiveNameTemplate, archiveNameIndex++ ) );
        }
      }
      catch 
      {
        MessageBox.Show( this, "Cannot create files in this folder." );
        return;
      }

      // Create an AbstractListViewItem and put it in edit mode. (a flag must
      // be raised so that when edition is done, we actually create the folder.
      m_creatingItem = true;
      
      AbstractListViewItem item = new TarArchiveListViewItem( tarFile, parentNode );
      FileView.Items.Add( item );

      item.BeginEdit();
    }

    private void CreateZipArchive()
    {
      string archiveName = "New Zip archive";
      string archiveExtension = ".zip";
      string archiveNameTemplate = archiveName + " ({0})" + archiveExtension;
      int archiveNameIndex = 2;

      // Get a reference on the selected node.
      AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;

      // The folder might not support file creation.
      AbstractFile zipFile = null;

      try
      {
        zipFile = parentNode.Folder.GetFile( archiveName + archiveExtension );

        // We want a file that does not already exists.
        while( zipFile.Exists )
        {
          zipFile = parentNode.Folder.GetFile( string.Format( archiveNameTemplate, archiveNameIndex++ ) );
        }
      }
      catch 
      {
        MessageBox.Show( this, "Cannot create files in this folder." );
        return;
      }

      // Create an AbstractListViewItem and put it in edit mode. (a flag must
      // be raised so that when edition is done, we actually create the folder.
      m_creatingItem = true;
      
      AbstractListViewItem item = new ZipArchiveListViewItem( zipFile, parentNode );
      FileView.Items.Add( item );

      item.BeginEdit();
    }

    private void CreateFtpConnection()
    {
      using( FtpConnectionInformationForm ftpConnectionInfo = new FtpConnectionInformationForm() )
      {
        FtpConnection ftpConnection;

        // We need information from the user.
        if( ftpConnectionInfo.ShowDialog( this, out ftpConnection ) == DialogResult.OK )
        {
          AbstractTreeViewNode node = ( AbstractTreeViewNode )FolderTree.SelectedNode;
          FtpConnectionsFolder ftpFolders = node.Folder as FtpConnectionsFolder;

          ftpConnection.CertificateReceived += new CertificateReceivedEventHandler( FtpConnection_CertificateReceived );
          ftpConnection.SynchronizingObject = this;

          try
          {
            // Create the ftp connection. 
            FtpConnectionFolder ftpFolder = ftpFolders.CreateConnectionFolder( ftpConnection );

            // Add the connection in the FileView.
            FileView.Items.Add( new FolderListViewItem( ftpFolder, node ) );

            // Add the connection in the FolderTree.
            node.Nodes.Add( new FolderTreeViewNode( ftpFolder, FileView ) );

            if( !node.IsExpanded )
              node.Expand();
          }
          catch
          {
            MessageBox.Show( this, "Can't create ftp connection.", "Ftp connection error", MessageBoxButtons.OK, MessageBoxIcon.Error ); 
          }
        }
      }
    }

    private void CopyItems()
    {
      m_clipboardCutItems = false;

      this.FillClipboard();
    }

    private void CutItems()
    {
      m_clipboardCutItems = true;

      this.FillClipboard();
    }

    private void DeleteItems()
    {
      if( FileView.SelectedItems.Count > 0 )
      {
        bool deleteConfirmed = false;

        // When there are multiple items to delete, we only want to show one message.
        if( FileView.SelectedItems.Count > 1 )
        {
          string message = string.Format( "Are you sure you want to delete these {0} items?", FileView.SelectedItems.Count );
          string caption = "Confirm Multiple File Delete";

          deleteConfirmed = true;

          if( MessageBox.Show( this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 ) == DialogResult.No )
            return;
        }

        FileView.BeginUpdate();

        foreach( AbstractListViewItem item in FileView.SelectedItems )
        {
          try
          {
            item.Delete( !deleteConfirmed );
          }
          catch
          {
            MessageBox.Show( 
              this, 
              string.Format( "The item {0} could not be deleted.", item.Text ), 
              "Error deleting item", 
              MessageBoxButtons.OK, 
              MessageBoxIcon.Error );
          }
        }

        FileView.EndUpdate();
      }
    }

    private void FillClipboard()
    {
      if( FileView.SelectedItems.Count > 0 )
      {
        AbstractListViewItem[] items = new AbstractListViewItem[ FileView.SelectedItems.Count ];

        for( int i = 0; i < FileView.SelectedItems.Count; i++ )
        {
          items[ i ] = ( AbstractListViewItem )FileView.SelectedItems[ i ];
        }

        m_clipBoard = new DataObject( items );
      }

      this.UpdateToolbarState();
    }

    private void OpenItem()
    {
      Cursor.Current = Cursors.WaitCursor;

      if( FileView.SelectedItems.Count > 0 )
      {
        AbstractListViewItem selectedItem = ( AbstractListViewItem )FileView.SelectedItems[ 0 ];

        if( selectedItem == null )
          return;

        if( selectedItem.ParentNode != null )
        {
          // Dealing with a folder, we will navigate to it in the navigation treeview.
          AbstractTreeViewNode matchingNode = this.FindMatchingNode( selectedItem.ParentNode, selectedItem.FileSystemItem );
          
          if( matchingNode != null )
          {
            matchingNode.EnsureVisible();
            FolderTree.SelectedNode = matchingNode;
            matchingNode.Expand();
          }
        }
        else 
        {
          string fileName = selectedItem.FileSystemItem.FullName;

          if( !( selectedItem.FileSystemItem is DiskFile ) )
          {
            // If the file is not already a local file, we need to copy that 
            // file to a temporary directory before launching the process. 

            using( TemporaryFileCopyActionForm actionForm = new TemporaryFileCopyActionForm() )
            {
              bool fileOnly;
              bool recursive;

              // Ask the user what files should be copied locally.
              if( actionForm.ShowDialog( this, out fileOnly, out recursive ) == DialogResult.Cancel )
                return;

              FileSystemEvents events = new FileSystemEvents();
              events.ByteProgression += new ByteProgressionEventHandler( Copy_ByteProgression );
              events.ItemException += new ItemExceptionEventHandler( Copy_ItemException );

              try
              {
                string tempFolderName = System.IO.Path.GetTempPath() + Guid.NewGuid();
                DiskFolder tempFolder = new DiskFolder( tempFolderName );

                // This will make the file to be destroyed when the application closes.
                AutoDeleter autoDeleter = new AutoDeleter( tempFolder );

                using( ProgressionForm progression = new ProgressionForm() )
                {
                  progression.ActionText = "Copying file...";
                  progression.Show();

                  if( fileOnly )
                  {
                    // Only the selected file. Just copy it to the temp directory.
                    fileName = selectedItem.FileSystemItem.CopyTo( events, progression, tempFolder, true ).FullName;
                  }
                  else
                  {
                    // Copy the files to the temporary folder.
                    selectedItem.FileSystemItem.ParentFolder.CopyFilesTo( events, progression, tempFolder, recursive, true );

                    // We need to have the new fullname of the file to execute.
                    fileName = tempFolder.GetFile( selectedItem.FileSystemItem.Name ).FullName;
                  }
                }
              }
              catch( System.Reflection.TargetInvocationException )
              {
                // Operation aborted.
              }
              catch
              {
                MessageBox.Show( this, "An error occured while copying files.", "Error copying files", MessageBoxButtons.OK, MessageBoxIcon.Error );
              }
              finally
              {
                events.ByteProgression -= new ByteProgressionEventHandler( Copy_ByteProgression );
                events.ItemException -= new ItemExceptionEventHandler( Copy_ItemException );
              }
            }
          }

          try
          {
            // Start the process.
            System.Diagnostics.Process.Start( fileName );
          }
          catch{}
        }
      }

      Cursor.Current = Cursors.Default;
    }

    private void PasteItems()
    {
      // Get the destination.
      AbstractFolder destination = 
        ( ( AbstractTreeViewNode )FolderTree.SelectedNode ).Folder;

      this.PasteItems( destination );
    }

    private void PasteItems( AbstractFolder destination )
    {
      if( destination == null )
        return;

      Cursor.Current = Cursors.WaitCursor;

      // If destination is a ZIP, we need to refresh the options since they might
      // have been changed.
      ZipArchive archive = destination as ZipArchive;

      if( archive != null )
      {
        archive.DefaultCompressionLevel = Options.ZipDefaultCompressionLevel;
        archive.DefaultCompressionMethod = Options.ZipDefaultCompressionMethod;
        archive.DefaultEncryptionPassword = Options.ZipDefaultEncryptionPassword;
        archive.DefaultEncryptionMethod = Options.ZipDefaultEncryptionMethod;
        archive.DefaultEncryptionStrength = Options.ZipDefaultEncryptionStrength;
      }

      bool needRefresh = false;

      // Check to see if the clipboard contains valid objects.
      if( !m_clipBoard.GetDataPresent( typeof( AbstractListViewItem[] ) ) )
        return;

      // Initialize the FileSystem events.
      FileSystemEvents copyEvents = new FileSystemEvents();
      copyEvents.ItemException += new ItemExceptionEventHandler( Copy_ItemException );
      copyEvents.ByteProgression += new ByteProgressionEventHandler( Copy_ByteProgression );

      FileSystemEvents moveEvents = new FileSystemEvents();
      moveEvents.ItemException += new ItemExceptionEventHandler( Move_ItemException );
      moveEvents.ByteProgression += new ByteProgressionEventHandler( Move_ByteProgression );

      try
      {
        AbstractListViewItem[] items = ( AbstractListViewItem[] )m_clipBoard.GetData( typeof( AbstractListViewItem[] ) );

        // Paste each items in the destination. 
        foreach( AbstractListViewItem item in items )
        {
          bool exists = false; 
          bool sourceEqualsDestination = 
            ( item.FileSystemItem.ParentFolder.FullName.ToLower() == destination.FullName.ToLower() );

          // Check to see if the item is already present.
          try
          {
            exists = 
              ( destination.GetFile( item.FileSystemItem.Name ).Exists || destination.GetFolder( item.FileSystemItem.Name ).Exists );
          }
          catch {}

          if( exists )
          {
            // We don't ask a question for replacing the item if the source and destination folder is the same.
            // When cutting and pasting over ourself, we do nothing and proceed to the next item.
            if( sourceEqualsDestination )
            {
              if( m_clipboardCutItems )
                break;
            }
            else
            {
              string message = "Do you want to replace the existing item?";
              string caption = "Replace confirmation";

              if( MessageBox.Show( this, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.No )
                break;
            }
          }

          if( m_clipboardCutItems )
          {
            using( ProgressionForm progression = new ProgressionForm() )
            {
              progression.ActionText = "Moving file...";
              progression.Show();

              try
              {
                item.FileSystemItem.MoveTo( moveEvents, progression, destination, true );
              }
              catch( System.Reflection.TargetInvocationException )
              {
                // Operation aborted.
              }
              catch
              {
                MessageBox.Show( this, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
              }
            }
          }
          else
          {
            // If the item is copied and pasted in place, we will add "Copy of" as prefix for the name.
            if( exists && sourceEqualsDestination )
            {
              string prefix = "Copy of ";
              int prefixIndex = 1;
              string name = prefix + item.FileSystemItem.Name;

              while( destination.GetFile( name ).Exists || destination.GetFolder( name ).Exists )
              {
                prefixIndex++;
                prefix = string.Format( "Copy ({0}) of ", prefixIndex );
                name = prefix + item.FileSystemItem.Name;
              }

              AbstractFile currentFile = item.FileSystemItem as AbstractFile;

              if( currentFile != null )
              {
                AbstractFile newFile = destination.GetFile( name );

                using( ProgressionForm progression = new ProgressionForm() )
                {
                  progression.ActionText = "Copying file...";
                  progression.Show();

                  try
                  {
                    currentFile.CopyTo( copyEvents, progression, newFile, false );
                  }
                  catch( System.Reflection.TargetInvocationException )
                  {
                    // Operation aborted.
                  }
                  catch
                  {
                    MessageBox.Show( this, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                  }
                }
              }
              else
              {
                AbstractFolder currentFolder = item.FileSystemItem as AbstractFolder;
                AbstractFolder newFolder = destination.GetFolder( name );

                using( ProgressionForm progression = new ProgressionForm() )
                {
                  progression.ActionText = "Copying file...";
                  progression.Show();

                  try
                  {
                    currentFolder.CopyTo( copyEvents, progression, newFolder, false );
                  }
                  catch( System.Reflection.TargetInvocationException )
                  {
                    // Operation aborted.
                  }
                  catch( Exception )
                  {
                    MessageBox.Show( this, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                  }
                }
              }
            }
            else
            {
              using( ProgressionForm progression = new ProgressionForm() )
              {
                progression.ActionText = "Copying file...";
                progression.Show();

                try
                {
                  item.FileSystemItem.CopyTo( copyEvents, progression, destination, true );
                }
                catch( System.Reflection.TargetInvocationException )
                {
                  // Operation aborted.
                }
                catch( Exception )
                {
                  MessageBox.Show( this, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
              }
            }
          }

          needRefresh = true;
        }

        if( m_clipboardCutItems )
        {
          m_clipBoard = null;
          m_clipboardCutItems = false;
        }

        if( needRefresh )
        {
          this.FillFolderTreeNode( ( AbstractTreeViewNode )FolderTree.SelectedNode );
          this.FillListView( ( AbstractTreeViewNode )FolderTree.SelectedNode );
        }
      }
      finally
      {
        copyEvents.ByteProgression -= new ByteProgressionEventHandler( Copy_ByteProgression );
        copyEvents.ItemException -= new ItemExceptionEventHandler( Copy_ItemException );

        moveEvents.ByteProgression -= new ByteProgressionEventHandler( Move_ByteProgression );
        moveEvents.ItemException -= new ItemExceptionEventHandler( Move_ItemException );
      }

      Cursor.Current = Cursors.Default;
    }

    private void FillListView( AbstractTreeViewNode parentNode )
    {
      Cursor.Current = Cursors.WaitCursor;

      FileView.BeginUpdate();

      FileView.RemoveSort();

      // Clear the actual list.
      FileView.Items.Clear();

      AbstractFolder parentFolder = parentNode.Folder;
    
      if( parentFolder != null )
      {
        // Show/hide the CompressedSize column.
        int compressedSizeColumnSize = 0;

        if( parentFolder.RootFolder.HostFile != null )
        {
          compressedSizeColumnSize = FileView.Columns[ 2 ].Width;
  
          if( compressedSizeColumnSize == 0 )
            compressedSizeColumnSize = 100;
        }

        FileView.Columns[ 2 ].Width = compressedSizeColumnSize;

        try
        {
          // Get a list of items from the parent folder.
          FileSystemItem[] items = parentFolder.GetItems( false );

          // Set defaults settings
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles;
          ZipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders;

          // Loop through the items and create an AbstractListViewItem for each.
          foreach( FileSystemItem item in items )
          {
            AbstractListViewItem listViewItem = null;
            bool itemCreated = false;

            // Folders are created as is. Nothing special to do.
            AbstractFolder folder = item as AbstractFolder;

            if( folder != null )
            {
              listViewItem = new FolderListViewItem( folder, parentNode );
              itemCreated = true;
            }
            else
            {
              // We are obviously a file. We must create a specialized AbstractFileListViewItem
              // for archives. Those files can also be encrypted inside a Zip archive. We must
              // handle those encrypted files and ask the user for a password if needed.
              ItemExceptionAction exceptionAction = ItemExceptionAction.Retry;

              while(  ( exceptionAction == ItemExceptionAction.Retry )
                &&    ( !itemCreated ) )
              {
                try
                {
                  string extension = Path.GetExtension( item.FullName );
                  AbstractTreeViewNode matchingNode = this.FindMatchingNode( parentNode, item );
                  AbstractFile file = item as AbstractFile;

                  switch( extension.ToUpper() )
                  {
                      // GZip archive
                    case ".GZ":
                    case ".TGZ":
                      listViewItem = new GZipArchiveListViewItem( file, parentNode );
                      itemCreated = true;
                      break;

                      // Tar archive
                    case ".TAR":
                      listViewItem = new TarArchiveListViewItem( file, parentNode );
                      itemCreated = true;
                      break;

                      // Zip archive
                    case ".ZIP":
                      listViewItem = new ZipArchiveListViewItem( file, parentNode );
                      itemCreated = true;
                      break;

                      // Regular file
                    default:
                      listViewItem = new FileListViewItem( file );
                      itemCreated = true;
                      break;
                  }
                }
                catch( InvalidDecryptionPasswordException decryptionPasswordExcept )
                {
                  // Make sure we are dealing with a ZippedFile.
                  if( item is ZippedFile )
                  {
                    // We need a decryption password. Ask the user.
                    using( InputPasswordForm passwordForm = new InputPasswordForm() )
                    {
                      if( passwordForm.ShowDialog( this, decryptionPasswordExcept.Item.FullName ) == DialogResult.OK )
                      {
                        // Set the decryption password on the ZipArchive instance and retry the operation.
                        ZipArchive zipArchive = item.RootFolder as ZipArchive;
                        if( zipArchive != null )
                        {
                          zipArchive.DefaultDecryptionPassword = passwordForm.Password;
                          exceptionAction = ItemExceptionAction.Retry;
                        }
                      }
                      else
                      {
                        // User cancelled, we just ignore this item and proceed to the next one.
                        exceptionAction = ItemExceptionAction.Ignore;
                      }
                    }
                  }
                }
              }
            }

            // Add the AbstractListViewItem item to the list.
            if( itemCreated )
              FileView.Items.Add( listViewItem ); 
          }
        }
        catch( Exception except )
        {
          System.Diagnostics.Debug.WriteLine( except.Message );
        }
      }
      
      FileView.Sort( 0 );

      FileView.EndUpdate();

      Cursor.Current = Cursors.Default;

      // Update the toolbar state.
      this.UpdateToolbarState();

      // Refresh the status bar.
      this.UpdateStatusBarInformation( parentNode );

      // Start refreshing the icons.
      FileView.RefreshIcons();
    }

    private void FillFolderTreeNode( AbstractTreeViewNode node )
    {
      Cursor.Current = Cursors.WaitCursor;

      FolderTree.BeginUpdate();

      // Remove any child nodes.
      node.Nodes.Clear();

      // Fill the node with child items.

      try
      {
        node.InitializeFolder();

        if( node.Folder != null )
        {
          // Get a list of items from the parent folder.
          FileSystemItem[] items = node.Folder.GetItems( false );

          // Set defaults settings
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles;
          ZipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders;

          // Loop through the items and create an AbstractListViewItem for each.
          foreach( FileSystemItem item in items )
          {
            AbstractTreeViewNode folderNode = null;
            bool itemCreated = false;

            // Folders are created as is. Nothing special to do.
            AbstractFolder folder = item as AbstractFolder;

            if( folder != null )
            {
              folderNode = new FolderTreeViewNode( folder, FileView );
              itemCreated = true;
            }
            else
            {
              // We are obviously a file. The only files that interests us are archives.
              // A regular file is not represented in the FolderTree. Those files can also 
              // be encrypted inside a Zip archive. We must handle those encrypted files and 
              //ask the user for a password if needed.
              ItemExceptionAction exceptionAction = ItemExceptionAction.Retry;

              while(  ( exceptionAction == ItemExceptionAction.Retry )
                &&    ( !itemCreated ) )
              {
                try
                {
                  string extension = Path.GetExtension( item.FullName );
                  AbstractFile file = item as AbstractFile;

                  switch( extension.ToUpper() )
                  {
                      // GZip archive
                    case ".GZ":
                    case ".TGZ":
                      folderNode = new GZipArchiveTreeViewNode( file, FileView );
                      itemCreated = true;

                      break;

                      // Tar archive
                    case ".TAR":
                      folderNode = new TarArchiveTreeViewNode( file, FileView );
                      itemCreated = true;

                      break;

                      // Zip archive
                    case ".ZIP":
                      folderNode = new ZipArchiveTreeViewNode( file, FileView );
                      itemCreated = true;

                      break;

                      // Regular file
                    default:
                      // Skip the file
                      exceptionAction = ItemExceptionAction.Ignore;
                      break;
                  }
                }
                catch( InvalidDecryptionPasswordException decryptionPasswordExcept )
                {
                  // Make sure we are dealing with a ZippedFile.
                  if( item is ZippedFile )
                  {
                    // We need a decryption password. Ask the user.
                    using( InputPasswordForm passwordForm = new InputPasswordForm() )
                    {
                      if( passwordForm.ShowDialog( this, decryptionPasswordExcept.Item.FullName ) == DialogResult.OK )
                      {
                        // Set the decryption password on the ZipArchive instance and retry the operation.
                        ZipArchive zipArchive = item.RootFolder as ZipArchive;
                        if( zipArchive != null )
                        {
                          zipArchive.DefaultDecryptionPassword = passwordForm.Password;
                          exceptionAction = ItemExceptionAction.Retry;
                        }
                      }
                      else
                      {
                        // User cancelled, we just ignore this item and proceed to the next one.
                        exceptionAction = ItemExceptionAction.Ignore;
                      }
                    }
                  }
                }
              }
            }

            // Add the AbstractTreeViewNode item to the list.
            if( itemCreated )
              node.Nodes.Add( folderNode ); 
          }
        }
      }
      catch( Exception except )
      {
        System.Diagnostics.Debug.WriteLine( except.Message );
      }
      finally
      {
        FolderTree.EndUpdate();

        Cursor.Current = Cursors.Default;
      }

      node.RefreshIcons();
    }

    private AbstractTreeViewNode FindMatchingNode( AbstractTreeViewNode parentNode, FileSystemItem item )
    {
      string name = item.Name;

      AbstractFolder folder = item as AbstractFolder;

      if(  ( folder != null ) 
        && ( folder.IsRoot ) )
      {
        name = item.FullName;
      }

      for( int i = 0; i < parentNode.Nodes.Count; i++ )
      {
        if( parentNode.Nodes[ i ].Text == name )
        {
          return parentNode.Nodes[ i ] as AbstractTreeViewNode;
        }
      }

      return null;
    }

    private void RenameItem()
    {
      // Rename the selected item.
      if( FileView.SelectedItems.Count > 0 )
        FileView.SelectedItems[ 0 ].BeginEdit();
    }

    private void ToggleFolderView()
    {
      // Show/hide the folder treeview.

      bool visible = FoldersTool.Checked;

      FolderTree.Visible = visible;
      splitter1.Visible = visible;
    }

    private void UpdateStatusBarInformation( AbstractTreeViewNode node )
    {
      try
      {
        // Objects
        FileSystemItem[] items = node.Folder.GetItems( false );
        ObjectPanel.Text = items.Length + " object" + ( items.Length > 0 ? "s" : "" );

        // Total size
        AbstractFile[] files = node.Folder.GetFiles( false );
      
        double totalSize = 0;
        foreach( AbstractFile file in files )
        {
          totalSize += file.Size;
        }

        string formattedSize = string.Empty;

        if( totalSize > 1073741824 )    // GB
        {
          totalSize = System.Math.Round( totalSize / 1073741824, 2 );
          formattedSize = totalSize.ToString( "#,##0.##" ) + " GB";
        }
        else if( totalSize > 1048576 )  // MB
        {
          totalSize = System.Math.Round( totalSize / 1048576, 2 );
          formattedSize = totalSize.ToString( "#,##0.##" ) + " MB";
        }
        else if( totalSize > 1024 )     // KB
        {
          totalSize = System.Math.Round( totalSize / 1024, 2 );
          formattedSize = totalSize.ToString( "#,##0.##" ) + " KB";
        }
        else                            // bytes
        {
          totalSize = System.Math.Round( totalSize, 0 );
          formattedSize = totalSize.ToString( "#,##0" ) + " bytes";
        }

        SizePanel.Text = formattedSize;

        // Location
        AbstractTreeViewNode rootNode = node.RootNode;
        LocationPanel.Text = rootNode.Text;
        LocationPanel.ImageIndex = rootNode.ImageIndex;
      }
      catch{}
    }

    private void UpdateToolbarState()
    {
      // Up tool and new menu button
      UpTool.Enabled = false;
      
      NewFileMenu.Enabled = false;
      NewFolderMenu.Enabled = false;
      NewZipArchiveMenu.Enabled = false;
      NewTarArchiveMenu.Enabled = false;
      NewGZipArchiveMenu.Enabled = false;

      NewFtpConnectionMenu.Enabled = false;

      AbstractTreeViewNode selectedNode = ( AbstractTreeViewNode )FolderTree.SelectedNode;
      if( selectedNode != null )
      {
        UpTool.Enabled = selectedNode.UpToolEnabled;
        
        NewFileMenu.Enabled = selectedNode.NewToolEnabled;
        NewFolderMenu.Enabled = selectedNode.NewToolEnabled;
        NewZipArchiveMenu.Enabled = selectedNode.NewToolEnabled;
        NewTarArchiveMenu.Enabled = selectedNode.NewToolEnabled;
        NewGZipArchiveMenu.Enabled = selectedNode.NewToolEnabled;

        NewFtpConnectionMenu.Enabled = selectedNode.NewFtpConnectionToolEnabled;
      }

      // Copy, Cut, Paste tool and Delete button
      CopyTool.Enabled = false;
      CopyMenu.Enabled = false;

      CutTool.Enabled = false;
      CutMenu.Enabled = false;

      PasteTool.Enabled = false;
      PasteMenu.Enabled = false;

      DeleteTool.Enabled = false;
      DeleteMenu.Enabled = false;

      RenameTool.Enabled = false;
      RenameMenu.Enabled = false;

      PropertiesMenu.Enabled = false;

      OpenMenu.Enabled = false;

      if( FileView.SelectedItems.Count > 0 )
      {
        AbstractListViewItem item = FileView.SelectedItems[ 0 ] as AbstractListViewItem;

        CopyTool.Enabled = item.CopyToolEnabled;
        CopyMenu.Enabled = CopyTool.Enabled;

        CutTool.Enabled = item.CutToolEnabled;
        CutMenu.Enabled = CutTool.Enabled;

        DeleteTool.Enabled = item.DeleteToolEnabled;
        DeleteMenu.Enabled = DeleteTool.Enabled;

        RenameTool.Enabled = item.RenameToolEnabled;
        RenameMenu.Enabled = RenameTool.Enabled;

        PropertiesMenu.Enabled = true;

        OpenMenu.Enabled = true;
      }

      if(  m_clipBoard != null 
        && m_clipBoard.GetDataPresent( typeof( AbstractListViewItem[] ) )
        && FolderTree.SelectedNode != null )
      {
        PasteTool.Enabled = ( ( AbstractTreeViewNode )FolderTree.SelectedNode ).PasteToolEnabled;
        PasteMenu.Enabled = PasteTool.Enabled;
      }
    }

    private void ShowItemProperties()
    {
      // Show the FileSystemItem item's properties.

      FileSystemItem item = ( ( AbstractListViewItem )FileView.SelectedItems[ 0 ] ).FileSystemItem;

      using( PropertyForm propertyForm = new PropertyForm( item ) )
      {
        if( propertyForm.ShowDialog( this ) == DialogResult.OK )
        {
          ( ( AbstractListViewItem )FileView.SelectedItems[ 0 ] ).Refresh( item );
        }
      }
    }

    #endregion PRIVATE METHODS

    #region FILESYSTEM EVENTS

    private void Copy_ByteProgression( object sender, ByteProgressionEventArgs e )
    {
      // Increment the copy progression bar.

      ProgressionForm progression = e.UserData as ProgressionForm;

      if( progression == null )
        return;

      if( progression.UserCancelled )
        throw new ApplicationException( "The user cancelled the operation." );

      if( e.AllFilesBytes.Percent == 0 )
        m_previousProgressionPct = 0;

      if( ( e.AllFilesBytes.Percent - m_previousProgressionPct ) > 0 )
      {
        progression.CurrentProgressValue = e.CurrentFileBytes.Percent;
        progression.TotalProgressValue = e.AllFilesBytes.Percent;
        progression.FromText = e.CurrentItem.FullName;
        progression.ToText = e.TargetItem.FullName;

        m_previousProgressionPct = e.AllFilesBytes.Percent;
      }

      Application.DoEvents();
    }

    private void Copy_ItemException( object sender, ItemExceptionEventArgs e )
    {
      // Operation aborted.
      if( e.Exception is System.Reflection.TargetInvocationException )
      {
        return;
      }

      // When overwriting a item that have the "system" attribute set, the system will throw an exception.
      // Therefore, we will delete the target item before retrying the copy.
      if( e.Exception is System.UnauthorizedAccessException )
      {
        if( e.CurrentItem.HasAttributes && ( ( e.CurrentItem.Attributes & FileAttributes.System ) == FileAttributes.System ) )
        {
          e.TargetItem.Delete();
          e.Action = ItemExceptionAction.Retry;
          return;
        }
      }

      // ZipArchive can be password protected. We need to ask the user to input that password.
      if( e.Exception is Xceed.Zip.InvalidDecryptionPasswordException )
      {
        using( InputPasswordForm passwordForm = new InputPasswordForm() )
        {
          if( passwordForm.ShowDialog( this, e.CurrentItem.FullName ) == DialogResult.OK )
          {
            if( ( e.CurrentItem is ZippedFile ) || ( e.CurrentItem is ZippedFolder ) )
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

      string message = string.Format( 
        "An error occured while copying the item {0}.", 
        e.CurrentItem.FullName );

      DialogResult result = MessageBox.Show( this, message, "Error Copying Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error );

      switch( result )
      {
        case DialogResult.Ignore:
          e.Action = ItemExceptionAction.Ignore;
          break;

        case DialogResult.Retry:
          e.Action = ItemExceptionAction.Retry;
          break;

        case DialogResult.Abort:
          e.Action = ItemExceptionAction.Abort;
          break;
      }
    }

    private void Move_ByteProgression( object sender, ByteProgressionEventArgs e )
    {
      // Increment the copy progression bar.

      ProgressionForm progression = e.UserData as ProgressionForm;

      if( progression == null )
        return;

      if( progression.UserCancelled )
        throw new ApplicationException( "The user cancelled the operation." );

      if( e.AllFilesBytes.Percent == 0 )
        m_previousProgressionPct = 0;

      if( ( e.AllFilesBytes.Percent - m_previousProgressionPct ) > 0 )
      {
        progression.CurrentProgressValue = e.CurrentFileBytes.Percent;
        progression.TotalProgressValue = e.AllFilesBytes.Percent;
        progression.FromText = e.CurrentItem.FullName;
        progression.ToText = e.TargetItem.FullName;

        m_previousProgressionPct = e.AllFilesBytes.Percent;
      }

      Application.DoEvents();
    }

    private void Move_ItemException( object sender, ItemExceptionEventArgs e )
    {
      // Operation aborted.
      if( e.Exception is System.Reflection.TargetInvocationException )
      {
        return;
      }

      // When overwriting a item that have the "system" attribute set, the system will throw an exception.
      // Therefore, we will delete the target item before retrying the copy.
      if( e.Exception is System.UnauthorizedAccessException )
      {
        if( e.CurrentItem.HasAttributes && ( ( e.CurrentItem.Attributes & FileAttributes.System ) == FileAttributes.System ) )
        {
          e.TargetItem.Delete();
          e.Action = ItemExceptionAction.Retry;
          return;
        }
      }

      // ZipArchive can be password protected. We need to ask the user to input that password.
      if( e.Exception is Xceed.Zip.InvalidDecryptionPasswordException )
      {
        using( InputPasswordForm passwordForm = new InputPasswordForm() )
        {
          if( passwordForm.ShowDialog( this, e.CurrentItem.FullName ) == DialogResult.OK )
          {
            if( ( e.CurrentItem is ZippedFile ) || ( e.CurrentItem is ZippedFolder ) )
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

      string message = string.Format( 
        "An error occured while moving the item {0}.", 
        e.CurrentItem.FullName );

      DialogResult result = MessageBox.Show( this, message, "Error Moving Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error );

      switch( result )
      {
        case DialogResult.Ignore:
          e.Action = ItemExceptionAction.Ignore;
          break;

        case DialogResult.Retry:
          e.Action = ItemExceptionAction.Retry;
          break;

        case DialogResult.Abort:
          e.Action = ItemExceptionAction.Abort;
          break;
      }
    }

    #endregion FILESYSTEM EVENTS

    #region FORM EVENTS

    private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      // Clear the icon cahce.
      IconCache.ClearCache();
    }

    #endregion FORM EVENTS

    #region MENU EVENTS

    private void CopyMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Copy the selected items.
      this.CopyItems();
    }

    private void CutMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Cut the selected items.
      this.CutItems();
    }

    private void DeleteMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Delete the selected items.
      this.DeleteItems();
    }

    private void PasteMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Paste the items from the clipboard.
      this.PasteItems();
    }

    private void RenameMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Rename the selected item.
      this.RenameItem();
    }

    private void OpenMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Opent the selected item.
      this.OpenItem();
    }

    private void PropertiesMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Show the item's properties.
      this.ShowItemProperties();
    }

    private void DetailsMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Update the view menus check state to "Details".
      IconsMenu.Checked = false;
      IconsToolbarMenu.Checked = false;

      ListMenu.Checked = false;
      ListToolbarMenu.Checked = false;

      DetailsMenu.Checked = true;
      DetailsToolbarMenu.Checked = true;

      // Change the view state of the ListView to "Details".
      FileView.View = View.Details;
    }

    private void IconsMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Update the view menus check state to "Icons".
      IconsMenu.Checked = true;
      IconsToolbarMenu.Checked = true;

      ListMenu.Checked = false;
      ListToolbarMenu.Checked = false;

      DetailsMenu.Checked = false;
      DetailsToolbarMenu.Checked = false;

      // Change the view state of the ListView to "Icons".
      FileView.View = View.LargeIcon;
    }

    private void ListMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Update the view menus check state to "List".
      IconsMenu.Checked = false;
      IconsToolbarMenu.Checked = false;

      ListMenu.Checked = true;
      ListToolbarMenu.Checked = true;

      DetailsMenu.Checked = false;
      DetailsToolbarMenu.Checked = false;

      // Change the view state of the ListView to "List".
      FileView.View = View.List;
    }

    private void NewFileMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new text file.
      this.CreateFile();
    }

    private void NewFolderMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new folder.
      this.CreateFolder();
    }

    private void NewGZipArchiveMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new GZip archive.
      this.CreateGZipArchive();
    }

    private void NewTarArchiveMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new Tar archive.
      this.CreateTarArchive();
    }

    private void NewZipArchiveMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new Zip archive.
      this.CreateZipArchive();
    }

    private void NewFtpConnectionMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Create a new Ftp connection.
      this.CreateFtpConnection();
    }

    private void OptionsMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Show the application's options.
      using( OptionsForm optionsForm = new OptionsForm() )
      {
        if( optionsForm.ShowDialog( this ) == DialogResult.OK )
        {
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles;
          ZipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders;
        }
      }
    }

    private void RefreshMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      AbstractTreeViewNode selectedNode = FolderTree.SelectedNode as AbstractTreeViewNode;

      if( selectedNode != null )
      {
        // Refresh the folder tree and the file list.
        this.FillFolderTreeNode( selectedNode );
        this.FillListView( selectedNode );
      }
    }

    private void ExitMenu_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Quit.
      Application.Exit();
    }

    #endregion MENU EVENTS

    #region CONTEXT MENU EVENTS

    private void CopyContextMenu_Click(object sender, System.EventArgs e)
    {
      // Copy the selected items.
      this.CopyItems();
    }

    private void CutContextMenu_Click(object sender, System.EventArgs e)
    {
      // Cut the selected items.
      this.CutItems();
    }

    private void DeleteContextMenu_Click(object sender, System.EventArgs e)
    {
      // Delete the selected items.
      this.DeleteItems();
    }

    private void PasteContextMenu_Click(object sender, System.EventArgs e)
    {
      // Paste the items from the clipboard.
      this.PasteItems();
    }

    private void RenameContextMenu_Click(object sender, System.EventArgs e)
    {
      // Rename the selected item.
      this.RenameItem();
    }

    private void OpenContextMenu_Click(object sender, System.EventArgs e)
    {
      // Open the selected item.
      this.OpenItem();
    }

    private void PropertiesContextMenu_Click(object sender, System.EventArgs e)
    {
      // Show the item's properties.
      this.ShowItemProperties();
    }

    private void NewFileContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new text file.
      this.CreateFile();
    }

    private void NewFolderContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new folder.
      this.CreateFolder();
    }

    private void NewGZipArchiveContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new GZip archive.
      this.CreateGZipArchive();
    }

    private void NewTarArchiveContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new Tar archive.
      this.CreateTarArchive();
    }

    private void NewZipArchiveContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new Zip archive.
      this.CreateZipArchive();
    }

    private void NewFtpConnectionContextMenu_Click(object sender, System.EventArgs e)
    {
      // Create a new Ftp connection.
      this.CreateFtpConnection();
    }

    private void EditContextMenu_Popup(object sender, System.EventArgs e)
    {
      // Enabled or disabled context menus depending on the selected item.

      CopyContextMenu.Enabled = false;
      CutContextMenu.Enabled = false;
      DeleteContextMenu.Enabled = false;
      PasteContextMenu.Enabled = false;
      RenameContextMenu.Enabled = false;
      PropertiesContextMenu.Enabled = false;
      OpenContextMenu.Enabled = false;

      NewFileContextMenu.Enabled = false;
      NewFolderContextMenu.Enabled = false;
      NewZipArchiveContextMenu.Enabled = false;
      NewTarArchiveContextMenu.Enabled = false;
      NewGZipArchiveContextMenu.Enabled = false;

      NewFtpConnectionContextMenu.Enabled = false;

      AbstractTreeViewNode node = FolderTree.SelectedNode as AbstractTreeViewNode;

      if(  ( m_clipBoard != null )
        && m_clipBoard.GetDataPresent( typeof( AbstractListViewItem[] ) ) )
      {
        PasteContextMenu.Enabled = true;
      }

      if( FileView.SelectedItems.Count > 0 )
      {
        AbstractListViewItem item = FileView.SelectedItems[ 0 ] as AbstractListViewItem;

        if( item != null )
        {
          CopyContextMenu.Enabled = item.CopyToolEnabled;
          CutContextMenu.Enabled = item.CutToolEnabled;
          DeleteContextMenu.Enabled = item.DeleteToolEnabled;
          RenameContextMenu.Enabled = item.RenameToolEnabled;
          PropertiesContextMenu.Enabled = true;
          OpenContextMenu.Enabled = true;

          if( ( item.FileSystemItem as AbstractFolder ) == null )
            PasteContextMenu.Enabled = false;
        }
      }
      
      if( PasteContextMenu.Enabled )
      {
        if( node != null && !node.PasteToolEnabled )
          PasteContextMenu.Enabled = false;
      }

      if( node != null )
      {
        NewFileContextMenu.Enabled = node.NewToolEnabled;
        NewFolderContextMenu.Enabled = node.NewToolEnabled;
        NewZipArchiveContextMenu.Enabled = node.NewToolEnabled;
        NewTarArchiveContextMenu.Enabled = node.NewToolEnabled;
        NewGZipArchiveContextMenu.Enabled = node.NewToolEnabled;

        NewFtpConnectionContextMenu.Enabled = node.NewFtpConnectionToolEnabled;
      }
    }

    #endregion CONTEXT MENU EVENTS

    #region TOOLBAR EVENTS

    private void CopyTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Copy the selected items.
      this.CopyItems();
    }

    private void CutTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Cut the selected items.
      this.CutItems();
    }

    private void DeleteTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Delete the selected items.
      this.DeleteItems();
    } 

    private void PasteTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Paste teh items from the clipboard.
      this.PasteItems();
    }

    private void RenameTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Rename the selected item.
      this.RenameItem();
    }

    private void UpTool_Click(object sender, Xceed.SmartUI.SmartItemClickEventArgs e)
    {
      // Go back to the parent node.

      TreeNode parentNode = FolderTree.SelectedNode.Parent;

      parentNode.EnsureVisible();
      FolderTree.SelectedNode = parentNode;
      parentNode.Expand();
    }

    private void FoldersTool_CheckedChanged(object sender, System.EventArgs e)
    {
      // Show/hide the FolderTree.
      this.ToggleFolderView();
    }

    #endregion TOOLBAR EVENTS

    #region LISTVIEW EVENTS

    private void FileView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      // The Enter key will open the selected item unless we are editing an item.

      if( e.KeyCode == Keys.Enter )
      {
        if( !m_editingItem )
        {
          this.OpenItem();
        }
      }
    }

    private void FileView_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
    {
      m_editingItem = false;

      AbstractListViewItem currentItem = FileView.Items[ e.Item ] as AbstractListViewItem;

      if( currentItem == null )
        return;

      // No changes were made.
      if( e.Label != null )
      {
        char[] invalidPathChars = Path.GetInvalidPathChars();

        // Validate the new name.
        if( e.Label.IndexOfAny( invalidPathChars ) > -1 )
        {
          string message = "The filename is invalid. It cannot contain any of the following characters: \n\n";

          for( int i = 0; i < invalidPathChars.Length; i++ )
          {
            if( i > 0 )
              message += " ";

            message += invalidPathChars[ i ].ToString();
          }

          string caption = "Error Renamming File or Folder";

          MessageBox.Show( this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
          e.CancelEdit = true;
          currentItem.BeginEdit();
          return;
        }

        // Check if the file already exists
        if( e.Label != null )
        {
          foreach( AbstractListViewItem item in FileView.Items )
          {
            if( item.Index != e.Item && item.Text == e.Label )
            {
              string message = "Cannot rename " + currentItem.Text + ": A file with the name you specified already exists. Specify a different file name.";
              string caption = "Error Renamming File or Folder";

              MessageBox.Show( this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error );
              e.CancelEdit = true;
              currentItem.BeginEdit();
              return;
            }
          }
        }

        try
        {
          // Rename the item.
          string originalFullName = currentItem.FileSystemItem.FullName;
          currentItem.Rename( e.Label );

          // Update the treeview.
          if( currentItem.ParentNode != null )
          {
            foreach( AbstractTreeViewNode node in currentItem.ParentNode.Nodes )
            {
              if( node.Item.FullName == originalFullName )
              {
                node.Refresh( currentItem.FileSystemItem );
                break;
              }
            }
          }
        }
        catch
        {
          MessageBox.Show( this, "The item could not be renamed. You might not have sufficient rights to do this operation.", "Error renaming item.", MessageBoxButtons.OK, MessageBoxIcon.Error );
          e.CancelEdit = true;
        }
      }

      if( m_creatingItem )
      {
        // Create the item physically.
        try
        {
          currentItem.Create();
        }
        catch
        {
          MessageBox.Show( this, "An error occured while creating the item. You might not have sufficient rights to do this operation.", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error );
          FileView.Items[ e.Item ].Remove();
        }
        finally
        {
          m_creatingItem = false;
        }
      }
    }

    private void FileView_BeforeLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
    {
      m_editingItem = true;

      // Check if we can rename.
      AbstractListViewItem item = FileView.Items[ e.Item ] as AbstractListViewItem;

      if(  ( item != null )
        && ( !item.RenameToolEnabled ) )
      {
        e.CancelEdit = true;
        m_editingItem = false;
      }
    }

    private void FileView_DoubleClick(object sender, System.EventArgs e)
    {
      // Open the selected item.
      this.OpenItem();
    }

    private void FileView_Enter( object sender, System.EventArgs e )
    {
      this.UpdateToolbarState();
    }

    private void FileView_Leave( object sender, System.EventArgs e )
    {
      this.UpdateToolbarState();
    }

    private void FileView_SelectedIndexChanged( object sender, System.EventArgs e )
    {
      this.UpdateToolbarState();
    }

    #endregion LISTVIEW EVENTS

    #region TREEVIEW EVENTS

    private void FolderTree_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
    {
      if( m_folderSelecting )
        return;

      // Fill the nodes with child nodes.
      this.FillFolderTreeNode( ( AbstractTreeViewNode )e.Node );

      // We only need to refresh the file list if the expanded node is the currently selected node.
      if( e.Node == FolderTree.SelectedNode )
      {
        // Fill the list with files and folders.
        this.FillListView( ( AbstractTreeViewNode )e.Node );
      }
    }

    private void FolderTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
      m_folderSelecting = true;

      // Fill the nodes with child nodes.
      this.FillFolderTreeNode( ( AbstractTreeViewNode )e.Node );

      // Fill the list with files and folders.
      this.FillListView( ( AbstractTreeViewNode )e.Node );

      // Ensure the node is expanded unless we were collapsing it.
      if( !m_folderTreeCollapsing )
        e.Node.Expand();

      m_folderSelecting = false;
    }

    private void FolderTree_BeforeCollapse(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
    {
      m_folderTreeCollapsing = true;
    }

    private void FolderTree_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
      m_folderTreeCollapsing = false;
    }

    private void FolderTree_Enter(object sender, System.EventArgs e)
    {
      this.UpdateToolbarState();
    }

    private void FolderTree_Leave(object sender, System.EventArgs e)
    {
      this.UpdateToolbarState();
    }

    #endregion TREEVIEW EVENTS

    #region FTPCONNECTION EVENTS

    private void OnCertificateReceived( object sender, CertificateReceivedEventArgs e )
    {
      string message = string.Empty;

      if( e.Status == VerificationStatus.ValidCertificate )
      {
        message = "A valid certificate was received from the server.\n\n";
      }
      else
      {
        message = "An invalid certificate was received from the server.\n"
          + "The error is: " + e.Status.ToString() + "\n\n";
      }

      message += e.ServerCertificate.ToString() + "\n\n"
        + "Do you want to accept this certificate?";

      DialogResult answer = MessageBox.Show( 
        this, 
        message, 
        "Server Certificate", 
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question );

      e.Action = ( answer == DialogResult.Yes )
        ? VerificationAction.Accept
        : VerificationAction.Reject;
    }

    private void FtpConnection_CertificateReceived( object sender, CertificateReceivedEventArgs e )
    {
      // This event is usually raised from one of the I/O threads, not the main 
      // thread. We must make sure to perform any GUI operations on the main thread.
      // BUT WATCH OUT: For this to work, the main thread MUST be pumping messages.
      // That's why we also handle the WaitingForAsyncOperation event below, and
      // pump messages there.
      if( this.InvokeRequired )
      {
        this.Invoke( 
          new CertificateReceivedEventHandler( this.OnCertificateReceived ), 
          new object[] { sender, e } );
      }
      else
      {
        this.OnCertificateReceived( sender, e );
      }
    }

    #endregion FTPCONNECTION EVENTS

    #region IDisposable OVERRIDES

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

    #endregion IDisposable OVERRIDES

    #region PRIVATE FIELDS

    // Clipboard
    private DataObject m_clipBoard; // = null 
    private bool m_clipboardCutItems; // = false

    // Flags
    private bool m_folderTreeCollapsing; // = false
    private bool m_folderSelecting; // = false
    private bool m_creatingItem; // = false
    private bool m_editingItem;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem9;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem RefreshMenu; // = false

    // Progression
    private int m_previousProgressionPct; // = 0;

    #endregion PRIVATE FIELDS
    
    #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
      this.FolderTree = new System.Windows.Forms.TreeView();
      this.EditContextMenu = new System.Windows.Forms.ContextMenu();
      this.OpenContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.NewContextMenu = new System.Windows.Forms.MenuItem();
      this.NewFileContextMenu = new System.Windows.Forms.MenuItem();
      this.NewFolderContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.NewGZipArchiveContextMenu = new System.Windows.Forms.MenuItem();
      this.NewTarArchiveContextMenu = new System.Windows.Forms.MenuItem();
      this.NewZipArchiveContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.NewFtpConnectionContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.CopyContextMenu = new System.Windows.Forms.MenuItem();
      this.CutContextMenu = new System.Windows.Forms.MenuItem();
      this.PasteContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.DeleteContextMenu = new System.Windows.Forms.MenuItem();
      this.RenameContextMenu = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.PropertiesContextMenu = new System.Windows.Forms.MenuItem();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.FileView = new Xceed.FileSystem.Samples.Utils.ListView.CustomListView();
      this.NameColumn = new System.Windows.Forms.ColumnHeader();
      this.SizeColumn = new System.Windows.Forms.ColumnHeader();
      this.CompressedSize = new System.Windows.Forms.ColumnHeader();
      this.TypeColumn = new System.Windows.Forms.ColumnHeader();
      this.DateModifiedColumn = new System.Windows.Forms.ColumnHeader();
      this.MainStatusBar = new Xceed.SmartUI.Controls.StatusBar.SmartStatusBar(this.components);
      this.ObjectPanel = new Xceed.SmartUI.Controls.StatusBar.SpringPanel();
      this.SizePanel = new Xceed.SmartUI.Controls.StatusBar.Panel();
      this.LocationPanel = new Xceed.SmartUI.Controls.StatusBar.Panel();
      this.GoTool = new Xceed.SmartUI.Controls.ToolBar.Tool("Go");
      this.MainToolbar = new Xceed.SmartUI.Controls.ToolBar.SmartToolBar(this.components);
      this.UpTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.separatorTool1 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.CopyTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.CutTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.PasteTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.separatorTool5 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.DeleteTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.RenameTool = new Xceed.SmartUI.Controls.ToolBar.Tool();
      this.separatorTool3 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.FoldersTool = new Xceed.SmartUI.Controls.ToolBar.CheckTool("Folders");
      this.separatorTool2 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.ViewsTool = new Xceed.SmartUI.Controls.ToolBar.MenuTool();
      this.IconsToolbarMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Icons");
      this.ListToolbarMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("List");
      this.DetailsToolbarMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Details");
      this.BackTool = new Xceed.SmartUI.Controls.ToolBar.MixedTool("Back");
      this.NextTool = new Xceed.SmartUI.Controls.ToolBar.MixedTool("Forward");
      this.separatorTool4 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.MainMenuBar = new Xceed.SmartUI.Controls.MenuBar.SmartMenuBar(this.components);
      this.FileMenu = new Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&File");
      this.NewMenu = new Xceed.SmartUI.Controls.MenuBar.PopupMenuItem("New");
      this.NewFileMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Text file");
      this.NewFolderMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Folder");
      this.separatorMenuItem4 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.NewGZipArchiveMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("GZip archive");
      this.NewTarArchiveMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Tar archive");
      this.NewZipArchiveMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Zip archive");
      this.separatorMenuItem6 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.NewFtpConnectionMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Ftp connection...");
      this.separatorMenuItem3 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.ExitMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("E&xit");
      this.EditMenu = new Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&Edit");
      this.OpenMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Open");
      this.separatorMenuItem8 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.CopyMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("&Copy");
      this.CutMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Cu&t");
      this.PasteMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("&Paste");
      this.separatorMenuItem1 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.DeleteMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("&Delete");
      this.RenameMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("&Rename");
      this.separatorMenuItem5 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.PropertiesMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Properties...");
      this.ViewMenu = new Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&View");
      this.IconsMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Icons");
      this.ListMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("List");
      this.DetailsMenu = new Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Details");
      this.ToolsMenu = new Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&Tools");
      this.OptionsMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("&Options...");
      this.separatorMenuItem2 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.comboBoxItem1 = new Xceed.SmartUI.Controls.ToolBar.ComboBoxItem("comboBoxItem1");
      this.node1 = new Xceed.SmartUI.Controls.TreeView.Node("node1");
      this.mixedTool1 = new Xceed.SmartUI.Controls.ToolBar.MixedTool("mixedTool1");
      this.NewTool = new Xceed.SmartUI.Controls.ToolBar.MenuTool("New");
      this.separatorTool6 = new Xceed.SmartUI.Controls.ToolBar.SeparatorTool();
      this.separatorMenuItem7 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.separatorMenuItem9 = new Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem();
      this.RefreshMenu = new Xceed.SmartUI.Controls.MenuBar.MenuItem("Refresh");
      ((System.ComponentModel.ISupportInitialize)(this.MainStatusBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MainToolbar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MainMenuBar)).BeginInit();
      this.SuspendLayout();
      // 
      // FolderTree
      // 
      this.FolderTree.Dock = System.Windows.Forms.DockStyle.Left;
      this.FolderTree.HideSelection = false;
      this.FolderTree.HotTracking = true;
      this.FolderTree.ImageIndex = -1;
      this.FolderTree.Location = new System.Drawing.Point(0, 58);
      this.FolderTree.Name = "FolderTree";
      this.FolderTree.SelectedImageIndex = -1;
      this.FolderTree.ShowLines = false;
      this.FolderTree.Size = new System.Drawing.Size(248, 390);
      this.FolderTree.TabIndex = 2;
      this.FolderTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.FolderTree_AfterCollapse);
      this.FolderTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FolderTree_AfterSelect);
      this.FolderTree.Leave += new System.EventHandler(this.FolderTree_Leave);
      this.FolderTree.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.FolderTree_BeforeCollapse);
      this.FolderTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.FolderTree_BeforeExpand);
      this.FolderTree.Enter += new System.EventHandler(this.FolderTree_Enter);
      // 
      // EditContextMenu
      // 
      this.EditContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.OpenContextMenu,
                                                                                    this.menuItem6,
                                                                                    this.NewContextMenu,
                                                                                    this.menuItem2,
                                                                                    this.CopyContextMenu,
                                                                                    this.CutContextMenu,
                                                                                    this.PasteContextMenu,
                                                                                    this.menuItem1,
                                                                                    this.DeleteContextMenu,
                                                                                    this.RenameContextMenu,
                                                                                    this.menuItem4,
                                                                                    this.PropertiesContextMenu});
      this.EditContextMenu.Popup += new System.EventHandler(this.EditContextMenu_Popup);
      // 
      // OpenContextMenu
      // 
      this.OpenContextMenu.DefaultItem = true;
      this.OpenContextMenu.Index = 0;
      this.OpenContextMenu.Text = "Open";
      this.OpenContextMenu.Click += new System.EventHandler(this.OpenContextMenu_Click);
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 1;
      this.menuItem6.Text = "-";
      // 
      // NewContextMenu
      // 
      this.NewContextMenu.Index = 2;
      this.NewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                   this.NewFileContextMenu,
                                                                                   this.NewFolderContextMenu,
                                                                                   this.menuItem3,
                                                                                   this.NewGZipArchiveContextMenu,
                                                                                   this.NewTarArchiveContextMenu,
                                                                                   this.NewZipArchiveContextMenu,
                                                                                   this.menuItem5,
                                                                                   this.NewFtpConnectionContextMenu});
      this.NewContextMenu.Text = "New";
      // 
      // NewFileContextMenu
      // 
      this.NewFileContextMenu.Index = 0;
      this.NewFileContextMenu.Text = "Text file";
      this.NewFileContextMenu.Click += new System.EventHandler(this.NewFileContextMenu_Click);
      // 
      // NewFolderContextMenu
      // 
      this.NewFolderContextMenu.Index = 1;
      this.NewFolderContextMenu.Text = "Folder";
      this.NewFolderContextMenu.Click += new System.EventHandler(this.NewFolderContextMenu_Click);
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 2;
      this.menuItem3.Text = "-";
      // 
      // NewGZipArchiveContextMenu
      // 
      this.NewGZipArchiveContextMenu.Index = 3;
      this.NewGZipArchiveContextMenu.Text = "GZip archive";
      this.NewGZipArchiveContextMenu.Click += new System.EventHandler(this.NewGZipArchiveContextMenu_Click);
      // 
      // NewTarArchiveContextMenu
      // 
      this.NewTarArchiveContextMenu.Index = 4;
      this.NewTarArchiveContextMenu.Text = "Tar archive";
      this.NewTarArchiveContextMenu.Click += new System.EventHandler(this.NewTarArchiveContextMenu_Click);
      // 
      // NewZipArchiveContextMenu
      // 
      this.NewZipArchiveContextMenu.Index = 5;
      this.NewZipArchiveContextMenu.Text = "Zip archive";
      this.NewZipArchiveContextMenu.Click += new System.EventHandler(this.NewZipArchiveContextMenu_Click);
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 6;
      this.menuItem5.Text = "-";
      // 
      // NewFtpConnectionContextMenu
      // 
      this.NewFtpConnectionContextMenu.Index = 7;
      this.NewFtpConnectionContextMenu.Text = "Ftp connection...";
      this.NewFtpConnectionContextMenu.Click += new System.EventHandler(this.NewFtpConnectionContextMenu_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 3;
      this.menuItem2.Text = "-";
      // 
      // CopyContextMenu
      // 
      this.CopyContextMenu.Index = 4;
      this.CopyContextMenu.Text = "Copy";
      this.CopyContextMenu.Click += new System.EventHandler(this.CopyContextMenu_Click);
      // 
      // CutContextMenu
      // 
      this.CutContextMenu.Index = 5;
      this.CutContextMenu.Text = "Cut";
      this.CutContextMenu.Click += new System.EventHandler(this.CutContextMenu_Click);
      // 
      // PasteContextMenu
      // 
      this.PasteContextMenu.Index = 6;
      this.PasteContextMenu.Text = "Paste";
      this.PasteContextMenu.Click += new System.EventHandler(this.PasteContextMenu_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 7;
      this.menuItem1.Text = "-";
      // 
      // DeleteContextMenu
      // 
      this.DeleteContextMenu.Index = 8;
      this.DeleteContextMenu.Text = "Delete";
      this.DeleteContextMenu.Click += new System.EventHandler(this.DeleteContextMenu_Click);
      // 
      // RenameContextMenu
      // 
      this.RenameContextMenu.Index = 9;
      this.RenameContextMenu.Text = "Rename";
      this.RenameContextMenu.Click += new System.EventHandler(this.RenameContextMenu_Click);
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 10;
      this.menuItem4.Text = "-";
      // 
      // PropertiesContextMenu
      // 
      this.PropertiesContextMenu.Index = 11;
      this.PropertiesContextMenu.Text = "Properties...";
      this.PropertiesContextMenu.Click += new System.EventHandler(this.PropertiesContextMenu_Click);
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(248, 58);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 390);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      // 
      // FileView
      // 
      this.FileView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                               this.NameColumn,
                                                                               this.SizeColumn,
                                                                               this.CompressedSize,
                                                                               this.TypeColumn,
                                                                               this.DateModifiedColumn});
      this.FileView.ContextMenu = this.EditContextMenu;
      this.FileView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FileView.HideSelection = false;
      this.FileView.LabelEdit = true;
      this.FileView.Location = new System.Drawing.Point(251, 58);
      this.FileView.Name = "FileView";
      this.FileView.Size = new System.Drawing.Size(491, 390);
      this.FileView.TabIndex = 4;
      this.FileView.View = System.Windows.Forms.View.Details;
      this.FileView.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.FileView_BeforeLabelEdit);
      this.FileView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileView_KeyDown);
      this.FileView.DoubleClick += new System.EventHandler(this.FileView_DoubleClick);
      this.FileView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.FileView_AfterLabelEdit);
      this.FileView.SelectedIndexChanged += new System.EventHandler(this.FileView_SelectedIndexChanged);
      this.FileView.Leave += new System.EventHandler(this.FileView_Leave);
      this.FileView.Enter += new System.EventHandler(this.FileView_Enter);
      // 
      // NameColumn
      // 
      this.NameColumn.Text = "Name";
      this.NameColumn.Width = 209;
      // 
      // SizeColumn
      // 
      this.SizeColumn.Text = "Size";
      this.SizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.SizeColumn.Width = 85;
      // 
      // CompressedSize
      // 
      this.CompressedSize.Text = "Compressed size";
      this.CompressedSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.CompressedSize.Width = 100;
      // 
      // TypeColumn
      // 
      this.TypeColumn.Text = "Type";
      this.TypeColumn.Width = 157;
      // 
      // DateModifiedColumn
      // 
      this.DateModifiedColumn.Text = "Date Modified";
      this.DateModifiedColumn.Width = 132;
      // 
      // MainStatusBar
      // 
      this.MainStatusBar.Items.AddRange(new object[] {
                                                       this.ObjectPanel,
                                                       this.SizePanel,
                                                       this.LocationPanel});
      this.MainStatusBar.Location = new System.Drawing.Point(0, 448);
      this.MainStatusBar.Name = "MainStatusBar";
      this.MainStatusBar.Size = new System.Drawing.Size(742, 23);
      this.MainStatusBar.TabIndex = 5;
      this.MainStatusBar.Text = "MainStatusBar";
      // 
      // GoTool
      // 
      this.GoTool.Image = ((System.Drawing.Image)(resources.GetObject("GoTool.Image")));
      this.GoTool.Text = "Go";
      // 
      // MainToolbar
      // 
      this.MainToolbar.Items.AddRange(new object[] {
                                                     this.UpTool,
                                                     this.separatorTool1,
                                                     this.CopyTool,
                                                     this.CutTool,
                                                     this.PasteTool,
                                                     this.separatorTool5,
                                                     this.DeleteTool,
                                                     this.RenameTool,
                                                     this.separatorTool3,
                                                     this.FoldersTool,
                                                     this.separatorTool2,
                                                     this.ViewsTool});
      this.MainToolbar.Location = new System.Drawing.Point(0, 24);
      this.MainToolbar.Name = "MainToolbar";
      this.MainToolbar.Size = new System.Drawing.Size(742, 34);
      this.MainToolbar.TabIndex = 7;
      // 
      // UpTool
      // 
      this.UpTool.Enabled = false;
      this.UpTool.Image = ((System.Drawing.Image)(resources.GetObject("UpTool.Image")));
      this.UpTool.ToolTipText = "Up";
      this.UpTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.UpTool_Click);
      // 
      // CopyTool
      // 
      this.CopyTool.Enabled = false;
      this.CopyTool.Image = ((System.Drawing.Image)(resources.GetObject("CopyTool.Image")));
      this.CopyTool.ToolTipText = "Copy";
      this.CopyTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.CopyTool_Click);
      // 
      // CutTool
      // 
      this.CutTool.Enabled = false;
      this.CutTool.Image = ((System.Drawing.Image)(resources.GetObject("CutTool.Image")));
      this.CutTool.ToolTipText = "Cut";
      this.CutTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.CutTool_Click);
      // 
      // PasteTool
      // 
      this.PasteTool.Enabled = false;
      this.PasteTool.Image = ((System.Drawing.Image)(resources.GetObject("PasteTool.Image")));
      this.PasteTool.ToolTipText = "Paste";
      this.PasteTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.PasteTool_Click);
      // 
      // DeleteTool
      // 
      this.DeleteTool.Enabled = false;
      this.DeleteTool.Image = ((System.Drawing.Image)(resources.GetObject("DeleteTool.Image")));
      this.DeleteTool.ToolTipText = "Delete";
      this.DeleteTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.DeleteTool_Click);
      // 
      // RenameTool
      // 
      this.RenameTool.Enabled = false;
      this.RenameTool.Image = ((System.Drawing.Image)(resources.GetObject("RenameTool.Image")));
      this.RenameTool.ToolTipText = "Rename";
      this.RenameTool.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.RenameTool_Click);
      // 
      // FoldersTool
      // 
      this.FoldersTool.Checked = true;
      this.FoldersTool.Image = ((System.Drawing.Image)(resources.GetObject("FoldersTool.Image")));
      this.FoldersTool.Text = "Folders";
      this.FoldersTool.CheckedChanged += new System.EventHandler(this.FoldersTool_CheckedChanged);
      // 
      // ViewsTool
      // 
      this.ViewsTool.Image = ((System.Drawing.Image)(resources.GetObject("ViewsTool.Image")));
      this.ViewsTool.Items.AddRange(new object[] {
                                                   this.IconsToolbarMenu,
                                                   this.ListToolbarMenu,
                                                   this.DetailsToolbarMenu});
      this.ViewsTool.ToolTipText = "Views";
      // 
      // IconsToolbarMenu
      // 
      this.IconsToolbarMenu.Text = "Icons";
      this.IconsToolbarMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.IconsMenu_Click);
      // 
      // ListToolbarMenu
      // 
      this.ListToolbarMenu.Text = "List";
      this.ListToolbarMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.ListMenu_Click);
      // 
      // DetailsToolbarMenu
      // 
      this.DetailsToolbarMenu.Checked = true;
      this.DetailsToolbarMenu.Text = "Details";
      this.DetailsToolbarMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.DetailsMenu_Click);
      // 
      // BackTool
      // 
      this.BackTool.Enabled = false;
      this.BackTool.Image = ((System.Drawing.Image)(resources.GetObject("BackTool.Image")));
      this.BackTool.Text = "Back";
      this.BackTool.Visible = false;
      // 
      // NextTool
      // 
      this.NextTool.Enabled = false;
      this.NextTool.Image = ((System.Drawing.Image)(resources.GetObject("NextTool.Image")));
      this.NextTool.Text = "Forward";
      this.NextTool.Visible = false;
      // 
      // MainMenuBar
      // 
      this.MainMenuBar.Items.AddRange(new object[] {
                                                     this.FileMenu,
                                                     this.EditMenu,
                                                     this.ViewMenu,
                                                     this.ToolsMenu});
      this.MainMenuBar.Location = new System.Drawing.Point(0, 0);
      this.MainMenuBar.Name = "MainMenuBar";
      this.MainMenuBar.Size = new System.Drawing.Size(742, 24);
      this.MainMenuBar.TabIndex = 8;
      this.MainMenuBar.Text = "MainMenuBar";
      this.MainMenuBar.UIStyle = Xceed.SmartUI.UIStyle.UIStyle.WindowsXP;
      // 
      // FileMenu
      // 
      this.FileMenu.Items.AddRange(new object[] {
                                                  this.NewMenu,
                                                  this.separatorMenuItem3,
                                                  this.ExitMenu});
      this.FileMenu.Text = "&File";
      // 
      // NewMenu
      // 
      this.NewMenu.Items.AddRange(new object[] {
                                                 this.NewFileMenu,
                                                 this.NewFolderMenu,
                                                 this.separatorMenuItem4,
                                                 this.NewGZipArchiveMenu,
                                                 this.NewTarArchiveMenu,
                                                 this.NewZipArchiveMenu,
                                                 this.separatorMenuItem6,
                                                 this.NewFtpConnectionMenu});
      this.NewMenu.Text = "New";
      // 
      // NewFileMenu
      // 
      this.NewFileMenu.Text = "Text file";
      this.NewFileMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewFileMenu_Click);
      // 
      // NewFolderMenu
      // 
      this.NewFolderMenu.Text = "Folder";
      this.NewFolderMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewFolderMenu_Click);
      // 
      // NewGZipArchiveMenu
      // 
      this.NewGZipArchiveMenu.Text = "GZip archive";
      this.NewGZipArchiveMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewGZipArchiveMenu_Click);
      // 
      // NewTarArchiveMenu
      // 
      this.NewTarArchiveMenu.Text = "Tar archive";
      this.NewTarArchiveMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewTarArchiveMenu_Click);
      // 
      // NewZipArchiveMenu
      // 
      this.NewZipArchiveMenu.Text = "Zip archive";
      this.NewZipArchiveMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewZipArchiveMenu_Click);
      // 
      // NewFtpConnectionMenu
      // 
      this.NewFtpConnectionMenu.Text = "Ftp connection...";
      this.NewFtpConnectionMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.NewFtpConnectionMenu_Click);
      // 
      // ExitMenu
      // 
      this.ExitMenu.Text = "E&xit";
      this.ExitMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.ExitMenu_Click);
      // 
      // EditMenu
      // 
      this.EditMenu.Items.AddRange(new object[] {
                                                  this.OpenMenu,
                                                  this.separatorMenuItem8,
                                                  this.CopyMenu,
                                                  this.CutMenu,
                                                  this.PasteMenu,
                                                  this.separatorMenuItem1,
                                                  this.DeleteMenu,
                                                  this.RenameMenu,
                                                  this.separatorMenuItem5,
                                                  this.PropertiesMenu});
      this.EditMenu.Text = "&Edit";
      // 
      // OpenMenu
      // 
      this.OpenMenu.ShortcutText = "Enter";
      this.OpenMenu.Text = "Open";
      this.OpenMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.OpenMenu_Click);
      // 
      // CopyMenu
      // 
      this.CopyMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
      this.CopyMenu.Text = "&Copy";
      this.CopyMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.CopyMenu_Click);
      // 
      // CutMenu
      // 
      this.CutMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
      this.CutMenu.Text = "Cu&t";
      this.CutMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.CutMenu_Click);
      // 
      // PasteMenu
      // 
      this.PasteMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
      this.PasteMenu.Text = "&Paste";
      this.PasteMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.PasteMenu_Click);
      // 
      // DeleteMenu
      // 
      this.DeleteMenu.Shortcut = System.Windows.Forms.Shortcut.Del;
      this.DeleteMenu.Text = "&Delete";
      this.DeleteMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.DeleteMenu_Click);
      // 
      // RenameMenu
      // 
      this.RenameMenu.Shortcut = System.Windows.Forms.Shortcut.F2;
      this.RenameMenu.Text = "&Rename";
      this.RenameMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.RenameMenu_Click);
      // 
      // PropertiesMenu
      // 
      this.PropertiesMenu.Text = "Properties...";
      this.PropertiesMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.PropertiesMenu_Click);
      // 
      // ViewMenu
      // 
      this.ViewMenu.Items.AddRange(new object[] {
                                                  this.IconsMenu,
                                                  this.ListMenu,
                                                  this.DetailsMenu,
                                                  this.separatorMenuItem9,
                                                  this.RefreshMenu});
      this.ViewMenu.Text = "&View";
      // 
      // IconsMenu
      // 
      this.IconsMenu.Text = "Icons";
      this.IconsMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.IconsMenu_Click);
      // 
      // ListMenu
      // 
      this.ListMenu.Text = "List";
      this.ListMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.ListMenu_Click);
      // 
      // DetailsMenu
      // 
      this.DetailsMenu.Checked = true;
      this.DetailsMenu.Text = "Details";
      this.DetailsMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.DetailsMenu_Click);
      // 
      // ToolsMenu
      // 
      this.ToolsMenu.Items.AddRange(new object[] {
                                                   this.OptionsMenu});
      this.ToolsMenu.Text = "&Tools";
      // 
      // OptionsMenu
      // 
      this.OptionsMenu.Text = "&Options...";
      this.OptionsMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.OptionsMenu_Click);
      // 
      // comboBoxItem1
      // 
      this.comboBoxItem1.Text = "comboBoxItem1";
      // 
      // node1
      // 
      this.node1.Text = "node1";
      // 
      // mixedTool1
      // 
      this.mixedTool1.Text = "mixedTool1";
      // 
      // NewTool
      // 
      this.NewTool.Text = "New";
      // 
      // RefreshMenu
      // 
      this.RefreshMenu.Shortcut = System.Windows.Forms.Shortcut.F5;
      this.RefreshMenu.Text = "Refresh";
      this.RefreshMenu.Click += new Xceed.SmartUI.SmartItemClickEventHandler(this.RefreshMenu_Click);
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(742, 471);
      this.Controls.Add(this.FileView);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.FolderTree);
      this.Controls.Add(this.MainStatusBar);
      this.Controls.Add(this.MainToolbar);
      this.Controls.Add(this.MainMenuBar);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "MainForm";
      this.Text = "Xceed File System Explorer for .NET";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
      ((System.ComponentModel.ISupportInitialize)(this.MainStatusBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MainToolbar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MainMenuBar)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.TreeView FolderTree;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.ColumnHeader TypeColumn;
    private System.Windows.Forms.ColumnHeader SizeColumn;
    private System.Windows.Forms.ColumnHeader DateModifiedColumn;
    private System.Windows.Forms.ColumnHeader NameColumn;
    private Xceed.SmartUI.Controls.StatusBar.SmartStatusBar MainStatusBar;
    private Xceed.SmartUI.Controls.StatusBar.SpringPanel ObjectPanel;
    private Xceed.SmartUI.Controls.StatusBar.Panel SizePanel;
    private Xceed.SmartUI.Controls.StatusBar.Panel LocationPanel;
    private Xceed.SmartUI.Controls.ToolBar.Tool GoTool;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool1;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool2;
    private Xceed.SmartUI.Controls.ToolBar.MixedTool BackTool;
    private Xceed.SmartUI.Controls.MenuBar.MainMenuItem FileMenu;
    private Xceed.SmartUI.Controls.ToolBar.MixedTool NextTool;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem ExitMenu;
    private Xceed.SmartUI.Controls.ToolBar.SmartToolBar MainToolbar;
    private Xceed.SmartUI.Controls.ToolBar.MenuTool ViewsTool;
    private Xceed.SmartUI.Controls.ToolBar.Tool UpTool;
    private Xceed.SmartUI.Controls.MenuBar.SmartMenuBar MainMenuBar;
    private Xceed.SmartUI.Controls.ToolBar.CheckTool FoldersTool;
    private Xceed.SmartUI.Controls.MenuBar.MainMenuItem ViewMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem IconsMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem ListMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem DetailsMenu;
    private Xceed.SmartUI.Controls.MenuBar.MainMenuItem EditMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem RenameMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem ListToolbarMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem DetailsToolbarMenu;
    private Xceed.SmartUI.Controls.MenuBar.CheckMenuItem IconsToolbarMenu;
    private Xceed.SmartUI.Controls.ToolBar.Tool CopyTool;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool3;
    private Xceed.SmartUI.Controls.ToolBar.Tool CutTool;
    private Xceed.SmartUI.Controls.ToolBar.Tool PasteTool;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool4;
    private Xceed.SmartUI.Controls.ToolBar.Tool DeleteTool;
    private System.Windows.Forms.MenuItem CopyContextMenu;
    private System.Windows.Forms.MenuItem CutContextMenu;
    private System.Windows.Forms.MenuItem PasteContextMenu;
    private System.Windows.Forms.MenuItem DeleteContextMenu;
    private System.Windows.Forms.ContextMenu EditContextMenu;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem RenameContextMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem CopyMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem CutMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem PasteMenu;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem1;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem DeleteMenu;
    private Xceed.SmartUI.Controls.ToolBar.Tool RenameTool;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool5;
    private Xceed.SmartUI.Controls.ToolBar.ComboBoxItem comboBoxItem1;
    private Xceed.SmartUI.Controls.TreeView.Node node1;
    private System.Windows.Forms.ColumnHeader CompressedSize;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem2;
    private Xceed.SmartUI.Controls.ToolBar.MixedTool mixedTool1;
    private Xceed.SmartUI.Controls.ToolBar.MenuTool NewTool;
    private Xceed.SmartUI.Controls.ToolBar.SeparatorTool separatorTool6;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem3;
    private Xceed.SmartUI.Controls.MenuBar.PopupMenuItem NewMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewFileMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewFolderMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewTarArchiveMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewZipArchiveMenu;
    private System.Windows.Forms.MenuItem NewContextMenu;
    private System.Windows.Forms.MenuItem NewFileContextMenu;
    private System.Windows.Forms.MenuItem NewFolderContextMenu;
    private System.Windows.Forms.MenuItem NewTarArchiveContextMenu;
    private System.Windows.Forms.MenuItem NewZipArchiveContextMenu;
    private System.Windows.Forms.MenuItem menuItem2;
    private Xceed.SmartUI.Controls.MenuBar.MainMenuItem ToolsMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem OptionsMenu;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewGZipArchiveMenu;
    private System.Windows.Forms.MenuItem NewGZipArchiveContextMenu;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem4;
    private System.Windows.Forms.MenuItem menuItem3;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem5;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem PropertiesMenu;
    private System.Windows.Forms.MenuItem menuItem4;
    private System.Windows.Forms.MenuItem PropertiesContextMenu;
    private System.Windows.Forms.MenuItem menuItem5;
    private System.Windows.Forms.MenuItem NewFtpConnectionContextMenu;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem6;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem NewFtpConnectionMenu;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem7;
    private Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem separatorMenuItem8;
    private Xceed.SmartUI.Controls.MenuBar.MenuItem OpenMenu;
    private System.Windows.Forms.MenuItem OpenContextMenu;
    private System.Windows.Forms.MenuItem menuItem6;
    private Xceed.FileSystem.Samples.Utils.ListView.CustomListView FileView;

    #endregion Windows Form Designer generated fields
  }
}
