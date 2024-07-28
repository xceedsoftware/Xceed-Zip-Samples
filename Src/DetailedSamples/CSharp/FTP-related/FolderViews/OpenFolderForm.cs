using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Ftp;

namespace FolderViews
{
	/// <summary>
	/// Summary description for OpenFolderForm.
	/// </summary>
	public class OpenFolderForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button openButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.RadioButton drivesRadio;
    private System.Windows.Forms.RadioButton networkRadio;
    private System.Windows.Forms.RadioButton isolatedRadio;
    private System.Windows.Forms.Label networkLabel;
    private System.Windows.Forms.TextBox networkTextBox;
    private System.Windows.Forms.RadioButton memoryRadio;
    private System.Windows.Forms.RadioButton ftpRadio;
    private System.Windows.Forms.Label userLabel;
    private System.Windows.Forms.Label passwordLabel;
    private System.Windows.Forms.TextBox userTextBox;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.TextBox hostTextBox;
    private System.Windows.Forms.Label hostLabel;
    private System.Windows.Forms.TextBox portTextBox;
    private System.Windows.Forms.Label portLabel;
    private CheckBox useModeZCheckBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OpenFolderForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
      this.drivesRadio = new System.Windows.Forms.RadioButton();
      this.networkRadio = new System.Windows.Forms.RadioButton();
      this.isolatedRadio = new System.Windows.Forms.RadioButton();
      this.label1 = new System.Windows.Forms.Label();
      this.networkLabel = new System.Windows.Forms.Label();
      this.networkTextBox = new System.Windows.Forms.TextBox();
      this.memoryRadio = new System.Windows.Forms.RadioButton();
      this.ftpRadio = new System.Windows.Forms.RadioButton();
      this.userLabel = new System.Windows.Forms.Label();
      this.passwordLabel = new System.Windows.Forms.Label();
      this.userTextBox = new System.Windows.Forms.TextBox();
      this.passwordTextBox = new System.Windows.Forms.TextBox();
      this.hostTextBox = new System.Windows.Forms.TextBox();
      this.hostLabel = new System.Windows.Forms.Label();
      this.portTextBox = new System.Windows.Forms.TextBox();
      this.portLabel = new System.Windows.Forms.Label();
      this.openButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.useModeZCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // drivesRadio
      // 
      this.drivesRadio.Location = new System.Drawing.Point( 16, 32 );
      this.drivesRadio.Name = "drivesRadio";
      this.drivesRadio.Size = new System.Drawing.Size( 168, 16 );
      this.drivesRadio.TabIndex = 0;
      this.drivesRadio.Text = "My computer\'s logical &drives";
      // 
      // networkRadio
      // 
      this.networkRadio.Location = new System.Drawing.Point( 16, 64 );
      this.networkRadio.Name = "networkRadio";
      this.networkRadio.Size = new System.Drawing.Size( 152, 16 );
      this.networkRadio.TabIndex = 1;
      this.networkRadio.Text = "A particular &network path";
      this.networkRadio.CheckedChanged += new System.EventHandler( this.networkRadio_CheckedChanged );
      // 
      // isolatedRadio
      // 
      this.isolatedRadio.Location = new System.Drawing.Point( 16, 144 );
      this.isolatedRadio.Name = "isolatedRadio";
      this.isolatedRadio.Size = new System.Drawing.Size( 200, 16 );
      this.isolatedRadio.TabIndex = 4;
      this.isolatedRadio.Text = "The current user\'s &isolated storage";
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.label1.Location = new System.Drawing.Point( 8, 8 );
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size( 336, 16 );
      this.label1.TabIndex = 0;
      this.label1.Text = "Select folder to view:";
      // 
      // networkLabel
      // 
      this.networkLabel.Enabled = false;
      this.networkLabel.Location = new System.Drawing.Point( 48, 88 );
      this.networkLabel.Name = "networkLabel";
      this.networkLabel.Size = new System.Drawing.Size( 100, 16 );
      this.networkLabel.TabIndex = 2;
      this.networkLabel.Text = "Network path:";
      // 
      // networkTextBox
      // 
      this.networkTextBox.Enabled = false;
      this.networkTextBox.Location = new System.Drawing.Point( 48, 104 );
      this.networkTextBox.Name = "networkTextBox";
      this.networkTextBox.Size = new System.Drawing.Size( 296, 20 );
      this.networkTextBox.TabIndex = 3;
      // 
      // memoryRadio
      // 
      this.memoryRadio.Location = new System.Drawing.Point( 16, 176 );
      this.memoryRadio.Name = "memoryRadio";
      this.memoryRadio.Size = new System.Drawing.Size( 152, 16 );
      this.memoryRadio.TabIndex = 5;
      this.memoryRadio.Text = "A volatile &memory folder";
      // 
      // ftpRadio
      // 
      this.ftpRadio.Location = new System.Drawing.Point( 16, 208 );
      this.ftpRadio.Name = "ftpRadio";
      this.ftpRadio.Size = new System.Drawing.Size( 184, 16 );
      this.ftpRadio.TabIndex = 6;
      this.ftpRadio.Text = "An &FTP server\'s starting folder";
      this.ftpRadio.CheckedChanged += new System.EventHandler( this.ftpRadio_CheckedChanged );
      // 
      // userLabel
      // 
      this.userLabel.Enabled = false;
      this.userLabel.Location = new System.Drawing.Point( 48, 272 );
      this.userLabel.Name = "userLabel";
      this.userLabel.Size = new System.Drawing.Size( 112, 16 );
      this.userLabel.TabIndex = 12;
      this.userLabel.Text = "Username:";
      // 
      // passwordLabel
      // 
      this.passwordLabel.Enabled = false;
      this.passwordLabel.Location = new System.Drawing.Point( 168, 272 );
      this.passwordLabel.Name = "passwordLabel";
      this.passwordLabel.Size = new System.Drawing.Size( 104, 16 );
      this.passwordLabel.TabIndex = 14;
      this.passwordLabel.Text = "Password:";
      // 
      // userTextBox
      // 
      this.userTextBox.Enabled = false;
      this.userTextBox.Location = new System.Drawing.Point( 48, 288 );
      this.userTextBox.Name = "userTextBox";
      this.userTextBox.Size = new System.Drawing.Size( 112, 20 );
      this.userTextBox.TabIndex = 9;
      this.userTextBox.Text = "anonymous";
      // 
      // passwordTextBox
      // 
      this.passwordTextBox.Enabled = false;
      this.passwordTextBox.Location = new System.Drawing.Point( 168, 288 );
      this.passwordTextBox.Name = "passwordTextBox";
      this.passwordTextBox.Size = new System.Drawing.Size( 112, 20 );
      this.passwordTextBox.TabIndex = 10;
      this.passwordTextBox.Text = "guest";
      this.passwordTextBox.UseSystemPasswordChar = true;
      // 
      // hostTextBox
      // 
      this.hostTextBox.Enabled = false;
      this.hostTextBox.Location = new System.Drawing.Point( 48, 248 );
      this.hostTextBox.Name = "hostTextBox";
      this.hostTextBox.Size = new System.Drawing.Size( 176, 20 );
      this.hostTextBox.TabIndex = 7;
      this.hostTextBox.Text = "ftp.xceed.com";
      // 
      // hostLabel
      // 
      this.hostLabel.Enabled = false;
      this.hostLabel.Location = new System.Drawing.Point( 48, 232 );
      this.hostLabel.Name = "hostLabel";
      this.hostLabel.Size = new System.Drawing.Size( 176, 16 );
      this.hostLabel.TabIndex = 8;
      this.hostLabel.Text = "FTP server hostname or address:";
      // 
      // portTextBox
      // 
      this.portTextBox.Enabled = false;
      this.portTextBox.Location = new System.Drawing.Point( 232, 248 );
      this.portTextBox.Name = "portTextBox";
      this.portTextBox.Size = new System.Drawing.Size( 48, 20 );
      this.portTextBox.TabIndex = 8;
      this.portTextBox.Text = "21";
      // 
      // portLabel
      // 
      this.portLabel.Enabled = false;
      this.portLabel.Location = new System.Drawing.Point( 232, 232 );
      this.portLabel.Name = "portLabel";
      this.portLabel.Size = new System.Drawing.Size( 48, 16 );
      this.portLabel.TabIndex = 10;
      this.portLabel.Text = "Port:";
      // 
      // openButton
      // 
      this.openButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.openButton.Location = new System.Drawing.Point( 189, 365 );
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size( 75, 23 );
      this.openButton.TabIndex = 12;
      this.openButton.Text = "&Open";
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point( 269, 365 );
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size( 75, 23 );
      this.cancelButton.TabIndex = 13;
      this.cancelButton.Text = "&Cancel";
      // 
      // useModeZCheckBox
      // 
      this.useModeZCheckBox.AutoSize = true;
      this.useModeZCheckBox.Location = new System.Drawing.Point( 48, 325 );
      this.useModeZCheckBox.Name = "useModeZCheckBox";
      this.useModeZCheckBox.Size = new System.Drawing.Size( 85, 17 );
      this.useModeZCheckBox.TabIndex = 11;
      this.useModeZCheckBox.Text = "Use Mode Z";
      this.useModeZCheckBox.UseVisualStyleBackColor = true;
      this.useModeZCheckBox.CheckedChanged += new System.EventHandler( this.UseModeZCheckBox_CheckedChanged );
      // 
      // OpenFolderForm
      // 
      this.AcceptButton = this.openButton;
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size( 360, 400 );
      this.ControlBox = false;
      this.Controls.Add( this.useModeZCheckBox );
      this.Controls.Add( this.cancelButton );
      this.Controls.Add( this.openButton );
      this.Controls.Add( this.portTextBox );
      this.Controls.Add( this.hostTextBox );
      this.Controls.Add( this.passwordTextBox );
      this.Controls.Add( this.userTextBox );
      this.Controls.Add( this.networkTextBox );
      this.Controls.Add( this.portLabel );
      this.Controls.Add( this.hostLabel );
      this.Controls.Add( this.passwordLabel );
      this.Controls.Add( this.userLabel );
      this.Controls.Add( this.ftpRadio );
      this.Controls.Add( this.memoryRadio );
      this.Controls.Add( this.networkLabel );
      this.Controls.Add( this.label1 );
      this.Controls.Add( this.isolatedRadio );
      this.Controls.Add( this.networkRadio );
      this.Controls.Add( this.drivesRadio );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "OpenFolderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Open a new folder";
      this.Load += new System.EventHandler( this.OpenFolderForm_Load );
      this.ResumeLayout( false );
      this.PerformLayout();

    }
		#endregion

    public new FolderForm ShowDialog( IWin32Window owner )
    {
      if( base.ShowDialog( owner ) == DialogResult.OK )
      {
        AbstractFolder[] folders = null;
        string title = string.Empty;

        if( drivesRadio.Checked )
        {
          string[] drives = System.IO.Directory.GetLogicalDrives();
          folders = new AbstractFolder[ drives.Length ];

          for( int index=0; index<drives.Length; index++ )
          {
            try
            {
              folders[ index ] = new DiskFolder( drives[ index ] );
            }
            catch {}
          }

          title = "My Computer's logical drives";
        }
        else if( networkRadio.Checked )
        {
          try
          {
            folders = new AbstractFolder[] { new DiskFolder( networkTextBox.Text ) };
          }
          catch( Exception except )
          {
            MessageBox.Show( 
              "The path \"" + networkTextBox.Text + "\" could not be found.\n" + except.Message,
              "Error",
              MessageBoxButtons.OK, 
              MessageBoxIcon.Error );

            return null;
          }

          title = "Path: " + networkTextBox.Text;
        }
        else if( isolatedRadio.Checked )
        {
          try
          {
            folders = new AbstractFolder[] { new IsolatedFolder( @"\" ) };
          }
          catch( Exception except )
          {
            MessageBox.Show( 
              "There was an error accessing the isolated storage.\n" + except.Message,
              "Error",
              MessageBoxButtons.OK, 
              MessageBoxIcon.Error );

            return null;
          }

          title = "Isolated storage";
        }
        else if( memoryRadio.Checked )
        {
          folders = new AbstractFolder[] { new MemoryFolder() };
          title = "Memory folder: " + folders[ 0 ].FullName;
        }
        else if( ftpRadio.Checked )
        {
          try
          {
            FtpConnection connection = new FtpConnection( 
              hostTextBox.Text, 
              int.Parse( portTextBox.Text ),
              userTextBox.Text,
              passwordTextBox.Text );

            if( useModeZCheckBox.Checked )
            {
              connection.TransferMode = TransferMode.ZLibCompressed;
            }

            folders = new AbstractFolder[] { new FtpFolder( connection ) };
          }
          catch( Exception except )
          {
            MessageBox.Show( 
              "There was an error connecting to the FTP server.\n" + except.Message,
              "Error",
              MessageBoxButtons.OK, 
              MessageBoxIcon.Error );

            return null;
          }

          title = "FTP server: " + hostTextBox.Text;
        }

        if( folders == null )
          return null;

        // As you can see, the same FolderForm type is used to display all these
        // kinds of AbstractFolder instances.
        return new FolderForm( title, folders );
      }

      return null;
    }

    private void networkRadio_CheckedChanged(object sender, System.EventArgs e)
    {
      networkLabel.Enabled = networkRadio.Checked;
      networkTextBox.Enabled = networkRadio.Checked;
    }

    private void ftpRadio_CheckedChanged(object sender, System.EventArgs e)
    {
      hostLabel.Enabled = ftpRadio.Checked;
      hostTextBox.Enabled = ftpRadio.Checked;
      portLabel.Enabled = ftpRadio.Checked;
      portTextBox.Enabled = ftpRadio.Checked;
      userLabel.Enabled = ftpRadio.Checked;
      userTextBox.Enabled = ftpRadio.Checked;
      passwordLabel.Enabled = ftpRadio.Checked;
      passwordTextBox.Enabled = ftpRadio.Checked;
      useModeZCheckBox.Enabled = ftpRadio.Checked;
    }

    private void OpenFolderForm_Load(object sender, System.EventArgs e)
    {
      ftpRadio.Checked = true;
    }

    private void UseModeZCheckBox_CheckedChanged( object sender, EventArgs e )
    {
      
    }
	}
}
