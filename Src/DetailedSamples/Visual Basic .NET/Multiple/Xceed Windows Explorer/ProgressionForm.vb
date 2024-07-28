'
'* Xceed Zip for .NET - Xceed Windows Explorer sample application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [ProgressionForm.vb]
'*
'* Form used to show a progression to the user.
'*
'* This file is part of Xceed Zip for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Xceed.FileSystem.Samples
  Public Class ProgressionForm : Inherits System.Windows.Forms.Form
#Region "CONSTRUCTORS"

    Public Sub New()
      InitializeComponent()
    End Sub

#End Region     ' CONSTRUCTORS

#Region "PUBLIC FIELDS"

    Public Property ActionText() As String
      Get
        Return Me.Text
      End Get
      Set(ByVal Value As String)
        Me.Text = Value
      End Set
    End Property

    Public Property FromText() As String
      Get
        Return FromLabel.Text
      End Get
      Set(ByVal Value As String)
        FromLabel.Text = Value
      End Set
    End Property

    Public Property ToText() As String
      Get
        Return ToLabel.Text
      End Get
      Set(ByVal Value As String)
        ToLabel.Text = Value
      End Set
    End Property

    Public Property CurrentProgressValue() As Integer
      Get
        Return CurrentProgressBar.Value
      End Get
      Set(ByVal Value As Integer)
        If Value > CurrentProgressBar.Maximum Then
          Value = CurrentProgressBar.Maximum
        End If

        If Value < CurrentProgressBar.Minimum Then
          Value = CurrentProgressBar.Minimum
        End If

        CurrentProgressBar.Value = Value
      End Set
    End Property

    Public Property TotalProgressValue() As Integer
      Get
        Return TotalProgressBar.Value
      End Get
      Set(ByVal Value As Integer)
        If Value > TotalProgressBar.Maximum Then
          Value = TotalProgressBar.Maximum
        End If

        If Value < TotalProgressBar.Minimum Then
          Value = TotalProgressBar.Minimum
        End If

        TotalProgressBar.Value = Value
      End Set
    End Property

    Public Property CancelEnabled() As Boolean
      Get
        Return CancelBtn.Enabled
      End Get
      Set(ByVal Value As Boolean)
        CancelBtn.Enabled = Value
      End Set
    End Property

    Public ReadOnly Property UserCancelled() As Boolean
      Get
        Return m_userCancelled
      End Get
    End Property

#End Region     ' PUBLIC FIELDS

#Region "EVENT HANDLERS"

    Private Sub CancelBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
      CancelBtn.Enabled = False
      m_userCancelled = True
    End Sub

#End Region     ' EVENT HANDLERS

#Region "PROTECTED METHODS"

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      MyBase.Dispose(disposing)
    End Sub

#End Region     ' PROTECTED METHODS

#Region "PRIVATE FIELDS"

    Private m_userCancelled As Boolean ' = false

#End Region     ' PRIVATE FIELDS

#Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ProgressionForm))
      Me.TotalProgressBar = New System.Windows.Forms.ProgressBar
      Me.CancelBtn = New System.Windows.Forms.Button
      Me.label1 = New System.Windows.Forms.Label
      Me.FromLabel = New Xceed.FileSystem.Samples.PathLabel
      Me.label2 = New System.Windows.Forms.Label
      Me.ToLabel = New Xceed.FileSystem.Samples.PathLabel
      Me.CurrentProgressBar = New System.Windows.Forms.ProgressBar
      Me.SuspendLayout()
      ' 
      ' TotalProgressBar
      ' 
      Me.TotalProgressBar.Location = New System.Drawing.Point(16, 152)
      Me.TotalProgressBar.Name = "TotalProgressBar"
      Me.TotalProgressBar.Size = New System.Drawing.Size(392, 16)
      Me.TotalProgressBar.TabIndex = 2
      ' 
      ' CancelBtn
      ' 
      Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CancelBtn.Location = New System.Drawing.Point(320, 176)
      Me.CancelBtn.Name = "CancelBtn"
      Me.CancelBtn.Size = New System.Drawing.Size(88, 24)
      Me.CancelBtn.TabIndex = 3
      Me.CancelBtn.Text = "&Cancel"
      '      Me.CancelBtn.Click += New System.EventHandler(Me.CancelBtn_Click);
      ' 
      ' label1
      ' 
      Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
      Me.label1.Location = New System.Drawing.Point(16, 16)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(72, 16)
      Me.label1.TabIndex = 4
      Me.label1.Text = "From"
      ' 
      ' FromLabel
      ' 
      Me.FromLabel.Location = New System.Drawing.Point(16, 40)
      Me.FromLabel.Name = "FromLabel"
      Me.FromLabel.Size = New System.Drawing.Size(384, 16)
      Me.FromLabel.TabIndex = 5
      ' 
      ' label2
      ' 
      Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
      Me.label2.Location = New System.Drawing.Point(16, 72)
      Me.label2.Name = "label2"
      Me.label2.Size = New System.Drawing.Size(72, 16)
      Me.label2.TabIndex = 6
      Me.label2.Text = "To"
      ' 
      ' ToLabel
      ' 
      Me.ToLabel.Location = New System.Drawing.Point(16, 96)
      Me.ToLabel.Name = "ToLabel"
      Me.ToLabel.Size = New System.Drawing.Size(384, 16)
      Me.ToLabel.TabIndex = 7
      Me.ToLabel.UseMnemonic = False
      ' 
      ' CurrentProgressBar
      ' 
      Me.CurrentProgressBar.Location = New System.Drawing.Point(16, 128)
      Me.CurrentProgressBar.Name = "CurrentProgressBar"
      Me.CurrentProgressBar.Size = New System.Drawing.Size(392, 16)
      Me.CurrentProgressBar.TabIndex = 8
      ' 
      ' ProgressionForm
      ' 
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.CancelBtn
      Me.ClientSize = New System.Drawing.Size(416, 208)
      Me.ControlBox = False
      Me.Controls.Add(Me.CurrentProgressBar)
      Me.Controls.Add(Me.label2)
      Me.Controls.Add(Me.ToLabel)
      Me.Controls.Add(Me.FromLabel)
      Me.Controls.Add(Me.label1)
      Me.Controls.Add(Me.CancelBtn)
      Me.Controls.Add(Me.TotalProgressBar)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "ProgressionForm"
      Me.ShowInTaskbar = False
      Me.Text = "Progression"
      Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Windows Form Designer generated fields"

    Private WithEvents CancelBtn As System.Windows.Forms.Button
    Private label1 As System.Windows.Forms.Label
    Private FromLabel As PathLabel
    Private label2 As System.Windows.Forms.Label
    Private TotalProgressBar As System.Windows.Forms.ProgressBar
    Private ToLabel As PathLabel
    Private CurrentProgressBar As System.Windows.Forms.ProgressBar

#End Region     ' Windows Form Designer generated fields
  End Class
End Namespace
