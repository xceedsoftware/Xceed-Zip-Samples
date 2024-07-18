'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [ListViewSortComparer.vb]
 '*
 '* Custom SortComparer that mimic the MS Windows Explorer sorting.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.FileSystem.Samples.Utils.TreeView
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class ListViewSortComparer : Implements IComparer
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal colIndex As Integer)
      m_columnIndex = colIndex
      m_sortOrder = SortOrder.None
    End Sub

    Public Sub New(ByVal colIndex As Integer, ByVal order As SortOrder)
      m_columnIndex = colIndex
      m_sortOrder = order
    End Sub

#End Region     ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    ''' <summary>
    ''' Gets or sets the column index on which to sort the list.
    ''' </summary>
    Public Property ColumnIndex() As Integer
      Get
        Return m_columnIndex
      End Get
      Set
        m_columnIndex = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sort order.
    ''' </summary>
    Public Property SortOrder() As SortOrder
      Get
        Return m_sortOrder
      End Get
      Set
        m_sortOrder = Value
      End Set
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "IComparer Members"

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
      ' The objects return here contains a FileSystemItem which will be used to compare.
      Dim xItem As AbstractListViewItem = CType(IIf(TypeOf x Is AbstractListViewItem, x, Nothing), AbstractListViewItem)
      Dim yItem As AbstractListViewItem = CType(IIf(TypeOf y Is AbstractListViewItem, y, Nothing), AbstractListViewItem)

      Select Case m_columnIndex
        Case 0 ' Name
          Return Me.CompareName(xItem, yItem)

        Case 1 ' Size
          Return Me.CompareSize(xItem, yItem)

        Case 2 ' Compressed Size
          Return Me.CompareCompressedSize(xItem, yItem)

        Case 3 ' Type
          Return Me.CompareType(xItem, yItem)

        Case 4 ' Modification date
          Return Me.CompareModificationDate(xItem, yItem)

        Case Else
          Return 0
      End Select
    End Function

    #End Region ' IComparer Members

    #Region "PRIVATE METHODS"

    ''' <summary>
    ''' Compare the compressed size of 2 AbstractListViewItem.
    ''' </summary>
    ''' <param name="xItem"></param>
    ''' <param name="yItem"></param>
    ''' <returns>Returns -1 if xItem is less than yItem, 1 if xItem is
    ''' greater than yItem and 0 if they are equal. Folders are compared 
    ''' using the display text. The values will be reverted if the sort order
    ''' is descending.</returns>
    Private Function CompareCompressedSize(ByVal xItem As AbstractListViewItem, ByVal yItem As AbstractListViewItem) As Integer
      ' Folders will have a size of -1.
      Dim xSize As Long = -1
      Dim ySize As Long = -1

      ' Folder should be sorted by name.
      Dim xFolder As AbstractFolder = CType(IIf(TypeOf xItem.FileSystemItem Is ZippedFolder, xItem.FileSystemItem, Nothing), ZippedFolder)
      Dim yFolder As AbstractFolder = CType(IIf(TypeOf yItem.FileSystemItem Is ZippedFolder, yItem.FileSystemItem, Nothing), ZippedFolder)

      If Not xFolder Is Nothing AndAlso Not yFolder Is Nothing Then
        Return Me.GetCompareResult(String.Compare(xItem.Text, yItem.Text))
      End If

      Dim xFile As AbstractFile = CType(IIf(TypeOf xItem.FileSystemItem Is ZippedFile, xItem.FileSystemItem, Nothing), ZippedFile)
      Dim yFile As AbstractFile = CType(IIf(TypeOf yItem.FileSystemItem Is ZippedFile, yItem.FileSystemItem, Nothing), ZippedFile)

      If Not xFile Is Nothing Then
        xSize = (CType(xFile, ZippedFile)).CompressedSize
      End If

      If Not yFile Is Nothing Then
        ySize = (CType(yFile, ZippedFile)).CompressedSize
      End If

      If xSize > ySize Then
        Return Me.GetCompareResult(1)
      Else If xSize < ySize Then
        Return Me.GetCompareResult(-1)
      Else ' Equal size, we sort by name.
        Return Me.GetCompareResult(String.Compare(yItem.Text, xItem.Text))
      End If
    End Function

    Private Function CompareModificationDate(ByVal xItem As AbstractListViewItem, ByVal yItem As AbstractListViewItem) As Integer
      ' If x is a folder and y is not, x shoud come first.
      If (Not CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(-1)
      End If

      ' If y is a folder and x is not, y shoud come first.
      If (CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (Not CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(1)
      End If

      Dim xDateTime As DateTime = DateTime.MinValue
      Dim yDateTime As DateTime = DateTime.MinValue

      If xItem.FileSystemItem.HasLastWriteDateTime Then
        xDateTime = xItem.FileSystemItem.LastWriteDateTime
      End If

      If yItem.FileSystemItem.HasLastWriteDateTime Then
        yDateTime = yItem.FileSystemItem.LastWriteDateTime
      End If

      If xDateTime > yDateTime Then
        Return Me.GetCompareResult(1)
      ElseIf xDateTime < yDateTime Then
        Return Me.GetCompareResult(-1)
      Else ' Equal size, we sort by name.
        Return Me.GetCompareResult(String.Compare(yItem.Text, xItem.Text))
      End If
    End Function

    Private Function CompareName(ByVal xItem As AbstractListViewItem, ByVal yItem As AbstractListViewItem) As Integer
      ' If x is a folder and y is not, x shoud come first.
      If (Not CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(-1)
      End If

      ' If y is a folder and x is not, y shoud come first.
      If (CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (Not CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(1)
      End If

      ' We are dealing with the same type of item, we just compare the 2 strings.
      Return Me.GetCompareResult(String.Compare(xItem.Text, yItem.Text))
    End Function

    Private Function CompareSize(ByVal xItem As AbstractListViewItem, ByVal yItem As AbstractListViewItem) As Integer
      ' Folders will have a size of -1.
      Dim xSize As Long = -1
      Dim ySize As Long = -1

      ' Folder should be sorted by name.
      Dim xFolder As AbstractFolder = CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder)
      Dim yFolder As AbstractFolder = CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder)

      If Not xFolder Is Nothing AndAlso Not yFolder Is Nothing Then
        Return Me.GetCompareResult(String.Compare(xItem.Text, yItem.Text))
      End If

      Dim xFile As AbstractFile = CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFile, xItem.FileSystemItem, Nothing), AbstractFile)
      Dim yFile As AbstractFile = CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFile, yItem.FileSystemItem, Nothing), AbstractFile)

      If Not xFile Is Nothing Then
        xSize = xFile.Size
      End If

      If Not yFile Is Nothing Then
        ySize = yFile.Size
      End If

      If xSize > ySize Then
        Return Me.GetCompareResult(1)
      Else If xSize < ySize Then
        Return Me.GetCompareResult(-1)
      Else ' Equal size, we sort by name.
        Return Me.GetCompareResult(String.Compare(yItem.Text, xItem.Text))
      End If
    End Function

    Private Function CompareType(ByVal xItem As AbstractListViewItem, ByVal yItem As AbstractListViewItem) As Integer
      ' If x is a folder and y is not, x shoud come first.
      If (Not CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(-1)
      End If

      ' If y is a folder and x is not, y shoud come first.
      If (CType(IIf(TypeOf xItem.FileSystemItem Is AbstractFolder, xItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) AndAlso (Not CType(IIf(TypeOf yItem.FileSystemItem Is AbstractFolder, yItem.FileSystemItem, Nothing), AbstractFolder) Is Nothing) Then
        Return Me.GetCompareResult(1)
      End If

      Dim xType As String = xItem.FileSystemItem.GetType().ToString()
      Dim yType As String = yItem.FileSystemItem.GetType().ToString()

      If xType <> yType Then
        Return Me.GetCompareResult(String.Compare(yType, xType))
      Else
        ' Same type, we sort by name.
        Return Me.GetCompareResult(String.Compare(yItem.Text, xItem.Text))
      End If
    End Function

    Private Function GetCompareResult(ByVal value As Integer) As Integer
      Select Case m_sortOrder
        Case SortOrder.Ascending
          Return value

        Case SortOrder.Descending
          Return -value

        Case Else
          Return 0
      End Select
    End Function

    #End Region ' PRIVATE METHODS

    #Region "PRIVATE FIELDS"

    Private m_sortOrder As SortOrder
    Private m_columnIndex As Integer = -1

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
