/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AnalysisResultDialog.Designer.cs]
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
  partial class AnalysisResultDialog
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
      this.InformationTextBox = new System.Windows.Forms.TextBox();
      this.OkButton = new System.Windows.Forms.Button();
      this.CancelButton = new System.Windows.Forms.Button();
      this.AnalysisListView = new System.Windows.Forms.ListView();
      this.Source = new System.Windows.Forms.ColumnHeader();
      this.Action = new System.Windows.Forms.ColumnHeader();
      this.Destination = new System.Windows.Forms.ColumnHeader();
      this.AllSameActionCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // InformationTextBox
      // 
      this.InformationTextBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.InformationTextBox.Enabled = false;
      this.InformationTextBox.Location = new System.Drawing.Point( 12, 12 );
      this.InformationTextBox.Multiline = true;
      this.InformationTextBox.Name = "InformationTextBox";
      this.InformationTextBox.Size = new System.Drawing.Size( 560, 63 );
      this.InformationTextBox.TabIndex = 3;
      this.InformationTextBox.Text = "Chose action(s) you want to perform in the list below :\r\n\r\nChecked\t\tAction will b" +
          "e performed\r\nUnchecked\tAction won\'t be performed";
      // 
      // OkButton
      // 
      this.OkButton.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.OkButton.Location = new System.Drawing.Point( 412, 329 );
      this.OkButton.Name = "OkButton";
      this.OkButton.Size = new System.Drawing.Size( 76, 23 );
      this.OkButton.TabIndex = 0;
      this.OkButton.Text = "Ok";
      this.OkButton.UseVisualStyleBackColor = true;
      this.OkButton.Click += new System.EventHandler( this.OkButton_Click );
      // 
      // CancelButton
      // 
      this.CancelButton.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelButton.Location = new System.Drawing.Point( 494, 329 );
      this.CancelButton.Name = "CancelButton";
      this.CancelButton.Size = new System.Drawing.Size( 78, 23 );
      this.CancelButton.TabIndex = 1;
      this.CancelButton.Text = "Cancel";
      this.CancelButton.UseVisualStyleBackColor = true;
      // 
      // AnalysisListView
      // 
      this.AnalysisListView.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.AnalysisListView.CheckBoxes = true;
      this.AnalysisListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.Source,
            this.Action,
            this.Destination} );
      this.AnalysisListView.GridLines = true;
      this.AnalysisListView.Location = new System.Drawing.Point( 12, 81 );
      this.AnalysisListView.Name = "AnalysisListView";
      this.AnalysisListView.Size = new System.Drawing.Size( 560, 242 );
      this.AnalysisListView.TabIndex = 2;
      this.AnalysisListView.UseCompatibleStateImageBehavior = false;
      this.AnalysisListView.View = System.Windows.Forms.View.Details;
      // 
      // Source
      // 
      this.Source.Text = "Source Item";
      this.Source.Width = 256;
      // 
      // Action
      // 
      this.Action.Text = "Action";
      this.Action.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.Action.Width = 80;
      // 
      // Destination
      // 
      this.Destination.Text = "Destination Item";
      this.Destination.Width = 256;
      // 
      // AllSameActionCheckBox
      // 
      this.AllSameActionCheckBox.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.AllSameActionCheckBox.AutoSize = true;
      this.AllSameActionCheckBox.Location = new System.Drawing.Point( 12, 333 );
      this.AllSameActionCheckBox.Name = "AllSameActionCheckBox";
      this.AllSameActionCheckBox.Size = new System.Drawing.Size( 306, 17 );
      this.AllSameActionCheckBox.TabIndex = 4;
      this.AllSameActionCheckBox.Text = "Do this for all files or folders (Only Conflicts will be displayed)";
      this.AllSameActionCheckBox.UseVisualStyleBackColor = true;
      this.AllSameActionCheckBox.CheckedChanged += new System.EventHandler( this.ApproveAllCheckBox_CheckedChanged );
      // 
      // AnalysisResultDialog
      // 
      this.AcceptButton = this.OkButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 584, 364 );
      this.Controls.Add( this.AllSameActionCheckBox );
      this.Controls.Add( this.AnalysisListView );
      this.Controls.Add( this.CancelButton );
      this.Controls.Add( this.OkButton );
      this.Controls.Add( this.InformationTextBox );
      this.MinimumSize = new System.Drawing.Size( 600, 400 );
      this.Name = "AnalysisResultDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Analysis Result";
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox InformationTextBox;
    private System.Windows.Forms.Button OkButton;
    private new System.Windows.Forms.Button CancelButton;
    private System.Windows.Forms.ListView AnalysisListView;
    private System.Windows.Forms.ColumnHeader Source;
    private System.Windows.Forms.ColumnHeader Action;
    private System.Windows.Forms.ColumnHeader Destination;
    private System.Windows.Forms.CheckBox AllSameActionCheckBox;
  }
}