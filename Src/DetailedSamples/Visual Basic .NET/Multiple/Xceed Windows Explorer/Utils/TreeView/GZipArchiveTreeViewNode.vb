'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [GZipArchiveTreeViewNode.vb]
 '*
 '* Implementation of the AbstractTreeViewNode class and represents a GZipArchive.
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
Imports Xceed.Zip
Imports Xceed.Tar
Imports Xceed.FileSystem.Samples.Utils.ListView
Imports Xceed.GZip

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public Class GZipArchiveTreeViewNode : Inherits AbstractTreeViewNode
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal file As AbstractFile)
      Me.New(file, Nothing)
    End Sub

    Public Sub New(ByVal file As AbstractFile, ByVal contentListView As System.Windows.Forms.ListView)
      If file Is Nothing Then
        Throw New ArgumentNullException("file")
      End If

      m_item = file
      m_contentListView = contentListView

      Me.RefreshIcon(False)
      Me.RefreshText()

      ' Display a [+] by default
      Me.Nodes.Add(String.Empty)

      ' Initialize the icon updater.
      m_iconUpdater = New TreeViewIconUpdater(Me)
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    ''' <summary>
    ''' Gets the AbstractFolder this node represent.
    ''' </summary>
    Public Overrides ReadOnly Property Folder() As AbstractFolder
      Get
        Return m_gzipArchive
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    Public Overrides Sub InitializeFolder()
      If Not m_gzipArchive Is Nothing Then
        Return
      End If

      Try
        GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles

        m_gzipArchive = New GZipArchive(CType(IIf(TypeOf m_item Is AbstractFile, m_item, Nothing), AbstractFile))
      Catch e1 As InvalidGZipStructureException
        MessageBox.Show("The GZip archive is either corrupted or not a valid archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch
        MessageBox.Show("An error occured while reading the archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
    End Sub

    ''' <summary>
    ''' Refresh the current item with this new FileSystemItem. 
    ''' </summary>
    ''' <param name="item">The new FileSystemItem that this item represent.</param>
    Public Overrides Sub Refresh(ByVal item As FileSystemItem)
      MyBase.Refresh(item)

      ' Reset the archive if it was previously loaded.
      If Not m_gzipArchive Is Nothing Then
        m_gzipArchive = Nothing

        Me.InitializeFolder()
      End If
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "PRIVATE FIELDS"

    Private m_gzipArchive As GZipArchive ' = null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace