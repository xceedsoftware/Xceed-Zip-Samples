'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FolderTreeViewNode.vb]
 '*
 '* Implementation of the AbstractTreeViewNode class and represent an AbstractFolder.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.FileSystem.Samples.Utils.ListView

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public Class FolderTreeViewNode : Inherits AbstractTreeViewNode
    #Region "CONSTRUSTORS"

    Public Sub New(ByVal folder As AbstractFolder)
      Me.New(folder, Nothing, String.Empty)
    End Sub

    Public Sub New(ByVal folder As AbstractFolder, ByVal contentListView As System.Windows.Forms.ListView)
      Me.New(folder, contentListView, String.Empty)
    End Sub

    Public Sub New(ByVal folder As AbstractFolder, ByVal contentListView As System.Windows.Forms.ListView, ByVal displayText As String)
      If folder Is Nothing Then
        Throw New ArgumentNullException("folder")
      End If

      m_item = folder
      m_contentListView = contentListView
      m_displayText = displayText

      Me.RefreshIcon(False)
      Me.RefreshText()

      ' Display a [+] by default
      Me.Nodes.Add(String.Empty)

      ' Initialize the icon updater.
      m_iconUpdater = New TreeViewIconUpdater(Me)
    End Sub

#End Region     ' CONSTRUSTORS

    #Region "PUBLIC PROPERTIES"

    Public Overrides ReadOnly Property Folder() As AbstractFolder
      Get
        Return CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES
  End Class
End Namespace