/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AbstractPathListView.cs]
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

namespace SynchronizeUISample.CustomListViewItem
{
  /// <summary>
  /// Base class for FolderListViewItem and FileListViewItem
  /// </summary>
	public abstract class AbstractPathListViewItem : ListViewItem
	{
    #region PUBLIC CONTRUCTORS

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="path">The full path of the item</param>
    /// <param name="imageIndex">The index of the image to use for this ListViewItem</param>
    public AbstractPathListViewItem( string path, int imageIndex )
      : base()
    {
      this.m_fullPath = path;
      this.ImageIndex = imageIndex;
    } 

    #endregion

    #region PUBLIC OVERRIDES

    /// <summary>
    /// Display the full path of the item
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return m_fullPath;
    } 

    #endregion

    #region PROTECTED FIELDS

    protected string m_fullPath = string.Empty; 

    #endregion
	}
}
