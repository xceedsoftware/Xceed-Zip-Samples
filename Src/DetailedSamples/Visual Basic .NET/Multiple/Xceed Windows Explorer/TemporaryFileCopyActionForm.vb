Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Xceed.FileSystem.Samples
  Public Class TemporaryFileCopyActionForm : Inherits System.Windows.Forms.Form
    #Region "CONSTRUCTORS"

    Public Sub New()
      InitializeComponent()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC METHODS"

    Public Overloads Function ShowDialog(ByVal owner As IWin32Window, <System.Runtime.InteropServices.Out()> ByRef fileOnly As Boolean, <System.Runtime.InteropServices.Out()> ByRef recursive As Boolean) As DialogResult
      fileOnly = True
      recursive = False

      Dim result As DialogResult = Me.ShowDialog(owner)

      If result = System.Windows.Forms.DialogResult.OK Then
        If FileOnlyOption.Checked Then
          fileOnly = True
          recursive = False
        Else
          fileOnly = False
          recursive = FileAndFolderRecursiveOption.Checked
        End If
      End If

      Return result
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
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TemporaryFileCopyActionForm))
      Me.label1 = New System.Windows.Forms.Label()
      Me.FileOnlyOption = New System.Windows.Forms.RadioButton()
      Me.FileAndFolderNonRecursiveOption = New System.Windows.Forms.RadioButton()
      Me.FileAndFolderRecursiveOption = New System.Windows.Forms.RadioButton()
      Me.OkBtn = New System.Windows.Forms.Button()
      Me.CancelBtn = New System.Windows.Forms.Button()
      Me.SuspendLayout()
      ' 
      ' label1
      ' 
      Me.label1.Location = New System.Drawing.Point(16, 16)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(312, 40)
      Me.label1.TabIndex = 0
      Me.label1.Text = "In order to complete this operation, some file(s) needs to be copied locally. Sel" & "ect which files you want to copy to a temporary folder."
      ' 
      ' FileOnlyOption
      ' 
      Me.FileOnlyOption.Checked = True
      Me.FileOnlyOption.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.FileOnlyOption.Location = New System.Drawing.Point(32, 64)
      Me.FileOnlyOption.Name = "FileOnlyOption"
      Me.FileOnlyOption.Size = New System.Drawing.Size(288, 24)
      Me.FileOnlyOption.TabIndex = 0
      Me.FileOnlyOption.TabStop = True
      Me.FileOnlyOption.Text = "Selected file only"
      ' 
      ' FileAndFolderNonRecursiveOption
      ' 
      Me.FileAndFolderNonRecursiveOption.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.FileAndFolderNonRecursiveOption.Location = New System.Drawing.Point(32, 88)
      Me.FileAndFolderNonRecursiveOption.Name = "FileAndFolderNonRecursiveOption"
      Me.FileAndFolderNonRecursiveOption.Size = New System.Drawing.Size(288, 24)
      Me.FileAndFolderNonRecursiveOption.TabIndex = 1
      Me.FileAndFolderNonRecursiveOption.Text = "Selected file and folder content (non-recursive)"
      ' 
      ' FileAndFolderRecursiveOption
      ' 
      Me.FileAndFolderRecursiveOption.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.FileAndFolderRecursiveOption.Location = New System.Drawing.Point(32, 112)
      Me.FileAndFolderRecursiveOption.Name = "FileAndFolderRecursiveOption"
      Me.FileAndFolderRecursiveOption.Size = New System.Drawing.Size(288, 24)
      Me.FileAndFolderRecursiveOption.TabIndex = 2
      Me.FileAndFolderRecursiveOption.Text = "Selected file and folder content (recursive)"
      ' 
      ' OkBtn
      ' 
      Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.OkBtn.Location = New System.Drawing.Point(160, 152)
      Me.OkBtn.Name = "OkBtn"
      Me.OkBtn.Size = New System.Drawing.Size(80, 23)
      Me.OkBtn.TabIndex = 3
      Me.OkBtn.Text = "&Ok"
      ' 
      ' CancelBtn
      ' 
      Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CancelBtn.Location = New System.Drawing.Point(248, 152)
      Me.CancelBtn.Name = "CancelBtn"
      Me.CancelBtn.Size = New System.Drawing.Size(80, 23)
      Me.CancelBtn.TabIndex = 4
      Me.CancelBtn.Text = "&Cancel"
      ' 
      ' TemporaryFileCopyActionForm
      ' 
      Me.AcceptButton = Me.OkBtn
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.CancelBtn
      Me.ClientSize = New System.Drawing.Size(338, 184)
      Me.Controls.Add(Me.CancelBtn)
      Me.Controls.Add(Me.OkBtn)
      Me.Controls.Add(Me.FileAndFolderRecursiveOption)
      Me.Controls.Add(Me.FileAndFolderNonRecursiveOption)
      Me.Controls.Add(Me.FileOnlyOption)
      Me.Controls.Add(Me.label1)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
      Me.MaximizeBox = False
      Me.Name = "TemporaryFileCopyActionForm"
      Me.Text = "Copying temporary files"
      Me.ResumeLayout(False)

    End Sub
    #End Region

    #Region "Windows Form Designer generated fields"

    Private label1 As System.Windows.Forms.Label
    Private OkBtn As System.Windows.Forms.Button
    Private FileOnlyOption As System.Windows.Forms.RadioButton
    Private FileAndFolderNonRecursiveOption As System.Windows.Forms.RadioButton
    Private FileAndFolderRecursiveOption As System.Windows.Forms.RadioButton
    Private CancelBtn As System.Windows.Forms.Button
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.Container = Nothing

    #End Region ' Windows Form Designer generated fields
  End Class
End Namespace
