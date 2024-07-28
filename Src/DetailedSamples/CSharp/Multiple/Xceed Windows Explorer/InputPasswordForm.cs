/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [InputPasswordForm.cs]
 * 
 * Form used to get a password from the user for encrypted archives.
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

namespace Xceed.FileSystem.Samples
{
	public class InputPasswordForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

		public InputPasswordForm()
		{
			InitializeComponent();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    public string Password
    {
      get{ return PasswordTextbox.Text; }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    public DialogResult ShowDialog( System.Windows.Forms.IWin32Window owner, string itemName )
    {
      ItemNameLabel.Text = itemName;

      return this.ShowDialog( owner );
    }

    #endregion PUBLIC METHODS
    
    #region PROTECTED METHODS

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

    #endregion PROTECTED METHODS

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InputPasswordForm));
      this.WarningLabel = new System.Windows.Forms.Label();
      this.PasswordTextbox = new System.Windows.Forms.TextBox();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.OkBtn = new System.Windows.Forms.Button();
      this.ItemNameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // WarningLabel
      // 
      this.WarningLabel.Location = new System.Drawing.Point(16, 32);
      this.WarningLabel.Name = "WarningLabel";
      this.WarningLabel.Size = new System.Drawing.Size(376, 40);
      this.WarningLabel.TabIndex = 0;
      this.WarningLabel.Text = "This item is encrypted in the archive. The current password is either invalid or " +
        "wasn\'t provided. Please input the password and click \'Ok\' to continue. ";
      // 
      // PasswordTextbox
      // 
      this.PasswordTextbox.Location = new System.Drawing.Point(16, 80);
      this.PasswordTextbox.Name = "PasswordTextbox";
      this.PasswordTextbox.PasswordChar = '*';
      this.PasswordTextbox.Size = new System.Drawing.Size(376, 20);
      this.PasswordTextbox.TabIndex = 1;
      this.PasswordTextbox.Text = "";
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point(312, 112);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(80, 24);
      this.CancelBtn.TabIndex = 6;
      this.CancelBtn.Text = "&Cancel";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.OkBtn.Location = new System.Drawing.Point(224, 112);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(80, 24);
      this.OkBtn.TabIndex = 5;
      this.OkBtn.Text = "&Ok";
      // 
      // ItemNameLabel
      // 
      this.ItemNameLabel.Location = new System.Drawing.Point(16, 8);
      this.ItemNameLabel.Name = "ItemNameLabel";
      this.ItemNameLabel.Size = new System.Drawing.Size(376, 16);
      this.ItemNameLabel.TabIndex = 4;
      // 
      // InputPasswordForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(402, 144);
      this.Controls.Add(this.ItemNameLabel);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.CancelBtn);
      this.Controls.Add(this.PasswordTextbox);
      this.Controls.Add(this.WarningLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InputPasswordForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Input password";
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.Windows.Forms.Label WarningLabel;
    private System.Windows.Forms.TextBox PasswordTextbox;
    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.Label ItemNameLabel;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated fields
	}
}
