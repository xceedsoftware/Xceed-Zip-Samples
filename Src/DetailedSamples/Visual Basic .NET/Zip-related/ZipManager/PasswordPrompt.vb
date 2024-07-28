Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Xceed.Zip

Namespace Xceed.Zip.Samples
  ''' <summary>
  ''' Summary description for PasswordPrompt.
  ''' </summary>
  Public Class PasswordPrompt : Inherits System.Windows.Forms.Form
    Private MessageLabel As System.Windows.Forms.Label
    Private PasswordLabel As System.Windows.Forms.Label
    Private PasswordText As System.Windows.Forms.TextBox
    Private EncryptionMethodBox As System.Windows.Forms.GroupBox
    Private TraditionalEncryptionRadio As System.Windows.Forms.RadioButton
    Private WinZipAESEncryptionRadio As System.Windows.Forms.RadioButton
    Private StrengthBox As System.Windows.Forms.GroupBox
    Private Bits256Radio As System.Windows.Forms.RadioButton
    Private Bits192Radio As System.Windows.Forms.RadioButton
    Private Bits128Radio As System.Windows.Forms.RadioButton
    Private AbortButton As System.Windows.Forms.Button
    Private OkButton As System.Windows.Forms.Button
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.Container = Nothing

    Public Sub New()
      '
      ' Required for Windows Form Designer support
      '
      InitializeComponent()

      '
      ' TODO: Add any constructor code after InitializeComponent call
      '
    End Sub

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

#Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Me.MessageLabel = New System.Windows.Forms.Label
      Me.PasswordLabel = New System.Windows.Forms.Label
      Me.PasswordText = New System.Windows.Forms.TextBox
      Me.EncryptionMethodBox = New System.Windows.Forms.GroupBox
      Me.StrengthBox = New System.Windows.Forms.GroupBox
      Me.Bits256Radio = New System.Windows.Forms.RadioButton
      Me.Bits192Radio = New System.Windows.Forms.RadioButton
      Me.Bits128Radio = New System.Windows.Forms.RadioButton
      Me.WinZipAESEncryptionRadio = New System.Windows.Forms.RadioButton
      Me.TraditionalEncryptionRadio = New System.Windows.Forms.RadioButton
      Me.AbortButton = New System.Windows.Forms.Button
      Me.OkButton = New System.Windows.Forms.Button
      Me.EncryptionMethodBox.SuspendLayout()
      Me.StrengthBox.SuspendLayout()
      Me.SuspendLayout()
      ' 
      ' MessageLabel
      ' 
      Me.MessageLabel.Location = New System.Drawing.Point(16, 16)
      Me.MessageLabel.Name = "MessageLabel"
      Me.MessageLabel.Size = New System.Drawing.Size(352, 40)
      Me.MessageLabel.TabIndex = 0
      Me.MessageLabel.Text = "label1"
      ' 
      ' PasswordLabel
      ' 
      Me.PasswordLabel.Location = New System.Drawing.Point(16, 64)
      Me.PasswordLabel.Name = "PasswordLabel"
      Me.PasswordLabel.Size = New System.Drawing.Size(352, 16)
      Me.PasswordLabel.TabIndex = 1
      Me.PasswordLabel.Text = "label1"
      ' 
      ' PasswordText
      ' 
      Me.PasswordText.Location = New System.Drawing.Point(16, 80)
      Me.PasswordText.Name = "PasswordText"
      Me.PasswordText.Size = New System.Drawing.Size(352, 20)
      Me.PasswordText.TabIndex = 2
      Me.PasswordText.Text = ""
      ' 
      ' EncryptionMethodBox
      ' 
      Me.EncryptionMethodBox.Controls.Add(Me.StrengthBox)
      Me.EncryptionMethodBox.Controls.Add(Me.WinZipAESEncryptionRadio)
      Me.EncryptionMethodBox.Controls.Add(Me.TraditionalEncryptionRadio)
      Me.EncryptionMethodBox.Location = New System.Drawing.Point(16, 112)
      Me.EncryptionMethodBox.Name = "EncryptionMethodBox"
      Me.EncryptionMethodBox.Size = New System.Drawing.Size(352, 136)
      Me.EncryptionMethodBox.TabIndex = 3
      Me.EncryptionMethodBox.TabStop = False
      Me.EncryptionMethodBox.Text = "Encryption Method"
      ' 
      ' StrengthBox
      ' 
      Me.StrengthBox.Controls.Add(Me.Bits256Radio)
      Me.StrengthBox.Controls.Add(Me.Bits192Radio)
      Me.StrengthBox.Controls.Add(Me.Bits128Radio)
      Me.StrengthBox.Location = New System.Drawing.Point(48, 72)
      Me.StrengthBox.Name = "StrengthBox"
      Me.StrengthBox.Size = New System.Drawing.Size(288, 48)
      Me.StrengthBox.TabIndex = 3
      Me.StrengthBox.TabStop = False
      Me.StrengthBox.Text = "Strength"
      ' 
      ' Bits256Radio
      ' 
      Me.Bits256Radio.Location = New System.Drawing.Point(176, 24)
      Me.Bits256Radio.Name = "Bits256Radio"
      Me.Bits256Radio.Size = New System.Drawing.Size(64, 16)
      Me.Bits256Radio.TabIndex = 5
      Me.Bits256Radio.Text = "256-bit"
      ' 
      ' Bits192Radio
      ' 
      Me.Bits192Radio.Location = New System.Drawing.Point(96, 24)
      Me.Bits192Radio.Name = "Bits192Radio"
      Me.Bits192Radio.Size = New System.Drawing.Size(64, 16)
      Me.Bits192Radio.TabIndex = 4
      Me.Bits192Radio.Text = "192-bit"
      ' 
      ' Bits128Radio
      ' 
      Me.Bits128Radio.Location = New System.Drawing.Point(16, 24)
      Me.Bits128Radio.Name = "Bits128Radio"
      Me.Bits128Radio.Size = New System.Drawing.Size(64, 16)
      Me.Bits128Radio.TabIndex = 3
      Me.Bits128Radio.Text = "128-bit"
      ' 
      ' WinZipAESEncryptionRadio
      ' 
      Me.WinZipAESEncryptionRadio.Location = New System.Drawing.Point(24, 48)
      Me.WinZipAESEncryptionRadio.Name = "WinZipAESEncryptionRadio"
      Me.WinZipAESEncryptionRadio.Size = New System.Drawing.Size(312, 16)
      Me.WinZipAESEncryptionRadio.TabIndex = 1
      Me.WinZipAESEncryptionRadio.Text = "WinZip &AES Encryption (strong)"
      ' 
      ' TraditionalEncryptionRadio
      ' 
      Me.TraditionalEncryptionRadio.Location = New System.Drawing.Point(24, 24)
      Me.TraditionalEncryptionRadio.Name = "TraditionalEncryptionRadio"
      Me.TraditionalEncryptionRadio.Size = New System.Drawing.Size(312, 16)
      Me.TraditionalEncryptionRadio.TabIndex = 0
      Me.TraditionalEncryptionRadio.Text = "&Traditional Zip Encryption (weak)"
      ' 
      ' AbortButton
      ' 
      Me.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.AbortButton.Location = New System.Drawing.Point(296, 256)
      Me.AbortButton.Name = "AbortButton"
      Me.AbortButton.TabIndex = 4
      Me.AbortButton.Text = "&Cancel"
      ' 
      ' OkButton
      ' 
      Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OkButton.Location = New System.Drawing.Point(216, 256)
      Me.OkButton.Name = "OkButton"
      Me.OkButton.TabIndex = 5
      Me.OkButton.Text = "&Ok"
      ' 
      ' PasswordPrompt
      ' 
      Me.AcceptButton = Me.OkButton
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.AbortButton
      Me.ClientSize = New System.Drawing.Size(384, 288)
      Me.Controls.Add(Me.OkButton)
      Me.Controls.Add(Me.AbortButton)
      Me.Controls.Add(Me.EncryptionMethodBox)
      Me.Controls.Add(Me.PasswordText)
      Me.Controls.Add(Me.PasswordLabel)
      Me.Controls.Add(Me.MessageLabel)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "PasswordPrompt"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.EncryptionMethodBox.ResumeLayout(False)
      Me.StrengthBox.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
#End Region

    Public Overloads Function ShowDialog(ByVal owner As IWin32Window, ByRef password As String, ByVal title As String, ByVal message As String, ByVal label As String, ByRef method As EncryptionMethod, ByRef strength As Integer, ByVal readingOnly As Boolean) As DialogResult
      Me.Text = title
      MessageLabel.Text = message
      PasswordLabel.Text = label
      PasswordText.Text = password

      TraditionalEncryptionRadio.Checked = (method = EncryptionMethod.Compatible)
      WinZipAESEncryptionRadio.Checked = (method = EncryptionMethod.WinZipAes)
      Bits128Radio.Checked = (strength = 128)
      Bits192Radio.Checked = (strength = 192)
      Bits256Radio.Checked = (strength = 256)

      EncryptionMethodBox.Enabled = Not readingOnly

      If MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK Then
        password = PasswordText.Text

        If (Not readingOnly) Then
          method = IIf((WinZipAESEncryptionRadio.Checked), (EncryptionMethod.WinZipAes), (EncryptionMethod.Compatible))
          strength = IIf((Bits128Radio.Checked), (128), (IIf((Bits192Radio.Checked), (192), (256))))
        End If

        Return System.Windows.Forms.DialogResult.OK
      End If

      Return System.Windows.Forms.DialogResult.Cancel
    End Function
  End Class
End Namespace
