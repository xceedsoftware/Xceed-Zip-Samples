/* Xceed Zip for .NET - ZipManager Sample Application
 * Copyright (c) 2000-2002 - Xceed Software Inc.
 * 
 * [ZipManager.cs]
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
using System.Data;
using Microsoft.Win32;
using Xceed.FileSystem;
using Xceed.Zip;
using Xceed.Compression;

namespace Xceed.Zip.Samples.ZipManager
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
  public class ZipManager : System.Windows.Forms.Form
  {
    internal System.Windows.Forms.MenuItem EditMenu;
    internal System.Windows.Forms.MenuItem ZipFilesMenu;
    internal System.Windows.Forms.MenuItem UnzipFilesMenu;
    internal System.Windows.Forms.MenuItem DeleteFilesMenu;
    internal System.Windows.Forms.StatusBarPanel MessagePanel;
    internal System.Windows.Forms.ColumnHeader AttributesColumn;
    internal System.Windows.Forms.MenuItem ExitMenu;
    internal System.Windows.Forms.ColumnHeader CompSizeColumn;
    internal System.Windows.Forms.ColumnHeader SizeColumn;
    internal System.Windows.Forms.ColumnHeader RatioColumn;
    internal System.Windows.Forms.MenuItem CompressionLevelMenu;
    internal System.Windows.Forms.MenuItem HighestLevelMenu;
    internal System.Windows.Forms.MenuItem NormalLevelMenu;
    internal System.Windows.Forms.MenuItem LowestLevelMenu;
    internal System.Windows.Forms.MenuItem NoneLevelMenu;
    internal System.Windows.Forms.SaveFileDialog NewZipFileDialog;
    internal System.Windows.Forms.MenuItem NewZipMenu;
    internal System.Windows.Forms.MenuItem FileMenu;
    internal System.Windows.Forms.MenuItem OpenZipMenu;
    internal System.Windows.Forms.MenuItem CloseZipMenu;
    internal System.Windows.Forms.MenuItem MenuItem4;
    internal System.Windows.Forms.MenuItem OptionMenu;
    internal System.Windows.Forms.MenuItem OptionsRememberPathMenu;
    internal System.Windows.Forms.MenuItem SfxOptionsMenu;
    internal System.Windows.Forms.Panel StatusBarPanel;
    internal System.Windows.Forms.StatusBar ZipManagerStatusBar;
    internal System.Windows.Forms.ProgressBar TotalBytesProgressBar;
    internal System.Windows.Forms.SaveFileDialog UnzipFilesDialog;
    internal System.Windows.Forms.MainMenu ZipManagerMenu;
    internal System.Windows.Forms.MenuItem HelpMenu;
    internal System.Windows.Forms.MenuItem AboutMenu;
    internal System.Windows.Forms.ToolBar ZipManagerToolBar;
    internal System.Windows.Forms.ToolBarButton NewZipButton;
    internal System.Windows.Forms.ToolBarButton OpenZipButton;
    internal System.Windows.Forms.ToolBarButton ZipFilesButton;
    internal System.Windows.Forms.ToolBarButton UnzipFilesButtom;
    internal System.Windows.Forms.ToolBarButton DeleteFilesButton;
    internal System.Windows.Forms.OpenFileDialog ZipFilesDialog;
    internal System.Windows.Forms.ColumnHeader FilenameColumn;
    internal System.Windows.Forms.ColumnHeader LastWriteColumn;
    internal System.Windows.Forms.OpenFileDialog OpenZipDialog;
    internal System.Windows.Forms.ListView ZipContentsList;
    internal System.Windows.Forms.MenuItem CompressionMethodMenu;
    internal System.Windows.Forms.MenuItem StoredMethodMenu;
    internal System.Windows.Forms.MenuItem DeflatedMethodMenu;
    internal System.Windows.Forms.MenuItem Deflated64MethodMenu;
    internal System.Windows.Forms.MenuItem OptionsEncryptFilesMenu;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public ZipManager()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      // Update menus and buttons
      RefreshMenuStates();

    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
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
      this.EditMenu = new System.Windows.Forms.MenuItem();
      this.ZipFilesMenu = new System.Windows.Forms.MenuItem();
      this.UnzipFilesMenu = new System.Windows.Forms.MenuItem();
      this.DeleteFilesMenu = new System.Windows.Forms.MenuItem();
      this.MessagePanel = new System.Windows.Forms.StatusBarPanel();
      this.AttributesColumn = new System.Windows.Forms.ColumnHeader();
      this.ExitMenu = new System.Windows.Forms.MenuItem();
      this.CompSizeColumn = new System.Windows.Forms.ColumnHeader();
      this.SizeColumn = new System.Windows.Forms.ColumnHeader();
      this.RatioColumn = new System.Windows.Forms.ColumnHeader();
      this.CompressionLevelMenu = new System.Windows.Forms.MenuItem();
      this.HighestLevelMenu = new System.Windows.Forms.MenuItem();
      this.NormalLevelMenu = new System.Windows.Forms.MenuItem();
      this.LowestLevelMenu = new System.Windows.Forms.MenuItem();
      this.NoneLevelMenu = new System.Windows.Forms.MenuItem();
      this.NewZipFileDialog = new System.Windows.Forms.SaveFileDialog();
      this.NewZipMenu = new System.Windows.Forms.MenuItem();
      this.FileMenu = new System.Windows.Forms.MenuItem();
      this.OpenZipMenu = new System.Windows.Forms.MenuItem();
      this.CloseZipMenu = new System.Windows.Forms.MenuItem();
      this.MenuItem4 = new System.Windows.Forms.MenuItem();
      this.OptionMenu = new System.Windows.Forms.MenuItem();
      this.OptionsRememberPathMenu = new System.Windows.Forms.MenuItem();
      this.CompressionMethodMenu = new System.Windows.Forms.MenuItem();
      this.StoredMethodMenu = new System.Windows.Forms.MenuItem();
      this.DeflatedMethodMenu = new System.Windows.Forms.MenuItem();
      this.Deflated64MethodMenu = new System.Windows.Forms.MenuItem();
      this.OptionsEncryptFilesMenu = new System.Windows.Forms.MenuItem();
      this.SfxOptionsMenu = new System.Windows.Forms.MenuItem();
      this.StatusBarPanel = new System.Windows.Forms.Panel();
      this.ZipManagerStatusBar = new System.Windows.Forms.StatusBar();
      this.TotalBytesProgressBar = new System.Windows.Forms.ProgressBar();
      this.UnzipFilesDialog = new System.Windows.Forms.SaveFileDialog();
      this.ZipManagerMenu = new System.Windows.Forms.MainMenu();
      this.HelpMenu = new System.Windows.Forms.MenuItem();
      this.AboutMenu = new System.Windows.Forms.MenuItem();
      this.ZipManagerToolBar = new System.Windows.Forms.ToolBar();
      this.NewZipButton = new System.Windows.Forms.ToolBarButton();
      this.OpenZipButton = new System.Windows.Forms.ToolBarButton();
      this.ZipFilesButton = new System.Windows.Forms.ToolBarButton();
      this.UnzipFilesButtom = new System.Windows.Forms.ToolBarButton();
      this.DeleteFilesButton = new System.Windows.Forms.ToolBarButton();
      this.ZipFilesDialog = new System.Windows.Forms.OpenFileDialog();
      this.FilenameColumn = new System.Windows.Forms.ColumnHeader();
      this.LastWriteColumn = new System.Windows.Forms.ColumnHeader();
      this.OpenZipDialog = new System.Windows.Forms.OpenFileDialog();
      this.ZipContentsList = new System.Windows.Forms.ListView();
      ((System.ComponentModel.ISupportInitialize)(this.MessagePanel)).BeginInit();
      this.StatusBarPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // EditMenu
      // 
      this.EditMenu.Index = 1;
      this.EditMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.ZipFilesMenu,
                                                                             this.UnzipFilesMenu,
                                                                             this.DeleteFilesMenu});
      this.EditMenu.Text = "&Edit";
      // 
      // ZipFilesMenu
      // 
      this.ZipFilesMenu.Index = 0;
      this.ZipFilesMenu.Shortcut = System.Windows.Forms.Shortcut.Ins;
      this.ZipFilesMenu.Text = "&Zip files...";
      this.ZipFilesMenu.Click += new System.EventHandler(this.ZipFilesMenu_Click);
      // 
      // UnzipFilesMenu
      // 
      this.UnzipFilesMenu.Index = 1;
      this.UnzipFilesMenu.Text = "&Unzip files...";
      this.UnzipFilesMenu.Click += new System.EventHandler(this.UnzipFilesMenu_Click);
      // 
      // DeleteFilesMenu
      // 
      this.DeleteFilesMenu.Index = 2;
      this.DeleteFilesMenu.Shortcut = System.Windows.Forms.Shortcut.Del;
      this.DeleteFilesMenu.Text = "&Delete files...";
      this.DeleteFilesMenu.Click += new System.EventHandler(this.DeleteFilesMenu_Click);
      // 
      // MessagePanel
      // 
      this.MessagePanel.Width = 2000;
      // 
      // AttributesColumn
      // 
      this.AttributesColumn.Text = "Attributes";
      // 
      // ExitMenu
      // 
      this.ExitMenu.Index = 4;
      this.ExitMenu.Text = "&Exit from ZipManager";
      this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
      // 
      // CompSizeColumn
      // 
      this.CompSizeColumn.Text = "Packed";
      // 
      // SizeColumn
      // 
      this.SizeColumn.Text = "Size";
      // 
      // RatioColumn
      // 
      this.RatioColumn.Text = "Ratio";
      // 
      // CompressionLevelMenu
      // 
      this.CompressionLevelMenu.Index = 2;
      this.CompressionLevelMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.HighestLevelMenu,
                                                                                         this.NormalLevelMenu,
                                                                                         this.LowestLevelMenu,
                                                                                         this.NoneLevelMenu});
      this.CompressionLevelMenu.Text = "&Compression level";
      // 
      // HighestLevelMenu
      // 
      this.HighestLevelMenu.Index = 0;
      this.HighestLevelMenu.Text = "&Highest";
      this.HighestLevelMenu.Click += new System.EventHandler(this.HighestLevelMenu_Click);
      // 
      // NormalLevelMenu
      // 
      this.NormalLevelMenu.Index = 1;
      this.NormalLevelMenu.Text = "No&rmal";
      this.NormalLevelMenu.Click += new System.EventHandler(this.NormalLevelMenu_Click);
      // 
      // LowestLevelMenu
      // 
      this.LowestLevelMenu.Index = 2;
      this.LowestLevelMenu.Text = "L&owest";
      this.LowestLevelMenu.Click += new System.EventHandler(this.LowestLevelMenu_Click);
      // 
      // NoneLevelMenu
      // 
      this.NoneLevelMenu.Index = 3;
      this.NoneLevelMenu.Text = "&None";
      this.NoneLevelMenu.Click += new System.EventHandler(this.NoneLevelMenu_Click);
      // 
      // NewZipFileDialog
      // 
      this.NewZipFileDialog.DefaultExt = "zip";
      this.NewZipFileDialog.Filter = "Zip Files (*.zip)|*.zip";
      this.NewZipFileDialog.RestoreDirectory = true;
      this.NewZipFileDialog.Title = "Enter the new Zip file name and destination";
      // 
      // NewZipMenu
      // 
      this.NewZipMenu.Index = 0;
      this.NewZipMenu.Text = "&New Zip file...";
      this.NewZipMenu.Click += new System.EventHandler(this.NewZipMenu_Click);
      // 
      // FileMenu
      // 
      this.FileMenu.Index = 0;
      this.FileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.NewZipMenu,
                                                                             this.OpenZipMenu,
                                                                             this.CloseZipMenu,
                                                                             this.MenuItem4,
                                                                             this.ExitMenu});
      this.FileMenu.Text = "&File";
      // 
      // OpenZipMenu
      // 
      this.OpenZipMenu.Index = 1;
      this.OpenZipMenu.Text = "&Open Zip File...";
      this.OpenZipMenu.Click += new System.EventHandler(this.OpenZipMenu_Click);
      // 
      // CloseZipMenu
      // 
      this.CloseZipMenu.Index = 2;
      this.CloseZipMenu.Text = "&Close Zip File";
      this.CloseZipMenu.Click += new System.EventHandler(this.CloseZipMenu_Click);
      // 
      // MenuItem4
      // 
      this.MenuItem4.Index = 3;
      this.MenuItem4.Text = "-";
      // 
      // OptionMenu
      // 
      this.OptionMenu.Index = 2;
      this.OptionMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                               this.OptionsRememberPathMenu,
                                                                               this.CompressionMethodMenu,
                                                                               this.CompressionLevelMenu,
                                                                               this.OptionsEncryptFilesMenu,
                                                                               this.SfxOptionsMenu});
      this.OptionMenu.Text = "&Options";
      // 
      // OptionsRememberPathMenu
      // 
      this.OptionsRememberPathMenu.Index = 0;
      this.OptionsRememberPathMenu.Text = "&Remember path when zipping";
      this.OptionsRememberPathMenu.Click += new System.EventHandler(this.OptionsRememberPathMenu_Click);
      // 
      // CompressionMethodMenu
      // 
      this.CompressionMethodMenu.Index = 1;
      this.CompressionMethodMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.StoredMethodMenu,
                                                                                          this.DeflatedMethodMenu,
                                                                                          this.Deflated64MethodMenu});
      this.CompressionMethodMenu.Text = "Compression &method";
      // 
      // StoredMethodMenu
      // 
      this.StoredMethodMenu.Index = 0;
      this.StoredMethodMenu.Text = "&Stored";
      this.StoredMethodMenu.Click += new System.EventHandler(this.StoredMethodMenu_Click);
      // 
      // DeflatedMethodMenu
      // 
      this.DeflatedMethodMenu.Index = 1;
      this.DeflatedMethodMenu.Text = "&Deflated";
      this.DeflatedMethodMenu.Click += new System.EventHandler(this.DeflatedMethodMenu_Click);
      // 
      // Deflated64MethodMenu
      // 
      this.Deflated64MethodMenu.Index = 2;
      this.Deflated64MethodMenu.Text = "Deflated&64";
      this.Deflated64MethodMenu.Click += new System.EventHandler(this.Deflated64MethodMenu_Click);
      // 
      // OptionsEncryptFilesMenu
      // 
      this.OptionsEncryptFilesMenu.Index = 3;
      this.OptionsEncryptFilesMenu.Text = "&Encrypt files";
      this.OptionsEncryptFilesMenu.Click += new System.EventHandler(this.OptionsEncryptFilesMenu_Click);
      // 
      // SfxOptionsMenu
      // 
      this.SfxOptionsMenu.Enabled = false;
      this.SfxOptionsMenu.Index = 4;
      this.SfxOptionsMenu.Text = "&Sfx options ";
      this.SfxOptionsMenu.Click += new System.EventHandler(this.SfxOptionsMenu_Click);
      // 
      // StatusBarPanel
      // 
      this.StatusBarPanel.Controls.Add(this.ZipManagerStatusBar);
      this.StatusBarPanel.Controls.Add(this.TotalBytesProgressBar);
      this.StatusBarPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.StatusBarPanel.Location = new System.Drawing.Point(0, 368);
      this.StatusBarPanel.Name = "StatusBarPanel";
      this.StatusBarPanel.Size = new System.Drawing.Size(544, 16);
      this.StatusBarPanel.TabIndex = 5;
      // 
      // ZipManagerStatusBar
      // 
      this.ZipManagerStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ZipManagerStatusBar.Location = new System.Drawing.Point(152, 0);
      this.ZipManagerStatusBar.Name = "ZipManagerStatusBar";
      this.ZipManagerStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
                                                                                           this.MessagePanel});
      this.ZipManagerStatusBar.ShowPanels = true;
      this.ZipManagerStatusBar.Size = new System.Drawing.Size(392, 16);
      this.ZipManagerStatusBar.TabIndex = 1;
      // 
      // TotalBytesProgressBar
      // 
      this.TotalBytesProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
      this.TotalBytesProgressBar.Location = new System.Drawing.Point(0, 0);
      this.TotalBytesProgressBar.Name = "TotalBytesProgressBar";
      this.TotalBytesProgressBar.Size = new System.Drawing.Size(152, 16);
      this.TotalBytesProgressBar.TabIndex = 0;
      // 
      // UnzipFilesDialog
      // 
      this.UnzipFilesDialog.AddExtension = false;
      this.UnzipFilesDialog.FileName = "Go to destination folder and press Save";
      this.UnzipFilesDialog.Filter = "All Files (*.*)|*.*";
      this.UnzipFilesDialog.OverwritePrompt = false;
      this.UnzipFilesDialog.RestoreDirectory = true;
      this.UnzipFilesDialog.Title = "Select destination folder...";
      // 
      // ZipManagerMenu
      // 
      this.ZipManagerMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                   this.FileMenu,
                                                                                   this.EditMenu,
                                                                                   this.OptionMenu,
                                                                                   this.HelpMenu});
      // 
      // HelpMenu
      // 
      this.HelpMenu.Index = 3;
      this.HelpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.AboutMenu});
      this.HelpMenu.Text = "&Help";
      // 
      // AboutMenu
      // 
      this.AboutMenu.Index = 0;
      this.AboutMenu.Text = "&About ZipManager";
      this.AboutMenu.Click += new System.EventHandler(this.AboutMenu_Click);
      // 
      // ZipManagerToolBar
      // 
      this.ZipManagerToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                         this.NewZipButton,
                                                                                         this.OpenZipButton,
                                                                                         this.ZipFilesButton,
                                                                                         this.UnzipFilesButtom,
                                                                                         this.DeleteFilesButton});
      this.ZipManagerToolBar.DropDownArrows = true;
      this.ZipManagerToolBar.Location = new System.Drawing.Point(0, 0);
      this.ZipManagerToolBar.Name = "ZipManagerToolBar";
      this.ZipManagerToolBar.ShowToolTips = true;
      this.ZipManagerToolBar.Size = new System.Drawing.Size(544, 42);
      this.ZipManagerToolBar.TabIndex = 3;
      this.ZipManagerToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ZipManagerToolBar_ButtonClick);
      // 
      // NewZipButton
      // 
      this.NewZipButton.Text = "New";
      // 
      // OpenZipButton
      // 
      this.OpenZipButton.Text = "Open";
      // 
      // ZipFilesButton
      // 
      this.ZipFilesButton.Text = "Zip";
      // 
      // UnzipFilesButtom
      // 
      this.UnzipFilesButtom.Text = "Unzip";
      // 
      // DeleteFilesButton
      // 
      this.DeleteFilesButton.Text = "Delete";
      // 
      // ZipFilesDialog
      // 
      this.ZipFilesDialog.AddExtension = false;
      this.ZipFilesDialog.Filter = "All Files (*.*)|*.*";
      this.ZipFilesDialog.Multiselect = true;
      this.ZipFilesDialog.Title = "Select files to zip...";
      // 
      // FilenameColumn
      // 
      this.FilenameColumn.Text = "Filename";
      this.FilenameColumn.Width = 212;
      // 
      // LastWriteColumn
      // 
      this.LastWriteColumn.Text = "Modified";
      // 
      // OpenZipDialog
      // 
      this.OpenZipDialog.DefaultExt = "zip";
      this.OpenZipDialog.Filter = "Zip Files (*.zip)|*.zip|All Files (*.*)|*.*";
      this.OpenZipDialog.Title = "Select Zip file to open...";
      // 
      // ZipContentsList
      // 
      this.ZipContentsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                      this.FilenameColumn,
                                                                                      this.LastWriteColumn,
                                                                                      this.SizeColumn,
                                                                                      this.CompSizeColumn,
                                                                                      this.RatioColumn,
                                                                                      this.AttributesColumn});
      this.ZipContentsList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ZipContentsList.Location = new System.Drawing.Point(0, 42);
      this.ZipContentsList.Name = "ZipContentsList";
      this.ZipContentsList.Size = new System.Drawing.Size(544, 326);
      this.ZipContentsList.TabIndex = 4;
      this.ZipContentsList.View = System.Windows.Forms.View.Details;
      this.ZipContentsList.SelectedIndexChanged += new System.EventHandler(this.ZipContentsList_SelectedIndexChanged);
      // 
      // ZipManager
      // 
      this.AllowDrop = true;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(544, 384);
      this.Controls.Add(this.ZipContentsList);
      this.Controls.Add(this.StatusBarPanel);
      this.Controls.Add(this.ZipManagerToolBar);
      this.Menu = this.ZipManagerMenu;
      this.Name = "ZipManager";
      this.Text = "ZipManager - Xceed Zip for .NET";
      this.Load += new System.EventHandler(this.ZipManager_Load);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.ZipManager_DragOver);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ZipManager_DragDrop);
      ((System.ComponentModel.ISupportInitialize)(this.MessagePanel)).EndInit();
      this.StatusBarPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      /* ================================
       * How to license Xceed components 
       * ================================       
       * To license your product, set the LicenseKey property to a valid trial or registered license key 
       * in the main entry point of the application to ensure components are licensed before any of the 
       * component methods are called.      
       * 
       * If the component is used in a DLL project (no entry point available), it is 
       * recommended that the LicenseKey property be set in a static constructor of a 
       * class that will be accessed systematically before any component is instantiated or, 
       * you can simply set the LicenseKey property immediately BEFORE you instantiate 
       * an instance of the component.
       * 
       * For instance, if you wanted to deploy this sample, the license key needs to be set here.
       * If your trial period has expired, you must purchase a registered license key,
       * uncomment the next line of code, and insert your registerd license key.
       * For more information, consult the "How the 45-day trial works" and the 
       * "How to license the component once you purchase" topics in the documentation of this product.
       */
        
      // Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 

      Application.Run(new ZipManager());
    }

    #region " Menu Events "

    private void NewZipMenu_Click(object sender, System.EventArgs e)
    {
      NewZipFile();
      SfxOptionsMenu.Enabled = true;
    }

    private void OpenZipMenu_Click(object sender, System.EventArgs e)
    {
      OpenZipFile();
      SfxOptionsMenu.Enabled = false;
    }

    private void CloseZipMenu_Click(object sender, System.EventArgs e)
    {
      CloseZipFile();
      SfxOptionsMenu.Enabled = false;
    }

    private void ExitMenu_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    private void ZipFilesMenu_Click(object sender, System.EventArgs e)
    {
      ZipFiles();
    }

    private void UnzipFilesMenu_Click(object sender, System.EventArgs e)
    {
      UnzipFiles();
    }

    private void DeleteFilesMenu_Click(object sender, System.EventArgs e)
    {
      DeleteFiles();
    }

    private void OptionsRememberPathMenu_Click(object sender, System.EventArgs e)
    {
      OptionsRememberPathMenu.Checked = !OptionsRememberPathMenu.Checked;

      RegistryKey regKey = GetZipManagerRegistryKey();

      if( regKey != null )
      {
        regKey.SetValue( "RememberPath", OptionsRememberPathMenu.Checked );
        regKey.Close();
      }
    }

    private void OptionsEncryptFilesMenu_Click(object sender, System.EventArgs e)
    {
      OptionsEncryptFilesMenu.Checked = !OptionsEncryptFilesMenu.Checked;

      RegistryKey regKey = GetZipManagerRegistryKey();

      if( regKey != null )
      {
        regKey.SetValue( "EncryptFiles", OptionsEncryptFilesMenu.Checked );
        regKey.Close();
      }
    }

    private void SfxOptionsMenu_Click(object sender, System.EventArgs e)
    {
      if( m_zipRoot != null )
      {
        SfxOptions sfxoptions = new SfxOptions( m_zipRoot.SfxPrefix );
        sfxoptions.ShowDialog();
        m_zipRoot.SfxPrefix = sfxoptions.SfxPrefix;

        if( m_zipRoot.SfxPrefix != null )
        {
          m_zipRoot.ZipFile.Name = m_zipRoot.ZipFile.Name.Substring( 0, m_zipRoot.ZipFile.Name.Length - 4 ) + ".exe";
        }
      }
      else
      {
        MessageBox.Show( "No zip file open!" );
      }
    }

    private void StoredMethodMenu_Click(object sender, System.EventArgs e)
    {
      CheckMethodMenu( CompressionMethod.Stored );
    }

    private void DeflatedMethodMenu_Click(object sender, System.EventArgs e)
    {
      CheckMethodMenu( CompressionMethod.Deflated );
    }

    private void Deflated64MethodMenu_Click(object sender, System.EventArgs e)
    {
      CheckMethodMenu( CompressionMethod.Deflated64 );
    }

    private void HighestLevelMenu_Click(object sender, System.EventArgs e)
    {
      CheckLevelMenu( CompressionLevel.Highest );
    }

    private void NormalLevelMenu_Click(object sender, System.EventArgs e)
    {
      CheckLevelMenu( CompressionLevel.Normal );
    }

    private void LowestLevelMenu_Click(object sender, System.EventArgs e)
    {
      CheckLevelMenu( CompressionLevel.Lowest );
    }

    private void NoneLevelMenu_Click(object sender, System.EventArgs e)
    {
      CheckLevelMenu( CompressionLevel.None );
    }

    private void AboutMenu_Click(object sender, System.EventArgs e)
    {
      AboutBox();
    }

    #endregion " Menu Events "

    #region " Form Events "
    
    private void ZipManager_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      string[] droppedFiles;
      DiskFile file = null;
      ZipArchive zipFile;
      bool open = false;
      bool add = false;

    
      if( e.Data.GetDataPresent( DataFormats.FileDrop, false ) )
      {
        try
        {
          droppedFiles = ( string[] )e.Data.GetData(DataFormats.FileDrop, false);

          if(  droppedFiles.Length == 1 )
          {
            // The user dropped only one file. We check if it's a zip file
            file = new DiskFile(droppedFiles[0]);
            try
            {
              zipFile = new ZipArchive(file);
              open = true;
            }
            catch( InvalidZipStructureException )
            {
              // It's not a zip file. We assume the user wants to add the
              // file to the current archive
              add = true;
            }
          }
          else
          {
            // The user dropped more than one file. We'll eventually add
            // them to the current archive.
            add = true;
          }

          if( open )
          {
            // Open the dropped zip file
            OpenZipFile(file.FullName);
          }
          else
          {
            if( add )
            {
              // Add the dropped file(s) to the current archive if there
              // is one.
              if( m_zipRoot != null )
              {
                Activate();
                if( MessageBox.Show( this, "Add the file(s) to the archive?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
                {
                  ZipFiles(droppedFiles);
                }
              }
              else
              {
                MessagePanel.Text = "No opened zip file";
              }
            }
          }
        }
        catch( Exception except )
        {
          MessagePanel.Text = except.Message;
        }
      }
    }
   
    private void ZipManager_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
      if( e.Data.GetDataPresent(DataFormats.FileDrop, false) )
      {
        e.Effect = DragDropEffects.Copy;
      }
   
    }
    
    private void ZipManager_Load(object sender, System.EventArgs e)
    {
      RegistryKey regKey = GetZipManagerRegistryKey();

      if( regKey != null )
      {
        OptionsRememberPathMenu.Checked =  bool.Parse( ( string )regKey.GetValue( "RememberPath", "false" ) );
        CheckLevelMenu( ( CompressionLevel )regKey.GetValue( "CompressionLevel", CompressionLevel.Normal ) );
        CheckMethodMenu( ( CompressionMethod )regKey.GetValue( "CompressionMethod", CompressionMethod.Deflated ) );
        OptionsEncryptFilesMenu.Checked = bool.Parse( ( string )regKey.GetValue( "EncryptFiles", "false" ) );

        regKey.Close();
      }
     
      m_zipEvents.DiskRequired += new DiskRequiredEventHandler( m_zipEvents_DiskRequired );
      m_zipEvents.ByteProgression += new ByteProgressionEventHandler( m_zipEvents_ByteProgression );
      m_zipEvents.ItemException += new ItemExceptionEventHandler( m_zipEvents_ItemException );
    }

    #endregion " Form Events "

    #region " ToolBar Events "

    private void ZipManagerToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
    {
      switch( ZipManagerToolBar.Buttons.IndexOf(e.Button) )
      {
        case 0:
          NewZipFile();
          SfxOptionsMenu.Enabled = true;
          break;
        case 1:
          OpenZipFile();
          SfxOptionsMenu.Enabled = false;
          break;
        case 2:
          ZipFiles();
          break;
        case 3:
          UnzipFiles();
          break;
        case 4:
          DeleteFiles();
          break;
      }
    }

    #endregion " ToolBar Events "

    #region " FileSystem Events "
    
    private void m_zipEvents_ByteProgression(object sender, Xceed.FileSystem.ByteProgressionEventArgs e ) 
    {
      TotalBytesProgressBar.Value = e.AllFilesBytes.Percent % 100;
    }

    #endregion " FileSystem Events "

    #region " Zip Events "
    
    private void m_zipEvents_DiskRequired(object sender, Xceed.Zip.DiskRequiredEventArgs e ) 
    {
      if( e.Action == DiskRequiredAction.Fail )
      {
        DialogResult result = MessageBox.Show( "Insert another disk.", "Insert disk.", MessageBoxButtons.OKCancel );

        if( result == DialogResult.OK )
          e.Action = DiskRequiredAction.Continue;
      }
    }

    private void m_zipEvents_ItemException(object sender, ItemExceptionEventArgs e)
    {
      if( e.Exception is InvalidDecryptionPasswordException )
      {
        ZippedFile file = e.CurrentItem as ZippedFile;

        if( file != null )
        {
          using( PasswordPrompt passwordForm = new PasswordPrompt() )
          {
            string password = m_zipRoot.DefaultDecryptionPassword;

            EncryptionMethod method = file.EncryptionMethod;
            int strength = file.EncryptionStrength;

            DialogResult result = passwordForm.ShowDialog( 
              this, 
              ref password, 
              "Encrypted file", 
              string.Format( "File '{0}' is encrypted. You must provide a password in order to unzip it.", file.FullName ),
              "Decryption Password:",
              ref method,
              ref strength,
              true );

            if( result == DialogResult.OK )
            {
              m_zipRoot.DefaultDecryptionPassword = password;
              e.Action = ItemExceptionAction.Retry;
            }
            else if( result == DialogResult.Ignore )
            {
              e.Action = ItemExceptionAction.Ignore;
            }
          }
        }
      }
      else if( e.Exception is ItemIsReadOnlyException )
      {
        // We could ask the user if he wants us to overwrite this read-only file.
        // Or we can force out the read-only attribute and retry!
        e.TargetItem.Attributes &= ~System.IO.FileAttributes.ReadOnly;
        e.Action = ItemExceptionAction.Retry;
      }
    }

    #endregion " Zip Events "


    #region " Controls Events "

    private void ZipContentsList_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      RefreshMenuStates();
    }

    #endregion " Controls Events "

    #region " Private Methods "

    private void RefreshMenuStates()
    {
      // File menu children and related buttons
      CloseZipMenu.Enabled = !( m_zipRoot == null );

      // Edit menu children and related buttons
      ZipFilesMenu.Enabled = CloseZipMenu.Enabled;
      ZipFilesButton.Enabled = CloseZipMenu.Enabled;

      UnzipFilesMenu.Enabled = CloseZipMenu.Enabled;
      UnzipFilesButtom.Enabled = CloseZipMenu.Enabled;
      DeleteFilesMenu.Enabled = (ZipContentsList.SelectedItems.Count > 0);
      DeleteFilesButton.Enabled = (ZipContentsList.SelectedItems.Count > 0);
    }

    private void FillFileList()
    {
      Cursor.Current = Cursors.WaitCursor;
      ZipContentsList.Items.Clear();

      if( m_zipRoot != null )
      {
        try
        {
          AbstractFile[] Files = m_zipRoot.GetFiles(m_zipEvents, null, true);

          // We want to freeze the list update while we add items.
          ZipContentsList.BeginUpdate();

          try
          {
            foreach( AbstractFile File in Files )
            {
              ZipContentsList.Items.Add( new ZipContentsItem( ( ZippedFile ) File ) );
            }
          }
          finally
          {
            ZipContentsList.EndUpdate();
          }
        }
        catch( Exception except )
        {
          MessagePanel.Text = except.Message;
        }
      }
     
      Cursor.Current = Cursors.Default;
    }

    private void NewZipFile()
    {
      DiskFile ZipFile;

      if( NewZipFileDialog.ShowDialog() == DialogResult.OK )
      {
        try
        {
          ZipFile = new DiskFile(NewZipFileDialog.FileName);

          if( ZipFile.Exists )
          {
            ZipFile.Delete();
          }

          ZipFile.Create();
          OpenZipFile(NewZipFileDialog.FileName);
        }
        catch( Exception except )
        {
          MessagePanel.Text = except.Message;
        }
      }
    }

    private void OpenZipFile()
    {
      // Ask for file to open using OpenZipDialog
      if( OpenZipDialog.ShowDialog() == DialogResult.OK ) 
      {
        OpenZipFile(OpenZipDialog.FileName);
      }
    }

    private void OpenZipFile( string ZipFilename )
    {
      try
      {
        DiskFile ZipFile = new DiskFile(ZipFilename);

        if( ZipFile.Exists )
        {
          m_zipRoot = new ZipArchive(ZipFile);
          FillFileList();
          RefreshMenuStates();
        }

        MessagePanel.Text = "Zip file opened";
      }
      catch( Exception except )
      {
        MessagePanel.Text = except.Message;
      }
    }

    private void CloseZipFile()
    {
      try
      {
        if( m_zipRoot.SfxPrefix != null )
        {
          m_zipRoot.ZipFile.Name = m_zipRoot.ZipFile.Name.Substring( 0, m_zipRoot.ZipFile.Name.Length - 4 ) + ".exe";
        }

        m_zipRoot = null;

        FillFileList();
        RefreshMenuStates();

        MessagePanel.Text = "Zip file closed";
      }
      catch( Exception except )
      {
        MessagePanel.Text = except.Message;
      }
    }

    private void ZipFiles()
    {
      if( ZipFilesDialog.ShowDialog() == DialogResult.OK )
      {
        ZipFiles(ZipFilesDialog.FileNames);
      }
    }

    private void ZipFiles( string[] Filenames )
    {
      DiskFile File ;
      ZippedFolder DestFolder;

      Cursor.Current = Cursors.WaitCursor;
      try
      {
        // We do not want the Zip file to be updated after each CopyTo,
        // but only after everything is copied. This is similar to the
        // ListView.BeginUpdate() method.
        m_zipRoot.BeginUpdate( m_zipEvents, null );
        m_zipRoot.DefaultCompressionLevel = GetCompressionLevelFromMenu();
        m_zipRoot.DefaultCompressionMethod = GetCompressionMethodFromMenu();

        if( OptionsEncryptFilesMenu.Checked )
        {
          using( PasswordPrompt passwordForm = new PasswordPrompt() )
          {
            string password = string.Empty;
            EncryptionMethod method = EncryptionMethod.Compatible;
            int strength = 256;

            DialogResult result = passwordForm.ShowDialog( 
              this, 
              ref password, 
              "Encrypting new files",
              "In order to encrypt all newly added files, you must provide an encryption password and confirm the encryption method.",
              "Encryption password:",
              ref method, 
              ref strength,
              false );

            switch( result )
            {
              case DialogResult.OK:
                m_zipRoot.DefaultEncryptionPassword = password;
                m_zipRoot.DefaultEncryptionMethod = method;
                m_zipRoot.DefaultEncryptionStrength = strength;
                break;

              case DialogResult.Ignore:
                m_zipRoot.DefaultEncryptionPassword = string.Empty;
                break;

              default:
                throw new ApplicationException( "Encryption parameters cancelled by user." );
            }
          }
        }
        else
        {
          m_zipRoot.DefaultEncryptionPassword = string.Empty;
        }

        m_zipRoot.AllowSpanning = true;

        try
        {
          foreach(   string Filename in Filenames )
          {
            File = new DiskFile(Filename);

            if( OptionsRememberPathMenu.Checked )
            {
              string RootName = System.IO.Path.GetPathRoot(Filename);
              string PathName = System.IO.Path.GetDirectoryName(Filename);

              PathName = PathName.Substring(RootName.Length, PathName.Length - RootName.Length);

              // If PathName and RootName are the same, then it is safe
              // to assume that the destination is the root of the zip file,
              // therefore, we will put "\" rather than string.empty to avoid
              // errors.
              if( PathName == string.Empty )
              {
                PathName = "\\";
              }

              DestFolder = ( ZippedFolder ) m_zipRoot.GetFolder(PathName);
            }
            else
            {
              DestFolder = m_zipRoot;
            }

            // CopyTo always creates the destination folder if it does not exist
            File.CopyTo( m_zipEvents, null, DestFolder, true );
          }
        }
        finally
        {
          // For every call to BeginUpdate, there must be a call to EndUpdate.
          m_zipRoot.EndUpdate( m_zipEvents, null );
        }

        MessagePanel.Text = "File(s) zipped successfully";
      }
      catch( Exception except )
      {
        MessagePanel.Text = except.Message;
      }
      Cursor.Current = Cursors.Default;

      FillFileList();
      RefreshMenuStates();
    }

    private void UnzipFiles()
    {
      if( UnzipFilesDialog.ShowDialog() == DialogResult.OK )
      {
        if( ZipContentsList.SelectedItems.Count == 0 )
        {
          UnzipFiles(System.IO.Path.GetDirectoryName(UnzipFilesDialog.FileName), ZipContentsList.Items);
        }
        else
        {
          UnzipFiles(System.IO.Path.GetDirectoryName(UnzipFilesDialog.FileName), ZipContentsList.SelectedItems);
        }
      }
    }

    private void UnzipFiles( string Destination, ICollection Items )
    {
      Cursor.Current = Cursors.WaitCursor;
      try
      {
        DiskFolder DestFolder = new DiskFolder(Destination);
        AbstractFolder SubDestFolder;

        // If we were unzipping elsewhere than in a DiskFolder, we could
        // want to check if it supports BeginUpdate/EndUpdate here.
        foreach( ZipContentsItem Item in Items )
        {
          if( Item.File.ParentFolder.IsRoot )
          {
            Item.File.CopyTo( m_zipEvents, null, DestFolder, true );
          }
          else
          {
            SubDestFolder = DestFolder.GetFolder( Item.File.ParentFolder.FullName.Substring( 1 ) );

            if( !SubDestFolder.Exists )
            {
              SubDestFolder.Create();
            }

            Item.File.CopyTo( m_zipEvents, null, SubDestFolder, true );
          }
        }

        MessagePanel.Text = "File(s) unzipped successfully";
      }
      catch( Exception except )
      {
        MessagePanel.Text = except.Message;
      }
      Cursor.Current = Cursors.Default;
    }

    private void DeleteFiles()
    {
      if( DialogResult.Yes == MessageBox.Show("Are you sure you wish to delete the selected file(s)?",
        "Please Confirm...", 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Question) )
      {
        DeleteFiles(ZipContentsList.SelectedItems);
      }
    }

    private void DeleteFiles( ICollection Items )
    {
      try
      {
        // We do not want the Zip file to be updated after each Delete,
        // but only after all selected files are deleted. This is similar 
        // to the ListView.BeginUpdate() method.
        m_zipRoot.BeginUpdate();

        try
        {
      
          foreach( ZipContentsItem Item in Items )
          {
            Item.File.Delete(m_zipEvents, null);
          }
        }
        finally
        {
          // For every call to BeginUpdate, there must be a call to EndUpdate.
          m_zipRoot.EndUpdate();
        }
      }
      catch( Exception except )
      {
        MessagePanel.Text = except.Message;
      }

      FillFileList();
      RefreshMenuStates();
    }

    private void CheckMethodMenu( CompressionMethod newCompressionMethod )
    {
      foreach( MenuItem menu in CompressionMethodMenu.MenuItems )
      {
        menu.Checked = false;
      }

      switch( newCompressionMethod )
      {
        case CompressionMethod.Stored:
          StoredMethodMenu.Checked = true;
          break;
        case CompressionMethod.Deflated:
          DeflatedMethodMenu.Checked = true;
          break;
        case CompressionMethod.Deflated64:
          Deflated64MethodMenu.Checked = true;
          break;
      }

      RegistryKey regKey = GetZipManagerRegistryKey();

      if( regKey != null )
      {
        regKey.SetValue( "CompressionMethod", ( int )newCompressionMethod );
        regKey.Close();
      }
    }

    private void CheckLevelMenu( CompressionLevel newCompressionLevel )
    {
      foreach( MenuItem menu in CompressionLevelMenu.MenuItems )
      {
        menu.Checked = false;
      }

      switch( newCompressionLevel )
      {
        case CompressionLevel.Highest:
          HighestLevelMenu.Checked = true;
          break;
        case CompressionLevel.Lowest:
          LowestLevelMenu.Checked = true;
          break;
        case CompressionLevel.None:
          NoneLevelMenu.Checked = true;
          break;
        case CompressionLevel.Normal:
          NormalLevelMenu.Checked = true;
          break;
      }

      RegistryKey regKey = GetZipManagerRegistryKey();

      if( regKey != null )
      {
        regKey.SetValue( "CompressionLevel", ( int )newCompressionLevel );
        regKey.Close();
      }
    }

    private RegistryKey GetZipManagerRegistryKey()
    {
      RegistryKey softwareKey; 
      RegistryKey xceedKey;
      RegistryKey zipManagerKey;

      zipManagerKey = null;

      softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);

      if( softwareKey == null )
      {
        softwareKey = Registry.CurrentUser.CreateSubKey("Software");
      }

      if( softwareKey != null )
      {
        xceedKey = softwareKey.OpenSubKey("Xceed", true);

        if( xceedKey == null )
        {
          xceedKey = softwareKey.CreateSubKey("Xceed");
        }

        if( xceedKey != null )
        {
          zipManagerKey = xceedKey.OpenSubKey("ZipManager", true);

          if( zipManagerKey == null )
          {
            //zipManagerKey = xceedKey.CreateSubKey("ZipManager");
          }
        }
      }

      return zipManagerKey;
    }

    private CompressionMethod GetCompressionMethodFromMenu()
    {
      CompressionMethod method = CompressionMethod.Stored;

      if( StoredMethodMenu.Checked )
      {
        method = CompressionMethod.Stored;
      }
      else if( DeflatedMethodMenu.Checked )
      {
        method = CompressionMethod.Deflated;
      }
      else if( Deflated64MethodMenu.Checked )
      {
        method = CompressionMethod.Deflated64;
      }
      
      return method;
    }


    private CompressionLevel GetCompressionLevelFromMenu()
    {
      CompressionLevel level = CompressionLevel.None;

      if( HighestLevelMenu.Checked )
      {
        level = CompressionLevel.Highest;
      }
      else if( NormalLevelMenu.Checked )
      {
        level = CompressionLevel.Normal;
      }
      else if( LowestLevelMenu.Checked )
      {
        level = CompressionLevel.Lowest;
      }
      else if( NoneLevelMenu.Checked  )
      {
        level = CompressionLevel.None;
      }

      return level;
    }

    private void AboutBox()
    {
      MessageBox.Show("Xceed Zip for .NET - ZipManager Sample Application" + Environment.NewLine +
        "Written in C#" + Environment.NewLine +
        "Copyrights (c) 2001-2006 Xceed Software Inc.",
        "About ZipManager...", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
  
    #endregion " Private Methods "

    #region " Private Members "

    // The root of the virtual disk space inside a Zip file.
    // Can also represent the Zip file itself.
    private ZipArchive m_zipRoot;

    // The event object we will pass to Zip methods to receive events.
    // Using "WithEvents" let's us use the combo boxes above to easily
    // implement event handlers without having to add this component to
    // the toolbox.
    private ZipEvents m_zipEvents = new ZipEvents();
    
    #endregion
  }
}
