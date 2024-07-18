/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FtpConnectionsTreeViewNode.cs]
 * 
 * Implementation of the AbstractTreeViewNode class and represents the Ftp connections repository.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using Xceed.FileSystem.Samples.Utils.ListView;
using Xceed.FileSystem.Samples.Utils.FileSystem;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public class FtpConnectionsTreeViewNode : AbstractTreeViewNode
  {
    #region CONSTRUCTORS

    public FtpConnectionsTreeViewNode( FtpConnectionsFolder folder )
      : this( folder, null, string.Empty )
    {
    }

    public FtpConnectionsTreeViewNode( FtpConnectionsFolder folder, System.Windows.Forms.ListView contentListView )
      : this( folder, contentListView, string.Empty )
    {
    }

    public FtpConnectionsTreeViewNode( FtpConnectionsFolder folder, System.Windows.Forms.ListView contentListView, string displayText )
    {
      if( folder == null )
        throw new ArgumentNullException( "folder" );

      m_item = folder;
      m_contentListView = contentListView;
      m_displayText = displayText;

      this.RefreshIcon( false );
      this.RefreshText();

      // Display a [+] by default
      this.Nodes.Add( string.Empty );
    
      // Initialize the icon updater.
      m_iconUpdater = new TreeViewIconUpdater( this );
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    public override AbstractFolder Folder
    {
      get
      {
        return m_item as AbstractFolder;
      }
    }

    /// <summary>
    /// Determine if the "New" function should be enabled.
    /// </summary>
    public override bool NewToolEnabled
    {
      get { return false; }
    }

    /// <summary>
    /// Determine if the "New Ftp connection" function should be enabled.
    /// </summary>
    public override bool NewFtpConnectionToolEnabled
    {
      get { return true; }
    }

    /// <summary>
    /// Determine if the "Paste" function should be enabled.
    /// </summary>
    public override bool PasteToolEnabled
    {
      get { return false; }
    }

    #endregion PUBLIC PROPERTIES
  }
}