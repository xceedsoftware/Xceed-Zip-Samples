Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace FolderViews
  Public Class AboutForm
    Inherits System.Windows.Forms.Form

    Private pictureBox1 As System.Windows.Forms.PictureBox
    Private panel1 As System.Windows.Forms.Panel
    Private label1 As System.Windows.Forms.Label
    Private label2 As System.Windows.Forms.Label
    Private okButton As System.Windows.Forms.Button
    Private components As System.ComponentModel.Container = Nothing

    Public Sub New()
      InitializeComponent()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If (disposing) Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

#Region "Windows Form Designer generated code"
    Private Sub InitializeComponent()
      Dim resources As New System.Resources.ResourceManager(GetType(AboutForm))
      Me.pictureBox1 = New System.Windows.Forms.PictureBox
      Me.panel1 = New System.Windows.Forms.Panel
      Me.okButton = New System.Windows.Forms.Button
      Me.label2 = New System.Windows.Forms.Label
      Me.label1 = New System.Windows.Forms.Label
      Me.panel1.SuspendLayout()
      Me.SuspendLayout()
      ' 
      ' pictureBox1
      ' 
      Me.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top
      Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
      Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
      Me.pictureBox1.Name = "pictureBox1"
      Me.pictureBox1.Size = New System.Drawing.Size(300, 89)
      Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
      Me.pictureBox1.TabIndex = 0
      Me.pictureBox1.TabStop = False
      ' 
      ' panel1
      ' 
      Me.panel1.BackColor = System.Drawing.Color.White
      Me.panel1.Controls.Add(Me.okButton)
      Me.panel1.Controls.Add(Me.label2)
      Me.panel1.Controls.Add(Me.label1)
      Me.panel1.Controls.Add(Me.pictureBox1)
      Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
      Me.panel1.Location = New System.Drawing.Point(5, 5)
      Me.panel1.Name = "panel1"
      Me.panel1.Size = New System.Drawing.Size(246, 261)
      Me.panel1.TabIndex = 1
      ' 
      ' okButton
      ' 
      Me.okButton.BackColor = System.Drawing.SystemColors.Control
      Me.okButton.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.okButton.ForeColor = System.Drawing.SystemColors.ControlText
      Me.okButton.Location = New System.Drawing.Point(160, 224)
      Me.okButton.Name = "okButton"
      Me.okButton.TabIndex = 3
      Me.okButton.Text = "Ok"
      ' 
      ' label2
      ' 
      Me.label2.Font = New System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, System.Byte))
      Me.label2.Location = New System.Drawing.Point(8, 128)
      Me.label2.Name = "label2"
      Me.label2.Size = New System.Drawing.Size(232, 88)
      Me.label2.TabIndex = 2
      Me.label2.Text = "This sample demonstrates how to use the Xceed FTP FileSystem interface to provide" + " universal use of any kind of file or folder, regardless of their location (on d" + "isk, on an FTP server, in memory, in isolated storage...)."
      ' 
      ' label1 
      Me.label1.Location = New System.Drawing.Point(8, 104)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(232, 23)
      Me.label1.TabIndex = 1
      Me.label1.Text = "FolderViews C# sample"
      ' 
      ' AboutForm
      ' 
      Me.AcceptButton = Me.okButton
      Me.AutoScaleBaseSize = New System.Drawing.Size(7, 15)
      Me.BackColor = System.Drawing.Color.MidnightBlue
      Me.ClientSize = New System.Drawing.Size(256, 271)
      Me.Controls.Add(Me.panel1)
      Me.DockPadding.All = 5
      Me.Font = New System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, System.Byte))
      Me.ForeColor = System.Drawing.Color.MidnightBlue
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
      Me.Name = "AboutForm"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "About the FolderViews sample..."
      Me.panel1.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
#End Region

    Const WM_NCHITTEST As Integer = &H84
    Private Const HTCAPTION As Integer = &H2

    Protected Overrides Sub WndProc(ByRef m As Message)
      If m.Msg = WM_NCHITTEST Then
        ' All the form's surface can be used to move it.
        'm.Result = CType(HTCAPTION, IntPtr)
        Return
      End If

      MyBase.WndProc(m)
    End Sub
  End Class
End Namespace
