Imports System.Windows.Forms
Imports Xceed.Ftp

Public Class RemoteListViewItem
  Inherits ListViewItem

  Public Sub New(ByVal info As FtpItemInfo)
    MyBase.New(info.Name)

    If info Is Nothing Then
      Throw New ArgumentNullException("info")
    End If

    m_info = info

    ' We only show the size for a File or unknown.
    If info.Type = FtpItemType.File Or info.Type = FtpItemType.Unknown Then
      Me.SubItems.Add(RemoteFolderTreeNode.FormatSize(info.Size))
    Else
      Me.SubItems.Add("")
    End If

    Me.SubItems.Add(info.DateTime.ToString())

    Select Case info.Type
      Case FtpItemType.File, FtpItemType.Unknown
        Me.ImageIndex = CInt(FtpItemIconEnum.File)

      Case FtpItemType.Folder
        Me.ImageIndex = CInt(FtpItemIconEnum.ClosedFolder)

      Case FtpItemType.Link
        Me.ImageIndex = CInt(FtpItemIconEnum.ClosedLink)
    End Select
  End Sub


  Public ReadOnly Property Info() As FtpItemInfo
    Get
      Return m_info
    End Get
  End Property


  Private m_info As FtpItemInfo = FtpItemInfo.Empty
End Class
