/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [IconFinder.cs]
 * 
 * Finds icons that are specific for the current computer.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using Xceed.FileSystem.Samples.Utils.API;

namespace Xceed.FileSystem.Samples.Utils.Icons
{
  public class IconFinder
	{
    #region PUBLIC STATIC METHODS

    public static void FindShellIcon( ShellIcon iconType, out Icon smallIcon, out Icon largeIcon )
    {
      smallIcon = null;
      largeIcon = null;

      GetIconFromFile( "shell32.dll", ( int )iconType, out smallIcon, out largeIcon );
    }

    public static void FindSortIcon( bool ascending, out Icon smallIcon, out Icon largeIcon )
    {
      string resourceName = "Xceed.FileSystem.Samples.Resources.Images.";

      if( ascending )
      {
        resourceName += "AscendingSort.ico";
      }
      else
      {
        resourceName += "DescendingSort.ico";
      }

      smallIcon = null;
      largeIcon = null;

      Icon icon = 
        new Icon( typeof( IconFinder ).Assembly.GetManifestResourceStream( resourceName ) );
        
      smallIcon = new Icon( icon, IconCache.SmallIconList.ImageSize );
      largeIcon = new Icon( icon, IconCache.LargeIconList.ImageSize );
    }

    public static void FindFtpIcon( bool selected, out Icon smallIcon, out Icon largeIcon )
    {
      string resourceName = "Xceed.FileSystem.Samples.Resources.Images.";

      if( selected )
      {
        resourceName += "OpenedFtpFolder.ico";
      }
      else
      {
        resourceName += "ClosedFtpFolder.ico";
      }

      smallIcon = null;
      largeIcon = null;

      Icon icon = 
        new Icon( typeof( IconFinder ).Assembly.GetManifestResourceStream( resourceName ) );
        
      smallIcon = new Icon( icon, IconCache.SmallIconList.ImageSize );
      largeIcon = new Icon( icon, IconCache.LargeIconList.ImageSize );
    }

    public static void FindZipIcon( bool selected, out Icon smallIcon, out Icon largeIcon )
    {
      string resourceName = "Xceed.FileSystem.Samples.Resources.Images.";

      if( selected )
      {
        resourceName += "OpenedZipFolder.ico";
      }
      else
      {
        resourceName += "ClosedZipFolder.ico";
      }

      smallIcon = null;
      largeIcon = null;

      Icon icon = 
        new Icon( typeof( IconFinder ).Assembly.GetManifestResourceStream( resourceName ) );
        
      smallIcon = new Icon( icon, IconCache.SmallIconList.ImageSize );
      largeIcon = new Icon( icon, IconCache.LargeIconList.ImageSize );
    }

    public static void FindIconFromExtension( string extension, System.IO.FileAttributes attributes, out Icon smallIcon, out Icon largeIcon )
    {
      if( extension == null )
        throw new ArgumentNullException( "extension" );

      smallIcon = null;
      largeIcon = null;

      if( extension.Length == 0 )
        return;

      if( !extension.StartsWith( "." ) )
        extension = "." + extension;

      uint flags = Win32.SHGFI_ICON|Win32.SHGFI_SHELLICONSIZE|Win32.SHGFI_USEFILEATTRIBUTES;

      Win32.SHFILEINFO fileInfo = new Win32.SHFILEINFO();
      fileInfo.dwAttributes = ( uint )attributes;

      IntPtr largeIconPtr = Win32.SHGetFileInfo( extension, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), flags );
      if( largeIconPtr != IntPtr.Zero )
      {
        largeIcon = ( Icon )Icon.FromHandle( fileInfo.hIcon ).Clone();
        Win32.DestroyIcon( fileInfo.hIcon );
      }

      flags |= Win32.SHGFI_SMALLICON;
      fileInfo = new Win32.SHFILEINFO();
      fileInfo.dwAttributes = ( uint )attributes;

      IntPtr smallIconPtr = Win32.SHGetFileInfo( extension, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), flags );
      if( smallIconPtr != IntPtr.Zero )
      {
        smallIcon = ( Icon )Icon.FromHandle( fileInfo.hIcon ).Clone();
        Win32.DestroyIcon( fileInfo.hIcon );
      }
    }

    public static void FindIconFromFile( string fullName, bool selectedIcon, out Icon smallIcon, out Icon largeIcon )
    {
      if( fullName == null )
        throw new ArgumentNullException( "fullName" );

      smallIcon = null;
      largeIcon = null;

      if( fullName.Length == 0 )
        return;

      uint flags = Win32.SHGFI_ICON|Win32.SHGFI_SHELLICONSIZE;
      if( selectedIcon )
        flags |= Win32.SHGFI_OPENICON;

      Win32.SHFILEINFO fileInfo = new Win32.SHFILEINFO();
      IntPtr largeIconPtr = Win32.SHGetFileInfo( fullName, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), flags );
      if( largeIconPtr != IntPtr.Zero )
      {
        largeIcon = ( Icon )Icon.FromHandle( fileInfo.hIcon ).Clone();
        Win32.DestroyIcon( fileInfo.hIcon );
      }

      flags |= Win32.SHGFI_SMALLICON;
      fileInfo = new Win32.SHFILEINFO();
      IntPtr smallIconPtr = Win32.SHGetFileInfo( fullName, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), flags );
      if( smallIconPtr != IntPtr.Zero )
      {
        smallIcon = ( Icon )Icon.FromHandle( fileInfo.hIcon ).Clone();
        Win32.DestroyIcon( fileInfo.hIcon );
      }
    }

    #endregion PUBLIC STATIC METHODS

    #region PRIVATE STATIC METHODS

    private static void GetIconFromFile( string file, int iconIndex, out Icon smallIcon, out Icon largeIcon )
    {
      IntPtr[] hSmallIcon = new IntPtr[ 1 ]{ IntPtr.Zero };
      IntPtr[] hLargeIcon = new IntPtr[ 1 ]{ IntPtr.Zero };

      smallIcon = null;
      largeIcon = null;

      int smallIconSize = 16;
      int largeIconSize = 32;

      Win32.SHFILEINFO fileInfo = new Win32.SHFILEINFO();
      if( Win32.SHGetFileInfo( Environment.SystemDirectory, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), Win32.SHGFI_ICON|Win32.SHGFI_SHELLICONSIZE ) != IntPtr.Zero )
      {
        Icon icon = Icon.FromHandle( fileInfo.hIcon );
        largeIconSize = icon.Width;
        Win32.DestroyIcon( fileInfo.hIcon );
      }
      fileInfo = new Win32.SHFILEINFO();
      if( Win32.SHGetFileInfo( Environment.SystemDirectory, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), Win32.SHGFI_ICON|Win32.SHGFI_SHELLICONSIZE|Win32.SHGFI_SMALLICON ) != IntPtr.Zero )
      {
        Icon icon = Icon.FromHandle( fileInfo.hIcon );
        smallIconSize = icon.Width;
        Win32.DestroyIcon( fileInfo.hIcon );
      }

      if( Win32.PrivateExtractIcons( file, iconIndex, smallIconSize, smallIconSize, hSmallIcon, 0, 1, 0 ) > 0 )
      {
        if( hSmallIcon[ 0 ] != IntPtr.Zero )
        {
          smallIcon = ( Icon )Icon.FromHandle( hSmallIcon[ 0 ] ).Clone();
          Win32.DestroyIcon( hSmallIcon[ 0 ] );
        }
      }

      if( Win32.PrivateExtractIcons( file, iconIndex, largeIconSize, largeIconSize, hLargeIcon, 0, 1, 0 ) > 0 )
      {
        if( hLargeIcon[ 0 ] != IntPtr.Zero )
        {
          largeIcon = ( Icon )Icon.FromHandle( hLargeIcon[ 0 ] ).Clone();
          Win32.DestroyIcon( hLargeIcon[ 0 ] );
        }
      }
    }

    #endregion PRIVATE STATIC METHODS
	}
}
  