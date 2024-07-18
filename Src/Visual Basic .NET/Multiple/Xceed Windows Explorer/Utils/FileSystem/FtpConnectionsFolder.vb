'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FtpConnectionsFolder.vb]
 '*
 '* Custom implementation of AbstractFolder. It contains a list of
 '* FtpConnectionFolder and is used to manage Ftp connections in the
 '* FolderTree.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports Xceed.FileSystem
Imports Xceed.Ftp

Namespace Xceed.FileSystem.Samples.Utils.FileSystem
  Public Class FtpConnectionsFolder : Inherits AbstractFolder
    #Region "PROTECTED PROPERTIES"

    Protected Overrides ReadOnly Property DoHasAttributes() As Boolean
      Get
        Return False
      End Get
    End Property

    Protected Overrides Property DoAttributes() As System.IO.FileAttributes
      Get
        Throw New NotSupportedException()
      End Get
      Set
        Throw New NotSupportedException()
      End Set
    End Property

    Protected Overrides ReadOnly Property DoHasCreationDateTime() As Boolean
      Get
        Return False
      End Get
    End Property

    Protected Overrides Property DoCreationDateTime() As DateTime
      Get
        Throw New NotSupportedException()
      End Get
      Set
        Throw New NotSupportedException()
      End Set
    End Property

    Protected Overrides ReadOnly Property DoExists() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides ReadOnly Property DoFullName() As String
      Get
        Return m_fullName
      End Get
    End Property

    Protected Overrides ReadOnly Property DoHasLastAccessDateTime() As Boolean
      Get
        Return False
      End Get
    End Property

    Protected Overrides Property DoLastAccessDateTime() As DateTime
      Get
        Throw New NotSupportedException()
      End Get
      Set
        Throw New NotSupportedException()
      End Set
    End Property

    Protected Overrides ReadOnly Property DoHasLastWriteDateTime() As Boolean
      Get
        Return False
      End Get
    End Property

    Protected Overrides Property DoLastWriteDateTime() As DateTime
      Get
        Throw New NotSupportedException()
      End Get
      Set
        Throw New NotSupportedException()
      End Set
    End Property

    Protected Overrides ReadOnly Property DoIsRoot() As Boolean
      Get
        Return True
      End Get
    End Property

    Protected Overrides Property DoName() As String
      Get
        Return m_fullName
      End Get
      Set
        Throw New NotSupportedException()
      End Set
    End Property

    Protected Overrides ReadOnly Property DoParentFolder() As AbstractFolder
      Get
        Return Me
      End Get
    End Property

    Protected Overrides ReadOnly Property DoRootFolder() As AbstractFolder
      Get
        Return Me
      End Get
    End Property

    #End Region ' PROTECTED PROPERTIES

    #Region "PUBLIC METHODS"

    Public Function CreateConnectionFolder(ByVal connection As FtpConnection) As FtpConnectionFolder
      Dim folder As FtpConnectionFolder = New FtpConnectionFolder(New FtpFolder(connection))

      m_children.Add(folder)

      Return folder
    End Function

    #End Region ' PUBLIC METHODS

    #Region "PROTECTED METHODS"

    Protected Overrides Sub DoCopyTo(ByVal session As FileSystemEventsSession, ByVal destination As FileSystemItem, ByVal replaceExistingFiles As Boolean)
      Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub DoCreate(ByVal session As FileSystemEventsSession)
      Throw New NotSupportedException()
    End Sub

    Protected Overrides Sub DoDelete(ByVal session As FileSystemEventsSession)
      Throw New NotSupportedException()
    End Sub

    Protected Overrides Function DoGetChildItems(ByVal session As FileSystemEventsSession) As FileSystemItem()
      Return CType(m_children.ToArray(GetType(FileSystemItem)), FileSystemItem())
    End Function

    Protected Overrides Function DoGetFile(ByVal session As FileSystemEventsSession, ByVal fileName As String) As AbstractFile
      Throw New NotSupportedException()
    End Function

    Protected Overrides Function DoGetFolder(ByVal session As FileSystemEventsSession, ByVal folderName As String) As AbstractFolder
      Throw New NotSupportedException()
    End Function

    Protected Overrides Sub DoRefresh(ByVal session As FileSystemEventsSession)
      ' Nothing to do.
    End Sub

    Protected Overrides Function IsPathRooted(ByVal path As String) As Boolean
      Return False
    End Function

    Public Overrides Function IsSameAs(ByVal target As FileSystemItem) As Boolean
      If target Is Nothing Then
        Throw New ArgumentNullException("target")
      End If

      Dim folder As AbstractFolder = CType(IIf(TypeOf target Is AbstractFolder, target, Nothing), AbstractFolder)

      If folder Is Nothing Then
        Return False
      End If

      Return (folder.FullName = Me.FullName)
    End Function

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE FIELDS"

    Private m_fullName As String = "Ftp Connections"
    Private m_children As ArrayList = New ArrayList()

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
