using System;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace FolderViews
{
	public class FolderTreeNode : TreeNode
	{
		public FolderTreeNode( AbstractFolder folder )
		{
      if( folder.Name.Length == 0 )
      {
        this.Text = folder.FullName;
      }
      else
      {
        this.Text = folder.Name;
      }

      this.Nodes.Add( new TreeNode() );

      m_folder = folder;
		}

    public AbstractFolder Folder
    {
      get { return m_folder; }
    }

    public FolderForm FolderForm
    {
      get 
      {
        if( this.TreeView == null )
          return null;

        return this.TreeView.FindForm() as FolderForm;
      }
    }

    public void OnBeforeExpand()
    {
      this.Update( false );
    }

    public void OnAfterSelect( ListView fileListView )
    {
      this.Update( false );

      fileListView.Items.Clear();

      foreach( FileSystemItem item in m_childItems )
      {
        AbstractFile file = item as AbstractFile;

        if( file != null )
        {
          fileListView.Items.Add( new FileListViewItem( file ) );
        }
      }
    }

    public void Update( bool forceUpdate )
    {
      if( forceUpdate || ( m_childItems == null ) )
      {
        this.TreeView.Cursor = Cursors.WaitCursor;

        try
        {
          m_childItems = m_folder.GetItems( false );
        }
        catch
        {
          // Most probably a missing permission. Report nothing.
          m_childItems = new FileSystemItem[ 0 ];
        }
        finally
        {
          this.TreeView.Cursor = Cursors.Default;
        }

        bool expanded = this.IsExpanded;

        this.Nodes.Clear();

        foreach( FileSystemItem item in m_childItems )
        {
          AbstractFolder folder = item as AbstractFolder;

          if( folder != null )
          {
            this.Nodes.Add( new FolderTreeNode( folder ) );
          }
        }

        if( expanded )
        {
          //# TODO
        }
      }
    }

    private AbstractFolder m_folder; // = null
    private FileSystemItem[] m_childItems; // = null
	}
}
