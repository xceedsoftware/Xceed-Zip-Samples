using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FolderViews
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
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
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.okButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(300, 89);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.Color.White;
      this.panel1.Controls.Add(this.okButton);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.pictureBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(5, 5);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(246, 261);
      this.panel1.TabIndex = 1;
      // 
      // okButton
      // 
      this.okButton.BackColor = System.Drawing.SystemColors.Control;
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.okButton.Location = new System.Drawing.Point(160, 224);
      this.okButton.Name = "okButton";
      this.okButton.TabIndex = 3;
      this.okButton.Text = "Ok";
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label2.Location = new System.Drawing.Point(8, 128);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(232, 88);
      this.label2.TabIndex = 2;
      this.label2.Text = "This sample demonstrates how to use the Xceed FTP FileSystem interface to provide" +
        " universal use of any kind of file or folder, regardless of their location (on d" +
        "isk, on an FTP server, in memory, in isolated storage...).";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 104);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(232, 23);
      this.label1.TabIndex = 1;
      this.label1.Text = "FolderViews C# sample";
      // 
      // AboutForm
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
      this.BackColor = System.Drawing.Color.MidnightBlue;
      this.ClientSize = new System.Drawing.Size(256, 271);
      this.Controls.Add(this.panel1);
      this.DockPadding.All = 5;
      this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.ForeColor = System.Drawing.Color.MidnightBlue;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "AboutForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About the FolderViews sample...";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    private const int WM_NCHITTEST = 0x0084;
    private const int HTCAPTION = 0x0002;

    protected override void WndProc(ref Message m)
    {
      if( m.Msg == WM_NCHITTEST )
      {
        // All the form's surface can be used to move it.
        m.Result = ( IntPtr )HTCAPTION;
        return;
      }

      base.WndProc( ref m );
    }
	}
}
