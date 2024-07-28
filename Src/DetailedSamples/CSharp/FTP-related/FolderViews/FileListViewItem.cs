using System;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace FolderViews
{
	public class FileListViewItem : ListViewItem
	{
		public FileListViewItem( AbstractFile file )
		{
      this.Text = file.Name;

      if( file.Size == -1 )
      {
        this.SubItems.Add( "N/A" );
      }
      else
      {
        double kiloBytes = System.Math.Ceiling( ( double )file.Size / ( double )1024 );
        this.SubItems.Add( kiloBytes.ToString( "N0" ) + " kb" );
      }

      if( file.LastWriteDateTime == DateTime.MinValue )
      {
        this.SubItems.Add( "N/A" );
      }
      else
      {
        this.SubItems.Add( file.LastWriteDateTime.ToString() );
      }

      m_file = file;
		}

    public AbstractFile File
    {
      get { return m_file; }
    }

    private AbstractFile m_file; // = null
	}
}
