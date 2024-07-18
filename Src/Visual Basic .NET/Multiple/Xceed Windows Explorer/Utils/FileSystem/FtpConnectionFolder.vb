'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FtpConnectionFolder.vb]
 '*
 '* Custom implementation of AbstractFolder. It is contained in a
 '* FtpConnectionsFolder and represent the root of a FtpFolder.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.IO
Imports Xceed.FileSystem
Imports Xceed.Ftp

Namespace Xceed.FileSystem.Samples.Utils.FileSystem
  Public Class FtpConnectionFolder : Inherits AbstractFolder
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal ftpFolder As FtpFolder)
      If ftpFolder Is Nothing Then
        Throw New ArgumentNullException("ftpFolder")
      End If

      m_ftpFolder = ftpFolder
    End Sub

    #End Region ' CONSTRUCTORS

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
        If m_ftpFolder.Connection Is Nothing Then
          Return String.Empty
        End If

        Dim name As String = m_ftpFolder.Connection.HostName

        If (Not m_ftpFolder.Connection.UserName Is Nothing) AndAlso (m_ftpFolder.Connection.UserName.Length > 0) Then
          name &= " (" & m_ftpFolder.Connection.UserName & ")"
        Else
          name &= " (anonymous)"
        End If

        Return name
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
        If m_ftpFolder.Connection Is Nothing Then
          Return String.Empty
        End If

        Dim name As String = m_ftpFolder.Connection.HostName

        If (Not m_ftpFolder.Connection.UserName Is Nothing) AndAlso (m_ftpFolder.Connection.UserName.Length > 0) Then
          name &= " (" & m_ftpFolder.Connection.UserName & ")"
        Else
          name &= " (anonymous)"
        End If

        Return name
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
      Return m_ftpFolder.GetItems(session.Events, session.UserData, False)
    End Function

    Protected Overrides Function DoGetFile(ByVal session As FileSystemEventsSession, ByVal fileName As String) As AbstractFile
      Return m_ftpFolder.GetFile(session.Events, session.UserData, fileName)
    End Function

    Protected Overrides Function DoGetFiles(ByVal session As FileSystemEventsSession, ByVal recursive As Boolean, ByVal filters As Filter()) As AbstractFile()
      Return m_ftpFolder.GetFiles(session.Events, session.UserData, recursive, filters)
    End Function

    Protected Overrides Function DoGetFolder(ByVal session As FileSystemEventsSession, ByVal folderName As String) As AbstractFolder
      Return m_ftpFolder.GetFolder(session.Events, session.UserData, folderName)
    End Function

    Protected Overrides Function DoGetFolders(ByVal session As FileSystemEventsSession, ByVal recursive As Boolean, ByVal filters As Filter()) As AbstractFolder()
      Return m_ftpFolder.GetFolders(session.Events, session.UserData, recursive, filters)
    End Function

    Protected Overrides Sub DoRefresh(ByVal session As FileSystemEventsSession)
      m_ftpFolder.Refresh()
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

    Private m_ftpFolder As FtpFolder ' = null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
