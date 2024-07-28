'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [MyComputerFolder.vb]
 '*
 '* Custom implementation of AbstractFolder to represent the "MyComputer"
 '* folder. It basically just returns the list of drives on the machine.
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

Namespace Xceed.FileSystem.Samples.Utils.FileSystem
  ''' <summary>
  ''' This class simulate the MyComputer on Windows System. It only expose
  ''' all the drives on the machine as child items. 
  ''' </summary>
  Public Class MyComputerFolder : Inherits AbstractFolder
    #Region "CONSTRUCTORS"

    Public Sub New()
      Me.Refresh()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

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
      End Set
    End Property

    Protected Overrides ReadOnly Property DoParentFolder() As AbstractFolder
      Get
        Return Nothing
      End Get
    End Property

    Protected Overrides ReadOnly Property DoRootFolder() As AbstractFolder
      Get
        Return Nothing
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

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
      Dim array As ArrayList = New ArrayList(m_children.Count)

      For Each folder As Object In m_children
        ' All drives are DiskFolders.
        array.Add(New DiskFolder(folder.ToString()))
      Next folder

      Return CType(array.ToArray(GetType(FileSystemItem)), FileSystemItem())
    End Function

    Protected Overrides Function DoGetFile(ByVal session As FileSystemEventsSession, ByVal fileName As String) As AbstractFile
      Throw New NotSupportedException()
    End Function

    Protected Overrides Function DoGetFolder(ByVal session As FileSystemEventsSession, ByVal folderName As String) As AbstractFolder
      If m_children.Contains(folderName) Then
        Return New DiskFolder(folderName)
      End If

      Return Nothing
    End Function

    Protected Overrides Sub DoRefresh(ByVal session As FileSystemEventsSession)
      ' Clear the actual list.
      m_children.Clear()

      Try
        ' Get the list of drives on the local machine and add them
        ' to the children list.
        Dim drives As String() = Environment.GetLogicalDrives()

        For Each drive As String In drives
          m_children.Add(drive)
        Next drive
      Catch
      End Try
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

    #End Region ' PUBLIC METHODS

    #Region "PRIVATE FIELDS"

    Private Const m_fullName As String = "My Computer"
    Private m_children As ArrayList = New ArrayList()

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
