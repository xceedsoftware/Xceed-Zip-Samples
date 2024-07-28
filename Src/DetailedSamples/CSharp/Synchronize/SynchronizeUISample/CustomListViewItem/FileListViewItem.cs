/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FileListViewItem.cs]
 * 
 * This application demonstrate how to use the Xceed Synchronize
 * functionnality.
 * 
 * This file is part of Xceed FileSystem for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SynchronizeUISample.CustomListViewItem
{
  /// <summary>
  /// Defines a ListViewItem to display informations about a file
  /// </summary>
  public class FileListViewItem : AbstractPathListViewItem
  {
    #region PUBLIC CONSTRUCTORS

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="filePath">The full path of the file</param>
    public FileListViewItem( string filePath)
      : base( filePath, 1)
    {
      this.Text = Path.GetFileName( this.m_fullPath );
      this.SubItems.Add( this.m_fullPath );
    } 

    #endregion
  }
}
