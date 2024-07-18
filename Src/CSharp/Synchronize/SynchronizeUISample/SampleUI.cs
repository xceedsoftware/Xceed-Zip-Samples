/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [SampleUI.cs]
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
using Xceed.FileSystem;
using SynchronizeUISample.CustomListViewItem;
using System.IO;
using Xceed.Synchronization;

namespace SynchronizeUISample
{
  /// <summary>
  /// Class defining the Main Form of the sample
  /// </summary>
  public partial class SampleUI : Form
  {
    #region PUBLIC CONSTRUCTORS

    public SampleUI()
    {
      this.InitializeComponent();
      this.InitializeSynchronizationEvents();
      this.InitializeSynchronizationOptions();
    }

    #endregion

    #region PRIVATE EVENT HANDLERS - BUTTONS

    /// <summary>
    /// Event handler for AddFileButton. Add a target item
    /// in the SlaveItemsListView if the file path is valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAddFileButton_Click( object sender, EventArgs e )
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = true;
      openFileDialog.Title = "Select file(s) to process";
      openFileDialog.CheckFileExists = false;
      openFileDialog.CheckPathExists = false;

      DialogResult result = openFileDialog.ShowDialog();

      if( result == DialogResult.OK )
      {
        string[] selectedFilePaths = openFileDialog.FileNames;

        foreach( string filePath in selectedFilePaths )
        {
          DiskFile file = new DiskFile( filePath );

          this.SlaveItemsListView.Items.Add( new FileListViewItem( filePath ) );
        }
      }
    }

    /// <summary>
    /// Event handler for AddFolderButton. Add a target item
    /// in the SlaveItemsListView if the folder path is valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAddFolderButton_Click( object sender, EventArgs e )
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.Description = "Select folder to use as synchronization target";
      folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

      DialogResult result = folderBrowserDialog.ShowDialog();

      if( result == DialogResult.OK )
      {
        string selectedFolderPath = folderBrowserDialog.SelectedPath;

        DiskFolder folder = new DiskFolder( selectedFolderPath );
        if( folder.Exists )
        {
          if( !selectedFolderPath.EndsWith( Path.DirectorySeparatorChar.ToString() ) )
          {
            selectedFolderPath += Path.DirectorySeparatorChar.ToString();
          }

          this.SlaveItemsListView.Items.Add( new FolderListViewItem( selectedFolderPath ) );
        }
        else
        {
          MessageBox.Show( "Folder " + selectedFolderPath + " does not exist" );
        }
      }
    }

    /// <summary>
    /// Event handler for RemoveButton. Remove the selected
    /// item from the SlaveItemsListView if the selection is
    /// valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnRemoveButton_Click( object sender, EventArgs e )
    {
      System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = this.SlaveItemsListView.SelectedItems;

      foreach( ListViewItem item in selectedItems )
      {
        this.SlaveItemsListView.Items.Remove( item );
      }
    }

    /// <summary>
    /// Event handler for BrowseMasterFileButton. Set the selected
    /// file path as the master item event if the file doesn't exist.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnBrowseMasterFileButton_Click( object sender, EventArgs e )
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = false;
      openFileDialog.CheckFileExists = false;
      openFileDialog.CheckPathExists = false;
      openFileDialog.Title = "Select master file";

      DialogResult result = openFileDialog.ShowDialog();

      if( result == DialogResult.OK )
      {
        // The master file may not exist. If so, it will be deleted in every target items
        this.MasterItemTextBox.Text = openFileDialog.FileName;
        this.m_masterItemType = MasterItemType.File;
      }
    }

    /// <summary>
    /// Event handler for BrowseMasterFolderButton. Set the selected
    /// folder path as the master item. The folder must exist.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnBrowseMasterFolderButton_Click( object sender, EventArgs e )
    {
      FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
      folderBrowserDialog.Description = "Select folder to use as master folder";
      folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

      DialogResult result = folderBrowserDialog.ShowDialog();

      if( result == DialogResult.OK )
      {
        // The master folder must exist.
        this.MasterItemTextBox.Text = folderBrowserDialog.SelectedPath;
        this.m_masterItemType = MasterItemType.Folder;
      }
    }

    /// <summary>
    /// Event handler for SynchronizeItemsButton. Starts the synchronization
    /// between the master item and the target items
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSynchronizeItemsButton_Click( object sender, EventArgs e )
    {
      this.ResetUIControls();
      if( !this.ValidateParameters() )
        return;

      // Create master item depending of its type
      FileSystemItem masterItem = null;
      switch( m_masterItemType )
      {
        case MasterItemType.File:
          masterItem = new DiskFile( this.MasterItemTextBox.Text );
          break;
        case MasterItemType.Folder:
          masterItem = new DiskFolder( this.MasterItemTextBox.Text );
          break;
        default:
          this.AppendMessageToListBox( "Unknow MasterItemType for path: " + this.MasterItemTextBox.Text );
          return;
      }

      List<FileSystemItem> itemsList = new List<FileSystemItem>();

      // Add master item at index 0
      itemsList.Insert( 0, masterItem );

      // Fill the itemsList to pass to Synchronize
      foreach( AbstractPathListViewItem listViewItem in this.SlaveItemsListView.Items )
      {
        if( listViewItem is FileListViewItem )
        {
          itemsList.Add( new DiskFile( listViewItem.ToString() ) );
        }
        else if( listViewItem is FolderListViewItem )
        {
          itemsList.Add( new DiskFolder( listViewItem.ToString() ) );
        }
      }

      // Set synchronization options with parameters defined by user
      this.m_synchronizationOptions.AutoConflictResolution = this.AutoConflictResolutionCheckBox.Checked;
      this.m_synchronizationOptions.AllowCreations = this.AllowCreationsCheckBox.Checked;
      this.m_synchronizationOptions.AllowDeletions = this.AllowDeletionsCheckBox.Checked;
      this.m_synchronizationOptions.CompareFileData = this.CompareFileDataCheckBox.Checked;
      this.m_synchronizationOptions.PreviewOnly = this.PreviewOnlyCheckBox.Checked;

      try
      {
        this.AbortButton.Enabled = true;
        // Master item is at index 0 of itemsList
        Synchronizer.EasySynchronize( m_synchronizationEvents, m_synchronizationOptions, 0, itemsList.ToArray() );
      }
      catch( AbortException exception )
      {
        // If abort was requested by user
        this.AppendMessageToListBox( "Abort requested, target item may be corrupted and/or unusable" );
        this.AppendMessageToListBox( exception.Message );

        // Reset progression information
        this.ProcessProgressBar.Value = 0;
      }
      catch( Exception exception )
      {
        // If abort was requested by user
        if( exception.InnerException is AbortException )
        {
          this.AppendMessageToListBox( "Abort requested, target item may be corrupted and/or unusable" );
          this.AppendMessageToListBox( exception.InnerException.Message );

          // Reset progression information
          this.ProcessProgressBar.Value = 0;
        }
        else
        {
          this.AppendMessageToListBox( "Exception occured:" + exception.Message );
        }
      }
      finally
      {
        this.AbortButton.Enabled = false;
        this.m_abort = false;
      }
    }

    /// <summary>
    /// Event handler for AbortButton
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAbortButton_Click( object sender, EventArgs e )
    {
      // Do not display if m_abort already true

      DialogResult result = MessageBox.Show(
                                this,
                                "If you abort, the target items will be left"
                                + " in an unknown state. Are you sure you want to cancel?",
                                "Cancel process",
                                MessageBoxButtons.YesNo );

      if( ( result == DialogResult.Yes ) )
      {
        this.m_abort = true;
      }
    }

    #endregion

    #region PRIVATE EVENT HANDLERS - CHECKBOXES

    /// <summary>
    /// Event handler for CheckBox MouseHover event. Set the
    /// description of the CheckBox option in the StatusLabel.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckBoxMouseEnter( object sender, EventArgs e )
    {
      if( sender.Equals( this.AllowCreationsCheckBox ) )
      {
        this.StatusLabel.Text = "Allow the creation of target items if they don't exist";
      }
      else if( sender.Equals( this.AllowDeletionsCheckBox ) )
      {
        this.StatusLabel.Text = "Allow the deletion of target items if they no more exist";
      }
      else if( sender.Equals( this.AutoConflictResolutionCheckBox) )
      {
        this.StatusLabel.Text = "Allow conflicts to be resolved automatically without any modification on target items";
      }
      else if( sender.Equals( this.CompareFileDataCheckBox ) )
      {
        this.StatusLabel.Text = "Allow data comparison between synchronized items";
      }
      else if( sender.Equals( this.PreviewOnlyCheckBox ) )
      {
        this.StatusLabel.Text = "Perfoms only a preview of the synchronization without creating/deleting target items";
      }
    }

    /// <summary>
    /// Event handler for CheckBox MouseLeave event. This clears the
    /// content of the StatusLabel.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckBoxMouseLeave( object sender, EventArgs e )
    {
      this.StatusLabel.Text = string.Empty;
    }

    #endregion

    #region PRIVATE EVENT HANDLERS - SYNCHRONIZATION EVENTS

    /// <summary>
    /// Event handler for SynchronizationEvents Analysis event. This event
    /// is triggered before any processing and gives information about the
    /// actions that will be performed by the synchronizer.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAnalysis( object sender, SynchronizationAnalysisEventArgs e )
    {
      // Display AnalysisResultDialog only if m_sameActionForAll is not set
      if( !this.m_approveAllFileAction && this.NeedActionConfirmation( e ) )
      {

        AnalysisResultDialog dialog = new AnalysisResultDialog( e );
        DialogResult result = dialog.ShowDialog( this );

        this.m_approveAllFileAction |= dialog.ApproveAll;

        if( this.m_approveAllFileAction )
        {
          this.m_defaultApproveFileAction = result;
        }

        if( result == DialogResult.OK )
        {
          bool[] approvedActions = dialog.ApprovedActions;

          // The master is always the first one and not displayed in the list
          System.Diagnostics.Debug.Assert( e.Actions.Length == ( approvedActions.Length + 1 ) );

          for( int i = 1; i < e.Actions.Length; i++ )
          {
            // Action not approved at index i
            if( !approvedActions[ i - 1 ] )
            {
              e.Actions[ i ] = SynchronizationAction.None;
            }
          }
        }
        else
        {
          e.Cancel = true;
          this.AppendMessageToListBox( "Synchronization canceled" );
          throw new AbortException( "Operation aborted" );
        }
      }
      else
      {
        switch( m_defaultApproveFileAction )
        {
          case DialogResult.OK:
            e.Cancel = false;
            break;
          default:
            e.Cancel = true;
            break;
        }

      }
    }

    /// <summary>
    /// Event handler for SynchronizationEvents CompareFileData event. This event is 
    /// triggered when comparing file data.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCompareFileData( object sender, SynchronizationCompareFileDataEventArgs e )
    {
      if( e.ComponentComparesFiles )
      {
        if( e.MasterFile != null )
        {
          this.AppendMessageToListBox(
              String.Format( "Comparing : '{0}' ...",
              e.MasterFile.FullName ) );
        }

        for( int i = 0, count = e.FilesToCompare.Length; i < count; i++ )
        {
          if( e.FilesToCompare[ i ] != null )
          {
            this.AppendMessageToListBox(
              String.Format( " ... with '{0}'",
              e.FilesToCompare[ i ].FullName ) );
          }
        }
      }
    }

    /// <summary>
    /// Event handler for SynchronizationEvents Conflict event. This event is 
    /// triggered when conflict are detected during analysis process.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnConflict( object sender, SynchronizationConflictEventArgs e )
    {
      for( int i = 0; i < e.Files.Length; i++ )
      {
        StringBuilder actionStringBuilder = new StringBuilder();

        if( e.ConflictReasons[ i ] != SynchronizationConflictReason.NoConflict )
        {
          actionStringBuilder.Append( "Item: " );
          actionStringBuilder.AppendLine( e.Files[ i ].FullName );

          switch( e.ConflictReasons[ i ] )
          {
            case SynchronizationConflictReason.MoreRecentThanMaster:
              actionStringBuilder.Append( "is more recent than master item" );
              break;
            case SynchronizationConflictReason.ModifiedAfterLastSynchronization:
              actionStringBuilder.Append( "was modified after last synchronization" );
              break;
            case SynchronizationConflictReason.SameDateAsMasterDifferentData:
              actionStringBuilder.Append( "has the same date as the master item, but data differs" );
              break;
          }

          actionStringBuilder.AppendLine( ", would you like to use it as master?" );

          // Display MessageBox of the action that will be taken
          DialogResult result = MessageBox.Show(
                                      this,
                                      actionStringBuilder.ToString(),
                                      "Conflict Warning",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Exclamation );

          // Empty and dispose of th actionStringBuilder
          actionStringBuilder.Length = 0;
          actionStringBuilder = null;

          if( result == DialogResult.Yes )
          {
            e.ChosenMasterFileIndex = i;
          }
          else
          {
            e.ChosenMasterFileIndex = e.OriginalMasterFileIndex;
          }
        }
      }
    }

    /// <summary>
    /// Event handler for SynchronizationEvents FolderOperationAnalysis
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnFolderOperationAnalysis( object sender, SynchronizationFolderAnalysisEventArgs e )
    {
      // Display AnalysisResultDialog only if m_approveAllFileAction is not set
      if( !this.m_approveAllFileAction )
      {
        AnalysisResultDialog dialog = new AnalysisResultDialog( e );
        DialogResult result = dialog.ShowDialog( this );

        this.m_approveAllFileAction |= dialog.ApproveAll;

        if( this.m_approveAllFileAction )
        {
          m_defaultApproveFileAction = result;
        }

        if( result == DialogResult.OK )
        {
          bool[] approvedActions = dialog.ApprovedActions;

          // The master is always the first one and not displayed in the list
          System.Diagnostics.Debug.Assert( e.Actions.Length == ( approvedActions.Length + 1 ) );

          for( int i = 1; i < e.Actions.Length; i++ )
          {
            // Action not approved at index i
            if( !approvedActions[ i - 1 ] )
            {
              e.Actions[ i ] = SynchronizationAction.None;
            }
          }
        }
      }
      else
      {
        if( m_defaultApproveFileAction != DialogResult.OK )
        {
          throw new AbortException( "Operation aborted" );
        }
      }
    }

    /// <summary>
    /// Event handler for SynchronizationEvents SynchronizationProgression event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSynchronizationProgression( object sender, SynchronizationProgressionEventArgs e )
    {
      if( m_abort )
      {
        throw new AbortException( "Operation aborted" );
      }
      this.ProcessProgressBar.Value = e.ByteProgression.Percent;
      Application.DoEvents();
    }

    #endregion

    #region PRIVATE METHODS

    /// <summary>
    /// Used to display a message in the OuputListBox. It allows to customize
    /// the way messages are displayed
    /// </summary>
    /// <param name="message"></param>
    private void AppendMessageToListBox( string message )
    {
      int insertedIndex = this.OutputListBox.Items.Add( message );
      this.OutputListBox.SelectedIndex = insertedIndex;
    }

    /// <summary>
    /// Register to all events of Synchronization Events
    /// </summary>
    private void InitializeSynchronizationEvents()
    {
      this.m_synchronizationEvents = new SynchronizationEvents();
      this.m_synchronizationEvents.Analysis += new AnalysisEventHandler( this.OnAnalysis );
      this.m_synchronizationEvents.CompareFileData += new CompareFileDataEventHandler( this.OnCompareFileData );
      this.m_synchronizationEvents.Conflict += new ConflictEventHandler( this.OnConflict );
      this.m_synchronizationEvents.FolderOperationAnalysis += new FolderOperationAnalysisEventHandler( this.OnFolderOperationAnalysis );
      this.m_synchronizationEvents.SynchronizationProgression += new SynchronizationProgressionEventHandler( this.OnSynchronizationProgression );
    }

    /// <summary>
    /// Initialize synchronization options
    /// </summary>
    private void InitializeSynchronizationOptions()
    {
      this.m_synchronizationOptions = new SynchronizationOptions();
    }


    /// <summary>
    /// Verify that actions or conflicts are present in 
    /// SynchronizationAnalysisEventArgs. Use to determine
    /// if the AnalysisResultDialog should be displayed or
    /// not.
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    private bool NeedActionConfirmation( SynchronizationAnalysisEventArgs e )
    {
      // Needed by default
      bool needActionConfirmation = true;

      // No action to be taken if master index is -1
      if( e.MasterFileIndex == -1 )
      {
        bool actionsMustBeTaken = false;
        bool conflictDetected = false;

        // Verify if actions have to be performed
        foreach( SynchronizationAction action in e.Actions )
        {
          if( action != SynchronizationAction.None )
          {
            actionsMustBeTaken = true;
            break;
          }
        }

        // Verify if conflicts are detected
        foreach( SynchronizationConflictReason conflict in e.ConflictReasons )
        {
          if( conflict != SynchronizationConflictReason.NoConflict )
          {
            conflictDetected = true;
            break;
          }
        }

        // Display the message and close the 
        if( !actionsMustBeTaken && !conflictDetected )
        {
          // Cancel synchronization for this item
          // since no action has to be taken
          // or conflict were detected
          e.Cancel = true;

          Application.DoEvents();

          // No confirmation needed
          needActionConfirmation = false;
        }
      }

      return needActionConfirmation;
    }

    /// <summary>
    /// Reset progression parameters and UI
    /// </summary>
    private void ResetUIControls()
    {
      this.ProcessProgressBar.Value = 0;
      this.OutputListBox.Items.Clear();
      this.m_approveAllFileAction = false;
      this.m_defaultApproveFileAction = DialogResult.No;
    }

    /// <summary>
    /// Validates that the minimum needed parameters are defined and
    /// there values are valid
    /// </summary>
    /// <returns>true if the parameters are correctly defined, else false</returns>
    private bool ValidateParameters()
    {
      if( string.IsNullOrEmpty( this.MasterItemTextBox.Text ) )
      {
        this.AppendMessageToListBox( "Master file must be set to synchronize" );
        return false;
      }

      if( this.SlaveItemsListView.Items.Count == 0 )
      {
        this.AppendMessageToListBox( "You must specify at least one target item before starting synchronization" );
        return false;
      }
      return true;
    }

    #endregion

    #region PRIVATE FIELDS

    private bool m_abort; // = false;
    private bool m_approveAllFileAction; // = false;
    private DialogResult m_defaultApproveFileAction = DialogResult.No;
    private MasterItemType m_masterItemType = MasterItemType.None;
    private SynchronizationEvents m_synchronizationEvents; // = null;
    private SynchronizationOptions m_synchronizationOptions; // = null;

    #endregion
  }
}