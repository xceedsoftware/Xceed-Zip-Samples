Imports Xceed.Zip
Imports Xceed.FileSystem

Public Class DiskRequiredForm
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
  Friend WithEvents ReasonLabel As System.Windows.Forms.Label
  Friend WithEvents ContinueButton As System.Windows.Forms.Button
  Friend WithEvents FailButton As System.Windows.Forms.Button
  Friend WithEvents FullNameText As System.Windows.Forms.TextBox
  Friend WithEvents RootNameLabel As System.Windows.Forms.Label
  Friend WithEvents IntroLabel As System.Windows.Forms.Label
  <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    Me.ReasonLabel = New System.Windows.Forms.Label()
    Me.ContinueButton = New System.Windows.Forms.Button()
    Me.FailButton = New System.Windows.Forms.Button()
    Me.FullNameText = New System.Windows.Forms.TextBox()
    Me.RootNameLabel = New System.Windows.Forms.Label()
    Me.IntroLabel = New System.Windows.Forms.Label()
    Me.SuspendLayout()
    '
    'ReasonLabel
    '
    Me.ReasonLabel.Location = New System.Drawing.Point(6, 8)
    Me.ReasonLabel.Name = "ReasonLabel"
    Me.ReasonLabel.Size = New System.Drawing.Size(440, 16)
    Me.ReasonLabel.TabIndex = 11
    '
    'ContinueButton
    '
    Me.ContinueButton.DialogResult = System.Windows.Forms.DialogResult.OK
    Me.ContinueButton.Location = New System.Drawing.Point(294, 112)
    Me.ContinueButton.Name = "ContinueButton"
    Me.ContinueButton.TabIndex = 9
    Me.ContinueButton.Text = "&Ok"
    '
    'FailButton
    '
    Me.FailButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.FailButton.Location = New System.Drawing.Point(374, 112)
    Me.FailButton.Name = "FailButton"
    Me.FailButton.TabIndex = 10
    Me.FailButton.Text = "&Cancel"
    '
    'FullNameText
    '
    Me.FullNameText.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right)
    Me.FullNameText.Location = New System.Drawing.Point(78, 80)
    Me.FullNameText.Name = "FullNameText"
    Me.FullNameText.Size = New System.Drawing.Size(370, 20)
    Me.FullNameText.TabIndex = 8
    Me.FullNameText.Text = "Path\Name"
    '
    'RootNameLabel
    '
    Me.RootNameLabel.AutoSize = True
    Me.RootNameLabel.Location = New System.Drawing.Point(6, 84)
    Me.RootNameLabel.Name = "RootNameLabel"
    Me.RootNameLabel.Size = New System.Drawing.Size(65, 13)
    Me.RootNameLabel.TabIndex = 7
    Me.RootNameLabel.Text = "RootName:\"
    '
    'IntroLabel
    '
    Me.IntroLabel.Location = New System.Drawing.Point(6, 32)
    Me.IntroLabel.Name = "IntroLabel"
    Me.IntroLabel.Size = New System.Drawing.Size(440, 40)
    Me.IntroLabel.TabIndex = 6
    Me.IntroLabel.Text = "Disk #N or Part #N of this zip file is required. If this zip file is located on a" & _
    " different removable disk, make sure the correct disk is inserted. If this part " & _
    "has a different name, change the filename below."
    '
    'DiskRequired
    '
    Me.AcceptButton = Me.ContinueButton
    Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
    Me.CancelButton = Me.FailButton
    Me.ClientSize = New System.Drawing.Size(456, 144)
    Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.ReasonLabel, Me.ContinueButton, Me.FailButton, Me.FullNameText, Me.RootNameLabel, Me.IntroLabel})
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "DiskRequired"
    Me.Text = "DiskRequiredSimple"
    Me.ResumeLayout(False)

  End Sub

#End Region


  Public Overloads Function ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window, ByRef zipFile As AbstractFile, ByVal diskNumber As Integer, ByVal reason As DiskRequiredReason) As System.Windows.Forms.DialogResult
    Dim rootName As String = zipFile.RootFolder.FullName
    Dim fullName As String = zipFile.FullName

    RootNameLabel.Text = rootName
    FullNameText.Text = fullName.Substring(rootName.Length, fullName.Length - rootName.Length)

    IntroLabel.Text = "Disk #" + diskNumber.ToString() & _
      " or Part #" + diskNumber.ToString() & _
      " of this zip file is required. If this zip file is located on a different removable disk, make sure the correct disk is inserted. If this part has a different name, change the filename below."

    Select Case (reason)

    Case DiskRequiredReason.Deleting
        ReasonLabel.Text = "Deleting unused zip file parts."
        
      Case DiskRequiredReason.DiskFull
        ReasonLabel.Text = "The disk is full."
          
      Case DiskRequiredReason.Reading
        ReasonLabel.Text = "Reading from a zip file part."
          
      Case DiskRequiredReason.SplitSizeReached
        ReasonLabel.Text = "Reached the maximum split size."


      Case DiskRequiredReason.Updating
        ReasonLabel.Text = "Building new zip file."
          
      Case Else
        ReasonLabel.Text = "Unknown reason."

    End Select

    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

    If (MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK) Then
      zipFile = zipFile.RootFolder.GetFile(FullNameText.Text)
      Return System.Windows.Forms.DialogResult.OK
    End If

    Return System.Windows.Forms.DialogResult.Cancel
  End Function

  Private Sub DiskRequired_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

    Dim Left As Integer = RootNameLabel.Left + RootNameLabel.Width + 4
    Dim diff As Integer = FullNameText.Left - Left

    FullNameText.Left = Left
    FullNameText.Width += diff

  End Sub

End Class
