using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Zip;
using Xceed.FileSystem.Samples.Utils.Icons;

namespace Xceed.FileSystem.Samples
{
	public class PropertyForm : System.Windows.Forms.Form
	{
    #region CONSTRUCTORS

    public PropertyForm( FileSystemItem item )
    {
      if( item == null )
        throw new ArgumentNullException( "item" );

      m_item = item;

      InitializeComponent();

      // Toggle custom panel.
      this.ToggleCustomPanels();

      // Enable options.
      this.EnableOptions();

      // Initialize the values.
      this.InitializeValues();
    }

    #endregion CONSTRUCTORS

    #region PRIVATE METHODS

    private void EnableOptions()
    {
      FileSystemItem item = m_item;

      if(  ( item.HostFile != null )
        && ( item.RootFolder == item ) )
      {
        item = item.HostFile;
      }

      // Atributes
      SystemAttributeCheck.Enabled = item.HasAttributes;
      HiddenAttributeCheck.Enabled = item.HasAttributes;
      ReadOnlyAttributeCheck.Enabled = item.HasAttributes;
      ArchiveAttributeCheck.Enabled = item.HasAttributes;

      // Name
      NameTextBox.ReadOnly = ( item.RootFolder == item );
    }
    
    private void InitializeValues()
    {
      FileSystemItem item = m_item;

      // Are we working with the archive file itself?
      if(  ( item.HostFile != null )
        && ( item.RootFolder == item ) )
      {
        item = item.HostFile;
      }
      
      // Name
      NameTextBox.Text = item.Name;

      if( item.RootFolder == item )
        NameTextBox.Text = item.FullName;
      
      // Sizes
      AbstractFile file = item as AbstractFile;

      if( file != null )
      {
        SizeLabel.Text = this.GetFormattedSize( file.Size );

        ZippedFile zippedFile = item as ZippedFile;

        if( zippedFile != null )
        {
          CompressedSizeLabel.Text =  this.GetFormattedSize( zippedFile.CompressedSize );
        }
        else
        {
          CompressedSizeLabel.Text = "N/A";
        }
      }
      else
      {
        SizeLabel.Text = "N/A";
        CompressedSizeLabel.Text = "N/A";
      }

      // Creation date
      CreationDateTimeLabel.Text = "N/A";

      if( item.HasCreationDateTime )
        CreationDateTimeLabel.Text = item.CreationDateTime.ToString();

      // Modification date
      ModificationDateTimeLabel.Text = "N/A";

      if( item.HasLastWriteDateTime )
        ModificationDateTimeLabel.Text = item.LastWriteDateTime.ToString();

      // Last access date
      LastAccessDateTimeLabel.Text = "N/A";

      if( item.HasLastAccessDateTime )
        LastAccessDateTimeLabel.Text = item.LastAccessDateTime.ToString();

      // Attributes
      if( item.HasAttributes )
      {
        HiddenAttributeCheck.Checked = ( ( item.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden );
        SystemAttributeCheck.Checked = ( ( item.Attributes & FileAttributes.System ) == FileAttributes.System );
        ReadOnlyAttributeCheck.Checked = ( ( item.Attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly );
        ArchiveAttributeCheck.Checked = ( ( item.Attributes & FileAttributes.Archive ) == FileAttributes.Archive );
      }

      // Zip comments
      if( item is ZippedFile )
      {
        CommentsText.Text = ( ( ZippedFile )item ).Comment;
      }
      else if( item is ZippedFolder )
      {
        CommentsText.Text = ( ( ZippedFolder )item ).Comment;
      }
      else if( m_item is ZippedFolder )
      {
        CommentsText.Text = ( ( ZippedFolder )m_item ).Comment;
      }
    }

    private void SaveValues()
    {
      FileSystemItem item = m_item;

      // Are we working with the archive file itself?
      if(  ( item.HostFile != null )
        && ( item.RootFolder == item ) )
      {
        item = item.HostFile;
      }

      // To avoid rebuilding archives for nothing, we will using BatchUpdate on the root.
      using( new AutoBatchUpdate( item.RootFolder ) )
      {
        // Name
        if(  ( item.RootFolder != item )
          && ( item.Name != NameTextBox.Text ) )
        {
          item.Name = NameTextBox.Text;
        }

        // Attributes
        if( item.HasAttributes )
        {
          // System
          if( SystemAttributeCheck.Checked )
          {
            // Add the attribute if not already set.
            if( ( item.Attributes & FileAttributes.System ) != FileAttributes.System )
              item.Attributes |= FileAttributes.System;
          }
          else
          {
            // Remove the attribute if already set.
            if( ( item.Attributes & FileAttributes.System ) == FileAttributes.System )
              item.Attributes &= ~FileAttributes.System;
          }

          // Read-only
          if( ReadOnlyAttributeCheck.Checked )
          {
            // Add the attribute if not already set.
            if( ( item.Attributes & FileAttributes.ReadOnly ) != FileAttributes.ReadOnly )
              item.Attributes |= FileAttributes.ReadOnly;
          }
          else
          {
            // Remove the attribute if already set.
            if( ( item.Attributes & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
              item.Attributes &= ~FileAttributes.ReadOnly;
          }

          // Hidden
          if( HiddenAttributeCheck.Checked )
          {
            // Add the attribute if not already set.
            if( ( item.Attributes & FileAttributes.Hidden ) != FileAttributes.Hidden )
              item.Attributes |= FileAttributes.Hidden;
          }
          else
          {
            // Remove the attribute if already set.
            if( ( item.Attributes & FileAttributes.Hidden ) == FileAttributes.Hidden )
              item.Attributes &= ~FileAttributes.Hidden;
          }

          // Archive
          if( ArchiveAttributeCheck.Checked )
          {
            // Add the attribute if not already set.
            if( ( item.Attributes & FileAttributes.Archive ) != FileAttributes.Archive )
              item.Attributes |= FileAttributes.Archive;
          }
          else
          {
            // Remove the attribute if already set.
            if( ( item.Attributes & FileAttributes.Archive ) == FileAttributes.Archive )
              item.Attributes &= ~FileAttributes.Archive;
          }
        }

        // Zip Comments
        ZippedFile zippedFile = item as ZippedFile;
        if( zippedFile != null )
        {
          if( zippedFile.Comment != CommentsText.Text )
            zippedFile.Comment = CommentsText.Text;
        }

        ZippedFolder zippedFolder = item as ZippedFolder;
        if( zippedFolder != null )
        {
          if( zippedFolder.Comment != CommentsText.Text )
            zippedFolder.Comment = CommentsText.Text;
        }

        ZippedFolder zipFile = m_item as ZippedFolder;
        if( zipFile != null )
        {
          if( zipFile.Comment != CommentsText.Text )
            zipFile.Comment = CommentsText.Text;
        }
      }
    }

    private bool ValidateValues()
    {
      FileSystemItem item = m_item;

      // Are we working with the archive file itself?
      if(  ( item.HostFile != null )
        && ( item.RootFolder == item ) )
      {
        item = item.HostFile;
      }

      // Make sure the new name does not already exist.
      bool exists = false;

      if(  ( item.RootFolder != item ) 
        && ( NameTextBox.Text != item.Name ) )
      {
        if( ( item as AbstractFile ) != null )
        {
          exists = ( item.ParentFolder.GetFile( NameTextBox.Text ).Exists );
        }
        else
        {
          exists = ( item.ParentFolder.GetFolder( NameTextBox.Text ).Exists );
        }

        if( exists )
        {
          MessageBox.Show( 
            this, 
            "Cannot rename " + item.Name + ": An item with the name you specified already exists. Specify a different name.", 
            "Error renamming item", 
            MessageBoxButtons.OK, 
            MessageBoxIcon.Error );

          return false;
        }
      }

      return true;
    }

    private void ToggleCustomPanels()
    {
      // The panel will be visible when:
      //    1. The item is a ZippedFile or ZippedFolder
      //    2. The item is another archive inside this one.

      MainTabControl.TabPages.Remove( ZipPage );

      if( ( m_item.RootFolder as ZippedFolder ) != null )
      {
        MainTabControl.TabPages.Add( ZipPage );
      }

      if(  ( m_item.HostFile != null )
        && ( m_item.HostFile.RootFolder is ZippedFolder )
        && ( m_item.RootFolder == m_item ) )
      {
        MainTabControl.TabPages.Add( ZipPage );
      }
    }

    private string GetFormattedSize( long size )
    {
      return size.ToString( "#,##0" ) + " bytes";
    }

    private void ItemPicture_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      // We draw the icon this way to keep the alpha blending of the icon.
      IconCache.LargeIconList.Draw( e.Graphics, 0, 0, IconCache.GetIconIndex( m_item, false, true ) );
    }

    private void PropertyForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

    #region PRIVATE FIELDS

    private FileSystemItem m_item; // = null

    #endregion PRIVATE FIELDS

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PropertyForm));
      this.MainTabControl = new System.Windows.Forms.TabControl();
      this.GeneralPage = new System.Windows.Forms.TabPage();
      this.ArchiveAttributeCheck = new System.Windows.Forms.CheckBox();
      this.ItemPicture = new System.Windows.Forms.PictureBox();
      this.label9 = new System.Windows.Forms.Label();
      this.HiddenAttributeCheck = new System.Windows.Forms.CheckBox();
      this.ReadOnlyAttributeCheck = new System.Windows.Forms.CheckBox();
      this.SystemAttributeCheck = new System.Windows.Forms.CheckBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.LastAccessDateTimeLabel = new System.Windows.Forms.Label();
      this.ModificationDateTimeLabel = new System.Windows.Forms.Label();
      this.CreationDateTimeLabel = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.CompressedSizeLabel = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.SizeLabel = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.NameTextBox = new System.Windows.Forms.TextBox();
      this.ZipPage = new System.Windows.Forms.TabPage();
      this.CommentBox = new System.Windows.Forms.GroupBox();
      this.CommentsText = new System.Windows.Forms.TextBox();
      this.CancelBtn = new System.Windows.Forms.Button();
      this.OkBtn = new System.Windows.Forms.Button();
      this.MainTabControl.SuspendLayout();
      this.GeneralPage.SuspendLayout();
      this.ZipPage.SuspendLayout();
      this.CommentBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // MainTabControl
      // 
      this.MainTabControl.Controls.Add(this.GeneralPage);
      this.MainTabControl.Controls.Add(this.ZipPage);
      this.MainTabControl.Location = new System.Drawing.Point(8, 8);
      this.MainTabControl.Name = "MainTabControl";
      this.MainTabControl.SelectedIndex = 0;
      this.MainTabControl.Size = new System.Drawing.Size(320, 360);
      this.MainTabControl.TabIndex = 0;
      // 
      // GeneralPage
      // 
      this.GeneralPage.Controls.Add(this.ArchiveAttributeCheck);
      this.GeneralPage.Controls.Add(this.ItemPicture);
      this.GeneralPage.Controls.Add(this.label9);
      this.GeneralPage.Controls.Add(this.HiddenAttributeCheck);
      this.GeneralPage.Controls.Add(this.ReadOnlyAttributeCheck);
      this.GeneralPage.Controls.Add(this.SystemAttributeCheck);
      this.GeneralPage.Controls.Add(this.groupBox3);
      this.GeneralPage.Controls.Add(this.LastAccessDateTimeLabel);
      this.GeneralPage.Controls.Add(this.ModificationDateTimeLabel);
      this.GeneralPage.Controls.Add(this.CreationDateTimeLabel);
      this.GeneralPage.Controls.Add(this.label6);
      this.GeneralPage.Controls.Add(this.label5);
      this.GeneralPage.Controls.Add(this.label4);
      this.GeneralPage.Controls.Add(this.groupBox2);
      this.GeneralPage.Controls.Add(this.CompressedSizeLabel);
      this.GeneralPage.Controls.Add(this.label3);
      this.GeneralPage.Controls.Add(this.SizeLabel);
      this.GeneralPage.Controls.Add(this.label2);
      this.GeneralPage.Controls.Add(this.groupBox1);
      this.GeneralPage.Controls.Add(this.NameTextBox);
      this.GeneralPage.Location = new System.Drawing.Point(4, 22);
      this.GeneralPage.Name = "GeneralPage";
      this.GeneralPage.Size = new System.Drawing.Size(312, 334);
      this.GeneralPage.TabIndex = 0;
      this.GeneralPage.Text = "General";
      // 
      // ArchiveAttributeCheck
      // 
      this.ArchiveAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ArchiveAttributeCheck.Location = new System.Drawing.Point(96, 304);
      this.ArchiveAttributeCheck.Name = "ArchiveAttributeCheck";
      this.ArchiveAttributeCheck.Size = new System.Drawing.Size(200, 16);
      this.ArchiveAttributeCheck.TabIndex = 5;
      this.ArchiveAttributeCheck.Text = "Archive";
      // 
      // ItemPicture
      // 
      this.ItemPicture.Location = new System.Drawing.Point(16, 8);
      this.ItemPicture.Name = "ItemPicture";
      this.ItemPicture.Size = new System.Drawing.Size(48, 48);
      this.ItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.ItemPicture.TabIndex = 19;
      this.ItemPicture.TabStop = false;
      this.ItemPicture.Paint += new System.Windows.Forms.PaintEventHandler(this.ItemPicture_Paint);
      // 
      // label9
      // 
      this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label9.Location = new System.Drawing.Point(8, 232);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(80, 16);
      this.label9.TabIndex = 18;
      this.label9.Text = "Attributes:";
      // 
      // HiddenAttributeCheck
      // 
      this.HiddenAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.HiddenAttributeCheck.Location = new System.Drawing.Point(96, 256);
      this.HiddenAttributeCheck.Name = "HiddenAttributeCheck";
      this.HiddenAttributeCheck.Size = new System.Drawing.Size(200, 16);
      this.HiddenAttributeCheck.TabIndex = 3;
      this.HiddenAttributeCheck.Text = "Hidden";
      // 
      // ReadOnlyAttributeCheck
      // 
      this.ReadOnlyAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ReadOnlyAttributeCheck.Location = new System.Drawing.Point(96, 232);
      this.ReadOnlyAttributeCheck.Name = "ReadOnlyAttributeCheck";
      this.ReadOnlyAttributeCheck.Size = new System.Drawing.Size(200, 16);
      this.ReadOnlyAttributeCheck.TabIndex = 2;
      this.ReadOnlyAttributeCheck.Text = "Read-only";
      // 
      // SystemAttributeCheck
      // 
      this.SystemAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.SystemAttributeCheck.Location = new System.Drawing.Point(96, 280);
      this.SystemAttributeCheck.Name = "SystemAttributeCheck";
      this.SystemAttributeCheck.Size = new System.Drawing.Size(200, 16);
      this.SystemAttributeCheck.TabIndex = 4;
      this.SystemAttributeCheck.Text = "System";
      // 
      // groupBox3
      // 
      this.groupBox3.Location = new System.Drawing.Point(8, 216);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(296, 4);
      this.groupBox3.TabIndex = 14;
      this.groupBox3.TabStop = false;
      // 
      // LastAccessDateTimeLabel
      // 
      this.LastAccessDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.LastAccessDateTimeLabel.Location = new System.Drawing.Point(96, 192);
      this.LastAccessDateTimeLabel.Name = "LastAccessDateTimeLabel";
      this.LastAccessDateTimeLabel.Size = new System.Drawing.Size(200, 16);
      this.LastAccessDateTimeLabel.TabIndex = 13;
      this.LastAccessDateTimeLabel.Text = "2006-03-02";
      // 
      // ModificationDateTimeLabel
      // 
      this.ModificationDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.ModificationDateTimeLabel.Location = new System.Drawing.Point(96, 168);
      this.ModificationDateTimeLabel.Name = "ModificationDateTimeLabel";
      this.ModificationDateTimeLabel.Size = new System.Drawing.Size(200, 16);
      this.ModificationDateTimeLabel.TabIndex = 12;
      this.ModificationDateTimeLabel.Text = "2006-03-02";
      // 
      // CreationDateTimeLabel
      // 
      this.CreationDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CreationDateTimeLabel.Location = new System.Drawing.Point(96, 144);
      this.CreationDateTimeLabel.Name = "CreationDateTimeLabel";
      this.CreationDateTimeLabel.Size = new System.Drawing.Size(200, 16);
      this.CreationDateTimeLabel.TabIndex = 11;
      this.CreationDateTimeLabel.Text = "2006-03-02";
      // 
      // label6
      // 
      this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label6.Location = new System.Drawing.Point(8, 192);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(88, 16);
      this.label6.TabIndex = 10;
      this.label6.Text = "Accessed:";
      // 
      // label5
      // 
      this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label5.Location = new System.Drawing.Point(8, 168);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(88, 16);
      this.label5.TabIndex = 9;
      this.label5.Text = "Modified:";
      // 
      // label4
      // 
      this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label4.Location = new System.Drawing.Point(8, 144);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(88, 16);
      this.label4.TabIndex = 8;
      this.label4.Text = "Created:";
      // 
      // groupBox2
      // 
      this.groupBox2.Location = new System.Drawing.Point(8, 128);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(296, 4);
      this.groupBox2.TabIndex = 7;
      this.groupBox2.TabStop = false;
      // 
      // CompressedSizeLabel
      // 
      this.CompressedSizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CompressedSizeLabel.Location = new System.Drawing.Point(96, 104);
      this.CompressedSizeLabel.Name = "CompressedSizeLabel";
      this.CompressedSizeLabel.Size = new System.Drawing.Size(200, 16);
      this.CompressedSizeLabel.TabIndex = 6;
      this.CompressedSizeLabel.Text = "0 KB";
      // 
      // label3
      // 
      this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label3.Location = new System.Drawing.Point(8, 104);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(80, 16);
      this.label3.TabIndex = 5;
      this.label3.Text = "Compressed:";
      // 
      // SizeLabel
      // 
      this.SizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.SizeLabel.Location = new System.Drawing.Point(96, 80);
      this.SizeLabel.Name = "SizeLabel";
      this.SizeLabel.Size = new System.Drawing.Size(200, 16);
      this.SizeLabel.TabIndex = 4;
      this.SizeLabel.Text = "0 KB";
      // 
      // label2
      // 
      this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.label2.Location = new System.Drawing.Point(8, 80);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(72, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "Size:";
      // 
      // groupBox1
      // 
      this.groupBox1.Location = new System.Drawing.Point(8, 64);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(296, 4);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      // 
      // NameTextBox
      // 
      this.NameTextBox.Location = new System.Drawing.Point(96, 16);
      this.NameTextBox.Name = "NameTextBox";
      this.NameTextBox.Size = new System.Drawing.Size(200, 20);
      this.NameTextBox.TabIndex = 1;
      this.NameTextBox.Text = "";
      // 
      // ZipPage
      // 
      this.ZipPage.Controls.Add(this.CommentBox);
      this.ZipPage.Location = new System.Drawing.Point(4, 22);
      this.ZipPage.Name = "ZipPage";
      this.ZipPage.Size = new System.Drawing.Size(312, 334);
      this.ZipPage.TabIndex = 1;
      this.ZipPage.Text = "Zip properties";
      // 
      // CommentBox
      // 
      this.CommentBox.Controls.Add(this.CommentsText);
      this.CommentBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CommentBox.Location = new System.Drawing.Point(8, 8);
      this.CommentBox.Name = "CommentBox";
      this.CommentBox.Size = new System.Drawing.Size(296, 320);
      this.CommentBox.TabIndex = 0;
      this.CommentBox.TabStop = false;
      this.CommentBox.Text = "Comments";
      // 
      // CommentsText
      // 
      this.CommentsText.AutoSize = false;
      this.CommentsText.Location = new System.Drawing.Point(8, 16);
      this.CommentsText.Multiline = true;
      this.CommentsText.Name = "CommentsText";
      this.CommentsText.Size = new System.Drawing.Size(280, 296);
      this.CommentsText.TabIndex = 6;
      this.CommentsText.Text = "";
      // 
      // CancelBtn
      // 
      this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.CancelBtn.Location = new System.Drawing.Point(248, 376);
      this.CancelBtn.Name = "CancelBtn";
      this.CancelBtn.Size = new System.Drawing.Size(80, 23);
      this.CancelBtn.TabIndex = 8;
      this.CancelBtn.Text = "&Cancel";
      // 
      // OkBtn
      // 
      this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.OkBtn.Location = new System.Drawing.Point(160, 376);
      this.OkBtn.Name = "OkBtn";
      this.OkBtn.Size = new System.Drawing.Size(80, 23);
      this.OkBtn.TabIndex = 7;
      this.OkBtn.Text = "&Ok";
      // 
      // PropertyForm
      // 
      this.AcceptButton = this.OkBtn;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.CancelBtn;
      this.ClientSize = new System.Drawing.Size(336, 408);
      this.Controls.Add(this.OkBtn);
      this.Controls.Add(this.CancelBtn);
      this.Controls.Add(this.MainTabControl);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "PropertyForm";
      this.Text = "Properties";
      this.Closing += new System.ComponentModel.CancelEventHandler(this.PropertyForm_Closing);
      this.MainTabControl.ResumeLayout(false);
      this.GeneralPage.ResumeLayout(false);
      this.ZipPage.ResumeLayout(false);
      this.CommentBox.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

    #region Windows Form Designer generated fields

    private System.Windows.Forms.CheckBox ArchiveAttributeCheck;
    private System.Windows.Forms.PictureBox ItemPicture;
    private System.Windows.Forms.TabPage ZipPage;
    private System.Windows.Forms.GroupBox CommentBox;
    private System.Windows.Forms.TextBox CommentsText;
    private System.Windows.Forms.TabControl MainTabControl;
    private System.Windows.Forms.Label ModificationDateTimeLabel;
    private System.Windows.Forms.Label LastAccessDateTimeLabel;
    private System.Windows.Forms.TabPage GeneralPage;
    private System.Windows.Forms.TextBox NameTextBox;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label SizeLabel;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label CompressedSizeLabel;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label CreationDateTimeLabel;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.Button CancelBtn;
    private System.Windows.Forms.Button OkBtn;
    private System.Windows.Forms.CheckBox SystemAttributeCheck;
    private System.Windows.Forms.CheckBox ReadOnlyAttributeCheck;
    private System.Windows.Forms.CheckBox HiddenAttributeCheck;
    private System.Windows.Forms.Label label9;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    #endregion Windows Form Designer generated fields
	}
}
