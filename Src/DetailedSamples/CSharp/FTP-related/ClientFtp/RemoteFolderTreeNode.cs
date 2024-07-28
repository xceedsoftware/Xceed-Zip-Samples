using System;
using System.Collections;
using System.Windows.Forms;
using Xceed.Ftp;

namespace ClientFtp
{
	public class RemoteFolderTreeNode : TreeNode
	{
		private RemoteFolderTreeNode( string parentFullPath, string name, AsyncFtpClient client )
      : base( name )
		{
      if( parentFullPath == null )
        throw new ArgumentNullException( "parentFullPath" );

      if( name == null )
        throw new ArgumentNullException( "name" );

      if( client == null )
        throw new ArgumentNullException( "client" );

      m_parentFullPath = parentFullPath;
      m_client = client;

      // Make sure a [+] appears, using a dummy node.
      this.Nodes.Add( new TreeNode( "retrieving folder contents..." ) );
    }

    public RemoteFolderTreeNode( AsyncFtpClient client )
      : base( "root" )
    {
      if( client == null )
        throw new ArgumentNullException( "client" );

      m_client = client;

      // Make sure a [+] appears, using a dummy node.
      this.Nodes.Add( new TreeNode( "retrieving folder contents..." ) );
    }

    public void Select( ListView contents, bool refresh )
    {
      if( contents == null )
        throw new ArgumentNullException( "contents" );

      contents.Enabled = false;
      this.TreeView.Enabled = false;

      if( refresh || ( m_items == null ) )
      {
        this.Refresh( contents );
      }
      else
      {
        // We fill the list and make sure the current folder is us.
        contents.Items.Clear();

        foreach( FtpItemInfo item in m_items )
        {
          contents.Items.Add( new RemoteListViewItem( item ) );
        }

        try
        {
          m_client.BeginChangeCurrentFolder( m_fullPath, new AsyncCallback( SelectCompleted ), contents );
        }
        catch( Exception except )
        {
          //# TODO
          MessageBox.Show( except.Message, "Error" );

          contents.Enabled = true;
          this.TreeView.Enabled = true;
        }
      }
    }

    private void SelectCompleted( IAsyncResult result )
    {
      ListView contents = result.AsyncState as ListView;

      try
      {
        m_client.EndChangeCurrentFolder( result );
      }
      catch( Exception except )
      {
        //# TODO
        MessageBox.Show( except.Message, "Error" );
      }
      finally
      {
        contents.Enabled = true;
        this.TreeView.Enabled = true;
      }
    }

    public void Refresh()
    {
      this.Refresh( null );
    }

    private void Refresh( ListView contents )
    {
      // contents may be null.
      m_contents = contents;

      // Since we are modifying our parent treeview in an async fashion, 
      // we disable it until completed to avoid user iteraction.
      this.TreeView.Enabled = false;

      // This will update our internal list which we use in Select.
      m_items = null;

      try
      {
        // We must always return to the current folder, except if we have
        // a listview to fill, which means we also get selected. If we don't
        // know our parent's fullpath, it means we are the root folder and
        // the current folder is our folder!
        if(  ( m_contents == null ) 
          || ( m_fullPath == null && m_parentFullPath == null ) )
        {
          m_client.BeginGetCurrentFolder( 
            new AsyncCallback( this.GetFolderCompleted ), 
            null );
        }
        else
        {
          // The first time we get refreshed, we must determine our full path.
          if( m_fullPath == null )
          {
            // We must go into our parent folder, then change into ourself.
            m_client.BeginChangeCurrentFolder( 
              m_parentFullPath, 
              new AsyncCallback( this.ChangeParentFolderCompleted ), 
              null );
          }
          else
          {
            // Go directly in our folder.
            m_client.BeginChangeCurrentFolder( 
              m_fullPath, 
              new AsyncCallback( this.ChangeFolderCompleted ), 
              null );
          }
        }
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );

        // Remove dummy child node.
        this.Nodes.Clear();

        // Enable controls
        this.TreeView.Enabled = true;

        if( m_contents != null )
        {
          m_contents.Items.Clear();
          m_contents.Enabled = true;
          m_contents = null;
        }
      }
    }

    private void GetFolderCompleted( IAsyncResult result )
    {
      try
      {
        string currentFolder = m_client.EndGetCurrentFolder( result );

        if( m_fullPath == null && m_parentFullPath == null )
        {
          // We are the root node and it's the first time we get selected.
          // The current folder is us!
          m_fullPath = currentFolder;

          // Update the root label.
          this.Text = m_fullPath;
        }

        if( m_fullPath == null )
        {
          if( currentFolder == m_parentFullPath )
          {
            // We can change into ourself right away!
            m_client.BeginChangeCurrentFolder( 
              this.Text, 
              new AsyncCallback( this.ChangeFolderCompleted ), 
              currentFolder );
          }
          else
          {
            // We must go into our parent folder, then change into ourself.
            m_client.BeginChangeCurrentFolder( 
              m_parentFullPath, 
              new AsyncCallback( this.ChangeParentFolderCompleted ), 
              currentFolder );
          }
        }
        else if( m_fullPath == currentFolder )
        {
          // We can get the contents right away, and we don't need to
          // pass currentFolder as the state, since we will end up in the
          // same folder.
          m_client.BeginGetFolderContents(
            new AsyncCallback( this.GetContentsCompleted ),
            null );
        }
        else
        {
          // Go directly in our folder.
          m_client.BeginChangeCurrentFolder( 
            m_fullPath, 
            new AsyncCallback( this.ChangeFolderCompleted ), 
            currentFolder );
        }
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );

        // Remove dummy child node.
        this.Nodes.Clear();

        // Enable treeview. If an error occurs here, we are still in the original folder.
        this.TreeView.Enabled = true;

        if( m_contents != null )
        {
          m_contents.Items.Clear();
          m_contents.Enabled = true;
          m_contents = null;
        }
      }
    }

    private void ChangeParentFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_client.EndChangeCurrentFolder( result );

        m_client.BeginChangeCurrentFolder( 
          this.Text, 
          new AsyncCallback( this.ChangeFolderCompleted ), 
          result.AsyncState );
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );

        // Remove dummy child node.
        this.Nodes.Clear();

        // Go back into original folder
        string currentFolder = result.AsyncState as string;

        if( currentFolder == null )
        {
          // Enable treeview
          this.TreeView.Enabled = true;
        }
        else
        {
          try
          {
            m_client.BeginChangeCurrentFolder( currentFolder, new AsyncCallback( this.ChangeBackCurrentFolderCompleted ), null );
          }
          catch
          {
            this.TreeView.Enabled = true;
          }
        }

        if( m_contents != null )
        {
          m_contents.Items.Clear();
          m_contents.Enabled = true;
          m_contents = null;
        }
      }
    }

    private void ChangeFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_client.EndChangeCurrentFolder( result );

        if( m_fullPath == null )
        {
          // We update our own full path the first time we get into it.
          m_client.BeginGetCurrentFolder( 
            new AsyncCallback( this.GetFullPathCompleted ), 
            result.AsyncState );
        }
        else
        {
          m_client.BeginGetFolderContents(
            new AsyncCallback( this.GetContentsCompleted ),
            result.AsyncState );
        }
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );

        // Remove dummy child node.
        this.Nodes.Clear();

        // Go back into original folder
        string currentFolder = result.AsyncState as string;

        if( currentFolder == null )
        {
          // Enable treeview
          this.TreeView.Enabled = true;
        }
        else
        {
          try
          {
            m_client.BeginChangeCurrentFolder( currentFolder, new AsyncCallback( this.ChangeBackCurrentFolderCompleted ), null );
          }
          catch
          {
            this.TreeView.Enabled = true;
          }
        }

        if( m_contents != null )
        {
          m_contents.Items.Clear();
          m_contents.Enabled = true;
          m_contents = null;
        }
      }
    }

    private void GetFullPathCompleted( IAsyncResult result )
    {
      try
      {
        m_fullPath = m_client.EndGetCurrentFolder( result );

        m_client.BeginGetFolderContents(
          new AsyncCallback( this.GetContentsCompleted ),
          result.AsyncState );
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );

        // Remove dummy child node.
        this.Nodes.Clear();

        // Go back into original folder
        string currentFolder = result.AsyncState as string;

        if( currentFolder == null )
        {
          // Enable treeview
          this.TreeView.Enabled = true;
        }
        else
        {
          try
          {
            m_client.BeginChangeCurrentFolder( currentFolder, new AsyncCallback( this.ChangeBackCurrentFolderCompleted ), null );
          }
          catch
          {
            this.TreeView.Enabled = true;
          }
        }

        if( m_contents != null )
        {
          m_contents.Items.Clear();
          m_contents.Enabled = true;
          m_contents = null;
        }
      }
    }

    private void GetContentsCompleted( IAsyncResult result )
    {
      string currentFolder = result.AsyncState as string;

      try
      {
        try
        {
          m_items = m_client.EndGetFolderContents( result );
        }
        finally
        {
          // Remove dummy child node.
          this.Nodes.Clear();

          if( m_items != null )
          {
            foreach( FtpItemInfo item in m_items )
            {
              if( ( item.Type == FtpItemType.Folder ) || ( item.Type == FtpItemType.Link ) )
              {
                this.Nodes.Add( new RemoteFolderTreeNode( m_fullPath, item.Name, m_client ) );
              }
            }

            if( m_contents != null )
            {
              this.Select( m_contents, false );

              // Since this changes the current folder to m_fullPath, we
              // don't have to return to "currentFolder". It will also
              // enable back the treeview.
              currentFolder = null;

              m_contents = null;
            }
            else
            {
              // We must enable back the treeview.
              this.TreeView.Enabled = true;
            }
          }
          else
          {
            if( m_contents != null )
            {
              // We can't call Select, since we would loop infinitely.
              m_contents.Items.Clear();
              m_contents.Enabled = true;
              m_contents = null;
            }

            if( currentFolder == null )
            {
              // We must enable back the treeview.
              this.TreeView.Enabled = true;
            }
          }
        }
      }
      catch( Exception except )
      {
        MessageBox.Show( 
          "An error occured while updating the contents of " + this.Text + ".\n\n\n" + except.Message, 
          "Error", 
          MessageBoxButtons.OK, 
          MessageBoxIcon.Error );
      }
      finally
      {
        if( currentFolder != null )
        {
          try
          {
            m_client.BeginChangeCurrentFolder( currentFolder, new AsyncCallback( this.ChangeBackCurrentFolderCompleted ), null );
          }
          catch
          {
            this.TreeView.Enabled = true;
          }
        }
      }
    }

    private void ChangeBackCurrentFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_client.EndChangeCurrentFolder( result );
      }
      catch { }
      finally
      {
        this.TreeView.Enabled = true;
      }
    }

    public static string FormatSize( long size )
    {
      // Format the received size to a readable format.

      string formattedSize = String.Empty;

      // Formats the size in bytes.
      if( size == 0 )
      {
        formattedSize = size.ToString( "n0" ) + " byte";
      }
      else
      {
        formattedSize = size.ToString( "n0" ) + " bytes";
      }

      // Format the size in kilobytes. (only if the size is at least 1 kb)
      if( size >= 1024 )
      {
        size = size / 1024;

        formattedSize = size.ToString( "n0" ) + " KB";
      }

      return formattedSize;
    }

    private AsyncFtpClient m_client = null;
    private string m_parentFullPath = null;
    private string m_fullPath = null;

    private FtpItemInfoList m_items = null;

    private ListView m_contents = null;
	}
}
