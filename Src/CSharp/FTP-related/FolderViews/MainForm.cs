using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace FolderViews
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class MainForm : System.Windows.Forms.Form
  {
    private System.Windows.Forms.MainMenu mainFormMenu;
    private System.Windows.Forms.MenuItem menuItem2;
    private System.Windows.Forms.MenuItem menuFileOpen;
    private System.Windows.Forms.MenuItem menuWindow;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuHelp;
    private System.Windows.Forms.MenuItem menuHelpAbout;
    private System.Windows.Forms.MenuItem menuExit;
    private System.Windows.Forms.MenuItem menuTileVertical;
    private System.Windows.Forms.MenuItem menuTileHorizontal;
    private System.Windows.Forms.MenuItem menuEdit;
    private System.Windows.Forms.StatusBar statusBar1;
    private System.Windows.Forms.StatusBarPanel infoPanel;
    private System.Windows.Forms.StatusBarPanel progressPanel;
    private System.Windows.Forms.StatusBarPanel actionPanel;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public MainForm()
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
        if( components != null )
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
      this.mainFormMenu = new System.Windows.Forms.MainMenu();
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuFileOpen = new System.Windows.Forms.MenuItem();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.menuExit = new System.Windows.Forms.MenuItem();
      this.menuEdit = new System.Windows.Forms.MenuItem();
      this.menuWindow = new System.Windows.Forms.MenuItem();
      this.menuTileVertical = new System.Windows.Forms.MenuItem();
      this.menuTileHorizontal = new System.Windows.Forms.MenuItem();
      this.menuHelp = new System.Windows.Forms.MenuItem();
      this.menuHelpAbout = new System.Windows.Forms.MenuItem();
      this.statusBar1 = new System.Windows.Forms.StatusBar();
      this.infoPanel = new System.Windows.Forms.StatusBarPanel();
      this.actionPanel = new System.Windows.Forms.StatusBarPanel();
      this.progressPanel = new System.Windows.Forms.StatusBarPanel();
      ( ( System.ComponentModel.ISupportInitialize )( this.infoPanel ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.actionPanel ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.progressPanel ) ).BeginInit();
      this.SuspendLayout();
      // 
      // mainFormMenu
      // 
      this.mainFormMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
                                                                                 this.menuFile,
                                                                                 this.menuEdit,
                                                                                 this.menuWindow,
                                                                                 this.menuHelp} );
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
                                                                             this.menuFileOpen,
                                                                             this.menuItem2,
                                                                             this.menuExit} );
      this.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuFile.Text = "&File";
      // 
      // menuFileOpen
      // 
      this.menuFileOpen.Index = 0;
      this.menuFileOpen.MergeOrder = 2;
      this.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
      this.menuFileOpen.Text = "&Open folder view...";
      this.menuFileOpen.Click += new System.EventHandler( this.menuFileOpen_Click );
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 1;
      this.menuItem2.MergeOrder = 4;
      this.menuItem2.Text = "-";
      // 
      // menuExit
      // 
      this.menuExit.Index = 2;
      this.menuExit.MergeOrder = 6;
      this.menuExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
      this.menuExit.Text = "&Exit";
      this.menuExit.Click += new System.EventHandler( this.menuExit_Click );
      // 
      // menuEdit
      // 
      this.menuEdit.Index = 1;
      this.menuEdit.MergeOrder = 10;
      this.menuEdit.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
      this.menuEdit.Text = "&Edit";
      // 
      // menuWindow
      // 
      this.menuWindow.Index = 2;
      this.menuWindow.MdiList = true;
      this.menuWindow.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
                                                                               this.menuTileVertical,
                                                                               this.menuTileHorizontal} );
      this.menuWindow.MergeOrder = 20;
      this.menuWindow.Text = "&Window";
      // 
      // menuTileVertical
      // 
      this.menuTileVertical.Index = 0;
      this.menuTileVertical.MergeOrder = 22;
      this.menuTileVertical.Text = "Tile &Vertically";
      this.menuTileVertical.Click += new System.EventHandler( this.menuTileVertical_Click );
      // 
      // menuTileHorizontal
      // 
      this.menuTileHorizontal.Index = 1;
      this.menuTileHorizontal.MergeOrder = 24;
      this.menuTileHorizontal.Text = "Tile &Horizontally";
      this.menuTileHorizontal.Click += new System.EventHandler( this.menuTileHorizontal_Click );
      // 
      // menuHelp
      // 
      this.menuHelp.Index = 3;
      this.menuHelp.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
                                                                             this.menuHelpAbout} );
      this.menuHelp.MergeOrder = 30;
      this.menuHelp.Text = "&Help";
      // 
      // menuHelpAbout
      // 
      this.menuHelpAbout.Index = 0;
      this.menuHelpAbout.MergeOrder = 32;
      this.menuHelpAbout.Text = "&About...";
      this.menuHelpAbout.Click += new System.EventHandler( this.menuHelpAbout_Click );
      // 
      // statusBar1
      // 
      this.statusBar1.Location = new System.Drawing.Point( 0, 472 );
      this.statusBar1.Name = "statusBar1";
      this.statusBar1.Panels.AddRange( new System.Windows.Forms.StatusBarPanel[] {
                                                                                  this.infoPanel,
                                                                                  this.actionPanel,
                                                                                  this.progressPanel} );
      this.statusBar1.ShowPanels = true;
      this.statusBar1.Size = new System.Drawing.Size( 752, 22 );
      this.statusBar1.TabIndex = 1;
      // 
      // infoPanel
      // 
      this.infoPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
      this.infoPanel.Width = 486;
      // 
      // actionPanel
      // 
      this.actionPanel.Width = 200;
      // 
      // progressPanel
      // 
      this.progressPanel.MinWidth = 50;
      this.progressPanel.Width = 50;
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
      this.ClientSize = new System.Drawing.Size( 752, 494 );
      this.Controls.Add( this.statusBar1 );
      this.IsMdiContainer = true;
      this.Menu = this.mainFormMenu;
      this.Name = "MainForm";
      this.Text = "Folder Views - Xceed FTP for .NET";
      this.Load += new System.EventHandler( this.MainForm_Load );
      ( ( System.ComponentModel.ISupportInitialize )( this.infoPanel ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.actionPanel ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.progressPanel ) ).EndInit();
      this.ResumeLayout( false );

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
       * For instance, if you wanted to deploy this sample, the license key needs to be set in the Main() method.
       * If your trial period has expired, you must purchase a registered license key,
       * uncomment the next line of code, and insert your registerd license key.
       * For more information, consult the "How the 45-day trial works" and the 
       * "How to license the component once you purchase" topics in the documentation of this product.
       */

      // Xceed.Ftp.Licenser.LicenseKey = "FTNXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 

      Application.Run( new MainForm() );
    }

    public void DisplayInformation( string info )
    {
      infoPanel.Text = info;
    }

    public void DisplayAction( string action )
    {
      if( action.Length == 0 )
      {
        progressPanel.Text = string.Empty;
      }
      else
      {
        infoPanel.Text = string.Empty;
      }

      actionPanel.Text = action;
    }

    public void DisplayProgress( int percent )
    {
      progressPanel.Text = percent.ToString() + "%";
    }

    private void MainForm_Load( object sender, System.EventArgs e )
    {
      this.menuFileOpen_Click( this, EventArgs.Empty );
    }

    private void menuFileOpen_Click( object sender, System.EventArgs e )
    {
      using( OpenFolderForm openFolderForm = new OpenFolderForm() )
      {
        FolderForm folderForm = openFolderForm.ShowDialog( this );

        if( folderForm != null )
        {
          folderForm.MdiParent = this;
          folderForm.Show();
        }
      }
    }

    private void menuExit_Click( object sender, System.EventArgs e )
    {
      this.Close();
    }

    private void menuTileVertical_Click( object sender, System.EventArgs e )
    {
      this.LayoutMdi( MdiLayout.TileVertical );
    }

    private void menuTileHorizontal_Click( object sender, System.EventArgs e )
    {
      this.LayoutMdi( MdiLayout.TileHorizontal );
    }

    private void menuHelpAbout_Click( object sender, System.EventArgs e )
    {
      using( AboutForm about = new AboutForm() )
      {
        about.ShowDialog( this );
      }
    }
  }
}
