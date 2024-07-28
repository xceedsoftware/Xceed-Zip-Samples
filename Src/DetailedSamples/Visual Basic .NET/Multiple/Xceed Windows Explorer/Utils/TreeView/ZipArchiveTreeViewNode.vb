'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [ZipArchiveTreeViewNode.vb]
 '*
 '* Implementation of the AbstractTreeViewNode and represent a ZipArchive.
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
Imports Xceed.FileSystem.Samples.Utils.ListView

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public Class ZipArchiveTreeViewNode : Inherits AbstractTreeViewNode
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
        Return m_zipArchive
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    Public Overrides Sub InitializeFolder()
      If Not m_zipArchive Is Nothing Then
        Return
      End If

      Try
        m_zipArchive = New ZipArchive(CType(IIf(TypeOf m_item Is AbstractFile, m_item, Nothing), AbstractFile))

        m_zipArchive.DefaultEncryptionPassword = Options.ZipDefaultEncryptionPassword
        m_zipArchive.DefaultCompressionLevel = Options.ZipDefaultCompressionLevel
        m_zipArchive.DefaultCompressionMethod = Options.ZipDefaultCompressionMethod
        m_zipArchive.DefaultEncryptionMethod = Options.ZipDefaultEncryptionMethod
        m_zipArchive.DefaultEncryptionStrength = Options.ZipDefaultEncryptionStrength

        If Options.ZipLastDecryptionPasswordUsed.Length > 0 Then
          m_zipArchive.DefaultDecryptionPassword = Options.ZipLastDecryptionPasswordUsed
        End If
      Catch e1 As InvalidZipStructureException
        MessageBox.Show("The Zip archive is either corrupted or not a valid archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      Catch
        MessageBox.Show("An error occured while reading the archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End Try
    End Sub

    ''' <summary>
    ''' Refresh the current item with this new AbstractFolder. 
    ''' </summary>
    ''' <param name="item">The new AbstractFolder that this item represent.</param>
    Public Overrides Sub Refresh(ByVal item As FileSystemItem)
      MyBase.Refresh(item)

      ' Refresh the archive if it was previously loaded.
      If Not m_zipArchive Is Nothing Then
        m_zipArchive = Nothing

        Me.InitializeFolder()
      End If
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "PRIVATE FIELDS"

    Private m_zipArchive As ZipArchive ' = null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace