/*
 * Xceed Zip for .NET - MiniExplorer Sample Application
 * Copyright (c) 2000-2003 - Xceed Software Inc.
 * 
 * [SplitSize.cs]
 * 
 * This application demonstrates how to use the Xceed FileSystem object model
 * in a generic way.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.MiniExplorer
{
	/// <summary>
	/// Summary description for SplitSize.
	/// </summary>
	public class SplitSizeForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button OkButton;
    private System.Windows.Forms.Button FailButton;
    private System.Windows.Forms.NumericUpDown SplitSizeUpDown;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SplitSizeForm()
		{
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();
		}

    public DialogResult ShowDialog( IWin32Window owner, ref long splitSize )
    {
      SplitSizeUpDown.Value = splitSize;
      this.DialogResult = DialogResult.Cancel;

      if( base.ShowDialog( owner ) == DialogResult.OK )
      {
        splitSize = ( long )SplitSizeUpDown.Value;
        return DialogResult.OK;
      }

      return DialogResult.Cancel;
    }

    /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
      this.label1 = new System.Windows.Forms.Label();
      this.SplitSizeUpDown = new System.Windows.Forms.NumericUpDown();
      this.OkButton = new System.Windows.Forms.Button();
      this.FailButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.SplitSizeUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(200, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Maximum size of each part (in bytes):";
      // 
      // SplitSizeUpDown
      // 
      this.SplitSizeUpDown.Increment = new System.Decimal(new int[] {
                                                                      1024,
                                                                      0,
                                                                      0,
                                                                      0});
      this.SplitSizeUpDown.Location = new System.Drawing.Point(208, 8);
      this.SplitSizeUpDown.Maximum = new System.Decimal(new int[] {
                                                                    999999999,
                                                                    0,
                                                                    0,
                                                                    0});
      this.SplitSizeUpDown.Name = "SplitSizeUpDown";
      this.SplitSizeUpDown.Size = new System.Drawing.Size(104, 20);
      this.SplitSizeUpDown.TabIndex = 1;
      this.SplitSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.SplitSizeUpDown.ThousandsSeparator = true;
      // 
      // OkButton
      // 
      this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkButton.Location = new System.Drawing.Point(160, 48);
      this.OkButton.Name = "OkButton";
      this.OkButton.TabIndex = 6;
      this.OkButton.Text = "&Ok";
      // 
      // FailButton
      // 
      this.FailButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.FailButton.Location = new System.Drawing.Point(240, 48);
      this.FailButton.Name = "FailButton";
      this.FailButton.TabIndex = 7;
      this.FailButton.Text = "&Cancel";
      // 
      // SplitSizeForm
      // 
      this.AcceptButton = this.OkButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.FailButton;
      this.ClientSize = new System.Drawing.Size(322, 80);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.FailButton,
                                                                  this.OkButton,
                                                                  this.SplitSizeUpDown,
                                                                  this.label1});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SplitSizeForm";
      this.Text = "Split existing Zip file...";
      ((System.ComponentModel.ISupportInitialize)(this.SplitSizeUpDown)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion
	}
}
