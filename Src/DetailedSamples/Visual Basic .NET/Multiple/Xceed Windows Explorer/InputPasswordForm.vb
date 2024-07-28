'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [InputPasswordForm.vb]
 '*
 '* Form used to get a password from the user for encrypted archives.
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

Namespace Xceed.FileSystem.Samples
  Public Class InputPasswordForm : Inherits System.Windows.Forms.Form
    #Region "CONSTRUCTORS"

    Public Sub New()
      InitializeComponent()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    Public ReadOnly Property Password() As String
      Get
        Return PasswordTextbox.Text
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, ByVal itemName As String) As DialogResult
      ItemNameLabel.Text = itemName

      Return Me.ShowDialog(owner)
    End Function

    #End Region ' PUBLIC METHODS

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

    #Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(InputPasswordForm))
      Me.WarningLabel = New System.Windows.Forms.Label()
      Me.PasswordTextbox = New System.Windows.Forms.TextBox()
      Me.CancelBtn = New System.Windows.Forms.Button()
      Me.OkBtn = New System.Windows.Forms.Button()
      Me.ItemNameLabel = New System.Windows.Forms.Label()
      Me.SuspendLayout()
      ' 
      ' WarningLabel
      ' 
      Me.WarningLabel.Location = New System.Drawing.Point(16, 32)
      Me.WarningLabel.Name = "WarningLabel"
      Me.WarningLabel.Size = New System.Drawing.Size(376, 40)
      Me.WarningLabel.TabIndex = 0
      Me.WarningLabel.Text = "This item is encrypted in the archive. The current password is either invalid or " & "wasn't provided. Please input the password and click 'Ok' to continue. "
      ' 
      ' PasswordTextbox
      ' 
      Me.PasswordTextbox.Location = New System.Drawing.Point(16, 80)
      Me.PasswordTextbox.Name = "PasswordTextbox"
      Me.PasswordTextbox.PasswordChar = "*"c
      Me.PasswordTextbox.Size = New System.Drawing.Size(376, 20)
      Me.PasswordTextbox.TabIndex = 1
      Me.PasswordTextbox.Text = ""
      ' 
      ' CancelBtn
      ' 
      Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CancelBtn.Location = New System.Drawing.Point(312, 112)
      Me.CancelBtn.Name = "CancelBtn"
      Me.CancelBtn.Size = New System.Drawing.Size(80, 24)
      Me.CancelBtn.TabIndex = 6
      Me.CancelBtn.Text = "&Cancel"
      ' 
      ' OkBtn
      ' 
      Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.OkBtn.Location = New System.Drawing.Point(224, 112)
      Me.OkBtn.Name = "OkBtn"
      Me.OkBtn.Size = New System.Drawing.Size(80, 24)
      Me.OkBtn.TabIndex = 5
      Me.OkBtn.Text = "&Ok"
      ' 
      ' ItemNameLabel
      ' 
      Me.ItemNameLabel.Location = New System.Drawing.Point(16, 8)
      Me.ItemNameLabel.Name = "ItemNameLabel"
      Me.ItemNameLabel.Size = New System.Drawing.Size(376, 16)
      Me.ItemNameLabel.TabIndex = 4
      ' 
      ' InputPasswordForm
      ' 
      Me.AcceptButton = Me.OkBtn
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(402, 144)
      Me.Controls.Add(Me.ItemNameLabel)
      Me.Controls.Add(Me.OkBtn)
      Me.Controls.Add(Me.CancelBtn)
      Me.Controls.Add(Me.PasswordTextbox)
      Me.Controls.Add(Me.WarningLabel)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "InputPasswordForm"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
      Me.Text = "Input password"
      Me.ResumeLayout(False)

    End Sub
    #End Region

    #Region "Windows Form Designer generated fields"

    Private WarningLabel As System.Windows.Forms.Label
    Private PasswordTextbox As System.Windows.Forms.TextBox
    Private CancelBtn As System.Windows.Forms.Button
    Private OkBtn As System.Windows.Forms.Button
    Private ItemNameLabel As System.Windows.Forms.Label
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.Container = Nothing

    #End Region ' Windows Form Designer generated fields
  End Class
End Namespace
