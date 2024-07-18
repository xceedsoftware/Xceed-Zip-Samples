'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [IconCache.vb]
 '*
 '* This class expose 2 static ImageList containing all the images that
 '* have been requested so far. It allows to retreive icons for a
 '* specified FileSystemItem.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.FileSystem.Samples.Utils.API
Imports Xceed.FileSystem.Samples.Utils.FileSystem
Imports Xceed.FileSystem.Samples.Utils.Icons
Imports Xceed.Zip
Imports Xceed.Ftp
Imports Xceed.Tar
Imports Xceed.GZip

Namespace Xceed.FileSystem.Samples.Utils.Icons
  Public Class IconCache
    #Region "CONSTRUCTORS"

    Shared Sub New()
      ' Initialize the objects.
      m_iconsIndexMapping = New SortedList()
      m_smallImageList = New ImageList()
      m_largeImageList = New ImageList()

      ' Get the system sizes.
      m_smallIconSize = GetSystemIconSize(True)
      m_largeIconSize = GetSystemIconSize(False)

      ' Set the image lists properties.
      m_smallImageList.ColorDepth = ColorDepth.Depth32Bit
      m_smallImageList.ImageSize = m_smallIconSize
      m_smallImageList.TransparentColor = Color.Transparent

      m_largeImageList.ColorDepth = ColorDepth.Depth32Bit
      m_largeImageList.ImageSize = m_largeIconSize
      m_largeImageList.TransparentColor = Color.Transparent

      ' Loads the default icons.
      IconCache.LoadDefaultIcons()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC STATIC PROPERTIES"

    Public Shared ReadOnly Property LargeIconList() As ImageList
      Get
        Return m_largeImageList
      End Get
    End Property

    Public Shared ReadOnly Property SmallIconList() As ImageList
      Get
        Return m_smallImageList
      End Get
    End Property

    #End Region ' PUBLIC STATIC PROPERTIES

    #Region "PUBLIC STATIC METHODS"

    Public Shared Sub ClearCache()
      IconCacheVault.Clear()
      m_iconsIndexMapping.Clear()
      m_largeImageList.Images.Clear()
      m_smallImageList.Images.Clear()
    End Sub

    Public Shared Function GetIconIndex(ByVal item As FileSystemItem, ByVal selectedIcon As Boolean, ByVal forceRefresh As Boolean) As Integer
      SyncLock m_lock
        If item Is Nothing Then
          Throw New ArgumentNullException("item")
        End If

        Dim key As String = String.Empty

        ' Handles special items for which we want to override the icon.
        If Not (CType(IIf(TypeOf item Is MyComputerFolder, item, Nothing), MyComputerFolder) Is Nothing) Then
          Return IconCache.GetMyComputerIconIndex(selectedIcon)
        End If

        If (Not (CType(IIf(TypeOf item Is FtpConnectionsFolder, item, Nothing), FtpConnectionsFolder)) Is Nothing) OrElse (Not (CType(IIf(TypeOf item Is FtpConnectionFolder, item, Nothing), FtpConnectionFolder)) Is Nothing) Then
          Return IconCache.GetFtpConnectionIconIndex(selectedIcon)
        End If

        Dim extension As String = System.IO.Path.GetExtension(item.Name).ToUpper()

        If (extension = ".GZ") OrElse (extension = ".TGZ") OrElse (extension = ".TAR") OrElse (extension = ".ZIP") Then
          Return IconCache.GetZipArchiveIconIndex(selectedIcon)
        End If


        ' If we are dealing with a physical item, we try to locate the icon by it's fullname.
        If Not (CType(IIf(TypeOf item Is DiskFolder, item, Nothing), DiskFolder)) Is Nothing Then
          Return IconCache.GetDiskFolderIconIndex(item.FullName, selectedIcon, forceRefresh)
        End If

        If Not (CType(IIf(TypeOf item Is DiskFile, item, Nothing), DiskFile)) Is Nothing Then
          Return IconCache.GetDiskFileIconIndex(item.FullName, forceRefresh)
        End If

        ' An AbstractFolder will always have the default icon.
        If Not (CType(IIf(TypeOf item Is AbstractFolder, item, Nothing), AbstractFolder)) Is Nothing Then
          Return IconCache.GetFolderDefaultIcon(selectedIcon)
        End If

        ' In the case of an AbstractFile, we will try to find the icon by it's extension. We Return the
        ' default if the file have no extension or no icon is found.
        If Not (CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile)) Is Nothing Then
          Dim attributes As System.IO.FileAttributes = System.IO.FileAttributes.Normal

          If item.HasAttributes Then
            attributes = item.Attributes
          End If

          Return IconCache.GetAbstractFileIconIndex(item.FullName, attributes, forceRefresh)
        End If

        Return -1
      End SyncLock
    End Function

    Public Shared Function GetSortIconIndex(ByVal ascending As Boolean) As Integer
      If ascending Then
        Return m_defaultSortAscendingIconIndex
      End If

      Return m_defaultSortDescendingIconIndex
    End Function

    #End Region ' PUBLIC STATIC METHODS

    #Region "PRIVATE STATIC METHODS"

    Private Shared Function Add(ByVal iconKey As String, ByVal smallIcon As Icon, ByVal largeIcon As Icon, ByVal forceRefresh As Boolean) As Integer
      If smallIcon Is Nothing Then
        Throw New ArgumentNullException("smallIcon")
      End If

      If largeIcon Is Nothing Then
        Throw New ArgumentNullException("largeIcon")
      End If

      ' If we already have that key, we return the index except if we are forcing a refresh.
      If IconCache.Contains(iconKey) Then
        If (Not forceRefresh) Then
          Return IconCache.IndexOf(iconKey)
        End If

        IconCache.Remove(iconKey)
      End If

      Dim index As Integer = -1

      Dim wrapper As IconWrapper = New IconWrapper(smallIcon)

      If IconCacheVault.Contains(wrapper) Then
        index = IconCacheVault.IndexOf(wrapper)
      Else
        ' Add the icon to the image lists.
        m_smallImageList.Images.Add(smallIcon)
        m_largeImageList.Images.Add(largeIcon)

        ' Get the index of the icon.
        index = m_smallImageList.Images.Count - 1
      End If

      ' Add the index to the list.
      m_iconsIndexMapping.Add(iconKey, index)

      ' Add the index to the vault.
      If (Not IconCacheVault.Contains(wrapper)) Then
        IconCacheVault.Add(wrapper, index)
      End If

      ' Return the newly added index.
      Return index
    End Function

    Private Shared Function Contains(ByVal iconKey As String) As Boolean
      Return m_iconsIndexMapping.Contains(iconKey)
    End Function

    Private Shared Function GetAbstractFileIconIndex(ByVal fullName As String, ByVal attributes As System.IO.FileAttributes, ByVal forceRefresh As Boolean) As Integer
      ' Find the icons by extension. 
      Dim key As String = Path.GetExtension(fullName)

      If key.Length > 0 Then
        If IconCache.Contains(key) AndAlso (Not forceRefresh) Then
          Return IconCache.IndexOf(key)
        End If

        Dim smallIcon As Icon = Nothing
        Dim largeIcon As Icon = Nothing

        IconFinder.FindIconFromExtension(key, attributes, smallIcon, largeIcon)

        If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
          Return IconCache.Add(key, smallIcon, largeIcon, forceRefresh)
        End If
      End If

      Return IconCache.GetFileDefaultIcon()
    End Function

    Private Shared Function GetDiskFileIconIndex(ByVal fullName As String, ByVal forceRefresh As Boolean) As Integer
      Dim key As String = fullName

      If (Not forceRefresh) Then
        ' Check to see if an icon was previously loaded for that folder.
        If IconCache.Contains(key) Then
          Return IconCache.IndexOf(key)
        End If
      Else
        ' Get the system icon for that folder, add it to the lists and return the index.
        Dim smallIcon As Icon = Nothing
        Dim largeIcon As Icon = Nothing

        IconFinder.FindIconFromFile(fullName, False, smallIcon, largeIcon)

        If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
          Return IconCache.Add(key, smallIcon, largeIcon, forceRefresh)
        End If
      End If

      ' Return the default icon.
      Return IconCache.GetFileDefaultIcon()
    End Function

    Private Shared Function GetDiskFolderIconIndex(ByVal fullName As String, ByVal selectedIcon As Boolean, ByVal forceRefresh As Boolean) As Integer
      Dim key As String = fullName

      If selectedIcon Then
        key &= "**SEL**"
      End If

      If (Not forceRefresh) Then
        ' Check to see if an icon was previously loaded for that folder.
        If IconCache.Contains(key) Then
          Return IconCache.IndexOf(key)
        End If
      Else
        ' Get the system icon for that folder, add it to the lists and return the index.
        Dim smallIcon As Icon = Nothing
        Dim largeIcon As Icon = Nothing

        IconFinder.FindIconFromFile(fullName, selectedIcon, smallIcon, largeIcon)

        If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
          Return IconCache.Add(key, smallIcon, largeIcon, forceRefresh)
        End If
      End If

      ' Return the default icon.
      Return IconCache.GetFolderDefaultIcon(selectedIcon)
    End Function

    Private Shared Function GetFileDefaultIcon() As Integer
      Dim key As String = m_defaultFileIconKey

      If IconCache.Contains(key) Then
        Return IconCache.IndexOf(key)
      End If

      Return -1
    End Function

    Private Shared Function GetFolderDefaultIcon(ByVal selectedIcon As Boolean) As Integer
      Dim key As String = m_defaultFolderIconKey

      If selectedIcon Then
        key = m_defaultSelectedFolderIconKey
      End If

      If IconCache.Contains(key) Then
        Return IconCache.IndexOf(key)
      End If

      Return -1
    End Function

    Private Shared Function GetMyComputerIconIndex(ByVal selectedIcon As Boolean) As Integer
      Dim key As String = m_defaultMyComputerIconKey

      If IconCache.Contains(key) Then
        Return IconCache.IndexOf(key)
      End If

      Return -1
    End Function

    Private Shared Function GetSystemIconSize(ByVal small As Boolean) As Size
      Dim defaultSize As Integer
      defaultSize = (IIf(small, 16, 32))
      Dim size As Size = New Size(defaultSize, defaultSize)

      Dim flags As Integer = Win32.SHGFI_ICON Or Win32.SHGFI_SHELLICONSIZE
      If small Then
        flags = flags Or Win32.SHGFI_SMALLICON
      End If

      Dim fileInfo As Win32.SHFILEINFO = New Win32.SHFILEINFO()
      If Not IntPtr.Zero.Equals(Win32.SHGetFileInfo(Environment.SystemDirectory, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), flags)) Then
        Dim systemIcon As Icon = Icon.FromHandle(fileInfo.hIcon)
        size = systemIcon.Size
        Win32.DestroyIcon(fileInfo.hIcon)
      End If

      Return size
    End Function

    Private Shared Function GetFtpConnectionIconIndex(ByVal selectedIcon As Boolean) As Integer
      Dim key As String = m_defaultFtpIconKey

      If selectedIcon Then
        key = m_defaultSelectedFtpIconKey
      End If

      If IconCache.Contains(key) Then
        Return IconCache.IndexOf(key)
      End If

      Return -1
    End Function

    Private Shared Function GetZipArchiveIconIndex(ByVal selectedIcon As Boolean) As Integer
      Dim key As String = m_defaultZipIconKey

      If selectedIcon Then
        key = m_defaultSelectedZipIconKey
      End If

      If IconCache.Contains(key) Then
        Return IconCache.IndexOf(key)
      End If

      Return -1
    End Function

    Private Shared Function IndexOf(ByVal iconKey As String) As Integer
      Return CInt(m_iconsIndexMapping(iconKey))
    End Function

    Private Shared Sub LoadDefaultIcons()
      Dim smallIcon As Icon '= null
      Dim largeIcon As Icon '= null

      ' Ascending sort icon
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindSortIcon(True, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        m_defaultSortAscendingIconIndex = IconCache.Add(m_defaultSortAscendingIconKey, smallIcon, largeIcon, True)
      End If

      ' Descending sort icon
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindSortIcon(False, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        m_defaultSortDescendingIconIndex = IconCache.Add(m_defaultSortDescendingIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for files.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindShellIcon(ShellIcon.File, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultFileIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for folders.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindShellIcon(ShellIcon.ClosedFolder, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultFolderIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for selected folders.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindShellIcon(ShellIcon.OpenedFolder, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultSelectedFolderIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for ftp connections.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindFtpIcon(False, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultFtpIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for zip files.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindZipIcon(False, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultZipIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for selected zip files.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindZipIcon(True, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultSelectedZipIconKey, smallIcon, largeIcon, True)
      End If

      ' Default icon for My Computer.
      smallIcon = Nothing
      largeIcon = Nothing
      IconFinder.FindShellIcon(ShellIcon.MyComputer, smallIcon, largeIcon)

      If Not smallIcon Is Nothing AndAlso Not largeIcon Is Nothing Then
        IconCache.Add(m_defaultMyComputerIconKey, smallIcon, largeIcon, True)
      End If
    End Sub

    Private Shared Sub Remove(ByVal iconKey As String)
      m_iconsIndexMapping.Remove(iconKey)
    End Sub

    #End Region ' PRIVATE STATIC METHODS

    #Region "PRIVATE STATIC FIELDS"

    ''' <summary>
    ''' A list containing a mapping between icons and index in the image lists.
    ''' </summary>
    Private Shared m_iconsIndexMapping As SortedList '= null

    Private Shared m_smallImageList As ImageList '= null
    Private Shared m_largeImageList As ImageList '= null

    Private Shared m_smallIconSize As Size '= null
    Private Shared m_largeIconSize As Size '= null

    Private Shared m_defaultFtpIconKey As String = "**FTP**"
    Private Shared m_defaultSelectedFtpIconKey As String = "**SEL_FTP**"
    Private Shared m_defaultZipIconKey As String = "**ZIP**"
    Private Shared m_defaultSelectedZipIconKey As String = "**SEL_ZIP**"
    Private Shared m_defaultFolderIconKey As String = "**FOLDER**"
    Private Shared m_defaultSelectedFolderIconKey As String = "**SEL_FOLDER**"
    Private Shared m_defaultFileIconKey As String = "**FILE**"
    Private Shared m_defaultMyComputerIconKey As String = "**MYCOMPUTER**"

    Private Shared m_defaultSortAscendingIconIndex As Integer = -1
    Private Shared m_defaultSortAscendingIconKey As String = "**SORTASCENDING**"

    Private Shared m_defaultSortDescendingIconIndex As Integer = -1
    Private Shared m_defaultSortDescendingIconKey As String = "**SORTDESCENDING**"

    Private Shared m_lock As Object = New Object()

    #End Region ' PRIVATE STATIC FIELDS
  End Class
End Namespace
