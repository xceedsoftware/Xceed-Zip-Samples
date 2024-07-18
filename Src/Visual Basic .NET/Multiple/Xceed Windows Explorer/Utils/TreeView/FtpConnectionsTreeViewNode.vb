'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FtpConnectionsTreeViewNode.vb]
 '*
 '* Implementation of the AbstractTreeViewNode class and represents the Ftp connections repository.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports Xceed.FileSystem.Samples.Utils.ListView
Imports Xceed.FileSystem.Samples.Utils.FileSystem

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public Class FtpConnectionsTreeViewNode : Inherits AbstractTreeViewNode
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal folder As FtpConnectionsFolder)
      Me.New(folder, Nothing, String.Empty)
    End Sub

    Public Sub New(ByVal folder As FtpConnectionsFolder, ByVal contentListView As System.Windows.Forms.ListView)
      Me.New(folder, contentListView, String.Empty)
    End Sub

    Public Sub New(ByVal folder As FtpConnectionsFolder, ByVal contentListView As System.Windows.Forms.ListView, ByVal displayText As String)
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

#End Region     ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    Public Overrides ReadOnly Property Folder() As AbstractFolder
      Get
        Return CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "New" function should be enabled.
    ''' </summary>
    Public Overrides ReadOnly Property NewToolEnabled() As Boolean
      Get
        Return False
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "New Ftp connection" function should be enabled.
    ''' </summary>
    Public Overrides ReadOnly Property NewFtpConnectionToolEnabled() As Boolean
      Get
        Return True
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Paste" function should be enabled.
    ''' </summary>
    Public Overrides ReadOnly Property PasteToolEnabled() As Boolean
      Get
        Return False
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES
  End Class
End Namespace