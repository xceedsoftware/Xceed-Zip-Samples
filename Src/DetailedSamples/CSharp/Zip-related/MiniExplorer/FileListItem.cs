/*
 * Xceed Zip for .NET - MiniExplorer Sample Application
 * Copyright (c) 2000-2003 - Xceed Software Inc.
 * 
 * [FileListItem.cs]
 * 
 * This application demonstrates how to use the Xceed FileSystem object model
 * in a generic way.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;
using System.Drawing;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.MiniExplorer
{
	/// <summary>
	/// Summary description for FileListItem.
	/// </summary>
	internal class FileListItem : ListViewItem 
	{
		public FileListItem( AbstractFile file )
      : base( file.Name, 0 )
		{
      try { SubItems.Add( file.Size.ToString() ); }
      catch { SubItems.Add( "NA" ); }

      try { SubItems.Add( file.Attributes.ToString() ); }
      catch { SubItems.Add( "NA" ); }

      try { SubItems.Add( file.LastWriteDateTime.ToString() ); }
      catch { SubItems.Add( "NA" ); }

      try 
      { 
        if( file.LastAccessDateTime == DateTime.MinValue )
          SubItems.Add( "NA" );
        else
          SubItems.Add( file.LastAccessDateTime.ToString() ); 
      }
      catch { SubItems.Add( "NA" ); }

      try 
      { 
        if( file.CreationDateTime == DateTime.MinValue )
          SubItems.Add( "NA" );
        else
          SubItems.Add( file.CreationDateTime.ToString() ); 
      }
      catch { SubItems.Add( "NA" ); }

      this.UseItemStyleForSubItems = false;

      Color alternate = this.BackColor;

      alternate = ( alternate.GetBrightness() > 0.5 ) 
        ? Color.FromArgb( alternate.R * 9 / 10, alternate.G * 19 / 20, alternate.B * 9 / 10 )
        : Color.FromArgb( alternate.R * 20 / 19, alternate.G * 10 / 9, alternate.B * 20 / 19 );

      this.SubItems[ 1 ].BackColor = 
      this.SubItems[ 3 ].BackColor = 
      this.SubItems[ 5 ].BackColor = alternate;
      
      if( ( file is ZippedFile ) && ( ( ZippedFile )file ).Encrypted )
      {
        base.ForeColor = Color.Red;
      }

      m_file = file;
		}

    public FileListItem( string fileName )
      : base( fileName, 0 )
    {
    }

    public AbstractFile File
    {
      get { return m_file; }
    }

    private AbstractFile m_file = null;
	}
}
