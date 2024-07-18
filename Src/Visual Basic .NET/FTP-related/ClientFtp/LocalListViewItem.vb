Imports System.Windows.Forms
Imports Xceed.FileSystem

Public Class LocalListViewItem
  Inherits ListViewItem

  Public Sub New(ByVal item As FileSystemItem)
    MyBase.New(item.Name)

    If item Is Nothing Then
      Throw New ArgumentNullException("item")
    End If

    If TypeOf item Is AbstractFile Then
      Dim file As AbstractFile = item

      Me.SubItems.Add(RemoteFolderTreeNode.FormatSize(file.Size))
      Me.ImageIndex = CInt(FtpItemIconEnum.File)
    Else
      Me.SubItems.Add("")
      Me.ImageIndex = CInt(FtpItemIconEnum.ClosedFolder)
    End If

    Me.SubItems.Add(item.LastWriteDateTime.ToString())

    m_item = item
  End Sub


  Public ReadOnly Property Item() As FileSystemItem
    Get
      Return m_item
    End Get
  End Property


  Private m_item As FileSystemItem = Nothing
End Class
