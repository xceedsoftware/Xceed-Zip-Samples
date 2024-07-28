' 
' Xceed FTP for .NET - ClientFtp Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [FtpItemName.vb]
'  
' This application demonstrates how to use the Xceed FTP Object model
' in a generic way.
'  
' This file is part of Xceed Ftp for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Public Class FtpItemName
  Inherits System.Windows.Forms.Form

#Region "PUBLIC CONSTRUCTORS"

  Public Sub New()
    MyBase.New()

    'This call is required by the Windows Form Designer.
    InitializeComponent()

  End Sub

#End Region

#Region "EVENTS"

  Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

  End Sub

  Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click

    Me.DialogResult = System.Windows.Forms.DialogResult.OK

  End Sub

#End Region

#Region "PUBLIC METHODS"

  Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, _
                                       ByVal formCaption As String, _
                                       ByRef itemName As String) As DialogResult

    Me.Text = formCaption

    txtName.Text = itemName

    Dim result As DialogResult = Me.ShowDialog(owner)

    If (result = System.Windows.Forms.DialogResult.OK) Then

      itemName = txtName.Text

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
  Friend WithEvents txtName As System.Windows.Forms.TextBox
  Friend WithEvents lblName As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FtpItemName))
    Me.cmdOk = New System.Windows.Forms.Button()
    Me.cmdCancel = New System.Windows.Forms.Button()
    Me.txtName = New System.Windows.Forms.TextBox()
    Me.lblName = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'cmdOk
    '
    Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.cmdOk.Location = New System.Drawing.Point(112, 40)
    Me.cmdOk.Name = "cmdOk"
    Me.cmdOk.Size = New System.Drawing.Size(72, 24)
    Me.cmdOk.TabIndex = 6
    Me.cmdOk.Text = "&Ok"
    '
    'cmdCancel
    '
    Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.cmdCancel.Location = New System.Drawing.Point(192, 40)
    Me.cmdCancel.Name = "cmdCancel"
    Me.cmdCancel.Size = New System.Drawing.Size(72, 24)
    Me.cmdCancel.TabIndex = 7
    Me.cmdCancel.Text = "&Cancel"
    '
    'txtName
    '
    Me.txtName.Location = New System.Drawing.Point(56, 8)
    Me.txtName.Name = "txtName"
    Me.txtName.Size = New System.Drawing.Size(208, 21)
    Me.txtName.TabIndex = 5
    Me.txtName.Text = ""
    '
    'lblName
    '
    Me.lblName.AutoSize = True
    Me.lblName.Location = New System.Drawing.Point(8, 8)
    Me.lblName.Name = "lblName"
    Me.lblName.Size = New System.Drawing.Size(33, 14)
    Me.lblName.TabIndex = 4
    Me.lblName.Text = "Name"
    '
    'FtpItemName
    '
    Me.AcceptButton = Me.cmdOk
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
    Me.CancelButton = Me.cmdCancel
    Me.ClientSize = New System.Drawing.Size(272, 70)
    Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmdOk, Me.cmdCancel, Me.txtName, Me.lblName})
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FtpItemName"
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
    Me.Text = "[RUNTIME]"
    Me.ResumeLayout(False)

  End Sub

#End Region
End Class
