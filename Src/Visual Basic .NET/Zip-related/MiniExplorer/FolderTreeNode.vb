' 
' Xceed Zip for .NET - MiniExplorer Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [FolderTreeNode.vb]
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
Imports Xceed.FileSystem

Namespace Xceed.FileSystem.Samples.MiniExplorer
  ' <summary>
  ' Summary description for FolderTreeNode.
  ' </summary>
  Friend Class FolderTreeNode
    Inherits TreeNode
    Public Sub New(ByVal folder As AbstractFolder, ByVal displayName As String)
      MyBase.New(displayName, 1, 1)
      m_folder = folder

      ' Display a [+] by default
      Me.Nodes.Add(String.Empty)

    End Sub

    Public Sub New(ByVal displayName As String)
      MyBase.New(displayName, 1, 1)
    End Sub

    Public ReadOnly Property Folder() As AbstractFolder
      Get
        Return m_folder
      End Get
    End Property

    Private m_folder As AbstractFolder = Nothing
  End Class
End Namespace

