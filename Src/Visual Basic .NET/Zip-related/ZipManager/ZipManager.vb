'Xceed Zip for .NET - ZipManager Sample Application
'Copyright (c) 2000-2002 - Xceed Software Inc.
'
'[ZipManager.vb]
'
'This application demonstrates how to use Xceed Zip for .NET.
'
'This file is part of Xceed Zip for .NET. The source code in this file is 
'only intended as a supplement to the documentation, and is provided 
'"as is", without warranty of any kind, either expressed or implied.

Imports Xceed.FileSystem
Imports Xceed.Zip
Imports Xceed.Compression
Imports Microsoft.Win32

Namespace Xceed.Zip.Samples
  Public Class ZipManager
    Inherits System.Windows.Forms.Form

    Public Sub New()
      MyBase.New()

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

      ' Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 


      'This call is required by the Windows Form Designer.
      InitializeComponent()

      ' Update menus and buttons
      RefreshMenuStates()
    End Sub

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not (components Is Nothing) Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents FilenameColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents LastWriteColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents SizeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents CompSizeColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents RatioColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents AttributesColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents NewZipButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents OpenZipButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents ZipManagerMenu As System.Windows.Forms.MainMenu
    Friend WithEvents FileMenu As System.Windows.Forms.MenuItem
    Friend WithEvents NewZipMenu As System.Windows.Forms.MenuItem
    Friend WithEvents OpenZipMenu As System.Windows.Forms.MenuItem
    Friend WithEvents CloseZipMenu As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents ExitMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditMenu As System.Windows.Forms.MenuItem
    Friend WithEvents ZipFilesMenu As System.Windows.Forms.MenuItem
    Friend WithEvents UnzipFilesMenu As System.Windows.Forms.MenuItem
    Friend WithEvents DeleteFilesMenu As System.Windows.Forms.MenuItem
    Friend WithEvents OptionMenu As System.Windows.Forms.MenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.MenuItem
    Friend WithEvents AboutMenu As System.Windows.Forms.MenuItem
    Friend WithEvents OpenZipDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ZipFilesDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ZipManagerToolBar As System.Windows.Forms.ToolBar
    Friend WithEvents ZipContentsList As System.Windows.Forms.ListView
    Friend WithEvents ZipFilesButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents UnzipFilesButtom As System.Windows.Forms.ToolBarButton
    Friend WithEvents DeleteFilesButton As System.Windows.Forms.ToolBarButton
    Friend WithEvents StatusBarPanel As System.Windows.Forms.Panel
    Friend WithEvents TotalBytesProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents ZipManagerStatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents MessagePanel As System.Windows.Forms.StatusBarPanel
    Friend WithEvents UnzipFilesDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OptionsRememberPathMenu As System.Windows.Forms.MenuItem
    Friend WithEvents NewZipFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CompressionLevelMenu As System.Windows.Forms.MenuItem
    Friend WithEvents HighestLevelMenu As System.Windows.Forms.MenuItem
    Friend WithEvents NormalLevelMenu As System.Windows.Forms.MenuItem
    Friend WithEvents LowestLevelMenu As System.Windows.Forms.MenuItem
    Friend WithEvents NoneLevelMenu As System.Windows.Forms.MenuItem

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents SfxOptionsMenu As System.Windows.Forms.MenuItem
    Friend WithEvents CompressionMethodMenu As System.Windows.Forms.MenuItem
    Friend WithEvents StoredMethodMenu As System.Windows.Forms.MenuItem
    Friend WithEvents DeflatedMethodMenu As System.Windows.Forms.MenuItem
    Friend WithEvents Deflated64MethodMenu As System.Windows.Forms.MenuItem
    Friend WithEvents OptionsEncryptFilesMenu As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.ZipManagerToolBar = New System.Windows.Forms.ToolBar
      Me.NewZipButton = New System.Windows.Forms.ToolBarButton
      Me.OpenZipButton = New System.Windows.Forms.ToolBarButton
      Me.ZipFilesButton = New System.Windows.Forms.ToolBarButton
      Me.UnzipFilesButtom = New System.Windows.Forms.ToolBarButton
      Me.DeleteFilesButton = New System.Windows.Forms.ToolBarButton
      Me.ZipContentsList = New System.Windows.Forms.ListView
      Me.FilenameColumn = New System.Windows.Forms.ColumnHeader
      Me.LastWriteColumn = New System.Windows.Forms.ColumnHeader
      Me.SizeColumn = New System.Windows.Forms.ColumnHeader
      Me.CompSizeColumn = New System.Windows.Forms.ColumnHeader
      Me.RatioColumn = New System.Windows.Forms.ColumnHeader
      Me.AttributesColumn = New System.Windows.Forms.ColumnHeader
      Me.ZipManagerMenu = New System.Windows.Forms.MainMenu
      Me.FileMenu = New System.Windows.Forms.MenuItem
      Me.NewZipMenu = New System.Windows.Forms.MenuItem
      Me.OpenZipMenu = New System.Windows.Forms.MenuItem
      Me.CloseZipMenu = New System.Windows.Forms.MenuItem
      Me.MenuItem4 = New System.Windows.Forms.MenuItem
      Me.ExitMenu = New System.Windows.Forms.MenuItem
      Me.EditMenu = New System.Windows.Forms.MenuItem
      Me.ZipFilesMenu = New System.Windows.Forms.MenuItem
      Me.UnzipFilesMenu = New System.Windows.Forms.MenuItem
      Me.DeleteFilesMenu = New System.Windows.Forms.MenuItem
      Me.OptionMenu = New System.Windows.Forms.MenuItem
      Me.OptionsRememberPathMenu = New System.Windows.Forms.MenuItem
      Me.CompressionMethodMenu = New System.Windows.Forms.MenuItem
      Me.StoredMethodMenu = New System.Windows.Forms.MenuItem
      Me.DeflatedMethodMenu = New System.Windows.Forms.MenuItem
      Me.Deflated64MethodMenu = New System.Windows.Forms.MenuItem
      Me.CompressionLevelMenu = New System.Windows.Forms.MenuItem
      Me.HighestLevelMenu = New System.Windows.Forms.MenuItem
      Me.NormalLevelMenu = New System.Windows.Forms.MenuItem
      Me.LowestLevelMenu = New System.Windows.Forms.MenuItem
      Me.NoneLevelMenu = New System.Windows.Forms.MenuItem
      Me.SfxOptionsMenu = New System.Windows.Forms.MenuItem
      Me.HelpMenu = New System.Windows.Forms.MenuItem
      Me.AboutMenu = New System.Windows.Forms.MenuItem
      Me.OpenZipDialog = New System.Windows.Forms.OpenFileDialog
      Me.ZipFilesDialog = New System.Windows.Forms.OpenFileDialog
      Me.StatusBarPanel = New System.Windows.Forms.Panel
      Me.ZipManagerStatusBar = New System.Windows.Forms.StatusBar
      Me.MessagePanel = New System.Windows.Forms.StatusBarPanel
      Me.TotalBytesProgressBar = New System.Windows.Forms.ProgressBar
      Me.UnzipFilesDialog = New System.Windows.Forms.SaveFileDialog
      Me.NewZipFileDialog = New System.Windows.Forms.SaveFileDialog
      Me.OptionsEncryptFilesMenu = New System.Windows.Forms.MenuItem
      Me.StatusBarPanel.SuspendLayout()
      CType(Me.MessagePanel, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'ZipManagerToolBar
      '
      Me.ZipManagerToolBar.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.NewZipButton, Me.OpenZipButton, Me.ZipFilesButton, Me.UnzipFilesButtom, Me.DeleteFilesButton})
      Me.ZipManagerToolBar.DropDownArrows = True
      Me.ZipManagerToolBar.Location = New System.Drawing.Point(0, 0)
      Me.ZipManagerToolBar.Name = "ZipManagerToolBar"
      Me.ZipManagerToolBar.ShowToolTips = True
      Me.ZipManagerToolBar.Size = New System.Drawing.Size(544, 42)
      Me.ZipManagerToolBar.TabIndex = 0
      '
      'NewZipButton
      '
      Me.NewZipButton.Text = "New"
      '
      'OpenZipButton
      '
      Me.OpenZipButton.Text = "Open"
      '
      'ZipFilesButton
      '
      Me.ZipFilesButton.Text = "Zip"
      '
      'UnzipFilesButtom
      '
      Me.UnzipFilesButtom.Text = "Unzip"
      '
      'DeleteFilesButton
      '
      Me.DeleteFilesButton.Text = "Delete"
      '
      'ZipContentsList
      '
      Me.ZipContentsList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FilenameColumn, Me.LastWriteColumn, Me.SizeColumn, Me.CompSizeColumn, Me.RatioColumn, Me.AttributesColumn})
      Me.ZipContentsList.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ZipContentsList.Location = New System.Drawing.Point(0, 42)
      Me.ZipContentsList.Name = "ZipContentsList"
      Me.ZipContentsList.Size = New System.Drawing.Size(544, 306)
      Me.ZipContentsList.TabIndex = 1
      Me.ZipContentsList.View = System.Windows.Forms.View.Details
      '
      'FilenameColumn
      '
      Me.FilenameColumn.Text = "Filename"
      Me.FilenameColumn.Width = 212
      '
      'LastWriteColumn
      '
      Me.LastWriteColumn.Text = "Modified"
      '
      'SizeColumn
      '
      Me.SizeColumn.Text = "Size"
      '
      'CompSizeColumn
      '
      Me.CompSizeColumn.Text = "Packed"
      '
      'RatioColumn
      '
      Me.RatioColumn.Text = "Ratio"
      '
      'AttributesColumn
      '
      Me.AttributesColumn.Text = "Attributes"
      '
      'ZipManagerMenu
      '
      Me.ZipManagerMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileMenu, Me.EditMenu, Me.OptionMenu, Me.HelpMenu})
      '
      'FileMenu
      '
      Me.FileMenu.Index = 0
      Me.FileMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.NewZipMenu, Me.OpenZipMenu, Me.CloseZipMenu, Me.MenuItem4, Me.ExitMenu})
      Me.FileMenu.Text = "&File"
      '
      'NewZipMenu
      '
      Me.NewZipMenu.Index = 0
      Me.NewZipMenu.Text = "&New Zip file..."
      '
      'OpenZipMenu
      '
      Me.OpenZipMenu.Index = 1
      Me.OpenZipMenu.Text = "&Open Zip File..."
      '
      'CloseZipMenu
      '
      Me.CloseZipMenu.Index = 2
      Me.CloseZipMenu.Text = "&Close Zip File"
      '
      'MenuItem4
      '
      Me.MenuItem4.Index = 3
      Me.MenuItem4.Text = "-"
      '
      'ExitMenu
      '
      Me.ExitMenu.Index = 4
      Me.ExitMenu.Text = "&Exit from ZipManager"
      '
      'EditMenu
      '
      Me.EditMenu.Index = 1
      Me.EditMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ZipFilesMenu, Me.UnzipFilesMenu, Me.DeleteFilesMenu})
      Me.EditMenu.Text = "&Edit"
      '
      'ZipFilesMenu
      '
      Me.ZipFilesMenu.Index = 0
      Me.ZipFilesMenu.Shortcut = System.Windows.Forms.Shortcut.Ins
      Me.ZipFilesMenu.Text = "&Zip files..."
      '
      'UnzipFilesMenu
      '
      Me.UnzipFilesMenu.Index = 1
      Me.UnzipFilesMenu.Text = "&Unzip files..."
      '
      'DeleteFilesMenu
      '
      Me.DeleteFilesMenu.Index = 2
      Me.DeleteFilesMenu.Shortcut = System.Windows.Forms.Shortcut.Del
      Me.DeleteFilesMenu.Text = "&Delete files..."
      '
      'OptionMenu
      '
      Me.OptionMenu.Index = 2
      Me.OptionMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.OptionsRememberPathMenu, Me.CompressionMethodMenu, Me.CompressionLevelMenu, Me.OptionsEncryptFilesMenu, Me.SfxOptionsMenu})
      Me.OptionMenu.Text = "&Options"
      '
      'OptionsRememberPathMenu
      '
      Me.OptionsRememberPathMenu.Index = 0
      Me.OptionsRememberPathMenu.Text = "&Remember path when zipping"
      '
      'CompressionMethodMenu
      '
      Me.CompressionMethodMenu.Index = 1
      Me.CompressionMethodMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.StoredMethodMenu, Me.DeflatedMethodMenu, Me.Deflated64MethodMenu})
      Me.CompressionMethodMenu.Text = "Compression &method"
      '
      'StoredMethodMenu
      '
      Me.StoredMethodMenu.Index = 0
      Me.StoredMethodMenu.Text = "&Stored"
      '
      'DeflatedMethodMenu
      '
      Me.DeflatedMethodMenu.Index = 1
      Me.DeflatedMethodMenu.Text = "&Deflated"
      '
      'Deflated64MethodMenu
      '
      Me.Deflated64MethodMenu.Index = 2
      Me.Deflated64MethodMenu.Text = "Deflated&64"
      '
      'CompressionLevelMenu
      '
      Me.CompressionLevelMenu.Index = 2
      Me.CompressionLevelMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.HighestLevelMenu, Me.NormalLevelMenu, Me.LowestLevelMenu, Me.NoneLevelMenu})
      Me.CompressionLevelMenu.Text = "&Compression level"
      '
      'HighestLevelMenu
      '
      Me.HighestLevelMenu.Index = 0
      Me.HighestLevelMenu.Text = "&Highest"
      '
      'NormalLevelMenu
      '
      Me.NormalLevelMenu.Index = 1
      Me.NormalLevelMenu.Text = "No&rmal"
      '
      'LowestLevelMenu
      '
      Me.LowestLevelMenu.Index = 2
      Me.LowestLevelMenu.Text = "L&owest"
      '
      'NoneLevelMenu
      '
      Me.NoneLevelMenu.Index = 3
      Me.NoneLevelMenu.Text = "&None"
      '
      'SfxOptionsMenu
      '
      Me.SfxOptionsMenu.Enabled = False
      Me.SfxOptionsMenu.Index = 4
      Me.SfxOptionsMenu.Text = "&Sfx options "
      '
      'HelpMenu
      '
      Me.HelpMenu.Index = 3
      Me.HelpMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.AboutMenu})
      Me.HelpMenu.Text = "&Help"
      '
      'AboutMenu
      '
      Me.AboutMenu.Index = 0
      Me.AboutMenu.Text = "&About ZipManager"
      '
      'OpenZipDialog
      '
      Me.OpenZipDialog.DefaultExt = "zip"
      Me.OpenZipDialog.Filter = "Zip Files (*.zip)|*.zip|All Files (*.*)|*.*"
      Me.OpenZipDialog.Title = "Select Zip file to open..."
      '
      'ZipFilesDialog
      '
      Me.ZipFilesDialog.AddExtension = False
      Me.ZipFilesDialog.Filter = "All Files (*.*)|*.*"
      Me.ZipFilesDialog.Multiselect = True
      Me.ZipFilesDialog.Title = "Select files to zip..."
      '
      'StatusBarPanel
      '
      Me.StatusBarPanel.Controls.Add(Me.ZipManagerStatusBar)
      Me.StatusBarPanel.Controls.Add(Me.TotalBytesProgressBar)
      Me.StatusBarPanel.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.StatusBarPanel.Location = New System.Drawing.Point(0, 348)
      Me.StatusBarPanel.Name = "StatusBarPanel"
      Me.StatusBarPanel.Size = New System.Drawing.Size(544, 16)
      Me.StatusBarPanel.TabIndex = 2
      '
      'ZipManagerStatusBar
      '
      Me.ZipManagerStatusBar.Dock = System.Windows.Forms.DockStyle.Fill
      Me.ZipManagerStatusBar.Location = New System.Drawing.Point(152, 0)
      Me.ZipManagerStatusBar.Name = "ZipManagerStatusBar"
      Me.ZipManagerStatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.MessagePanel})
      Me.ZipManagerStatusBar.ShowPanels = True
      Me.ZipManagerStatusBar.Size = New System.Drawing.Size(392, 16)
      Me.ZipManagerStatusBar.TabIndex = 1
      '
      'MessagePanel
      '
      Me.MessagePanel.Width = 2000
      '
      'TotalBytesProgressBar
      '
      Me.TotalBytesProgressBar.Dock = System.Windows.Forms.DockStyle.Left
      Me.TotalBytesProgressBar.Location = New System.Drawing.Point(0, 0)
      Me.TotalBytesProgressBar.Name = "TotalBytesProgressBar"
      Me.TotalBytesProgressBar.Size = New System.Drawing.Size(152, 16)
      Me.TotalBytesProgressBar.TabIndex = 0
      '
      'UnzipFilesDialog
      '
      Me.UnzipFilesDialog.AddExtension = False
      Me.UnzipFilesDialog.FileName = "Go to destination folder and press Save"
      Me.UnzipFilesDialog.Filter = "All Files (*.*)|*.*"
      Me.UnzipFilesDialog.OverwritePrompt = False
      Me.UnzipFilesDialog.RestoreDirectory = True
      Me.UnzipFilesDialog.Title = "Select destination folder..."
      '
      'NewZipFileDialog
      '
      Me.NewZipFileDialog.DefaultExt = "zip"
      Me.NewZipFileDialog.Filter = "Zip Files (*.zip)|*.zip"
      Me.NewZipFileDialog.RestoreDirectory = True
      Me.NewZipFileDialog.Title = "Enter the new Zip file name and destination"
      '
      'OptionsEncryptFilesMenu
      '
      Me.OptionsEncryptFilesMenu.Index = 3
      Me.OptionsEncryptFilesMenu.Text = "&Encrypt files"
      '
      'ZipManager
      '
      Me.AllowDrop = True
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(544, 364)
      Me.Controls.Add(Me.ZipContentsList)
      Me.Controls.Add(Me.StatusBarPanel)
      Me.Controls.Add(Me.ZipManagerToolBar)
      Me.Menu = Me.ZipManagerMenu
      Me.Name = "ZipManager"
      Me.Text = "ZipManager - Xceed Zip for .NET"
      Me.StatusBarPanel.ResumeLayout(False)
      CType(Me.MessagePanel, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Menu Events "
    Private Sub NewZipMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewZipMenu.Click
      NewZipFile()
      SfxOptionsMenu.Enabled = True
    End Sub

    Private Sub OpenZipMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenZipMenu.Click
      OpenZipFile()
      SfxOptionsMenu.Enabled = False
    End Sub

    Private Sub CloseZipMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseZipMenu.Click
      CloseZipFile()
      SfxOptionsMenu.Enabled = False
    End Sub

    Private Sub ExitMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitMenu.Click
      Me.Close()
    End Sub

    Private Sub ZipFilesMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZipFilesMenu.Click
      ZipFiles()
    End Sub

    Private Sub UnzipFilesMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnzipFilesMenu.Click
      UnzipFiles()
    End Sub

    Private Sub DeleteFilesMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFilesMenu.Click
      DeleteFiles()
    End Sub

    Private Sub OptionsRememberPathMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsRememberPathMenu.Click
      OptionsRememberPathMenu.Checked = Not OptionsRememberPathMenu.Checked

      Dim regKey As RegistryKey = GetZipManagerRegistryKey()

      If Not regKey Is Nothing Then
        regKey.SetValue("RememberPath", OptionsRememberPathMenu.Checked)
        regKey.Close()
      End If
    End Sub

    Private Sub OptionsEncryptFilesMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptionsEncryptFilesMenu.Click
      OptionsEncryptFilesMenu.Checked = Not OptionsEncryptFilesMenu.Checked

      Dim regKey As RegistryKey = GetZipManagerRegistryKey()

      If Not regKey Is Nothing Then
        regKey.SetValue("EncryptFiles", OptionsEncryptFilesMenu.Checked)
        regKey.Close()
      End If
    End Sub


    Private Sub SfxMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SfxOptionsMenu.Click
      If (Not m_zipRoot Is Nothing) Then
        Dim sfxoptions As New sfxoptions(m_zipRoot.SfxPrefix)
        sfxoptions.ShowDialog()
        m_zipRoot.SfxPrefix = sfxoptions.SfxPrefix

        If (Not m_zipRoot.SfxPrefix Is Nothing) Then
          m_zipRoot.ZipFile.Name = m_zipRoot.ZipFile.Name.Substring(0, m_zipRoot.ZipFile.Name.Length - 4) + ".exe"
        End If

      Else
        MessageBox.Show("No zip file open!")
      End If
    End Sub

    Private Sub StoredMethodMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StoredMethodMenu.Click
      Call CheckMethodMenu(CompressionMethod.Stored)
    End Sub

    Private Sub DeflatedMethodMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeflatedMethodMenu.Click
      Call CheckMethodMenu(CompressionMethod.Deflated)
    End Sub

    Private Sub Deflated64MethodMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Deflated64MethodMenu.Click
      Call CheckMethodMenu(CompressionMethod.Deflated64)
    End Sub

    Private Sub HighestLevelMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HighestLevelMenu.Click
      Call CheckLevelMenu(CompressionLevel.Highest)
    End Sub

    Private Sub NormalLevelMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalLevelMenu.Click
      Call CheckLevelMenu(CompressionLevel.Normal)
    End Sub

    Private Sub LowestLevelMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowestLevelMenu.Click
      Call CheckLevelMenu(CompressionLevel.Lowest)
    End Sub

    Private Sub NoneLevelMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoneLevelMenu.Click
      Call CheckLevelMenu(CompressionLevel.None)
    End Sub

    Private Sub AboutMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutMenu.Click
      AboutBox()
    End Sub
#End Region

#Region " Form Events "
    Private Sub ZipManager_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
      Dim droppedFiles As String()
      Dim file As DiskFile
      Dim zipFile As ZipArchive
      Dim open As Boolean
      Dim add As Boolean

      open = False
      add = False

      If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
        Try
          droppedFiles = e.Data.GetData(DataFormats.FileDrop, False)
          If droppedFiles.Length = 1 Then
            ' The user dropped only one file. We check if it's a zip file
            file = New DiskFile(droppedFiles(0))
            Try
              zipFile = New ZipArchive(file)
              open = True
            Catch Except As InvalidZipStructureException
              ' It's not a zip file. We assume the user wants to add the
              ' file to the current archive
              add = True
            End Try
          Else
            ' The user dropped more than one file. We'll eventually add
            ' them to the current archive.
            file = Nothing
            add = True
          End If

          If open Then
            ' Open the dropped zip file
            OpenZipFile(file.FullName)
          Else
            If add Then
              ' Add the dropped file(s) to the current archive if there
              ' is one.
              If Not m_zipRoot Is Nothing Then
                Me.Activate()
                If MessageBox.Show(Me, "Add the file(s) to the archive?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                  ZipFiles(droppedFiles)
                End If
              Else
                MessagePanel.Text = "No opened zip file"
              End If
            End If
          End If
        Catch Except As Exception
          MessagePanel.Text = except.Message
        End Try
      End If
    End Sub

    Private Sub ZipManager_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragOver
      If e.Data.GetDataPresent(DataFormats.FileDrop, False) Then
        e.Effect = DragDropEffects.Copy
      End If
    End Sub

    Private Sub ZipManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
      Dim regKey As RegistryKey = GetZipManagerRegistryKey()

      If Not regKey Is Nothing Then
        OptionsRememberPathMenu.Checked = Boolean.Parse(CStr(regKey.GetValue("RememberPath", False)))
        CheckLevelMenu(regKey.GetValue("CompressionLevel", CompressionLevel.Normal))
        CheckMethodMenu(regKey.GetValue("CompressionMethod", CompressionMethod.Deflated))
        OptionsEncryptFilesMenu.Checked = Boolean.Parse(CStr(regKey.GetValue("EncryptFiles", "false")))

        regKey.Close()
      End If
    End Sub
#End Region

#Region " ToolBar Events "
    Private Sub ZipManagerToolBar_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ZipManagerToolBar.ButtonClick
      Select Case ZipManagerToolBar.Buttons.IndexOf(e.Button)
        Case ZipManagerToolBar.Buttons.IndexOf(NewZipButton)
          NewZipFile()
          SfxOptionsMenu.Enabled = True
        Case ZipManagerToolBar.Buttons.IndexOf(OpenZipButton)
          OpenZipFile()
          SfxOptionsMenu.Enabled = False
        Case ZipManagerToolBar.Buttons.IndexOf(ZipFilesButton)
          ZipFiles()
        Case ZipManagerToolBar.Buttons.IndexOf(UnzipFilesButtom)
          UnzipFiles()
        Case ZipManagerToolBar.Buttons.IndexOf(DeleteFilesButton)
          DeleteFiles()
      End Select
    End Sub
#End Region

#Region " FileSystem Events "
    Private Sub m_zipEvents_ByteProgression(ByVal sender As Object, ByVal e As Xceed.FileSystem.ByteProgressionEventArgs) Handles m_zipEvents.ByteProgression
      TotalBytesProgressBar.Value = e.AllFilesBytes.Percent Mod 100
    End Sub

    Private Sub m_zipEvents_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs) Handles m_zipEvents.ItemException
      If TypeOf e.Exception Is InvalidDecryptionPasswordException Then
        Dim file As ZippedFile = IIf(TypeOf e.CurrentItem Is ZippedFile, e.CurrentItem, Nothing)

        If Not file Is Nothing Then
          Dim passwordForm As PasswordPrompt = New PasswordPrompt

          Try
            Dim password As String = m_zipRoot.DefaultDecryptionPassword

            Dim method As EncryptionMethod = file.EncryptionMethod
            Dim strength As Integer = file.EncryptionStrength

            Dim result As DialogResult = passwordForm.ShowDialog(Me, password, "Encrypted file", String.Format("File '{0}' is encrypted. You must provide a password in order to unzip it.", file.FullName), "Decryption Password:", method, strength, True)

            If result = System.Windows.Forms.DialogResult.OK Then
              m_zipRoot.DefaultDecryptionPassword = password
              e.Action = ItemExceptionAction.Retry
            ElseIf result = System.Windows.Forms.DialogResult.Ignore Then
              e.Action = ItemExceptionAction.Ignore
            End If
          Finally
            passwordForm.Close()
          End Try
        End If
      ElseIf TypeOf e.Exception Is ItemIsReadOnlyException Then
        ' We could ask the user if he wants us to overwrite this read-only file.
        ' Or we can force out the read-only attribute and retry!
        e.TargetItem.Attributes = e.TargetItem.Attributes And Not System.IO.FileAttributes.ReadOnly
        e.Action = ItemExceptionAction.Retry
      End If
    End Sub
#End Region

#Region " Zip Events "
    Private Sub m_zipEvents_DiskRequired(ByVal sender As Object, ByVal e As Xceed.Zip.DiskRequiredEventArgs) Handles m_zipEvents.DiskRequired

      If (e.Action = DiskRequiredAction.Fail) Then
        Dim result As DialogResult
        result = MessageBox.Show("Insert another disk.", "Insert disk.", MessageBoxButtons.OKCancel)

        If (result = System.Windows.Forms.DialogResult.OK) Then
          e.Action = DiskRequiredAction.[Continue]
        End If

      End If

    End Sub
#End Region

#Region " Controls Events "
    Private Sub ZipContentsList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZipContentsList.SelectedIndexChanged
      RefreshMenuStates()
    End Sub
#End Region

#Region " Private Methods "
    Private Sub RefreshMenuStates()
      ' File menu children and related buttons
      CloseZipMenu.Enabled = Not (m_zipRoot Is Nothing)

      ' Edit menu children and related buttons
      ZipFilesMenu.Enabled = CloseZipMenu.Enabled
      ZipFilesButton.Enabled = CloseZipMenu.Enabled

      UnzipFilesMenu.Enabled = CloseZipMenu.Enabled
      UnzipFilesButtom.Enabled = CloseZipMenu.Enabled
      DeleteFilesMenu.Enabled = (ZipContentsList.SelectedItems.Count > 0)
      DeleteFilesButton.Enabled = (ZipContentsList.SelectedItems.Count > 0)
    End Sub

    Private Sub FillFileList()
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
      ZipContentsList.Items.Clear()

      If Not m_zipRoot Is Nothing Then
        Try
          Dim Files() As AbstractFile = m_zipRoot.GetFiles(m_zipEvents, Nothing, True)

          ' We want to freeze the list update while we add items.
          ZipContentsList.BeginUpdate()

          Try
            Dim File As AbstractFile
            For Each File In Files
              ZipContentsList.Items.Add(New ZipContentsItem(File))
            Next
          Finally
            ZipContentsList.EndUpdate()
          End Try
        Catch Except As Exception
          MessagePanel.Text = Except.Message
        End Try
      End If
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub NewZipFile()
      Dim ZipFile As DiskFile

      If NewZipFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        Try
          ZipFile = New DiskFile(NewZipFileDialog.FileName)

          If ZipFile.Exists Then
            ZipFile.Delete()
          End If

          ZipFile.Create()
          OpenZipFile(NewZipFileDialog.FileName)
        Catch Except As Exception
          MessagePanel.Text = Except.Message
        End Try
      End If
    End Sub

    Private Sub OpenZipFile()
      'Ask for file to open using OpenZipDialog
      If OpenZipDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        OpenZipFile(OpenZipDialog.FileName)
      End If
    End Sub

    Private Sub OpenZipFile(ByVal ZipFilename As String)
      Try
        Dim ZipFile As DiskFile = New DiskFile(ZipFilename)

        If ZipFile.Exists Then
          m_zipRoot = New ZipArchive(ZipFile)
          FillFileList()
          RefreshMenuStates()
        End If

        MessagePanel.Text = "Zip file opened"
      Catch Except As Exception
        MessagePanel.Text = Except.Message
      End Try
    End Sub

    Private Sub CloseZipFile()
      Try
        m_zipRoot = Nothing

        FillFileList()
        RefreshMenuStates()

        MessagePanel.Text = "Zip file closed"
      Catch Except As Exception
        MessagePanel.Text = Except.Message
      End Try
    End Sub

    Private Sub ZipFiles()
      If ZipFilesDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        ZipFiles(ZipFilesDialog.FileNames)
      End If
    End Sub

    Private Sub ZipFiles(ByVal Filenames() As String)
      Dim Filename As String
      Dim File As DiskFile
      Dim DestFolder As ZippedFolder

      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
      Try
        ' We do not want the Zip file to be updated after each CopyTo,
        ' but only after everything is copied. This is similar to the
        ' ListView.BeginUpdate() method.
        m_zipRoot.BeginUpdate(m_zipEvents, Nothing)
        m_zipRoot.DefaultCompressionLevel = GetCompressionLevelFromMenu()
        m_zipRoot.DefaultCompressionMethod = GetCompressionMethodFromMenu()

        If OptionsEncryptFilesMenu.Checked Then
          Dim passwordForm As PasswordPrompt = New PasswordPrompt

          Try
            Dim password As String = String.Empty
            Dim method As EncryptionMethod = EncryptionMethod.Compatible
            Dim strength As Integer = 256

            Dim result As DialogResult = passwordForm.ShowDialog(Me, password, "Encrypting new files", "In order to encrypt all newly added files, you must provide an encryption password and confirm the encryption method.", "Encryption password:", method, strength, False)

            Select Case result
              Case System.Windows.Forms.DialogResult.OK
                m_zipRoot.DefaultEncryptionPassword = password
                m_zipRoot.DefaultEncryptionMethod = method
                m_zipRoot.DefaultEncryptionStrength = strength

              Case System.Windows.Forms.DialogResult.Ignore
                m_zipRoot.DefaultEncryptionPassword = String.Empty

              Case Else
                Throw New ApplicationException("Encryption parameters cancelled by user.")
            End Select
          Finally
            passwordForm.Close()
          End Try
        Else
          m_zipRoot.DefaultEncryptionPassword = String.Empty
        End If

        m_zipRoot.AllowSpanning = True

        Try
          For Each Filename In Filenames
            File = New DiskFile(Filename)

            If OptionsRememberPathMenu.Checked Then
              Dim RootName As String = System.IO.Path.GetPathRoot(Filename)
              Dim PathName As String = System.IO.Path.GetDirectoryName(Filename)

              PathName = PathName.Substring(RootName.Length, PathName.Length - RootName.Length)

              ' If PathName and RootName are the same, then it is safe
              ' to assume that the destination is the root of the zip file,
              ' therefore, we will put "\" rather than string.empty to avoid
              ' errors.
              If PathName = String.Empty Then
                PathName = "\"
              End If

              DestFolder = m_zipRoot.GetFolder(PathName)
            Else
              DestFolder = m_zipRoot
            End If

            ' CopyTo always creates the destination folder if it does not exist
            File.CopyTo(m_zipEvents, Nothing, DestFolder, True)
          Next
        Finally
          ' For every call to BeginUpdate, there must be a call to EndUpdate.
          m_zipRoot.EndUpdate(m_zipEvents, Nothing)
        End Try

        MessagePanel.Text = "File(s) zipped successfully"
      Catch Except As Exception
        MessagePanel.Text = Except.Message
      End Try
      System.Windows.Forms.Cursor.Current = Cursors.Default

      FillFileList()
      RefreshMenuStates()
    End Sub

    Private Sub UnzipFiles()
      If UnzipFilesDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        If ZipContentsList.SelectedItems.Count = 0 Then
          UnzipFiles(System.IO.Path.GetDirectoryName(UnzipFilesDialog.FileName), _
                     ZipContentsList.Items)
        Else
          UnzipFiles(System.IO.Path.GetDirectoryName(UnzipFilesDialog.FileName), _
                     ZipContentsList.SelectedItems)
        End If
      End If
    End Sub

    Private Sub UnzipFiles(ByVal Destination As String, ByVal Items As ICollection)
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
      Try
        Dim DestFolder As DiskFolder = New DiskFolder(Destination)
        Dim SubDestFolder As AbstractFolder

        ' If we were unzipping elsewhere than in a DiskFolder, we could
        ' want to check if it supports BeginUpdate/EndUpdate here.

        Dim Item As ZipContentsItem
        For Each Item In Items
          If Item.File.ParentFolder.IsRoot Then
            Item.File.CopyTo(m_zipEvents, Nothing, DestFolder, True)
          Else
            SubDestFolder = DestFolder.GetFolder(Mid(Item.File.ParentFolder.FullName, 2))

            If Not SubDestFolder.Exists Then
              SubDestFolder.Create()
            End If

            Item.File.CopyTo(m_zipEvents, Nothing, SubDestFolder, True)
          End If
        Next

        MessagePanel.Text = "File(s) unzipped successfully"
      Catch Except As Exception
        MessagePanel.Text = Except.Message
      End Try
      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub DeleteFiles()
      If System.Windows.Forms.DialogResult.Yes = MessageBox.Show("Are you sure you wish to delete the selected file(s)?", _
                                            "Please Confirm...", MessageBoxButtons.YesNo, _
                                            MessageBoxIcon.Question) Then
        DeleteFiles(ZipContentsList.SelectedItems)
      End If
    End Sub

    Private Sub DeleteFiles(ByVal Items As ICollection)
      Try
        ' We do not want the Zip file to be updated after each Delete,
        ' but only after all selected files are deleted. This is similar 
        ' to the ListView.BeginUpdate() method.
        m_zipRoot.BeginUpdate()

        Try
          Dim Item As ZipContentsItem
          For Each Item In Items
            Item.File.Delete(m_zipEvents, Nothing)
          Next
        Finally
          ' For every call to BeginUpdate, there must be a call to EndUpdate.
          m_zipRoot.EndUpdate()
        End Try
      Catch Except As Exception
        MessagePanel.Text = Except.Message
      End Try

      FillFileList()
      RefreshMenuStates()
    End Sub

    Private Sub CheckMethodMenu(ByVal newCompressionMethod As CompressionMethod)
      Dim menu As MenuItem

      For Each menu In CompressionMethodMenu.MenuItems
        menu.Checked = False
      Next

      Select Case newCompressionMethod
        Case CompressionMethod.Stored
          StoredMethodMenu.Checked = True
        Case CompressionMethod.Deflated
          DeflatedMethodMenu.Checked = True
        Case CompressionMethod.Deflated64
          Deflated64MethodMenu.Checked = True
      End Select

      Dim regKey As RegistryKey = GetZipManagerRegistryKey()

      If Not regKey Is Nothing Then
        regKey.SetValue("CompressionMethod", CInt(newCompressionMethod))
        regKey.Close()
      End If
    End Sub

    Private Sub CheckLevelMenu(ByVal newCompressionLevel As CompressionLevel)
      Dim menu As MenuItem      

      For Each menu In CompressionLevelMenu.MenuItems
        menu.Checked = False
      Next

      Select Case newCompressionLevel
        Case CompressionLevel.Highest
          HighestLevelMenu.Checked = True
        Case CompressionLevel.Lowest
          LowestLevelMenu.Checked = True
        Case CompressionLevel.None
          NoneLevelMenu.Checked = True
        Case CompressionLevel.Normal
          NormalLevelMenu.Checked = True
      End Select

      Dim regKey As RegistryKey = GetZipManagerRegistryKey()

      If Not regKey Is Nothing Then
        regKey.SetValue("CompressionLevel", CInt(newCompressionLevel))
        regKey.Close()
      End If
    End Sub

    Private Function GetZipManagerRegistryKey() As RegistryKey
      Dim softwareKey As RegistryKey
      Dim xceedKey As RegistryKey
      Dim zipManagerKey As RegistryKey

      zipManagerKey = Nothing

      softwareKey = Registry.CurrentUser.OpenSubKey("Software", True)

      If softwareKey Is Nothing Then
        softwareKey = Registry.CurrentUser.CreateSubKey("Software")
      End If

      If Not softwareKey Is Nothing Then
        xceedKey = softwareKey.OpenSubKey("Xceed", True)

        If xceedKey Is Nothing Then
          xceedKey = softwareKey.CreateSubKey("Xceed")
        End If

        If Not xceedKey Is Nothing Then
          zipManagerKey = xceedKey.OpenSubKey("ZipManager", True)

          If zipManagerKey Is Nothing Then
            zipManagerKey = xceedKey.CreateSubKey("ZipManager")
          End If
        End If
      End If

      GetZipManagerRegistryKey = zipManagerKey
    End Function

    Private Function GetCompressionMethodFromMenu() As CompressionMethod
      If StoredMethodMenu.Checked Then
        GetCompressionMethodFromMenu = CompressionMethod.Stored
      Else
        If DeflatedMethodMenu.Checked Then
          GetCompressionMethodFromMenu = CompressionMethod.Deflated
        Else
          If Deflated64MethodMenu.Checked Then
            GetCompressionMethodFromMenu = CompressionMethod.Deflated64
          End If
        End If
      End If
    End Function

    Private Function GetCompressionLevelFromMenu() As CompressionLevel
      If HighestLevelMenu.Checked Then
        GetCompressionLevelFromMenu = CompressionLevel.Highest
      Else
        If NormalLevelMenu.Checked Then
          GetCompressionLevelFromMenu = CompressionLevel.Normal
        Else
          If LowestLevelMenu.Checked Then
            GetCompressionLevelFromMenu = CompressionLevel.Lowest
          Else
            If NoneLevelMenu.Checked Then
              GetCompressionLevelFromMenu = CompressionLevel.None
            End If
          End If
        End If
      End If
    End Function

    Private Sub AboutBox()
      MessageBox.Show("Xceed Zip for .NET - ZipManager Sample Application" + vbCrLf + _
                      "Written in VisualBasic.NET" + vbCrLf + _
                      "Copyrights (c) 2001-2006 Xceed Software Inc.", _
                      "About ZipManager...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region " Private Members "
    ' The root of the virtual disk space inside a Zip file.
    ' Can also represent the Zip file itself.
    Private m_zipRoot As ZipArchive

    ' The event object we will pass to Zip methods to receive events.
    ' Using "WithEvents" let's us use the combo boxes above to easily
    ' implement event handlers without having to add this component to
    ' the toolbox.
    Private WithEvents m_zipEvents As New ZipEvents
#End Region


  End Class
End Namespace