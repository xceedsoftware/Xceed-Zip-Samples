'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [SampleUI.Designer.vb]
'*
'* This application demonstrate how to use the Xceed Synchronize
'* functionnality.
'*
'* This file is part of Xceed FileSystem for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.


Imports Microsoft.VisualBasic
Imports System
Namespace SynchronizeUISample
    Partial Public Class SampleUI
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Not components Is Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SampleUI))
            Me.MasterItemGroupBox = New System.Windows.Forms.GroupBox()
            Me.MasterItemTextBox = New System.Windows.Forms.TextBox()
            Me.BrowseMasterFolderButton = New System.Windows.Forms.Button()
            Me.FilesListViewImageList = New System.Windows.Forms.ImageList(Me.components)
            Me.BrowseMasterFilesterFileButton = New System.Windows.Forms.Button()
            Me.MasterFileLabel = New System.Windows.Forms.Label()
            Me.SlaveGroupBox = New System.Windows.Forms.GroupBox()
            Me.SlaveItemsListView = New System.Windows.Forms.ListView()
            Me.FileNameColumnHeader = New System.Windows.Forms.ColumnHeader()
            Me.FilePathColumnHeader = New System.Windows.Forms.ColumnHeader()
            Me.AddFileButton = New System.Windows.Forms.Button()
            Me.AddFolderButton = New System.Windows.Forms.Button()
            Me.RemoveButton = New System.Windows.Forms.Button()
            Me.SynchronizationGroupBox = New System.Windows.Forms.GroupBox()
            Me.PreviewOnlyCheckBox = New System.Windows.Forms.CheckBox()
            Me.CompareFileDataCheckBox = New System.Windows.Forms.CheckBox()
            Me.AllowDeletionsCheckBox = New System.Windows.Forms.CheckBox()
            Me.AllowCreationsCheckBox = New System.Windows.Forms.CheckBox()
            Me.AutoConflictResolutionCheckBox = New System.Windows.Forms.CheckBox()
            Me.ButtonsGroupBox = New System.Windows.Forms.GroupBox()
            Me.SynchonizeItemsButton = New System.Windows.Forms.Button()
            Me.AbortButton = New System.Windows.Forms.Button()
            Me.ProgressionGroupBox = New System.Windows.Forms.GroupBox()
            Me.OutputListBox = New System.Windows.Forms.ListBox()
            Me.StatusStrip = New System.Windows.Forms.StatusStrip()
            Me.ProcessProgressBar = New System.Windows.Forms.ToolStripProgressBar()
            Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
            Me.MasterItemGroupBox.SuspendLayout()
            Me.SlaveGroupBox.SuspendLayout()
            Me.SynchronizationGroupBox.SuspendLayout()
            Me.ButtonsGroupBox.SuspendLayout()
            Me.ProgressionGroupBox.SuspendLayout()
            Me.StatusStrip.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' MasterItemGroupBox
            ' 
            Me.MasterItemGroupBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.MasterItemGroupBox.Controls.Add(Me.MasterItemTextBox)
            Me.MasterItemGroupBox.Controls.Add(Me.BrowseMasterFolderButton)
            Me.MasterItemGroupBox.Controls.Add(Me.BrowseMasterFilesterFileButton)
            Me.MasterItemGroupBox.Controls.Add(Me.MasterFileLabel)
            Me.MasterItemGroupBox.Location = New System.Drawing.Point(12, 12)
            Me.MasterItemGroupBox.Name = "MasterItemGroupBox"
            Me.MasterItemGroupBox.Size = New System.Drawing.Size(762, 68)
            Me.MasterItemGroupBox.TabIndex = 3
            Me.MasterItemGroupBox.TabStop = False
            Me.MasterItemGroupBox.Text = "1 - Select the Master Item ( 1 File or 1 Folder )"
            ' 
            ' MasterItemTextBox
            ' 
            Me.MasterItemTextBox.Anchor = (CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.MasterItemTextBox.Location = New System.Drawing.Point(75, 29)
            Me.MasterItemTextBox.Name = "MasterItemTextBox"
            Me.MasterItemTextBox.Size = New System.Drawing.Size(574, 20)
            Me.MasterItemTextBox.TabIndex = 0
            ' 
            ' BrowseMasterFolderButton
            ' 
            Me.BrowseMasterFolderButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.BrowseMasterFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BrowseMasterFolderButton.ImageIndex = 0
            Me.BrowseMasterFolderButton.ImageList = Me.FilesListViewImageList
            Me.BrowseMasterFolderButton.Location = New System.Drawing.Point(655, 39)
            Me.BrowseMasterFolderButton.Name = "BrowseMasterFolderButton"
            Me.BrowseMasterFolderButton.Size = New System.Drawing.Size(101, 23)
            Me.BrowseMasterFolderButton.TabIndex = 2
            Me.BrowseMasterFolderButton.Text = "Choose Folder"
            Me.BrowseMasterFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.BrowseMasterFolderButton.UseVisualStyleBackColor = True
            '      Me.BrowseMasterFolderButton.Click += New System.EventHandler(Me.OnBrowseMasterFolderButton_Click);
            ' 
            ' FilesListViewImageList
            ' 
            Me.FilesListViewImageList.ImageStream = (CType(resources.GetObject("FilesListViewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer))
            Me.FilesListViewImageList.TransparentColor = System.Drawing.Color.Transparent
            Me.FilesListViewImageList.Images.SetKeyName(0, "Folder")
            Me.FilesListViewImageList.Images.SetKeyName(1, "File")
            Me.FilesListViewImageList.Images.SetKeyName(2, "DeleteHS.png")
            Me.FilesListViewImageList.Images.SetKeyName(3, "PlayHS.png")
            Me.FilesListViewImageList.Images.SetKeyName(4, "StopHS.png")
            ' 
            ' BrowseMasterFilesterFileButton
            ' 
            Me.BrowseMasterFilesterFileButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.BrowseMasterFilesterFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.BrowseMasterFilesterFileButton.ImageIndex = 1
            Me.BrowseMasterFilesterFileButton.ImageList = Me.FilesListViewImageList
            Me.BrowseMasterFilesterFileButton.Location = New System.Drawing.Point(655, 11)
            Me.BrowseMasterFilesterFileButton.Name = "BrowseMasterFilesterFileButton"
            Me.BrowseMasterFilesterFileButton.Size = New System.Drawing.Size(101, 23)
            Me.BrowseMasterFilesterFileButton.TabIndex = 1
            Me.BrowseMasterFilesterFileButton.Text = "Choose File"
            Me.BrowseMasterFilesterFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.BrowseMasterFilesterFileButton.UseVisualStyleBackColor = True
            '      Me.BrowseMasterFilesterFileButton.Click += New System.EventHandler(Me.OnBrowseMasterFileButton_Click);
            ' 
            ' MasterFileLabel
            ' 
            Me.MasterFileLabel.AutoSize = True
            Me.MasterFileLabel.Location = New System.Drawing.Point(11, 32)
            Me.MasterFileLabel.Name = "MasterFileLabel"
            Me.MasterFileLabel.Size = New System.Drawing.Size(65, 13)
            Me.MasterFileLabel.TabIndex = 1
            Me.MasterFileLabel.Text = "Master Item:"
            ' 
            ' SlaveGroupBox
            ' 
            Me.SlaveGroupBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.SlaveGroupBox.Controls.Add(Me.SlaveItemsListView)
            Me.SlaveGroupBox.Controls.Add(Me.AddFileButton)
            Me.SlaveGroupBox.Controls.Add(Me.AddFolderButton)
            Me.SlaveGroupBox.Controls.Add(Me.RemoveButton)
            Me.SlaveGroupBox.Location = New System.Drawing.Point(12, 86)
            Me.SlaveGroupBox.Name = "SlaveGroupBox"
            Me.SlaveGroupBox.Size = New System.Drawing.Size(762, 201)
            Me.SlaveGroupBox.TabIndex = 1
            Me.SlaveGroupBox.TabStop = False
            Me.SlaveGroupBox.Text = "2 - Select the Target Item(s) ( File(s) and/or Folder(s) )"
            ' 
            ' SlaveItemsListView
            ' 
            Me.SlaveItemsListView.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.SlaveItemsListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileNameColumnHeader, Me.FilePathColumnHeader})
            Me.SlaveItemsListView.GridLines = True
            Me.SlaveItemsListView.Location = New System.Drawing.Point(110, 18)
            Me.SlaveItemsListView.Name = "SlaveItemsListView"
            Me.SlaveItemsListView.Size = New System.Drawing.Size(646, 182)
            Me.SlaveItemsListView.SmallImageList = Me.FilesListViewImageList
            Me.SlaveItemsListView.TabIndex = 0
            Me.SlaveItemsListView.UseCompatibleStateImageBehavior = False
            Me.SlaveItemsListView.View = System.Windows.Forms.View.Details
            ' 
            ' FileNameColumnHeader
            ' 
            Me.FileNameColumnHeader.Text = "Name"
            Me.FileNameColumnHeader.Width = 200
            ' 
            ' FilePathColumnHeader
            ' 
            Me.FilePathColumnHeader.Text = "Full Path"
            Me.FilePathColumnHeader.Width = 442
            ' 
            ' AddFileButton
            ' 
            Me.AddFileButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AddFileButton.ImageIndex = 1
            Me.AddFileButton.ImageList = Me.FilesListViewImageList
            Me.AddFileButton.Location = New System.Drawing.Point(14, 18)
            Me.AddFileButton.Name = "AddFileButton"
            Me.AddFileButton.Size = New System.Drawing.Size(90, 23)
            Me.AddFileButton.TabIndex = 0
            Me.AddFileButton.Text = "Add File"
            Me.AddFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.AddFileButton.UseVisualStyleBackColor = True
            '      Me.AddFileButton.Click += New System.EventHandler(Me.OnAddFileButton_Click);
            ' 
            ' AddFolderButton
            ' 
            Me.AddFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AddFolderButton.ImageIndex = 0
            Me.AddFolderButton.ImageList = Me.FilesListViewImageList
            Me.AddFolderButton.Location = New System.Drawing.Point(14, 47)
            Me.AddFolderButton.Name = "AddFolderButton"
            Me.AddFolderButton.Size = New System.Drawing.Size(90, 23)
            Me.AddFolderButton.TabIndex = 1
            Me.AddFolderButton.Text = "Add Folder"
            Me.AddFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.AddFolderButton.UseVisualStyleBackColor = True
            '      Me.AddFolderButton.Click += New System.EventHandler(Me.OnAddFolderButton_Click);
            ' 
            ' RemoveButton
            ' 
            Me.RemoveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.RemoveButton.ImageIndex = 2
            Me.RemoveButton.ImageList = Me.FilesListViewImageList
            Me.RemoveButton.Location = New System.Drawing.Point(14, 76)
            Me.RemoveButton.Name = "RemoveButton"
            Me.RemoveButton.Size = New System.Drawing.Size(90, 23)
            Me.RemoveButton.TabIndex = 2
            Me.RemoveButton.Text = "Remove"
            Me.RemoveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.RemoveButton.UseVisualStyleBackColor = True
            '      Me.RemoveButton.Click += New System.EventHandler(Me.OnRemoveButton_Click);
            ' 
            ' SynchronizationGroupBox
            ' 
            Me.SynchronizationGroupBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.SynchronizationGroupBox.Controls.Add(Me.PreviewOnlyCheckBox)
            Me.SynchronizationGroupBox.Controls.Add(Me.CompareFileDataCheckBox)
            Me.SynchronizationGroupBox.Controls.Add(Me.AllowDeletionsCheckBox)
            Me.SynchronizationGroupBox.Controls.Add(Me.AllowCreationsCheckBox)
            Me.SynchronizationGroupBox.Controls.Add(Me.AutoConflictResolutionCheckBox)
            Me.SynchronizationGroupBox.Location = New System.Drawing.Point(12, 293)
            Me.SynchronizationGroupBox.Name = "SynchronizationGroupBox"
            Me.SynchronizationGroupBox.Size = New System.Drawing.Size(762, 63)
            Me.SynchronizationGroupBox.TabIndex = 3
            Me.SynchronizationGroupBox.TabStop = False
            Me.SynchronizationGroupBox.Text = "3 - Choose Synchronization Options"
            ' 
            ' PreviewOnlyCheckBox
            ' 
            Me.PreviewOnlyCheckBox.AutoSize = True
            Me.PreviewOnlyCheckBox.Location = New System.Drawing.Point(284, 19)
            Me.PreviewOnlyCheckBox.Name = "PreviewOnlyCheckBox"
            Me.PreviewOnlyCheckBox.Size = New System.Drawing.Size(88, 17)
            Me.PreviewOnlyCheckBox.TabIndex = 5
            Me.PreviewOnlyCheckBox.Text = "Preview Only"
            Me.PreviewOnlyCheckBox.UseVisualStyleBackColor = True
            '      Me.PreviewOnlyCheckBox.MouseLeave += New System.EventHandler(Me.OnCheckBoxMouseLeave);
            '      Me.PreviewOnlyCheckBox.MouseEnter += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            ' 
            ' CompareFileDataCheckBox
            ' 
            Me.CompareFileDataCheckBox.AutoSize = True
            Me.CompareFileDataCheckBox.Checked = True
            Me.CompareFileDataCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.CompareFileDataCheckBox.Location = New System.Drawing.Point(128, 40)
            Me.CompareFileDataCheckBox.Name = "CompareFileDataCheckBox"
            Me.CompareFileDataCheckBox.Size = New System.Drawing.Size(113, 17)
            Me.CompareFileDataCheckBox.TabIndex = 3
            Me.CompareFileDataCheckBox.Text = "Compare File Data"
            Me.CompareFileDataCheckBox.UseVisualStyleBackColor = True
            '      Me.CompareFileDataCheckBox.MouseLeave += New System.EventHandler(Me.OnCheckBoxMouseLeave);
            '      Me.CompareFileDataCheckBox.MouseEnter += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            ' 
            ' AllowDeletionsCheckBox
            ' 
            Me.AllowDeletionsCheckBox.AutoSize = True
            Me.AllowDeletionsCheckBox.Location = New System.Drawing.Point(11, 42)
            Me.AllowDeletionsCheckBox.Name = "AllowDeletionsCheckBox"
            Me.AllowDeletionsCheckBox.Size = New System.Drawing.Size(98, 17)
            Me.AllowDeletionsCheckBox.TabIndex = 2
            Me.AllowDeletionsCheckBox.Text = "Allow Deletions"
            Me.AllowDeletionsCheckBox.UseVisualStyleBackColor = True
            '      Me.AllowDeletionsCheckBox.MouseLeave += New System.EventHandler(Me.OnCheckBoxMouseLeave);
            '      Me.AllowDeletionsCheckBox.MouseEnter += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            ' 
            ' AllowCreationsCheckBox
            ' 
            Me.AllowCreationsCheckBox.AutoSize = True
            Me.AllowCreationsCheckBox.Checked = True
            Me.AllowCreationsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
            Me.AllowCreationsCheckBox.Location = New System.Drawing.Point(11, 19)
            Me.AllowCreationsCheckBox.Name = "AllowCreationsCheckBox"
            Me.AllowCreationsCheckBox.Size = New System.Drawing.Size(98, 17)
            Me.AllowCreationsCheckBox.TabIndex = 1
            Me.AllowCreationsCheckBox.Text = "Allow Creations"
            Me.AllowCreationsCheckBox.UseVisualStyleBackColor = True
            '      Me.AllowCreationsCheckBox.MouseLeave += New System.EventHandler(Me.OnCheckBoxMouseLeave);
            '      Me.AllowCreationsCheckBox.MouseEnter += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            ' 
            ' AutoConflictResolutionCheckBox
            ' 
            Me.AutoConflictResolutionCheckBox.AutoSize = True
            Me.AutoConflictResolutionCheckBox.Location = New System.Drawing.Point(128, 19)
            Me.AutoConflictResolutionCheckBox.Name = "AutoConflictResolutionCheckBox"
            Me.AutoConflictResolutionCheckBox.Size = New System.Drawing.Size(139, 17)
            Me.AutoConflictResolutionCheckBox.TabIndex = 0
            Me.AutoConflictResolutionCheckBox.Text = "Auto Conflict Resolution"
            Me.AutoConflictResolutionCheckBox.UseVisualStyleBackColor = True
            '      Me.AutoConflictResolutionCheckBox.MouseLeave += New System.EventHandler(Me.OnCheckBoxMouseLeave);
            '      Me.AutoConflictResolutionCheckBox.MouseEnter += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            '      Me.AutoConflictResolutionCheckBox.MouseHover += New System.EventHandler(Me.OnCheckBoxMouseEnter);
            ' 
            ' ButtonsGroupBox
            ' 
            Me.ButtonsGroupBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.ButtonsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.ButtonsGroupBox.Controls.Add(Me.SynchonizeItemsButton)
            Me.ButtonsGroupBox.Controls.Add(Me.AbortButton)
            Me.ButtonsGroupBox.Location = New System.Drawing.Point(12, 362)
            Me.ButtonsGroupBox.Name = "ButtonsGroupBox"
            Me.ButtonsGroupBox.Size = New System.Drawing.Size(762, 54)
            Me.ButtonsGroupBox.TabIndex = 0
            Me.ButtonsGroupBox.TabStop = False
            Me.ButtonsGroupBox.Text = "4 - Start / Abort Synchronization"
            ' 
            ' SynchonizeItemsButton
            ' 
            Me.SynchonizeItemsButton.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.SynchonizeItemsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.SynchonizeItemsButton.ImageIndex = 3
            Me.SynchonizeItemsButton.ImageList = Me.FilesListViewImageList
            Me.SynchonizeItemsButton.Location = New System.Drawing.Point(11, 19)
            Me.SynchonizeItemsButton.Name = "SynchonizeItemsButton"
            Me.SynchonizeItemsButton.Size = New System.Drawing.Size(137, 23)
            Me.SynchonizeItemsButton.TabIndex = 0
            Me.SynchonizeItemsButton.Text = "Start Synchronization"
            Me.SynchonizeItemsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.SynchonizeItemsButton.UseVisualStyleBackColor = True
            '      Me.SynchonizeItemsButton.Click += New System.EventHandler(Me.OnSynchronizeItemsButton_Click);
            ' 
            ' AbortButton
            ' 
            Me.AbortButton.Anchor = System.Windows.Forms.AnchorStyles.Left
            Me.AbortButton.Enabled = False
            Me.AbortButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AbortButton.ImageIndex = 4
            Me.AbortButton.ImageList = Me.FilesListViewImageList
            Me.AbortButton.Location = New System.Drawing.Point(156, 19)
            Me.AbortButton.Name = "AbortButton"
            Me.AbortButton.Size = New System.Drawing.Size(63, 23)
            Me.AbortButton.TabIndex = 1
            Me.AbortButton.Text = "Abort"
            Me.AbortButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.AbortButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
            Me.AbortButton.UseVisualStyleBackColor = True
            '      Me.AbortButton.Click += New System.EventHandler(Me.OnAbortButton_Click);
            ' 
            ' ProgressionGroupBox
            ' 
            Me.ProgressionGroupBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.ProgressionGroupBox.Controls.Add(Me.OutputListBox)
            Me.ProgressionGroupBox.Location = New System.Drawing.Point(12, 422)
            Me.ProgressionGroupBox.Name = "ProgressionGroupBox"
            Me.ProgressionGroupBox.Size = New System.Drawing.Size(762, 109)
            Me.ProgressionGroupBox.TabIndex = 1
            Me.ProgressionGroupBox.TabStop = False
            Me.ProgressionGroupBox.Text = "5 - Output Window"
            ' 
            ' OutputListBox
            ' 
            Me.OutputListBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
            Me.OutputListBox.Location = New System.Drawing.Point(3, 16)
            Me.OutputListBox.Name = "OutputListBox"
            Me.OutputListBox.Size = New System.Drawing.Size(756, 82)
            Me.OutputListBox.TabIndex = 0
            ' 
            ' StatusStrip
            ' 
            Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProcessProgressBar, Me.StatusLabel})
            Me.StatusStrip.Location = New System.Drawing.Point(0, 542)
            Me.StatusStrip.Name = "StatusStrip"
            Me.StatusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
            Me.StatusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes
            Me.StatusStrip.Size = New System.Drawing.Size(784, 22)
            Me.StatusStrip.TabIndex = 4
            Me.StatusStrip.Text = "Synchronize Demo"
            ' 
            ' ProcessProgressBar
            ' 
            Me.ProcessProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ProcessProgressBar.AutoSize = False
            Me.ProcessProgressBar.Name = "ProcessProgressBar"
            Me.ProcessProgressBar.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.ProcessProgressBar.RightToLeftLayout = True
            Me.ProcessProgressBar.Size = New System.Drawing.Size(200, 16)
            Me.ProcessProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            ' 
            ' StatusLabel
            ' 
            Me.StatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.StatusLabel.Name = "StatusLabel"
            Me.StatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
            Me.StatusLabel.Size = New System.Drawing.Size(636, 17)
            Me.StatusLabel.Spring = True
            Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            ' 
            ' SampleUI
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(784, 564)
            Me.Controls.Add(Me.StatusStrip)
            Me.Controls.Add(Me.ProgressionGroupBox)
            Me.Controls.Add(Me.MasterItemGroupBox)
            Me.Controls.Add(Me.ButtonsGroupBox)
            Me.Controls.Add(Me.SynchronizationGroupBox)
            Me.Controls.Add(Me.SlaveGroupBox)
            Me.MinimumSize = New System.Drawing.Size(800, 600)
            Me.Name = "SampleUI"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Synchronize Sample"
            Me.MasterItemGroupBox.ResumeLayout(False)
            Me.MasterItemGroupBox.PerformLayout()
            Me.SlaveGroupBox.ResumeLayout(False)
            Me.SynchronizationGroupBox.ResumeLayout(False)
            Me.SynchronizationGroupBox.PerformLayout()
            Me.ButtonsGroupBox.ResumeLayout(False)
            Me.ProgressionGroupBox.ResumeLayout(False)
            Me.StatusStrip.ResumeLayout(False)
            Me.StatusStrip.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        Private WithEvents AddFolderButton As System.Windows.Forms.Button
        Private WithEvents RemoveButton As System.Windows.Forms.Button
        Private WithEvents AddFileButton As System.Windows.Forms.Button
        Private SlaveItemsListView As System.Windows.Forms.ListView
        Private MasterItemTextBox As System.Windows.Forms.TextBox
        Private MasterFileLabel As System.Windows.Forms.Label
        Private WithEvents BrowseMasterFilesterFileButton As System.Windows.Forms.Button
        Private FilePathColumnHeader As System.Windows.Forms.ColumnHeader
        Private WithEvents AbortButton As System.Windows.Forms.Button
        Private WithEvents SynchonizeItemsButton As System.Windows.Forms.Button
        Private OutputListBox As System.Windows.Forms.ListBox
        Private FilesListViewImageList As System.Windows.Forms.ImageList
        Private SynchronizationGroupBox As System.Windows.Forms.GroupBox
        Private WithEvents AutoConflictResolutionCheckBox As System.Windows.Forms.CheckBox
        Private WithEvents AllowCreationsCheckBox As System.Windows.Forms.CheckBox
        Private WithEvents AllowDeletionsCheckBox As System.Windows.Forms.CheckBox
        Private WithEvents CompareFileDataCheckBox As System.Windows.Forms.CheckBox
        Private WithEvents PreviewOnlyCheckBox As System.Windows.Forms.CheckBox
        Private WithEvents BrowseMasterFolderButton As System.Windows.Forms.Button
        Private SlaveGroupBox As System.Windows.Forms.GroupBox
        Private MasterItemGroupBox As System.Windows.Forms.GroupBox
        Private ButtonsGroupBox As System.Windows.Forms.GroupBox
        Private ProgressionGroupBox As System.Windows.Forms.GroupBox
        Private FileNameColumnHeader As System.Windows.Forms.ColumnHeader
        Private StatusStrip As System.Windows.Forms.StatusStrip
        Private ProcessProgressBar As System.Windows.Forms.ToolStripProgressBar
        Private StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    End Class
End Namespace

