/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [TreeViewIconUpdater.cs]
 * 
 * Class use to update the icons of a node's children on a seperate thread.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Threading;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
	public class TreeViewIconUpdater
	{
    #region CONSTRUCTORS

		public TreeViewIconUpdater( AbstractTreeViewNode node )
		{
      if( node == null )
        throw new ArgumentNullException( "node" );

      m_node = node;
		}

    #endregion CONSTRUCTORS

    #region PUBLIC METHODS

    /// <summary>
    /// Start refreshing icons.
    /// </summary>
    public void StartUpdate()
    {
      m_stopUpdate = false;

      ThreadPool.QueueUserWorkItem( new WaitCallback( this.UpdateTreeViewNode ) );
    }

    /// <summary>
    /// Stop the current refresh process.
    /// </summary>
    public void StopUpdate()
    {
      m_stopUpdate = true;
    }

    #endregion PUBLIC METHODS

    #region PRIVATE METHODS

    private void UpdateTreeViewNode( object stateInfo )
    {
      int nodesCount = m_node.Nodes.Count;

      for( int i = 0; i < nodesCount; i++ )
      {
        if( m_stopUpdate || m_node.Nodes.Count != nodesCount )
          break;

        try
        {
          ( m_node.Nodes[ i ] as AbstractTreeViewNode ).RefreshIcon( true );
        }
        catch {}
      }
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private AbstractTreeViewNode m_node; //= null
    private bool m_stopUpdate; //= false

    #endregion PRIVATE FIELDS
	}
}
