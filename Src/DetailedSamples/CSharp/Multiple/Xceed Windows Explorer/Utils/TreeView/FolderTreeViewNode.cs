/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FolderTreeViewNode.cs]
 * 
 * Implementation of the AbstractTreeViewNode class and represent an AbstractFolder.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.IO;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.FileSystem.Samples.Utils.ListView;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public class FolderTreeViewNode : AbstractTreeViewNode
  {
    #region CONSTRUSTORS

    public FolderTreeViewNode( AbstractFolder folder )
      : this( folder, null, string.Empty )
    {
    }

    public FolderTreeViewNode( AbstractFolder folder, System.Windows.Forms.ListView contentListView )
      : this( folder, contentListView, string.Empty )
    {
    }

    public FolderTreeViewNode( AbstractFolder folder, System.Windows.Forms.ListView contentListView, string displayText )
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

    #endregion CONSTRUSTORS

    #region PUBLIC PROPERTIES

    public override AbstractFolder Folder
    {
      get
      {
        return m_item as AbstractFolder;
      }
    }

    #endregion PUBLIC PROPERTIES
  }
}