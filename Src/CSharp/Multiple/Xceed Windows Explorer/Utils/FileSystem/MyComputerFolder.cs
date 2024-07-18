/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [MyComputerFolder.cs]
 * 
 * Custom implementation of AbstractFolder to represent the "MyComputer" 
 * folder. It basically just returns the list of drives on the machine.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.IO;
using Xceed.FileSystem;

namespace Xceed.FileSystem.Samples.Utils.FileSystem
{
  /// <summary>
  /// This class simulate the MyComputer on Windows System. It only expose
  /// all the drives on the machine as child items. 
  /// </summary>
	public class MyComputerFolder : AbstractFolder
	{
    #region CONSTRUCTORS

		public MyComputerFolder()
		{
      this.Refresh();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    protected override bool DoHasAttributes
    {
      get{ return false; }
    }

    protected override System.IO.FileAttributes DoAttributes
    {
      get{ throw new NotSupportedException(); }
      set{ throw new NotSupportedException(); }
    }

    protected override bool DoHasCreationDateTime
    {
      get{ return false; }
    }

    protected override DateTime DoCreationDateTime
    {
      get{ throw new NotSupportedException(); }
      set{ throw new NotSupportedException(); }
    }

    protected override bool DoExists
    {
      get{ return true; }
    }

    protected override string DoFullName
    {
      get{ return m_fullName; }
    }

    protected override bool DoHasLastAccessDateTime
    {
      get{ return false; }
    }

    protected override DateTime DoLastAccessDateTime
    {
      get{ throw new NotSupportedException(); }
      set{ throw new NotSupportedException(); }
    }

    protected override bool DoHasLastWriteDateTime
    {
      get{ return false; }
    }

    protected override DateTime DoLastWriteDateTime
    {
      get{ throw new NotSupportedException(); }
      set{ throw new NotSupportedException(); }
    }

    protected override bool DoIsRoot
    {
      get{ return true; }
    }

    protected override string DoName
    {
      get{ return m_fullName; }
      set{}
    }

    protected override AbstractFolder DoParentFolder
    {
      get{ return null; }
    }

    protected override AbstractFolder DoRootFolder
    {
      get{ return null; }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    protected override void DoCopyTo( FileSystemEventsSession session, FileSystemItem destination, bool replaceExistingFiles )
    {
      throw new NotSupportedException();
    }

    protected override void DoCreate( FileSystemEventsSession session )
    {
      throw new NotSupportedException();
    }

    protected override void DoDelete( FileSystemEventsSession session )
    {
      throw new NotSupportedException();
    }

    protected override FileSystemItem[] DoGetChildItems(FileSystemEventsSession session)
    {
      ArrayList array = new ArrayList( m_children.Count );

      foreach( object folder in m_children )
      {
        // All drives are DiskFolders.
        array.Add( new DiskFolder( folder.ToString() ) );
      }

      return ( FileSystemItem[] )array.ToArray( typeof( FileSystemItem ) );
    }

    protected override AbstractFile DoGetFile( FileSystemEventsSession session, string fileName )
    {
      throw new NotSupportedException();
    }

    protected override AbstractFolder DoGetFolder( FileSystemEventsSession session, string folderName )
    {
      if( m_children.Contains( folderName ) )
        return new DiskFolder( folderName );

      return null;
    }

    protected override void DoRefresh( FileSystemEventsSession session )
    {
      // Clear the actual list.
      m_children.Clear();

      try
      {
        // Get the list of drives on the local machine and add them
        // to the children list.
        string[] drives = Environment.GetLogicalDrives();

        foreach( string drive in drives )
        {
          m_children.Add( drive );
        }
      }
      catch{}
    }

    protected override bool IsPathRooted( string path )
    {
      return false;
    }

    public override bool IsSameAs( FileSystemItem target )
    {
      if( target == null )
        throw new ArgumentNullException( "target" );

      AbstractFolder folder = target as AbstractFolder;

      if( folder == null )
        return false;

      return ( folder.FullName == this.FullName );
    }

    #endregion PUBLIC METHODS

    #region PRIVATE FIELDS

    private const string m_fullName = "My Computer";
    private ArrayList m_children = new ArrayList();

    #endregion PRIVATE FIELDS
	}
}
