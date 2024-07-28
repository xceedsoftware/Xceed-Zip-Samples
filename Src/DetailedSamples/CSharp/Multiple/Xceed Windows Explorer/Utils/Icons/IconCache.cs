/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [IconCache.cs]
 * 
 * This class expose 2 static ImageList containing all the images that 
 * have been requested so far. It allows to retreive icons for a 
 * specified FileSystemItem.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.FileSystem.Samples.Utils.API;
using Xceed.FileSystem.Samples.Utils.FileSystem;
using Xceed.FileSystem.Samples.Utils.Icons;
using Xceed.Zip;
using Xceed.Ftp;
using Xceed.Tar;
using Xceed.GZip;

namespace Xceed.FileSystem.Samples.Utils.Icons
{
  public class IconCache
  {
    #region CONSTRUCTORS

    static IconCache()
    {
      // Initialize the objects.
      m_iconsIndexMapping = new SortedList();
      m_smallImageList = new ImageList();
      m_largeImageList = new ImageList();

      // Get the system sizes.
      m_smallIconSize = GetSystemIconSize( true );
      m_largeIconSize = GetSystemIconSize( false );

      // Set the image lists properties.
      m_smallImageList.ColorDepth = ColorDepth.Depth32Bit;
      m_smallImageList.ImageSize = m_smallIconSize;
      m_smallImageList.TransparentColor = Color.Transparent;

      m_largeImageList.ColorDepth = ColorDepth.Depth32Bit;
      m_largeImageList.ImageSize = m_largeIconSize;
      m_largeImageList.TransparentColor = Color.Transparent;

      // Loads the default icons.
      IconCache.LoadDefaultIcons();
    }

    #endregion CONSTRUCTORS

    #region PUBLIC STATIC PROPERTIES

    public static ImageList LargeIconList
    {
      get{ return m_largeImageList; }
    }

    public static ImageList SmallIconList
    {
      get{ return m_smallImageList; }
    }

    #endregion PUBLIC STATIC PROPERTIES

    #region PUBLIC STATIC METHODS

    public static void ClearCache()
    {
      IconCacheVault.Clear();
      m_iconsIndexMapping.Clear();
      m_largeImageList.Images.Clear();
      m_smallImageList.Images.Clear();
    }

    public static int GetIconIndex( FileSystemItem item, bool selectedIcon, bool forceRefresh )
    {
      lock( m_lock )
      {
        if( item == null )
          throw new ArgumentNullException( "item" );

        string key = string.Empty;

        // Handles special items for which we want to override the icon.
        if( item as MyComputerFolder != null )
          return IconCache.GetMyComputerIconIndex( selectedIcon );

        if(  ( ( item as FtpConnectionsFolder ) != null )
          || ( ( item as FtpConnectionFolder ) != null ) )
        {
          return IconCache.GetFtpConnectionIconIndex( selectedIcon );
        }

        string extension = System.IO.Path.GetExtension( item.Name ).ToUpper();

        if(  ( extension == ".GZ" )
          || ( extension == ".TGZ" )
          || ( extension == ".TAR" )
          || ( extension == ".ZIP" ) )
        {
          return IconCache.GetZipArchiveIconIndex( selectedIcon );
        }


        // If we are dealing with a physical item, we try to locate the icon by it's fullname.
        if( ( item as DiskFolder ) != null )
          return IconCache.GetDiskFolderIconIndex( item.FullName, selectedIcon, forceRefresh );

        if( ( item as DiskFile ) != null )
          return IconCache.GetDiskFileIconIndex( item.FullName, forceRefresh );

        // An AbstractFolder will always have the default icon.
        if( ( item as AbstractFolder ) != null )
          return IconCache.GetFolderDefaultIcon( selectedIcon );

        // In the case of an AbstractFile, we will try to find the icon by it's extension. We Return the
        // default if the file have no extension or no icon is found.
        if( ( item as AbstractFile ) != null )
        {
          System.IO.FileAttributes attributes = System.IO.FileAttributes.Normal;

          if( item.HasAttributes )
            attributes = item.Attributes;

          return IconCache.GetAbstractFileIconIndex( item.FullName, attributes, forceRefresh );
        }

        return -1;
      }
    }

    public static int GetSortIconIndex( bool ascending )
    {
      if( ascending )
        return m_defaultSortAscendingIconIndex;

      return m_defaultSortDescendingIconIndex;
    }

    #endregion PUBLIC STATIC METHODS

    #region PRIVATE STATIC METHODS

    private static int Add( string iconKey, Icon smallIcon, Icon largeIcon, bool forceRefresh )
    {
      if( smallIcon == null )
        throw new ArgumentNullException( "smallIcon" );
      
      if( largeIcon == null )
        throw new ArgumentNullException( "largeIcon" );

      // If we already have that key, we return the index except if we are forcing a refresh.
      if( IconCache.Contains( iconKey ) )
      {
        if( !forceRefresh )
          return IconCache.IndexOf( iconKey );

        IconCache.Remove( iconKey );
      }

      int index = -1;

      IconWrapper wrapper = new IconWrapper( smallIcon );

      if( IconCacheVault.Contains( wrapper ) )
      {
        index = IconCacheVault.IndexOf( wrapper );
      }
      else
      {
        // Add the icon to the image lists.
        m_smallImageList.Images.Add( smallIcon );
        m_largeImageList.Images.Add( largeIcon );

        // Get the index of the icon.
        index = m_smallImageList.Images.Count - 1;
      }

      // Add the index to the list.
      m_iconsIndexMapping.Add( iconKey, index );

      // Add the index to the vault.
      if( !IconCacheVault.Contains( wrapper ) )
        IconCacheVault.Add( wrapper, index );

      // Return the newly added index.
      return index;
    }

    private static bool Contains( string iconKey )
    {
      return m_iconsIndexMapping.Contains( iconKey );
    }

    private static int GetAbstractFileIconIndex( string fullName, System.IO.FileAttributes attributes, bool forceRefresh )
    {
      // Find the icons by extension. 
      string key = Path.GetExtension( fullName );

      if( key.Length > 0 )
      {
        if( IconCache.Contains( key ) && !forceRefresh )
          return IconCache.IndexOf( key );

        Icon smallIcon = null;
        Icon largeIcon = null;

        IconFinder.FindIconFromExtension( key, attributes, out smallIcon, out largeIcon );

        if( smallIcon != null && largeIcon != null )
          return IconCache.Add( key, smallIcon, largeIcon, forceRefresh );
      }

      return IconCache.GetFileDefaultIcon();
    }

    private static int GetDiskFileIconIndex( string fullName, bool forceRefresh )
    {
      string key = fullName;

      if( !forceRefresh )
      {
        // Check to see if an icon was previously loaded for that folder.
        if( IconCache.Contains( key ) )
          return IconCache.IndexOf( key );
      }
      else
      {
        // Get the system icon for that folder, add it to the lists and return the index.
        Icon smallIcon = null;
        Icon largeIcon = null;

        IconFinder.FindIconFromFile( fullName, false, out smallIcon, out largeIcon );

        if( smallIcon != null && largeIcon != null )
          return IconCache.Add( key, smallIcon, largeIcon, forceRefresh );
      }

      // Return the default icon.
      return IconCache.GetFileDefaultIcon();
    }

    private static int GetDiskFolderIconIndex( string fullName, bool selectedIcon, bool forceRefresh )
    {
      string key = fullName;

      if( selectedIcon )
        key += "**SEL**";

      if( !forceRefresh )
      {
        // Check to see if an icon was previously loaded for that folder.
        if( IconCache.Contains( key ) )
          return IconCache.IndexOf( key );
      }
      else
      {
        // Get the system icon for that folder, add it to the lists and return the index.
        Icon smallIcon = null;
        Icon largeIcon = null;

        IconFinder.FindIconFromFile( fullName, selectedIcon, out smallIcon, out largeIcon );

        if( smallIcon != null && largeIcon != null )
          return IconCache.Add( key, smallIcon, largeIcon, forceRefresh );
      }

      // Return the default icon.
      return IconCache.GetFolderDefaultIcon( selectedIcon );
    }

    private static int GetFileDefaultIcon()
    {
      string key = m_defaultFileIconKey;

      if( IconCache.Contains( key ) )
        return IconCache.IndexOf( key );

      return -1;
    }

    private static int GetFolderDefaultIcon( bool selectedIcon )
    {
      string key = m_defaultFolderIconKey;

      if( selectedIcon )
        key = m_defaultSelectedFolderIconKey;

      if( IconCache.Contains( key ) )
        return IconCache.IndexOf( key );

      return -1;
    }

    private static int GetMyComputerIconIndex( bool selectedIcon )
    {
      string key = m_defaultMyComputerIconKey;

      if( IconCache.Contains( key ) )
        return IconCache.IndexOf( key );

      return -1;
    }

    private static Size GetSystemIconSize( bool small )
    {
      int defaultSize = ( small ? 16 : 32 );
      Size size = new Size( defaultSize, defaultSize );

      uint flags = Win32.SHGFI_ICON|Win32.SHGFI_SHELLICONSIZE;
      if( small )
        flags |= Win32.SHGFI_SMALLICON;

      Win32.SHFILEINFO fileInfo = new Win32.SHFILEINFO();
      if( IntPtr.Zero != Win32.SHGetFileInfo( Environment.SystemDirectory, 0, ref fileInfo, ( uint )Marshal.SizeOf( fileInfo ), flags ) )
      {
        Icon systemIcon = Icon.FromHandle( fileInfo.hIcon );
        size = systemIcon.Size;
        Win32.DestroyIcon( fileInfo.hIcon );
      }

      return size;
    }

    private static int GetFtpConnectionIconIndex( bool selectedIcon )
    {
      string key = m_defaultFtpIconKey;

      if( selectedIcon )
        key = m_defaultSelectedFtpIconKey;

      if( IconCache.Contains( key ) )
        return IconCache.IndexOf( key );

      return -1;
    }

    private static int GetZipArchiveIconIndex( bool selectedIcon )
    {
      string key = m_defaultZipIconKey;

      if( selectedIcon )
        key = m_defaultSelectedZipIconKey;

      if( IconCache.Contains( key ) )
        return IconCache.IndexOf( key );

      return -1;
    }

    private static int IndexOf( string iconKey )
    {
      return ( int )m_iconsIndexMapping[ iconKey ];
    }

    private static void LoadDefaultIcons()
    {
      Icon smallIcon; //= null
      Icon largeIcon; //= null

      // Ascending sort icon
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindSortIcon( true, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        m_defaultSortAscendingIconIndex = IconCache.Add( m_defaultSortAscendingIconKey, smallIcon, largeIcon, true );

      // Descending sort icon
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindSortIcon( false, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        m_defaultSortDescendingIconIndex = IconCache.Add( m_defaultSortDescendingIconKey, smallIcon, largeIcon, true );

      // Default icon for files.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindShellIcon( ShellIcon.File, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultFileIconKey, smallIcon, largeIcon, true );

      // Default icon for folders.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindShellIcon( ShellIcon.ClosedFolder, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultFolderIconKey, smallIcon, largeIcon, true );

      // Default icon for selected folders.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindShellIcon( ShellIcon.OpenedFolder, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultSelectedFolderIconKey, smallIcon, largeIcon, true );

      // Default icon for ftp connections.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindFtpIcon( false, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultFtpIconKey, smallIcon, largeIcon, true );

      // Default icon for zip files.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindZipIcon( false, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultZipIconKey, smallIcon, largeIcon, true );

      // Default icon for selected zip files.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindZipIcon( true, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultSelectedZipIconKey, smallIcon, largeIcon, true );

      // Default icon for My Computer.
      smallIcon = null;
      largeIcon = null;
      IconFinder.FindShellIcon( ShellIcon.MyComputer, out smallIcon, out largeIcon );

      if( smallIcon != null && largeIcon != null )
        IconCache.Add( m_defaultMyComputerIconKey, smallIcon, largeIcon, true );
    }

    private static void Remove( string iconKey )
    {
      m_iconsIndexMapping.Remove( iconKey );
    }

    #endregion PRIVATE STATIC METHODS

    #region PRIVATE STATIC FIELDS

    /// <summary>
    /// A list containing a mapping between icons and index in the image lists.
    /// </summary>
    private static SortedList m_iconsIndexMapping; //= null

    private static ImageList m_smallImageList; //= null
    private static ImageList m_largeImageList; //= null

    private static Size m_smallIconSize; //= null
    private static Size m_largeIconSize; //= null

    private static string m_defaultFtpIconKey = "**FTP**";
    private static string m_defaultSelectedFtpIconKey = "**SEL_FTP**";
    private static string m_defaultZipIconKey = "**ZIP**";
    private static string m_defaultSelectedZipIconKey = "**SEL_ZIP**";
    private static string m_defaultFolderIconKey = "**FOLDER**";
    private static string m_defaultSelectedFolderIconKey = "**SEL_FOLDER**";
    private static string m_defaultFileIconKey = "**FILE**";
    private static string m_defaultMyComputerIconKey = "**MYCOMPUTER**";

    private static int m_defaultSortAscendingIconIndex = -1;
    private static string m_defaultSortAscendingIconKey = "**SORTASCENDING**";

    private static int m_defaultSortDescendingIconIndex = -1;
    private static string m_defaultSortDescendingIconKey = "**SORTDESCENDING**";

    private static object m_lock = new object();

    #endregion PRIVATE STATIC FIELDS
  }
}
