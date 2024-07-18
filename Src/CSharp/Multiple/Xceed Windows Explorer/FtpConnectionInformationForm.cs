/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [FtpConnectionInformationForm.cs]
 * 
 * Form used to get Ftp connection information from the user.
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
using Xceed.Ftp;

namespace Xceed.FileSystem.Samples
{
  public class FtpConnectionInformationForm : System.Windows.Forms.Form
  {
    #region CONSTRUCTORS

    public FtpConnectionInformationForm()
    {
      InitializeComponent();

      // Fill the combo
      this.AuthenticationMethodCombo.DataSource = Enum.GetValues( typeof( Xceed.Ftp.AuthenticationMethod ) );
    }

    #endregion CONSTRUCTORS

    #region PUBLIC METHODS

    public DialogResult ShowDialog( IWin32Window owner, out FtpConnection connection )
    {
      connection = null;

      DialogResult result = this.ShowDialog( owner );

      if( result == DialogResult.OK )
      {
        // Default credential for anonymous connection.
        string userName = "anonymous";
        string password = "guest";

        if( !this.AnonymousConnectionCheck.Checked )
        {
          userName = this.UserNameText.Text;
          password = this.PasswordText.Text;
        }

        // Create a new Ftp connection with the specified information.
        AuthenticationMethod authenticationMethod = ( AuthenticationMethod )this.AuthenticationMethodCombo.SelectedValue;

        if( authenticationMethod != AuthenticationMethod.None )
        {
          connection = new FtpConnection(
            this.ServerAddressText.Text,
            ( int )this.PortUpDown.Value, 
            userName,
            password,
            authenticationMethod,
            VerificationFlags.None,
            null, 
            DataChannelProtection.Private,
            ImplicitEncryptionCheck.Checked );
        }
        else
        {
          connection = new FtpConnection(
            this.ServerAddressText.Text,
            ( int )this.PortUpDown.Value,
            userName,
            password );
        }

        // Configure the proxy on the connection.
        if( Options.FtpConnectThroughProxy )
          connection.Proxy = new HttpProxyClient( Options.FtpProxyServerAddress, Options.FtpProxyServerPort, Options.FtpProxyUsername, Options.FtpProxyPassword );

        // Configure TransferMode on the connection.
        if( this.ModeZCheckBox.Checked )
          connection.TransferMode = TransferMode.ZLibCompressed;
      }

      return result;
    }

    #endregion PUBLIC METHODS

    #region EVENT HANDLERS

    private void FtpConnectionInformationForm_Closing( object sender, System.ComponentModel.CancelEventArgs e )
    {
      if( this.DialogResult == DialogResult.Cancel )
        return;

      if( !this.ValidateValues() )
      {
        e.Cancel = true;
        return;
      }
    }

    private void AnonymousConnectionCheck_CheckedChanged(object sender, System.EventArgs e)
    {
      this.UserNameText.ReadOnly = this.AnonymousConnectionCheck.Checked;
      this.PasswordText.ReadOnly = this.AnonymousConnectionCheck.Checked;
    }

    #endregion EVENT HANDLERS

    #region PRIVATE METHODS
    
    private bool ValidateValues()
    {
      if( this.ServerAddressText.Text.Length == 0 )
      {
        MessageBox.Show( this, "You must specify a server address.", "Incomplete information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        return false;
      }

      if(  ( !this.AnonymousConnectionCheck.Checked )
        && ( this.UserNameText.Text.Length == 0 ) )
      {
        MessageBox.Show( this, "You must specify a user name.", "Incomplete information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
        return false;
      }

      return true;
    }

    #endregion PRIVATE METHODS

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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( FtpConnectionInformationForm ) );
      this.label1 = new System.Windows.Forms.Label();
      this.ServerAddressText = new System.Windows.Forms.TextBox();
      this.AnonymousConnectionCheck = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.UserNameText = new System.Windows.Forms.TextBox();
      this.PasswordText = new System.Windows.Forms.TextBox();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.OkBtn = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.PortUpDown = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.AuthenticationMethodCombo = new System.Windows.Forms.ComboBox();
      this.ImplicitEncryptionCheck = new System.Windows.Forms.CheckBox();
      this.ModeZCheckBox = new System.Windows.Forms.CheckBox();
      ( ( System.ComponentModel.ISupportInitialize )( this.PortUpDown ) ).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point( 16, 16 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 96, 16 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Server address:";
      // 
      // ServerAddressText
      // 
      this.ServerAddressText.Location = new System.Drawing.Point( 128, 16 );
      this.ServerAddressText.Name = "ServerAddressText";
      this.ServerAddressText.Size = new System.Drawing.Size( 200, 20 );
      this.ServerAddressText.TabIndex = 0;
      // 
      // AnonymousConnectionCheck
      // 
      this.AnonymousConnectionCheck.Checked = true;
      this.AnonymousConnectionCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.AnonymousConnectionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.AnonymousConnectionCheck.Location = new System.Drawing.Point( 16, 169 );
      this.AnonymousConnectionCheck.Name = "AnonymousConnectionCheck";
      this.AnonymousConnectionCheck.Size = new System.Drawing.Size( 312, 16 );
      this.AnonymousConnectionCheck.TabIndex = 5;
      this.AnonymousConnectionCheck.Text = "Anonymous connection";
      this.AnonymousConnectionCheck.CheckedChanged += new System.EventHandler( this.AnonymousConnectionCheck_CheckedChanged );
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point( 32, 193 );
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size( 72, 16 );
      this.label2.TabIndex = 3;
      this.label2.Text = "User name:";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point( 32, 217 );
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size( 72, 16 );
      this.label3.TabIndex = 4;
      this.label3.Text = "Password:";
      // 
      // UserNameText
      // 
      this.UserNameText.Location = new System.Drawing.Point( 128, 193 );
      this.UserNameText.Name = "UserNameText";
      this.UserNameText.ReadOnly = true;
      this.UserNameText.Size = new System.Drawing.Size( 200, 20 );
      this.UserNameText.TabIndex = 6;
      // 
      // PasswordText
      // 
      this.PasswordText.Location = new System.Drawing.Point( 128, 217 );
      this.PasswordText.Name = "PasswordText";
      this.PasswordText.PasswordChar = '*';
      this.PasswordText.ReadOnly = true;
      this.PasswordText.Size = new System.Drawing.Size( 200, 20 );
      this.PasswordText.TabIndex = 7;
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point( 248, 249 );
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size( 80, 24 );
      this.CancelBtn.TabIndex = 9;
      this.CancelBtn.Text = "&Cancel";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.OkBtn.Location = new System.Drawing.Point( 160, 249 );
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size( 80, 24 );
      this.OkBtn.TabIndex = 8;
      this.OkBtn.Text = "&Ok";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point( 16, 40 );
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size( 96, 16 );
      this.label4.TabIndex = 2;
      this.label4.Text = "Port:";
      // 
      // PortUpDown
      // 
      this.PortUpDown.Location = new System.Drawing.Point( 128, 40 );
      this.PortUpDown.Maximum = new decimal( new int[] {
            65535,
            0,
            0,
            0} );
      this.PortUpDown.Name = "PortUpDown";
      this.PortUpDown.Size = new System.Drawing.Size( 72, 20 );
      this.PortUpDown.TabIndex = 1;
      this.PortUpDown.Value = new decimal( new int[] {
            21,
            0,
            0,
            0} );
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point( 16, 72 );
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size( 104, 32 );
      this.label5.TabIndex = 4;
      this.label5.Text = "Authentication method";
      // 
      // AuthenticationMethodCombo
      // 
      this.AuthenticationMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.AuthenticationMethodCombo.Location = new System.Drawing.Point( 128, 72 );
      this.AuthenticationMethodCombo.Name = "AuthenticationMethodCombo";
      this.AuthenticationMethodCombo.Size = new System.Drawing.Size( 200, 21 );
      this.AuthenticationMethodCombo.TabIndex = 2;
      // 
      // ImplicitEncryptionCheck
      // 
      this.ImplicitEncryptionCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ImplicitEncryptionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ImplicitEncryptionCheck.Location = new System.Drawing.Point( 16, 137 );
      this.ImplicitEncryptionCheck.Name = "ImplicitEncryptionCheck";
      this.ImplicitEncryptionCheck.Size = new System.Drawing.Size( 128, 16 );
      this.ImplicitEncryptionCheck.TabIndex = 4;
      this.ImplicitEncryptionCheck.Text = "Implicit encryption";
      // 
      // ModeZCheckBox
      // 
      this.ModeZCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.ModeZCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ModeZCheckBox.Location = new System.Drawing.Point( 16, 107 );
      this.ModeZCheckBox.Name = "ModeZCheckBox";
      this.ModeZCheckBox.Size = new System.Drawing.Size( 128, 16 );
      this.ModeZCheckBox.TabIndex = 3;
      this.ModeZCheckBox.Text = "Use Mode Z";
      // 
      // FtpConnectionInformationForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.CancelButton = this.CancelBtn;
      this.ClientSize = new System.Drawing.Size( 336, 277 );
      this.Controls.Add( this.ModeZCheckBox );
      this.Controls.Add( this.ImplicitEncryptionCheck );
      this.Controls.Add( this.AuthenticationMethodCombo );
      this.Controls.Add( this.label5 );
      this.Controls.Add( this.PortUpDown );
      this.Controls.Add( this.label4 );
      this.Controls.Add( this.OkBtn );
      this.Controls.Add( this.CancelBtn );
      this.Controls.Add( this.PasswordText );
      this.Controls.Add( this.UserNameText );
      this.Controls.Add( this.AnonymousConnectionCheck );
      this.Controls.Add( this.ServerAddressText );
      this.Controls.Add( this.label3 );
      this.Controls.Add( this.label2 );
      this.Controls.Add( this.label1 );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
      this.MaximizeBox = false;
      this.Name = "FtpConnectionInformationForm";
      this.Text = "Ftp connection information";
      this.Closing += new System.ComponentModel.CancelEventHandler( this.FtpConnectionInformationForm_Closing );
      ( ( System.ComponentModel.ISupportInitialize )( this.PortUpDown ) ).EndInit();
      this.ResumeLayout( false );
      this.PerformLayout();

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox ServerAddressText;
    private System.Windows.Forms.CheckBox AnonymousConnectionCheck;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox UserNameText;
    private System.Windows.Forms.TextBox PasswordText;
    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown PortUpDown;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.ComboBox AuthenticationMethodCombo;
    private System.Windows.Forms.CheckBox ImplicitEncryptionCheck;
    private CheckBox ModeZCheckBox;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated fields
	}
}