'
' Xceed Zip for .NET - MiniExplorer Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [SplitSize.vb]
' 
' This application demonstrates how to use the Xceed FileSystem object model
' in a generic way.
' 
' This file is part of Xceed Zip for .NET. The source code in this file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Imports Xceed.Zip

Public Class SplitSizeForm
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
  Friend WithEvents FailButton As System.Windows.Forms.Button
  Friend WithEvents OkButton As System.Windows.Forms.Button
  Friend WithEvents SplitSizeUpDown As System.Windows.Forms.NumericUpDown
  Friend WithEvents label1 As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.FailButton = New System.Windows.Forms.Button()
    Me.OkButton = New System.Windows.Forms.Button()
    Me.SplitSizeUpDown = New System.Windows.Forms.NumericUpDown()
    Me.label1 = New System.Windows.Forms.Label()
    CType(Me.SplitSizeUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'FailButton
    '
    Me.FailButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.FailButton.Location = New System.Drawing.Point(239, 48)
    Me.FailButton.Name = "FailButton"
    Me.FailButton.TabIndex = 11
    Me.FailButton.Text = "&Cancel"
    '
    'OkButton
    '
    Me.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.OkButton.Location = New System.Drawing.Point(159, 48)
    Me.OkButton.Name = "OkButton"
    Me.OkButton.TabIndex = 10
    Me.OkButton.Text = "&Ok"
    '
    'SplitSizeUpDown
    '
    Me.SplitSizeUpDown.Increment = New Decimal(New Integer() {1024, 0, 0, 0})
    Me.SplitSizeUpDown.Location = New System.Drawing.Point(207, 8)
    Me.SplitSizeUpDown.Maximum = New Decimal(New Integer() {999999999, 0, 0, 0})
    Me.SplitSizeUpDown.Name = "SplitSizeUpDown"
    Me.SplitSizeUpDown.Size = New System.Drawing.Size(104, 20)
    Me.SplitSizeUpDown.TabIndex = 9
    Me.SplitSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.SplitSizeUpDown.ThousandsSeparator = True
    '
    'label1
    '
    Me.label1.Location = New System.Drawing.Point(7, 12)
    Me.label1.Name = "label1"
    Me.label1.Size = New System.Drawing.Size(200, 16)
    Me.label1.TabIndex = 8
    Me.label1.Text = "Maximum size of each part (in bytes):"
    '
    'SplitSizeForm
    '
    Me.AcceptButton = Me.OkButton
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.FailButton
    Me.ClientSize = New System.Drawing.Size(320, 78)
    Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.FailButton, Me.OkButton, Me.SplitSizeUpDown, Me.label1})
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "SplitSizeForm"
    Me.Text = "Split existing Zip file..."
    CType(Me.SplitSizeUpDown, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#End Region

  Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, ByRef splitSize As Long) As System.Windows.Forms.DialogResult

    SplitSizeUpDown.Value = splitSize
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

    If (MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK) Then
      splitSize = CType(SplitSizeUpDown.Value, Long)
      Return System.Windows.Forms.DialogResult.OK
    End If

    Return System.Windows.Forms.DialogResult.Cancel

  End Function
End Class
