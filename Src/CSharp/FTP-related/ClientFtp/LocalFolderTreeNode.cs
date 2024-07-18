using System;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace ClientFtp
{
	public class LocalFolderTreeNode : TreeNode
	{
		public LocalFolderTreeNode( AbstractFolder folder )
		{
      if( folder == null )
        throw new ArgumentNullException( "folder" );

      m_folder = folder;

      if( folder.IsRoot )
      {
        this.Text = folder.FullName;
      }
      else
      {
        this.Text = folder.Name;
      }

      this.ImageIndex = ( int ) FtpItemIconEnum.ClosedFolder;
      this.SelectedImageIndex = ( int ) FtpItemIconEnum.OpenedFolder;

      // Make sure a [+] appears, using a dummy node.
      this.Nodes.Add( new TreeNode( "Browsing folder contents..." ) );
    }

    public AbstractFolder Folder
    {
      get { return m_folder; }
    }

    public void UpdateContents()
    {
      // Make sure to stay in the same expanded state
      bool expanded = this.IsExpanded;
      AbstractFolder[] folders = null;

      try
      {
        folders = m_folder.GetFolders( false );
      }
      finally
      {
        // Make sure to empty even if we get an exception.
        this.Nodes.Clear();
      }

      foreach( AbstractFolder folder in folders )
      {
        this.Nodes.Add( new LocalFolderTreeNode( folder ) );
      }

      if( expanded )
      {
        this.Expand();
      }
    }

    public void FillList( ListView contents )
    {
      FileSystemItem[] items = null;

      try
      {
        items = m_folder.GetItems( false );
      }
      finally
      {
        // Make sure to empty even if we get an exception.
        contents.Items.Clear();
      }

      foreach( FileSystemItem item in items )
      {
        contents.Items.Add( new LocalListViewItem( item ) );
      }
    }

    private AbstractFolder m_folder = null;
	}
}
