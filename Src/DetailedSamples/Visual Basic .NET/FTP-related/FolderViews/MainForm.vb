Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data

Namespace FolderViews
  Public Class MainForm
    Inherits System.Windows.Forms.Form

    Private mainFormMenu As System.Windows.Forms.MainMenu
    Private menuItem2 As System.Windows.Forms.MenuItem
    Private menuFileOpen As System.Windows.Forms.MenuItem
    Private menuWindow As System.Windows.Forms.MenuItem
    Private menuFile As System.Windows.Forms.MenuItem
    Private menuHelp As System.Windows.Forms.MenuItem
    Private menuHelpAbout As System.Windows.Forms.MenuItem
    Private menuExit As System.Windows.Forms.MenuItem
    Private menuTileVertical As System.Windows.Forms.MenuItem
    Private menuTileHorizontal As System.Windows.Forms.MenuItem
    Private menuEdit As System.Windows.Forms.MenuItem
    Private statusBar1 As System.Windows.Forms.StatusBar
    Private infoPanel As System.Windows.Forms.StatusBarPanel
    Private progressPanel As System.Windows.Forms.StatusBarPanel
    Private actionPanel As System.Windows.Forms.StatusBarPanel
    Private components As System.ComponentModel.Container = Nothing

    Public Sub New()
      InitializeComponent()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		  If (Disposing) Then
			  If Not components Is Nothing Then

          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"
    Private Sub InitializeComponent()
      Me.mainFormMenu = New System.Windows.Forms.MainMenu
      Me.menuFile = New System.Windows.Forms.MenuItem
      Me.menuFileOpen = New System.Windows.Forms.MenuItem
      Me.menuItem2 = New System.Windows.Forms.MenuItem
      Me.menuExit = New System.Windows.Forms.MenuItem
      Me.menuEdit = New System.Windows.Forms.MenuItem
      Me.menuWindow = New System.Windows.Forms.MenuItem
      Me.menuTileVertical = New System.Windows.Forms.MenuItem
      Me.menuTileHorizontal = New System.Windows.Forms.MenuItem
      Me.menuHelp = New System.Windows.Forms.MenuItem
      Me.menuHelpAbout = New System.Windows.Forms.MenuItem
      Me.statusBar1 = New System.Windows.Forms.StatusBar
      Me.infoPanel = New System.Windows.Forms.StatusBarPanel
      Me.actionPanel = New System.Windows.Forms.StatusBarPanel
      Me.progressPanel = New System.Windows.Forms.StatusBarPanel
      CType(Me.infoPanel, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.actionPanel, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.progressPanel, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      ' 
      ' mainFormMenu
      ' 
      Me.mainFormMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuEdit, Me.menuWindow, Me.menuHelp})
      ' 
      ' menuFile
      ' 
      Me.menuFile.Index = 0
      Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFileOpen, Me.menuItem2, Me.menuExit})
      Me.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems
      Me.menuFile.Text = "&File"
      ' 
      ' menuFileOpen
      ' 
      Me.menuFileOpen.Index = 0
      Me.menuFileOpen.MergeOrder = 2
      Me.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
      Me.menuFileOpen.Text = "&Open folder view..."
      AddHandler Me.menuFileOpen.Click, AddressOf Me.menuFileOpen_Click
      ' 
      ' menuItem2
      ' 
      Me.menuItem2.Index = 1
      Me.menuItem2.MergeOrder = 4
      Me.menuItem2.Text = "-"
      ' 
      ' menuExit
      ' 
      Me.menuExit.Index = 2
      Me.menuExit.MergeOrder = 6
      Me.menuExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
      Me.menuExit.Text = "&Exit"
      AddHandler Me.menuExit.Click, AddressOf Me.menuExit_Click
      ' 
      ' menuEdit
      ' 
      Me.menuEdit.Index = 1
      Me.menuEdit.MergeOrder = 10
      Me.menuEdit.MergeType = System.Windows.Forms.MenuMerge.MergeItems
      Me.menuEdit.Text = "&Edit"
      ' 
      ' menuWindow
      ' 
      Me.menuWindow.Index = 2
      Me.menuWindow.MdiList = True
      Me.menuWindow.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuTileVertical, Me.menuTileHorizontal})
      Me.menuWindow.MergeOrder = 20
      Me.menuWindow.Text = "&Window"
      ' 
      ' menuTileVertical
      ' 
      Me.menuTileVertical.Index = 0
      Me.menuTileVertical.MergeOrder = 22
      Me.menuTileVertical.Text = "Tile &Vertically"
      AddHandler Me.menuTileVertical.Click, AddressOf Me.menuTileVertical_Click
      ' 
      ' menuTileHorizontal
      ' 
      Me.menuTileHorizontal.Index = 1
      Me.menuTileHorizontal.MergeOrder = 24
      Me.menuTileHorizontal.Text = "Tile &Horizontally"
      AddHandler Me.menuTileHorizontal.Click, AddressOf Me.menuTileHorizontal_Click
      ' 
      ' menuHelp
      ' 
      Me.menuHelp.Index = 3
      Me.menuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuHelpAbout})
      Me.menuHelp.MergeOrder = 30
      Me.menuHelp.Text = "&Help"
      ' 
      ' menuHelpAbout 
      Me.menuHelpAbout.Index = 0
      Me.menuHelpAbout.MergeOrder = 32
      Me.menuHelpAbout.Text = "&About..."
      AddHandler Me.menuHelpAbout.Click, AddressOf Me.menuHelpAbout_Click
      ' 
      ' statusBar1
      ' 
      Me.statusBar1.Location = New System.Drawing.Point(0, 472)
      Me.statusBar1.Name = "statusBar1"
      Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.infoPanel, Me.actionPanel, Me.progressPanel})
      Me.statusBar1.ShowPanels = True
      Me.statusBar1.Size = New System.Drawing.Size(752, 22)
      Me.statusBar1.TabIndex = 1
      ' 
      ' infoPanel
      ' 
      Me.infoPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
      Me.infoPanel.Width = 486
      ' 
      ' actionPanel
      ' 
      Me.actionPanel.Width = 200
      ' 
      ' progressPanel
      ' 
      Me.progressPanel.MinWidth = 50
      Me.progressPanel.Width = 50
      ' 
      ' MainForm
      ' 
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(752, 494)
      Me.Controls.Add(Me.statusBar1)
      Me.IsMdiContainer = True
      Me.Menu = Me.mainFormMenu
      Me.Name = "MainForm"
      Me.Text = "Folder Views - Xceed FTP for .NET"
      AddHandler Me.Load, AddressOf Me.MainForm_Load
      CType(Me.infoPanel, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.actionPanel, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.progressPanel, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub
#End Region

    Shared Sub Main()
            ' ================================
            ' How to license Xceed components 
            ' ================================       
            ' To license your product, set the LicenseKey property to a valid trial or registered license key 
            ' in the main entry point of the application to ensure components are licensed before any of the 
            ' component methods are called.      
            ' 
            ' If the component is used in a DLL project (no entry point available), it is 
            ' recommended that the LicenseKey property be set in a static constructor of a 
            ' class that will be accessed systematically before any component is instantiated or, 
            ' you can simply set the LicenseKey property immediately BEFORE you instantiate 
            ' an instance of the component.
            ' 
            ' For instance, if you wanted to deploy this sample, the license key needs to be set here.
            ' If your trial period has expired, you must purchase a registered license key,
            ' uncomment the next line of code, and insert your registerd license key.
            ' For more information, consult the "How the 45-day trial works" and the 
            ' "How to license the component once you purchase" topics in the documentation of this product.

            ' Xceed.Ftp.Licenser.LicenseKey = "FTNXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 
            Application.Run(New MainForm)
    End Sub

    Public Sub DisplayInformation(ByVal info As String)
      infoPanel.Text = info
    End Sub 'DisplayInformation

    Public Sub DisplayAction(ByVal action As String)
      If (action.Length = 0) Then
        progressPanel.Text = String.Empty
      Else
        infoPanel.Text = String.Empty
      End If

      actionPanel.Text = action
    End Sub

    Public Sub DisplayProgress(ByVal percent As Integer)
      progressPanel.Text = percent.ToString() + "%"
    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs)
      Me.menuFileOpen_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub menuFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Dim openFolderForm As New FolderViews.OpenFolderForm
      Dim folderForm As FolderViews.FolderForm = openFolderForm.ShowDialog(Me)

      If Not folderForm Is Nothing Then
        folderForm.MdiParent = Me
        folderForm.Show()
      End If

      openFolderForm.Dispose()
    End Sub

    Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Me.Close()
    End Sub

    Private Sub menuTileVertical_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub menuTileHorizontal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub menuHelpAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Dim about As New AboutForm
      about.ShowDialog(Me)
      about.Dispose()
    End Sub
  End Class
End Namespace
