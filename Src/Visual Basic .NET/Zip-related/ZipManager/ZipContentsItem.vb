'Xceed Zip for .NET - ZipManager Sample Application
'Copyright (c) 2000-2002 - Xceed Software Inc.
'
'[ZipContentsItem.vb]
'
'This application demonstrates how to use Xceed Zip for .NET.
'
'This file is part of Xceed Zip for .NET. The source code in this file 
'is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.

Imports Xceed.FileSystem
Imports Xceed.Zip

Namespace Xceed.Zip.Samples
  Public Class ZipContentsItem
    Inherits System.Windows.Forms.ListViewItem

    Public Sub New(ByVal File As ZippedFile)
      Call MyBase.New(File.FullName)
      MyBase.SubItems.Add(File.LastWriteDateTime.ToString())
      MyBase.SubItems.Add(File.Size.ToString())
      MyBase.SubItems.Add(File.CompressedSize.ToString())
      If File.Size <> 0 Then
        MyBase.SubItems.Add(System.Math.Round(100 - File.CompressedSize / File.Size * 100).ToString() + "%")
      Else
        MyBase.SubItems.Add("0%")
      End If
      MyBase.SubItems.Add(File.Attributes.ToString())

      If File.Encrypted Then
        MyBase.ForeColor = Color.Red
      End If

      m_file = File
    End Sub

    Public ReadOnly Property File() As ZippedFile
      Get
        Return m_file
      End Get
    End Property

    Private m_file As ZippedFile
  End Class
End Namespace