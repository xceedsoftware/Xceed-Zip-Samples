/* Xceed Zip for .NET - ZipManager Sample Application
 * Copyright (c) 2000-2002 - Xceed Software Inc.
 * 
 * [SfxOptions.cs]
 * 
 * This application demonstrates how to use Xceed Zip for .NET.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file is 
 * only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Zip.Sfx;
using System.IO;

namespace Xceed.Zip.Samples.ZipManager
{
  /// <summary>
  /// Summary description for SfxOptions.
  /// </summary>
  public class SfxOptions : System.Windows.Forms.Form
  {
    internal System.Windows.Forms.Button btnOk;
    internal System.Windows.Forms.CheckBox chkCreateSFX;
    internal System.Windows.Forms.OpenFileDialog dlgOpen;
    internal System.Windows.Forms.GroupBox grpStringsAndMessages;
    internal System.Windows.Forms.TextBox txtSuccess;
    internal System.Windows.Forms.TextBox txtIntroduction;
    internal System.Windows.Forms.TextBox txtTitle;
    internal System.Windows.Forms.Label lblSuccess;
    internal System.Windows.Forms.Label lblIntroduction;
    internal System.Windows.Forms.Label lblTitle;
    internal System.Windows.Forms.Button btnCancel;
    internal System.Windows.Forms.GroupBox grpFilesAndFolders;
    internal System.Windows.Forms.Button btnIcon;
    internal System.Windows.Forms.Button btnReadme;
    internal System.Windows.Forms.TextBox txtIcon;
    internal System.Windows.Forms.Label lblIcon;
    internal System.Windows.Forms.TextBox txtReadme;
    internal System.Windows.Forms.Label lblReadme;
    internal System.Windows.Forms.TextBox txtFolder;
    internal System.Windows.Forms.Label lblFolder;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public SfxOptions( SfxPrefix sfxPrefix )
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //

      m_sfxPrefix = sfxPrefix;
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
      this.btnOk = new System.Windows.Forms.Button();
      this.chkCreateSFX = new System.Windows.Forms.CheckBox();
      this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
      this.grpStringsAndMessages = new System.Windows.Forms.GroupBox();
      this.txtSuccess = new System.Windows.Forms.TextBox();
      this.txtIntroduction = new System.Windows.Forms.TextBox();
      this.txtTitle = new System.Windows.Forms.TextBox();
      this.lblSuccess = new System.Windows.Forms.Label();
      this.lblIntroduction = new System.Windows.Forms.Label();
      this.lblTitle = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.grpFilesAndFolders = new System.Windows.Forms.GroupBox();
      this.btnIcon = new System.Windows.Forms.Button();
      this.btnReadme = new System.Windows.Forms.Button();
      this.txtIcon = new System.Windows.Forms.TextBox();
      this.lblIcon = new System.Windows.Forms.Label();
      this.txtReadme = new System.Windows.Forms.TextBox();
      this.lblReadme = new System.Windows.Forms.Label();
      this.txtFolder = new System.Windows.Forms.TextBox();
      this.lblFolder = new System.Windows.Forms.Label();
      this.grpStringsAndMessages.SuspendLayout();
      this.grpFilesAndFolders.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(487, 18);
      this.btnOk.Name = "btnOk";
      this.btnOk.TabIndex = 6;
      this.btnOk.Text = "&OK";
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // chkCreateSFX
      // 
      this.chkCreateSFX.Location = new System.Drawing.Point(15, 18);
      this.chkCreateSFX.Name = "chkCreateSFX";
      this.chkCreateSFX.Size = new System.Drawing.Size(184, 24);
      this.chkCreateSFX.TabIndex = 5;
      this.chkCreateSFX.Text = "Create a &self-extracting zip file";
      this.chkCreateSFX.CheckedChanged += new System.EventHandler(this.chkCreateSFX_CheckedChanged);
      // 
      // dlgOpen
      // 
      this.dlgOpen.AddExtension = false;
      this.dlgOpen.RestoreDirectory = true;
      // 
      // grpStringsAndMessages
      // 
      this.grpStringsAndMessages.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                        this.txtSuccess,
                                                                                        this.txtIntroduction,
                                                                                        this.txtTitle,
                                                                                        this.lblSuccess,
                                                                                        this.lblIntroduction,
                                                                                        this.lblTitle});
      this.grpStringsAndMessages.Enabled = false;
      this.grpStringsAndMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.grpStringsAndMessages.Location = new System.Drawing.Point(15, 50);
      this.grpStringsAndMessages.Name = "grpStringsAndMessages";
      this.grpStringsAndMessages.Size = new System.Drawing.Size(456, 192);
      this.grpStringsAndMessages.TabIndex = 8;
      this.grpStringsAndMessages.TabStop = false;
      this.grpStringsAndMessages.Text = "Strings and Messages";
      // 
      // txtSuccess
      // 
      this.txtSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtSuccess.Location = new System.Drawing.Point(176, 120);
      this.txtSuccess.Multiline = true;
      this.txtSuccess.Name = "txtSuccess";
      this.txtSuccess.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtSuccess.Size = new System.Drawing.Size(264, 56);
      this.txtSuccess.TabIndex = 10;
      this.txtSuccess.Text = "All files were succesfully unzipped.";
      // 
      // txtIntroduction
      // 
      this.txtIntroduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtIntroduction.Location = new System.Drawing.Point(176, 56);
      this.txtIntroduction.Multiline = true;
      this.txtIntroduction.Name = "txtIntroduction";
      this.txtIntroduction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtIntroduction.Size = new System.Drawing.Size(264, 56);
      this.txtIntroduction.TabIndex = 9;
      this.txtIntroduction.Text = "Welcome to the Xceed Zip Self-Extractor.\r\nThis program will unzip some files onto" +
        " your system.";
      // 
      // txtTitle
      // 
      this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtTitle.Location = new System.Drawing.Point(176, 28);
      this.txtTitle.Name = "txtTitle";
      this.txtTitle.Size = new System.Drawing.Size(264, 20);
      this.txtTitle.TabIndex = 8;
      this.txtTitle.Text = "The Xceed Zip Self-Extractor";
      // 
      // lblSuccess
      // 
      this.lblSuccess.AutoSize = true;
      this.lblSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblSuccess.Location = new System.Drawing.Point(16, 120);
      this.lblSuccess.Name = "lblSuccess";
      this.lblSuccess.Size = new System.Drawing.Size(99, 13);
      this.lblSuccess.TabIndex = 7;
      this.lblSuccess.Text = "Success message:";
      // 
      // lblIntroduction
      // 
      this.lblIntroduction.AutoSize = true;
      this.lblIntroduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblIntroduction.Location = new System.Drawing.Point(16, 60);
      this.lblIntroduction.Name = "lblIntroduction";
      this.lblIntroduction.Size = new System.Drawing.Size(116, 13);
      this.lblIntroduction.TabIndex = 6;
      this.lblIntroduction.Text = "Introduction message:";
      // 
      // lblTitle
      // 
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblTitle.Location = new System.Drawing.Point(16, 32);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(144, 13);
      this.lblTitle.TabIndex = 5;
      this.lblTitle.Text = "Self-extracting zip file\'s title:";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(487, 50);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.TabIndex = 7;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // grpFilesAndFolders
      // 
      this.grpFilesAndFolders.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                     this.btnIcon,
                                                                                     this.btnReadme,
                                                                                     this.txtIcon,
                                                                                     this.lblIcon,
                                                                                     this.txtReadme,
                                                                                     this.lblReadme,
                                                                                     this.txtFolder,
                                                                                     this.lblFolder});
      this.grpFilesAndFolders.Enabled = false;
      this.grpFilesAndFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.grpFilesAndFolders.Location = new System.Drawing.Point(15, 258);
      this.grpFilesAndFolders.Name = "grpFilesAndFolders";
      this.grpFilesAndFolders.Size = new System.Drawing.Size(456, 112);
      this.grpFilesAndFolders.TabIndex = 9;
      this.grpFilesAndFolders.TabStop = false;
      this.grpFilesAndFolders.Text = "Files and Folders";
      // 
      // btnIcon
      // 
      this.btnIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.btnIcon.Location = new System.Drawing.Point(408, 80);
      this.btnIcon.Name = "btnIcon";
      this.btnIcon.Size = new System.Drawing.Size(32, 23);
      this.btnIcon.TabIndex = 16;
      this.btnIcon.Text = "...";
      this.btnIcon.Click += new System.EventHandler(this.btnIcon_Click);
      // 
      // btnReadme
      // 
      this.btnReadme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.btnReadme.Location = new System.Drawing.Point(408, 48);
      this.btnReadme.Name = "btnReadme";
      this.btnReadme.Size = new System.Drawing.Size(32, 23);
      this.btnReadme.TabIndex = 15;
      this.btnReadme.Text = "...";
      this.btnReadme.Click += new System.EventHandler(this.btnReadme_Click);
      // 
      // txtIcon
      // 
      this.txtIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtIcon.Location = new System.Drawing.Point(176, 80);
      this.txtIcon.Name = "txtIcon";
      this.txtIcon.Size = new System.Drawing.Size(224, 20);
      this.txtIcon.TabIndex = 14;
      this.txtIcon.Text = "";
      // 
      // lblIcon
      // 
      this.lblIcon.AutoSize = true;
      this.lblIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblIcon.Location = new System.Drawing.Point(16, 84);
      this.lblIcon.Name = "lblIcon";
      this.lblIcon.Size = new System.Drawing.Size(79, 13);
      this.lblIcon.TabIndex = 13;
      this.lblIcon.Text = "SFX file\'s icon:";
      // 
      // txtReadme
      // 
      this.txtReadme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtReadme.Location = new System.Drawing.Point(176, 50);
      this.txtReadme.Name = "txtReadme";
      this.txtReadme.Size = new System.Drawing.Size(224, 20);
      this.txtReadme.TabIndex = 12;
      this.txtReadme.Text = "";
      // 
      // lblReadme
      // 
      this.lblReadme.AutoSize = true;
      this.lblReadme.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblReadme.Location = new System.Drawing.Point(16, 54);
      this.lblReadme.Name = "lblReadme";
      this.lblReadme.Size = new System.Drawing.Size(119, 13);
      this.lblReadme.TabIndex = 11;
      this.lblReadme.Text = "Readme file to display:";
      // 
      // txtFolder
      // 
      this.txtFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.txtFolder.Location = new System.Drawing.Point(176, 20);
      this.txtFolder.Name = "txtFolder";
      this.txtFolder.Size = new System.Drawing.Size(264, 20);
      this.txtFolder.TabIndex = 10;
      this.txtFolder.Text = "";
      // 
      // lblFolder
      // 
      this.lblFolder.AutoSize = true;
      this.lblFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblFolder.Location = new System.Drawing.Point(16, 24);
      this.lblFolder.Name = "lblFolder";
      this.lblFolder.Size = new System.Drawing.Size(153, 13);
      this.lblFolder.TabIndex = 9;
      this.lblFolder.Text = "Default folder to unzip files to:";
      // 
      // SfxOptions
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(576, 389);
      this.ControlBox = false;
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.chkCreateSFX,
                                                                  this.grpStringsAndMessages,
                                                                  this.btnCancel,
                                                                  this.grpFilesAndFolders,
                                                                  this.btnOk});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SfxOptions";
      this.Text = "Self-extracting zip file options";
      this.Load += new System.EventHandler(this.SfxOptions_Load);
      this.grpStringsAndMessages.ResumeLayout(false);
      this.grpFilesAndFolders.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion


    public SfxPrefix SfxPrefix
    {
      get
      {
        return m_sfxPrefix;
      }
      set
      {
        m_sfxPrefix = value;
      }
    }

    private void SfxOptions_Load(object sender, System.EventArgs e)
    {
      txtFolder.Text = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length);
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
      if(m_sfxPrefix == null)
      {
        XceedSfxPrefix xceedSfxPrefix = new XceedSfxPrefix( new DiskFile(@"..\..\xcdsfx32.bin"));

        xceedSfxPrefix.DialogStrings[DialogStrings.Title] = txtTitle.Text;
        xceedSfxPrefix.DialogMessages[DialogMessages.Introduction] = txtIntroduction.Text;
        xceedSfxPrefix.DialogMessages[DialogMessages.Success] = txtSuccess.Text;
        xceedSfxPrefix.DefaultDestinationFolder = txtFolder.Text;

        if(txtIcon.Text != String.Empty)
        {
          xceedSfxPrefix.Icon = new Icon(txtIcon.Text);
        }

        if (txtReadme.Text != String.Empty)
        {
          xceedSfxPrefix.ExecuteAfter.Add(txtReadme.Text);
        }

        m_sfxPrefix = xceedSfxPrefix;
      }

      Close();
    }

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    private void chkCreateSFX_CheckedChanged(object sender, System.EventArgs e)
    {
      grpStringsAndMessages.Enabled = !grpStringsAndMessages.Enabled;
      grpFilesAndFolders.Enabled = !grpFilesAndFolders.Enabled;
    }

    private void btnReadme_Click(object sender, System.EventArgs e)
    {
      dlgOpen.Filter = "Readme files (*.txt)|*.txt";
      dlgOpen.Title = "Select readme file";
      dlgOpen.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length);
      if(dlgOpen.ShowDialog() == DialogResult.OK)
      {
        txtReadme.Text = dlgOpen.FileName;
      }
    }

    private void btnIcon_Click(object sender, System.EventArgs e)
    {
      dlgOpen.Filter = "Icon files (*.ico)|*.ico";
      dlgOpen.Title = "Select icon file";
      dlgOpen.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length);
      if(dlgOpen.ShowDialog() == DialogResult.OK) 
      {
        txtIcon.Text = dlgOpen.FileName;
      }
    }

    private SfxPrefix m_sfxPrefix = new SfxPrefix();

  }
}
