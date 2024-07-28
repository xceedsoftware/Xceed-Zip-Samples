'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [ZipArchiveListViewItem.vb]
 '*
 '* Special implementation if the FileListViewItem class that handles the treeview management
 '* for Zip archives.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports Xceed.GZip
Imports Xceed.Tar
Imports Xceed.FileSystem
Imports Xceed.Zip
Imports Xceed.FileSystem.Samples.Utils.TreeView

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class ZipArchiveListViewItem : Inherits FileListViewItem
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal file As AbstractFile, ByVal parentNode As AbstractTreeViewNode)
      Me.New(file, parentNode, String.Empty)
    End Sub

    Public Sub New(ByVal file As AbstractFile, ByVal parentNode As AbstractTreeViewNode, ByVal displayText As String)
      MyBase.New(file, displayText)
      m_parentNode = parentNode
    End Sub

#End Region     ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    Public Overrides ReadOnly Property ParentNode() As AbstractTreeViewNode
      Get
        Return m_parentNode
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Create the item physically and add it to the FolderTree if applicable.
    ''' </summary>
    Public Overrides Sub Create()
      MyBase.Create()

      ' Add a matching folder in the treeview.
      Dim node As AbstractTreeViewNode = New ZipArchiveTreeViewNode(Me.File, Me.ListView)
      m_parentNode.Nodes.Add(node)
    End Sub

    ''' <summary>
    ''' Delete the item physically and remove it from the FolderTree if applicable.
    ''' </summary>
    ''' <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    Public Overrides Sub Delete(ByVal confirmDelete As Boolean)
      ' Ensure that the attributes are up-to-date.
      m_item.Refresh()

      Dim itemIsReadonly As Boolean = ((m_item.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly)

      If confirmDelete Then
        Dim message As String = String.Format("Are you sure you want to delete '{0}'?", m_item.Name)
        Dim caption As String = "Confirm Zip Archive Delete"

        If itemIsReadonly Then
          message = String.Format("The Zip archive '{0}' is a read-only file. Are you sure you want to delete it?", m_item.Name)
        End If

        If MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
          Return
        End If
      End If

      ' Find the related treeview node
      For Each node As AbstractTreeViewNode In m_parentNode.Nodes
        If node.Text = Me.Text Then
          node.Remove()
        End If
      Next node

      If m_item.Exists Then
        If itemIsReadonly Then
          m_item.Attributes = m_item.Attributes And Not System.IO.FileAttributes.ReadOnly
        End If

        Dim deleteEvents As FileSystemEvents = New FileSystemEvents()
        AddHandler deleteEvents.ItemException, AddressOf Delete_ItemException

        m_item.Delete(deleteEvents, Nothing)

        RemoveHandler deleteEvents.ItemException, AddressOf Delete_ItemException
      End If

      Me.Remove()
    End Sub

    ''' <summary>
    ''' Refresh the current item with this new FileSystemItem. A refresh to the matching
    ''' FolderTree node will be made if applicable.
    ''' </summary>
    ''' <param name="item">The new FileSystemItem that this item represent.</param>
    Public Overrides Sub Refresh(ByVal item As FileSystemItem)
      If item Is Nothing Then
        Throw New ArgumentNullException("item")
      End If

      ' Refresh the associated node.
      If Not m_parentNode Is Nothing Then
        For Each node As AbstractTreeViewNode In m_parentNode.Nodes
          ' The FileSystemItem is already renammed. We must search for the display text.
          If node.Text = Me.Text Then
            node.Refresh(item)
            Exit For
          End If
        Next node
      End If

      m_item = item

      Me.Text = m_item.Name

      Me.RefreshIcon(True)
    End Sub

    ''' <summary>
    ''' Rename the item physically and the matching FolderTree node if applicable.
    ''' </summary>
    ''' <param name="name">The new name for the item.</param>
    Public Overrides Sub Rename(ByVal name As String)
      If name Is Nothing Then
        Throw New ArgumentNullException("name")
      End If

      Dim originalName As String = m_item.Name

      If originalName <> name Then
        ' Rename the zip file.
        m_item.Name = name

        ' We need to refresh the associated TreeNode.
        If Not m_parentNode Is Nothing Then
          For Each node As AbstractTreeViewNode In m_parentNode.Nodes
            If node.Text = originalName Then
              node.Refresh(m_item)
              Exit For
            End If
          Next node
        End If

        ' Refresh the icon.
        Me.RefreshIcon(True)
      End If
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "FILESYSTEM EVENTS"

    Private Sub Delete_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs)
      ' The item was probably deleted by some other application. We just skip this one then.
      If Not CType(IIf(TypeOf e.Exception Is ItemDoesNotExistException, e.Exception, Nothing), ItemDoesNotExistException) Is Nothing Then
        e.Action = ItemExceptionAction.Ignore
        Return
      End If

      ' At this point, we always want to delete read only items.
      If Not CType(IIf(TypeOf e.Exception Is ItemIsReadOnlyException, e.Exception, Nothing), ItemIsReadOnlyException) Is Nothing Then
        e.CurrentItem.Attributes = e.CurrentItem.Attributes And Not FileAttributes.ReadOnly
        e.Action = ItemExceptionAction.Retry
        Return
      End If

      ' For any other exceptions, we show an error message and wait for the user action.
      Dim message As String = String.Format("An error occured while deleting the item {0}.", e.CurrentItem.FullName)

      Dim result As DialogResult = MessageBox.Show(message, "Error Deleting Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)

      Select Case result
        Case DialogResult.Abort
          e.Action = ItemExceptionAction.Abort

        Case DialogResult.Retry
          e.Action = ItemExceptionAction.Retry

        Case DialogResult.Ignore
          e.Action = ItemExceptionAction.Ignore
      End Select
    End Sub

    #End Region ' FILESYSTEM EVENTS

    #Region "PROTECTED METHODS"

    Protected Overrides Sub FillData()
      ' Name
      Dim name As String = Me.File.Name

      If m_displayText.Length > 0 Then
        name = m_displayText
      End If

      Me.Text = name

      ' Size
      Dim formattedSize As String = Me.GetFormattedSize()
      Me.SubItems.Add(formattedSize)

      ' CompressedSize
      Dim formattedCompressedSize As String = Me.GetFormattedCompressedSize()
      Me.SubItems.Add(formattedCompressedSize)

      ' Type
      Dim type As String = GetType(Xceed.Zip.ZipArchive).ToString()
      Me.SubItems.Add(type)

      ' Modification date
      Dim modifiedDateFormatted As String = "N/A"

      If m_item.HasLastWriteDateTime Then
        modifiedDateFormatted = m_item.LastWriteDateTime.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern & " " & DateTimeFormatInfo.CurrentInfo.ShortTimePattern)
      End If

      Me.SubItems.Add(modifiedDateFormatted)

      ' Encryption (ZIP)
      Dim zippedFile As ZippedFile = CType(IIf(TypeOf m_item Is ZippedFile, m_item, Nothing), ZippedFile)

      If (Not zippedFile Is Nothing) AndAlso (zippedFile.Encrypted) Then
        Me.ForeColor = System.Drawing.Color.Green
      Else
        Me.ForeColor = System.Drawing.SystemColors.WindowText
      End If
    End Sub

    Protected Overrides Function GetFormattedCompressedSize() As String
      If m_item.RootFolder.HostFile Is Nothing Then
        Return String.Empty
      End If

      If TypeOf m_item Is ZippedFile Then
        Return Me.GetZipCompressedSize()
      Else If TypeOf m_item Is GZippedFile Then
        Return Me.GetGZipCompressedSize()
      Else If TypeOf m_item Is TarredFile Then
        Return Me.GetTarCompressedSize()
      Else
        Return "0 KB"
      End If
    End Function

    Protected Overrides Function GetFormattedSize() As String
      Dim formattedSize As String = "0 KB"

      Dim file As AbstractFile = Me.File

      ' KB
      If file.Exists AndAlso file.Size > 0 Then
        formattedSize = (CLng((file.Size + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return formattedSize
    End Function

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE METHODS"

    Private Function GetZipCompressedSize() As String
      Dim zippedFile As ZippedFile = CType(IIf(TypeOf m_item Is ZippedFile, m_item, Nothing), ZippedFile)

      zippedFile.Refresh()

      ' KB
      If zippedFile.Exists AndAlso zippedFile.CompressedSize > 0 Then
        Return (CLng((zippedFile.CompressedSize + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return "0 KB"
    End Function

    Private Function GetGZipCompressedSize() As String
      ' GZip does not expose CompressedSize.
      Return "N/A"
    End Function

    Private Function GetTarCompressedSize() As String
      Dim tarredFile As TarredFile = CType(IIf(TypeOf m_item Is TarredFile, m_item, Nothing), TarredFile)

      tarredFile.Refresh()

      ' KB
      If tarredFile.Exists AndAlso tarredFile.Size > 0 Then
        Return (CLng((tarredFile.Size + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return "0 KB"
    End Function

    #End Region ' PRIVATE METHODS

    #Region "PRIVATE FIELDS"

    Private m_parentNode As AbstractTreeViewNode '= null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace