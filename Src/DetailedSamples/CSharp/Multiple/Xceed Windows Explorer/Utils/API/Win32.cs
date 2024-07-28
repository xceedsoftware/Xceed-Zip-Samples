/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [Win32.cs]
 * 
 * Contains Win32 API declaration
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Runtime.InteropServices;

namespace Xceed.FileSystem.Samples.Utils.API
{
  public class Win32
  {
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
      public IntPtr hIcon;
      public IntPtr iIcon;
      public uint dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
      public string szTypeName;
    };

    [StructLayout(LayoutKind.Sequential)] 
    public struct HDITEM 
    { 
      public Int32     mask; 
      public Int32     cxy;   
      [MarshalAs(UnmanagedType.LPTStr)] 
      public String    pszText; 
      public IntPtr    hbm; 
      public Int32     cchTextMax; 
      public Int32     fmt; 
      public Int32     lParam; 
      public Int32     iImage; 
      public Int32     iOrder; 
    };

    [DllImport("User32.dll")]
    public static extern int PrivateExtractIcons( 
      string path, 
      int iconIndex, 
      int sizeX, 
      int sizeY, 
      IntPtr[] icons, 
      uint iconID, 
      uint numberOfIconToExtract, 
      uint flags );

    [DllImport("user32.dll", EntryPoint="DestroyIcon", SetLastError=true)]
    public static extern int DestroyIcon( IntPtr hIcon );

    // Flags use by the SHGetFileInfo method
    public const uint SHGFI_ICON				        = 0x00000100;     // get icon
    public const uint SHGFI_DISPLAYNAME			    = 0x00000200;     // get display name
    public const uint SHGFI_TYPENAME          	= 0x00000400;     // get type name
    public const uint SHGFI_ATTRIBUTES        	= 0x00000800;     // get attributes
    public const uint SHGFI_ICONLOCATION      	= 0x00001000;     // get icon location
    public const uint SHGFI_EXETYPE           	= 0x00002000;     // return exe type
    public const uint SHGFI_SYSICONINDEX      	= 0x00004000;     // get system icon index
    public const uint SHGFI_LINKOVERLAY       	= 0x00008000;     // put a link overlay on icon
    public const uint SHGFI_SELECTED          	= 0x00010000;     // show icon in selected state
    public const uint SHGFI_ATTR_SPECIFIED    	= 0x00020000;     // get only specified attributes
    public const uint SHGFI_LARGEICON         	= 0x00000000;     // get large icon
    public const uint SHGFI_SMALLICON         	= 0x00000001;     // get small icon
    public const uint SHGFI_OPENICON          	= 0x00000002;     // get open icon
    public const uint SHGFI_SHELLICONSIZE     	= 0x00000004;     // get shell size icon
    public const uint SHGFI_PIDL              	= 0x00000008;     // pszPath is a pidl
    public const uint SHGFI_USEFILEATTRIBUTES 	= 0x00000010;     // use passed dwFileAttribute
    public const uint SHGFI_ADDOVERLAYS       	= 0x00000020;     // apply the appropriate overlays
    public const uint SHGFI_OVERLAYINDEX      	= 0x00000040;     // Get the index of the overlay

    [DllImport("shell32.dll")]
    public static extern IntPtr SHGetFileInfo(
      string pszPath,
      uint dwFileAttributes,
      ref SHFILEINFO psfi,
      uint cbSizeFileInfo,
      uint uFlags);

    [DllImport("USER32.DLL", EntryPoint= "SendMessage")]
    public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);  

    [DllImport("USER32.DLL", EntryPoint= "SendMessage")]
    public static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref HDITEM hdItem);  
   
    // Mask values for the HDITEM structure
    public const int HDI_WIDTH                    = 0x0001; 
    public const int HDI_HEIGHT                   = HDI_WIDTH; 
    public const int HDI_TEXT                     = 0x0002; 
    public const int HDI_FORMAT                   = 0x0004; 
    public const int HDI_LPARAM                   = 0x0008; 
    public const int HDI_BITMAP                   = 0x0010; 
    public const int HDI_IMAGE                    = 0x0020; 
    public const int HDI_DI_SETITEM               = 0x0040; 
    public const int HDI_ORDER                    = 0x0080; 
    public const int HDI_FILTER                   = 0x0100;               // 0x0500 

    // Format values for the HDITEM structure
    public const int HDF_LEFT                     = 0x0000; 
    public const int HDF_RIGHT                    = 0x0001; 
    public const int HDF_CENTER                   = 0x0002; 
    public const int HDF_JUSTIFYMASK              = 0x0003; 
    public const int HDF_RTLREADING               = 0x0004; 
    public const int HDF_OWNERDRAW                = 0x8000; 
    public const int HDF_STRING                   = 0x4000; 
    public const int HDF_BITMAP                   = 0x2000; 
    public const int HDF_BITMAP_ON_RIGHT          = 0x1000; 
    public const int HDF_IMAGE                    = 0x0800; 
    public const int HDF_SORTUP                   = 0x0400;               // 0x0501 
    public const int HDF_SORTDOWN                 = 0x0200;               // 0x0501 

    // Contants used with SendMessage to control a ListView
    public const int LVM_FIRST                    = 0x1000;               // List messages 
    public const int LVM_GETHEADER                = LVM_FIRST + 31; 
    public const int LVM_SETSELECTEDCOLUMN        = LVM_FIRST + 140; 

    // Contants used with SendMessage to control a ListView's ColumnHeader
    public const int HDM_FIRST                    = 0x1200;               // Header messages 
    public const int HDM_SETIMAGELIST             = HDM_FIRST + 8; 
    public const int HDM_GETIMAGELIST             = HDM_FIRST + 9; 
    public const int HDM_GETITEM                  = HDM_FIRST + 11; 
    public const int HDM_SETITEM                  = HDM_FIRST + 12; 
  }
}
