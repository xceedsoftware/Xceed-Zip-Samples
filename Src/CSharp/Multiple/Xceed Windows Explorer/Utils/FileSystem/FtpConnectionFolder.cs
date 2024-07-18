/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FtpConnectionFolder.cs]
 * 
 * Custom implementation of AbstractFolder. It is contained in a 
 * FtpConnectionsFolder and represent the root of a FtpFolder.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.IO;
using Xceed.FileSystem;
using Xceed.Ftp;

namespace Xceed.FileSystem.Samples.Utils.FileSystem
{
  public class FtpConnectionFolder : AbstractFolder
  {
    #region CONSTRUCTORS

    public FtpConnectionFolder( FtpFolder ftpFolder )
    {
      if( ftpFolder == null )
        throw new ArgumentNullException( "ftpFolder" );

      m_ftpFolder = ftpFolder;
    }

    #endregion CONSTRUCTORS

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
      get 
      { 
        if( m_ftpFolder.Connection == null )
          return string.Empty;

        string name = m_ftpFolder.Connection.HostName;

        if(  ( m_ftpFolder.Connection.UserName != null )
          && ( m_ftpFolder.Connection.UserName.Length > 0 ) )
        {
          name += " (" + m_ftpFolder.Connection.UserName + ")";
        }
        else
        {
          name += " (anonymous)";
        }

        return name; 
      }
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
      get 
      { 
        if( m_ftpFolder.Connection == null )
          return string.Empty;

        string name = m_ftpFolder.Connection.HostName;

        if(  ( m_ftpFolder.Connection.UserName != null )
          && ( m_ftpFolder.Connection.UserName.Length > 0 ) )
        {
          name += " (" + m_ftpFolder.Connection.UserName + ")";
        }
        else
        {
          name += " (anonymous)";
        }

        return name; 
      }
      set 
      {
        throw new NotSupportedException(); 
      }
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
      return m_ftpFolder.GetItems( session.Events, session.UserData, false );
    }

    protected override AbstractFile DoGetFile( FileSystemEventsSession session, string fileName )
    {
      return m_ftpFolder.GetFile( session.Events, session.UserData, fileName );
    }

    protected override AbstractFile[] DoGetFiles(FileSystemEventsSession session, bool recursive, Filter[] filters)
    {
      return m_ftpFolder.GetFiles( session.Events, session.UserData, recursive, filters );
    }

    protected override AbstractFolder DoGetFolder( FileSystemEventsSession session, string folderName )
    {
      return m_ftpFolder.GetFolder( session.Events, session.UserData, folderName );
    }

    protected override AbstractFolder[] DoGetFolders(FileSystemEventsSession session, bool recursive, Filter[] filters)
    {
      return m_ftpFolder.GetFolders( session.Events, session.UserData, recursive, filters );
    }

    protected override void DoRefresh( FileSystemEventsSession session )
    {
      m_ftpFolder.Refresh();
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

    private FtpFolder m_ftpFolder; // = null

    #endregion PRIVATE FIELDS
  }
}
