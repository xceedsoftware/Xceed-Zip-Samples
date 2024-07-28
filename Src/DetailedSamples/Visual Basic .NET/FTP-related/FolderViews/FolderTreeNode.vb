Imports System
Imports System.Windows.Forms
Imports Xceed.FileSystem

Namespace FolderViews
  Public Class FolderTreeNode
    Inherits TreeNode

    Public Sub New(ByVal folder As AbstractFolder)
      If (folder.Name.Length = 0) Then
        Me.Text = folder.FullName
      Else
        Me.Text = folder.Name
      End If

      Me.Nodes.Add(New TreeNode)

      m_folder = folder
    End Sub

    Public ReadOnly Property Folder() As AbstractFolder
      Get
        Return m_folder
      End Get
    End Property

    Public ReadOnly Property FolderForm() As FolderForm
      Get
        If Me.TreeView Is Nothing Then
          Return Nothing
        End If

        Return Me.TreeView.FindForm()
      End Get
    End Property

    Public Sub OnBeforeExpand()
      Me.Update(False)
    End Sub

    Public Sub OnAfterSelect(ByVal fileListView As ListView)
      Me.Update(False)

      fileListView.Items.Clear()

      Dim item As FileSystemItem
      For Each item In m_childItems
        If TypeOf item Is AbstractFile Then
          Dim file As AbstractFile = item

          If Not file Is Nothing Then
            fileListView.Items.Add(New FileListViewItem(file))
          End If
        End If
      Next item
    End Sub

    Public Sub Update(ByVal forceUpdate As Boolean)
      If forceUpdate Or (m_childItems Is Nothing) Then
        Me.TreeView.Cursor = Cursors.WaitCursor

        Try
          m_childItems = m_folder.GetItems(False)
        Catch
          ' Most probably a missing permission. Report nothing.
          m_childItems = New FileSystemItem(1) {}
        Finally
          Me.TreeView.Cursor = Cursors.Default
        End Try

        Dim expanded As Boolean = Me.IsExpanded

        Me.Nodes.Clear()

        Dim item As FileSystemItem
        For Each item In m_childItems
          If TypeOf item Is AbstractFolder Then
            Dim folder As AbstractFolder = item

            If Not folder Is Nothing Then
              Me.Nodes.Add(New FolderTreeNode(folder))
            End If
          End If
        Next item

        If expanded Then
          ''# TODO
        End If
      End If
    End Sub

    Private m_folder As AbstractFolder  'Nothing
    Private m_childItems() As FileSystemItem 'Nothing
  End Class
End Namespace
