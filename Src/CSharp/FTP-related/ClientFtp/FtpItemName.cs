/*
 * Xceed FTP for .NET - ClientFTP Sample Application
 * Copyright (c) 2003 - Xceed Software Inc.
 * 
 * [FtpItemName.cs]
 * 
 * This application demonstrate how to use the Xceed FTP object model
 * in a generic way.
 * 
 * This file is part of Xceed FTP for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ClientFtp
{
	public class FtpItemName : System.Windows.Forms.Form
	{
    //=========================================================================
    #region PUBLIC CONSTRUCTORS

		public FtpItemName()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

    #endregion PUBLIC CONSTRUCTORS
    //=========================================================================
    #region EVENTS

    private void cmdCancel_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void cmdOk_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    #endregion EVENTS
    //=========================================================================
    #region PUBLIC METHODS

    public DialogResult ShowDialog( 
      System.Windows.Forms.IWin32Window owner,
      string formCaption,
      ref string itemName )
    {
      this.Text = formCaption;

      txtName.Text = itemName;

      DialogResult result = this.ShowDialog( owner );

      if( result == DialogResult.OK )
      {
        itemName = txtName.Text;
      }

      return result;
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
    //=========================================================================
    #region Windows Form Designer generated members

    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOk;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated members

    #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FtpItemName));
      this.lblName = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOk = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblName
      // 
      this.lblName.AutoSize = true;
      this.lblName.Location = new System.Drawing.Point(8, 8);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(33, 14);
      this.lblName.TabIndex = 0;
      this.lblName.Text = "Name";
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(56, 8);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(208, 21);
      this.txtName.TabIndex = 1;
      this.txtName.Text = "";
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdCancel.Location = new System.Drawing.Point(192, 40);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(72, 24);
      this.cmdCancel.TabIndex = 3;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
      // 
      // cmdOk
      // 
      this.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdOk.Location = new System.Drawing.Point(112, 40);
      this.cmdOk.Name = "cmdOk";
      this.cmdOk.Size = new System.Drawing.Size(72, 24);
      this.cmdOk.TabIndex = 2;
      this.cmdOk.Text = "&Ok";
      this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
      // 
      // FtpItemName
      // 
      this.AcceptButton = this.cmdOk;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size(272, 70);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.cmdOk,
                                                                  this.cmdCancel,
                                                                  this.txtName,
                                                                  this.lblName});
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FtpItemName";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "[RUNTIME]";
      this.ResumeLayout(false);

    }
		#endregion
	}
}
