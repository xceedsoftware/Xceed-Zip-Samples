/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [ListViewIconUpdater.cs]
 * 
 * Class use to update the icons in a ListView on a seperate thread.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Threading;
using System.Windows.Forms;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
	public class ListViewIconUpdater
	{
    #region CONSTRUCTORS

		public ListViewIconUpdater( System.Windows.Forms.ListView listView )
		{
      if( listView == null )
        throw new ArgumentNullException( "listView" );

      m_listView = listView;
		}

    #endregion CONSTRUCTORS

    #region PUBLIC METHODS

    /// <summary>
    /// Start refreshing icons.
    /// </summary>
    public void StartUpdate()
    {
      m_stopUpdate = false;

      ThreadPool.QueueUserWorkItem( new WaitCallback( this.UpdateListView ) );
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

    private void UpdateListView( object stateInfo )
    {
      int itemsCount = m_listView.Items.Count;

      for( int i = 0; i < itemsCount; i++ )
      {
        if( m_stopUpdate || m_listView.Items.Count != itemsCount )
          break;

        try
        {
          ( m_listView.Items[ i ] as AbstractListViewItem ).RefreshIcon( true );
        }
        catch {}
      }
    }

    #endregion PRIVATE METHODS

    #region PRIVATE FIELDS

    private System.Windows.Forms.ListView m_listView; //= null
    private bool m_stopUpdate; //= false

    #endregion PRIVATE FIELDS
	}
}
