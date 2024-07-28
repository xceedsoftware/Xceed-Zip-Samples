using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Xceed.FileSystem.Samples
{
	public class TemporaryFileCopyActionForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

		public TemporaryFileCopyActionForm()
		{
			InitializeComponent();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC METHODS

    public DialogResult ShowDialog( IWin32Window owner, out bool fileOnly, out bool recursive )
    {
      fileOnly = true;
      recursive = false;

      DialogResult result = this.ShowDialog( owner );

      if( result == DialogResult.OK )
      {
        if( FileOnlyOption.Checked )
        {
          fileOnly = true;
          recursive = false;
        }
        else 
        {
          fileOnly = false;
          recursive = FileAndFolderRecursiveOption.Checked;
        }
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TemporaryFileCopyActionForm));
      this.label1 = new System.Windows.Forms.Label();
      this.FileOnlyOption = new System.Windows.Forms.RadioButton();
      this.FileAndFolderNonRecursiveOption = new System.Windows.Forms.RadioButton();
      this.FileAndFolderRecursiveOption = new System.Windows.Forms.RadioButton();
      this.OkBtn = new System.Windows.Forms.Button();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(312, 40);
      this.label1.TabIndex = 0;
      this.label1.Text = "In order to complete this operation, some file(s) needs to be copied locally. Sel" +
        "ect which files you want to copy to a temporary folder.";
      // 
      // FileOnlyOption
      // 
      this.FileOnlyOption.Checked = true;
      this.FileOnlyOption.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.FileOnlyOption.Location = new System.Drawing.Point(32, 64);
      this.FileOnlyOption.Name = "FileOnlyOption";
      this.FileOnlyOption.Size = new System.Drawing.Size(288, 24);
      this.FileOnlyOption.TabIndex = 0;
      this.FileOnlyOption.TabStop = true;
      this.FileOnlyOption.Text = "Selected file only";
      // 
      // FileAndFolderNonRecursiveOption
      // 
      this.FileAndFolderNonRecursiveOption.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.FileAndFolderNonRecursiveOption.Location = new System.Drawing.Point(32, 88);
      this.FileAndFolderNonRecursiveOption.Name = "FileAndFolderNonRecursiveOption";
      this.FileAndFolderNonRecursiveOption.Size = new System.Drawing.Size(288, 24);
      this.FileAndFolderNonRecursiveOption.TabIndex = 1;
      this.FileAndFolderNonRecursiveOption.Text = "Selected file and folder content (non-recursive)";
      // 
      // FileAndFolderRecursiveOption
      // 
      this.FileAndFolderRecursiveOption.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.FileAndFolderRecursiveOption.Location = new System.Drawing.Point(32, 112);
      this.FileAndFolderRecursiveOption.Name = "FileAndFolderRecursiveOption";
      this.FileAndFolderRecursiveOption.Size = new System.Drawing.Size(288, 24);
      this.FileAndFolderRecursiveOption.TabIndex = 2;
      this.FileAndFolderRecursiveOption.Text = "Selected file and folder content (recursive)";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.OkBtn.Location = new System.Drawing.Point(160, 152);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(80, 23);
      this.OkBtn.TabIndex = 3;
      this.OkBtn.Text = "&Ok";
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point(248, 152);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(80, 23);
      this.CancelBtn.TabIndex = 4;
      this.CancelBtn.Text = "&Cancel";
      // 
      // TemporaryFileCopyActionForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.CancelBtn;
      this.ClientSize = new System.Drawing.Size(338, 184);
      this.Controls.Add(this.CancelBtn);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.FileAndFolderRecursiveOption);
      this.Controls.Add(this.FileAndFolderNonRecursiveOption);
      this.Controls.Add(this.FileOnlyOption);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "TemporaryFileCopyActionForm";
      this.Text = "Copying temporary files";
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.RadioButton FileOnlyOption;
    private System.Windows.Forms.RadioButton FileAndFolderNonRecursiveOption;
    private System.Windows.Forms.RadioButton FileAndFolderRecursiveOption;
    private System.Windows.Forms.Button CancelBtn;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated fields
	}
}
