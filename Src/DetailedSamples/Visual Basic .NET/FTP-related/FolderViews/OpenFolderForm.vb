Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.Ftp

Namespace FolderViews
    Public Class OpenFolderForm
        Inherits System.Windows.Forms.Form

        Private label1 As System.Windows.Forms.Label
        Private openButton As System.Windows.Forms.Button
        Private Shadows cancelButton As System.Windows.Forms.Button
        Private drivesRadio As System.Windows.Forms.RadioButton
        WithEvents networkRadio As System.Windows.Forms.RadioButton
        Private isolatedRadio As System.Windows.Forms.RadioButton
        Private networkLabel As System.Windows.Forms.Label
        Private networkTextBox As System.Windows.Forms.TextBox
        Private memoryRadio As System.Windows.Forms.RadioButton
        WithEvents ftpRadio As System.Windows.Forms.RadioButton
        Private userLabel As System.Windows.Forms.Label
        Private passwordLabel As System.Windows.Forms.Label
        Private userTextBox As System.Windows.Forms.TextBox
        Private passwordTextBox As System.Windows.Forms.TextBox
        Private hostTextBox As System.Windows.Forms.TextBox
        Private hostLabel As System.Windows.Forms.Label
        Private portTextBox As System.Windows.Forms.TextBox
        Private portLabel As System.Windows.Forms.Label
        WithEvents useModeZCheckBox As System.Windows.Forms.CheckBox

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
            Me.drivesRadio = New System.Windows.Forms.RadioButton
            Me.networkRadio = New System.Windows.Forms.RadioButton
            Me.isolatedRadio = New System.Windows.Forms.RadioButton
            Me.label1 = New System.Windows.Forms.Label
            Me.networkLabel = New System.Windows.Forms.Label
            Me.networkTextBox = New System.Windows.Forms.TextBox
            Me.memoryRadio = New System.Windows.Forms.RadioButton
            Me.ftpRadio = New System.Windows.Forms.RadioButton
            Me.userLabel = New System.Windows.Forms.Label
            Me.passwordLabel = New System.Windows.Forms.Label
            Me.userTextBox = New System.Windows.Forms.TextBox
            Me.passwordTextBox = New System.Windows.Forms.TextBox
            Me.hostTextBox = New System.Windows.Forms.TextBox
            Me.hostLabel = New System.Windows.Forms.Label
            Me.portTextBox = New System.Windows.Forms.TextBox
            Me.portLabel = New System.Windows.Forms.Label
            Me.openButton = New System.Windows.Forms.Button
            Me.cancelButton = New System.Windows.Forms.Button
            Me.useModeZCheckBox = New System.Windows.Forms.CheckBox
            Me.SuspendLayout()
            '
            'drivesRadio
            '
            Me.drivesRadio.Location = New System.Drawing.Point(16, 32)
            Me.drivesRadio.Name = "drivesRadio"
            Me.drivesRadio.Size = New System.Drawing.Size(168, 16)
            Me.drivesRadio.TabIndex = 0
            Me.drivesRadio.Text = "My computer's logical &drives"
            '
            'networkRadio
            '
            Me.networkRadio.Location = New System.Drawing.Point(16, 64)
            Me.networkRadio.Name = "networkRadio"
            Me.networkRadio.Size = New System.Drawing.Size(152, 16)
            Me.networkRadio.TabIndex = 1
            Me.networkRadio.Text = "A particular &network path"
            '
            'isolatedRadio
            '
            Me.isolatedRadio.Location = New System.Drawing.Point(16, 144)
            Me.isolatedRadio.Name = "isolatedRadio"
            Me.isolatedRadio.Size = New System.Drawing.Size(200, 16)
            Me.isolatedRadio.TabIndex = 3
            Me.isolatedRadio.Text = "The current user's &isolated storage"
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label1.Location = New System.Drawing.Point(8, 8)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(336, 16)
            Me.label1.TabIndex = 0
            Me.label1.Text = "Select folder to view:"
            '
            'networkLabel
            '
            Me.networkLabel.Enabled = False
            Me.networkLabel.Location = New System.Drawing.Point(48, 88)
            Me.networkLabel.Name = "networkLabel"
            Me.networkLabel.Size = New System.Drawing.Size(100, 16)
            Me.networkLabel.TabIndex = 3
            Me.networkLabel.Text = "Network path:"
            '
            'networkTextBox
            '
            Me.networkTextBox.Enabled = False
            Me.networkTextBox.Location = New System.Drawing.Point(48, 104)
            Me.networkTextBox.Name = "networkTextBox"
            Me.networkTextBox.Size = New System.Drawing.Size(296, 20)
            Me.networkTextBox.TabIndex = 2
            '
            'memoryRadio
            '
            Me.memoryRadio.Location = New System.Drawing.Point(16, 176)
            Me.memoryRadio.Name = "memoryRadio"
            Me.memoryRadio.Size = New System.Drawing.Size(152, 16)
            Me.memoryRadio.TabIndex = 4
            Me.memoryRadio.Text = "A volatile &memory folder"
            '
            'ftpRadio
            '
            Me.ftpRadio.Location = New System.Drawing.Point(16, 208)
            Me.ftpRadio.Name = "ftpRadio"
            Me.ftpRadio.Size = New System.Drawing.Size(184, 16)
            Me.ftpRadio.TabIndex = 5
            Me.ftpRadio.Text = "An &FTP server's starting folder"
            '
            'userLabel
            '
            Me.userLabel.Enabled = False
            Me.userLabel.Location = New System.Drawing.Point(48, 272)
            Me.userLabel.Name = "userLabel"
            Me.userLabel.Size = New System.Drawing.Size(112, 16)
            Me.userLabel.TabIndex = 12
            Me.userLabel.Text = "Username:"
            '
            'passwordLabel
            '
            Me.passwordLabel.Enabled = False
            Me.passwordLabel.Location = New System.Drawing.Point(168, 272)
            Me.passwordLabel.Name = "passwordLabel"
            Me.passwordLabel.Size = New System.Drawing.Size(104, 16)
            Me.passwordLabel.TabIndex = 14
            Me.passwordLabel.Text = "Password:"
            '
            'userTextBox
            '
            Me.userTextBox.Enabled = False
            Me.userTextBox.Location = New System.Drawing.Point(48, 288)
            Me.userTextBox.Name = "userTextBox"
            Me.userTextBox.Size = New System.Drawing.Size(112, 20)
            Me.userTextBox.TabIndex = 8
            Me.userTextBox.Text = "anonymous"
            '
            'passwordTextBox
            '
            Me.passwordTextBox.Enabled = False
            Me.passwordTextBox.Location = New System.Drawing.Point(168, 288)
            Me.passwordTextBox.Name = "passwordTextBox"
            Me.passwordTextBox.Size = New System.Drawing.Size(112, 20)
            Me.passwordTextBox.TabIndex = 9
            Me.passwordTextBox.Text = "guest"
            Me.passwordTextBox.UseSystemPasswordChar = True
            '
            'hostTextBox
            '
            Me.hostTextBox.Enabled = False
            Me.hostTextBox.Location = New System.Drawing.Point(48, 248)
            Me.hostTextBox.Name = "hostTextBox"
            Me.hostTextBox.Size = New System.Drawing.Size(176, 20)
            Me.hostTextBox.TabIndex = 6
            Me.hostTextBox.Text = "ftp.xceed.com"
            '
            'hostLabel
            '
            Me.hostLabel.Enabled = False
            Me.hostLabel.Location = New System.Drawing.Point(48, 232)
            Me.hostLabel.Name = "hostLabel"
            Me.hostLabel.Size = New System.Drawing.Size(176, 16)
            Me.hostLabel.TabIndex = 8
            Me.hostLabel.Text = "FTP server hostname or address:"
            '
            'portTextBox
            '
            Me.portTextBox.Enabled = False
            Me.portTextBox.Location = New System.Drawing.Point(232, 248)
            Me.portTextBox.Name = "portTextBox"
            Me.portTextBox.Size = New System.Drawing.Size(48, 20)
            Me.portTextBox.TabIndex = 7
            Me.portTextBox.Text = "21"
            '
            'portLabel
            '
            Me.portLabel.Enabled = False
            Me.portLabel.Location = New System.Drawing.Point(232, 232)
            Me.portLabel.Name = "portLabel"
            Me.portLabel.Size = New System.Drawing.Size(48, 16)
            Me.portLabel.TabIndex = 10
            Me.portLabel.Text = "Port:"
            '
            'openButton
            '
            Me.openButton.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.openButton.Location = New System.Drawing.Point(189, 364)
            Me.openButton.Name = "openButton"
            Me.openButton.Size = New System.Drawing.Size(75, 23)
            Me.openButton.TabIndex = 11
            Me.openButton.Text = "&Open"
            '
            'cancelButton
            '
            Me.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.cancelButton.Location = New System.Drawing.Point(269, 364)
            Me.cancelButton.Name = "cancelButton"
            Me.cancelButton.Size = New System.Drawing.Size(75, 23)
            Me.cancelButton.TabIndex = 12
            Me.cancelButton.Text = "&Cancel"
            '
            'useModeZCheckBox
            '
            Me.useModeZCheckBox.AutoSize = True
            Me.useModeZCheckBox.Location = New System.Drawing.Point(48, 324)
            Me.useModeZCheckBox.Name = "useModeZCheckBox"
            Me.useModeZCheckBox.Size = New System.Drawing.Size(85, 17)
            Me.useModeZCheckBox.TabIndex = 10
            Me.useModeZCheckBox.Text = "Use Mode Z"
            Me.useModeZCheckBox.UseVisualStyleBackColor = True
            '
            'OpenFolderForm
            '
            Me.AcceptButton = Me.openButton
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(360, 399)
            Me.ControlBox = False
            Me.Controls.Add(Me.useModeZCheckBox)
            Me.Controls.Add(Me.cancelButton)
            Me.Controls.Add(Me.openButton)
            Me.Controls.Add(Me.portTextBox)
            Me.Controls.Add(Me.hostTextBox)
            Me.Controls.Add(Me.passwordTextBox)
            Me.Controls.Add(Me.userTextBox)
            Me.Controls.Add(Me.networkTextBox)
            Me.Controls.Add(Me.portLabel)
            Me.Controls.Add(Me.hostLabel)
            Me.Controls.Add(Me.passwordLabel)
            Me.Controls.Add(Me.userLabel)
            Me.Controls.Add(Me.ftpRadio)
            Me.Controls.Add(Me.memoryRadio)
            Me.Controls.Add(Me.networkLabel)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.isolatedRadio)
            Me.Controls.Add(Me.networkRadio)
            Me.Controls.Add(Me.drivesRadio)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "OpenFolderForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Open a new folder"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        Public Shadows Function ShowDialog(ByVal owner As IWin32Window) As FolderForm
            If MyBase.ShowDialog(owner) = System.Windows.Forms.DialogResult.OK Then
                Dim folders As AbstractFolder() = Nothing

                Dim title As String = String.Empty

                If (drivesRadio.Checked) Then
                    Dim drives As String() = System.IO.Directory.GetLogicalDrives()
                    folders = New AbstractFolder(drives.Length - 1) {}

                    Dim index As Integer
                    For index = 0 To drives.Length - 1
                        Try
                            folders(index) = New DiskFolder(drives(index))
                        Catch
                        End Try
                    Next index

                    title = "My Computer's logical drives"
                ElseIf (networkRadio.Checked) Then
                    Try
                        folders = New AbstractFolder() {New DiskFolder(networkTextBox.Text)}
                    Catch except As Exception
                        MessageBox.Show("The path " + networkTextBox.Text + " could not be found." + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Return Nothing
                    End Try
                    title = "Path: " + networkTextBox.Text

                ElseIf (isolatedRadio.Checked) Then
                    Try
                        folders = New AbstractFolder() {New IsolatedFolder("\")}
                    Catch except As Exception
                        MessageBox.Show("There was an error accessing the isolated storage.\n" + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Return Nothing
                    End Try

                    title = "Isolated storage"
                ElseIf (memoryRadio.Checked) Then
                    folders = New AbstractFolder() {New MemoryFolder}
                    title = "Memory folder: " + folders(0).FullName
                ElseIf (ftpRadio.Checked) Then
                    Try
                        Dim connection As New FtpConnection(hostTextBox.Text, Integer.Parse(portTextBox.Text), userTextBox.Text, passwordTextBox.Text)

                        If (useModeZCheckBox.Checked) Then
                            connection.TransferMode = TransferMode.ZLibCompressed
                        End If

                        folders = New AbstractFolder() {New FtpFolder(connection)}
                    Catch except As Exception
                        MessageBox.Show("There was an error connecting to the FTP server.\n" + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Return Nothing
                    End Try

                    title = "FTP server: " + hostTextBox.Text
                End If

                If folders Is Nothing Then
                    Return Nothing
                End If

                ' As you can see, the same FolderForm type is used to display all these
                ' kinds of AbstractFolder instances.
                Return New FolderForm(title, folders)
            End If

            Return Nothing
        End Function

        Private Sub networkRadio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles networkRadio.CheckedChanged
            networkLabel.Enabled = networkRadio.Checked
            networkTextBox.Enabled = networkRadio.Checked
        End Sub

        Private Sub ftpRadio_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ftpRadio.CheckedChanged
            hostLabel.Enabled = ftpRadio.Checked
            hostTextBox.Enabled = ftpRadio.Checked
            portLabel.Enabled = ftpRadio.Checked
            portTextBox.Enabled = ftpRadio.Checked
            userLabel.Enabled = ftpRadio.Checked
            userTextBox.Enabled = ftpRadio.Checked
            passwordLabel.Enabled = ftpRadio.Checked
            passwordTextBox.Enabled = ftpRadio.Checked
            useModeZCheckBox.Enabled = ftpRadio.Checked
        End Sub

        Private Sub OpenFolderForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ftpRadio.Checked = True
        End Sub
    End Class
End Namespace