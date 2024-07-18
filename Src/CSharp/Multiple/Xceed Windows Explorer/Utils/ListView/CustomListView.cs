/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [CustomListView.cs]
 * 
 * Custom ListView that handles sorting and icon updating.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;
using Xceed.FileSystem.Samples.Utils;
using Xceed.FileSystem.Samples.Utils.API;
using Xceed.FileSystem.Samples.Utils.Icons;
using Xceed.FileSystem.Samples.Utils.TreeView;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
  public class CustomListView : System.Windows.Forms.ListView
  {
    #region CONSTRUCTORS

    public CustomListView()
    {
      m_iconUpdater = new ListViewIconUpdater( this );
    }

    ~CustomListView()
    {
      m_iconUpdater.StopUpdate();
    }

    #endregion CONSTRUCTORS

    #region PUBLIC METHODS

    /// <summary>
    /// Refresh the icons in the ListView.
    /// </summary>
    public void RefreshIcons()
    {
      // To avoid multiple refresh process running at the same time, we stop
      // any current refresh process and start a new one.
      m_iconUpdater.StopUpdate();
      m_iconUpdater.StartUpdate();
    }

    /// <summary>
    /// Remove the current ListView's sorting.
    /// </summary>
    public void RemoveSort()
    {
      // When the ListView is being filled, we don't want the list to be sorted each time
      // a new item is added to the list.
      if( m_sortComparer.ColumnIndex > -1 )
      {
        this.SetHeaderImage( this.Columns[ m_sortComparer.ColumnIndex ], SortOrder.None, m_sortComparer.ColumnIndex );
        this.SetSelectedColumn( -1 );
      }

      this.ListViewItemSorter = null;
      
      m_sortComparer = new ListViewSortComparer( -1 );
    }

    /// <summary>
    /// Sort the LitView on the specified column.
    /// </summary>
    /// <param name="columnIndex"></param>
    public void Sort( int columnIndex )
    {
      // Sort the ListView on the specified column index. This will add the sort arrow
      // on the column header and set teh background color of the column to lightgray.

      this.SuspendSort();

      int currentIndex = m_sortComparer.ColumnIndex;

      // If we are sorting the same column, we will just update the sorting order.
      if( m_sortComparer.ColumnIndex == columnIndex )
      {
        SortOrder sortOrder = SortOrder.Ascending;

        if( m_sortComparer.SortOrder == SortOrder.Ascending )
          sortOrder = SortOrder.Descending;

        m_sortComparer.SortOrder = sortOrder;
      }
      else
      {
        m_sortComparer.ColumnIndex = columnIndex;
        m_sortComparer.SortOrder = SortOrder.Ascending;
      }

      // Update the column icon.
      if( currentIndex == columnIndex )
      {
        // Same column, just update the arrow.
        this.SetHeaderImage( this.Columns[ columnIndex ], m_sortComparer.SortOrder, columnIndex );
      }
      else
      {
        // New column, remove sorting tips from the current column and update the new one.
        if( currentIndex > -1 )
          this.SetHeaderImage( this.Columns[ currentIndex ], SortOrder.None, currentIndex );

        this.SetHeaderImage( this.Columns[ columnIndex ], m_sortComparer.SortOrder, columnIndex );
        this.SetSelectedColumn( columnIndex );
      }

      this.ResumeSort();
    }

    #endregion PUBLIC METHODS

    #region OVERRIDES

    protected override void OnColumnClick( ColumnClickEventArgs e )
    {
      // Sort the list based on the clicked column.

      base.OnColumnClick(e);

      // Sort the column.
      this.Sort( e.Column );
    }

    #endregion OVERRIDES

    #region PRIVATE METHODS

    private void SuspendSort()
    {
      this.ListViewItemSorter = null;
    }

    private void ResumeSort()
    {
      this.ListViewItemSorter = m_sortComparer;
    }

    unsafe private void SetHeaderImage( ColumnHeader ch, SortOrder sortOrder, int columnIndex )
    {
      // We use Win32 API calls to draw an arrow icon on a column header. 
      // This happen when a column is sorted.

      try
      {
        if( columnIndex < 0 || columnIndex >= this.Columns.Count) 
          return; 

        IntPtr headerPtr = Win32.SendMessage( this.Handle, Win32.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero ); 

        ColumnHeader headerColumn = this.Columns[ columnIndex ]; 

        Win32.HDITEM hd = new Win32.HDITEM(); 
        hd.mask = Win32.HDI_IMAGE | Win32.HDI_FORMAT; 

        HorizontalAlignment align = headerColumn.TextAlign; 

        if( align == HorizontalAlignment.Left ) 
        {
          hd.fmt = Win32.HDF_LEFT | Win32.HDF_STRING | Win32.HDF_BITMAP_ON_RIGHT; 
        }
        else if( align == HorizontalAlignment.Center ) 
        {
          hd.fmt = Win32.HDF_CENTER | Win32.HDF_STRING | Win32.HDF_BITMAP_ON_RIGHT; 
        }
        else    // HorizontalAlignment.Right 
        {
          hd.fmt = Win32.HDF_RIGHT | Win32.HDF_STRING | Win32.HDF_BITMAP_ON_RIGHT; 
        }

        if( sortOrder != SortOrder.None ) 
          hd.fmt |= Win32.HDF_IMAGE; 

        if( sortOrder == SortOrder.None )
        {
          hd.iImage = -1;
        }
        else
        {
          hd.iImage = IconCache.GetSortIconIndex( ( sortOrder == SortOrder.Ascending ) );
        }

        Win32.SendMessage( headerPtr, Win32.HDM_SETITEM, new IntPtr( columnIndex ), ref hd ); 
      }
      catch{}
    }

    private void SetSelectedColumn( int columnIndex )
    {
      // We use Win32 API calls to set the background color of the selected column to lightgray.

      try
      {
        Win32.SendMessage( this.Handle, Win32.LVM_SETSELECTEDCOLUMN, new IntPtr( columnIndex ), new IntPtr(0) );
      }
      catch{}
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private ListViewSortComparer m_sortComparer = new ListViewSortComparer( -1 );
    private ListViewIconUpdater m_iconUpdater; //= null

    #endregion PRIVATE FIELDS
  }
}
