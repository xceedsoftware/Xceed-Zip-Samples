/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [ListViewSortComparer.cs]
 * 
 * Custom SortComparer that mimic the MS Windows Explorer sorting.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.FileSystem.Samples.Utils.TreeView;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
	public class ListViewSortComparer : IComparer
	{
    #region CONSTRUCTORS

		public ListViewSortComparer( int columnIndex )
      : this( columnIndex, SortOrder.None )
    {
    }

    public ListViewSortComparer( int columnIndex, SortOrder sortOrder )
    {
      m_columnIndex = columnIndex;
      m_sortOrder = sortOrder;
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    /// <summary>
    /// Gets or sets the column index on which to sort the list.
    /// </summary>
    public int ColumnIndex
    {
      get{ return m_columnIndex; }
      set{ m_columnIndex = value; }
    }
    
    /// <summary>
    /// Gets or sets the sort order.
    /// </summary>
    public SortOrder SortOrder
    {
      get{ return m_sortOrder; }
      set{ m_sortOrder = value; }
    }

    #endregion PUBLIC PROPERTIES

    #region IComparer Members

    public int Compare(object x, object y)
    {
      // The objects return here contains a FileSystemItem which will be used to compare.
      AbstractListViewItem xItem = x as AbstractListViewItem;
      AbstractListViewItem yItem = y as AbstractListViewItem;

      switch( m_columnIndex )
      {
        case 0: // Name
          return this.CompareName( xItem, yItem );

        case 1: // Size
          return this.CompareSize( xItem, yItem );

        case 2: // Compressed Size
          return this.CompareCompressedSize( xItem, yItem );

        case 3: // Type
          return this.CompareType( xItem, yItem );

        case 4: // Modification date
          return this.CompareModificationDate( xItem, yItem );          

        default:
          return 0;
      }
    }

    #endregion IComparer Members

    #region PRIVATE METHODS

    /// <summary>
    /// Compare the compressed size of 2 AbstractListViewItem.
    /// </summary>
    /// <param name="xItem"></param>
    /// <param name="yItem"></param>
    /// <returns>Returns -1 if xItem is less than yItem, 1 if xItem is
    /// greater than yItem and 0 if they are equal. Folders are compared 
    /// using the display text. The values will be reverted if the sort order
    /// is descending.</returns>
    private int CompareCompressedSize( AbstractListViewItem xItem, AbstractListViewItem yItem )
    {
      // Folders will have a size of -1.
      long xSize = -1;
      long ySize = -1;

      // Folder should be sorted by name.
      AbstractFolder xFolder = xItem.FileSystemItem as ZippedFolder;
      AbstractFolder yFolder = yItem.FileSystemItem as ZippedFolder;

      if( xFolder != null && yFolder != null )
      {
        return this.GetCompareResult( string.Compare( xItem.Text, yItem.Text ) );
      }

      AbstractFile xFile = xItem.FileSystemItem as ZippedFile;
      AbstractFile yFile = yItem.FileSystemItem as ZippedFile;

      if( xFile != null )
        xSize = ( ( ZippedFile )xFile ).CompressedSize;

      if( yFile != null )
        ySize = ( ( ZippedFile )yFile ).CompressedSize;

      if( xSize > ySize )
      {
        return this.GetCompareResult( 1 );
      }
      else if( xSize < ySize )
      {
        return this.GetCompareResult( -1 );
      }
      else // Equal size, we sort by name.
      {
        return this.GetCompareResult( string.Compare( yItem.Text, xItem.Text ) );
      }
    }

    private int CompareModificationDate( AbstractListViewItem xItem, AbstractListViewItem yItem )
    {
      // If x is a folder and y is not, x shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder != null ) 
        && ( yItem.FileSystemItem as AbstractFolder == null ) )
      {
        return this.GetCompareResult( -1 );
      }

      // If y is a folder and x is not, y shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder == null ) 
        && ( yItem.FileSystemItem as AbstractFolder != null ) )
      {
        return this.GetCompareResult( 1 );
      }

      DateTime xDateTime = DateTime.MinValue;
      DateTime yDateTime = DateTime.MinValue;

      if( xItem.FileSystemItem.HasLastWriteDateTime )
        xDateTime = xItem.FileSystemItem.LastWriteDateTime;

      if( yItem.FileSystemItem.HasLastWriteDateTime )
        yDateTime = yItem.FileSystemItem.LastWriteDateTime;

      if( xDateTime > yDateTime )
      {
        return this.GetCompareResult( 1 );
      }
      else if( xDateTime < yDateTime )
      {
        return this.GetCompareResult( -1 );
      }
      else // Equal size, we sort by name.
      {
        return this.GetCompareResult( string.Compare( yItem.Text, xItem.Text ) );
      }
    }

    private int CompareName( AbstractListViewItem xItem, AbstractListViewItem yItem )
    {
      // If x is a folder and y is not, x shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder != null ) 
        && ( yItem.FileSystemItem as AbstractFolder == null ) )
      {
        return this.GetCompareResult( -1 );
      }

      // If y is a folder and x is not, y shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder == null ) 
        && ( yItem.FileSystemItem as AbstractFolder != null ) )
      {
        return this.GetCompareResult( 1 );
      }

      // We are dealing with the same type of item, we just compare the 2 strings.
      return this.GetCompareResult( string.Compare( xItem.Text, yItem.Text ) );
    }

    private int CompareSize( AbstractListViewItem xItem, AbstractListViewItem yItem )
    {
      // Folders will have a size of -1.
      long xSize = -1;
      long ySize = -1;

      // Folder should be sorted by name.
      AbstractFolder xFolder = xItem.FileSystemItem as AbstractFolder;
      AbstractFolder yFolder = yItem.FileSystemItem as AbstractFolder;

      if( xFolder != null && yFolder != null )
      {
        return this.GetCompareResult( string.Compare( xItem.Text, yItem.Text ) );
      }

      AbstractFile xFile = xItem.FileSystemItem as AbstractFile;
      AbstractFile yFile = yItem.FileSystemItem as AbstractFile;

      if( xFile != null )
        xSize = xFile.Size;

      if( yFile != null )
        ySize = yFile.Size;

      if( xSize > ySize )
      {
        return this.GetCompareResult( 1 );
      }
      else if( xSize < ySize )
      {
        return this.GetCompareResult( -1 );
      }
      else // Equal size, we sort by name.
      {
        return this.GetCompareResult( string.Compare( yItem.Text, xItem.Text ) );
      }
    }

    private int CompareType( AbstractListViewItem xItem, AbstractListViewItem yItem )
    {
      // If x is a folder and y is not, x shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder != null ) 
        && ( yItem.FileSystemItem as AbstractFolder == null ) )
      {
        return this.GetCompareResult( -1 );
      }

      // If y is a folder and x is not, y shoud come first.
      if(  ( xItem.FileSystemItem as AbstractFolder == null ) 
        && ( yItem.FileSystemItem as AbstractFolder != null ) )
      {
        return this.GetCompareResult( 1 );
      }

      string xType = xItem.FileSystemItem.GetType().ToString();
      string yType = yItem.FileSystemItem.GetType().ToString();

      if( xType != yType )
      {
        return this.GetCompareResult( string.Compare( yType, xType ) );
      }
      else
      {
        // Same type, we sort by name.
        return this.GetCompareResult( string.Compare( yItem.Text, xItem.Text ) );
      }
    }

    private int GetCompareResult( int value )
    {
      switch( m_sortOrder )
      {
        case SortOrder.Ascending:
          return value;

        case SortOrder.Descending:
          return -value;

        default:
          return 0;
      }
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private SortOrder m_sortOrder;
    private int m_columnIndex = -1;

    #endregion PRIVATE FIELDS
  }
}
