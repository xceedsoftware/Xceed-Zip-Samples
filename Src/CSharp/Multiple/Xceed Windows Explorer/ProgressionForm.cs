/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [ProgressionForm.cs]
 * 
 * Form used to show a progression to the user. 
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
	public class ProgressionForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

		public ProgressionForm()
		{
			InitializeComponent();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC FIELDS

    public string ActionText
    {
      get { return this.Text; }
      set { this.Text = value; }
    }

    public string FromText
    {
      get { return FromLabel.Text; }
      set { FromLabel.Text = value; }
    }

    public string ToText
    {
      get { return ToLabel.Text; }
      set { ToLabel.Text = value; }
    }

    public int CurrentProgressValue
    {
      get { return CurrentProgressBar.Value; }
      set 
      { 
        if( value > CurrentProgressBar.Maximum )
          value = CurrentProgressBar.Maximum;

        if( value < CurrentProgressBar.Minimum )
          value = CurrentProgressBar.Minimum;

        CurrentProgressBar.Value = value; 
      }
    }

    public int TotalProgressValue
    {
      get { return TotalProgressBar.Value; }
      set 
      { 
        if( value > TotalProgressBar.Maximum )
          value = TotalProgressBar.Maximum;

        if( value < TotalProgressBar.Minimum )
          value = TotalProgressBar.Minimum;

        TotalProgressBar.Value = value; 
      }
    }

    public bool CancelEnabled
    {
      get { return CancelBtn.Enabled; }
      set { CancelBtn.Enabled = value; }
    }

    public bool UserCancelled
    {
      get { return m_userCancelled; }
    }

    #endregion PUBLIC FIELDS

    #region EVENT HANDLERS

    private void CancelBtn_Click( object sender, System.EventArgs e )
    {
      CancelBtn.Enabled = false;
      m_userCancelled = true;
    }

    #endregion EVENT HANDLERS

    #region PROTECTED METHODS

    /// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

    #endregion PROTECTED METHODS

    #region PRIVATE FIELDS

    private bool m_userCancelled; // = false

    #endregion PRIVATE FIELDS

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProgressionForm));
      this.TotalProgressBar = new System.Windows.Forms.ProgressBar();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.FromLabel = new Xceed.FileSystem.Samples.PathLabel();
      this.label2 = new System.Windows.Forms.Label();
      this.ToLabel = new Xceed.FileSystem.Samples.PathLabel();
      this.CurrentProgressBar = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // TotalProgressBar
      // 
      this.TotalProgressBar.Location = new System.Drawing.Point(16, 152);
      this.TotalProgressBar.Name = "TotalProgressBar";
      this.TotalProgressBar.Size = new System.Drawing.Size(392, 16);
      this.TotalProgressBar.TabIndex = 2;
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point(320, 176);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(88, 24);
      this.CancelBtn.TabIndex = 3;
      this.CancelBtn.Text = "&Cancel";
      this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label1.Location = new System.Drawing.Point(16, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 16);
      this.label1.TabIndex = 4;
      this.label1.Text = "From";
      // 
      // FromLabel
      // 
      this.FromLabel.Location = new System.Drawing.Point(16, 40);
      this.FromLabel.Name = "FromLabel";
      this.FromLabel.Size = new System.Drawing.Size(384, 16);
      this.FromLabel.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.label2.Location = new System.Drawing.Point(16, 72);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 16);
      this.label2.TabIndex = 6;
      this.label2.Text = "To";
      // 
      // ToLabel
      // 
      this.ToLabel.Location = new System.Drawing.Point(16, 96);
      this.ToLabel.Name = "ToLabel";
      this.ToLabel.Size = new System.Drawing.Size(384, 16);
      this.ToLabel.TabIndex = 7;
      this.ToLabel.UseMnemonic = false;
      // 
      // CurrentProgressBar
      // 
      this.CurrentProgressBar.Location = new System.Drawing.Point(16, 128);
      this.CurrentProgressBar.Name = "CurrentProgressBar";
      this.CurrentProgressBar.Size = new System.Drawing.Size(392, 16);
      this.CurrentProgressBar.TabIndex = 8;
      // 
      // ProgressionForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.CancelBtn;
      this.ClientSize = new System.Drawing.Size(416, 208);
      this.ControlBox = false;
      this.Controls.Add(this.CurrentProgressBar);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.ToLabel);
      this.Controls.Add(this.FromLabel);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.CancelBtn);
      this.Controls.Add(this.TotalProgressBar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ProgressionForm";
      this.ShowInTaskbar = false;
      this.Text = "Progression";
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.Label label1;
    private PathLabel FromLabel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ProgressBar TotalProgressBar;
    private PathLabel ToLabel;
    private System.Windows.Forms.ProgressBar CurrentProgressBar;

    #endregion Windows Form Designer generated fields
  }
}
