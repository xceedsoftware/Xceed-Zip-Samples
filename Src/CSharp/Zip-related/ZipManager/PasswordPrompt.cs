using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.Zip;

namespace Xceed.Zip.Samples.ZipManager
{
	/// <summary>
	/// Summary description for PasswordPrompt.
	/// </summary>
	public class PasswordPrompt : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label MessageLabel;
    private System.Windows.Forms.Label PasswordLabel;
    private System.Windows.Forms.TextBox PasswordText;
    private System.Windows.Forms.GroupBox EncryptionMethodBox;
    private System.Windows.Forms.RadioButton TraditionalEncryptionRadio;
    private System.Windows.Forms.RadioButton WinZipAESEncryptionRadio;
    private System.Windows.Forms.GroupBox StrengthBox;
    private System.Windows.Forms.RadioButton Bits256Radio;
    private System.Windows.Forms.RadioButton Bits192Radio;
    private System.Windows.Forms.RadioButton Bits128Radio;
    private System.Windows.Forms.Button AbortButton;
    private System.Windows.Forms.Button OkButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PasswordPrompt()
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
      this.MessageLabel = new System.Windows.Forms.Label();
      this.PasswordLabel = new System.Windows.Forms.Label();
      this.PasswordText = new System.Windows.Forms.TextBox();
      this.EncryptionMethodBox = new System.Windows.Forms.GroupBox();
      this.StrengthBox = new System.Windows.Forms.GroupBox();
      this.Bits256Radio = new System.Windows.Forms.RadioButton();
      this.Bits192Radio = new System.Windows.Forms.RadioButton();
      this.Bits128Radio = new System.Windows.Forms.RadioButton();
      this.WinZipAESEncryptionRadio = new System.Windows.Forms.RadioButton();
      this.TraditionalEncryptionRadio = new System.Windows.Forms.RadioButton();
      this.AbortButton = new System.Windows.Forms.Button();
      this.OkButton = new System.Windows.Forms.Button();
      this.EncryptionMethodBox.SuspendLayout();
      this.StrengthBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // MessageLabel
      // 
      this.MessageLabel.Location = new System.Drawing.Point(16, 16);
      this.MessageLabel.Name = "MessageLabel";
      this.MessageLabel.Size = new System.Drawing.Size(352, 40);
      this.MessageLabel.TabIndex = 0;
      this.MessageLabel.Text = "label1";
      // 
      // PasswordLabel
      // 
      this.PasswordLabel.Location = new System.Drawing.Point(16, 64);
      this.PasswordLabel.Name = "PasswordLabel";
      this.PasswordLabel.Size = new System.Drawing.Size(352, 16);
      this.PasswordLabel.TabIndex = 1;
      this.PasswordLabel.Text = "label1";
      // 
      // PasswordText
      // 
      this.PasswordText.Location = new System.Drawing.Point(16, 80);
      this.PasswordText.Name = "PasswordText";
      this.PasswordText.Size = new System.Drawing.Size(352, 20);
      this.PasswordText.TabIndex = 2;
      this.PasswordText.Text = "";
      // 
      // EncryptionMethodBox
      // 
      this.EncryptionMethodBox.Controls.Add(this.StrengthBox);
      this.EncryptionMethodBox.Controls.Add(this.WinZipAESEncryptionRadio);
      this.EncryptionMethodBox.Controls.Add(this.TraditionalEncryptionRadio);
      this.EncryptionMethodBox.Location = new System.Drawing.Point(16, 112);
      this.EncryptionMethodBox.Name = "EncryptionMethodBox";
      this.EncryptionMethodBox.Size = new System.Drawing.Size(352, 136);
      this.EncryptionMethodBox.TabIndex = 3;
      this.EncryptionMethodBox.TabStop = false;
      this.EncryptionMethodBox.Text = "Encryption Method";
      // 
      // StrengthBox
      // 
      this.StrengthBox.Controls.Add(this.Bits256Radio);
      this.StrengthBox.Controls.Add(this.Bits192Radio);
      this.StrengthBox.Controls.Add(this.Bits128Radio);
      this.StrengthBox.Location = new System.Drawing.Point(48, 72);
      this.StrengthBox.Name = "StrengthBox";
      this.StrengthBox.Size = new System.Drawing.Size(288, 48);
      this.StrengthBox.TabIndex = 3;
      this.StrengthBox.TabStop = false;
      this.StrengthBox.Text = "Strength";
      // 
      // Bits256Radio
      // 
      this.Bits256Radio.Location = new System.Drawing.Point(176, 24);
      this.Bits256Radio.Name = "Bits256Radio";
      this.Bits256Radio.Size = new System.Drawing.Size(64, 16);
      this.Bits256Radio.TabIndex = 5;
      this.Bits256Radio.Text = "256-bit";
      // 
      // Bits192Radio
      // 
      this.Bits192Radio.Location = new System.Drawing.Point(96, 24);
      this.Bits192Radio.Name = "Bits192Radio";
      this.Bits192Radio.Size = new System.Drawing.Size(64, 16);
      this.Bits192Radio.TabIndex = 4;
      this.Bits192Radio.Text = "192-bit";
      // 
      // Bits128Radio
      // 
      this.Bits128Radio.Location = new System.Drawing.Point(16, 24);
      this.Bits128Radio.Name = "Bits128Radio";
      this.Bits128Radio.Size = new System.Drawing.Size(64, 16);
      this.Bits128Radio.TabIndex = 3;
      this.Bits128Radio.Text = "128-bit";
      // 
      // WinZipAESEncryptionRadio
      // 
      this.WinZipAESEncryptionRadio.Location = new System.Drawing.Point(24, 48);
      this.WinZipAESEncryptionRadio.Name = "WinZipAESEncryptionRadio";
      this.WinZipAESEncryptionRadio.Size = new System.Drawing.Size(312, 16);
      this.WinZipAESEncryptionRadio.TabIndex = 1;
      this.WinZipAESEncryptionRadio.Text = "WinZip &AES Encryption (strong)";
      // 
      // TraditionalEncryptionRadio
      // 
      this.TraditionalEncryptionRadio.Location = new System.Drawing.Point(24, 24);
      this.TraditionalEncryptionRadio.Name = "TraditionalEncryptionRadio";
      this.TraditionalEncryptionRadio.Size = new System.Drawing.Size(312, 16);
      this.TraditionalEncryptionRadio.TabIndex = 0;
      this.TraditionalEncryptionRadio.Text = "&Traditional Zip Encryption (weak)";
      // 
      // AbortButton
      // 
      this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.AbortButton.Location = new System.Drawing.Point(296, 256);
      this.AbortButton.Name = "AbortButton";
      this.AbortButton.TabIndex = 4;
      this.AbortButton.Text = "&Cancel";
      // 
      // OkButton
      // 
      this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkButton.Location = new System.Drawing.Point(216, 256);
      this.OkButton.Name = "OkButton";
      this.OkButton.TabIndex = 5;
      this.OkButton.Text = "&Ok";
      // 
      // PasswordPrompt
      // 
      this.AcceptButton = this.OkButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.AbortButton;
      this.ClientSize = new System.Drawing.Size(384, 288);
      this.Controls.Add(this.OkButton);
      this.Controls.Add(this.AbortButton);
      this.Controls.Add(this.EncryptionMethodBox);
      this.Controls.Add(this.PasswordText);
      this.Controls.Add(this.PasswordLabel);
      this.Controls.Add(this.MessageLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PasswordPrompt";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.EncryptionMethodBox.ResumeLayout(false);
      this.StrengthBox.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    public DialogResult ShowDialog( 
      IWin32Window owner,
      ref string password, 
      string title, 
      string message, 
      string label, 
      ref EncryptionMethod method, 
      ref int strength,
      bool readOnly )
    {
      this.Text = title;
      MessageLabel.Text = message;
      PasswordLabel.Text = label;
      PasswordText.Text = password;

      TraditionalEncryptionRadio.Checked = ( method == EncryptionMethod.Compatible );
      WinZipAESEncryptionRadio.Checked = ( method == EncryptionMethod.WinZipAes );
      Bits128Radio.Checked = ( strength == 128 );
      Bits192Radio.Checked = ( strength == 192 );
      Bits256Radio.Checked = ( strength == 256 );

      EncryptionMethodBox.Enabled = !readOnly;
        
      if( base.ShowDialog( owner ) == DialogResult.OK )
      {
        password = PasswordText.Text;

        if( !readOnly )
        {
          method = ( WinZipAESEncryptionRadio.Checked ) ? ( EncryptionMethod.WinZipAes ) : ( EncryptionMethod.Compatible );
          strength = ( Bits128Radio.Checked ) ? ( 128 ) : ( ( Bits192Radio.Checked ) ? ( 192 ) : ( 256 ) );
        }

        return DialogResult.OK;
      }

      return DialogResult.Cancel;
    }
	}
}
