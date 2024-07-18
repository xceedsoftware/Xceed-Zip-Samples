/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FtpConnectionsFolder.cs]
 * 
 * Custom implementation of AbstractFolder. It contains a list of 
 * FtpConnectionFolder and is used to manage Ftp connections in the 
 * FolderTree.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using Xceed.FileSystem;
using Xceed.Ftp;

namespace Xceed.FileSystem.Samples.Utils.FileSystem
{
  public class FtpConnectionsFolder : AbstractFolder
  {
    #region PROTECTED PROPERTIES

    protected override bool DoHasAttributes
    {
      get { return false; }
    }

    protected override System.IO.FileAttributes DoAttributes
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }

    protected override bool DoHasCreationDateTime
    {
      get { return false; }
    }

    protected override DateTime DoCreationDateTime
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }

    protected override bool DoExists
    {
      get { return true; }
    }

    protected override string DoFullName
    {
      get { return m_fullName; }
    }

    protected override bool DoHasLastAccessDateTime
    {
      get { return false; }
    }

    protected override DateTime DoLastAccessDateTime
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }

    protected override bool DoHasLastWriteDateTime
    {
      get { return false; }
    }

    protected override DateTime DoLastWriteDateTime
    {
      get { throw new NotSupportedException(); }
      set { throw new NotSupportedException(); }
    }

    protected override bool DoIsRoot
    {
      get { return true; }
    }

    protected override string DoName
    {
      get { return m_fullName; }
      set { throw new NotSupportedException(); }
    }

    protected override AbstractFolder DoParentFolder
    {
      get { return this; }
    }

    protected override AbstractFolder DoRootFolder
    {
      get { return this; }
    }

    #endregion PROTECTED PROPERTIES

    #region PUBLIC METHODS

    public FtpConnectionFolder CreateConnectionFolder( FtpConnection connection )
    {
      FtpConnectionFolder folder = new FtpConnectionFolder( new FtpFolder( connection ) );

      m_children.Add( folder );

      return folder;
    }

    #endregion PUBLIC METHODS

    #region PROTECTED METHODS

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

    protected override FileSystemItem[] DoGetChildItems( FileSystemEventsSession session )
    {
      return ( FileSystemItem[] )m_children.ToArray( typeof( FileSystemItem ) );
    }

    protected override AbstractFile DoGetFile( FileSystemEventsSession session, string fileName )
    {
      throw new NotSupportedException();
    }

    protected override AbstractFolder DoGetFolder( FileSystemEventsSession session, string folderName )
    {
      throw new NotSupportedException();
    }

    protected override void DoRefresh( FileSystemEventsSession session )
    {
      // Nothing to do.
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

    #endregion PROTECTED METHODS

    #region PRIVATE FIELDS

    private string m_fullName = "Ftp Connections";
    private ArrayList m_children = new ArrayList();

    #endregion PRIVATE FIELDS
  }
}
