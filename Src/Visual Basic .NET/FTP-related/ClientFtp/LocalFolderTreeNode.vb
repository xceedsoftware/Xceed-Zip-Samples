Imports System.Windows.Forms
Imports Xceed.FileSystem

Public Class LocalFolderTreeNode
  Inherits TreeNode

  Public Sub New(ByVal folder As AbstractFolder)
    If folder Is Nothing Then
      Throw New ArgumentNullException("folder")
    End If

    m_folder = folder

    If folder.IsRoot Then
      Me.Text = folder.FullName
    Else
      Me.Text = folder.Name
    End If

    Me.ImageIndex = CInt(FtpItemIconEnum.ClosedFolder)
    Me.SelectedImageIndex = CInt(FtpItemIconEnum.OpenedFolder)

    ' Make sure a [+] appears, using a dummy node.
    Me.Nodes.Add(New TreeNode("Browsing folder contents..."))
  End Sub


  Public ReadOnly Property Folder() As AbstractFolder
    Get
      Return m_folder
    End Get
  End Property


  Public Sub UpdateContents()
    ' Make sure to stay in the same expanded state
    Dim expanded As Boolean = Me.IsExpanded
    Dim folders As AbstractFolder() = Nothing

    Try
      folders = m_folder.GetFolders(False)
    Finally
      ' Make sure to empty even if we get an exception.
      Me.Nodes.Clear()
    End Try

    Dim folder As AbstractFolder

    For Each folder In folders
      Me.Nodes.Add(New LocalFolderTreeNode(folder))
    Next folder

    If expanded Then
      Me.Expand()
    End If
  End Sub


  Public Sub FillList(ByVal contents As ListView)
    Dim items As FileSystemItem() = Nothing

    Try
      items = m_folder.GetItems(False)
    Finally
      ' Make sure to empty even if we get an exception.
      contents.Items.Clear()
    End Try

    Dim item As FileSystemItem
    For Each item In items
      contents.Items.Add(New LocalListViewItem(item))
    Next item
  End Sub


  Private m_folder As AbstractFolder = Nothing
End Class
