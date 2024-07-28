' 
' Xceed FTP for .NET - ClientFtp Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [ConnectionInformation.vb]
'  
' This application demonstrates how to use the Xceed FTP Object model
' in a generic way.
'  
' This file is part of Xceed Ftp for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Public Class ConnectionInformation
  Inherits System.Windows.Forms.Form

#Region "PUBLIC CONSTRUCTORS"

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

  End Sub

#End Region

#Region "EVENTS"

  Private Sub chkAnonymous_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAnonymous.CheckedChanged

    If (chkAnonymous.Checked) Then

      ' Reset the user information to the default values.
      txtUserName.Text = "anonymous"
      txtPassword.Text = "guest"

    End If

    txtUserName.Enabled = Not chkAnonymous.Checked
    txtPassword.Enabled = Not chkAnonymous.Checked

  End Sub

  Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

  End Sub

  Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click

    Me.DialogResult = System.Windows.Forms.DialogResult.OK

  End Sub

  Private Sub chkUseHttpProxy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUseHttpProxy.CheckedChanged

    txtProxyAddress.Enabled = chkUseHttpProxy.Enabled
    txtProxyPort.Enabled = chkUseHttpProxy.Enabled
    txtProxyUserName.Enabled = chkUseHttpProxy.Enabled
    txtProxyPassword.Enabled = chkUseHttpProxy.Enabled

  End Sub

#End Region

#Region "PUBLIC METHODS"

  Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, _
                                       ByRef hostAddress As String, _
                                       ByRef hostPort As Integer, _
                                       ByRef anonymousConnection As Boolean, _
                                       ByRef userName As String, _
                                       ByRef password As String, _
                                       ByRef proxyAddress As String, _
                                       ByRef proxyPort As Integer, _
                                       ByRef proxyUserName As String, _
                                       ByRef proxyPassword As String) As DialogResult

    Return Me.ShowDialog( _
      owner, hostAddress, hostPort, anonymousConnection, userName, password, _
      proxyAddress, proxyPort, proxyUserName, proxyPassword, False)

  End Function

  Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, _
                                       ByRef hostAddress As String, _
                                       ByRef hostPort As Integer, _
                                       ByRef anonymousConnection As Boolean, _
                                       ByRef userName As String, _
                                       ByRef password As String, _
                                       ByRef proxyAddress As String, _
                                       ByRef proxyPort As Integer, _
                                       ByRef proxyUserName As String, _
                                       ByRef proxyPassword As String, _
                                       ByVal userInfoOnly As Boolean) As DialogResult

    ' Initialize the controls with the specified values.
    txtHostAddress.Text = hostAddress
    txtPort.Value = hostPort
    chkAnonymous.Checked = anonymousConnection
    txtUserName.Text = userName
    txtPassword.Text = password
    chkUseHttpProxy.Checked = (proxyAddress.Length > 0)
    txtProxyAddress.Text = proxyAddress
    txtProxyPort.Value = proxyPort
    txtProxyUserName.Text = proxyUserName
    txtProxyPassword.Text = proxyPassword

    If (userInfoOnly) Then

      ' In mode UserInfoOnly, we disable the server address controls.
      txtHostAddress.Enabled = False
      txtPort.Enabled = False

      chkUseHttpProxy.Checked = False
      chkUseHttpProxy.Enabled = False

    End If

    ' Show the dialog.
    Dim result As DialogResult = Me.ShowDialog(owner)

    If (result = System.Windows.Forms.DialogResult.OK) Then

      ' Get the values from the form.
      hostAddress = txtHostAddress.Text
      hostPort = txtPort.Value
      anonymousConnection = chkAnonymous.Checked
      userName = txtUserName.Text
      password = txtPassword.Text

      If (chkUseHttpProxy.Checked) Then

        proxyAddress = txtProxyAddress.Text
        proxyPort = CInt(txtProxyPort.Value)
        proxyUserName = txtProxyUserName.Text
        proxyPassword = txtProxyPassword.Text

      ElseIf Not userInfoOnly Then

        proxyAddress = String.Empty

      End If

    End If

    Return result

  End Function

#End Region

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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  Friend WithEvents cmdOk As System.Windows.Forms.Button
  Friend WithEvents cmdCancel As System.Windows.Forms.Button
  Friend WithEvents grpCredential As System.Windows.Forms.GroupBox
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents lblPassword As System.Windows.Forms.Label
  Friend WithEvents txtUserName As System.Windows.Forms.TextBox
  Friend WithEvents chkAnonymous As System.Windows.Forms.CheckBox
  Friend WithEvents lblUserName As System.Windows.Forms.Label
  Friend WithEvents grpHost As System.Windows.Forms.GroupBox
  Friend WithEvents txtPort As System.Windows.Forms.NumericUpDown
  Friend WithEvents txtHostAddress As System.Windows.Forms.TextBox
  Friend WithEvents lblPort As System.Windows.Forms.Label
  Friend WithEvents lblHostAddress As System.Windows.Forms.Label
  Friend WithEvents grpHttpProxy As System.Windows.Forms.GroupBox
  Friend WithEvents txtProxyPassword As System.Windows.Forms.TextBox
  Friend WithEvents label3 As System.Windows.Forms.Label
  Friend WithEvents txtProxyUserName As System.Windows.Forms.TextBox
  Friend WithEvents label4 As System.Windows.Forms.Label
  Friend WithEvents txtProxyPort As System.Windows.Forms.NumericUpDown
  Friend WithEvents txtProxyAddress As System.Windows.Forms.TextBox
  Friend WithEvents label1 As System.Windows.Forms.Label
  Friend WithEvents label2 As System.Windows.Forms.Label
  Friend WithEvents chkUseHttpProxy As System.Windows.Forms.CheckBox
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectionInformation))
        Me.cmdOk = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.grpCredential = New System.Windows.Forms.GroupBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.chkAnonymous = New System.Windows.Forms.CheckBox
        Me.lblUserName = New System.Windows.Forms.Label
        Me.grpHost = New System.Windows.Forms.GroupBox
        Me.txtPort = New System.Windows.Forms.NumericUpDown
        Me.txtHostAddress = New System.Windows.Forms.TextBox
        Me.lblPort = New System.Windows.Forms.Label
        Me.lblHostAddress = New System.Windows.Forms.Label
        Me.grpHttpProxy = New System.Windows.Forms.GroupBox
        Me.txtProxyPassword = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.txtProxyUserName = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.txtProxyPort = New System.Windows.Forms.NumericUpDown
        Me.txtProxyAddress = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.chkUseHttpProxy = New System.Windows.Forms.CheckBox
        Me.grpCredential.SuspendLayout()
        Me.grpHost.SuspendLayout()
        CType(Me.txtPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpHttpProxy.SuspendLayout()
        CType(Me.txtProxyPort, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdOk
        '
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdOk.Location = New System.Drawing.Point(264, 344)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(72, 24)
        Me.cmdOk.TabIndex = 21
        Me.cmdOk.Text = "&Ok"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cmdCancel.Location = New System.Drawing.Point(344, 344)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(72, 24)
        Me.cmdCancel.TabIndex = 22
        Me.cmdCancel.Text = "&Cancel"
        '
        'grpCredential
        '
        Me.grpCredential.Controls.Add(Me.txtPassword)
        Me.grpCredential.Controls.Add(Me.lblPassword)
        Me.grpCredential.Controls.Add(Me.txtUserName)
        Me.grpCredential.Controls.Add(Me.chkAnonymous)
        Me.grpCredential.Controls.Add(Me.lblUserName)
        Me.grpCredential.Location = New System.Drawing.Point(8, 96)
        Me.grpCredential.Name = "grpCredential"
        Me.grpCredential.Size = New System.Drawing.Size(408, 104)
        Me.grpCredential.TabIndex = 5
        Me.grpCredential.TabStop = False
        Me.grpCredential.Text = "Credential"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(96, 72)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(304, 21)
        Me.txtPassword.TabIndex = 10
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(24, 72)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 9
        Me.lblPassword.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(96, 48)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(304, 21)
        Me.txtUserName.TabIndex = 8
        '
        'chkAnonymous
        '
        Me.chkAnonymous.Location = New System.Drawing.Point(8, 24)
        Me.chkAnonymous.Name = "chkAnonymous"
        Me.chkAnonymous.Size = New System.Drawing.Size(392, 16)
        Me.chkAnonymous.TabIndex = 6
        Me.chkAnonymous.Text = "Anonymous connection"
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(24, 48)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(58, 13)
        Me.lblUserName.TabIndex = 7
        Me.lblUserName.Text = "User name"
        '
        'grpHost
        '
        Me.grpHost.Controls.Add(Me.txtPort)
        Me.grpHost.Controls.Add(Me.txtHostAddress)
        Me.grpHost.Controls.Add(Me.lblPort)
        Me.grpHost.Controls.Add(Me.lblHostAddress)
        Me.grpHost.Location = New System.Drawing.Point(8, 8)
        Me.grpHost.Name = "grpHost"
        Me.grpHost.Size = New System.Drawing.Size(408, 80)
        Me.grpHost.TabIndex = 0
        Me.grpHost.TabStop = False
        Me.grpHost.Text = "Host"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(72, 48)
        Me.txtPort.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(64, 21)
        Me.txtPort.TabIndex = 4
        '
        'txtHostAddress
        '
        Me.txtHostAddress.Location = New System.Drawing.Point(72, 24)
        Me.txtHostAddress.Name = "txtHostAddress"
        Me.txtHostAddress.Size = New System.Drawing.Size(328, 21)
        Me.txtHostAddress.TabIndex = 2
        '
        'lblPort
        '
        Me.lblPort.AutoSize = True
        Me.lblPort.Location = New System.Drawing.Point(8, 48)
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(27, 13)
        Me.lblPort.TabIndex = 3
        Me.lblPort.Text = "Port"
        '
        'lblHostAddress
        '
        Me.lblHostAddress.AutoSize = True
        Me.lblHostAddress.Location = New System.Drawing.Point(8, 24)
        Me.lblHostAddress.Name = "lblHostAddress"
        Me.lblHostAddress.Size = New System.Drawing.Size(46, 13)
        Me.lblHostAddress.TabIndex = 1
        Me.lblHostAddress.Text = "Address"
        '
        'grpHttpProxy
        '
        Me.grpHttpProxy.Controls.Add(Me.txtProxyPassword)
        Me.grpHttpProxy.Controls.Add(Me.label3)
        Me.grpHttpProxy.Controls.Add(Me.txtProxyUserName)
        Me.grpHttpProxy.Controls.Add(Me.label4)
        Me.grpHttpProxy.Controls.Add(Me.txtProxyPort)
        Me.grpHttpProxy.Controls.Add(Me.txtProxyAddress)
        Me.grpHttpProxy.Controls.Add(Me.label1)
        Me.grpHttpProxy.Controls.Add(Me.label2)
        Me.grpHttpProxy.Controls.Add(Me.chkUseHttpProxy)
        Me.grpHttpProxy.Location = New System.Drawing.Point(8, 208)
        Me.grpHttpProxy.Name = "grpHttpProxy"
        Me.grpHttpProxy.Size = New System.Drawing.Size(408, 128)
        Me.grpHttpProxy.TabIndex = 11
        Me.grpHttpProxy.TabStop = False
        Me.grpHttpProxy.Text = "HTTP Proxy"
        '
        'txtProxyPassword
        '
        Me.txtProxyPassword.Enabled = False
        Me.txtProxyPassword.Location = New System.Drawing.Point(96, 96)
        Me.txtProxyPassword.Name = "txtProxyPassword"
        Me.txtProxyPassword.Size = New System.Drawing.Size(304, 21)
        Me.txtProxyPassword.TabIndex = 20
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(24, 96)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(53, 13)
        Me.label3.TabIndex = 19
        Me.label3.Text = "Password"
        '
        'txtProxyUserName
        '
        Me.txtProxyUserName.Enabled = False
        Me.txtProxyUserName.Location = New System.Drawing.Point(96, 72)
        Me.txtProxyUserName.Name = "txtProxyUserName"
        Me.txtProxyUserName.Size = New System.Drawing.Size(304, 21)
        Me.txtProxyUserName.TabIndex = 18
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(24, 72)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(58, 13)
        Me.label4.TabIndex = 17
        Me.label4.Text = "User name"
        '
        'txtProxyPort
        '
        Me.txtProxyPort.Enabled = False
        Me.txtProxyPort.Location = New System.Drawing.Point(336, 48)
        Me.txtProxyPort.Maximum = New Decimal(New Integer() {65536, 0, 0, 0})
        Me.txtProxyPort.Name = "txtProxyPort"
        Me.txtProxyPort.Size = New System.Drawing.Size(64, 21)
        Me.txtProxyPort.TabIndex = 16
        '
        'txtProxyAddress
        '
        Me.txtProxyAddress.Enabled = False
        Me.txtProxyAddress.Location = New System.Drawing.Point(96, 48)
        Me.txtProxyAddress.Name = "txtProxyAddress"
        Me.txtProxyAddress.Size = New System.Drawing.Size(184, 21)
        Me.txtProxyAddress.TabIndex = 14
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(296, 48)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(27, 13)
        Me.label1.TabIndex = 15
        Me.label1.Text = "Port"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(24, 48)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(46, 13)
        Me.label2.TabIndex = 13
        Me.label2.Text = "Address"
        '
        'chkUseHttpProxy
        '
        Me.chkUseHttpProxy.Location = New System.Drawing.Point(16, 24)
        Me.chkUseHttpProxy.Name = "chkUseHttpProxy"
        Me.chkUseHttpProxy.Size = New System.Drawing.Size(376, 16)
        Me.chkUseHttpProxy.TabIndex = 12
        Me.chkUseHttpProxy.Text = "Connect throught an &HTTP Proxy"
        '
        'ConnectionInformation
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(424, 376)
        Me.Controls.Add(Me.grpHttpProxy)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.grpCredential)
        Me.Controls.Add(Me.grpHost)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConnectionInformation"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Connection Information"
        Me.grpCredential.ResumeLayout(False)
        Me.grpCredential.PerformLayout()
        Me.grpHost.ResumeLayout(False)
        Me.grpHost.PerformLayout()
        CType(Me.txtPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpHttpProxy.ResumeLayout(False)
        Me.grpHttpProxy.PerformLayout()
        CType(Me.txtProxyPort, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
