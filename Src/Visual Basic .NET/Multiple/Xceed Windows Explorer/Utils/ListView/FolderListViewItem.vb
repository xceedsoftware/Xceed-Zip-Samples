'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FileListViewItem.vb]
 '*
 '* Implementation of the AbstractListViewItem class and represent an AbstractFolder.
 '* It expose methods to controls the action the user can make on this item. (copy, paste, delete...)
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
Imports Xceed.FileSystem
Imports Xceed.FileSystem.Samples.Utils.TreeView
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class FolderListViewItem : Inherits AbstractListViewItem
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal folder As AbstractFolder)
      Me.New(folder, Nothing, String.Empty)
    End Sub

    Public Sub New(ByVal folder As AbstractFolder, ByVal parentNode As AbstractTreeViewNode)
      Me.New(folder, parentNode, String.Empty)
    End Sub

    Public Sub New(ByVal folder As AbstractFolder, ByVal parentNode As AbstractTreeViewNode, ByVal displayText As String)
      If folder Is Nothing Then
        Throw New ArgumentNullException("folder")
      End If

      m_item = folder
      m_parentNode = parentNode
      m_displayText = displayText

      Me.RefreshIcon(False)
      Me.FillData()
    End Sub

#End Region     ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    ''' <summary>
    ''' Gets the internal FileSystemItem object as an AbstractFolder.
    ''' </summary>
    Public ReadOnly Property Folder() As AbstractFolder
      Get
        Return CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)
      End Get
    End Property

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
      ' Let the base class create the item physically.
      MyBase.Create()

      ' Add a matching folder in the treeview.
      Dim node As AbstractTreeViewNode = New FolderTreeViewNode(Me.Folder, Me.ListView)
      m_parentNode.Nodes.Add(node)
    End Sub

    ''' <summary>
    ''' Delete the item physically and remove it from the FolderTree if applicable.
    ''' </summary>
    ''' <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    Public Overrides Sub Delete(ByVal confirmDelete As Boolean)
      ' Ensure that the attributes are up-to-date.
      m_item.Refresh()

      Dim folder As AbstractFolder = Me.Folder

      If Not folder Is Nothing AndAlso folder.IsRoot Then
        Throw New NotSupportedException("Cannot delete a root folder.")
      End If

      Dim itemIsReadonly As Boolean = ((m_item.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly)

      ' Confirm deletion.
      If confirmDelete Then
        Dim message As String = String.Format("Are you sure you want to remove the folder '{0}' and all its contents?", m_item.Name)
        Dim caption As String = "Confirm Folder Delete"

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

        Dim deleteEvents As FileSystemEvents = New FileSystemEvents
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

      If CType(IIf(TypeOf item Is AbstractFolder, item, Nothing), AbstractFolder) Is Nothing Then
        Throw New ArgumentException("The item must be an AbstractFolder.")
      End If

      ' Refresh the associated node.
      If (Not m_parentNode Is Nothing) AndAlso (TypeOf m_item Is AbstractFolder) Then
        For Each node As AbstractTreeViewNode In m_parentNode.Nodes
          ' The FileSystemItem is already renammed. We must search for the display text.
          If node.Text = Me.Text Then
            node.Refresh(item)
            Exit For
          End If
        Next node
      End If

      ' Refresh ourself.
      m_item = item

      Me.FillData()

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

      If (Not Me.RenameToolEnabled) Then
        Throw New ApplicationException("Cannot rename a root folder.")
      End If

      Dim originalName As String = m_item.Name

      If name <> originalName Then
        m_item.Name = name

        If (Not m_parentNode Is Nothing) AndAlso (TypeOf m_item Is AbstractFolder) Then
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

      ' Zipped folder can be password protected. We need to ask the user to input that password.
      If Not CType(IIf(TypeOf e.Exception Is Xceed.Zip.InvalidDecryptionPasswordException, e.Exception, Nothing), Xceed.Zip.InvalidDecryptionPasswordException) Is Nothing Then
        Dim passwordForm As InputPasswordForm = New InputPasswordForm
        Try
          If passwordForm.ShowDialog(Nothing, e.CurrentItem.FullName) = System.Windows.Forms.DialogResult.OK Then
            If (Not CType(IIf(TypeOf e.CurrentItem Is ZippedFile, e.CurrentItem, Nothing), ZippedFile) Is Nothing) OrElse (CType(Not IIf(TypeOf e.CurrentItem Is ZippedFolder, e.CurrentItem, Nothing), ZippedFolder) Is Nothing) Then
              Dim archive As ZipArchive = CType(IIf(TypeOf e.CurrentItem.RootFolder Is ZipArchive, e.CurrentItem.RootFolder, Nothing), ZipArchive)

              If Not archive Is Nothing Then
                archive.DefaultDecryptionPassword = passwordForm.Password
                e.Action = ItemExceptionAction.Retry
                Return
              End If

              ' Set the last decryption password used for future use.
              Options.ZipLastDecryptionPasswordUsed = passwordForm.Password
            End If
          End If
        Finally
          CType(passwordForm, IDisposable).Dispose()
        End Try
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
      Me.SubItems.Clear()

      ' Name
      Dim name As String = m_item.Name

      If Me.Folder.IsRoot Then
        name = m_item.FullName
      End If

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
      Dim type As String = m_item.GetType().ToString()
      Me.SubItems.Add(type)

      ' Modification date
      Dim modifiedDateFormatted As String = "N/A"

      If m_item.HasLastWriteDateTime Then
        modifiedDateFormatted = m_item.LastWriteDateTime.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern & " " & DateTimeFormatInfo.CurrentInfo.ShortTimePattern)
      End If

      Me.SubItems.Add(modifiedDateFormatted)
    End Sub

    Protected Overrides Function GetFormattedCompressedSize() As String
      ' Folders don't have a size.
      Return String.Empty
    End Function

    Protected Overrides Function GetFormattedSize() As String
      ' Folders don't have a size.
      Return String.Empty
    End Function

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE FIELDS"

    Private m_parentNode As AbstractTreeViewNode '= null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
