'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [CustomListView.vb]
 '*
 '* Custom ListView that handles sorting and icon updating.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Xceed.FileSystem.Samples.Utils
Imports Xceed.FileSystem.Samples.Utils.API
Imports Xceed.FileSystem.Samples.Utils.Icons
Imports Xceed.FileSystem.Samples.Utils.TreeView

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class CustomListView : Inherits System.Windows.Forms.ListView
    #Region "CONSTRUCTORS"

    Public Sub New()
      m_iconUpdater = New ListViewIconUpdater(Me)
    End Sub

    Protected Overrides Sub Finalize()
      m_iconUpdater.StopUpdate()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Refresh the icons in the ListView.
    ''' </summary>
    Public Sub RefreshIcons()
      ' To avoid multiple refresh process running at the same time, we stop
      ' any current refresh process and start a new one.
      m_iconUpdater.StopUpdate()
      m_iconUpdater.StartUpdate()
    End Sub

    ''' <summary>
    ''' Remove the current ListView's sorting.
    ''' </summary>
    Public Sub RemoveSort()
      ' When the ListView is being filled, we don't want the list to be sorted each time
      ' a new item is added to the list.
      If m_sortComparer.ColumnIndex > -1 Then
        Me.SetHeaderImage(Me.Columns(m_sortComparer.ColumnIndex), SortOrder.None, m_sortComparer.ColumnIndex)
        Me.SetSelectedColumn(-1)
      End If

      Me.ListViewItemSorter = Nothing

      m_sortComparer = New ListViewSortComparer(-1)
    End Sub

    ''' <summary>
    ''' Sort the LitView on the specified column.
    ''' </summary>
    ''' <param name="columnIndex"></param>
    Public Overloads Sub Sort(ByVal columnIndex As Integer)
      ' Sort the ListView on the specified column index. This will add the sort arrow
      ' on the column header and set teh background color of the column to lightgray.

      Me.SuspendSort()

      Dim currentIndex As Integer = m_sortComparer.ColumnIndex

      ' If we are sorting the same column, we will just update the sorting order.
      If m_sortComparer.ColumnIndex = columnIndex Then
        Dim sortOrder As SortOrder = SortOrder.Ascending

        If m_sortComparer.SortOrder = SortOrder.Ascending Then
          sortOrder = SortOrder.Descending
        End If

        m_sortComparer.SortOrder = sortOrder
      Else
        m_sortComparer.ColumnIndex = columnIndex
        m_sortComparer.SortOrder = SortOrder.Ascending
      End If

      ' Update the column icon.
      If currentIndex = columnIndex Then
        ' Same column, just update the arrow.
        Me.SetHeaderImage(Me.Columns(columnIndex), m_sortComparer.SortOrder, columnIndex)
      Else
        ' New column, remove sorting tips from the current column and update the new one.
        If currentIndex > -1 Then
          Me.SetHeaderImage(Me.Columns(currentIndex), SortOrder.None, currentIndex)
        End If

        Me.SetHeaderImage(Me.Columns(columnIndex), m_sortComparer.SortOrder, columnIndex)
        Me.SetSelectedColumn(columnIndex)
      End If

      Me.ResumeSort()
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "OVERRIDES"

    Protected Overrides Sub OnColumnClick(ByVal e As ColumnClickEventArgs)
      ' Sort the list based on the clicked column.

      MyBase.OnColumnClick(e)

      ' Sort the column.
      Me.Sort(e.Column)
    End Sub

    #End Region ' OVERRIDES

    #Region "PRIVATE METHODS"

    Private Sub SuspendSort()
      Me.ListViewItemSorter = Nothing
    End Sub

    Private Sub ResumeSort()
      Me.ListViewItemSorter = m_sortComparer
    End Sub

    Private Sub SetHeaderImage(ByVal ch As ColumnHeader, ByVal sortOrder As SortOrder, ByVal columnIndex As Integer)
      ' We use Win32 API calls to draw an arrow icon on a column header. 
      ' This happen when a column is sorted.

      Try
        If columnIndex < 0 OrElse columnIndex >= Me.Columns.Count Then
          Return
        End If

        Dim headerPtr As IntPtr = Win32.SendMessage(Me.Handle, Win32.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)

        Dim headerColumn As ColumnHeader = Me.Columns(columnIndex)

        Dim hd As Win32.HDITEM = New Win32.HDITEM
        hd.mask = Win32.HDI_IMAGE Or Win32.HDI_FORMAT

        Dim align As HorizontalAlignment = headerColumn.TextAlign

        If align = HorizontalAlignment.Left Then
          hd.fmt = Win32.HDF_LEFT Or Win32.HDF_STRING Or Win32.HDF_BITMAP_ON_RIGHT
        ElseIf align = HorizontalAlignment.Center Then
          hd.fmt = Win32.HDF_CENTER Or Win32.HDF_STRING Or Win32.HDF_BITMAP_ON_RIGHT
        Else ' HorizontalAlignment.Right
          hd.fmt = Win32.HDF_RIGHT Or Win32.HDF_STRING Or Win32.HDF_BITMAP_ON_RIGHT
        End If

        If sortOrder <> sortOrder.None Then
          hd.fmt = hd.fmt Or Win32.HDF_IMAGE
        End If

        If sortOrder = sortOrder.None Then
          hd.iImage = -1
        Else
          hd.iImage = IconCache.GetSortIconIndex((sortOrder = sortOrder.Ascending))
        End If

        Win32.SendMessage(headerPtr, Win32.HDM_SETITEM, New IntPtr(columnIndex), hd)
      Catch
      End Try
    End Sub

    Private Sub SetSelectedColumn(ByVal columnIndex As Integer)
      ' We use Win32 API calls to set the background color of the selected column to lightgray.

      Try
        Win32.SendMessage(Me.Handle, Win32.LVM_SETSELECTEDCOLUMN, New IntPtr(columnIndex), New IntPtr(0))
      Catch
      End Try
    End Sub

#End Region     ' PRIVATE METHODS

    #Region "PRIVATE FIELDS"

    Private m_sortComparer As ListViewSortComparer = New ListViewSortComparer(-1)
    Private m_iconUpdater As ListViewIconUpdater '= null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
