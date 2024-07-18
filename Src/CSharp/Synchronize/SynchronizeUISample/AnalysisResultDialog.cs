/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AnalysisResultDialog.cs]
 * 
 * This application demonstrate how to use the Xceed Synchronize
 * functionnality.
 * 
 * This file is part of Xceed FileSystem for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Xceed.Synchronization;

namespace SynchronizeUISample
{
  /// <summary>
  /// Class defining a Dialog displaying the actions to be
  /// taken on synchronized items. This dialog allows approving
  /// or not actions to be performed by synchronization.
  /// </summary>
  public partial class AnalysisResultDialog : Form
  {
    #region PUBLIC CONSTRUCTORS

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="synchronizationAnalysisEventArgs">The SynchronizationAnalysisEventArgs containing 
    /// informations about the current synchronization process</param>
    public AnalysisResultDialog( SynchronizationAnalysisEventArgs synchronizationAnalysisEventArgs )
    {
      InitializeComponent();
      AnalyseSynchronizationEventArgs( synchronizationAnalysisEventArgs );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="synchronizationFolderAnalysisEventArgs">The SynchronizationFolderAnalysisEventArgs containing 
    /// informations about the current synchronization process</param>
    public AnalysisResultDialog( SynchronizationFolderAnalysisEventArgs synchronizationFolderAnalysisEventArgs )
    {
      InitializeComponent();
      AnalyseSynchronizationFolderAnalysisEventArgs( synchronizationFolderAnalysisEventArgs );
    }

    #endregion

    #region PUBLIC FIELDS

    /// <summary>
    /// A bool array defining if action at the 
    /// specified index must be taken or not
    /// </summary>
    public bool[] ApprovedActions
    {
      get
      {
        return m_approvedActions;
      }
      set
      {
        m_approvedActions = value;
      }
    }

    /// <summary>
    /// Defines if all the future synchronization analysis should be accepted without
    /// displaying the AnalysisResultDialog
    /// </summary>
    public bool ApproveAll
    {
      get
      {
        return m_sameActionForAll;
      }
      set
      {
        m_sameActionForAll = value;
      }
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Fill the AnalysisListView with items synchronize
    /// displaying: 
    /// -the source item (if needed)
    /// -the action to perform
    /// -the destination item
    /// </summary>
    private void AnalyseSynchronizationEventArgs( SynchronizationAnalysisEventArgs synchronizationAnalysisEventArgs )
    {
      if( synchronizationAnalysisEventArgs == null )
      {
        throw new Exception( "SynchronizationEventArgs must exist" );
      }

      // Get master item index
      int masterIndex = synchronizationAnalysisEventArgs.MasterFileIndex;

      for( int i = 0; i < synchronizationAnalysisEventArgs.Files.Length; i++ )
      {
        // Do not consider master file in action
        if( i == synchronizationAnalysisEventArgs.MasterFileIndex )
          continue;

        // Add every item to synchronize
        ListViewItem insertedItem = this.AnalysisListView.Items.Add(
          new AnalysisListViewItem(
            synchronizationAnalysisEventArgs.Files[ masterIndex ].FullName,
            synchronizationAnalysisEventArgs.Actions[ i ],
            synchronizationAnalysisEventArgs.Files[ i ].FullName,
            true ) );
      }
    }

    /// <summary>
    /// Fill the AnalysisListView with items synchronize
    /// displaying: 
    /// -the source item (if needed)
    /// -the action to perform
    /// -the destination item
    /// </summary>
    private void AnalyseSynchronizationFolderAnalysisEventArgs( SynchronizationFolderAnalysisEventArgs synchronizationFolderAnalysisEventArgs )
    {
      if( synchronizationFolderAnalysisEventArgs == null )
      {
        throw new Exception( "SynchronizationFolderAnalysisEventArgs must exist" );
      }

      // Get master item index
      int masterIndex = synchronizationFolderAnalysisEventArgs.MasterFolderIndex;

      for( int i = 0; i < synchronizationFolderAnalysisEventArgs.Folders.Length; i++ )
      {
        // Do not consider master file in action
        if( i == synchronizationFolderAnalysisEventArgs.MasterFolderIndex)
          continue;

        // Add every item to synchronize
        ListViewItem insertedItem = this.AnalysisListView.Items.Add(
          new AnalysisListViewItem(
            synchronizationFolderAnalysisEventArgs.Folders[ masterIndex ].FullName,
            synchronizationFolderAnalysisEventArgs.Actions[ i ],
            synchronizationFolderAnalysisEventArgs.Folders[ i ].FullName,
            true ) );
      }
    }

    #endregion

    #region PRIVATE EVENT HANDLERS

    /// <summary>
    /// Event handler for OkButton
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OkButton_Click( object sender, EventArgs e )
    {
      FillApprovedActionsArray();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    /// <summary>
    /// Set true every index selected in AnalysisListView in ApprovedActions array
    /// </summary>
    private void FillApprovedActionsArray()
    {
      ListView.CheckedIndexCollection checkedIndexCollection = this.AnalysisListView.CheckedIndices;

      this.m_approvedActions = new bool[ this.AnalysisListView.Items.Count ];
      foreach( int index in checkedIndexCollection )
      {
        this.m_approvedActions[ index ] = true;
      }
    }

    /// <summary>
    /// Event handler for ApproveAllCheckBox. This changes the m_sameActionForAll private
    /// field to avoid to be notified of future analysis events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ApproveAllCheckBox_CheckedChanged( object sender, EventArgs e )
    {
      m_sameActionForAll = this.AllSameActionCheckBox.Checked;
    }

    #endregion

    #region PRIVATE FIELDS

    private bool[] m_approvedActions;
    private bool m_sameActionForAll = false;

    #endregion
  }
}