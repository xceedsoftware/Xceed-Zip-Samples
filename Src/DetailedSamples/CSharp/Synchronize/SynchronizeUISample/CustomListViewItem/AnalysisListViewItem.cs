/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AnalsisListViewItem.cs]
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
using System.Text;
using System.Windows.Forms;
using Xceed.Synchronization;

namespace SynchronizeUISample
{
  /// <summary>
  /// Defines a ListViewItem to display informations about AnalysisSynchronizationEventhArgs
  /// </summary>
  public class AnalysisListViewItem : ListViewItem
  {
    #region PUBLIC CONSTRUCTORS

    ///// <summary>
    ///// Constructor
    ///// </summary>
    ///// <param name="masterItemPath">The path of the master item</param>
    ///// <param name="action">The action to process for synchronization</param>
    ///// <param name="itemPath">The path of the target item</param>
    ///// <param name="selected">Defines if the item is selected or not</param>
    //public AnalysisListViewItem( string masterItemPath, string action, string itemPath, bool selected )
    //{
    //  this.Text = masterItemPath;
    //  this.SubItems.Add( action );
    //  this.SubItems.Add( itemPath );
    //  this.Checked = selected;
    //}

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="masterItemPath">The path of the master item</param>
    /// <param name="action">The action to process for synchronization</param>
    /// <param name="itemPath">The path of the target item</param>
    /// <param name="selected">Defines if the item is selected or not</param>
    public AnalysisListViewItem( string masterItemPath, SynchronizationAction action, string itemPath, bool selected )
    {
      this.Checked = selected;

      // Get action for current item
      string sourceItem = string.Empty;
      string actionItem = string.Empty;

      switch( action )
      {
        case SynchronizationAction.SuspendedCreateOrOverwrite:
          sourceItem = masterItemPath;
          actionItem = "should be used to create";
          break;
        case SynchronizationAction.OverwriteAttributesOnly:
          sourceItem = masterItemPath;
          actionItem = "used to overwrite attributes";
          break;
        case SynchronizationAction.CreateOrOverwriteWithMaster:
          sourceItem = masterItemPath;
          actionItem = "will replace";
          break;
        case SynchronizationAction.Delete:
          actionItem = "delete";
          break;
        case SynchronizationAction.SuspendedDelete:
          actionItem = "should delete";
          break;
      }

      this.Text = sourceItem;
      this.SubItems.Add( actionItem );
      this.SubItems.Add( itemPath );
    }

    #endregion
  }
}
