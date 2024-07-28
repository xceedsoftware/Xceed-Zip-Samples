using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace FolderViews
{
	/// <summary>
	/// Summary description for FolderForm.
	/// </summary>
	public class FolderForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.TreeView folderTreeView;
    private System.Windows.Forms.ListView fileListView;
    private System.Windows.Forms.ColumnHeader filenameColumn;
    private System.Windows.Forms.ColumnHeader sizeColumn;
    private System.Windows.Forms.ColumnHeader lastModifiedColumn;
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuFileClose;
    private System.Windows.Forms.MenuItem menuItem1;
    private System.Windows.Forms.MenuItem menuDelete;
    private System.Windows.Forms.MenuItem menuCreateFolder;
    private System.Windows.Forms.MenuItem menuCut;
    private System.Windows.Forms.MenuItem menuCopy;
    private System.Windows.Forms.MenuItem menuPaste;
    private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FolderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

    public FolderForm( string title, params AbstractFolder[] folders )
    {
      InitializeComponent();

      this.Text = title;

      foreach( AbstractFolder folder in folders )
      {
        folderTreeView.Nodes.Add( new FolderTreeNode( folder ) );
      }
    }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
      this.folderTreeView = new System.Windows.Forms.TreeView();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.fileListView = new System.Windows.Forms.ListView();
      this.filenameColumn = new System.Windows.Forms.ColumnHeader();
      this.sizeColumn = new System.Windows.Forms.ColumnHeader();
      this.lastModifiedColumn = new System.Windows.Forms.ColumnHeader();
      this.mainMenu1 = new System.Windows.Forms.MainMenu();
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuFileClose = new System.Windows.Forms.MenuItem();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.menuCut = new System.Windows.Forms.MenuItem();
      this.menuCopy = new System.Windows.Forms.MenuItem();
      this.menuPaste = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuCreateFolder = new System.Windows.Forms.MenuItem();
      this.menuDelete = new System.Windows.Forms.MenuItem();
      this.SuspendLayout();
      // 
      // folderTreeView
      // 
      this.folderTreeView.AllowDrop = true;
      this.folderTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Left;
      this.folderTreeView.FullRowSelect = true;
      this.folderTreeView.HideSelection = false;
      this.folderTreeView.HotTracking = true;
      this.folderTreeView.ImageIndex = -1;
      this.folderTreeView.Location = new System.Drawing.Point(0, 0);
      this.folderTreeView.Name = "folderTreeView";
      this.folderTreeView.SelectedImageIndex = -1;
      this.folderTreeView.Size = new System.Drawing.Size(168, 330);
      this.folderTreeView.TabIndex = 0;
      this.folderTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.Any_DragOver);
      this.folderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTreeView_AfterSelect);
      this.folderTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.folderTreeView_BeforeExpand);
      this.folderTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.folderTreeView_ItemDrag);
      this.folderTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.Any_DragDrop);
      // 
      // splitter1
      // 
      this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
      this.splitter1.Location = new System.Drawing.Point(168, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 330);
      this.splitter1.TabIndex = 2;
      this.splitter1.TabStop = false;
      // 
      // fileListView
      // 
      this.fileListView.AllowDrop = true;
      this.fileListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.fileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                   this.filenameColumn,
                                                                                   this.sizeColumn,
                                                                                   this.lastModifiedColumn});
      this.fileListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.fileListView.FullRowSelect = true;
      this.fileListView.HideSelection = false;
      this.fileListView.Location = new System.Drawing.Point(171, 0);
      this.fileListView.Name = "fileListView";
      this.fileListView.Size = new System.Drawing.Size(421, 330);
      this.fileListView.TabIndex = 3;
      this.fileListView.View = System.Windows.Forms.View.Details;
      this.fileListView.DragOver += new System.Windows.Forms.DragEventHandler(this.Any_DragOver);
      this.fileListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.Any_DragDrop);
      this.fileListView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.fileListView_ItemDrag);
      // 
      // filenameColumn
      // 
      this.filenameColumn.Text = "Filename";
      this.filenameColumn.Width = 180;
      // 
      // sizeColumn
      // 
      this.sizeColumn.Text = "Size";
      this.sizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.sizeColumn.Width = 80;
      // 
      // lastModifiedColumn
      // 
      this.lastModifiedColumn.Text = "Last modified";
      this.lastModifiedColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.lastModifiedColumn.Width = 140;
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuFile,
                                                                              this.menuItem1});
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuFileClose});
      this.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuFile.Text = "&File";
      // 
      // menuFileClose
      // 
      this.menuFileClose.Index = 0;
      this.menuFileClose.MergeOrder = 3;
      this.menuFileClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
      this.menuFileClose.Text = "&Close folder view";
      this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 1;
      this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuCut,
                                                                              this.menuCopy,
                                                                              this.menuPaste,
                                                                              this.menuItem2,
                                                                              this.menuCreateFolder,
                                                                              this.menuDelete});
      this.menuItem1.MergeOrder = 10;
      this.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuItem1.Text = "&Edit";
      // 
      // menuCut
      // 
      this.menuCut.Index = 0;
      this.menuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
      this.menuCut.Text = "Cu&t";
      this.menuCut.Click += new System.EventHandler(this.menuCut_Click);
      // 
      // menuCopy
      // 
      this.menuCopy.Index = 1;
      this.menuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
      this.menuCopy.Text = "&Copy";
      this.menuCopy.Click += new System.EventHandler(this.menuCopy_Click);
      // 
      // menuPaste
      // 
      this.menuPaste.Index = 2;
      this.menuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
      this.menuPaste.Text = "&Paste";
      this.menuPaste.Click += new System.EventHandler(this.menuPaste_Click);
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 3;
      this.menuItem2.Text = "-";
      // 
      // menuCreateFolder
      // 
      this.menuCreateFolder.Index = 4;
      this.menuCreateFolder.MergeOrder = 11;
      this.menuCreateFolder.Shortcut = System.Windows.Forms.Shortcut.Ins;
      this.menuCreateFolder.Text = "&Create folder...";
      this.menuCreateFolder.Click += new System.EventHandler(this.menuCreateFolder_Click);
      // 
      // menuDelete
      // 
      this.menuDelete.Index = 5;
      this.menuDelete.MergeOrder = 11;
      this.menuDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
      this.menuDelete.Text = "&Delete";
      this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
      // 
      // FolderForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(592, 330);
      this.Controls.Add(this.fileListView);
      this.Controls.Add(this.splitter1);
      this.Controls.Add(this.folderTreeView);
      this.Menu = this.mainMenu1;
      this.Name = "FolderForm";
      this.Load += new System.EventHandler(this.FolderForm_Load);
      this.ResumeLayout(false);

    }
		#endregion

    // Can't use Clipboard, as items are not serializable.
    private static DataObject mg_clipboard = new DataObject();
    private static DragDropEffects mg_clipboardAction = DragDropEffects.None;

    private FileSystemEvents m_events = new FileSystemEvents();

    private MainForm MainForm
    {
      get { return this.ParentForm as MainForm; }
    }

    public void UpdateFileList()
    {
      FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

      if( selected == null )
      {
        fileListView.Items.Clear();
      }
      else
      {
        selected.OnAfterSelect( fileListView );
      }
    }

    private void folderTreeView_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
    {
      FolderTreeNode node = e.Node as FolderTreeNode;

      if( node == null )
      {
        System.Diagnostics.Debug.Fail( "Unexpected node type expanded." );
        e.Node.Nodes.Clear();
      }
      else
      {
        node.OnBeforeExpand();
      }
    }

    private void folderTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
    {
      FolderTreeNode node = e.Node as FolderTreeNode;

      if( node != null )
      {
        node.OnAfterSelect( fileListView );
      }
    }

    private void FolderForm_Load(object sender, System.EventArgs e)
    {
      m_events.ByteProgression += new ByteProgressionEventHandler(m_events_ByteProgression);
      if( folderTreeView.Nodes.Count > 0 )
      {
        folderTreeView.SelectedNode = folderTreeView.Nodes[ 0 ];
      }
    }

    private void folderTreeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
    {
      FolderTreeNode node = e.Item as FolderTreeNode;

      if( node != null )
      {
        DoDragDrop( node, DragDropEffects.Copy | DragDropEffects.Move );
      }
    }

    private void fileListView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
    {
      // We don't drag e.Item, but all selected items.
      if( fileListView.SelectedItems.Count > 0 )
      {
        FileListViewItem[] files = new FileListViewItem[ fileListView.SelectedItems.Count ];

        for( int index=0; index<fileListView.SelectedItems.Count; index++ )
        {
          files[ index ] = fileListView.SelectedItems[ index ] as FileListViewItem;
        }

        DoDragDrop( files, DragDropEffects.Copy | DragDropEffects.Move );
      }
    }

    private void Any_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
      e.Effect = DragDropEffects.None;

      if(  e.Data.GetDataPresent( typeof( FolderTreeNode ) )
        || e.Data.GetDataPresent( typeof( FileListViewItem[] ) ) )
      {
        if(  ( ( e.AllowedEffect & DragDropEffects.Move ) == DragDropEffects.Move )
          && ( ( e.KeyState & ( 4 + 8 + 32 ) ) == 4 ) )
        {
          // Shift + drag
          e.Effect = DragDropEffects.Move;
        }
        else if( ( e.AllowedEffect & DragDropEffects.Copy ) == DragDropEffects.Copy )
        {
          e.Effect = DragDropEffects.Copy;
        }
      }
    }

    private void Any_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      // We copy in the currently selected folder.
      FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

      if( selected != null )
      {
        this.CopyData( e.Data, e.Effect, selected );
      }
    }

    private void menuFileClose_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void menuCreateFolder_Click(object sender, System.EventArgs e)
    {
      FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

      if( selected != null )
      {
        if( !selected.IsExpanded )
        {
          selected.Expand();
        }

        TreeNode newFolder = new TreeNode( "new folder" );

        selected.Nodes.Add( newFolder );
        selected.TreeView.LabelEdit = true;
        selected.TreeView.AfterLabelEdit += new NodeLabelEditEventHandler( CreateFolder_AfterLabelEdit );

        newFolder.BeginEdit();
      }
    }

    private void CreateFolder_AfterLabelEdit( object sender, NodeLabelEditEventArgs e )
    {
      FolderTreeNode parent = e.Node.Parent as FolderTreeNode;

      // We unadvise for this event and remove this dummy node.
      e.Node.TreeView.LabelEdit = false;
      e.Node.TreeView.AfterLabelEdit -= new NodeLabelEditEventHandler( CreateFolder_AfterLabelEdit );
      e.Node.Remove();

      // Then check if we need to create an actual folder.
      if( ( parent != null ) && ( !e.CancelEdit ) )
      {
        AbstractFolder newFolder = parent.Folder.CreateFolder( e.Label );
        FolderTreeNode newNode = new FolderTreeNode( newFolder );

        parent.Nodes.Add( newNode );
        newNode.EnsureVisible();
        newNode.TreeView.SelectedNode = newNode;
      }
    }

    private void menuDelete_Click(object sender, System.EventArgs e)
    {
      // I don't like that, but heck...
      if( fileListView.Focused )
      {
        DialogResult answer = DialogResult.No;

        if( fileListView.SelectedItems.Count == 1 )
        {
          answer = MessageBox.Show(
            "Are you sure you wish to delete file \"" + fileListView.SelectedItems[ 0 ].Text + "\"?",
            "Confirm",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question );
        }
        else if( fileListView.SelectedItems.Count > 1 )
        {
          answer = MessageBox.Show(
            "Are you sure you wish to delete selected " + fileListView.SelectedItems.Count.ToString() + "files?",
            "Confirm",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question );
        }

        if( answer == DialogResult.Yes )
        {
          foreach( FileListViewItem item in fileListView.SelectedItems )
          {
            try
            {
              item.File.Delete();
              item.Remove();
            }
            catch( Exception except )
            {
              this.MainForm.DisplayInformation( except.Message );
            }
          }
        }
      }
      else
      {
        FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

        if( selected != null )
        {
          if( DialogResult.Yes == MessageBox.Show(
            "Are you sure you wish to delete folder \"" + selected.Folder.FullName + "\"?",
            "Confirm",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question ) )
          {
            try
            {
              selected.Folder.Delete();
              selected.Remove();
            }
            catch( Exception except )
            {
              this.MainForm.DisplayInformation( except.Message );
            }
          }
        }
      }
    }

    private void menuCut_Click(object sender, System.EventArgs e)
    {
      if( fileListView.Focused )
      {
        FileListViewItem[] files = new FileListViewItem[ fileListView.SelectedItems.Count ];

        for( int index=0; index<fileListView.SelectedItems.Count; index++ )
        {
          files[ index ] = fileListView.SelectedItems[ index ] as FileListViewItem;
        }

        mg_clipboard.SetData( files );
        mg_clipboardAction = DragDropEffects.Move;
      }
      else
      {
        FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

        if( selected != null )
        {
          mg_clipboard.SetData( selected );
          mg_clipboardAction = DragDropEffects.Move;
        }
      }
    }

    private void menuCopy_Click(object sender, System.EventArgs e)
    {
      if( fileListView.Focused )
      {
        FileListViewItem[] files = new FileListViewItem[ fileListView.SelectedItems.Count ];

        for( int index=0; index<fileListView.SelectedItems.Count; index++ )
        {
          files[ index ] = fileListView.SelectedItems[ index ] as FileListViewItem;
        }

        mg_clipboard.SetData( files );
        mg_clipboardAction = DragDropEffects.Copy;
      }
      else
      {
        FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

        if( selected != null )
        {
          mg_clipboard.SetData( selected );
          mg_clipboardAction = DragDropEffects.Copy;
        }
      }
    }

    private void menuPaste_Click(object sender, System.EventArgs e)
    {
      FolderTreeNode selected = folderTreeView.SelectedNode as FolderTreeNode;

      if( selected != null )
      {
        this.CopyData( 
          mg_clipboard, 
          mg_clipboardAction,
          selected );
      }
    }

    private void CopyData( 
      IDataObject data, 
      DragDropEffects action, 
      FolderTreeNode destination )
    {
      if( data.GetDataPresent( typeof( FolderTreeNode ) ) )
      {
        FolderTreeNode source = data.GetData( typeof( FolderTreeNode ) ) as FolderTreeNode;

        if( source != null )
        {
          this.Cursor = Cursors.WaitCursor;

          try
          {
            using( new AutoBatchUpdate( destination.Folder.RootFolder ) )
            {
              if( action == DragDropEffects.Copy )
              {
                this.MainForm.DisplayAction( "Copying " + source.Folder.FullName );
                source.Folder.CopyTo( m_events, null, destination.Folder, true );
              }
              else
              {
                System.Diagnostics.Debug.Assert( action == DragDropEffects.Move );

                using( new AutoBatchUpdate( source.Folder.RootFolder ) )
                {
                  this.MainForm.DisplayAction( "Moving " + source.Folder.FullName );
                  source.Folder.MoveTo( m_events, null, destination.Folder, true );
                }

                source.Remove();
              }
            }
          }
          catch( Exception except ) 
          {
            this.MainForm.DisplayInformation( except.Message );
          }
          finally
          {
            this.MainForm.DisplayAction( string.Empty );
            this.Cursor = Cursors.Default;
          }

          destination.Update( true );
          destination.FolderForm.UpdateFileList();
        }
      }
      else
      {
        FileListViewItem[] files = data.GetData( typeof( FileListViewItem[] ) ) as FileListViewItem[];

        if( ( files != null ) && ( files.Length > 0 ) )
        {
          this.Cursor = Cursors.WaitCursor;

          try
          {
            using( new AutoBatchUpdate( destination.Folder.RootFolder ) )
            {
              if( action == DragDropEffects.Copy )
              {
                foreach( FileListViewItem file in files )
                {
                  try
                  {
                    this.MainForm.DisplayAction( "Copying " + file.File.FullName );
                    file.File.CopyTo( m_events, null, destination.Folder, true );
                  }
                  catch( Exception except ) 
                  {
                    this.MainForm.DisplayInformation( except.Message );
                  }
                }
              }
              else
              {
                System.Diagnostics.Debug.Assert( action == DragDropEffects.Move );

                // We assume each file has the same root folder.
                using( new AutoBatchUpdate( files[ 0 ].File.RootFolder ) )
                {
                  foreach( FileListViewItem file in files )
                  {
                    try
                    {
                      this.MainForm.DisplayAction( "Moving " + file.File.FullName );
                      file.File.MoveTo( m_events, null, destination.Folder, true );
                      file.Remove();
                    }
                    catch( Exception except ) 
                    {
                      this.MainForm.DisplayInformation( except.Message );
                    }
                  }

                  //# TODO: I would prefer refreshing the parent folder node.
                }
              }
            }
          }
          catch( Exception except ) 
          {
            this.MainForm.DisplayInformation( except.Message );
          }
          finally
          {
            this.MainForm.DisplayAction( string.Empty );
            this.Cursor = Cursors.Default;
          }

          destination.Update( true );
          destination.FolderForm.UpdateFileList();
        }
      }
    }

    private void m_events_ByteProgression(object sender, ByteProgressionEventArgs e)
    {
      this.MainForm.DisplayProgress( e.AllFilesBytes.Percent );
    }
  }
}
