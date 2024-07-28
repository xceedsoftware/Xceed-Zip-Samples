using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.MiniExplorer
{
	/// <summary>
	/// Summary description for DiskRequiredSimple.
	/// </summary>
	public class DiskRequiredForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label IntroLabel;
    private System.Windows.Forms.TextBox FullNameText;
    private System.Windows.Forms.Label RootNameLabel;
    private System.Windows.Forms.Button FailButton;
    private System.Windows.Forms.Button ContinueButton;
    private System.Windows.Forms.Label ReasonLabel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public DiskRequiredForm()
		{
			//
			// Required for Windows Form Designer support
			//

			InitializeComponent();
		}

    public DialogResult ShowDialog( IWin32Window owner, ref AbstractFile zipFile, int diskNumber, DiskRequiredReason reason )
    {
      string rootName = zipFile.RootFolder.FullName;
      string fullName = zipFile.FullName;

      RootNameLabel.Text = rootName;
      FullNameText.Text = fullName.Substring( rootName.Length, fullName.Length - rootName.Length );;

      IntroLabel.Text = 
        "Disk #" + diskNumber.ToString() +
        " or Part #" + diskNumber.ToString() +
        " of this zip file is required. If this zip file is located on a different removable disk, make sure the correct disk is inserted. If this part has a different name, change the filename below.";

      switch( reason )
      {
        case DiskRequiredReason.Deleting:
          ReasonLabel.Text = "Deleting unused zip file parts.";
          break;

        case DiskRequiredReason.DiskFull:
          ReasonLabel.Text = "The disk is full.";
          break;

        case DiskRequiredReason.Reading:
          ReasonLabel.Text = "Reading from a zip file part.";
          break;

        case DiskRequiredReason.SplitSizeReached:
          ReasonLabel.Text = "Reached the maximum split size.";
          break;

        case DiskRequiredReason.Updating:
          ReasonLabel.Text = "Building new zip file.";
          break;

        default:
          ReasonLabel.Text = "Unknown reason.";
          break;
      }

      this.DialogResult = DialogResult.Cancel;

      if( base.ShowDialog( owner ) == DialogResult.OK )
      {
        zipFile = zipFile.RootFolder.GetFile( FullNameText.Text );
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
      this.IntroLabel = new System.Windows.Forms.Label();
      this.RootNameLabel = new System.Windows.Forms.Label();
      this.FullNameText = new System.Windows.Forms.TextBox();
      this.FailButton = new System.Windows.Forms.Button();
      this.ContinueButton = new System.Windows.Forms.Button();
      this.ReasonLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // IntroLabel
      // 
      this.IntroLabel.Location = new System.Drawing.Point(8, 32);
      this.IntroLabel.Name = "IntroLabel";
      this.IntroLabel.Size = new System.Drawing.Size(440, 40);
      this.IntroLabel.TabIndex = 0;
      this.IntroLabel.Text = "Disk #N or Part #N of this zip file is required. If this zip file is located on a" +
        " different removable disk, make sure the correct disk is inserted. If this part " +
        "has a different name, change the filename below.";
      // 
      // RootNameLabel
      // 
      this.RootNameLabel.AutoSize = true;
      this.RootNameLabel.Location = new System.Drawing.Point(8, 84);
      this.RootNameLabel.Name = "RootNameLabel";
      this.RootNameLabel.Size = new System.Drawing.Size(65, 13);
      this.RootNameLabel.TabIndex = 1;
      this.RootNameLabel.Text = "RootName:\\";
      // 
      // FullNameText
      // 
      this.FullNameText.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.FullNameText.Location = new System.Drawing.Point(80, 80);
      this.FullNameText.Name = "FullNameText";
      this.FullNameText.Size = new System.Drawing.Size(368, 20);
      this.FullNameText.TabIndex = 2;
      this.FullNameText.Text = "Path\\Name";
      // 
      // FailButton
      // 
      this.FailButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.FailButton.Location = new System.Drawing.Point(376, 112);
      this.FailButton.Name = "FailButton";
      this.FailButton.TabIndex = 4;
      this.FailButton.Text = "&Cancel";
      // 
      // ContinueButton
      // 
      this.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.ContinueButton.Location = new System.Drawing.Point(296, 112);
      this.ContinueButton.Name = "ContinueButton";
      this.ContinueButton.TabIndex = 3;
      this.ContinueButton.Text = "&Ok";
      // 
      // ReasonLabel
      // 
      this.ReasonLabel.Location = new System.Drawing.Point(8, 8);
      this.ReasonLabel.Name = "ReasonLabel";
      this.ReasonLabel.Size = new System.Drawing.Size(440, 16);
      this.ReasonLabel.TabIndex = 5;
      // 
      // DiskRequiredForm
      // 
      this.AcceptButton = this.ContinueButton;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.FailButton;
      this.ClientSize = new System.Drawing.Size(456, 144);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.ReasonLabel,
                                                                  this.ContinueButton,
                                                                  this.FailButton,
                                                                  this.FullNameText,
                                                                  this.RootNameLabel,
                                                                  this.IntroLabel});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DiskRequiredForm";
      this.Text = "DiskRequiredSimple";
      this.Load += new System.EventHandler(this.DiskRequiredSimple_Load);
      this.ResumeLayout(false);

    }
		#endregion

    private void DiskRequiredSimple_Load(object sender, System.EventArgs e)
    {
      int left = RootNameLabel.Left + RootNameLabel.Width + 4;
      int diff = FullNameText.Left - left;

      FullNameText.Left = left;
      FullNameText.Width += diff;
    }
	}
}
