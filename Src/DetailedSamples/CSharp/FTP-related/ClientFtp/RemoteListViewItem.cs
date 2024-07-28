using System;
using System.Windows.Forms;
using Xceed.Ftp;

namespace ClientFtp
{
	public class RemoteListViewItem : ListViewItem
	{
		public RemoteListViewItem( FtpItemInfo info )
      : base( info.Name )
		{
      if( info == null )
        throw new ArgumentNullException( "info" );

      m_info = info;

      // We only show the size for a File or unknown.
      if( ( info.Type == FtpItemType.File ) || ( info.Type == FtpItemType.Unknown ) )
      {
        this.SubItems.Add( RemoteFolderTreeNode.FormatSize( info.Size ) );
      }
      else
      {
        this.SubItems.Add( "" );
      }

      this.SubItems.Add( info.DateTime.ToString() );

      switch( info.Type )
      {
        case FtpItemType.File:
        case FtpItemType.Unknown:
          this.ImageIndex = ( int ) FtpItemIconEnum.File;
          break;

        case FtpItemType.Folder:
          this.ImageIndex = ( int ) FtpItemIconEnum.ClosedFolder;
          break;

        case FtpItemType.Link:
          this.ImageIndex = ( int ) FtpItemIconEnum.ClosedLink;
          break;
      }
    }

    public FtpItemInfo Info
    {
      get { return m_info; }
    }

    private FtpItemInfo m_info = FtpItemInfo.Empty;
	}
}
