/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [OptionsForm.cs]
 * 
 * Form used to sets global application options.
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
using Xceed.Compression;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples
{
	public class OptionsForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

		public OptionsForm()
		{
			InitializeComponent();

      // Fill the combo boxes with values of the corresponding enumeration.
      this.CompressionLevelCombo.DataSource = Enum.GetValues( typeof( CompressionLevel ) );
      this.CompressionMethodCombo.DataSource = Enum.GetValues( typeof( CompressionMethod ) );
      this.EncryptionMethodCombo.DataSource = Enum.GetValues( typeof( EncryptionMethod ) );

      this.ManageProxyControlState();
    }

    #endregion CONSTRUCTORS

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

    #region PRIVATE METHODS

    private void InitializeValues()
    {
      // GZIP
      this.AllowMultipleFilesCheck.Checked = Options.GZipAllowMultipleFiles;

      // ZIP
      this.CompressionLevelCombo.SelectedItem = Options.ZipDefaultCompressionLevel;
      this.CompressionMethodCombo.SelectedItem = Options.ZipDefaultCompressionMethod;
      this.FileTimesExtraHeaderCheck.Checked = ( ( Options.ZipDefaultExtraHeaders & ExtraHeaders.FileTimes ) == ExtraHeaders.FileTimes );
      this.UnicodeExtraHeaderCheck.Checked = ( ( Options.ZipDefaultExtraHeaders & ExtraHeaders.Unicode ) == ExtraHeaders.Unicode );
      this.EncryptionPasswordText.Text = Options.ZipDefaultEncryptionPassword;
      this.ConfirmPasswordText.Text = Options.ZipDefaultEncryptionPassword;
      this.EncryptionMethodCombo.SelectedItem = Options.ZipDefaultEncryptionMethod;

      // FTP
      this.ProxyCheckBox.Checked = Options.FtpConnectThroughProxy;
      this.ProxyAddressText.Text = Options.FtpProxyServerAddress;
      this.ProxyAddressPort.Value = Options.FtpProxyServerPort;
      this.ProxyUsernameText.Text = Options.FtpProxyUsername;
      this.ProxyPassword.Text = Options.FtpProxyPassword;
    }

    private bool ValidateValues()
    {
      if( this.EncryptionPasswordText.Text != this.ConfirmPasswordText.Text )
      {
        MessageBox.Show( this, "The encryption passwords don't match.", "Encryption password error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        return false;
      }

      if(  ( this.ProxyCheckBox.Checked ) 
        && ( this.ProxyAddressText.Text.Length > 0 ) )
      {
        try
        {
          System.Net.IPHostEntry host = System.Net.Dns.Resolve( this.ProxyAddressText.Text );
        }
        catch( Exception except )
        {
          MessageBox.Show( this, "Unable to resolve the proxy server's address.\n\n" + except.Message, "DNS error", MessageBoxButtons.OK, MessageBoxIcon.Error );
          return false;
        }
      }

      return true;
    }

    private void SaveValues()
    {
      // GZIP
      Options.GZipAllowMultipleFiles = this.AllowMultipleFilesCheck.Checked;

      // ZIP
      Options.ZipDefaultCompressionLevel = ( CompressionLevel )this.CompressionLevelCombo.SelectedItem;
      Options.ZipDefaultCompressionMethod = ( CompressionMethod )this.CompressionMethodCombo.SelectedItem;

      ExtraHeaders extraHeaders = ExtraHeaders.None;
      
      if( this.FileTimesExtraHeaderCheck.Checked )
        extraHeaders |= ExtraHeaders.FileTimes;

      if( this.UnicodeExtraHeaderCheck.Checked )
        extraHeaders |= ExtraHeaders.Unicode;

      Options.ZipDefaultExtraHeaders = extraHeaders;

      Options.ZipDefaultEncryptionPassword = this.EncryptionPasswordText.Text;
      Options.ZipLastDecryptionPasswordUsed = this.EncryptionPasswordText.Text;
      Options.ZipDefaultEncryptionMethod = ( EncryptionMethod )this.EncryptionMethodCombo.SelectedItem;

      // FTP
      Options.FtpConnectThroughProxy = this.ProxyCheckBox.Checked;
      Options.FtpProxyServerAddress = ( Options.FtpConnectThroughProxy ? this.ProxyAddressText.Text : string.Empty );
      Options.FtpProxyServerPort = ( Options.FtpConnectThroughProxy ? ( int )this.ProxyAddressPort.Value : 8080 );
      Options.FtpProxyUsername = ( Options.FtpConnectThroughProxy ? this.ProxyUsernameText.Text : string.Empty );
      Options.FtpProxyPassword = ( Options.FtpConnectThroughProxy ? this.ProxyPassword.Text : string.Empty );
    }

    private void ManageProxyControlState()
    {
      this.ProxyAddressText.Enabled = this.ProxyCheckBox.Checked;
      this.ProxyAddressPort.Enabled = this.ProxyCheckBox.Checked;
      this.ProxyUsernameText.Enabled = this.ProxyCheckBox.Checked;
      this.ProxyPassword.Enabled = this.ProxyCheckBox.Checked;
    }

    #endregion PRIVATE METHODS

    #region EVENT HANDLERS

    private void OptionsForm_Load(object sender, System.EventArgs e)
    {
      this.InitializeValues();
    }

    private void OptionsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if( this.DialogResult == DialogResult.Cancel )
        return;

      if( !this.ValidateValues() )
      {
        e.Cancel = true;
        return;
      }

      this.SaveValues();
    }

    private void ProxyCheckBox_CheckedChanged(object sender, System.EventArgs e)
    {
      this.ManageProxyControlState();
    }

    #endregion EVENT HANDLERS

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OptionsForm));
      this.ExtraHeadersGroup = new System.Windows.Forms.GroupBox();
      this.FileTimesExtraHeaderCheck = new System.Windows.Forms.CheckBox();
      this.UnicodeExtraHeaderCheck = new System.Windows.Forms.CheckBox();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.OkBtn = new System.Windows.Forms.Button();
      this.CompressionGroup = new System.Windows.Forms.GroupBox();
      this.CompressionMethodCombo = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.CompressionLevelCombo = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.ZipArchivesPage = new System.Windows.Forms.TabPage();
      this.EncryptionBox = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.EncryptionMethodCombo = new System.Windows.Forms.ComboBox();
      this.ConfirmPasswordText = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.EncryptionPasswordText = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.GZipArchivesPage = new System.Windows.Forms.TabPage();
      this.AllowMultipleFilesCheck = new System.Windows.Forms.CheckBox();
      this.FtpConnectionsPage = new System.Windows.Forms.TabPage();
      this.ProxyGroup = new System.Windows.Forms.GroupBox();
      this.ProxyPassword = new System.Windows.Forms.TextBox();
      this.ProxyUsernameText = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.ProxyAddressPort = new System.Windows.Forms.NumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.ProxyAddressText = new System.Windows.Forms.TextBox();
      this.Address = new System.Windows.Forms.Label();
      this.ProxyCheckBox = new System.Windows.Forms.CheckBox();
      this.ExtraHeadersGroup.SuspendLayout();
      this.CompressionGroup.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.ZipArchivesPage.SuspendLayout();
      this.EncryptionBox.SuspendLayout();
      this.GZipArchivesPage.SuspendLayout();
      this.FtpConnectionsPage.SuspendLayout();
      this.ProxyGroup.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.ProxyAddressPort)).BeginInit();
      this.SuspendLayout();
      // 
      // ExtraHeadersGroup
      // 
      this.ExtraHeadersGroup.Controls.Add(this.FileTimesExtraHeaderCheck);
      this.ExtraHeadersGroup.Controls.Add(this.UnicodeExtraHeaderCheck);
      this.ExtraHeadersGroup.Location = new System.Drawing.Point(8, 96);
      this.ExtraHeadersGroup.Name = "ExtraHeadersGroup";
      this.ExtraHeadersGroup.Size = new System.Drawing.Size(320, 72);
      this.ExtraHeadersGroup.TabIndex = 6;
      this.ExtraHeadersGroup.TabStop = false;
      this.ExtraHeadersGroup.Text = "Extra headers";
      // 
      // FileTimesExtraHeaderCheck
      // 
      this.FileTimesExtraHeaderCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.FileTimesExtraHeaderCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.FileTimesExtraHeaderCheck.Location = new System.Drawing.Point(16, 48);
      this.FileTimesExtraHeaderCheck.Name = "FileTimesExtraHeaderCheck";
      this.FileTimesExtraHeaderCheck.Size = new System.Drawing.Size(296, 16);
      this.FileTimesExtraHeaderCheck.TabIndex = 8;
      this.FileTimesExtraHeaderCheck.Text = "File times (creation, modification and last accessed)";
      // 
      // UnicodeExtraHeaderCheck
      // 
      this.UnicodeExtraHeaderCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.UnicodeExtraHeaderCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.UnicodeExtraHeaderCheck.Location = new System.Drawing.Point(16, 24);
      this.UnicodeExtraHeaderCheck.Name = "UnicodeExtraHeaderCheck";
      this.UnicodeExtraHeaderCheck.Size = new System.Drawing.Size(296, 16);
      this.UnicodeExtraHeaderCheck.TabIndex = 7;
      this.UnicodeExtraHeaderCheck.Text = "Unicode";
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point(264, 328);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(88, 24);
      this.CancelBtn.TabIndex = 18;
      this.CancelBtn.Text = "&Cancel";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.OkBtn.Location = new System.Drawing.Point(168, 328);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(88, 24);
      this.OkBtn.TabIndex = 17;
      this.OkBtn.Text = "&Ok";
      // 
      // CompressionGroup
      // 
      this.CompressionGroup.Controls.Add(this.CompressionMethodCombo);
      this.CompressionGroup.Controls.Add(this.label2);
      this.CompressionGroup.Controls.Add(this.CompressionLevelCombo);
      this.CompressionGroup.Controls.Add(this.label1);
      this.CompressionGroup.Location = new System.Drawing.Point(8, 8);
      this.CompressionGroup.Name = "CompressionGroup";
      this.CompressionGroup.Size = new System.Drawing.Size(320, 80);
      this.CompressionGroup.TabIndex = 1;
      this.CompressionGroup.TabStop = false;
      this.CompressionGroup.Text = "Compression";
      // 
      // CompressionMethodCombo
      // 
      this.CompressionMethodCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.CompressionMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CompressionMethodCombo.Location = new System.Drawing.Point(144, 48);
      this.CompressionMethodCombo.Name = "CompressionMethodCombo";
      this.CompressionMethodCombo.Size = new System.Drawing.Size(168, 21);
      this.CompressionMethodCombo.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(12, 50);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(112, 16);
      this.label2.TabIndex = 4;
      this.label2.Text = "Compression method";
      // 
      // CompressionLevelCombo
      // 
      this.CompressionLevelCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right)));
      this.CompressionLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.CompressionLevelCombo.Location = new System.Drawing.Point(144, 24);
      this.CompressionLevelCombo.Name = "CompressionLevelCombo";
      this.CompressionLevelCombo.Size = new System.Drawing.Size(168, 21);
      this.CompressionLevelCombo.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(12, 24);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(112, 16);
      this.label1.TabIndex = 2;
      this.label1.Text = "Compression level";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.ZipArchivesPage);
      this.tabControl1.Controls.Add(this.GZipArchivesPage);
      this.tabControl1.Controls.Add(this.FtpConnectionsPage);
      this.tabControl1.Location = new System.Drawing.Point(8, 8);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(344, 312);
      this.tabControl1.TabIndex = 0;
      // 
      // ZipArchivesPage
      // 
      this.ZipArchivesPage.Controls.Add(this.EncryptionBox);
      this.ZipArchivesPage.Controls.Add(this.ExtraHeadersGroup);
      this.ZipArchivesPage.Controls.Add(this.CompressionGroup);
      this.ZipArchivesPage.Location = new System.Drawing.Point(4, 22);
      this.ZipArchivesPage.Name = "ZipArchivesPage";
      this.ZipArchivesPage.Size = new System.Drawing.Size(336, 286);
      this.ZipArchivesPage.TabIndex = 0;
      this.ZipArchivesPage.Text = "Zip archives";
      // 
      // EncryptionBox
      // 
      this.EncryptionBox.Controls.Add(this.label5);
      this.EncryptionBox.Controls.Add(this.EncryptionMethodCombo);
      this.EncryptionBox.Controls.Add(this.ConfirmPasswordText);
      this.EncryptionBox.Controls.Add(this.label4);
      this.EncryptionBox.Controls.Add(this.EncryptionPasswordText);
      this.EncryptionBox.Controls.Add(this.label3);
      this.EncryptionBox.Location = new System.Drawing.Point(8, 176);
      this.EncryptionBox.Name = "EncryptionBox";
      this.EncryptionBox.Size = new System.Drawing.Size(320, 104);
      this.EncryptionBox.TabIndex = 9;
      this.EncryptionBox.TabStop = false;
      this.EncryptionBox.Text = "Encryption (set a password to encrypt new files)";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(16, 72);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(100, 16);
      this.label5.TabIndex = 14;
      this.label5.Text = "Encryption method";
      // 
      // EncryptionMethodCombo
      // 
      this.EncryptionMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.EncryptionMethodCombo.Location = new System.Drawing.Point(144, 72);
      this.EncryptionMethodCombo.Name = "EncryptionMethodCombo";
      this.EncryptionMethodCombo.Size = new System.Drawing.Size(168, 21);
      this.EncryptionMethodCombo.TabIndex = 15;
      // 
      // ConfirmPasswordText
      // 
      this.ConfirmPasswordText.Location = new System.Drawing.Point(144, 48);
      this.ConfirmPasswordText.MaxLength = 80;
      this.ConfirmPasswordText.Name = "ConfirmPasswordText";
      this.ConfirmPasswordText.PasswordChar = '*';
      this.ConfirmPasswordText.Size = new System.Drawing.Size(168, 20);
      this.ConfirmPasswordText.TabIndex = 13;
      this.ConfirmPasswordText.Text = "";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(16, 48);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(112, 16);
      this.label4.TabIndex = 12;
      this.label4.Text = "Confirm password";
      // 
      // EncryptionPasswordText
      // 
      this.EncryptionPasswordText.Location = new System.Drawing.Point(144, 24);
      this.EncryptionPasswordText.MaxLength = 80;
      this.EncryptionPasswordText.Name = "EncryptionPasswordText";
      this.EncryptionPasswordText.PasswordChar = '*';
      this.EncryptionPasswordText.Size = new System.Drawing.Size(168, 20);
      this.EncryptionPasswordText.TabIndex = 11;
      this.EncryptionPasswordText.Text = "";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(16, 24);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(112, 16);
      this.label3.TabIndex = 10;
      this.label3.Text = "Encryption password";
      // 
      // GZipArchivesPage
      // 
      this.GZipArchivesPage.Controls.Add(this.AllowMultipleFilesCheck);
      this.GZipArchivesPage.Location = new System.Drawing.Point(4, 22);
      this.GZipArchivesPage.Name = "GZipArchivesPage";
      this.GZipArchivesPage.Size = new System.Drawing.Size(336, 286);
      this.GZipArchivesPage.TabIndex = 1;
      this.GZipArchivesPage.Text = "GZip archives";
      // 
      // AllowMultipleFilesCheck
      // 
      this.AllowMultipleFilesCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.AllowMultipleFilesCheck.Location = new System.Drawing.Point(16, 16);
      this.AllowMultipleFilesCheck.Name = "AllowMultipleFilesCheck";
      this.AllowMultipleFilesCheck.Size = new System.Drawing.Size(312, 16);
      this.AllowMultipleFilesCheck.TabIndex = 16;
      this.AllowMultipleFilesCheck.Text = "Allow multiple files";
      // 
      // FtpConnectionsPage
      // 
      this.FtpConnectionsPage.Controls.Add(this.ProxyGroup);
      this.FtpConnectionsPage.Location = new System.Drawing.Point(4, 22);
      this.FtpConnectionsPage.Name = "FtpConnectionsPage";
      this.FtpConnectionsPage.Size = new System.Drawing.Size(336, 286);
      this.FtpConnectionsPage.TabIndex = 2;
      this.FtpConnectionsPage.Text = "FTP connections";
      // 
      // ProxyGroup
      // 
      this.ProxyGroup.Controls.Add(this.ProxyPassword);
      this.ProxyGroup.Controls.Add(this.ProxyUsernameText);
      this.ProxyGroup.Controls.Add(this.label8);
      this.ProxyGroup.Controls.Add(this.label7);
      this.ProxyGroup.Controls.Add(this.ProxyAddressPort);
      this.ProxyGroup.Controls.Add(this.label6);
      this.ProxyGroup.Controls.Add(this.ProxyAddressText);
      this.ProxyGroup.Controls.Add(this.Address);
      this.ProxyGroup.Controls.Add(this.ProxyCheckBox);
      this.ProxyGroup.Location = new System.Drawing.Point(8, 8);
      this.ProxyGroup.Name = "ProxyGroup";
      this.ProxyGroup.Size = new System.Drawing.Size(320, 136);
      this.ProxyGroup.TabIndex = 0;
      this.ProxyGroup.TabStop = false;
      this.ProxyGroup.Text = "Proxy";
      // 
      // ProxyPassword
      // 
      this.ProxyPassword.Location = new System.Drawing.Point(96, 104);
      this.ProxyPassword.Name = "ProxyPassword";
      this.ProxyPassword.PasswordChar = '*';
      this.ProxyPassword.Size = new System.Drawing.Size(216, 20);
      this.ProxyPassword.TabIndex = 8;
      this.ProxyPassword.Text = "";
      // 
      // ProxyUsernameText
      // 
      this.ProxyUsernameText.Location = new System.Drawing.Point(96, 80);
      this.ProxyUsernameText.Name = "ProxyUsernameText";
      this.ProxyUsernameText.Size = new System.Drawing.Size(216, 20);
      this.ProxyUsernameText.TabIndex = 7;
      this.ProxyUsernameText.Text = "";
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(32, 104);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(64, 16);
      this.label8.TabIndex = 6;
      this.label8.Text = "Password";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(32, 80);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(64, 16);
      this.label7.TabIndex = 5;
      this.label7.Text = "User name";
      // 
      // ProxyAddressPort
      // 
      this.ProxyAddressPort.Location = new System.Drawing.Point(256, 48);
      this.ProxyAddressPort.Maximum = new System.Decimal(new int[] {
                                                                     65535,
                                                                     0,
                                                                     0,
                                                                     0});
      this.ProxyAddressPort.Name = "ProxyAddressPort";
      this.ProxyAddressPort.Size = new System.Drawing.Size(56, 20);
      this.ProxyAddressPort.TabIndex = 4;
      this.ProxyAddressPort.Value = new System.Decimal(new int[] {
                                                                   8080,
                                                                   0,
                                                                   0,
                                                                   0});
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(216, 48);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(32, 16);
      this.label6.TabIndex = 3;
      this.label6.Text = "Port";
      // 
      // ProxyAddressText
      // 
      this.ProxyAddressText.Location = new System.Drawing.Point(96, 48);
      this.ProxyAddressText.Name = "ProxyAddressText";
      this.ProxyAddressText.Size = new System.Drawing.Size(104, 20);
      this.ProxyAddressText.TabIndex = 2;
      this.ProxyAddressText.Text = "";
      // 
      // Address
      // 
      this.Address.Location = new System.Drawing.Point(32, 48);
      this.Address.Name = "Address";
      this.Address.Size = new System.Drawing.Size(56, 16);
      this.Address.TabIndex = 1;
      this.Address.Text = "Address";
      // 
      // ProxyCheckBox
      // 
      this.ProxyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ProxyCheckBox.Location = new System.Drawing.Point(16, 24);
      this.ProxyCheckBox.Name = "ProxyCheckBox";
      this.ProxyCheckBox.Size = new System.Drawing.Size(296, 16);
      this.ProxyCheckBox.TabIndex = 0;
      this.ProxyCheckBox.Text = "Connect through an HTTP proxy server";
      this.ProxyCheckBox.CheckedChanged += new System.EventHandler(this.ProxyCheckBox_CheckedChanged);
      // 
      // OptionsForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.CancelBtn;
      this.ClientSize = new System.Drawing.Size(362, 360);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.CancelBtn);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "OptionsForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Options";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.OptionsForm_Closing);
      this.Load += new System.EventHandler(this.OptionsForm_Load);
      this.ExtraHeadersGroup.ResumeLayout(false);
      this.CompressionGroup.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.ZipArchivesPage.ResumeLayout(false);
      this.EncryptionBox.ResumeLayout(false);
      this.GZipArchivesPage.ResumeLayout(false);
      this.FtpConnectionsPage.ResumeLayout(false);
      this.ProxyGroup.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.ProxyAddressPort)).EndInit();
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated members

    private System.Windows.Forms.GroupBox ExtraHeadersGroup;
    private System.Windows.Forms.CheckBox FileTimesExtraHeaderCheck;
    private System.Windows.Forms.CheckBox UnicodeExtraHeaderCheck;
    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.GroupBox CompressionGroup;
    private System.Windows.Forms.ComboBox CompressionMethodCombo;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox CompressionLevelCombo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage ZipArchivesPage;
    private System.Windows.Forms.TabPage GZipArchivesPage;
    private System.Windows.Forms.CheckBox AllowMultipleFilesCheck;
    private System.Windows.Forms.GroupBox EncryptionBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox EncryptionPasswordText;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox ConfirmPasswordText;
    private System.Windows.Forms.ComboBox EncryptionMethodCombo;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TabPage FtpConnectionsPage;
    private System.Windows.Forms.GroupBox ProxyGroup;
    private System.Windows.Forms.CheckBox ProxyCheckBox;
    private System.Windows.Forms.Label Address;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox ProxyAddressText;
    private System.Windows.Forms.NumericUpDown ProxyAddressPort;
    private System.Windows.Forms.TextBox ProxyUsernameText;
    private System.Windows.Forms.TextBox ProxyPassword;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated members
	}
}
