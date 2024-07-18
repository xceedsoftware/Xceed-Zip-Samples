Imports System
Imports System.Windows.Forms
Imports Xceed.FileSystem

Namespace FolderViews
  Public Class FileListViewItem
    Inherits ListViewItem

    Public Sub New(ByVal file As AbstractFile)
      Me.Text = file.Name

      If (file.Size = -1) Then
        Me.SubItems.Add("N/A")
      Else
        Dim kiloBytes As Double = System.Math.Ceiling(CType(file.Size, Double) / CType(1024, Double))
        Me.SubItems.Add(kiloBytes.ToString("N0") + " kb")
      End If

      If (file.LastWriteDateTime = DateTime.MinValue) Then
        Me.SubItems.Add("N/A")
      Else
        Me.SubItems.Add(file.LastWriteDateTime.ToString())
      End If

      m_file = file
    End Sub

    Public ReadOnly Property File() As AbstractFile
      Get
        Return m_file
      End Get
    End Property

    Private m_file As AbstractFile ' = Nothing
  End Class
end namespace
