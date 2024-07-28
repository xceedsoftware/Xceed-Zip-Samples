'Xceed Zip for .NET - ZipManager Sample Application
'Copyright (c) 2000-2002 - Xceed Software Inc.
'
'[SfxOptions.vb]
'
'This application demonstrates how to use Xceed Zip for .NET.
'
'This file is part of Xceed Zip for .NET. The source code in this file is 
'only intended as a supplement to the documentation, and is provided 
'"as is", without warranty of any kind, either expressed or implied.

Imports Xceed.FileSystem
Imports Xceed.Zip.Sfx
Imports System.IO

Namespace Xceed.Zip.Samples
  Public Class SfxOptions
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal sfxPrefix As SfxPrefix)
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call
      m_sfxPrefix = sfxPrefix

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
    Friend WithEvents chkCreateSFX As System.Windows.Forms.CheckBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpStringsAndMessages As System.Windows.Forms.GroupBox
    Friend WithEvents grpFilesAndFolders As System.Windows.Forms.GroupBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents dlgOpen As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblIntroduction As System.Windows.Forms.Label
    Friend WithEvents lblSuccess As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtIntroduction As System.Windows.Forms.TextBox
    Friend WithEvents txtSuccess As System.Windows.Forms.TextBox
    Friend WithEvents txtFolder As System.Windows.Forms.TextBox
    Friend WithEvents lblFolder As System.Windows.Forms.Label
    Friend WithEvents txtReadme As System.Windows.Forms.TextBox
    Friend WithEvents lblReadme As System.Windows.Forms.Label
    Friend WithEvents txtIcon As System.Windows.Forms.TextBox
    Friend WithEvents lblIcon As System.Windows.Forms.Label
    Friend WithEvents btnReadme As System.Windows.Forms.Button
    Friend WithEvents btnIcon As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.chkCreateSFX = New System.Windows.Forms.CheckBox()
      Me.btnOk = New System.Windows.Forms.Button()
      Me.btnCancel = New System.Windows.Forms.Button()
      Me.grpStringsAndMessages = New System.Windows.Forms.GroupBox()
      Me.txtSuccess = New System.Windows.Forms.TextBox()
      Me.txtIntroduction = New System.Windows.Forms.TextBox()
      Me.txtTitle = New System.Windows.Forms.TextBox()
      Me.lblSuccess = New System.Windows.Forms.Label()
      Me.lblIntroduction = New System.Windows.Forms.Label()
      Me.lblTitle = New System.Windows.Forms.Label()
      Me.grpFilesAndFolders = New System.Windows.Forms.GroupBox()
      Me.btnIcon = New System.Windows.Forms.Button()
      Me.btnReadme = New System.Windows.Forms.Button()
      Me.txtIcon = New System.Windows.Forms.TextBox()
      Me.lblIcon = New System.Windows.Forms.Label()
      Me.txtReadme = New System.Windows.Forms.TextBox()
      Me.lblReadme = New System.Windows.Forms.Label()
      Me.txtFolder = New System.Windows.Forms.TextBox()
      Me.lblFolder = New System.Windows.Forms.Label()
      Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
      Me.grpStringsAndMessages.SuspendLayout()
      Me.grpFilesAndFolders.SuspendLayout()
      Me.SuspendLayout()
      '
      'chkCreateSFX
      '
      Me.chkCreateSFX.Location = New System.Drawing.Point(16, 16)
      Me.chkCreateSFX.Name = "chkCreateSFX"
      Me.chkCreateSFX.Size = New System.Drawing.Size(184, 24)
      Me.chkCreateSFX.TabIndex = 0
      Me.chkCreateSFX.Text = "Create a &self-extracting zip file"
      '
      'btnOk
      '
      Me.btnOk.Location = New System.Drawing.Point(488, 16)
      Me.btnOk.Name = "btnOk"
      Me.btnOk.TabIndex = 1
      Me.btnOk.Text = "&OK"
      '
      'btnCancel
      '
      Me.btnCancel.Location = New System.Drawing.Point(488, 48)
      Me.btnCancel.Name = "btnCancel"
      Me.btnCancel.TabIndex = 2
      Me.btnCancel.Text = "&Cancel"
      '
      'grpStringsAndMessages
      '
      Me.grpStringsAndMessages.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtSuccess, Me.txtIntroduction, Me.txtTitle, Me.lblSuccess, Me.lblIntroduction, Me.lblTitle})
      Me.grpStringsAndMessages.Enabled = False
      Me.grpStringsAndMessages.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.grpStringsAndMessages.Location = New System.Drawing.Point(16, 48)
      Me.grpStringsAndMessages.Name = "grpStringsAndMessages"
      Me.grpStringsAndMessages.Size = New System.Drawing.Size(456, 192)
      Me.grpStringsAndMessages.TabIndex = 3
      Me.grpStringsAndMessages.TabStop = False
      Me.grpStringsAndMessages.Text = "Strings and Messages"
      '
      'txtSuccess
      '
      Me.txtSuccess.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtSuccess.Location = New System.Drawing.Point(176, 120)
      Me.txtSuccess.Multiline = True
      Me.txtSuccess.Name = "txtSuccess"
      Me.txtSuccess.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.txtSuccess.Size = New System.Drawing.Size(264, 56)
      Me.txtSuccess.TabIndex = 10
      Me.txtSuccess.Text = "All files were succesfully unzipped."
      '
      'txtIntroduction
      '
      Me.txtIntroduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtIntroduction.Location = New System.Drawing.Point(176, 56)
      Me.txtIntroduction.Multiline = True
      Me.txtIntroduction.Name = "txtIntroduction"
      Me.txtIntroduction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.txtIntroduction.Size = New System.Drawing.Size(264, 56)
      Me.txtIntroduction.TabIndex = 9
      Me.txtIntroduction.Text = "Welcome to the Xceed Zip Self-Extractor." & Microsoft.VisualBasic.ChrW(13) & Microsoft.VisualBasic.ChrW(10) & "This program will unzip some files onto" & _
      " your system."
      '
      'txtTitle
      '
      Me.txtTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtTitle.Location = New System.Drawing.Point(176, 28)
      Me.txtTitle.Name = "txtTitle"
      Me.txtTitle.Size = New System.Drawing.Size(264, 20)
      Me.txtTitle.TabIndex = 8
      Me.txtTitle.Text = "The Xceed Zip Self-Extractor"
      '
      'lblSuccess
      '
      Me.lblSuccess.AutoSize = True
      Me.lblSuccess.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblSuccess.Location = New System.Drawing.Point(16, 120)
      Me.lblSuccess.Name = "lblSuccess"
      Me.lblSuccess.Size = New System.Drawing.Size(99, 13)
      Me.lblSuccess.TabIndex = 7
      Me.lblSuccess.Text = "Success message:"
      '
      'lblIntroduction
      '
      Me.lblIntroduction.AutoSize = True
      Me.lblIntroduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblIntroduction.Location = New System.Drawing.Point(16, 60)
      Me.lblIntroduction.Name = "lblIntroduction"
      Me.lblIntroduction.Size = New System.Drawing.Size(116, 13)
      Me.lblIntroduction.TabIndex = 6
      Me.lblIntroduction.Text = "Introduction message:"
      '
      'lblTitle
      '
      Me.lblTitle.AutoSize = True
      Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblTitle.Location = New System.Drawing.Point(16, 32)
      Me.lblTitle.Name = "lblTitle"
      Me.lblTitle.Size = New System.Drawing.Size(144, 13)
      Me.lblTitle.TabIndex = 5
      Me.lblTitle.Text = "Self-extracting zip file's title:"
      '
      'grpFilesAndFolders
      '
      Me.grpFilesAndFolders.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnIcon, Me.btnReadme, Me.txtIcon, Me.lblIcon, Me.txtReadme, Me.lblReadme, Me.txtFolder, Me.lblFolder})
      Me.grpFilesAndFolders.Enabled = False
      Me.grpFilesAndFolders.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.grpFilesAndFolders.Location = New System.Drawing.Point(16, 256)
      Me.grpFilesAndFolders.Name = "grpFilesAndFolders"
      Me.grpFilesAndFolders.Size = New System.Drawing.Size(456, 112)
      Me.grpFilesAndFolders.TabIndex = 4
      Me.grpFilesAndFolders.TabStop = False
      Me.grpFilesAndFolders.Text = "Files and Folders"
      '
      'btnIcon
      '
      Me.btnIcon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.btnIcon.Location = New System.Drawing.Point(408, 80)
      Me.btnIcon.Name = "btnIcon"
      Me.btnIcon.Size = New System.Drawing.Size(32, 23)
      Me.btnIcon.TabIndex = 16
      Me.btnIcon.Text = "..."
      '
      'btnReadme
      '
      Me.btnReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.btnReadme.Location = New System.Drawing.Point(408, 48)
      Me.btnReadme.Name = "btnReadme"
      Me.btnReadme.Size = New System.Drawing.Size(32, 23)
      Me.btnReadme.TabIndex = 15
      Me.btnReadme.Text = "..."
      '
      'txtIcon
      '
      Me.txtIcon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtIcon.Location = New System.Drawing.Point(176, 80)
      Me.txtIcon.Name = "txtIcon"
      Me.txtIcon.Size = New System.Drawing.Size(224, 20)
      Me.txtIcon.TabIndex = 14
      Me.txtIcon.Text = ""
      '
      'lblIcon
      '
      Me.lblIcon.AutoSize = True
      Me.lblIcon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblIcon.Location = New System.Drawing.Point(16, 84)
      Me.lblIcon.Name = "lblIcon"
      Me.lblIcon.Size = New System.Drawing.Size(79, 13)
      Me.lblIcon.TabIndex = 13
      Me.lblIcon.Text = "SFX file's icon:"
      '
      'txtReadme
      '
      Me.txtReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtReadme.Location = New System.Drawing.Point(176, 50)
      Me.txtReadme.Name = "txtReadme"
      Me.txtReadme.Size = New System.Drawing.Size(224, 20)
      Me.txtReadme.TabIndex = 12
      Me.txtReadme.Text = ""
      '
      'lblReadme
      '
      Me.lblReadme.AutoSize = True
      Me.lblReadme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblReadme.Location = New System.Drawing.Point(16, 54)
      Me.lblReadme.Name = "lblReadme"
      Me.lblReadme.Size = New System.Drawing.Size(119, 13)
      Me.lblReadme.TabIndex = 11
      Me.lblReadme.Text = "Readme file to display:"
      '
      'txtFolder
      '
      Me.txtFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.txtFolder.Location = New System.Drawing.Point(176, 20)
      Me.txtFolder.Name = "txtFolder"
      Me.txtFolder.Size = New System.Drawing.Size(264, 20)
      Me.txtFolder.TabIndex = 10
      Me.txtFolder.Text = ""
      '
      'lblFolder
      '
      Me.lblFolder.AutoSize = True
      Me.lblFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.lblFolder.Location = New System.Drawing.Point(16, 24)
      Me.lblFolder.Name = "lblFolder"
      Me.lblFolder.Size = New System.Drawing.Size(153, 13)
      Me.lblFolder.TabIndex = 9
      Me.lblFolder.Text = "Default folder to unzip files to:"
      '
      'dlgOpen
      '
      Me.dlgOpen.AddExtension = False
      Me.dlgOpen.RestoreDirectory = True
      '
      'SfxOptions
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(578, 391)
      Me.ControlBox = False
      Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.grpFilesAndFolders, Me.grpStringsAndMessages, Me.btnCancel, Me.btnOk, Me.chkCreateSFX})
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "SfxOptions"
      Me.Text = "Self-extracting zip file options"
      Me.grpStringsAndMessages.ResumeLayout(False)
      Me.grpFilesAndFolders.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub

#End Region

    Public Property SfxPrefix() As SfxPrefix
      Get
        Return m_sfxPrefix
      End Get
      Set(ByVal Value As SfxPrefix)
        m_sfxPrefix = Value
      End Set
    End Property


    Private Sub SfxOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      txtFolder.Text = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
      If (m_sfxPrefix Is Nothing) Then

        Dim xceedSfxPrefix As New xceedSfxPrefix(New DiskFile("..\..\Xcdsfx32.bin"))

        xceedSfxPrefix.DialogStrings(DialogStrings.Title) = txtTitle.Text
        xceedSfxPrefix.DialogMessages(DialogMessages.Introduction) = txtIntroduction.Text
        xceedSfxPrefix.DialogMessages(DialogMessages.Success) = txtSuccess.Text
        xceedSfxPrefix.DefaultDestinationFolder = txtFolder.Text

        If (Not txtIcon.Text = String.Empty) Then
          xceedSfxPrefix.Icon = New Icon(txtIcon.Text)
        End If

        If (Not txtReadme.Text = String.Empty) Then
          xceedSfxPrefix.ExecuteAfter.Add(txtReadme.Text)
        End If

        m_sfxPrefix = xceedSfxPrefix

      End If

      Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
      Me.Close()
    End Sub

    Private Sub chkCreateSFX_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCreateSFX.CheckedChanged
      grpStringsAndMessages.Enabled = Not grpStringsAndMessages.Enabled
      grpFilesAndFolders.Enabled = Not grpFilesAndFolders.Enabled
    End Sub

    Private Sub btnReadme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReadme.Click
      dlgOpen.Filter = "Readme files (*.txt)|*.txt"
      dlgOpen.Title = "Select readme file"
      dlgOpen.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length)
      If (dlgOpen.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
        txtReadme.Text = dlgOpen.FileName
      End If
    End Sub

    Private Sub btnIcon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIcon.Click
      dlgOpen.Filter = "Icon files (*.ico)|*.ico"
      dlgOpen.Title = "Select icon file"
      dlgOpen.InitialDirectory = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - "ZipManager.exe".Length)
      If (dlgOpen.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
        txtIcon.Text = dlgOpen.FileName
      End If
    End Sub

    Private m_sfxPrefix As New SfxPrefix()

  End Class
End Namespace