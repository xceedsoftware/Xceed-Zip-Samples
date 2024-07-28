'
'* Xceed Zip for .NET - Xceed Windows Explorer sample application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [OptionsForm.vb]
'*
'* Form used to sets global application options.
'*
'* This file is part of Xceed Zip for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Xceed.Compression
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples
  Public Class OptionsForm : Inherits System.Windows.Forms.Form
    #Region "CONSTRUCTORS"

    Public Sub New()
      InitializeComponent()

      ' Fill the combo boxes with values of the corresponding enumeration.
      Me.CompressionLevelCombo.DataSource = System.Enum.GetValues(GetType(CompressionLevel))
      Me.CompressionMethodCombo.DataSource = System.Enum.GetValues(GetType(CompressionMethod))
      Me.EncryptionMethodCombo.DataSource = System.Enum.GetValues(GetType(EncryptionMethod))

      Me.ManageProxyControlState()
    End Sub

#End Region     ' CONSTRUCTORS

    #Region "PROTECTED METHODS"

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE METHODS"

    Private Sub InitializeValues()
      ' GZIP
      Me.AllowMultipleFilesCheck.Checked = Options.GZipAllowMultipleFiles

      ' ZIP
      Me.CompressionLevelCombo.SelectedItem = Options.ZipDefaultCompressionLevel
      Me.CompressionMethodCombo.SelectedItem = Options.ZipDefaultCompressionMethod
      Me.FileTimesExtraHeaderCheck.Checked = ((Options.ZipDefaultExtraHeaders And ExtraHeaders.FileTimes) = ExtraHeaders.FileTimes)
      Me.UnicodeExtraHeaderCheck.Checked = ((Options.ZipDefaultExtraHeaders And ExtraHeaders.Unicode) = ExtraHeaders.Unicode)
      Me.EncryptionPasswordText.Text = Options.ZipDefaultEncryptionPassword
      Me.ConfirmPasswordText.Text = Options.ZipDefaultEncryptionPassword
      Me.EncryptionMethodCombo.SelectedItem = Options.ZipDefaultEncryptionMethod

      ' FTP
      Me.ProxyCheckBox.Checked = Options.FtpConnectThroughProxy
      Me.ProxyAddressText.Text = Options.FtpProxyServerAddress
      Me.ProxyAddressPort.Value = Options.FtpProxyServerPort
      Me.ProxyUsernameText.Text = Options.FtpProxyUsername
      Me.ProxyPassword.Text = Options.FtpProxyPassword
    End Sub

    Private Function ValidateValues() As Boolean
      If Me.EncryptionPasswordText.Text <> Me.ConfirmPasswordText.Text Then
        MessageBox.Show(Me, "The encryption password don't match.", "Encryption password error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
      End If

      If (Me.ProxyCheckBox.Checked) AndAlso (Me.ProxyAddressText.Text.Length > 0) Then
        Try
          Dim host As System.Net.IPHostEntry = System.Net.Dns.Resolve(Me.ProxyAddressText.Text)
        Catch except As Exception
          MessageBox.Show(Me, "Unable to resolve the proxy server's address." & Constants.vbLf + Constants.vbLf + except.Message, "DNS error", MessageBoxButtons.OK, MessageBoxIcon.Error)
          Return False
        End Try
      End If

      Return True
    End Function

    Private Sub SaveValues()
      ' GZIP
      Options.GZipAllowMultipleFiles = Me.AllowMultipleFilesCheck.Checked

      ' ZIP
      Options.ZipDefaultCompressionLevel = CType(Me.CompressionLevelCombo.SelectedItem, CompressionLevel)
      Options.ZipDefaultCompressionMethod = CType(Me.CompressionMethodCombo.SelectedItem, CompressionMethod)

      Dim extraHeaders As extraHeaders = extraHeaders.None

      If Me.FileTimesExtraHeaderCheck.Checked Then
        extraHeaders = extraHeaders Or extraHeaders.FileTimes
      End If

      If Me.UnicodeExtraHeaderCheck.Checked Then
        extraHeaders = extraHeaders Or extraHeaders.Unicode
      End If

      Options.ZipDefaultExtraHeaders = extraHeaders

      Options.ZipDefaultEncryptionPassword = Me.EncryptionPasswordText.Text
      Options.ZipLastDecryptionPasswordUsed = Me.EncryptionPasswordText.Text
      Options.ZipDefaultEncryptionMethod = CType(Me.EncryptionMethodCombo.SelectedItem, EncryptionMethod)

      ' FTP
      Options.FtpConnectThroughProxy = Me.ProxyCheckBox.Checked
      If Options.FtpConnectThroughProxy Then
        Options.FtpProxyServerAddress = (Me.ProxyAddressText.Text)
      Else
        Options.FtpProxyServerAddress = (String.Empty)
      End If
      If Options.FtpConnectThroughProxy Then
        Options.FtpProxyServerPort = (CInt(Me.ProxyAddressPort.Value))
      Else
        Options.FtpProxyServerPort = (8080)
      End If
      If Options.FtpConnectThroughProxy Then
        Options.FtpProxyUsername = (Me.ProxyUsernameText.Text)
      Else
        Options.FtpProxyUsername = (String.Empty)
      End If
      If Options.FtpConnectThroughProxy Then
        Options.FtpProxyPassword = (Me.ProxyPassword.Text)
      Else
        Options.FtpProxyPassword = (String.Empty)
      End If
    End Sub

    Private Sub ManageProxyControlState()
      Me.ProxyAddressText.Enabled = Me.ProxyCheckBox.Checked
      Me.ProxyAddressPort.Enabled = Me.ProxyCheckBox.Checked
      Me.ProxyUsernameText.Enabled = Me.ProxyCheckBox.Checked
      Me.ProxyPassword.Enabled = Me.ProxyCheckBox.Checked
    End Sub

#End Region     ' PRIVATE METHODS

    #Region "EVENT HANDLERS"

    Private Sub OptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
      Me.InitializeValues()
    End Sub

    Private Sub OptionsForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
      If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
        Return
      End If

      If (Not Me.ValidateValues()) Then
        e.Cancel = True
        Return
      End If

      Me.SaveValues()
    End Sub

    Private Sub ProxyCheckBox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProxyCheckBox.CheckedChanged
      Me.ManageProxyControlState()
    End Sub

#End Region     ' EVENT HANDLERS

    #Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(OptionsForm))
      Me.ExtraHeadersGroup = New System.Windows.Forms.GroupBox
      Me.FileTimesExtraHeaderCheck = New System.Windows.Forms.CheckBox
      Me.UnicodeExtraHeaderCheck = New System.Windows.Forms.CheckBox
      Me.CancelBtn = New System.Windows.Forms.Button
      Me.OkBtn = New System.Windows.Forms.Button
      Me.CompressionGroup = New System.Windows.Forms.GroupBox
      Me.CompressionMethodCombo = New System.Windows.Forms.ComboBox
      Me.label2 = New System.Windows.Forms.Label
      Me.CompressionLevelCombo = New System.Windows.Forms.ComboBox
      Me.label1 = New System.Windows.Forms.Label
      Me.tabControl1 = New System.Windows.Forms.TabControl
      Me.ZipArchivesPage = New System.Windows.Forms.TabPage
      Me.EncryptionBox = New System.Windows.Forms.GroupBox
      Me.label5 = New System.Windows.Forms.Label
      Me.EncryptionMethodCombo = New System.Windows.Forms.ComboBox
      Me.ConfirmPasswordText = New System.Windows.Forms.TextBox
      Me.label4 = New System.Windows.Forms.Label
      Me.EncryptionPasswordText = New System.Windows.Forms.TextBox
      Me.label3 = New System.Windows.Forms.Label
      Me.GZipArchivesPage = New System.Windows.Forms.TabPage
      Me.AllowMultipleFilesCheck = New System.Windows.Forms.CheckBox
      Me.FtpConnectionsPage = New System.Windows.Forms.TabPage
      Me.ProxyGroup = New System.Windows.Forms.GroupBox
      Me.ProxyPassword = New System.Windows.Forms.TextBox
      Me.ProxyUsernameText = New System.Windows.Forms.TextBox
      Me.label8 = New System.Windows.Forms.Label
      Me.label7 = New System.Windows.Forms.Label
      Me.ProxyAddressPort = New System.Windows.Forms.NumericUpDown
      Me.label6 = New System.Windows.Forms.Label
      Me.ProxyAddressText = New System.Windows.Forms.TextBox
      Me.Address = New System.Windows.Forms.Label
      Me.ProxyCheckBox = New System.Windows.Forms.CheckBox
      Me.ExtraHeadersGroup.SuspendLayout()
      Me.CompressionGroup.SuspendLayout()
      Me.tabControl1.SuspendLayout()
      Me.ZipArchivesPage.SuspendLayout()
      Me.EncryptionBox.SuspendLayout()
      Me.GZipArchivesPage.SuspendLayout()
      Me.FtpConnectionsPage.SuspendLayout()
      Me.ProxyGroup.SuspendLayout()
      CType(Me.ProxyAddressPort, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'ExtraHeadersGroup
      '
      Me.ExtraHeadersGroup.Controls.Add(Me.FileTimesExtraHeaderCheck)
      Me.ExtraHeadersGroup.Controls.Add(Me.UnicodeExtraHeaderCheck)
      Me.ExtraHeadersGroup.Location = New System.Drawing.Point(8, 96)
      Me.ExtraHeadersGroup.Name = "ExtraHeadersGroup"
      Me.ExtraHeadersGroup.Size = New System.Drawing.Size(320, 72)
      Me.ExtraHeadersGroup.TabIndex = 6
      Me.ExtraHeadersGroup.TabStop = False
      Me.ExtraHeadersGroup.Text = "Extra headers"
      '
      'FileTimesExtraHeaderCheck
      '
      Me.FileTimesExtraHeaderCheck.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.FileTimesExtraHeaderCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.FileTimesExtraHeaderCheck.Location = New System.Drawing.Point(16, 48)
      Me.FileTimesExtraHeaderCheck.Name = "FileTimesExtraHeaderCheck"
      Me.FileTimesExtraHeaderCheck.Size = New System.Drawing.Size(296, 16)
      Me.FileTimesExtraHeaderCheck.TabIndex = 8
      Me.FileTimesExtraHeaderCheck.Text = "File times (creation, modification and last accessed)"
      '
      'UnicodeExtraHeaderCheck
      '
      Me.UnicodeExtraHeaderCheck.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.UnicodeExtraHeaderCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.UnicodeExtraHeaderCheck.Location = New System.Drawing.Point(16, 24)
      Me.UnicodeExtraHeaderCheck.Name = "UnicodeExtraHeaderCheck"
      Me.UnicodeExtraHeaderCheck.Size = New System.Drawing.Size(296, 16)
      Me.UnicodeExtraHeaderCheck.TabIndex = 7
      Me.UnicodeExtraHeaderCheck.Text = "Unicode"
      '
      'CancelBtn
      '
      Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CancelBtn.Location = New System.Drawing.Point(264, 328)
      Me.CancelBtn.Name = "CancelBtn"
      Me.CancelBtn.Size = New System.Drawing.Size(88, 24)
      Me.CancelBtn.TabIndex = 18
      Me.CancelBtn.Text = "&Cancel"
      '
      'OkBtn
      '
      Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.OkBtn.Location = New System.Drawing.Point(168, 328)
      Me.OkBtn.Name = "OkBtn"
      Me.OkBtn.Size = New System.Drawing.Size(88, 24)
      Me.OkBtn.TabIndex = 17
      Me.OkBtn.Text = "&Ok"
      '
      'CompressionGroup
      '
      Me.CompressionGroup.Controls.Add(Me.CompressionMethodCombo)
      Me.CompressionGroup.Controls.Add(Me.label2)
      Me.CompressionGroup.Controls.Add(Me.CompressionLevelCombo)
      Me.CompressionGroup.Controls.Add(Me.label1)
      Me.CompressionGroup.Location = New System.Drawing.Point(8, 8)
      Me.CompressionGroup.Name = "CompressionGroup"
      Me.CompressionGroup.Size = New System.Drawing.Size(320, 80)
      Me.CompressionGroup.TabIndex = 1
      Me.CompressionGroup.TabStop = False
      Me.CompressionGroup.Text = "Compression"
      '
      'CompressionMethodCombo
      '
      Me.CompressionMethodCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.CompressionMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.CompressionMethodCombo.Location = New System.Drawing.Point(144, 48)
      Me.CompressionMethodCombo.Name = "CompressionMethodCombo"
      Me.CompressionMethodCombo.Size = New System.Drawing.Size(168, 21)
      Me.CompressionMethodCombo.TabIndex = 5
      '
      'label2
      '
      Me.label2.Location = New System.Drawing.Point(12, 50)
      Me.label2.Name = "label2"
      Me.label2.Size = New System.Drawing.Size(112, 16)
      Me.label2.TabIndex = 4
      Me.label2.Text = "Compression method"
      '
      'CompressionLevelCombo
      '
      Me.CompressionLevelCombo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                  Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.CompressionLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.CompressionLevelCombo.Location = New System.Drawing.Point(144, 24)
      Me.CompressionLevelCombo.Name = "CompressionLevelCombo"
      Me.CompressionLevelCombo.Size = New System.Drawing.Size(168, 21)
      Me.CompressionLevelCombo.TabIndex = 3
      '
      'label1
      '
      Me.label1.Location = New System.Drawing.Point(12, 24)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(112, 16)
      Me.label1.TabIndex = 2
      Me.label1.Text = "Compression level"
      '
      'tabControl1
      '
      Me.tabControl1.Controls.Add(Me.ZipArchivesPage)
      Me.tabControl1.Controls.Add(Me.GZipArchivesPage)
      Me.tabControl1.Controls.Add(Me.FtpConnectionsPage)
      Me.tabControl1.Location = New System.Drawing.Point(8, 8)
      Me.tabControl1.Name = "tabControl1"
      Me.tabControl1.SelectedIndex = 0
      Me.tabControl1.Size = New System.Drawing.Size(344, 312)
      Me.tabControl1.TabIndex = 0
      '
      'ZipArchivesPage
      '
      Me.ZipArchivesPage.Controls.Add(Me.EncryptionBox)
      Me.ZipArchivesPage.Controls.Add(Me.ExtraHeadersGroup)
      Me.ZipArchivesPage.Controls.Add(Me.CompressionGroup)
      Me.ZipArchivesPage.Location = New System.Drawing.Point(4, 22)
      Me.ZipArchivesPage.Name = "ZipArchivesPage"
      Me.ZipArchivesPage.Size = New System.Drawing.Size(336, 286)
      Me.ZipArchivesPage.TabIndex = 0
      Me.ZipArchivesPage.Text = "Zip archives"
      '
      'EncryptionBox
      '
      Me.EncryptionBox.Controls.Add(Me.label5)
      Me.EncryptionBox.Controls.Add(Me.EncryptionMethodCombo)
      Me.EncryptionBox.Controls.Add(Me.ConfirmPasswordText)
      Me.EncryptionBox.Controls.Add(Me.label4)
      Me.EncryptionBox.Controls.Add(Me.EncryptionPasswordText)
      Me.EncryptionBox.Controls.Add(Me.label3)
      Me.EncryptionBox.Location = New System.Drawing.Point(8, 176)
      Me.EncryptionBox.Name = "EncryptionBox"
      Me.EncryptionBox.Size = New System.Drawing.Size(320, 104)
      Me.EncryptionBox.TabIndex = 9
      Me.EncryptionBox.TabStop = False
      Me.EncryptionBox.Text = "Encryption (set a password to encrypt new files)"
      '
      'label5
      '
      Me.label5.Location = New System.Drawing.Point(16, 72)
      Me.label5.Name = "label5"
      Me.label5.Size = New System.Drawing.Size(100, 16)
      Me.label5.TabIndex = 14
      Me.label5.Text = "Encryption method"
      '
      'EncryptionMethodCombo
      '
      Me.EncryptionMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
      Me.EncryptionMethodCombo.Location = New System.Drawing.Point(144, 72)
      Me.EncryptionMethodCombo.Name = "EncryptionMethodCombo"
      Me.EncryptionMethodCombo.Size = New System.Drawing.Size(168, 21)
      Me.EncryptionMethodCombo.TabIndex = 15
      '
      'ConfirmPasswordText
      '
      Me.ConfirmPasswordText.Location = New System.Drawing.Point(144, 48)
      Me.ConfirmPasswordText.MaxLength = 80
      Me.ConfirmPasswordText.Name = "ConfirmPasswordText"
      Me.ConfirmPasswordText.PasswordChar = Microsoft.VisualBasic.ChrW(42)
      Me.ConfirmPasswordText.Size = New System.Drawing.Size(168, 20)
      Me.ConfirmPasswordText.TabIndex = 13
      Me.ConfirmPasswordText.Text = ""
      '
      'label4
      '
      Me.label4.Location = New System.Drawing.Point(16, 48)
      Me.label4.Name = "label4"
      Me.label4.Size = New System.Drawing.Size(112, 16)
      Me.label4.TabIndex = 12
      Me.label4.Text = "Confirm password"
      '
      'EncryptionPasswordText
      '
      Me.EncryptionPasswordText.Location = New System.Drawing.Point(144, 24)
      Me.EncryptionPasswordText.MaxLength = 80
      Me.EncryptionPasswordText.Name = "EncryptionPasswordText"
      Me.EncryptionPasswordText.PasswordChar = Microsoft.VisualBasic.ChrW(42)
      Me.EncryptionPasswordText.Size = New System.Drawing.Size(168, 20)
      Me.EncryptionPasswordText.TabIndex = 11
      Me.EncryptionPasswordText.Text = ""
      '
      'label3
      '
      Me.label3.Location = New System.Drawing.Point(16, 24)
      Me.label3.Name = "label3"
      Me.label3.Size = New System.Drawing.Size(112, 16)
      Me.label3.TabIndex = 10
      Me.label3.Text = "Encryption password"
      '
      'GZipArchivesPage
      '
      Me.GZipArchivesPage.Controls.Add(Me.AllowMultipleFilesCheck)
      Me.GZipArchivesPage.Location = New System.Drawing.Point(4, 22)
      Me.GZipArchivesPage.Name = "GZipArchivesPage"
      Me.GZipArchivesPage.Size = New System.Drawing.Size(336, 286)
      Me.GZipArchivesPage.TabIndex = 1
      Me.GZipArchivesPage.Text = "GZip archives"
      '
      'AllowMultipleFilesCheck
      '
      Me.AllowMultipleFilesCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.AllowMultipleFilesCheck.Location = New System.Drawing.Point(16, 16)
      Me.AllowMultipleFilesCheck.Name = "AllowMultipleFilesCheck"
      Me.AllowMultipleFilesCheck.Size = New System.Drawing.Size(312, 16)
      Me.AllowMultipleFilesCheck.TabIndex = 16
      Me.AllowMultipleFilesCheck.Text = "Allow multiple files"
      '
      'FtpConnectionsPage
      '
      Me.FtpConnectionsPage.Controls.Add(Me.ProxyGroup)
      Me.FtpConnectionsPage.Location = New System.Drawing.Point(4, 22)
      Me.FtpConnectionsPage.Name = "FtpConnectionsPage"
      Me.FtpConnectionsPage.Size = New System.Drawing.Size(336, 286)
      Me.FtpConnectionsPage.TabIndex = 2
      Me.FtpConnectionsPage.Text = "FTP connections"
      '
      'ProxyGroup
      '
      Me.ProxyGroup.Controls.Add(Me.ProxyPassword)
      Me.ProxyGroup.Controls.Add(Me.ProxyUsernameText)
      Me.ProxyGroup.Controls.Add(Me.label8)
      Me.ProxyGroup.Controls.Add(Me.label7)
      Me.ProxyGroup.Controls.Add(Me.ProxyAddressPort)
      Me.ProxyGroup.Controls.Add(Me.label6)
      Me.ProxyGroup.Controls.Add(Me.ProxyAddressText)
      Me.ProxyGroup.Controls.Add(Me.Address)
      Me.ProxyGroup.Controls.Add(Me.ProxyCheckBox)
      Me.ProxyGroup.Location = New System.Drawing.Point(8, 8)
      Me.ProxyGroup.Name = "ProxyGroup"
      Me.ProxyGroup.Size = New System.Drawing.Size(320, 136)
      Me.ProxyGroup.TabIndex = 0
      Me.ProxyGroup.TabStop = False
      Me.ProxyGroup.Text = "Proxy"
      '
      'ProxyPassword
      '
      Me.ProxyPassword.Location = New System.Drawing.Point(96, 104)
      Me.ProxyPassword.Name = "ProxyPassword"
      Me.ProxyPassword.PasswordChar = Microsoft.VisualBasic.ChrW(42)
      Me.ProxyPassword.Size = New System.Drawing.Size(216, 20)
      Me.ProxyPassword.TabIndex = 8
      Me.ProxyPassword.Text = ""
      '
      'ProxyUsernameText
      '
      Me.ProxyUsernameText.Location = New System.Drawing.Point(96, 80)
      Me.ProxyUsernameText.Name = "ProxyUsernameText"
      Me.ProxyUsernameText.Size = New System.Drawing.Size(216, 20)
      Me.ProxyUsernameText.TabIndex = 7
      Me.ProxyUsernameText.Text = ""
      '
      'label8
      '
      Me.label8.Location = New System.Drawing.Point(32, 104)
      Me.label8.Name = "label8"
      Me.label8.Size = New System.Drawing.Size(64, 16)
      Me.label8.TabIndex = 6
      Me.label8.Text = "Password"
      '
      'label7
      '
      Me.label7.Location = New System.Drawing.Point(32, 80)
      Me.label7.Name = "label7"
      Me.label7.Size = New System.Drawing.Size(64, 16)
      Me.label7.TabIndex = 5
      Me.label7.Text = "User name"
      '
      'ProxyAddressPort
      '
      Me.ProxyAddressPort.Location = New System.Drawing.Point(256, 48)
      Me.ProxyAddressPort.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
      Me.ProxyAddressPort.Name = "ProxyAddressPort"
      Me.ProxyAddressPort.Size = New System.Drawing.Size(56, 20)
      Me.ProxyAddressPort.TabIndex = 4
      Me.ProxyAddressPort.Value = New Decimal(New Integer() {8080, 0, 0, 0})
      '
      'label6
      '
      Me.label6.Location = New System.Drawing.Point(216, 48)
      Me.label6.Name = "label6"
      Me.label6.Size = New System.Drawing.Size(32, 16)
      Me.label6.TabIndex = 3
      Me.label6.Text = "Port"
      '
      'ProxyAddressText
      '
      Me.ProxyAddressText.Location = New System.Drawing.Point(96, 48)
      Me.ProxyAddressText.Name = "ProxyAddressText"
      Me.ProxyAddressText.Size = New System.Drawing.Size(104, 20)
      Me.ProxyAddressText.TabIndex = 2
      Me.ProxyAddressText.Text = ""
      '
      'Address
      '
      Me.Address.Location = New System.Drawing.Point(32, 48)
      Me.Address.Name = "Address"
      Me.Address.Size = New System.Drawing.Size(56, 16)
      Me.Address.TabIndex = 1
      Me.Address.Text = "Address"
      '
      'ProxyCheckBox
      '
      Me.ProxyCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ProxyCheckBox.Location = New System.Drawing.Point(16, 24)
      Me.ProxyCheckBox.Name = "ProxyCheckBox"
      Me.ProxyCheckBox.Size = New System.Drawing.Size(296, 16)
      Me.ProxyCheckBox.TabIndex = 0
      Me.ProxyCheckBox.Text = "Connect through an HTTP proxy server"
      '
      'OptionsForm
      '
      Me.AcceptButton = Me.OkBtn
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.CancelBtn
      Me.ClientSize = New System.Drawing.Size(362, 360)
      Me.Controls.Add(Me.tabControl1)
      Me.Controls.Add(Me.OkBtn)
      Me.Controls.Add(Me.CancelBtn)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
      Me.MaximizeBox = False
      Me.Name = "OptionsForm"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "Options"
      Me.ExtraHeadersGroup.ResumeLayout(False)
      Me.CompressionGroup.ResumeLayout(False)
      Me.tabControl1.ResumeLayout(False)
      Me.ZipArchivesPage.ResumeLayout(False)
      Me.EncryptionBox.ResumeLayout(False)
      Me.GZipArchivesPage.ResumeLayout(False)
      Me.FtpConnectionsPage.ResumeLayout(False)
      Me.ProxyGroup.ResumeLayout(False)
      CType(Me.ProxyAddressPort, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub
#End Region

    #Region "Windows Form Designer generated members"

    Private ExtraHeadersGroup As System.Windows.Forms.GroupBox
    Private FileTimesExtraHeaderCheck As System.Windows.Forms.CheckBox
    Private UnicodeExtraHeaderCheck As System.Windows.Forms.CheckBox
    Private CancelBtn As System.Windows.Forms.Button
    Private OkBtn As System.Windows.Forms.Button
    Private CompressionGroup As System.Windows.Forms.GroupBox
    Private CompressionMethodCombo As System.Windows.Forms.ComboBox
    Private label2 As System.Windows.Forms.Label
    Private CompressionLevelCombo As System.Windows.Forms.ComboBox
    Private label1 As System.Windows.Forms.Label
    Private tabControl1 As System.Windows.Forms.TabControl
    Private ZipArchivesPage As System.Windows.Forms.TabPage
    Private GZipArchivesPage As System.Windows.Forms.TabPage
    Private AllowMultipleFilesCheck As System.Windows.Forms.CheckBox
    Private EncryptionBox As System.Windows.Forms.GroupBox
    Private label3 As System.Windows.Forms.Label
    Private EncryptionPasswordText As System.Windows.Forms.TextBox
    Private label4 As System.Windows.Forms.Label
    Private ConfirmPasswordText As System.Windows.Forms.TextBox
    Private EncryptionMethodCombo As System.Windows.Forms.ComboBox
    Private label5 As System.Windows.Forms.Label
    Private FtpConnectionsPage As System.Windows.Forms.TabPage
    Private ProxyGroup As System.Windows.Forms.GroupBox
    Private WithEvents ProxyCheckBox As System.Windows.Forms.CheckBox
    Private Address As System.Windows.Forms.Label
    Private label6 As System.Windows.Forms.Label
    Private label7 As System.Windows.Forms.Label
    Private label8 As System.Windows.Forms.Label
    Private ProxyAddressText As System.Windows.Forms.TextBox
    Private ProxyAddressPort As System.Windows.Forms.NumericUpDown
    Private ProxyUsernameText As System.Windows.Forms.TextBox
    Private ProxyPassword As System.Windows.Forms.TextBox
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.Container = Nothing

    #End Region ' Windows Form Designer generated members
  End Class
End Namespace
