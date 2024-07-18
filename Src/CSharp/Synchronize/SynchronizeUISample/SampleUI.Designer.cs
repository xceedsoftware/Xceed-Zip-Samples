/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [SampleUI.Designer.cs]
 * 
 * This application demonstrate how to use the Xceed Synchronize
 * functionnality.
 * 
 * This file is part of Xceed FileSystem for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

namespace SynchronizeUISample
{
  partial class SampleUI
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( SampleUI ) );
      this.MasterItemGroupBox = new System.Windows.Forms.GroupBox();
      this.MasterItemTextBox = new System.Windows.Forms.TextBox();
      this.BrowseMasterFolderButton = new System.Windows.Forms.Button();
      this.FilesListViewImageList = new System.Windows.Forms.ImageList( this.components );
      this.BrowseMasterFilesterFileButton = new System.Windows.Forms.Button();
      this.MasterFileLabel = new System.Windows.Forms.Label();
      this.SlaveGroupBox = new System.Windows.Forms.GroupBox();
      this.SlaveItemsListView = new System.Windows.Forms.ListView();
      this.FileNameColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.FilePathColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.AddFileButton = new System.Windows.Forms.Button();
      this.AddFolderButton = new System.Windows.Forms.Button();
      this.RemoveButton = new System.Windows.Forms.Button();
      this.SynchronizationGroupBox = new System.Windows.Forms.GroupBox();
      this.PreviewOnlyCheckBox = new System.Windows.Forms.CheckBox();
      this.CompareFileDataCheckBox = new System.Windows.Forms.CheckBox();
      this.AllowDeletionsCheckBox = new System.Windows.Forms.CheckBox();
      this.AllowCreationsCheckBox = new System.Windows.Forms.CheckBox();
      this.AutoConflictResolutionCheckBox = new System.Windows.Forms.CheckBox();
      this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
      this.SynchonizeItemsButton = new System.Windows.Forms.Button();
      this.AbortButton = new System.Windows.Forms.Button();
      this.ProgressionGroupBox = new System.Windows.Forms.GroupBox();
      this.OutputListBox = new System.Windows.Forms.ListBox();
      this.StatusStrip = new System.Windows.Forms.StatusStrip();
      this.ProcessProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.MasterItemGroupBox.SuspendLayout();
      this.SlaveGroupBox.SuspendLayout();
      this.SynchronizationGroupBox.SuspendLayout();
      this.ButtonsGroupBox.SuspendLayout();
      this.ProgressionGroupBox.SuspendLayout();
      this.StatusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // MasterItemGroupBox
      // 
      this.MasterItemGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.MasterItemGroupBox.Controls.Add( this.MasterItemTextBox );
      this.MasterItemGroupBox.Controls.Add( this.BrowseMasterFolderButton );
      this.MasterItemGroupBox.Controls.Add( this.BrowseMasterFilesterFileButton );
      this.MasterItemGroupBox.Controls.Add( this.MasterFileLabel );
      this.MasterItemGroupBox.Location = new System.Drawing.Point( 12, 12 );
      this.MasterItemGroupBox.Name = "MasterItemGroupBox";
      this.MasterItemGroupBox.Size = new System.Drawing.Size( 762, 68 );
      this.MasterItemGroupBox.TabIndex = 3;
      this.MasterItemGroupBox.TabStop = false;
      this.MasterItemGroupBox.Text = "1 - Select the Master Item ( 1 File or 1 Folder )";
      // 
      // MasterItemTextBox
      // 
      this.MasterItemTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.MasterItemTextBox.Location = new System.Drawing.Point( 75, 29 );
      this.MasterItemTextBox.Name = "MasterItemTextBox";
      this.MasterItemTextBox.Size = new System.Drawing.Size( 574, 20 );
      this.MasterItemTextBox.TabIndex = 0;
      // 
      // BrowseMasterFolderButton
      // 
      this.BrowseMasterFolderButton.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.BrowseMasterFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BrowseMasterFolderButton.ImageIndex = 0;
      this.BrowseMasterFolderButton.ImageList = this.FilesListViewImageList;
      this.BrowseMasterFolderButton.Location = new System.Drawing.Point( 655, 39 );
      this.BrowseMasterFolderButton.Name = "BrowseMasterFolderButton";
      this.BrowseMasterFolderButton.Size = new System.Drawing.Size( 101, 23 );
      this.BrowseMasterFolderButton.TabIndex = 2;
      this.BrowseMasterFolderButton.Text = "Choose Folder";
      this.BrowseMasterFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.BrowseMasterFolderButton.UseVisualStyleBackColor = true;
      this.BrowseMasterFolderButton.Click += new System.EventHandler( this.OnBrowseMasterFolderButton_Click );
      // 
      // FilesListViewImageList
      // 
      this.FilesListViewImageList.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "FilesListViewImageList.ImageStream" ) ) );
      this.FilesListViewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.FilesListViewImageList.Images.SetKeyName( 0, "Folder" );
      this.FilesListViewImageList.Images.SetKeyName( 1, "File" );
      this.FilesListViewImageList.Images.SetKeyName( 2, "DeleteHS.png" );
      this.FilesListViewImageList.Images.SetKeyName( 3, "PlayHS.png" );
      this.FilesListViewImageList.Images.SetKeyName( 4, "StopHS.png" );
      // 
      // BrowseMasterFilesterFileButton
      // 
      this.BrowseMasterFilesterFileButton.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.BrowseMasterFilesterFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.BrowseMasterFilesterFileButton.ImageIndex = 1;
      this.BrowseMasterFilesterFileButton.ImageList = this.FilesListViewImageList;
      this.BrowseMasterFilesterFileButton.Location = new System.Drawing.Point( 655, 11 );
      this.BrowseMasterFilesterFileButton.Name = "BrowseMasterFilesterFileButton";
      this.BrowseMasterFilesterFileButton.Size = new System.Drawing.Size( 101, 23 );
      this.BrowseMasterFilesterFileButton.TabIndex = 1;
      this.BrowseMasterFilesterFileButton.Text = "Choose File";
      this.BrowseMasterFilesterFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.BrowseMasterFilesterFileButton.UseVisualStyleBackColor = true;
      this.BrowseMasterFilesterFileButton.Click += new System.EventHandler( this.OnBrowseMasterFileButton_Click );
      // 
      // MasterFileLabel
      // 
      this.MasterFileLabel.AutoSize = true;
      this.MasterFileLabel.Location = new System.Drawing.Point( 11, 32 );
      this.MasterFileLabel.Name = "MasterFileLabel";
      this.MasterFileLabel.Size = new System.Drawing.Size( 65, 13 );
      this.MasterFileLabel.TabIndex = 1;
      this.MasterFileLabel.Text = "Master Item:";
      // 
      // SlaveGroupBox
      // 
      this.SlaveGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.SlaveGroupBox.Controls.Add( this.SlaveItemsListView );
      this.SlaveGroupBox.Controls.Add( this.AddFileButton );
      this.SlaveGroupBox.Controls.Add( this.AddFolderButton );
      this.SlaveGroupBox.Controls.Add( this.RemoveButton );
      this.SlaveGroupBox.Location = new System.Drawing.Point( 12, 86 );
      this.SlaveGroupBox.Name = "SlaveGroupBox";
      this.SlaveGroupBox.Size = new System.Drawing.Size( 762, 201 );
      this.SlaveGroupBox.TabIndex = 1;
      this.SlaveGroupBox.TabStop = false;
      this.SlaveGroupBox.Text = "2 - Select the Target Item(s) ( File(s) and/or Folder(s) )";
      // 
      // SlaveItemsListView
      // 
      this.SlaveItemsListView.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.SlaveItemsListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.FileNameColumnHeader,
            this.FilePathColumnHeader} );
      this.SlaveItemsListView.GridLines = true;
      this.SlaveItemsListView.Location = new System.Drawing.Point( 110, 18 );
      this.SlaveItemsListView.Name = "SlaveItemsListView";
      this.SlaveItemsListView.Size = new System.Drawing.Size( 646, 182 );
      this.SlaveItemsListView.SmallImageList = this.FilesListViewImageList;
      this.SlaveItemsListView.TabIndex = 0;
      this.SlaveItemsListView.UseCompatibleStateImageBehavior = false;
      this.SlaveItemsListView.View = System.Windows.Forms.View.Details;
      // 
      // FileNameColumnHeader
      // 
      this.FileNameColumnHeader.Text = "Name";
      this.FileNameColumnHeader.Width = 200;
      // 
      // FilePathColumnHeader
      // 
      this.FilePathColumnHeader.Text = "Full Path";
      this.FilePathColumnHeader.Width = 442;
      // 
      // AddFileButton
      // 
      this.AddFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.AddFileButton.ImageIndex = 1;
      this.AddFileButton.ImageList = this.FilesListViewImageList;
      this.AddFileButton.Location = new System.Drawing.Point( 14, 18 );
      this.AddFileButton.Name = "AddFileButton";
      this.AddFileButton.Size = new System.Drawing.Size( 90, 23 );
      this.AddFileButton.TabIndex = 0;
      this.AddFileButton.Text = "Add File";
      this.AddFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.AddFileButton.UseVisualStyleBackColor = true;
      this.AddFileButton.Click += new System.EventHandler( this.OnAddFileButton_Click );
      // 
      // AddFolderButton
      // 
      this.AddFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.AddFolderButton.ImageIndex = 0;
      this.AddFolderButton.ImageList = this.FilesListViewImageList;
      this.AddFolderButton.Location = new System.Drawing.Point( 14, 47 );
      this.AddFolderButton.Name = "AddFolderButton";
      this.AddFolderButton.Size = new System.Drawing.Size( 90, 23 );
      this.AddFolderButton.TabIndex = 1;
      this.AddFolderButton.Text = "Add Folder";
      this.AddFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.AddFolderButton.UseVisualStyleBackColor = true;
      this.AddFolderButton.Click += new System.EventHandler( this.OnAddFolderButton_Click );
      // 
      // RemoveButton
      // 
      this.RemoveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.RemoveButton.ImageIndex = 2;
      this.RemoveButton.ImageList = this.FilesListViewImageList;
      this.RemoveButton.Location = new System.Drawing.Point( 14, 76 );
      this.RemoveButton.Name = "RemoveButton";
      this.RemoveButton.Size = new System.Drawing.Size( 90, 23 );
      this.RemoveButton.TabIndex = 2;
      this.RemoveButton.Text = "Remove";
      this.RemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.RemoveButton.UseVisualStyleBackColor = true;
      this.RemoveButton.Click += new System.EventHandler( this.OnRemoveButton_Click );
      // 
      // SynchronizationGroupBox
      // 
      this.SynchronizationGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.SynchronizationGroupBox.Controls.Add( this.PreviewOnlyCheckBox );
      this.SynchronizationGroupBox.Controls.Add( this.CompareFileDataCheckBox );
      this.SynchronizationGroupBox.Controls.Add( this.AllowDeletionsCheckBox );
      this.SynchronizationGroupBox.Controls.Add( this.AllowCreationsCheckBox );
      this.SynchronizationGroupBox.Controls.Add( this.AutoConflictResolutionCheckBox );
      this.SynchronizationGroupBox.Location = new System.Drawing.Point( 12, 293 );
      this.SynchronizationGroupBox.Name = "SynchronizationGroupBox";
      this.SynchronizationGroupBox.Size = new System.Drawing.Size( 762, 63 );
      this.SynchronizationGroupBox.TabIndex = 3;
      this.SynchronizationGroupBox.TabStop = false;
      this.SynchronizationGroupBox.Text = "3 - Choose Synchronization Options";
      // 
      // PreviewOnlyCheckBox
      // 
      this.PreviewOnlyCheckBox.AutoSize = true;
      this.PreviewOnlyCheckBox.Location = new System.Drawing.Point( 284, 19 );
      this.PreviewOnlyCheckBox.Name = "PreviewOnlyCheckBox";
      this.PreviewOnlyCheckBox.Size = new System.Drawing.Size( 88, 17 );
      this.PreviewOnlyCheckBox.TabIndex = 5;
      this.PreviewOnlyCheckBox.Text = "Preview Only";
      this.PreviewOnlyCheckBox.UseVisualStyleBackColor = true;
      this.PreviewOnlyCheckBox.MouseLeave += new System.EventHandler( this.OnCheckBoxMouseLeave );
      this.PreviewOnlyCheckBox.MouseEnter += new System.EventHandler( this.OnCheckBoxMouseEnter );
      // 
      // CompareFileDataCheckBox
      // 
      this.CompareFileDataCheckBox.AutoSize = true;
      this.CompareFileDataCheckBox.Checked = true;
      this.CompareFileDataCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.CompareFileDataCheckBox.Location = new System.Drawing.Point( 128, 40 );
      this.CompareFileDataCheckBox.Name = "CompareFileDataCheckBox";
      this.CompareFileDataCheckBox.Size = new System.Drawing.Size( 113, 17 );
      this.CompareFileDataCheckBox.TabIndex = 3;
      this.CompareFileDataCheckBox.Text = "Compare File Data";
      this.CompareFileDataCheckBox.UseVisualStyleBackColor = true;
      this.CompareFileDataCheckBox.MouseLeave += new System.EventHandler( this.OnCheckBoxMouseLeave );
      this.CompareFileDataCheckBox.MouseEnter += new System.EventHandler( this.OnCheckBoxMouseEnter );
      // 
      // AllowDeletionsCheckBox
      // 
      this.AllowDeletionsCheckBox.AutoSize = true;
      this.AllowDeletionsCheckBox.Location = new System.Drawing.Point( 11, 42 );
      this.AllowDeletionsCheckBox.Name = "AllowDeletionsCheckBox";
      this.AllowDeletionsCheckBox.Size = new System.Drawing.Size( 98, 17 );
      this.AllowDeletionsCheckBox.TabIndex = 2;
      this.AllowDeletionsCheckBox.Text = "Allow Deletions";
      this.AllowDeletionsCheckBox.UseVisualStyleBackColor = true;
      this.AllowDeletionsCheckBox.MouseLeave += new System.EventHandler( this.OnCheckBoxMouseLeave );
      this.AllowDeletionsCheckBox.MouseEnter += new System.EventHandler( this.OnCheckBoxMouseEnter );
      // 
      // AllowCreationsCheckBox
      // 
      this.AllowCreationsCheckBox.AutoSize = true;
      this.AllowCreationsCheckBox.Checked = true;
      this.AllowCreationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AllowCreationsCheckBox.Location = new System.Drawing.Point( 11, 19 );
      this.AllowCreationsCheckBox.Name = "AllowCreationsCheckBox";
      this.AllowCreationsCheckBox.Size = new System.Drawing.Size( 98, 17 );
      this.AllowCreationsCheckBox.TabIndex = 1;
      this.AllowCreationsCheckBox.Text = "Allow Creations";
      this.AllowCreationsCheckBox.UseVisualStyleBackColor = true;
      this.AllowCreationsCheckBox.MouseLeave += new System.EventHandler( this.OnCheckBoxMouseLeave );
      this.AllowCreationsCheckBox.MouseEnter += new System.EventHandler( this.OnCheckBoxMouseEnter );
      // 
      // AutoConflictResolutionCheckBox
      // 
      this.AutoConflictResolutionCheckBox.AutoSize = true;
      this.AutoConflictResolutionCheckBox.Location = new System.Drawing.Point( 128, 19 );
      this.AutoConflictResolutionCheckBox.Name = "AutoConflictResolutionCheckBox";
      this.AutoConflictResolutionCheckBox.Size = new System.Drawing.Size( 139, 17 );
      this.AutoConflictResolutionCheckBox.TabIndex = 0;
      this.AutoConflictResolutionCheckBox.Text = "Auto Conflict Resolution";
      this.AutoConflictResolutionCheckBox.UseVisualStyleBackColor = true;
      this.AutoConflictResolutionCheckBox.MouseLeave += new System.EventHandler( this.OnCheckBoxMouseLeave );
      this.AutoConflictResolutionCheckBox.MouseEnter += new System.EventHandler( this.OnCheckBoxMouseEnter );
      this.AutoConflictResolutionCheckBox.MouseHover += new System.EventHandler( this.OnCheckBoxMouseEnter );
      // 
      // ButtonsGroupBox
      // 
      this.ButtonsGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.ButtonsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ButtonsGroupBox.Controls.Add( this.SynchonizeItemsButton );
      this.ButtonsGroupBox.Controls.Add( this.AbortButton );
      this.ButtonsGroupBox.Location = new System.Drawing.Point( 12, 362 );
      this.ButtonsGroupBox.Name = "ButtonsGroupBox";
      this.ButtonsGroupBox.Size = new System.Drawing.Size( 762, 54 );
      this.ButtonsGroupBox.TabIndex = 0;
      this.ButtonsGroupBox.TabStop = false;
      this.ButtonsGroupBox.Text = "4 - Start / Abort Synchronization";
      // 
      // SynchonizeItemsButton
      // 
      this.SynchonizeItemsButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.SynchonizeItemsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.SynchonizeItemsButton.ImageIndex = 3;
      this.SynchonizeItemsButton.ImageList = this.FilesListViewImageList;
      this.SynchonizeItemsButton.Location = new System.Drawing.Point( 11, 19 );
      this.SynchonizeItemsButton.Name = "SynchonizeItemsButton";
      this.SynchonizeItemsButton.Size = new System.Drawing.Size( 137, 23 );
      this.SynchonizeItemsButton.TabIndex = 0;
      this.SynchonizeItemsButton.Text = "Start Synchronization";
      this.SynchonizeItemsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.SynchonizeItemsButton.UseVisualStyleBackColor = true;
      this.SynchonizeItemsButton.Click += new System.EventHandler( this.OnSynchronizeItemsButton_Click );
      // 
      // AbortButton
      // 
      this.AbortButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.AbortButton.Enabled = false;
      this.AbortButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.AbortButton.ImageIndex = 4;
      this.AbortButton.ImageList = this.FilesListViewImageList;
      this.AbortButton.Location = new System.Drawing.Point( 156, 19 );
      this.AbortButton.Name = "AbortButton";
      this.AbortButton.Size = new System.Drawing.Size( 63, 23 );
      this.AbortButton.TabIndex = 1;
      this.AbortButton.Text = "Abort";
      this.AbortButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.AbortButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.AbortButton.UseVisualStyleBackColor = true;
      this.AbortButton.Click += new System.EventHandler( this.OnAbortButton_Click );
      // 
      // ProgressionGroupBox
      // 
      this.ProgressionGroupBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.ProgressionGroupBox.Controls.Add( this.OutputListBox );
      this.ProgressionGroupBox.Location = new System.Drawing.Point( 12, 422 );
      this.ProgressionGroupBox.Name = "ProgressionGroupBox";
      this.ProgressionGroupBox.Size = new System.Drawing.Size( 762, 109 );
      this.ProgressionGroupBox.TabIndex = 1;
      this.ProgressionGroupBox.TabStop = false;
      this.ProgressionGroupBox.Text = "5 - Output Window";
      // 
      // OutputListBox
      // 
      this.OutputListBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.OutputListBox.Location = new System.Drawing.Point( 3, 16 );
      this.OutputListBox.Name = "OutputListBox";
      this.OutputListBox.Size = new System.Drawing.Size( 756, 82 );
      this.OutputListBox.TabIndex = 0;
      // 
      // StatusStrip
      // 
      this.StatusStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.ProcessProgressBar,
            this.StatusLabel} );
      this.StatusStrip.Location = new System.Drawing.Point( 0, 542 );
      this.StatusStrip.Name = "StatusStrip";
      this.StatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.StatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.StatusStrip.Size = new System.Drawing.Size( 784, 22 );
      this.StatusStrip.TabIndex = 4;
      this.StatusStrip.Text = "Synchronize Demo";
      // 
      // ProcessProgressBar
      // 
      this.ProcessProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.ProcessProgressBar.AutoSize = false;
      this.ProcessProgressBar.Name = "ProcessProgressBar";
      this.ProcessProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.ProcessProgressBar.RightToLeftLayout = true;
      this.ProcessProgressBar.Size = new System.Drawing.Size( 200, 16 );
      this.ProcessProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      // 
      // StatusLabel
      // 
      this.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
      this.StatusLabel.Size = new System.Drawing.Size( 636, 17 );
      this.StatusLabel.Spring = true;
      this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // SampleUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 784, 564 );
      this.Controls.Add( this.StatusStrip );
      this.Controls.Add( this.ProgressionGroupBox );
      this.Controls.Add( this.MasterItemGroupBox );
      this.Controls.Add( this.ButtonsGroupBox );
      this.Controls.Add( this.SynchronizationGroupBox );
      this.Controls.Add( this.SlaveGroupBox );
      this.MinimumSize = new System.Drawing.Size( 800, 600 );
      this.Name = "SampleUI";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Synchronize Sample";
      this.MasterItemGroupBox.ResumeLayout( false );
      this.MasterItemGroupBox.PerformLayout();
      this.SlaveGroupBox.ResumeLayout( false );
      this.SynchronizationGroupBox.ResumeLayout( false );
      this.SynchronizationGroupBox.PerformLayout();
      this.ButtonsGroupBox.ResumeLayout( false );
      this.ProgressionGroupBox.ResumeLayout( false );
      this.StatusStrip.ResumeLayout( false );
      this.StatusStrip.PerformLayout();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button AddFolderButton;
    private System.Windows.Forms.Button RemoveButton;
    private System.Windows.Forms.Button AddFileButton;
    private System.Windows.Forms.ListView SlaveItemsListView;
    private System.Windows.Forms.TextBox MasterItemTextBox;
    private System.Windows.Forms.Label MasterFileLabel;
    private System.Windows.Forms.Button BrowseMasterFilesterFileButton;
    private System.Windows.Forms.ColumnHeader FilePathColumnHeader;
    private System.Windows.Forms.Button AbortButton;
    private System.Windows.Forms.Button SynchonizeItemsButton;
    private System.Windows.Forms.ListBox OutputListBox;
    private System.Windows.Forms.ImageList FilesListViewImageList;
    private System.Windows.Forms.GroupBox SynchronizationGroupBox;
    private System.Windows.Forms.CheckBox AutoConflictResolutionCheckBox;
    private System.Windows.Forms.CheckBox AllowCreationsCheckBox;
    private System.Windows.Forms.CheckBox AllowDeletionsCheckBox;
    private System.Windows.Forms.CheckBox CompareFileDataCheckBox;
    private System.Windows.Forms.CheckBox PreviewOnlyCheckBox;
    private System.Windows.Forms.Button BrowseMasterFolderButton;
    private System.Windows.Forms.GroupBox SlaveGroupBox;
    private System.Windows.Forms.GroupBox MasterItemGroupBox;
    private System.Windows.Forms.GroupBox ButtonsGroupBox;
    private System.Windows.Forms.GroupBox ProgressionGroupBox;
    private System.Windows.Forms.ColumnHeader FileNameColumnHeader;
    private System.Windows.Forms.StatusStrip StatusStrip;
    private System.Windows.Forms.ToolStripProgressBar ProcessProgressBar;
    private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
  }
}

