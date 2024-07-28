'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [Win32.vb]
 '*
 '* Contains Win32 API declaration
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.InteropServices

Namespace Xceed.FileSystem.Samples.Utils.API
  Public Class Win32
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure SHFILEINFO
      Public hIcon As IntPtr
      Public iIcon As IntPtr
      Public dwAttributes As System.UInt32
      <MarshalAs(UnmanagedType.ByValTStr, SizeConst := 260)> _
      Public szDisplayName As String
      <MarshalAs(UnmanagedType.ByValTStr, SizeConst := 80)> _
      Public szTypeName As String
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Public Structure HDITEM
      Public mask As Int32
      Public cxy As Int32
      <MarshalAs(UnmanagedType.LPTStr)> _
      Public pszText As String
      Public hbm As IntPtr
      Public cchTextMax As Int32
      Public fmt As Int32
      Public lParam As Int32
      Public iImage As Int32
      Public iOrder As Int32
    End Structure

    <DllImport("User32.dll")> _
    Public Shared Function PrivateExtractIcons(ByVal path As String, ByVal iconIndex As Integer, ByVal sizeX As Integer, ByVal sizeY As Integer, ByVal icons As IntPtr(), ByVal iconID As Integer, ByVal numberOfIconToExtract As Integer, ByVal flags As Integer) As Integer
    End Function

    <DllImport("user32.dll", EntryPoint:="DestroyIcon", SetLastError:=True)> _
    Public Shared Function DestroyIcon(ByVal hIcon As IntPtr) As Integer
    End Function

    ' Flags use by the SHGetFileInfo method
    Public Const SHGFI_ICON As Integer = &H100              ' get icon
    Public Const SHGFI_DISPLAYNAME As Integer = &H200       ' get display name
    Public Const SHGFI_TYPENAME As Integer = &H400          ' get type name
    Public Const SHGFI_ATTRIBUTES As Integer = &H800        ' get attributes
    Public Const SHGFI_ICONLOCATION As Integer = &H1000     ' get icon location
    Public Const SHGFI_EXETYPE As Integer = &H2000          ' return exe type
    Public Const SHGFI_SYSICONINDEX As Integer = &H4000     ' get system icon index
    Public Const SHGFI_LINKOVERLAY As Integer = &H8000      ' put a link overlay on icon
    Public Const SHGFI_SELECTED As Integer = &H10000        ' show icon in selected state
    Public Const SHGFI_ATTR_SPECIFIED As Integer = &H20000  ' get only specified attributes
    Public Const SHGFI_LARGEICON As Integer = &H0           ' get large icon
    Public Const SHGFI_SMALLICON As Integer = &H1           ' get small icon
    Public Const SHGFI_OPENICON As Integer = &H2            ' get open icon
    Public Const SHGFI_SHELLICONSIZE As Integer = &H4       ' get shell size icon
    Public Const SHGFI_PIDL As Integer = &H8                ' pszPath is a pidl
    Public Const SHGFI_USEFILEATTRIBUTES As Integer = &H10  ' use passed dwFileAttribute
    Public Const SHGFI_ADDOVERLAYS As Integer = &H20        ' apply the appropriate overlays
    Public Const SHGFI_OVERLAYINDEX As Integer = &H40       ' Get the index of the overlay

    <DllImport("shell32.dll")> _
    Public Shared Function SHGetFileInfo(ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbSizeFileInfo As System.UInt32, ByVal uFlags As Integer) As IntPtr
    End Function

    <DllImport("USER32.DLL", EntryPoint:="SendMessage")> _
    Public Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("USER32.DLL", EntryPoint:="SendMessage")> _
    Public Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef hdItem As HDITEM) As IntPtr
    End Function

    ' Mask values for the HDITEM structure
    Public Const HDI_WIDTH As Integer = &H1
    Public Const HDI_HEIGHT As Integer = HDI_WIDTH
    Public Const HDI_TEXT As Integer = &H2
    Public Const HDI_FORMAT As Integer = &H4
    Public Const HDI_LPARAM As Integer = &H8
    Public Const HDI_BITMAP As Integer = &H10
    Public Const HDI_IMAGE As Integer = &H20
    Public Const HDI_DI_SETITEM As Integer = &H40
    Public Const HDI_ORDER As Integer = &H80
    Public Const HDI_FILTER As Integer = &H100  ' 0x0500

    ' Format values for the HDITEM structure
    Public Const HDF_LEFT As Integer = &H0
    Public Const HDF_RIGHT As Integer = &H1
    Public Const HDF_CENTER As Integer = &H2
    Public Const HDF_JUSTIFYMASK As Integer = &H3
    Public Const HDF_RTLREADING As Integer = &H4
    Public Const HDF_OWNERDRAW As Integer = &H8000
    Public Const HDF_STRING As Integer = &H4000
    Public Const HDF_BITMAP As Integer = &H2000
    Public Const HDF_BITMAP_ON_RIGHT As Integer = &H1000
    Public Const HDF_IMAGE As Integer = &H800
    Public Const HDF_SORTUP As Integer = &H400  ' 0x0501
    Public Const HDF_SORTDOWN As Integer = &H200  ' 0x0501

    ' Contants used with SendMessage to control a ListView
    Public Const LVM_FIRST As Integer = &H1000 ' List messages
    Public Const LVM_GETHEADER As Integer = LVM_FIRST + 31
    Public Const LVM_SETSELECTEDCOLUMN As Integer = LVM_FIRST + 140

    ' Contants used with SendMessage to control a ListView's ColumnHeader
    Public Const HDM_FIRST As Integer = &H1200 ' Header messages
    Public Const HDM_SETIMAGELIST As Integer = HDM_FIRST + 8
    Public Const HDM_GETIMAGELIST As Integer = HDM_FIRST + 9
    Public Const HDM_GETITEM As Integer = HDM_FIRST + 11
    Public Const HDM_SETITEM As Integer = HDM_FIRST + 12
  End Class
End Namespace
