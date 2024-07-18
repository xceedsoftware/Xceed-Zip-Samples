/* Xceed Zip for .NET - ZipManager Sample Application
 * Copyright (c) 2000-2002 - Xceed Software Inc.
 *
 * [ZipContentsItem.cs]
 * 
 * This application demonstrates how to use Xceed Zip for .NET.
 *
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Xceed.Zip.Samples.ZipManager
{
  public class ZipContentsItem: System.Windows.Forms.ListViewItem
  {

    public ZipContentsItem( ZippedFile File ) : base( File.FullName )
    {
      SubItems.Add(File.LastWriteDateTime.ToString());
      SubItems.Add(File.Size.ToString());
      SubItems.Add(File.CompressedSize.ToString());
      if( File.Size != 0 )
      {
        int ratio = 100 - ( int )Math.Round( ( double )File.CompressedSize / ( double )File.Size * 100 );
        SubItems.Add( ratio.ToString() + "%" );
      }
      else
      {
        SubItems.Add( "0%" );
      }
      SubItems.Add(File.Attributes.ToString());

      if( File.Encrypted )
      {
        ForeColor = Color.Red;
      }

      m_file = File;
    }

    public ZippedFile File
    {
      get{ return m_file; }
    }

    private ZippedFile m_file;
  }
}