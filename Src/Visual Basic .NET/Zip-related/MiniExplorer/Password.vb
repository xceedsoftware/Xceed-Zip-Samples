'
' Xceed Zip for .NET - MiniExplorer Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [Password.vb]
' 
' This application demonstrates how to use the Xceed FileSystem object model
' in a generic way.
' 
' This file is part of Xceed Zip for .NET. The source code in this file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Xceed.FileSystem.Samples.MiniExplorer
  Public Class PasswordForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

    End Sub

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
    Friend WithEvents HideCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PasswordText As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents FilenameLabel As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents CancelPushButton As System.Windows.Forms.Button
    Friend WithEvents OKPushButton As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.HideCheckBox = New System.Windows.Forms.CheckBox()
      Me.PasswordText = New System.Windows.Forms.TextBox()
      Me.label3 = New System.Windows.Forms.Label()
      Me.FilenameLabel = New System.Windows.Forms.Label()
      Me.label1 = New System.Windows.Forms.Label()
      Me.CancelPushButton = New System.Windows.Forms.Button()
      Me.OKPushButton = New System.Windows.Forms.Button()
      Me.SuspendLayout()
      '
      'HideCheckBox
      '
      Me.HideCheckBox.Location = New System.Drawing.Point(7, 95)
      Me.HideCheckBox.Name = "HideCheckBox"
      Me.HideCheckBox.Size = New System.Drawing.Size(136, 16)
      Me.HideCheckBox.TabIndex = 11
      Me.HideCheckBox.Text = "Hide password chars"
      '
      'PasswordText
      '
      Me.PasswordText.Location = New System.Drawing.Point(7, 71)
      Me.PasswordText.Name = "PasswordText"
      Me.PasswordText.Size = New System.Drawing.Size(352, 20)
      Me.PasswordText.TabIndex = 10
      Me.PasswordText.Text = ""
      '
      'label3
      '
      Me.label3.Location = New System.Drawing.Point(7, 55)
      Me.label3.Name = "label3"
      Me.label3.Size = New System.Drawing.Size(240, 16)
      Me.label3.TabIndex = 9
      Me.label3.Text = "Please provide a valid decryption password:"
      '
      'FilenameLabel
      '
      Me.FilenameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
      Me.FilenameLabel.Location = New System.Drawing.Point(7, 23)
      Me.FilenameLabel.Name = "FilenameLabel"
      Me.FilenameLabel.Size = New System.Drawing.Size(352, 16)
      Me.FilenameLabel.TabIndex = 8
      Me.FilenameLabel.Text = "label2"
      '
      'label1
      '
      Me.label1.Location = New System.Drawing.Point(7, 7)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(192, 16)
      Me.label1.TabIndex = 7
      Me.label1.Text = "Decryption password required for file:"
      '
      'CancelPushButton
      '
      Me.CancelPushButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelPushButton.Location = New System.Drawing.Point(279, 111)
      Me.CancelPushButton.Name = "CancelPushButton"
      Me.CancelPushButton.TabIndex = 13
      Me.CancelPushButton.Text = "&Cancel"
      '
      'OKPushButton
      '
      Me.OKPushButton.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OKPushButton.Location = New System.Drawing.Point(199, 111)
      Me.OKPushButton.Name = "OKPushButton"
      Me.OKPushButton.TabIndex = 12
      Me.OKPushButton.Text = "&OK"
      '
      'PasswordForm
      '
      Me.AcceptButton = Me.OKPushButton
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.CancelPushButton
      Me.ClientSize = New System.Drawing.Size(366, 141)
      Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.HideCheckBox, Me.PasswordText, Me.label3, Me.FilenameLabel, Me.label1, Me.CancelPushButton, Me.OKPushButton})
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "PasswordForm"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "Password"
      Me.ResumeLayout(False)

    End Sub

#End Region

    Public Overloads Function ShowDialog(ByVal owner As IWin32Window, ByVal filename As String, ByRef password As String) As DialogResult
      FilenameLabel.Text = filename
      PasswordText.Text = password
      HideCheckBox.Checked = True

      Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

      If MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK Then
        password = PasswordText.Text
        Return System.Windows.Forms.DialogResult.OK
      End If

      Return System.Windows.Forms.DialogResult.Cancel
    End Function

    Private Sub HideCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
      If (HideCheckBox.Checked) Then
        PasswordText.PasswordChar = "*"
      Else
        PasswordText.PasswordChar = "\0"
      End If
    End Sub

  End Class
End Namespace