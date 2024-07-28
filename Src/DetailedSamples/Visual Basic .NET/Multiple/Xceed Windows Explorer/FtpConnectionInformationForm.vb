'
'* Xceed Zip for .NET - Xceed Windows Explorer sample application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [FtpConnectionInformationForm.vb]
'*
'* Form used to get Ftp connection information from the user.
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
Imports Xceed.Ftp

Namespace Xceed.FileSystem.Samples
    Public Class FtpConnectionInformationForm : Inherits System.Windows.Forms.Form
#Region "CONSTRUCTORS"

        Public Sub New()
            InitializeComponent()

            ' Fill the combo
            Me.AuthenticationMethodCombo.DataSource = System.Enum.GetValues(GetType(Xceed.Ftp.AuthenticationMethod))
        End Sub

#End Region     ' CONSTRUCTORS

#Region "PUBLIC METHODS"

        Public Overloads Function ShowDialog(ByVal owner As IWin32Window, <System.Runtime.InteropServices.Out()> ByRef connection As FtpConnection) As DialogResult
            connection = Nothing

            Dim result As DialogResult = Me.ShowDialog(owner)

            If result = System.Windows.Forms.DialogResult.OK Then
                ' Default credential for anonymous connection.
                Dim userName As String = "anonymous"
                Dim password As String = "guest"

                If (Not Me.AnonymousConnectionCheck.Checked) Then
                    userName = Me.UserNameText.Text
                    password = Me.PasswordText.Text
                End If

                ' Create a new Ftp connection with the specified information.
                Dim authenticationMethod As Xceed.Ftp.AuthenticationMethod = CType(Me.AuthenticationMethodCombo.SelectedValue, AuthenticationMethod)

                If authenticationMethod <> Xceed.Ftp.AuthenticationMethod.None Then
                    connection = New FtpConnection(Me.ServerAddressText.Text, CInt(Me.PortUpDown.Value), userName, password, authenticationMethod, VerificationFlags.None, Nothing, DataChannelProtection.Private, ImplicitEncryptionCheck.Checked)
                Else
                    connection = New FtpConnection(Me.ServerAddressText.Text, CInt(Me.PortUpDown.Value), userName, password)
                End If

                ' Configure the proxy on the connection.
                If Options.FtpConnectThroughProxy Then
                    connection.Proxy = New HttpProxyClient(Options.FtpProxyServerAddress, Options.FtpProxyServerPort, Options.FtpProxyUsername, Options.FtpProxyPassword)
                End If

                ' Configure TransferMode on the connection.
                If Me.ModeZCheckBox.Checked Then
                    connection.TransferMode = TransferMode.ZLibCompressed
                End If

            End If

            Return result
        End Function

#End Region ' PUBLIC METHODS

#Region "EVENT HANDLERS"

        Private Sub FtpConnectionInformationForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
            If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
                Return
            End If

            If (Not Me.ValidateValues()) Then
                e.Cancel = True
                Return
            End If
        End Sub

        Private Sub AnonymousConnectionCheck_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnonymousConnectionCheck.CheckedChanged
            Me.UserNameText.ReadOnly = Me.AnonymousConnectionCheck.Checked
            Me.PasswordText.ReadOnly = Me.AnonymousConnectionCheck.Checked
        End Sub

#End Region ' EVENT HANDLERS

#Region "PRIVATE METHODS"

        Private Function ValidateValues() As Boolean
            If Me.ServerAddressText.Text.Length = 0 Then
                MessageBox.Show(Me, "You must specify a server address.", "Incomplete information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            If ((Not Me.AnonymousConnectionCheck.Checked)) AndAlso (Me.UserNameText.Text.Length = 0) Then
                MessageBox.Show(Me, "You must specify a user name.", "Incomplete information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
            End If

            Return True
        End Function

#End Region ' PRIVATE METHODS

#Region "PROTECTED METHODS"

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#End Region ' PROTECTED METHODS

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FtpConnectionInformationForm))
            Me.label1 = New System.Windows.Forms.Label
            Me.ServerAddressText = New System.Windows.Forms.TextBox
            Me.AnonymousConnectionCheck = New System.Windows.Forms.CheckBox
            Me.label2 = New System.Windows.Forms.Label
            Me.label3 = New System.Windows.Forms.Label
            Me.UserNameText = New System.Windows.Forms.TextBox
            Me.PasswordText = New System.Windows.Forms.TextBox
            Me.CancelBtn = New System.Windows.Forms.Button
            Me.OkBtn = New System.Windows.Forms.Button
            Me.label4 = New System.Windows.Forms.Label
            Me.PortUpDown = New System.Windows.Forms.NumericUpDown
            Me.label5 = New System.Windows.Forms.Label
            Me.AuthenticationMethodCombo = New System.Windows.Forms.ComboBox
            Me.ImplicitEncryptionCheck = New System.Windows.Forms.CheckBox
            Me.ModeZCheckBox = New System.Windows.Forms.CheckBox
            CType(Me.PortUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(16, 16)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(96, 16)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Server address:"
            '
            'ServerAddressText
            '
            Me.ServerAddressText.Location = New System.Drawing.Point(128, 16)
            Me.ServerAddressText.Name = "ServerAddressText"
            Me.ServerAddressText.Size = New System.Drawing.Size(200, 20)
            Me.ServerAddressText.TabIndex = 0
            '
            'AnonymousConnectionCheck
            '
            Me.AnonymousConnectionCheck.Checked = True
            Me.AnonymousConnectionCheck.CheckState = System.Windows.Forms.CheckState.Checked
            Me.AnonymousConnectionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.AnonymousConnectionCheck.Location = New System.Drawing.Point(16, 168)
            Me.AnonymousConnectionCheck.Name = "AnonymousConnectionCheck"
            Me.AnonymousConnectionCheck.Size = New System.Drawing.Size(312, 16)
            Me.AnonymousConnectionCheck.TabIndex = 5
            Me.AnonymousConnectionCheck.Text = "Anonymous connection"
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(32, 192)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(72, 16)
            Me.label2.TabIndex = 3
            Me.label2.Text = "User name:"
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(32, 216)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(72, 16)
            Me.label3.TabIndex = 4
            Me.label3.Text = "Password:"
            '
            'UserNameText
            '
            Me.UserNameText.Location = New System.Drawing.Point(128, 192)
            Me.UserNameText.Name = "UserNameText"
            Me.UserNameText.ReadOnly = True
            Me.UserNameText.Size = New System.Drawing.Size(200, 20)
            Me.UserNameText.TabIndex = 6
            '
            'PasswordText
            '
            Me.PasswordText.Location = New System.Drawing.Point(128, 216)
            Me.PasswordText.Name = "PasswordText"
            Me.PasswordText.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
            Me.PasswordText.ReadOnly = True
            Me.PasswordText.Size = New System.Drawing.Size(200, 20)
            Me.PasswordText.TabIndex = 7
            '
            'CancelBtn
            '
            Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.CancelBtn.Location = New System.Drawing.Point(248, 248)
            Me.CancelBtn.Name = "CancelBtn"
            Me.CancelBtn.Size = New System.Drawing.Size(80, 24)
            Me.CancelBtn.TabIndex = 9
            Me.CancelBtn.Text = "&Cancel"
            '
            'OkBtn
            '
            Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.OkBtn.Location = New System.Drawing.Point(160, 248)
            Me.OkBtn.Name = "OkBtn"
            Me.OkBtn.Size = New System.Drawing.Size(80, 24)
            Me.OkBtn.TabIndex = 8
            Me.OkBtn.Text = "&Ok"
            '
            'label4
            '
            Me.label4.Location = New System.Drawing.Point(16, 40)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(96, 16)
            Me.label4.TabIndex = 6
            Me.label4.Text = "Port:"
            '
            'PortUpDown
            '
            Me.PortUpDown.Location = New System.Drawing.Point(128, 40)
            Me.PortUpDown.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
            Me.PortUpDown.Name = "PortUpDown"
            Me.PortUpDown.Size = New System.Drawing.Size(72, 20)
            Me.PortUpDown.TabIndex = 1
            Me.PortUpDown.Value = New Decimal(New Integer() {21, 0, 0, 0})
            '
            'label5
            '
            Me.label5.Location = New System.Drawing.Point(16, 72)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(104, 32)
            Me.label5.TabIndex = 7
            Me.label5.Text = "Authentication method"
            '
            'AuthenticationMethodCombo
            '
            Me.AuthenticationMethodCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.AuthenticationMethodCombo.Location = New System.Drawing.Point(128, 72)
            Me.AuthenticationMethodCombo.Name = "AuthenticationMethodCombo"
            Me.AuthenticationMethodCombo.Size = New System.Drawing.Size(200, 21)
            Me.AuthenticationMethodCombo.TabIndex = 2
            '
            'ImplicitEncryptionCheck
            '
            Me.ImplicitEncryptionCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ImplicitEncryptionCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.ImplicitEncryptionCheck.Location = New System.Drawing.Point(16, 136)
            Me.ImplicitEncryptionCheck.Name = "ImplicitEncryptionCheck"
            Me.ImplicitEncryptionCheck.Size = New System.Drawing.Size(128, 16)
            Me.ImplicitEncryptionCheck.TabIndex = 4
            Me.ImplicitEncryptionCheck.Text = "Implicit encryption"
            '
            'ModeZCheckBox
            '
            Me.ModeZCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
            Me.ModeZCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System
            Me.ModeZCheckBox.Location = New System.Drawing.Point(16, 107)
            Me.ModeZCheckBox.Name = "ModeZCheckBox"
            Me.ModeZCheckBox.Size = New System.Drawing.Size(128, 16)
            Me.ModeZCheckBox.TabIndex = 3
            Me.ModeZCheckBox.Text = "Use Mode Z"
            '
            'FtpConnectionInformationForm
            '
            Me.AcceptButton = Me.OkBtn
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.CancelButton = Me.CancelBtn
            Me.ClientSize = New System.Drawing.Size(336, 276)
            Me.Controls.Add(Me.ModeZCheckBox)
            Me.Controls.Add(Me.ImplicitEncryptionCheck)
            Me.Controls.Add(Me.AuthenticationMethodCombo)
            Me.Controls.Add(Me.label5)
            Me.Controls.Add(Me.PortUpDown)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.OkBtn)
            Me.Controls.Add(Me.CancelBtn)
            Me.Controls.Add(Me.PasswordText)
            Me.Controls.Add(Me.UserNameText)
            Me.Controls.Add(Me.AnonymousConnectionCheck)
            Me.Controls.Add(Me.ServerAddressText)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.label1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "FtpConnectionInformationForm"
            Me.Text = "Ftp connection information"
            CType(Me.PortUpDown, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

#Region "Windows Form Designer generated fields"

        Private label1 As System.Windows.Forms.Label
        Private ServerAddressText As System.Windows.Forms.TextBox
        Private WithEvents AnonymousConnectionCheck As System.Windows.Forms.CheckBox
        Private label2 As System.Windows.Forms.Label
        Private label3 As System.Windows.Forms.Label
        Private UserNameText As System.Windows.Forms.TextBox
        Private PasswordText As System.Windows.Forms.TextBox
        Private CancelBtn As System.Windows.Forms.Button
        Private OkBtn As System.Windows.Forms.Button
        Private label4 As System.Windows.Forms.Label
        Private PortUpDown As System.Windows.Forms.NumericUpDown
        Private label5 As System.Windows.Forms.Label
        Private AuthenticationMethodCombo As System.Windows.Forms.ComboBox
        Private ImplicitEncryptionCheck As System.Windows.Forms.CheckBox
        Private WithEvents ModeZCheckBox As System.Windows.Forms.CheckBox
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

#End Region ' Windows Form Designer generated fields
    End Class
End Namespace