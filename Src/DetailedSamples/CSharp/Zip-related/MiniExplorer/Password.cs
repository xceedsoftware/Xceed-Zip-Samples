/*
 * Xceed Zip for .NET - MiniExplorer Sample Application
 * Copyright (c) 2000-2003 - Xceed Software Inc.
 * 
 * [Password.cs]
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

namespace Xceed.FileSystem.Samples.MiniExplorer
{
	/// <summary>
	/// Summary description for Password.
	/// </summary>
	public class PasswordForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label FilenameLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox PasswordText;
    private System.Windows.Forms.CheckBox HideCheckBox;
    private System.Windows.Forms.Button OKPushButton;
    private System.Windows.Forms.Button CancelPushButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PasswordForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}


    public DialogResult ShowDialog( IWin32Window owner, string filename, ref string password )
    {
      FilenameLabel.Text = filename;
      PasswordText.Text = password;
      HideCheckBox.Checked = true;

      this.DialogResult = DialogResult.Cancel;

      if( base.ShowDialog( owner ) == DialogResult.OK )
      {
        password = PasswordText.Text;
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
      this.OKPushButton = new System.Windows.Forms.Button();
      this.CancelPushButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.FilenameLabel = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.PasswordText = new System.Windows.Forms.TextBox();
      this.HideCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // OKPushButton
      // 
      this.OKPushButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OKPushButton.Location = new System.Drawing.Point(200, 112);
      this.OKPushButton.Name = "OKPushButton";
      this.OKPushButton.TabIndex = 5;
      this.OKPushButton.Text = "&OK";
      // 
      // CancelPushButton
      // 
      this.CancelPushButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelPushButton.Location = new System.Drawing.Point(280, 112);
      this.CancelPushButton.Name = "CancelPushButton";
      this.CancelPushButton.TabIndex = 6;
      this.CancelPushButton.Text = "&Cancel";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(192, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Decryption password required for file:";
      // 
      // FilenameLabel
      // 
      this.FilenameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.FilenameLabel.Location = new System.Drawing.Point(8, 24);
      this.FilenameLabel.Name = "FilenameLabel";
      this.FilenameLabel.Size = new System.Drawing.Size(352, 16);
      this.FilenameLabel.TabIndex = 1;
      this.FilenameLabel.Text = "label2";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(8, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(240, 16);
      this.label3.TabIndex = 2;
      this.label3.Text = "Please provide a valid decryption password:";
      // 
      // PasswordText
      // 
      this.PasswordText.Location = new System.Drawing.Point(8, 72);
      this.PasswordText.Name = "PasswordText";
      this.PasswordText.Size = new System.Drawing.Size(352, 20);
      this.PasswordText.TabIndex = 3;
      this.PasswordText.Text = "";
      // 
      // HideCheckBox
      // 
      this.HideCheckBox.Location = new System.Drawing.Point(8, 96);
      this.HideCheckBox.Name = "HideCheckBox";
      this.HideCheckBox.Size = new System.Drawing.Size(136, 16);
      this.HideCheckBox.TabIndex = 4;
      this.HideCheckBox.Text = "Hide password chars";
      this.HideCheckBox.CheckedChanged += new System.EventHandler(this.HideCheckBox_CheckedChanged);
      // 
      // PasswordForm
      // 
      this.AcceptButton = this.OKPushButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.CancelPushButton;
      this.ClientSize = new System.Drawing.Size(368, 143);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.HideCheckBox,
                                                                  this.PasswordText,
                                                                  this.label3,
                                                                  this.FilenameLabel,
                                                                  this.label1,
                                                                  this.CancelPushButton,
                                                                  this.OKPushButton});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PasswordForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Password";
      this.ResumeLayout(false);

    }
		#endregion

    private void HideCheckBox_CheckedChanged(object sender, System.EventArgs e)
    {
      PasswordText.PasswordChar = ( HideCheckBox.Checked ? '*' : '\0' );
    }
	}
}
