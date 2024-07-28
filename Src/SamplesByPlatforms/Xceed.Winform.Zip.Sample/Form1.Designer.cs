namespace Xceed.Winform.Zip.Sample
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.CompressFileButton = new System.Windows.Forms.Button();
			this.CompressFolderButton = new System.Windows.Forms.Button();
			this.ListZipContentButton = new System.Windows.Forms.Button();
			this.UnzipFilesButton = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CompressFileButton
			// 
			this.CompressFileButton.BackColor = System.Drawing.Color.Black;
			this.CompressFileButton.Location = new System.Drawing.Point(47, 62);
			this.CompressFileButton.Name = "CompressFileButton";
			this.CompressFileButton.Size = new System.Drawing.Size(220, 40);
			this.CompressFileButton.TabIndex = 0;
			this.CompressFileButton.Text = "Select a file or many and save as zip";
			this.CompressFileButton.UseVisualStyleBackColor = false;
			this.CompressFileButton.Click += new System.EventHandler(this.CompressFileButton_Click);
			// 
			// CompressFolderButton
			// 
			this.CompressFolderButton.BackColor = System.Drawing.Color.Black;
			this.CompressFolderButton.Location = new System.Drawing.Point(47, 124);
			this.CompressFolderButton.Name = "CompressFolderButton";
			this.CompressFolderButton.Size = new System.Drawing.Size(220, 40);
			this.CompressFolderButton.TabIndex = 1;
			this.CompressFolderButton.Text = "Select a folder and save as Zip";
			this.CompressFolderButton.UseVisualStyleBackColor = false;
			this.CompressFolderButton.Click += new System.EventHandler(this.CompressFolderButton_Click);
			// 
			// ListZipContentButton
			// 
			this.ListZipContentButton.BackColor = System.Drawing.Color.Black;
			this.ListZipContentButton.Location = new System.Drawing.Point(47, 182);
			this.ListZipContentButton.Name = "ListZipContentButton";
			this.ListZipContentButton.Size = new System.Drawing.Size(220, 40);
			this.ListZipContentButton.TabIndex = 2;
			this.ListZipContentButton.Text = "Select a zip to list the content";
			this.ListZipContentButton.UseVisualStyleBackColor = false;
			this.ListZipContentButton.Click += new System.EventHandler(this.ListZipContentButton_Click);
			// 
			// UnzipFilesButton
			// 
			this.UnzipFilesButton.BackColor = System.Drawing.Color.Black;
			this.UnzipFilesButton.Location = new System.Drawing.Point(47, 240);
			this.UnzipFilesButton.Name = "UnzipFilesButton";
			this.UnzipFilesButton.Size = new System.Drawing.Size(220, 40);
			this.UnzipFilesButton.TabIndex = 3;
			this.UnzipFilesButton.Text = "Select a zip file to decompress.";
			this.UnzipFilesButton.UseVisualStyleBackColor = false;
			this.UnzipFilesButton.Click += new System.EventHandler(this.UnzipFilesButton_Click);
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.InfoText;
			this.listBox1.ForeColor = System.Drawing.SystemColors.Info;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(314, 63);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(252, 290);
			this.listBox1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(311, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Zip elements:";
			// 
			// progressBar1
			// 
			this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.progressBar1.Location = new System.Drawing.Point(27, 359);
			this.progressBar1.MarqueeAnimationSpeed = 20;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(539, 26);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 6;
			this.progressBar1.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(23, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(414, 24);
			this.label2.TabIndex = 7;
			this.label2.Text = "Select a button to check how Xceed.Zip works!!!";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(573, 397);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.UnzipFilesButton);
			this.Controls.Add(this.ListZipContentButton);
			this.Controls.Add(this.CompressFolderButton);
			this.Controls.Add(this.CompressFileButton);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Xceed Zip Sample for Winform";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button CompressFileButton;
		private System.Windows.Forms.Button CompressFolderButton;
		private System.Windows.Forms.Button ListZipContentButton;
		private System.Windows.Forms.Button UnzipFilesButton;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label label2;
	}
}

