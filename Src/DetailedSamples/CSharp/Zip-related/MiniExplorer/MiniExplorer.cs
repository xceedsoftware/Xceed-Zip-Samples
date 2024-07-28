/*
 * Xceed Zip for .NET - MiniExplorer Sample Application
 * Copyright (c) 2000-2003 - Xceed Software Inc.
 * 
 * [MiniExplorer.cs]
 * 
 * This application demonstrates how to use the Xceed FileSystem object model
 * in a generic way.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.MiniExplorer
{
  /// <summary>
  /// Summary description for MiniExplorer.
  /// </summary>
  public class MiniExplorerForm : System.Windows.Forms.Form
  {
    private System.Windows.Forms.TreeView FolderTree;
    private System.Windows.Forms.ListView FileList;
    private System.Windows.Forms.ColumnHeader FileListColName;
    private System.Windows.Forms.ColumnHeader FileListColSize;
    private System.Windows.Forms.ColumnHeader FileListColAttributes;
    private System.Windows.Forms.ListBox ResultList;
    private System.Windows.Forms.MainMenu MiniExplorerMenu;
    private System.Windows.Forms.MenuItem FileMenu;
    private System.Windows.Forms.MenuItem EditMenu;
    private System.Windows.Forms.MenuItem HelpMenu;
    private System.Windows.Forms.MenuItem HelpAboutMenu;
    private System.Windows.Forms.MenuItem FileQuitMenu;
    private System.Windows.Forms.MenuItem EditDeleteMenu;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Splitter splitter2;
    private System.Windows.Forms.Panel ProgressPanel;
    private System.Windows.Forms.MenuItem EditNewMenu;
    private System.Windows.Forms.MenuItem EditNewFolderMenu;
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.ProgressBar CurrentProgressBar;
    private System.Windows.Forms.ProgressBar AllProgressBar;
    private System.Windows.Forms.ImageList FileSystemItemImages;
    private System.Windows.Forms.MenuItem EditNewFileMenu;
    private System.Windows.Forms.MenuItem EditCopyMenu;
    private System.Windows.Forms.MenuItem EditCutMenu;
    private System.Windows.Forms.MenuItem EditPasteMenu;
    private System.Windows.Forms.MenuItem menuItem5;
    private System.Windows.Forms.MenuItem EditRefreshMenu;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem EditNewZipFileMenu;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem EditSplitMenu;
    private System.Windows.Forms.ColumnHeader FileListColLastWrite;
    private System.Windows.Forms.ColumnHeader FileListColLastAccess;
    private System.Windows.Forms.ColumnHeader FileListColCreation;
    private System.Windows.Forms.MenuItem EditRenameMenu;

    public MiniExplorerForm()
    {
      /* ================================
       * How to license Xceed components 
       * ================================       
       * To license your product, set the LicenseKey property to a valid trial or registered license key 
       * in the main entry point of the application to ensure components are licensed before any of the 
       * component methods are called.      
       * 
       * If the component is used in a DLL project (no entry point available), it is 
       * recommended that the LicenseKey property be set in a static constructor of a 
       * class that will be accessed systematically before any component is instantiated or, 
       * you can simply set the LicenseKey property immediately BEFORE you instantiate 
       * an instance of the component.
       * 
       * For instance, if you wanted to deploy this sample, the license key needs to be set here.
       * If your trial period has expired, you must purchase a registered license key,
       * uncomment the next line of code, and insert your registerd license key.
       * For more information, consult the "How the 45-day trial works" and the 
       * "How to license the component once you purchase" topics in the documentation of this product.
       */
        
       // Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 

      //
      // Required for Windows Form Designer support
      //

      InitializeComponent();

      //
      // Populate our folder tree view with available disks
      //

      string[] drives = System.IO.Directory.GetLogicalDrives();

      foreach( string drive in drives )
      {
        FolderTreeNode folder = new FolderTreeNode( new DiskFolder( drive ), drive );
        FolderTree.Nodes.Add( folder );
      }

      // Let's also add a virtual RAM drive just for fun!
      // (and to show you how amasing are the MemoryFolder and MemoryFile classes!)

      MemoryFolder ramDrive = new MemoryFolder( "RAM", "\\" );
      FolderTree.Nodes.Add( new FolderTreeNode( ramDrive, ramDrive.FullName ) );

      // Let's add a user store drive

      IsolatedFolder isoDrive = new IsolatedFolder( "\\" );
      FolderTree.Nodes.Add( new FolderTreeNode( isoDrive, "UserStore:\\" ) );

      // Set the second node as the starting point, to avoid exception on empty floppy      

      FolderTree.SelectedNode = FolderTree.Nodes[ 1 ]; 
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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

		#region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MiniExplorerForm));
      this.FolderTree = new System.Windows.Forms.TreeView();
      this.FileSystemItemImages = new System.Windows.Forms.ImageList(this.components);
      this.FileList = new System.Windows.Forms.ListView();
      this.FileListColName = new System.Windows.Forms.ColumnHeader();
      this.FileListColSize = new System.Windows.Forms.ColumnHeader();
      this.FileListColAttributes = new System.Windows.Forms.ColumnHeader();
      this.FileListColLastWrite = new System.Windows.Forms.ColumnHeader();
      this.FileListColLastAccess = new System.Windows.Forms.ColumnHeader();
      this.FileListColCreation = new System.Windows.Forms.ColumnHeader();
      this.ResultList = new System.Windows.Forms.ListBox();
      this.MiniExplorerMenu = new System.Windows.Forms.MainMenu();
      this.FileMenu = new System.Windows.Forms.MenuItem();
      this.FileQuitMenu = new System.Windows.Forms.MenuItem();
      this.EditMenu = new System.Windows.Forms.MenuItem();
      this.EditCutMenu = new System.Windows.Forms.MenuItem();
      this.EditCopyMenu = new System.Windows.Forms.MenuItem();
      this.EditPasteMenu = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.EditNewMenu = new System.Windows.Forms.MenuItem();
      this.EditNewFolderMenu = new System.Windows.Forms.MenuItem();
      this.EditNewFileMenu = new System.Windows.Forms.MenuItem();
      this.EditNewZipFileMenu = new System.Windows.Forms.MenuItem();
      this.EditRenameMenu = new System.Windows.Forms.MenuItem();
      this.EditDeleteMenu = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.EditRefreshMenu = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.EditSplitMenu = new System.Windows.Forms.MenuItem();
      this.HelpMenu = new System.Windows.Forms.MenuItem();
      this.HelpAboutMenu = new System.Windows.Forms.MenuItem();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.ProgressPanel = new System.Windows.Forms.Panel();
      this.CurrentProgressBar = new System.Windows.Forms.ProgressBar();
      this.AllProgressBar = new System.Windows.Forms.ProgressBar();
      this.ProgressPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // FolderTree
      // 
      this.FolderTree.AllowDrop = true;
      this.FolderTree.Dock = System.Windows.Forms.DockStyle.Left;
      this.FolderTree.HideSelection = false;
      this.FolderTree.ImageIndex = 1;
      this.FolderTree.ImageList = this.FileSystemItemImages;
      this.FolderTree.LabelEdit = true;
      this.FolderTree.Location = new System.Drawing.Point(0, 0);
      this.FolderTree.Name = "FolderTree";
      this.FolderTree.Size = new System.Drawing.Size(200, 363);
      this.FolderTree.TabIndex = 0;
      this.FolderTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FolderTree_KeyDown);
      this.FolderTree.DragOver += new System.Windows.Forms.DragEventHandler(this.FolderTree_DragOver);
      this.FolderTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FolderTree_AfterSelect);
      this.FolderTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.FolderTree_AfterLabelEdit);
      this.FolderTree.Leave += new System.EventHandler(this.FolderTree_Leave);
      this.FolderTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.FolderTree_BeforeExpand);
      this.FolderTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.FolderTree_ItemDrag);
      this.FolderTree.DragLeave += new System.EventHandler(this.FolderTree_DragLeave);
      this.FolderTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.FolderTree_DragDrop);
      this.FolderTree.Enter += new System.EventHandler(this.FolderTree_Enter);
      // 
      // FileSystemItemImages
      // 
      this.FileSystemItemImages.ImageSize = new System.Drawing.Size(16, 16);
      this.FileSystemItemImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FileSystemItemImages.ImageStream")));
      this.FileSystemItemImages.TransparentColor = System.Drawing.Color.White;
      // 
      // FileList
      // 
      this.FileList.AllowDrop = true;
      this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                               this.FileListColName,
                                                                               this.FileListColSize,
                                                                               this.FileListColAttributes,
                                                                               this.FileListColLastWrite,
                                                                               this.FileListColLastAccess,
                                                                               this.FileListColCreation});
      this.FileList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.FileList.FullRowSelect = true;
      this.FileList.HideSelection = false;
      this.FileList.LabelEdit = true;
      this.FileList.Location = new System.Drawing.Point(203, 0);
      this.FileList.Name = "FileList";
      this.FileList.Size = new System.Drawing.Size(485, 363);
      this.FileList.SmallImageList = this.FileSystemItemImages;
      this.FileList.TabIndex = 2;
      this.FileList.View = System.Windows.Forms.View.Details;
      this.FileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileList_KeyDown);
      this.FileList.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.FileList_AfterLabelEdit);
      this.FileList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.FileList_ItemDrag);
      // 
      // FileListColName
      // 
      this.FileListColName.Text = "Name";
      this.FileListColName.Width = 285;
      // 
      // FileListColSize
      // 
      this.FileListColSize.Text = "Size";
      // 
      // FileListColAttributes
      // 
      this.FileListColAttributes.Text = "Attributes";
      this.FileListColAttributes.Width = 85;
      // 
      // FileListColLastWrite
      // 
      this.FileListColLastWrite.Text = "Last modified";
      this.FileListColLastWrite.Width = 125;
      // 
      // FileListColLastAccess
      // 
      this.FileListColLastAccess.Text = "Last accessed";
      this.FileListColLastAccess.Width = 125;
      // 
      // FileListColCreation
      // 
      this.FileListColCreation.Text = "Created";
      this.FileListColCreation.Width = 125;
      // 
      // ResultList
      // 
      this.ResultList.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.ResultList.Location = new System.Drawing.Point(0, 366);
      this.ResultList.Name = "ResultList";
      this.ResultList.Size = new System.Drawing.Size(688, 69);
      this.ResultList.TabIndex = 3;
      // 
      // MiniExplorerMenu
      // 
      this.MiniExplorerMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.FileMenu,
                                                                                     this.EditMenu,
                                                                                     this.HelpMenu});
      // 
      // FileMenu
      // 
      this.FileMenu.Index = 0;
      this.FileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.FileQuitMenu});
      this.FileMenu.Text = "&File";
      // 
      // FileQuitMenu
      // 
      this.FileQuitMenu.Index = 0;
      this.FileQuitMenu.Text = "&Quit";
      this.FileQuitMenu.Click += new System.EventHandler(this.FileQuitMenu_Click);
      // 
      // EditMenu
      // 
      this.EditMenu.Index = 1;
      this.EditMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.EditCutMenu,
                                                                             this.EditCopyMenu,
                                                                             this.EditPasteMenu,
                                                                             this.menuItem5,
                                                                             this.EditNewMenu,
                                                                             this.EditRenameMenu,
                                                                             this.EditDeleteMenu,
                                                                             this.menuItem1,
                                                                             this.EditRefreshMenu,
                                                                             this.menuItem2,
                                                                             this.EditSplitMenu});
      this.EditMenu.Text = "&Edit";
      // 
      // EditCutMenu
      // 
      this.EditCutMenu.Index = 0;
      this.EditCutMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
      this.EditCutMenu.Text = "C&ut";
      this.EditCutMenu.Click += new System.EventHandler(this.EditCutMenu_Click);
      // 
      // EditCopyMenu
      // 
      this.EditCopyMenu.Index = 1;
      this.EditCopyMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
      this.EditCopyMenu.Text = "C&opy";
      this.EditCopyMenu.Click += new System.EventHandler(this.EditCopyMenu_Click);
      // 
      // EditPasteMenu
      // 
      this.EditPasteMenu.Index = 2;
      this.EditPasteMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
      this.EditPasteMenu.Text = "&Paste";
      this.EditPasteMenu.Click += new System.EventHandler(this.EditPasteMenu_Click);
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 3;
      this.menuItem5.Text = "-";
      // 
      // EditNewMenu
      // 
      this.EditNewMenu.Index = 4;
      this.EditNewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                this.EditNewFolderMenu,
                                                                                this.EditNewFileMenu,
                                                                                this.EditNewZipFileMenu});
      this.EditNewMenu.Text = "&New";
      // 
      // EditNewFolderMenu
      // 
      this.EditNewFolderMenu.Index = 0;
      this.EditNewFolderMenu.Text = "&Folder...";
      this.EditNewFolderMenu.Click += new System.EventHandler(this.FileNewFolderMenu_Click);
      // 
      // EditNewFileMenu
      // 
      this.EditNewFileMenu.Index = 1;
      this.EditNewFileMenu.Text = "Fil&e...";
      this.EditNewFileMenu.Click += new System.EventHandler(this.EditNewFileMenu_Click);
      // 
      // EditNewZipFileMenu
      // 
      this.EditNewZipFileMenu.Index = 2;
      this.EditNewZipFileMenu.Text = "Zip file...";
      this.EditNewZipFileMenu.Click += new System.EventHandler(this.EditNewZipFileMenu_Click);
      // 
      // EditRenameMenu
      // 
      this.EditRenameMenu.Index = 5;
      this.EditRenameMenu.Shortcut = System.Windows.Forms.Shortcut.F2;
      this.EditRenameMenu.Text = "&Rename";
      this.EditRenameMenu.Click += new System.EventHandler(this.EditRenameMenu_Click);
      // 
      // EditDeleteMenu
      // 
      this.EditDeleteMenu.Index = 6;
      this.EditDeleteMenu.Shortcut = System.Windows.Forms.Shortcut.Del;
      this.EditDeleteMenu.Text = "&Delete";
      this.EditDeleteMenu.Click += new System.EventHandler(this.EditDeleteMenu_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 7;
      this.menuItem1.Text = "-";
      // 
      // EditRefreshMenu
      // 
      this.EditRefreshMenu.Index = 8;
      this.EditRefreshMenu.Shortcut = System.Windows.Forms.Shortcut.F5;
      this.EditRefreshMenu.Text = "R&efresh";
      this.EditRefreshMenu.Click += new System.EventHandler(this.EditRefreshMenu_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 9;
      this.menuItem2.Text = "-";
      // 
      // EditSplitMenu
      // 
      this.EditSplitMenu.Index = 10;
      this.EditSplitMenu.Text = "Split / Unsplit zip file...";
      this.EditSplitMenu.Click += new System.EventHandler(this.EditSplitMenu_Click);
      // 
      // HelpMenu
      // 
      this.HelpMenu.Index = 2;
      this.HelpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.HelpAboutMenu});
      this.HelpMenu.Text = "&Help";
      // 
      // HelpAboutMenu
      // 
      this.HelpAboutMenu.Index = 0;
      this.HelpAboutMenu.Text = "&About MiniExplorer";
      this.HelpAboutMenu.Click += new System.EventHandler(this.HelpAboutMenu_Click);
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.splitter1.Location = new System.Drawing.Point(0, 363);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(688, 3);
      this.splitter1.TabIndex = 7;
      this.splitter1.TabStop = false;
      // 
      // splitter2
      // 
      this.splitter2.Location = new System.Drawing.Point(200, 0);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(3, 363);
      this.splitter2.TabIndex = 8;
      this.splitter2.TabStop = false;
      // 
      // ProgressPanel
      // 
      this.ProgressPanel.Controls.Add(this.CurrentProgressBar);
      this.ProgressPanel.Controls.Add(this.AllProgressBar);
      this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.ProgressPanel.Location = new System.Drawing.Point(0, 435);
      this.ProgressPanel.Name = "ProgressPanel";
      this.ProgressPanel.Size = new System.Drawing.Size(688, 16);
      this.ProgressPanel.TabIndex = 9;
      // 
      // CurrentProgressBar
      // 
      this.CurrentProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
      this.CurrentProgressBar.Location = new System.Drawing.Point(0, 0);
      this.CurrentProgressBar.Name = "CurrentProgressBar";
      this.CurrentProgressBar.Size = new System.Drawing.Size(192, 16);
      this.CurrentProgressBar.TabIndex = 8;
      // 
      // AllProgressBar
      // 
      this.AllProgressBar.Dock = System.Windows.Forms.DockStyle.Right;
      this.AllProgressBar.Location = new System.Drawing.Point(198, 0);
      this.AllProgressBar.Name = "AllProgressBar";
      this.AllProgressBar.Size = new System.Drawing.Size(490, 16);
      this.AllProgressBar.TabIndex = 7;
      // 
      // MiniExplorerForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(688, 451);
      this.Controls.Add(this.FileList);
      this.Controls.Add(this.splitter2);
      this.Controls.Add(this.FolderTree);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.ResultList);
      this.Controls.Add(this.ProgressPanel);
      this.Menu = this.MiniExplorerMenu;
      this.Name = "MiniExplorerForm";
      this.Text = "Xceed MiniExplorer";
      this.Load += new System.EventHandler(this.MiniExplorer_Load);
      this.ProgressPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      Application.Run(new MiniExplorerForm());
    }

    #region FOLDERTREE EVENTS

    private void FolderTree_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
    {
      FillFolderTree( ( FolderTreeNode )e.Node );
    }

    private void FolderTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
      FillFileList( ( FolderTreeNode ) e.Node );
    }

    private void FolderTree_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
    {
      // We always remove the node that was used to rename this new folder.
      FolderTreeNode node = ( FolderTreeNode )e.Node.Parent;  

      string newLabel = e.Label;

      if( newLabel == null )
        newLabel = e.Node.Text;
      
      if( ( !e.CancelEdit ) && ( newLabel != null ) )
      {
        try
        {
          if( ( ( ( ( FolderTreeNode )e.Node ).Folder == null ) || ( newLabel != e.Node.Text ) ) &&
            ( node.Folder.GetFolder( newLabel ).Exists ) )
          {
            MessageBox.Show( "This folder already exists", "Error creating folder", 
              MessageBoxButtons.OK, MessageBoxIcon.Error );
            e.CancelEdit = true;
            e.Node.BeginEdit();
            return;
          }          

          if( ( ( FolderTreeNode )e.Node ).Folder == null )
          {            
            // Create the REAL folder and the node for it.
            AbstractFolder newFolder = node.Folder.CreateFolder( newLabel );
            FolderTreeNode newNode = new FolderTreeNode( newFolder, newLabel );

            node.Nodes.Add( newNode );
          }
          else
          {
            // Rename
            ( ( FolderTreeNode )e.Node ).Folder.Name = newLabel;
          }
        }        
        catch( Exception except )
        {
          ResultList.Items.Add( except.Message );
        }
      }

      // We remove the dummy node only at the very end
      if( e.Node is FolderTreeNode )
      {
        if( ( ( FolderTreeNode )e.Node ).Folder == null )
        {
          // This was a new folder and not a rename.
          e.Node.Remove();
        }
      }
    }

    private void FolderTree_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
    {
      ResultList.Items.Clear();

      DataObject data = new DataObject();
      data.SetData( typeof( FolderTreeNode ), e.Item );
      DoDragDrop( data, DragDropEffects.Copy | DragDropEffects.Move );
    }

    private void FolderTree_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
      TreeNode node = FolderTree.GetNodeAt( this.PointToClient( new Point( e.X, e.Y ) ) );
      AbstractFolder sourceFolder = null;

      e.Effect = DragDropEffects.None;

      // We don't allow to drop the file/folder in the source folder
      // Check for this.
      if( e.Data.GetDataPresent( typeof( FolderTreeNode ) ) )
      {
        // Copy/move the folder. The source folder is provided in e.Data
        sourceFolder = ( ( FolderTreeNode )e.Data.GetData( typeof( FolderTreeNode ) ) ).Folder;
      }
      else
      {
        // Copy/move the file(s). The source folder is the selected one.
        sourceFolder = ( ( FolderTreeNode ) FolderTree.SelectedNode ).Folder;
      }

      if( node != null && !( ( FolderTreeNode )node ).Folder.Equals( sourceFolder ) )
      {
        if( !node.Equals( m_previousDropNode ) )
        {
          HighlightDropInNode( node );
        }

        if( ( e.KeyState & 4 ) > 0 && ( e.AllowedEffect & DragDropEffects.Move ) > 0 )
          e.Effect = DragDropEffects.Move;
        else if( ( e.AllowedEffect & DragDropEffects.Copy ) > 0 )
          e.Effect = DragDropEffects.Copy;
      }
    }

    private void FolderTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.GetNodeAt( PointToClient( new Point( e.X, e.Y ) ) );

      HighlightDropInNode( null );

      try
      {
        if( e.Effect == DragDropEffects.Copy )
        {
          PasteFromDataObject( node, e.Data, false );
        }
        else if( e.Effect == DragDropEffects.Move )
        {
          PasteFromDataObject( node, e.Data, true );
        }
      }
      catch( Exception except )
      {
        ResultList.Items.Add( except.Message );
      }
    }

    private void FolderTree_DragLeave(object sender, System.EventArgs e)
    {
      HighlightDropInNode( null );
    }

    private void FolderTree_Enter(object sender, System.EventArgs e)
    {
      EditNewFolderMenu.Enabled = true;
    }

    private void FolderTree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      // Check if only the Ctrl key is pressed
      if( e.Modifiers == Keys.Control )
      {
        switch( e.KeyCode )
        {
          case Keys.Insert:
            CopyFolderInClipboard( false );
            break;
        }
      }

      // Check if only the Shift key is pressed
      if( e.Modifiers == Keys.Shift )
      {
        switch( e.KeyCode )
        {
          case Keys.Insert:
            PasteFromClipboard();
            break;

          case Keys.Delete:
            CopyFolderInClipboard( true );
            break;
        }
      }
    }

    private void FolderTree_Leave(object sender, System.EventArgs e)
    {
      EditNewFolderMenu.Enabled = false;
    }

    #endregion FOLDERTREE EVENTS

    #region FILELIST EVENTS

    private void FileList_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;
      string newLabel = e.Label;

      if( newLabel == null )
        newLabel = FileList.Items[ e.Item ].Text;

      // Check if the file already exists
      if( newLabel != null )
      {
        if( ( ( ( ( FileListItem ) FileList.Items[ e.Item ] ).File == null ) ||
              ( newLabel != FileList.Items[ e.Item ].Text ) ) &&
          ( node.Folder.GetFile( newLabel ).Exists ) )
        {
          MessageBox.Show( "This file already exists", "Error creating file", 
            MessageBoxButtons.OK, MessageBoxIcon.Error );
          e.CancelEdit = true;
          FileList.Items[ e.Item ].BeginEdit();
          return;
        }
      }

      if( !e.CancelEdit && newLabel != null )
      {
        try
        {
          if( ( ( FileListItem ) FileList.Items[ e.Item ] ).File == null )
          {
            // We always remove the node that was used to rename this new zip file.
            FileList.Items[ e.Item ].Remove();

            AbstractFile newFile = node.Folder.CreateFile( newLabel, false );
            FileListItem newItem = new FileListItem( newFile );

            FileList.Items.Add( newItem );
          }
          else
          {
            // Rename
            ( ( FileListItem ) FileList.Items[ e.Item ] ).File.Name = newLabel;
          }

          // We must also refresh the FolderTree since this file may end by ".zip"
          RefreshFolderTree( node );
        }
        catch( Exception except )
        {
          ResultList.Items.Add( except.Message );
        }
      }
    }

    private void FileList_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
    {
      AbstractFile[] files = new AbstractFile[ FileList.SelectedItems.Count ];
      int index = 0;

      foreach( FileListItem item in FileList.SelectedItems )
      {
        files[ index++ ] = item.File;
        ResultList.Items.Add( "Dragging " + item.File.FullName );
      }

      DataObject data = new DataObject( files );
      DoDragDrop( data, DragDropEffects.Copy | DragDropEffects.Move );
    }


    private void FileList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      // Check if only the Ctrl key is pressed
      if( e.Modifiers == Keys.Control )
      {
        switch( e.KeyCode )
        {
          case Keys.Insert:
            CopyFilesInClipboard( false );
            break;
        }
      }

      // Check if only the Shift key is pressed
      if( e.Modifiers == Keys.Shift )
      {
        switch( e.KeyCode )
        {
          case Keys.Insert:
            PasteFromClipboard();
            break;

          case Keys.Delete:
            CopyFilesInClipboard( true );
            break;
        }
      }
    }
    #endregion FILELIST EVENTS

    #region MENU EVENTS

    private void EditCutMenu_Click(object sender, System.EventArgs e)
    {
      if( FolderTree.Focused )
      {
        CopyFolderInClipboard( true );
      }
      else if( FileList.Focused )
      {
        CopyFilesInClipboard( true );
      }
    }

    private void EditCopyMenu_Click(object sender, System.EventArgs e)
    {
      if( FolderTree.Focused )
      {
        CopyFolderInClipboard( false );
      }
      else if( FileList.Focused )
      {
        CopyFilesInClipboard( false);
      }
    }

    private void EditPasteMenu_Click(object sender, System.EventArgs e)
    {
      PasteFromClipboard();
    }

    private void EditDeleteMenu_Click(object sender, System.EventArgs e)
    {
      if( FolderTree.Focused )
      {
        DeleteCurrentFolder();
      }
      else if( FileList.Focused )
      {
        DeleteCurrentFile();
      }
    }

    private void EditNewFileMenu_Click(object sender, System.EventArgs e)
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

      if( node != null )
      {
        string name = "New file";
        int number = 2;

        while( node.Folder.GetFile( name ).Exists || node.Folder.GetFolder( name ).Exists )
        {
          name = "New file (" + number.ToString() + ")";
          number++;        
        }

        FileListItem newFile = new FileListItem( name );
        FileList.Items.Add( newFile );

        newFile.BeginEdit();
      }
    }

    private void EditNewZipFileMenu_Click(object sender, System.EventArgs e)
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

      if( node != null )
      {
        string name = "New file.zip";
        int number = 2;

        while( node.Folder.GetFile( name ).Exists || node.Folder.GetFolder( name ).Exists )
        {
          name = "New file (" + number.ToString() + ").zip";
          number++;        
        }

        FileListItem newFile = new FileListItem( name );
        FileList.Items.Add( newFile );

        newFile.BeginEdit();
      }
    }

    private void EditRefreshMenu_Click(object sender, System.EventArgs e)
    {
      FillFileList( ( FolderTreeNode )FolderTree.SelectedNode );
    }

    private void EditRenameMenu_Click(object sender, System.EventArgs e)
    {
      if( FolderTree.Focused )
      {
        RenameCurrentFolder();
      }
      else if( FileList.Focused )
      {
        RenameCurrentFile();
      }
    }

    private void EditSplitMenu_Click( object sender, System.EventArgs e )
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

      if( ( node != null ) && ( node.Folder is ZipArchive ) )
      {
        ZipArchive zip = ( ZipArchive )node.Folder;

        long splitSize = zip.SplitSize;

        SplitSizeForm splitForm = new SplitSizeForm();

        if( splitForm.ShowDialog( this, ref splitSize ) == DialogResult.OK )
        {
          zip.BeginUpdate( m_zipEvents, null );
          Cursor.Current = Cursors.WaitCursor;

          try
          {
            zip.SplitSize = splitSize;
            zip.SplitNameFormat = SplitNameFormat.PkZip;

            // Setting the above properties does not flag the zip file to update itself.
            // Since we have no other modification to make, we can use the Comment property
            // as a way to tell the zip file to update itself!
            zip.Comment = "!";
            zip.Comment = "";
          }
          finally
          {
            zip.EndUpdate( m_zipEvents, null );
            Cursor.Current = Cursors.Default;

            RefreshFolderTree( node );
            FillFileList( ( FolderTreeNode )FolderTree.SelectedNode );
          }
        }
      }
    }

    private void FileNewFolderMenu_Click(object sender, System.EventArgs e)
    {
      if( FolderTree.Focused )
      {
        FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

        if( node != null )
        {
          node.Expand();

          string name = "New folder";
          int number = 2;

          while( node.Folder.GetFolder( name ).Exists || node.Folder.GetFile( name ).Exists )
          {
            name = "New folder (" + number.ToString() + ")";
            number++;
          }

          FolderTreeNode newFolder = new FolderTreeNode( name );
          node.Nodes.Add( newFolder );

          // Expand again, without refreshing view, if newly added item is alone
          if( node.Nodes.Count == 1 )
          {
            m_preventTreeUpdate = true;
            node.Expand();
            m_preventTreeUpdate = false;
          }

          newFolder.BeginEdit();
        }
      }
    }

    private void FileQuitMenu_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void HelpAboutMenu_Click(object sender, System.EventArgs e)
    {
      MessageBox.Show("Xceed Zip for .NET - MiniExplorer Sample Application\n" + 
                      "Written in C#\n" +
                      "Copyrights (c) 2000-2003 - Xceed Software Inc.", 
                      "About MiniExplorer...", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    #endregion MENU EVENTS

    #region FILESYSTEM EVENTS

    private void m_zipEvents_ByteProgression(object sender, Xceed.FileSystem.ByteProgressionEventArgs e)
    {
      CurrentProgressBar.Value = e.CurrentFileBytes.Percent;
      AllProgressBar.Value = e.AllFilesBytes.Percent;

      if( e.AllFilesBytes.Percent == 100 )
      {
        CurrentProgressBar.Value = 0;
        AllProgressBar.Value = 0;
      }
    }

    private void m_zipEvents_ItemException(object sender, Xceed.FileSystem.ItemExceptionEventArgs e)
    {
      if( e.Exception is InvalidDecryptionPasswordException )
      {
        try
        {
          ZipArchive rootZip = ( ZipArchive )( ( ZippedFile )e.CurrentItem ).RootFolder;
          if( m_currentPassword == rootZip.DefaultDecryptionPassword )
          {
            PasswordForm passwordDialog = new PasswordForm();

            if( passwordDialog.ShowDialog( this, e.CurrentItem.FullName, ref m_currentPassword ) == DialogResult.OK )
            {
              rootZip.DefaultDecryptionPassword = m_currentPassword;
              e.Action = ItemExceptionAction.Retry;
            }
          }
          else
          {
            rootZip.DefaultDecryptionPassword = m_currentPassword;
            e.Action = ItemExceptionAction.Retry;
          }
        }
        catch( Exception except )
        {
          System.Diagnostics.Debug.WriteLine( except.Message );
        }
      }
      else if( e.Exception is ItemIsReadOnlyException )
      {
        DialogResult answer = MessageBox.Show( 
          e.Exception.Message + "\n" + "\nDo you wish to remove the read-only attribute on this item?", e.Exception.GetType().ToString() + "...", 
          MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question );

        switch( answer )
        {
          case DialogResult.Yes:
            try
            {
              // An ItemIsReadOnlyException exception may occur if overwriting an existing
              // read-only item (then TargetItem is the one), or when deleting a read-only
              // file (then CurrentItem is the one and TargetItem is null).
              if( e.TargetItem == null )
              {
                e.CurrentItem.Attributes &= ~( System.IO.FileAttributes.ReadOnly );
              }
              else
              {
                e.TargetItem.Attributes &= ~( System.IO.FileAttributes.ReadOnly );
              }

              e.Action = ItemExceptionAction.Retry;
            }
            catch( Exception except )
            {
              System.Diagnostics.Debug.WriteLine( except.ToString() );
            }
            break;

          case DialogResult.No:
            e.Action = ItemExceptionAction.Ignore;
            break;

          case DialogResult.Cancel:
            e.Action = ItemExceptionAction.Abort;
            break;
        }
      }
      else
      {
        DialogResult answer = MessageBox.Show( 
          e.Exception.Message + "\n" + "\nWhat do you wish to do?", e.Exception.GetType().ToString() + "...", 
          MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question );

        switch( answer )
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
    }

    #endregion FILESYSTEM EVENTS

    #region ZIP EVENTS

    private void m_zipEvents_BuildingZipByteProgression(object sender, Xceed.FileSystem.ByteProgressionEventArgs e)
    {
      if( e.AllFilesBytes.Percent != m_buildingProgression )
      {
        ResultList.Items.Add( "Building Temporary Zip file " + e.AllFilesBytes.Percent +" % Done.");
        m_buildingProgression = e.AllFilesBytes.Percent;
      }

      if( e.AllFilesBytes.Percent == 100 )
        m_buildingProgression = 255;
    }

    private void m_zipEvents_DiskRequired( object sender, DiskRequiredEventArgs e )
    {
      if( e.Action == DiskRequiredAction.Fail )
      {
        // The user must provide us with the required file part. The library
        // cannot automatically find the required zip file part.

        // Now if the reason is for deleting useless disks, we simply skip that
        // step by setting the Action to Fail. This instructs the library to
        // skip that step without error.
        if( e.Reason == DiskRequiredReason.Deleting )
        {
          e.Action = DiskRequiredAction.Fail;
        }
        else
        {
          DiskRequiredForm diskForm = new DiskRequiredForm();

          AbstractFile zipFile = e.ZipFile;

          if( diskForm.ShowDialog( this, ref zipFile, e.DiskNumber, e.Reason ) == DialogResult.OK )
          {
            e.ZipFile = zipFile;
            e.Action = DiskRequiredAction.Continue;
          }
        }
      }
      else
      {
        // When the default action is to continue, we give Xceed Zip a chance with
        // his split name formating.
        ResultList.Items.Add( "Switching to file " + e.ZipFile.FullName );
      }
    }
    
    #endregion ZIP EVENTS

    #region FORM EVENTS

    private void MiniExplorer_Load(object sender, System.EventArgs e)
    {
      m_zipEvents = new ZipEvents();
      m_zipEvents.ByteProgression += new ByteProgressionEventHandler( m_zipEvents_ByteProgression );
      m_zipEvents.ItemException += new ItemExceptionEventHandler( m_zipEvents_ItemException );
      m_zipEvents.BuildingZipByteProgression += new BuildingZipByteProgressionEventHandler( m_zipEvents_BuildingZipByteProgression );
      m_zipEvents.DiskRequired += new DiskRequiredEventHandler( m_zipEvents_DiskRequired );
    }

    #endregion FORM EVENTS

    #region PRIVATE METHODS

    private void CopyFolderInClipboard( bool cut )
    {
      m_Clipboard = new DataObject();

      m_Clipboard.SetData( typeof( FolderTreeNode ), ( FolderTreeNode )FolderTree.SelectedNode );
      // As a goodie, we provide a textual representation of the Folder in the Clipboard
      m_Clipboard.SetData( DataFormats.Text, ( ( FolderTreeNode )FolderTree.SelectedNode ).Folder.FullName );
      System.Windows.Forms.Clipboard.SetDataObject( m_Clipboard );

      m_cutClipboard = cut;
    }

    private void CopyFilesInClipboard( bool cut )
    {
      AbstractFile[] files = new AbstractFile[ FileList.SelectedItems.Count ];
      int index = 0;
      System.Text.StringBuilder textContent = new System.Text.StringBuilder();

      foreach( FileListItem item in FileList.SelectedItems )
      {
        files[ index++ ] = item.File;
        // As a goodie, we provide a textual representation of each selected file in the Clipboard
        textContent.Append( item.File.FullName + "\r\n" );
      }

      m_Clipboard = new DataObject();

      m_Clipboard.SetData( typeof( AbstractFile[] ), files );
      m_Clipboard.SetData( DataFormats.Text, textContent );
      Clipboard.SetDataObject( m_Clipboard );

      m_cutClipboard = cut;
    }

    private void PasteFromClipboard()
    {
      if( FolderTree.Focused || FileList.Focused )
      {
        // We use the m_Clipboard object because of a bug in the Clipboard.GetDataObject().
        PasteFromDataObject( ( FolderTreeNode )FolderTree.SelectedNode, m_Clipboard, m_cutClipboard );
        
        // The potential following "pastes" shall not "cut"
        m_cutClipboard = false;
      }
    }

    private void PasteFromDataObject( FolderTreeNode destinationNode, IDataObject dataObject, bool move )
    {
      if( move )
      {
        ResultList.Items.Add( "Moving to folder " + destinationNode.Folder.FullName + "..." );
      }
      else
      {
        ResultList.Items.Add( "Copying to folder " + destinationNode.Folder.FullName + "..." );
      }

      IBatchUpdateable batch = destinationNode.Folder.RootFolder as IBatchUpdateable;

      if( batch != null )
        batch.BeginUpdate( m_zipEvents, null );

      Cursor.Current = Cursors.WaitCursor;

      try
      {
        if( dataObject.GetDataPresent( typeof( FolderTreeNode ) ) )
        {
          FolderTreeNode sourceNode = ( FolderTreeNode )dataObject.GetData( typeof( FolderTreeNode ) );
          AbstractFolder folder = sourceNode.Folder;

          ResultList.Items.Add( " folder " + folder.FullName );

          if( move )
          {
            // If we are attempting to move a zip file, we must move the
            // file itself, not the ZipArchive representing the root folder
            // of the zip file contents.
            if( folder is ZipArchive )
            {
              ( ( ZipArchive )folder ).ZipFile.MoveTo( m_zipEvents, null, destinationNode.Folder, false );
            }
            else
            {
              folder.MoveTo( m_zipEvents, null, destinationNode.Folder, false );
            }

            // We just moved the folder. Refresh its (previous) parent.
            RefreshFolderTree( ( FolderTreeNode )sourceNode.Parent );
          }
          else
          {
            // If we are attempting to copy a zip file, we must copy the
            // file itself, not the ZipArchive representing the root folder
            // of the zip file contents.
            if( folder is ZipArchive )
            {
              ( ( ZipArchive )folder ).ZipFile.CopyTo( m_zipEvents, null, destinationNode.Folder, false );
            }
            else
            {
              folder.CopyTo( m_zipEvents, null, destinationNode.Folder, false );
            }
          }
        }
        else if( dataObject.GetDataPresent( typeof( AbstractFile[] ) ) )
        {
          foreach( AbstractFile file in ( AbstractFile[] )dataObject.GetData( typeof( AbstractFile[] ) ) )
          {
            ResultList.Items.Add( " file " + file.FullName );
            if( move )
            {
              file.MoveTo( m_zipEvents, null, destinationNode.Folder, true );
            }
            else
            {
              file.CopyTo( m_zipEvents, null, destinationNode.Folder, true );              
            }
          }
        }
        else
        {
          ResultList.Items.Add( "Unknown data type." );
        }
      }
      finally
      {
        if( batch != null )
          batch.EndUpdate( m_zipEvents, null );

        Cursor.Current = Cursors.Default;
      }

      RefreshFolderTree( destinationNode );
      FillFileList( ( FolderTreeNode )FolderTree.SelectedNode );
    }

    private void RefreshFolderTree( FolderTreeNode rootNode )
    {
      if( rootNode.IsExpanded )
      {
        rootNode.Collapse();
        rootNode.Expand();
      }
      else if( rootNode.Nodes.Count == 0 )
      {
        // This folder may have a newly copied/moved subfolder or zip file.
        // Re-add the initial dummy child so it has a [+] sign.
        rootNode.Nodes.Add( string.Empty );
      }
    }

    private void RenameCurrentFolder()
    {
      if( FolderTree.SelectedNode != null )
      {
        FolderTree.SelectedNode.BeginEdit();
      }
    }

    private void RenameCurrentFile()
    {
      if( FileList.SelectedItems.Count > 0 )
      {
        FileList.SelectedItems[ 0 ].BeginEdit();
      }
    }

    private void FillFolderTree( FolderTreeNode node )
    {
      if( !m_preventTreeUpdate )
      {
        Cursor.Current = Cursors.WaitCursor;
        node.Nodes.Clear();

        try
        {
          // We want to display zip files as folders
          foreach( AbstractFile file in node.Folder.GetFiles( false, "*.zip" ) )
          {
            try
            {
              /* Not anymore!!!
              // We can't show the content of zip file that is in a zip.
              // Since we'd have to unzip it on disk to do so.
              if(!( file is ZippedFile ) )
              */
              {
                FolderTreeNode sub = new FolderTreeNode( new ZipArchive( m_zipEvents, null, file ), file.Name );
                sub.ForeColor = Color.Green;
                node.Nodes.Add( sub );

                // We always allow spanning of modified zip files
                ( ( ZipArchive )sub.Folder ).AllowSpanning = true;
              }
            }
            catch( Exception except )
            {
              ResultList.Items.Add( except.Message );
            }
          }
        }
        catch( Exception except )
        {
          ResultList.Items.Add( except.Message );
        }

        try
        {
          // We must not forget normal folders  
          foreach( AbstractFolder folder in node.Folder.GetFolders( false ) )
          {
            try
            {
              FolderTreeNode sub = new FolderTreeNode( folder, folder.Name );
              node.Nodes.Add( sub );
            }
            catch( Exception except )
            {
              ResultList.Items.Add( except.Message );
            }
          }
        }
        catch( Exception except )
        {
          ResultList.Items.Add( except.Message );
        }

        Cursor.Current = Cursors.Default;
      }
    }

    private void FillFileList( FolderTreeNode node )
    {
      FileList.Items.Clear();

      if( node != null )
      {
        Cursor.Current = Cursors.WaitCursor;

        try
        {
          foreach( AbstractFile file in node.Folder.GetFiles( false ) )
          {
            try
            {
              FileListItem item = new FileListItem( file );
              FileList.Items.Add( item );
            }
            catch( Exception except )
            {
              ResultList.Items.Add( except.Message );
            }
          }
        }
        catch( Exception except )
        {
          ResultList.Items.Add( except.Message );
        }

        Cursor.Current = Cursors.Default;
      }
    }

    private void HighlightDropInNode( TreeNode node )
    {
      if( node != null )
      {
        node.BackColor = Color.FromKnownColor( KnownColor.Highlight );
        node.ForeColor = Color.FromKnownColor( KnownColor.HighlightText );
      }

      if( m_previousDropNode != null )
      {
        if( ( ( FolderTreeNode )m_previousDropNode ).Folder is ZipArchive )
        {
          m_previousDropNode.BackColor = Color.FromKnownColor( KnownColor.Window );
          m_previousDropNode.ForeColor = Color.Green;
        }
        else
        {
          m_previousDropNode.BackColor = Color.FromKnownColor( KnownColor.Window );
          m_previousDropNode.ForeColor = Color.FromKnownColor( KnownColor.WindowText );
        }
      }

      m_previousDropNode = node;
    }

    private void DeleteCurrentFolder()
    {
      FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

      if( node != null )
      {
        string folderName = string.Empty;

        if( node.Folder is ZipArchive )
        {
          // The name of the "folder" is the name of the zip file
          folderName = ( ( ZipArchive )node.Folder ).ZipFile.FullName;
        }
        else
        {
          folderName = node.Folder.FullName;
        }

        DialogResult result = MessageBox.Show(
          "Please confirm you wish to delete the following folder:\n" + folderName,
          "Deleting folder...", MessageBoxButtons.YesNo, MessageBoxIcon.Question );

        if( result == DialogResult.Yes )
        {
          try
          {
            if( node.Folder is ZipArchive )
            {
              // Delete the zip file
              ( ( ZipArchive )node.Folder ).ZipFile.Delete( m_zipEvents, null );
            }
            else
            {
              // Delete the folder
              node.Folder.Delete( m_zipEvents, null );
            }

            node.Remove();
          }
          catch( Exception except )
          {
            ResultList.Items.Add( except.Message );
          }
        }
      }
    }

    private void DeleteCurrentFile()
    {
      DialogResult result = DialogResult.No;

      if( FileList.SelectedItems.Count == 1 )
      {
        FileListItem item = ( FileListItem )FileList.SelectedItems[ 0 ];

        result = MessageBox.Show( 
          "Please confirm you wish to delete the following file:\n" + item.File.FullName,
          "Deleting file...", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
      }
      else if( FileList.SelectedItems.Count > 1 )
      {
        result = MessageBox.Show( 
          "Please confirm you wish to delete all selected files.",
          "Deleting files...", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
      }

      if( result == DialogResult.Yes )
      {
        FolderTreeNode node = ( FolderTreeNode )FolderTree.SelectedNode;

        IBatchUpdateable batch = node.Folder.RootFolder as IBatchUpdateable;

        if( batch != null )
          batch.BeginUpdate( m_zipEvents, null );

        try
        {
          foreach( FileListItem item in FileList.SelectedItems )
          {
            try
            {
              item.File.Delete( m_zipEvents, null );

              // We delete the file entry in the list view
              FileList.Items.Remove( item );
            }
            catch( Exception except )
            {
              ResultList.Items.Add( except.Message );
            }
          }
        }
        finally
        {
          if( batch != null )
            batch.EndUpdate( m_zipEvents, null );

          // We must update the FolderTree since we may have deleted zip files.
          RefreshFolderTree( node );
        }
      }
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private bool m_preventTreeUpdate = false;
    private TreeNode m_previousDropNode = null;
    private DataObject m_Clipboard = null;
    private bool m_cutClipboard = false;
    private ZipEvents m_zipEvents = null;
    private byte m_buildingProgression = 255;
    
    private string m_currentPassword = string.Empty;

    #endregion PRIVATE FIELDS
	}
}
