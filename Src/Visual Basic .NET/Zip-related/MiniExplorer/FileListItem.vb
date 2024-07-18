' 
' Xceed Zip for .NET - MiniExplorer Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
' 
' [FileListItem.vb]
'  
' This application demonstrates how to use the Xceed FileSystem Object model
' in a generic way.
'  
' This file is part of Xceed Zip for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
'

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports Xceed.FileSystem
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples.MiniExplorer
  ' <summary>
  ' Summary description for FileListItem.
  ' </summary>
  Friend Class FileListItem
    Inherits ListViewItem
    Public Sub New(ByVal file As AbstractFile)
      MyBase.New(file.Name, 0)
      Try
        SubItems.Add(file.Size.ToString())
      Catch
        SubItems.Add("NA")
      End Try

      Try
        SubItems.Add(file.Attributes.ToString())
      Catch
        SubItems.Add("NA")
      End Try

      Try
        SubItems.Add(file.LastWriteDateTime.ToString())
      Catch
        SubItems.Add("NA")
      End Try

      Try
        If file.LastAccessDateTime = DateTime.MinValue Then
          SubItems.Add("NA")
        Else
          SubItems.Add(file.LastAccessDateTime.ToString())
        End If
      Catch
        SubItems.Add("NA")
      End Try

      Try
        If file.CreationDateTime = DateTime.MinValue Then
          SubItems.Add("NA")
        Else
          SubItems.Add(file.CreationDateTime.ToString())
        End If
      Catch
        SubItems.Add("NA")
      End Try

      Me.UseItemStyleForSubItems = False

      Dim alternate As Color = Me.BackColor

      If alternate.GetBrightness() > 0.5 Then
        alternate = Color.FromArgb(alternate.R * 9 / 10, alternate.G * 19 / 20, alternate.B * 9 / 10)
      Else
        alternate = Color.FromArgb(alternate.R * 20 / 19, alternate.G * 10 / 9, alternate.B * 20 / 19)
      End If

      Me.SubItems(1).BackColor = alternate
      Me.SubItems(3).BackColor = alternate
      Me.SubItems(5).BackColor = alternate

      If (TypeOf (file) Is ZippedFile) Then
        If (CType(file, ZippedFile).Encrypted) Then
          MyBase.ForeColor = Color.Red
        End If
      End If

      m_file = file
    End Sub

    Public Sub New(ByVal fileName As String)
      MyBase.New(fileName, 0)
    End Sub

    Public ReadOnly Property File() As AbstractFile
      Get
        Return m_file
      End Get
    End Property

    Private m_file As AbstractFile = Nothing
  End Class
End Namespace


