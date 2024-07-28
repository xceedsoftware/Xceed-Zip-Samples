using System;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace ClientFtp
{
	public class LocalListViewItem : ListViewItem
	{
		public LocalListViewItem( FileSystemItem item )
      : base( item.Name )
		{
      if( item == null )
        throw new ArgumentNullException( "item" );

      AbstractFile file = item as AbstractFile;

      // We only show the size for a File.
      if( file != null )
      {
        this.SubItems.Add( RemoteFolderTreeNode.FormatSize( file.Size ) );
        this.ImageIndex = ( int ) FtpItemIconEnum.File;
      }
      else
      {
        this.SubItems.Add( "" );
        this.ImageIndex = ( int ) FtpItemIconEnum.ClosedFolder;
      }

      this.SubItems.Add( item.LastWriteDateTime.ToString() );

      m_item = item;
    }

    public FileSystemItem Item
    {
      get { return m_item; }
    }

    private FileSystemItem m_item = null;
	}
}
