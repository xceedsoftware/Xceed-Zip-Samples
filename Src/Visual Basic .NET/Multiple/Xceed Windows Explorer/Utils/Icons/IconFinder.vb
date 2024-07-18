'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [IconFinder.vb]
 '*
 '* Finds icons that are specific for the current computer.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Imports Xceed.FileSystem.Samples.Utils.API

Namespace Xceed.FileSystem.Samples.Utils.Icons
  Public Class IconFinder
    #Region "PUBLIC STATIC METHODS"

    Public Shared Sub FindShellIcon(ByVal iconType As ShellIcon, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      smallIcon = Nothing
      largeIcon = Nothing

      GetIconFromFile("shell32.dll", CInt(iconType), smallIcon, largeIcon)
    End Sub

    Public Shared Sub FindSortIcon(ByVal ascending As Boolean, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      Dim resourceName As String = String.Empty

      If ascending Then
        resourceName &= "AscendingSort.ico"
      Else
        resourceName &= "DescendingSort.ico"
      End If

      smallIcon = Nothing
      largeIcon = Nothing

      Dim icon As Icon = New Icon(GetType(IconFinder).Assembly.GetManifestResourceStream(resourceName))

      smallIcon = New Icon(icon, IconCache.SmallIconList.ImageSize)
      largeIcon = New Icon(icon, IconCache.LargeIconList.ImageSize)
    End Sub

    Public Shared Sub FindFtpIcon(ByVal selected As Boolean, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      Dim resourceName As String = String.Empty

      If selected Then
        resourceName &= "OpenedFtpFolder.ico"
      Else
        resourceName &= "ClosedFtpFolder.ico"
      End If

      smallIcon = Nothing
      largeIcon = Nothing

      Dim icon As Icon = New Icon(GetType(IconFinder).Assembly.GetManifestResourceStream(resourceName))

      smallIcon = New Icon(icon, IconCache.SmallIconList.ImageSize)
      largeIcon = New Icon(icon, IconCache.LargeIconList.ImageSize)
    End Sub

    Public Shared Sub FindZipIcon(ByVal selected As Boolean, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      Dim resourceName As String = String.Empty

      If selected Then
        resourceName &= "OpenedZipFolder.ico"
      Else
        resourceName &= "ClosedZipFolder.ico"
      End If

      smallIcon = Nothing
      largeIcon = Nothing

      Dim icon As Icon = New Icon(GetType(IconFinder).Assembly.GetManifestResourceStream(resourceName))

      smallIcon = New Icon(icon, IconCache.SmallIconList.ImageSize)
      largeIcon = New Icon(icon, IconCache.LargeIconList.ImageSize)
    End Sub

    Public Shared Sub FindIconFromExtension(ByVal extension As String, ByVal attributes As System.IO.FileAttributes, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      If extension Is Nothing Then
        Throw New ArgumentNullException("extension")
      End If

      smallIcon = Nothing
      largeIcon = Nothing

      If extension.Length = 0 Then
        Return
      End If

      If (Not extension.StartsWith(".")) Then
        extension = "." & extension
      End If

      Dim flags As Integer = Win32.SHGFI_ICON Or Win32.SHGFI_SHELLICONSIZE Or Win32.SHGFI_USEFILEATTRIBUTES

      Dim fileInfo As Win32.SHFILEINFO = New Win32.SHFILEINFO()
      fileInfo.dwAttributes = System.Convert.ToUInt32(attributes)

      Dim largeIconPtr As IntPtr = Win32.SHGetFileInfo(extension, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), flags)
      If Not largeIconPtr.Equals(IntPtr.Zero) Then
        largeIcon = CType(Icon.FromHandle(fileInfo.hIcon).Clone(), Icon)
        Win32.DestroyIcon(fileInfo.hIcon)
      End If

      flags = flags Or Win32.SHGFI_SMALLICON
      fileInfo = New Win32.SHFILEINFO()
      fileInfo.dwAttributes = System.Convert.ToUInt32(attributes)

      Dim smallIconPtr As IntPtr = Win32.SHGetFileInfo(extension, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), flags)
      If Not smallIconPtr.Equals(IntPtr.Zero) Then
        smallIcon = CType(Icon.FromHandle(fileInfo.hIcon).Clone(), Icon)
        Win32.DestroyIcon(fileInfo.hIcon)
      End If
    End Sub

    Public Shared Sub FindIconFromFile(ByVal fullName As String, ByVal selectedIcon As Boolean, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      If fullName Is Nothing Then
        Throw New ArgumentNullException("fullName")
      End If

      smallIcon = Nothing
      largeIcon = Nothing

      If fullName.Length = 0 Then
        Return
      End If

      Dim flags As Integer = Win32.SHGFI_ICON Or Win32.SHGFI_SHELLICONSIZE
      If selectedIcon Then
        flags = flags Or Win32.SHGFI_OPENICON
      End If

      Dim fileInfo As Win32.SHFILEINFO = New Win32.SHFILEINFO()
      Dim largeIconPtr As IntPtr = Win32.SHGetFileInfo(fullName, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), flags)
      If Not largeIconPtr.Equals(IntPtr.Zero) Then
        largeIcon = CType(Icon.FromHandle(fileInfo.hIcon).Clone(), Icon)
        Win32.DestroyIcon(fileInfo.hIcon)
      End If

      flags = flags Or Win32.SHGFI_SMALLICON
      fileInfo = New Win32.SHFILEINFO()
      Dim smallIconPtr As IntPtr = Win32.SHGetFileInfo(fullName, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), flags)
      If Not smallIconPtr.Equals(IntPtr.Zero) Then
        smallIcon = CType(Icon.FromHandle(fileInfo.hIcon).Clone(), Icon)
        Win32.DestroyIcon(fileInfo.hIcon)
      End If
    End Sub

    #End Region ' PUBLIC STATIC METHODS

    #Region "PRIVATE STATIC METHODS"

    Private Shared Sub GetIconFromFile(ByVal file As String, ByVal iconIndex As Integer, <System.Runtime.InteropServices.Out()> ByRef smallIcon As Icon, <System.Runtime.InteropServices.Out()> ByRef largeIcon As Icon)
      Dim hSmallIcon As IntPtr() = New IntPtr(0){ IntPtr.Zero }
      Dim hLargeIcon As IntPtr() = New IntPtr(0){ IntPtr.Zero }

      smallIcon = Nothing
      largeIcon = Nothing

      Dim smallIconSize As Integer = 16
      Dim largeIconSize As Integer = 32

      Dim fileInfo As Win32.SHFILEINFO = New Win32.SHFILEINFO()
      If Not Win32.SHGetFileInfo(Environment.SystemDirectory, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), Win32.SHGFI_ICON Or Win32.SHGFI_SHELLICONSIZE).Equals(IntPtr.Zero) Then
        Dim icon As Icon = System.Drawing.Icon.FromHandle(fileInfo.hIcon)
        largeIconSize = icon.Width
        Win32.DestroyIcon(fileInfo.hIcon)
      End If
      fileInfo = New Win32.SHFILEINFO
      If Not Win32.SHGetFileInfo(Environment.SystemDirectory, 0, fileInfo, System.Convert.ToUInt32(Marshal.SizeOf(fileInfo)), Win32.SHGFI_ICON Or Win32.SHGFI_SHELLICONSIZE Or Win32.SHGFI_SMALLICON).Equals(IntPtr.Zero) Then
        Dim icon As Icon = System.Drawing.Icon.FromHandle(fileInfo.hIcon)
        smallIconSize = icon.Width
        Win32.DestroyIcon(fileInfo.hIcon)
      End If

      If Win32.PrivateExtractIcons(file, iconIndex, smallIconSize, smallIconSize, hSmallIcon, 0, 1, 0) > 0 Then
        If Not hSmallIcon(0).Equals(IntPtr.Zero) Then
          smallIcon = CType(Icon.FromHandle(hSmallIcon(0)).Clone(), Icon)
          Win32.DestroyIcon(hSmallIcon(0))
        End If
      End If

      If Win32.PrivateExtractIcons(file, iconIndex, largeIconSize, largeIconSize, hLargeIcon, 0, 1, 0) > 0 Then
        If Not hLargeIcon(0).Equals(IntPtr.Zero) Then
          largeIcon = CType(Icon.FromHandle(hLargeIcon(0)).Clone(), Icon)
          Win32.DestroyIcon(hLargeIcon(0))
        End If
      End If
    End Sub

    #End Region ' PRIVATE STATIC METHODS
  End Class
End Namespace
