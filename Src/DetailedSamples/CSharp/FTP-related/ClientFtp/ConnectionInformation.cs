/*
 * Xceed FTP for .NET - ClientFTP Sample Application
 * Copyright (c) 2003 - Xceed Software Inc.
 * 
 * [ConnectionInformation.cs]
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
	public class ConnectionInformation : System.Windows.Forms.Form
	{
    //=========================================================================
    #region PUBLIC CONSTRUCTORS

		public ConnectionInformation()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

    #endregion PUBLIC CONSTRUCTORS
    //=========================================================================
    #region EVENTS

    private void chkAnonymous_CheckedChanged(object sender, System.EventArgs e)
    {
      if( chkAnonymous.Checked )
      {
        // Reset the user information to the default values.
        txtUserName.Text = "anonymous";
        txtPassword.Text = "guest";
      }

      txtUserName.Enabled = !chkAnonymous.Checked;
      txtPassword.Enabled = !chkAnonymous.Checked;
    }

    private void cmdCancel_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void cmdOk_Click(object sender, System.EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
    }

    private void chkUseHttpProxy_CheckedChanged(object sender, System.EventArgs e)
    {
      txtProxyAddress.Enabled = chkUseHttpProxy.Enabled;
      txtProxyPort.Enabled = chkUseHttpProxy.Enabled;
      txtProxyUserName.Enabled = chkUseHttpProxy.Enabled;
      txtProxyPassword.Enabled = chkUseHttpProxy.Enabled;
    }

    #endregion EVENTS
    //=========================================================================
    #region PUBLIC METHODS

    public DialogResult ShowDialog(
      System.Windows.Forms.IWin32Window owner,
      ref string hostAddress,
      ref int hostPort,
      ref bool anonymousConnection,
      ref string userName,
      ref string password,
      ref string proxyAddress,
      ref int proxyPort,
      ref string proxyUserName,
      ref string proxyPassword )
    {
      return this.ShowDialog( 
        owner, 
        ref hostAddress, 
        ref hostPort, 
        ref anonymousConnection, 
        ref userName, 
        ref password, 
        ref proxyAddress,
        ref proxyPort, 
        ref proxyUserName,
        ref proxyPassword, 
        false );
    }

    public DialogResult ShowDialog(
      System.Windows.Forms.IWin32Window owner,
      ref string hostAddress,
      ref int hostPort,
      ref bool anonymousConnection,
      ref string userName,
      ref string password,
      ref string proxyAddress,
      ref int proxyPort,
      ref string proxyUserName,
      ref string proxyPassword, 
      bool userInfoOnly )
    {
      // Initialize the controls with the specified values.
      txtHostAddress.Text = hostAddress;
      txtPort.Value = hostPort;
      chkAnonymous.Checked = anonymousConnection;
      txtUserName.Text = userName;
      txtPassword.Text = password;
      chkUseHttpProxy.Checked = ( proxyAddress.Length > 0 );
      txtProxyAddress.Text = proxyAddress;
      txtProxyPort.Value = proxyPort;
      txtProxyUserName.Text = proxyUserName;
      txtProxyPassword.Text = proxyPassword;

      if( userInfoOnly )
      {
        // In mode UserInfoOnly, we disable the server address and proxy controls.
        txtHostAddress.Enabled = false;
        txtPort.Enabled = false;

        chkUseHttpProxy.Checked = false;
        chkUseHttpProxy.Enabled = false;
      }

      // Show the dialog.
      DialogResult result = this.ShowDialog( owner );

      if( result == DialogResult.OK )
      {
        // Get the values from the form.
        hostAddress = txtHostAddress.Text;
        hostPort = ( int )txtPort.Value;
        anonymousConnection = chkAnonymous.Checked;
        userName = txtUserName.Text;
        password = txtPassword.Text;

        if( chkUseHttpProxy.Checked )
        {
          proxyAddress = txtProxyAddress.Text;
          proxyPort = ( int )txtProxyPort.Value;
          proxyUserName = txtProxyUserName.Text;
          proxyPassword = txtProxyPassword.Text;
        }
        else if( !userInfoOnly )
        {
          proxyAddress = string.Empty;
        }
      }

      return result;
    }

    #endregion PUBLIC METHODS
    //=========================================================================
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

    private System.Windows.Forms.GroupBox grpHost;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.Label lblHostAddress;
    private System.Windows.Forms.TextBox txtHostAddress;
    private System.Windows.Forms.GroupBox grpCredential;
    private System.Windows.Forms.Label lblUserName;
    private System.Windows.Forms.CheckBox chkAnonymous;
    private System.Windows.Forms.TextBox txtUserName;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.Button cmdOk;
    private System.Windows.Forms.NumericUpDown txtPort;
    private System.Windows.Forms.GroupBox grpHttpProxy;
    private System.Windows.Forms.CheckBox chkUseHttpProxy;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown txtProxyPort;
    private System.Windows.Forms.TextBox txtProxyAddress;
    private System.Windows.Forms.TextBox txtProxyPassword;
    private System.Windows.Forms.TextBox txtProxyUserName;
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ConnectionInformation ) );
      this.grpHost = new System.Windows.Forms.GroupBox();
      this.txtPort = new System.Windows.Forms.NumericUpDown();
      this.txtHostAddress = new System.Windows.Forms.TextBox();
      this.lblPort = new System.Windows.Forms.Label();
      this.lblHostAddress = new System.Windows.Forms.Label();
      this.grpCredential = new System.Windows.Forms.GroupBox();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.txtUserName = new System.Windows.Forms.TextBox();
      this.chkAnonymous = new System.Windows.Forms.CheckBox();
      this.lblUserName = new System.Windows.Forms.Label();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.cmdOk = new System.Windows.Forms.Button();
      this.grpHttpProxy = new System.Windows.Forms.GroupBox();
      this.txtProxyPassword = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtProxyUserName = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtProxyPort = new System.Windows.Forms.NumericUpDown();
      this.txtProxyAddress = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.chkUseHttpProxy = new System.Windows.Forms.CheckBox();
      this.grpHost.SuspendLayout();
      ( ( System.ComponentModel.ISupportInitialize )( this.txtPort ) ).BeginInit();
      this.grpCredential.SuspendLayout();
      this.grpHttpProxy.SuspendLayout();
      ( ( System.ComponentModel.ISupportInitialize )( this.txtProxyPort ) ).BeginInit();
      this.SuspendLayout();
      // 
      // grpHost
      // 
      this.grpHost.Controls.Add( this.txtPort );
      this.grpHost.Controls.Add( this.txtHostAddress );
      this.grpHost.Controls.Add( this.lblPort );
      this.grpHost.Controls.Add( this.lblHostAddress );
      this.grpHost.Location = new System.Drawing.Point( 8, 8 );
      this.grpHost.Name = "grpHost";
      this.grpHost.Size = new System.Drawing.Size( 408, 80 );
      this.grpHost.TabIndex = 0;
      this.grpHost.TabStop = false;
      this.grpHost.Text = "Host";
      // 
      // txtPort
      // 
      this.txtPort.Location = new System.Drawing.Point( 72, 48 );
      this.txtPort.Maximum = new decimal( new int[] {
            65536,
            0,
            0,
            0} );
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new System.Drawing.Size( 64, 21 );
      this.txtPort.TabIndex = 4;
      // 
      // txtHostAddress
      // 
      this.txtHostAddress.Location = new System.Drawing.Point( 72, 24 );
      this.txtHostAddress.Name = "txtHostAddress";
      this.txtHostAddress.Size = new System.Drawing.Size( 328, 21 );
      this.txtHostAddress.TabIndex = 2;
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point( 8, 48 );
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size( 27, 13 );
      this.lblPort.TabIndex = 3;
      this.lblPort.Text = "Port";
      // 
      // lblHostAddress
      // 
      this.lblHostAddress.AutoSize = true;
      this.lblHostAddress.Location = new System.Drawing.Point( 8, 24 );
      this.lblHostAddress.Name = "lblHostAddress";
      this.lblHostAddress.Size = new System.Drawing.Size( 46, 13 );
      this.lblHostAddress.TabIndex = 1;
      this.lblHostAddress.Text = "Address";
      // 
      // grpCredential
      // 
      this.grpCredential.Controls.Add( this.txtPassword );
      this.grpCredential.Controls.Add( this.lblPassword );
      this.grpCredential.Controls.Add( this.txtUserName );
      this.grpCredential.Controls.Add( this.chkAnonymous );
      this.grpCredential.Controls.Add( this.lblUserName );
      this.grpCredential.Location = new System.Drawing.Point( 8, 96 );
      this.grpCredential.Name = "grpCredential";
      this.grpCredential.Size = new System.Drawing.Size( 408, 104 );
      this.grpCredential.TabIndex = 5;
      this.grpCredential.TabStop = false;
      this.grpCredential.Text = "Credential";
      // 
      // txtPassword
      // 
      this.txtPassword.Location = new System.Drawing.Point( 96, 72 );
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.Size = new System.Drawing.Size( 304, 21 );
      this.txtPassword.TabIndex = 10;
      this.txtPassword.UseSystemPasswordChar = true;
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point( 24, 72 );
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size( 53, 13 );
      this.lblPassword.TabIndex = 9;
      this.lblPassword.Text = "Password";
      // 
      // txtUserName
      // 
      this.txtUserName.Location = new System.Drawing.Point( 96, 48 );
      this.txtUserName.Name = "txtUserName";
      this.txtUserName.Size = new System.Drawing.Size( 304, 21 );
      this.txtUserName.TabIndex = 8;
      // 
      // chkAnonymous
      // 
      this.chkAnonymous.Location = new System.Drawing.Point( 16, 24 );
      this.chkAnonymous.Name = "chkAnonymous";
      this.chkAnonymous.Size = new System.Drawing.Size( 376, 16 );
      this.chkAnonymous.TabIndex = 6;
      this.chkAnonymous.Text = "Anonymous connection";
      this.chkAnonymous.CheckedChanged += new System.EventHandler( this.chkAnonymous_CheckedChanged );
      // 
      // lblUserName
      // 
      this.lblUserName.AutoSize = true;
      this.lblUserName.Location = new System.Drawing.Point( 24, 48 );
      this.lblUserName.Name = "lblUserName";
      this.lblUserName.Size = new System.Drawing.Size( 58, 13 );
      this.lblUserName.TabIndex = 7;
      this.lblUserName.Text = "User name";
      // 
      // cmdCancel
      // 
      this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdCancel.Location = new System.Drawing.Point( 344, 344 );
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size( 72, 24 );
      this.cmdCancel.TabIndex = 22;
      this.cmdCancel.Text = "&Cancel";
      this.cmdCancel.Click += new System.EventHandler( this.cmdCancel_Click );
      // 
      // cmdOk
      // 
      this.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdOk.Location = new System.Drawing.Point( 264, 344 );
      this.cmdOk.Name = "cmdOk";
      this.cmdOk.Size = new System.Drawing.Size( 72, 24 );
      this.cmdOk.TabIndex = 21;
      this.cmdOk.Text = "&Ok";
      this.cmdOk.Click += new System.EventHandler( this.cmdOk_Click );
      // 
      // grpHttpProxy
      // 
      this.grpHttpProxy.Controls.Add( this.txtProxyPassword );
      this.grpHttpProxy.Controls.Add( this.label3 );
      this.grpHttpProxy.Controls.Add( this.txtProxyUserName );
      this.grpHttpProxy.Controls.Add( this.label4 );
      this.grpHttpProxy.Controls.Add( this.txtProxyPort );
      this.grpHttpProxy.Controls.Add( this.txtProxyAddress );
      this.grpHttpProxy.Controls.Add( this.label1 );
      this.grpHttpProxy.Controls.Add( this.label2 );
      this.grpHttpProxy.Controls.Add( this.chkUseHttpProxy );
      this.grpHttpProxy.Location = new System.Drawing.Point( 8, 208 );
      this.grpHttpProxy.Name = "grpHttpProxy";
      this.grpHttpProxy.Size = new System.Drawing.Size( 408, 128 );
      this.grpHttpProxy.TabIndex = 11;
      this.grpHttpProxy.TabStop = false;
      this.grpHttpProxy.Text = "HTTP Proxy";
      // 
      // txtProxyPassword
      // 
      this.txtProxyPassword.Enabled = false;
      this.txtProxyPassword.Location = new System.Drawing.Point( 96, 96 );
      this.txtProxyPassword.Name = "txtProxyPassword";
      this.txtProxyPassword.Size = new System.Drawing.Size( 304, 21 );
      this.txtProxyPassword.TabIndex = 20;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point( 24, 96 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 53, 13 );
      this.label3.TabIndex = 19;
      this.label3.Text = "Password";
      // 
      // txtProxyUserName
      // 
      this.txtProxyUserName.Enabled = false;
      this.txtProxyUserName.Location = new System.Drawing.Point( 96, 72 );
      this.txtProxyUserName.Name = "txtProxyUserName";
      this.txtProxyUserName.Size = new System.Drawing.Size( 304, 21 );
      this.txtProxyUserName.TabIndex = 18;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point( 24, 72 );
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size( 58, 13 );
      this.label4.TabIndex = 17;
      this.label4.Text = "User name";
      // 
      // txtProxyPort
      // 
      this.txtProxyPort.Enabled = false;
      this.txtProxyPort.Location = new System.Drawing.Point( 336, 48 );
      this.txtProxyPort.Maximum = new decimal( new int[] {
            65536,
            0,
            0,
            0} );
      this.txtProxyPort.Name = "txtProxyPort";
      this.txtProxyPort.Size = new System.Drawing.Size( 64, 21 );
      this.txtProxyPort.TabIndex = 16;
      // 
      // txtProxyAddress
      // 
      this.txtProxyAddress.Enabled = false;
      this.txtProxyAddress.Location = new System.Drawing.Point( 96, 48 );
      this.txtProxyAddress.Name = "txtProxyAddress";
      this.txtProxyAddress.Size = new System.Drawing.Size( 184, 21 );
      this.txtProxyAddress.TabIndex = 14;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point( 296, 48 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 27, 13 );
      this.label1.TabIndex = 15;
      this.label1.Text = "Port";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point( 24, 48 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 46, 13 );
      this.label2.TabIndex = 13;
      this.label2.Text = "Address";
      // 
      // chkUseHttpProxy
      // 
      this.chkUseHttpProxy.Location = new System.Drawing.Point( 16, 24 );
      this.chkUseHttpProxy.Name = "chkUseHttpProxy";
      this.chkUseHttpProxy.Size = new System.Drawing.Size( 376, 16 );
      this.chkUseHttpProxy.TabIndex = 12;
      this.chkUseHttpProxy.Text = "Connect throught an &HTTP Proxy";
      this.chkUseHttpProxy.CheckedChanged += new System.EventHandler( this.chkUseHttpProxy_CheckedChanged );
      // 
      // ConnectionInformation
      // 
      this.AcceptButton = this.cmdOk;
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 14 );
      this.CancelButton = this.cmdCancel;
      this.ClientSize = new System.Drawing.Size( 426, 376 );
      this.Controls.Add( this.grpHttpProxy );
      this.Controls.Add( this.cmdOk );
      this.Controls.Add( this.cmdCancel );
      this.Controls.Add( this.grpCredential );
      this.Controls.Add( this.grpHost );
      this.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConnectionInformation";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Connection Information";
      this.grpHost.ResumeLayout( false );
      this.grpHost.PerformLayout();
      ( ( System.ComponentModel.ISupportInitialize )( this.txtPort ) ).EndInit();
      this.grpCredential.ResumeLayout( false );
      this.grpCredential.PerformLayout();
      this.grpHttpProxy.ResumeLayout( false );
      this.grpHttpProxy.PerformLayout();
      ( ( System.ComponentModel.ISupportInitialize )( this.txtProxyPort ) ).EndInit();
      this.ResumeLayout( false );

    }
		#endregion
	}
}
