/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [MyComputerTreeViewNode.cs]
 * 
 * Implementation of the AbstractTreeViewNode class and represent the My Computer
 * root folder.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using Xceed.FileSystem.Samples.Utils.ListView;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public class MyComputerTreeViewNode : AbstractTreeViewNode
  {
    #region CONSTRUCTORS

    public MyComputerTreeViewNode( AbstractFolder folder )
      : this( folder, null, string.Empty )
    {
    }

    public MyComputerTreeViewNode( AbstractFolder folder, System.Windows.Forms.ListView contentListView )
      : this( folder, contentListView, string.Empty )
    {
    }

    public MyComputerTreeViewNode( AbstractFolder folder, System.Windows.Forms.ListView contentListView, string displayText )
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

    public override bool NewToolEnabled
    {
      // Cannot create new items in the MyComputer folder.
      get { return false; }
    }

    /// <summary>
    /// Determine if the "Paste" function should be enabled.
    /// </summary>
    public override bool PasteToolEnabled
    {
      get { return false; }
    }

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