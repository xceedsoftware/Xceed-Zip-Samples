' 
' Xceed FTP for .NET - ClientFtp Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [MainForm.vb]
'  
' This application demonstrates how to use the Xceed FTP Object model
' in a generic way.
'  
' This file is part of Xceed Ftp for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Imports System.IO
Imports Xceed.Ftp
Imports Xceed.FileSystem
Imports System.Reflection

Public Class MainForm
    Inherits System.Windows.Forms.Form

#Region "PUBLIC CONSTRUCTORS"

    Public Sub New()
        MyBase.New()

        ' ================================
        ' How to license Xceed components 
        ' ================================       
        ' To license your product, set the LicenseKey property to a valid trial or registered license key 
        ' in the main entry point of the application to ensure components are licensed before any of the 
        ' component methods are called.      
        ' 
        ' If the component is used in a DLL project (no entry point available), it is 
        ' recommended that the LicenseKey property be set in a static constructor of a 
        ' class that will be accessed systematically before any component is instantiated or, 
        ' you can simply set the LicenseKey property immediately BEFORE you instantiate 
        ' an instance of the component.
        ' 
        ' For instance, if you wanted to deploy this sample, the license key needs to be set here.
        ' If your trial period has expired, you must purchase a registered license key,
        ' uncomment the next line of code, and insert your registerd license key.
        ' For more information, consult the "How the 45-day trial works" and the 
        ' "How to license the component once you purchase" topics in the documentation of this product.

        ' Xceed.Ftp.Licenser.LicenseKey = "FTNXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 

        Application.EnableVisualStyles()
        Application.DoEvents()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        m_asyncFtpClient = New AsyncFtpClient()

        ' Since this is a GUI application, we want events to be raised on the main thread.
        ' This property only applies to async method calls.
        m_asyncFtpClient.SynchronizingObject = Me
    End Sub

#End Region

#Region "FtpClient EVENTS"

    Private Sub m_asyncFtpClient_CertificateReceived(ByVal sender As Object, ByVal e As CertificateReceivedEventArgs) Handles m_asyncFtpClient.CertificateReceived

        Dim message As String = String.Empty

        If e.Status = VerificationStatus.ValidCertificate Then
            message = "A valid certificate was received from the server." + vbCrLf + vbCrLf
        Else
            message = "An invalid certificate was received from the server." + vbCrLf _
              + "The error is: " + e.Status.ToString() + vbCrLf + vbCrLf
        End If

        message += e.ServerCertificate.ToString() + vbCrLf + vbCrLf _
          + "Do you want to accept this certificate?"

        Dim answer As DialogResult = MessageBox.Show(
          Me, message, "Server Certificate", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question)

        e.Action = IIf(answer = System.Windows.Forms.DialogResult.Yes, VerificationAction.Accept, VerificationAction.Reject)

    End Sub

    Private Sub m_asyncFtpClient_CommandSent(ByVal sender As Object, ByVal e As Xceed.Ftp.CommandSentEventArgs) Handles m_asyncFtpClient.CommandSent

        ' We want to log every commands sent to the FTP server.

        Me.AddConnectionLogInformation("> " + e.Command)

    End Sub

    Private Sub m_asyncFtpClient_Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_asyncFtpClient.Disconnected

        ' Reset the form to a disconnected state.

        Me.ManageConnectionButtonsMenus()
        Me.ClearRemoteFolderContent()

    End Sub

    Private Sub m_asyncFtpClient_FileTransferStatus(ByVal sender As Object, ByVal e As Xceed.Ftp.FileTransferStatusEventArgs) Handles m_asyncFtpClient.FileTransferStatus

        'Update the progress bar and the speed panel.

        pgbTransfer.Value = e.BytesPercent
        pnlSpeed.Text = Me.FormatSpeed(e.AllBytesPerSecond)

    End Sub

    Private Sub m_asyncFtpClient_MultipleFileTransferError(ByVal sender As Object, ByVal e As Xceed.Ftp.MultipleFileTransferErrorEventArgs) Handles m_asyncFtpClient.MultipleFileTransferError

        ' This event occur when the SendMultipleFiles or ReceiveMultipleFiles is called and
        ' an error occured while transfering a file. We ask the user what to do. (Abort; Ignore; Retry)

        Dim message As String = "An error occured while "

        If (m_asyncFtpClient.State = FtpClientState.ReceivingFile) Then
            If (e.RemoteItemType = FtpItemType.Folder) Then
                message += "receiving the folder '" + e.RemoteItemName + "'."
            Else
                message += "receiving the file '" + e.RemoteItemName + "'."
            End If
        Else
            message += "sending the file '" + e.LocalItemName + "'."
        End If

        message += Environment.NewLine + Environment.NewLine + e.Exception.Message

        Dim result As DialogResult = MessageBox.Show(
          Me,
          message,
          "File transfer error",
          MessageBoxButtons.AbortRetryIgnore,
          MessageBoxIcon.Exclamation)

        Select Case result
            Case System.Windows.Forms.DialogResult.Abort
                e.Action = MultipleFileTransferErrorAction.Abort
                Exit Select

            Case System.Windows.Forms.DialogResult.Ignore
                e.Action = MultipleFileTransferErrorAction.Ignore
                Exit Select

            Case System.Windows.Forms.DialogResult.Retry
                e.Action = MultipleFileTransferErrorAction.Retry
                Exit Select

        End Select

    End Sub

    Private Sub m_asyncFtpClient_ParsingListingLine(ByVal sender As Object, ByVal e As Xceed.Ftp.ParsingListingLineEventArgs) Handles m_asyncFtpClient.ParsingListingLine

        ' If the server returns a . and/or a .. item, we don't want them in the list.
        If (e.Item.Name = "." Or e.Item.Name = "..") Then
            e.Valid = False
        End If

    End Sub

    Private Sub m_asyncFtpClient_ReceivingFile(ByVal sender As Object, ByVal e As Xceed.Ftp.TransferringFileEventArgs) Handles m_asyncFtpClient.ReceivingFile

        pnlProgressInfo.Text = "Receiving : " + e.RemoteFilename
        stbMain.Refresh()

    End Sub

    Private Sub m_asyncFtpClient_ReplyReceived(ByVal sender As Object, ByVal e As Xceed.Ftp.ReplyReceivedEventArgs) Handles m_asyncFtpClient.ReplyReceived

        ' We want to log every reply received from the FTP server.

        Me.AddConnectionLogInformation(e.Reply)

    End Sub

    Private Sub m_asyncFtpClient_SendingFile(ByVal sender As Object, ByVal e As Xceed.Ftp.TransferringFileEventArgs) Handles m_asyncFtpClient.SendingFile

        pnlProgressInfo.Text = "Sending : " + e.LocalFilename
        stbMain.Refresh()

    End Sub

    Private Sub m_asyncFtpClient_StateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles m_asyncFtpClient.StateChanged

        ' Update the status shown in the status bar.

        Me.ShowCurrentConnectionStatus()

    End Sub

#End Region

#Region "MainForm EVENTS"

    Private Sub MainForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        ' If we are still connected to the server, we want to give the user 
        ' a chance to cancel the closing process.

        If (m_asyncFtpClient.Connected) Then
            If (MessageBox.Show(Me, "You are still connected to the server. Do you wish to close the connection and exit the application?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No) Then
                e.Cancel = True
            Else
                ' Disconnect silently
                Try
                    m_asyncFtpClient.BeginDisconnect(New AsyncCallback(AddressOf Me.DisconnectCompleted), Nothing)
                Catch
                End Try
            End If
        End If

        If (Not e.Cancel) Then
            m_asyncFtpClient.TraceWriter.Close()
            m_asyncFtpClient.TraceWriter = Nothing
        End If

    End Sub

    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Initialize the local drives and the form's controls.
        Me.LoadLocalDrives()
        Me.ManageConnectionButtonsMenus()
        Me.ShowCurrentConnectionStatus()

        ' Set the file to save the trace in. Since you must provide an already open
        ' TextWriter, it's also your responsability to close it when done (see MainForm_Closing).
        m_asyncFtpClient.TraceWriter = New StreamWriter(Application.StartupPath + "\ftp.log")

        ' Set the timeout for the FTP Client object to 15 seconds.
        m_asyncFtpClient.Timeout = 15

    End Sub

    Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        ' Put the splitter in the middle of the form.
        pnlRemote.Height = ((pnlRemote.Height + pnlLocal.Height + splMain.Height) / 2) - (splMain.Height / 2)

    End Sub

#End Region

#Region "MENU EVENTS"

    Private Sub MenuOptionRepresentationTypes_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuOptionRepresentationTypeAscii.Click, mnuOptionRepresentationTypeBinary.Click

        Dim clickedMenu As MenuItem = sender
        Dim menuItem As MenuItem

        ' Check/uncheck the menu items.
        For Each menuItem In mnuOptionRepresentationType.MenuItems
            menuItem.Checked = (menuItem.Text = clickedMenu.Text)
        Next menuItem

        ' Set the RepresentationType depending on which menu was clicked.
        If (clickedMenu Is mnuOptionRepresentationTypeAscii) Then
            m_asyncFtpClient.RepresentationType = RepresentationType.Ascii
        ElseIf (clickedMenu Is mnuOptionRepresentationTypeBinary) Then
            m_asyncFtpClient.RepresentationType = RepresentationType.Binary
        End If

    End Sub

    Private Sub MenuOptionSecure_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles mnuOptionSecureNone.Click, mnuOptionSecureExplicit.Click, mnuOptionSecureImplicit.Click

        Dim clickedMenu As MenuItem = sender
        Dim menuItem As MenuItem

        ' Check/uncheck the menu items.
        For Each menuItem In mnuOptionSecure.MenuItems
            menuItem.Checked = (menuItem Is clickedMenu)
        Next

        ' This change affects next connection.

    End Sub

    Private Sub mnuActionAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuActionAbort.Click

        Me.AbortAction()

    End Sub

    Private Sub mnuActionChangeCurrentUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuActionChangeCurrentUser.Click

        Me.ChangeUserAction()

    End Sub

    Private Sub mnuActionConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuActionConnect.Click

        Me.ConnectAction()

    End Sub

    Private Sub mnuActionDisconnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuActionDisconnect.Click

        Me.DisconnectAction()

    End Sub

    Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuFileExit.Click

        Me.Close()

    End Sub

    Private Sub mnuLocalCreateFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLocalCreateFolder.Click

        Me.LocalCreateFolderAction()

    End Sub

    Private Sub mnuLocalDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLocalDelete.Click

        Me.LocalDeleteAction()

    End Sub

    Private Sub mnuLocalRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLocalRename.Click

        Me.LocalRenameAction()

    End Sub

    Private Sub mnuLocalTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuLocalTransfer.Click

        Me.LocalTransferAction()

    End Sub

    Private Sub mnuOptionPassiveTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOptionPassiveTransfer.Click

        ' Set the FTP client to passive mode or not.
        mnuOptionPassiveTransfer.Checked = Not mnuOptionPassiveTransfer.Checked
        m_asyncFtpClient.PassiveTransfer = mnuOptionPassiveTransfer.Checked

    End Sub

    Private Sub mnuOptionPreAllocateStorage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuOptionPreAllocateStorage.Click

        ' Set the FTP client to pre-allocate the storage or not.
        mnuOptionPreAllocateStorage.Checked = Not mnuOptionPreAllocateStorage.Checked
        m_asyncFtpClient.PreAllocateStorage = mnuOptionPreAllocateStorage.Checked

    End Sub

    Private Sub mnuOptionModeZ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptionModeZ.Click
        mnuOptionModeZ.Checked = Not mnuOptionModeZ.Checked

        If m_asyncFtpClient.Connected Then
            Dim transferMode As TransferMode

            If mnuOptionModeZ.Checked Then
                transferMode = Ftp.TransferMode.ZLibCompressed
            End If

            Dim result As IAsyncResult

            result = m_asyncFtpClient.BeginChangeTransferMode(
                transferMode,
                Nothing,
                Nothing)

            While Not result.IsCompleted
                Application.DoEvents()
            End While

            m_asyncFtpClient.EndChangeTransferMode(result)
        End If
    End Sub

    Private Sub mnuRemoteCreateFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoteCreateFolder.Click

        Me.CreateRemoteFolder()

    End Sub

    Private Sub mnuRemoteDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoteDelete.Click

        Me.DeleteRemoteSelectedItems()

    End Sub

    Private Sub mnuRemoteRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoteRename.Click

        Me.RenameRemoteSelectedItems()

    End Sub

    Private Sub mnuRemoteTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRemoteTransfer.Click

        Me.TransferRemoteSelectedItems()

    End Sub

#End Region

#Region "OTHER EVENTS"

    Private Sub tlbMain_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbMain.ButtonClick

        If (e.Button Is btnConnect) Then
            Me.ConnectAction()
        ElseIf (e.Button Is btnDisconnect) Then
            Me.DisconnectAction()
        ElseIf (e.Button Is btnChangeUser) Then
            Me.ChangeUserAction()
        ElseIf (e.Button Is btnAbort) Then
            Me.AbortAction()
        End If

    End Sub

    Private Sub lsvRemoteFiles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lsvRemoteFiles.DoubleClick

        ' Do the appropriate action depending on the type of the clicked item.

        If (lsvRemoteFiles.SelectedItems.Count > 0) Then
            Dim item As RemoteListViewItem = lsvRemoteFiles.SelectedItems(0)

            Select Case item.Info.Type
                Case FtpItemType.File
                    ' Transfer the file.
                    Me.TransferRemoteSelectedItems()

                Case FtpItemType.Folder
                    ' Open the folder.
                    Me.SelectRemoteFolderNode(item.Info)

                Case FtpItemType.Link
                    ' Open the link (which is the same as a folder).
                    Me.SelectRemoteFolderNode(item.Info)

                Case FtpItemType.Unknown
                    ' We consider the unknown type as a file.
                    Me.TransferRemoteSelectedItems()

            End Select
        End If

    End Sub

    Private Sub tlbRemoteFolderContent_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbRemoteFolderContent.ButtonClick

        If (e.Button Is btnRemoteDelete) Then
            Me.DeleteRemoteSelectedItems()
        ElseIf (e.Button Is btnRemoteCreateFolder) Then
            Me.CreateRemoteFolder()
        ElseIf (e.Button Is btnRemoteRename) Then
            Me.RenameRemoteSelectedItems()
        ElseIf (e.Button Is btnRemoteTransfer) Then
            Me.TransferRemoteSelectedItems()
        End If

    End Sub

    Private Sub trvRemoteDir_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvRemoteDir.AfterSelect

        Me.LoadRemoteFolderContent(e.Node, False)

    End Sub

    Private Sub trvRemoteDir_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvRemoteDir.BeforeExpand

        If Not m_preventUpdate Then
            ' We update this folder without filling the listview.
            Me.UpdateRemoteFolder(e.Node)
        End If

    End Sub

    Private Sub lsvLocalFiles_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lsvLocalFiles.DoubleClick

        ' Do the appropriate action depending on the type of the clicked item.

        If (lsvLocalFiles.SelectedItems.Count > 0) Then
            Dim item As LocalListViewItem = lsvLocalFiles.SelectedItems(0)

            If TypeOf item.Item Is AbstractFolder Then
                ' Open the folder.
                Me.SelectLocalFolderNode(item.Item)
            Else
                ' Transfer the file.
                Me.LocalTransferAction()
            End If

        End If

    End Sub

    Private Sub tlbLocalFolderContent_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbLocalFolderContent.ButtonClick

        If (e.Button Is btnLocalDelete) Then
            Me.LocalDeleteAction()
        ElseIf (e.Button Is btnLocalCreateFolder) Then
            Me.LocalCreateFolderAction()
        ElseIf (e.Button Is btnLocalRename) Then
            Me.LocalRenameAction()
        ElseIf (e.Button Is btnLocalTransfer) Then
            Me.LocalTransferAction()
        End If

    End Sub

    Private Sub trvLocalDir_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvLocalDir.AfterSelect

        Me.LoadLocalFolderContent(e.Node)

    End Sub

    Private Sub trvLocalDir_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvLocalDir.BeforeExpand

        Me.UpdateLocalFolder(e.Node)

    End Sub

#End Region

#Region "GENERAL METHODS"

    Private Sub AbortAction()

        ' Tell the FTP server we wish to abort the current operation. It is safe
        ' and acceptable to call Abort/BeginAbort even if no operation is in progress.

        Try
            m_asyncFtpClient.BeginAbort(New AsyncCallback(AddressOf Me.AbortCompleted), Nothing)
        Catch ex As Exception
            MessageBox.Show(
              Me,
              "An error occured while trying to abort the current operation." _
              + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message,
              "Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AbortCompleted(ByVal result As IAsyncResult)

        Try
            m_asyncFtpClient.EndAbort(result)
        Catch
        End Try

    End Sub

    Private Sub AddConnectionLogInformation(ByVal reply As FtpReply)

        ' Add each reply line to the connection information log.
        Dim line As String

        For Each line In reply.Lines
            Me.AddConnectionLogInformation("< " + line)
        Next line

    End Sub

    Private Sub AddConnectionLogInformation(ByVal info As String)

        ' Add an entry in the connection information log control and scroll to the end of the text.
        txtConnectionLogInformation.AppendText(info.Trim() + System.Environment.NewLine)
        txtConnectionLogInformation.SelectionStart = txtConnectionLogInformation.Text.Length
        txtConnectionLogInformation.ScrollToCaret()

    End Sub

    Private Sub Authenticate()

        ' Secure the connection explicitly

        Try
            m_asyncFtpClient.BeginAuthenticate(
              AuthenticationMethod.TlsAuto, VerificationFlags.None, Nothing, DataChannelProtection.Private,
              New AsyncCallback(AddressOf Me.AuthenticateCompleted), Nothing)
        Catch ex As Exception
            MessageBox.Show(
              Me, "An error occured while authenticating." +
              Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message,
              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try

    End Sub

    Private Sub AuthenticateCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndAuthenticate(asyncResult)
            pnlSecure.Width = 25

            If m_asyncFtpClient.ActiveSecurityOptions.AuthenticationMethod <> AuthenticationMethod.None Then
                Me.AddConnectionLogInformation("- " + m_asyncFtpClient.ActiveSecurityOptions.AuthenticationMethod.ToString())
                Me.AddConnectionLogInformation("- Hash : " + m_asyncFtpClient.ActiveSecurityOptions.HashStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.HashAlgorithm.ToString())
                Me.AddConnectionLogInformation("- Key Exchange : " + m_asyncFtpClient.ActiveSecurityOptions.KeyExchangeStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.KeyExchangeAlgorithm.ToString())
                Me.AddConnectionLogInformation("- Session Cipher : " + m_asyncFtpClient.ActiveSecurityOptions.CipherStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.CipherAlgorithm.ToString())
            End If

            Me.Login()
        Catch ex As Exception
            MessageBox.Show(
              Me, "An error occured while authenticating." +
              Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message,
              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try

    End Sub

    Private Sub ClearConnectionLogInformation()

        txtConnectionLogInformation.Clear()

    End Sub

    Private Sub ChangeUser()

        ' Change the currently logged user.

        Try
            Me.ClearRemoteFolderContent()

            m_asyncFtpClient.BeginChangeUser(
              m_userName,
              m_password,
              New AsyncCallback(AddressOf Me.ChangeUserCompleted),
              Nothing)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to change the user." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try
    End Sub

    Private Sub ChangeUserCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndChangeUser(asyncResult)

            Me.AddRemoteRootTreeviewNode()
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to change the user." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try

    End Sub

    Private Sub ChangeUserAction()

        ' Change the currently logged user. This method is used by the menu or the toolbar.

        Cursor = Cursors.WaitCursor

        Dim connectionInformation As New ConnectionInformation()
        Dim result As DialogResult = connectionInformation.ShowDialog(
          Me, m_hostAddress, m_hostPort, m_anonymousConnection, m_userName, m_password,
          m_proxyAddress, m_proxyPort, m_proxyUserName, m_proxyPassword, True)

        connectionInformation = Nothing

        If (result = System.Windows.Forms.DialogResult.OK) Then
            Me.ChangeUser()
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub Connect()

        ' Connect the FTP client to the server.
        Try
            If m_proxyAddress.Length = 0 Then

                m_asyncFtpClient.Proxy = Nothing

            Else

                m_asyncFtpClient.Proxy = New HttpProxyClient(m_proxyAddress, m_proxyPort, m_proxyUserName, m_proxyPassword)

            End If

            Me.ClearConnectionLogInformation()

            If mnuOptionSecureImplicit.Checked Then
                m_asyncFtpClient.BeginConnect(
                  m_hostAddress, m_hostPort,
                  AuthenticationMethod.TlsAuto, VerificationFlags.None, Nothing,
                  New AsyncCallback(AddressOf Me.ConnectCompleted), Nothing)
            Else
                m_asyncFtpClient.BeginConnect(
                  m_hostAddress, m_hostPort,
                  New AsyncCallback(AddressOf Me.ConnectCompleted), Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to connect to host." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ConnectCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndConnect(asyncResult)

            If mnuOptionSecureExplicit.Checked Then
                Me.Authenticate()
            Else
                Me.Login()
            End If
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to connect to host." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConnectAction()

        ' Ask the user the server's information and establish a connection to the server. 
        ' This method is used by the menu or the toolbar.

        Cursor = Cursors.WaitCursor

        Dim connectionInformation As ConnectionInformation = New ConnectionInformation()
        Dim result As DialogResult = connectionInformation.ShowDialog(
          Me, m_hostAddress, m_hostPort, m_anonymousConnection, m_userName, m_password,
          m_proxyAddress, m_proxyPort, m_proxyUserName, m_proxyPassword)

        If (result = System.Windows.Forms.DialogResult.OK) Then
            Me.Connect()
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub DisconnectAction()

        ' Disconnect the FTP client from the server.

        Try
            If m_asyncFtpClient.Connected Then
                m_asyncFtpClient.BeginDisconnect(New AsyncCallback(AddressOf Me.DisconnectCompleted), Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to disconnect from host." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DisconnectCompleted(ByVal asyncResult As IAsyncResult)

        ' Complete the disconnection. We reset our main form in OnDisconnected, since
        ' disconnection can also occur without a call to Disconnect.

        Try
            m_asyncFtpClient.EndDisconnect(asyncResult)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to disconnect from host." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function FormatSpeed(ByVal bytesPerSecond As Long) As String

        ' Format the received speed to a readable format.

        Dim speed As Double = bytesPerSecond

        ' Bytes per second.
        Dim formattedSpeed As String = speed.ToString("n2") + " bytes/Sec"

        ' KB per second. (only if at least 1 kb/sec)
        If (speed > 1024) Then
            speed /= 1024
            formattedSpeed = speed.ToString("n2") + " KB/Sec"

            ' MB per second. (only if at least 1 mb/sec)
            If (speed > 1024) Then
                speed /= 1024
                formattedSpeed = speed.ToString("n2") + " MB/Sec"
            End If
        End If

        Return formattedSpeed

    End Function

    Private Function GetConnectionStatusText() As String

        ' Returns a formatted description of the current state of the FTP client.

        Select Case m_asyncFtpClient.State
            Case FtpClientState.ChangingCurrentFolder
                Return "Changing current folder"

            Case FtpClientState.ChangingToParentFolder
                Return "Changing to parent folder"

            Case FtpClientState.ChangingUser
                Return "Changing user"

            Case FtpClientState.Connected
                Return "Connected" + " (" + m_asyncFtpClient.ServerAddress.ToString() + ")"

            Case FtpClientState.Connecting
                Return "Connecting"

            Case FtpClientState.CreatingFolder
                Return "Creating folder"

            Case FtpClientState.DeletingFile
                Return "Deleting file"

            Case FtpClientState.DeletingFolder
                Return "Changing folder"

            Case FtpClientState.Disconnecting
                Return "Disconnecting"

            Case FtpClientState.GettingCurrentFolder
                Return "Getting current folder"

            Case FtpClientState.GettingFolderContents
                Return "Getting folder contents"

            Case FtpClientState.NotConnected
                Return "Not connected"

            Case FtpClientState.ReceivingFile
                Return "Receiving file"

            Case FtpClientState.ReceivingMultipleFiles
                Return "Receiving multiple files"

            Case FtpClientState.RenamingFile
                Return "Renaming a file or folder"

            Case FtpClientState.SendingCustomCommand
                Return "Sending custom command"

            Case FtpClientState.SendingFile
                Return "Sending file"

            Case FtpClientState.SendingMultipleFiles
                Return "Sending multiple files"

            Case Else
                Return ""

        End Select

    End Function


    Private Sub Login()

        ' Initialize the login for a user.

        Try
            m_asyncFtpClient.BeginLogin(
              m_userName,
              m_password,
              New AsyncCallback(AddressOf Me.LoginCompleted),
              Nothing)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to login." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try

    End Sub

    Private Sub LoginCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndLogin(asyncResult)

            ChangeTranserMode()

        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to login." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.DisconnectAction()
        End Try

    End Sub

    Private Sub ChangeTranserMode()
        Try
            Dim transferMode As TransferMode

            If mnuOptionModeZ.Checked Then
                transferMode = Ftp.TransferMode.ZLibCompressed
            End If

            m_asyncFtpClient.BeginChangeTransferMode(
                transferMode,
                New AsyncCallback(AddressOf Me.ChangeTranserModeCompleted),
                Nothing)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to change transfer mode.\n\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DisconnectAction()
        End Try
    End Sub

    Private Sub ChangeTranserModeCompleted(ByVal asyncResult As IAsyncResult)
        Try
            m_asyncFtpClient.EndChangeTransferMode(asyncResult)

            Me.ManageConnectionButtonsMenus()
            Me.AddRemoteRootTreeviewNode()
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while trying to change transfer mode.\n\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DisconnectAction()
        End Try
    End Sub

    Private Sub ManageConnectionButtonsMenus()

        ' Enable or disable controls depending if the FTP client is connected or not.

        btnConnect.Enabled = Not m_asyncFtpClient.Connected
        btnDisconnect.Enabled = m_asyncFtpClient.Connected
        btnChangeUser.Enabled = m_asyncFtpClient.Connected
        btnAbort.Enabled = m_asyncFtpClient.Connected

        mnuActionConnect.Enabled = Not m_asyncFtpClient.Connected
        mnuActionDisconnect.Enabled = m_asyncFtpClient.Connected
        mnuActionChangeCurrentUser.Enabled = m_asyncFtpClient.Connected
        mnuActionAbort.Enabled = m_asyncFtpClient.Connected
        mnuOptionSecure.Enabled = Not m_asyncFtpClient.Connected

        If Not m_asyncFtpClient.Connected Then
            pnlSecure.Width = 0
        End If

    End Sub

    Private Sub ShowCurrentConnectionStatus()

        ' Update the connection status information panel.

        Try
            pnlConnectionStatus.Text = Me.GetConnectionStatusText()
        Catch
        End Try

    End Sub

#End Region

#Region "REMOTE-SIDE METHODS"

    Private Sub AddRemoteRootTreeviewNode()

        ' Clear the actual list.
        trvRemoteDir.Nodes.Clear()

        Dim node As New RemoteFolderTreeNode(m_asyncFtpClient)
        trvRemoteDir.Nodes.Add(node)

        ' Select the root node.
        trvRemoteDir.SelectedNode = node

    End Sub

    Private Sub ClearRemoteFolderContent()

        ' Clear the treeview and the listview of the host.

        trvRemoteDir.Nodes.Clear()
        lsvRemoteFiles.Items.Clear()

    End Sub


    Private Sub CreateRemoteFolder()

        ' Create a folder in the current folder of the host.

        Dim ftpItemName As New FtpItemName()

        Try
            Dim newFolderName As String = String.Empty

            If ftpItemName.ShowDialog(Me, "Enter the name of the new folder", newFolderName) = System.Windows.Forms.DialogResult.OK Then
                Try
                    m_asyncFtpClient.BeginCreateFolder(
                      newFolderName,
                      New AsyncCallback(AddressOf Me.CreateFolderCompleted),
                      Nothing)
                Catch ex As Exception
                    MessageBox.Show(Me, "An error occured while creating a folder." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Finally
            ftpItemName.Dispose()
        End Try

    End Sub

    Private Sub CreateFolderCompleted(ByVal result As IAsyncResult)

        Try
            m_asyncFtpClient.EndCreateFolder(result)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while creating a folder." + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DeleteRemoteSelectedItems()

        ' Deletes all the selected items from the host.

        If lsvRemoteFiles.SelectedItems.Count > 0 Then

            ' Build the confirmation message.
            Dim confirmMsg As String = String.Empty
            Dim confirmCaption As String = String.Empty

            If lsvRemoteFiles.SelectedItems.Count > 1 Then
                confirmMsg = "Are you sure you want to delete these " + lsvRemoteFiles.SelectedItems.Count.ToString() + " items?" + Environment.NewLine + "(Deleting a folder will delete all of its contents)"
                confirmCaption = "Confirm Multiple File Delete"
            Else
                Dim selectedItem As RemoteListViewItem = lsvRemoteFiles.SelectedItems(0)

                If (selectedItem.Info.Type = FtpItemType.Folder) Or (selectedItem.Info.Type = FtpItemType.Link) Then
                    confirmMsg = "Are you sure you want to delete the folder '" + selectedItem.Info.Name + "'?" + Environment.NewLine + "(Deleting a folder will delete all of its contents)"
                    confirmCaption = "Confirm Folder Delete"
                Else
                    confirmMsg = "Are you sure you want to delete the file '" + selectedItem.Info.Name + "'?"
                    confirmCaption = "Confirm File Delete"
                End If
            End If

            ' Confirm the deletion.
            If MessageBox.Show(Me, confirmMsg, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                pnlLocal.Enabled = False
                pnlRemote.Enabled = False

                ' Delete each selected item.
                Me.DeleteRemoteSelectedItem(0)
            End If
        End If

    End Sub

    Private Sub DeleteRemoteSelectedItem(ByVal index As Integer)

        If index >= lsvRemoteFiles.SelectedItems.Count Then
            ' We completed.
            Me.RemoteChangesCompleted()
        Else
            Dim item As RemoteListViewItem = lsvRemoteFiles.SelectedItems(index)

            Try
                If item.Info.Type = FtpItemType.Folder Or item.Info.Type = FtpItemType.Link Then
                    m_asyncFtpClient.BeginDeleteFolder(item.Info.Name, True, New AsyncCallback(AddressOf Me.DeleteFolderCompleted), index)
                Else
                    m_asyncFtpClient.BeginDeleteFile(item.Info.Name, New AsyncCallback(AddressOf Me.DeleteFileCompleted), index)
                End If
            Catch except As Exception
                MessageBox.Show(Me, "An error occured while deleting '" + item.Info.Name + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Me.RemoteChangesCompleted()
            End Try
        End If

    End Sub

    Private Sub DeleteFolderCompleted(ByVal result As IAsyncResult)

        Try
            m_asyncFtpClient.EndDeleteFolder(result)

            Dim index As Integer = CInt(result.AsyncState)
            Me.DeleteRemoteSelectedItem(index + 1)
        Catch except As Exception
            MessageBox.Show(Me, "An error occured while deleting a folder" + Environment.NewLine + Environment.NewLine + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try

    End Sub

    Private Sub DeleteFileCompleted(ByVal result As IAsyncResult)

        Try
            m_asyncFtpClient.EndDeleteFile(result)

            Dim index As Integer = CInt(result.AsyncState)
            Me.DeleteRemoteSelectedItem(index + 1)
        Catch except As Exception
            ' We could try deleting that name as a folder.
            MessageBox.Show(Me, "An error occured while deleting a file" + Environment.NewLine + Environment.NewLine + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try

    End Sub

    Private Sub UpdateRemoteFolder(ByVal selectedNode As RemoteFolderTreeNode)

        Try
            selectedNode.Refresh()
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while getting the contents of folder '" + selectedNode.Text + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LoadRemoteFolderContent(ByVal selectedNode As RemoteFolderTreeNode, ByVal refresh As Boolean)

        Try
            selectedNode.SelectFolder(lsvRemoteFiles, refresh)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while getting the contents of folder '" + selectedNode.Text + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ReceiveFile(ByVal itemInfo As FtpItemInfo, ByVal index As Integer)

        ' Receive a file from the host.

        Try
            Dim selectedNode As LocalFolderTreeNode = trvLocalDir.SelectedNode

            m_asyncFtpClient.BeginReceiveFile(itemInfo.Name, selectedNode.Folder.FullName + itemInfo.Name, New AsyncCallback(AddressOf Me.ReceiveFileCompleted), index)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while receiving the file '" + itemInfo.Name + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.LocalChangesCompleted()
        End Try

    End Sub

    Private Sub ReceiveFileCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndReceiveFile(asyncResult)

            Dim index As Integer = CInt(asyncResult.AsyncState)
            Me.TransferRemoteSelectedItem(index + 1)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.LocalChangesCompleted()
        End Try

    End Sub



    Private Sub ReceiveMultipleFiles(ByVal itemInfo As FtpItemInfo, ByVal index As Integer)

        ' Receive a folder from the host. The ReceiveMultipleFiles can also be used 
        ' to receive files matching a specified mask but we use it only to receive 
        ' a folder (and it's subdirectories).

        ' Notice that we don't use an "*" in the filemask, but simply make sure
        ' the path ends with a folder separator. That's because some FTP servers
        ' consider the "LIST *" command as a request to see a recursive listing of
        ' everything. Since we only want "everything in the current folder", an 
        ' empty mask is the best approach.
        Try
            Dim selectedNode As LocalFolderTreeNode = trvLocalDir.SelectedNode

            m_asyncFtpClient.BeginReceiveMultipleFiles(
              itemInfo.Name + Path.DirectorySeparatorChar,
              selectedNode.Folder.FullName,
              True,
              True,
              New AsyncCallback(AddressOf Me.ReceiveMultipleFilesCompleted),
              index)
        Catch ex As Exception
            MessageBox.Show(Me, "An error occured while receiving the folder '" + itemInfo.Name + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.LocalChangesCompleted()
        End Try

    End Sub

    Private Sub ReceiveMultipleFilesCompleted(ByVal asyncResult As IAsyncResult)
        Try
            m_asyncFtpClient.EndReceiveMultipleFiles(asyncResult)

            Dim index As Integer = CInt(asyncResult.AsyncState)
            Me.TransferRemoteSelectedItem(index + 1)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.LocalChangesCompleted()
        End Try

    End Sub


    Private Sub RemoteChangesCompleted()

        ' Enable panels
        pnlLocal.Enabled = True
        pnlRemote.Enabled = True

        ' Refresh selected node
        Me.LoadRemoteFolderContent(trvRemoteDir.SelectedNode, True)

        ' Reset the progress bar.
        pgbTransfer.Value = 0
        pnlProgressInfo.Text = ""
        pnlSpeed.Text = ""

    End Sub

    Private Sub RenameRemoteSelectedItems()

        ' Loop through all the selected items and for each, ask for a new name.

        If lsvRemoteFiles.SelectedItems.Count > 0 Then
            pnlLocal.Enabled = False
            pnlRemote.Enabled = False

            Me.RenameRemoteSelectedItem(0)
        End If
    End Sub

    Private Sub RenameRemoteSelectedItem(ByVal index As Integer)

        If index >= lsvRemoteFiles.SelectedItems.Count Then
            ' We completed renaming
            Me.RemoteChangesCompleted()
        Else
            Dim item As RemoteListViewItem = lsvRemoteFiles.SelectedItems(index)
            Dim ftpItemName As New FtpItemName()

            Try
                Dim newItemName As String = item.Info.Name
                Dim formCaption As String = "Enter new name for the "

                Select Case item.Info.Type
                    Case FtpItemType.File
                        formCaption += "file"

                    Case FtpItemType.Folder
                        formCaption += "folder"

                    Case FtpItemType.Link
                        formCaption += "link"

                    Case FtpItemType.Unknown
                        formCaption += "unknown item"
                End Select

                If ftpItemName.ShowDialog(Me, formCaption, newItemName) = System.Windows.Forms.DialogResult.OK Then
                    Try
                        m_asyncFtpClient.BeginRenameFile(
                          item.Info.Name,
                          newItemName,
                          New AsyncCallback(AddressOf Me.RenameFileCompleted),
                          index)
                    Catch except As Exception
                        MessageBox.Show(Me, "An error occured while renaming '" + item.Info.Name + "'" + Environment.NewLine + Environment.NewLine + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                        Me.RemoteChangesCompleted()
                    End Try
                End If
            Finally
                ftpItemName.Dispose()
            End Try
        End If
    End Sub

    Private Sub RenameFileCompleted(ByVal result As IAsyncResult)

        Try
            m_asyncFtpClient.EndRenameFile(result)

            Dim index As Integer = CInt(result.AsyncState)
            Me.RenameRemoteSelectedItem((index + 1))
        Catch except As Exception
            MessageBox.Show(Me, "An error occured while renaming a file" + Environment.NewLine + Environment.NewLine + Environment.NewLine + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try
    End Sub

    Private Sub SelectRemoteFolderNode(ByVal itemInfo As FtpItemInfo)

        ' We find the corresponding folder in the treeview and simulate a click that will
        ' load the content of the folder.

        If Not trvRemoteDir.SelectedNode.IsExpanded Then
            ' We need to expand the node before selecting a child node. But we
            ' don't want to generate another update.
            m_preventUpdate = True
            trvRemoteDir.SelectedNode.Expand()
            m_preventUpdate = False
        End If

        Dim node As RemoteFolderTreeNode

        For Each node In trvRemoteDir.SelectedNode.Nodes
            If node.Text = itemInfo.Name Then
                trvRemoteDir.SelectedNode = node
                Return
            End If
        Next node

    End Sub


    Private Sub TransferRemoteSelectedItems()

        ' We start with the first selected item and we will transfer each item
        ' asynchroneously. But we don't want the remote nor local selections and
        ' current folders to change, so we disable the two panels.

        If lsvRemoteFiles.SelectedItems.Count > 0 Then
            pnlLocal.Enabled = False
            pnlRemote.Enabled = False

            Me.TransferRemoteSelectedItem(0)
        End If

    End Sub

    Private Sub TransferRemoteSelectedItem(ByVal index As Integer)

        If index >= lsvRemoteFiles.SelectedItems.Count Then
            Me.LocalChangesCompleted()
        Else
            ' For each selected remote items, transfer it by calling the appropriate method.
            Dim item As RemoteListViewItem = lsvRemoteFiles.SelectedItems(index)

            Select Case item.Info.Type
                Case FtpItemType.File
                    Me.ReceiveFile(item.Info, index)

                Case FtpItemType.Folder
                    Me.ReceiveMultipleFiles(item.Info, index)

                Case FtpItemType.Link
                    ' Links can either point to files or folders.
                    ' In this sample, we only support links to folders.
                    Me.ReceiveMultipleFiles(item.Info, index)

                Case FtpItemType.Unknown
                    Me.ReceiveFile(item.Info, index)
            End Select
        End If

    End Sub

#End Region

#Region "LOCAL-SIDE METHODS"

    Private Function CreateLocalFolder() As Boolean

        ' Create a folder on the local drive. (Depending on which folder
        ' is currently selected in the treeview)

        Dim ftpItemName As New FtpItemName()

        Dim newFolderName As String = String.Empty

        If ftpItemName.ShowDialog(Me, "Enter the name of the new folder", newFolderName) = System.Windows.Forms.DialogResult.OK Then
            Try
                Dim selectedNode As LocalFolderTreeNode = trvLocalDir.SelectedNode

                selectedNode.Folder.CreateFolder(newFolderName)

                Return True
            Catch ex As Exception
                MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        Else
            Return False
        End If

    End Function

    Private Function DeleteLocalSelectedItems() As Boolean

        ' Delete the selected local files and folders. 
        Dim deleted As Boolean = False

        If lsvLocalFiles.SelectedItems.Count = 0 Then
            Return False
        End If

        ' Build the confirmation message.
        Dim confirmMsg As String = String.Empty
        Dim confirmCaption As String = String.Empty

        If lsvLocalFiles.SelectedItems.Count > 1 Then
            confirmMsg = "Are you sure you want to delete these " + lsvLocalFiles.SelectedItems.Count.ToString() + " items?" + ControlChars.Lf + "(Deleting a folder will delete all of its contents)"
            confirmCaption = "Confirm Multiple File Delete"
        Else
            Dim selectedItem As LocalListViewItem = lsvLocalFiles.SelectedItems(0)

            If TypeOf selectedItem.Item Is AbstractFolder Then
                confirmMsg = "Are you sure you want to delete the folder '" + selectedItem.Item.Name + "'?" + Environment.NewLine + "(Deleting a folder will delete all of its contents)"
                confirmCaption = "Confirm Folder Delete"
            Else
                confirmMsg = "Are you sure you want to delete the file '" + selectedItem.Item.Name + "'?"
                confirmCaption = "Confirm File Delete"
            End If
        End If

        If MessageBox.Show(Me, confirmMsg, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            ' Delete each selected items.
            Dim item As LocalListViewItem

            For Each item In lsvLocalFiles.SelectedItems
                Try
                    item.Item.Delete()
                    deleted = True
                Catch ex As Exception
                    MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            Next item
        End If

        Return deleted

    End Function

    Private Sub LoadLocalDrives()

        ' Clear the actual content.
        trvLocalDir.Nodes.Clear()

        ' Get a list of all the local drive on the system.
        Dim drives As String() = System.IO.Directory.GetLogicalDrives()
        Dim drive As String

        For Each drive In drives
            Dim node As New LocalFolderTreeNode(New DiskFolder(drive))
            trvLocalDir.Nodes.Add(node)

            ' Select the C drive by default.
            If drive = "C:\" Then
                trvLocalDir.SelectedNode = node
            End If
        Next drive

    End Sub

    Private Sub UpdateLocalFolder(ByVal selectedNode As LocalFolderTreeNode)

        Try
            selectedNode.UpdateContents()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub LoadLocalFolderContent(ByVal selectedNode As LocalFolderTreeNode)

        Try
            selectedNode.FillList(lsvLocalFiles)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub


    Private Sub LocalChangesCompleted()

        ' Enable panels back
        pnlLocal.Enabled = True
        pnlRemote.Enabled = True

        ' Refresh the local folder content.
        Me.LoadLocalFolderContent(trvLocalDir.SelectedNode)

        ' Reset the progress bar.
        pgbTransfer.Value = 0
        pnlProgressInfo.Text = ""
        pnlSpeed.Text = ""

    End Sub

    Private Sub LocalCreateFolderAction()

        ' Create a folder on the local disk. This method is intended to be used
        ' by the menu or the toolbar.

        Cursor = Cursors.WaitCursor

        If (Me.CreateLocalFolder()) Then
            ' Reload the current file list and the subfolders in the treeview.
            Me.UpdateLocalFolder(trvLocalDir.SelectedNode)
            Me.LoadLocalFolderContent(trvLocalDir.SelectedNode)
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub LocalDeleteAction()

        ' Delete the selected items from the local disk. This method 
        ' is intended to be used by the menu or the toolbar.

        Cursor = Cursors.WaitCursor

        If (Me.DeleteLocalSelectedItems()) Then
            ' Reload the current file list and the subfolders in the treeview.
            Me.UpdateLocalFolder(trvLocalDir.SelectedNode)
            Me.LoadLocalFolderContent(trvLocalDir.SelectedNode)
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub LocalRenameAction()

        ' Rename the selected items from the local disk. This method 
        ' is intended to be used by the menu or the toolbar.

        Cursor = Cursors.WaitCursor

        If (Me.RenameLocalSelectedItems()) Then
            ' Reload the current file list and the subfolders in the treeview.
            Me.UpdateLocalFolder(trvLocalDir.SelectedNode)
            Me.LoadLocalFolderContent(trvLocalDir.SelectedNode)
        End If

        Cursor = Cursors.Default

    End Sub

    Private Sub LocalTransferAction()

        ' Transfer the selected items from the local disk. This method 
        ' is intended to be used by the menu or the toolbar.

        Me.TransferLocalSelectedItems()

    End Sub

    Private Function RenameLocalSelectedItems() As Boolean

        ' Rename all the selected items and for each, ask the user for a new name.

        Dim renamed As Boolean = False

        If lsvLocalFiles.SelectedItems.Count = 0 Then
            Return False
        End If

        Dim item As LocalListViewItem

        For Each item In lsvLocalFiles.SelectedItems
            Dim ftpItemName As New FtpItemName()

            Try
                Dim newItemName As String = item.Item.Name
                Dim formCaption As String = "Enter new name for the "

                If TypeOf item.Item Is AbstractFolder Then
                    formCaption += "folder"
                Else
                    formCaption += "file"
                End If

                If ftpItemName.ShowDialog(Me, formCaption, newItemName) = System.Windows.Forms.DialogResult.OK Then
                    Try
                        item.Item.Name = newItemName
                        renamed = True
                    Catch ex As Exception
                        MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                End If
            Finally
                ftpItemName.Dispose()
            End Try
        Next item

        Return renamed

    End Function

    Private Sub SelectLocalFolderNode(ByVal folder As AbstractFolder)

        ' We find the corresponding folder in the treeview and simulate a click that will
        ' load the content of the folder.

        If Not trvLocalDir.SelectedNode.IsExpanded Then
            ' We need to expand the node before selecting a child node.
            trvLocalDir.SelectedNode.Expand()
        End If

        Dim node As LocalFolderTreeNode

        For Each node In trvLocalDir.SelectedNode.Nodes
            If node.Folder.Name = folder.Name Then
                trvLocalDir.SelectedNode = node
                Return
            End If
        Next node

    End Sub

    Private Sub SendFile(ByVal file As AbstractFile, ByVal index As Integer)

        ' Send a file to the host's current folder.

        Try
            m_asyncFtpClient.BeginSendFile(
              file.FullName,
              file.Name,
              False,
              New AsyncCallback(AddressOf Me.SendFileCompleted),
              index)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try

    End Sub

    Private Sub SendFileCompleted(ByVal asyncResult As IAsyncResult)

        Try
            m_asyncFtpClient.EndSendFile(asyncResult)

            Dim index As Integer = CInt(asyncResult.AsyncState)
            Me.TransferLocalSelectedItem(index + 1)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try

    End Sub


    Private Sub SendMultipleFiles(ByVal folder As AbstractFolder, ByVal index As Integer)

        ' Send a folder to the host's current folder. The SendMultipleFiles can also be used to send
        ' files matching a specified mask but we use it only to send a folder. (and it's subdirectories)

        Try
            ' SendMultipleFiles transfers the contents of the source folder, but does
            ' not create that folder remotely. It creates subfolders only, assuming the 
            ' current remote working folder matches this local folder. Since we want to 
            ' replicate this folder, we first create it remotely, then copy the local 
            ' folder's contents.

            m_asyncFtpClient.BeginCreateFolder(
              folder.Name,
              New AsyncCallback(AddressOf Me.SendMultipleFilesCreateFolderCompleted),
              New Object() {folder, index})
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try

    End Sub

    Private Sub SendMultipleFilesCreateFolderCompleted(ByVal result As IAsyncResult)

        Try
            ' If we can't create that folder, we assume it's because it already exists.
            ' We continue with the "ChangeCurrentFolder". If this one fails, we'll error out.

            m_asyncFtpClient.EndCreateFolder(result)
        Catch
        Finally
            Dim folder As AbstractFolder = CType(CType(result.AsyncState, Object())(0), AbstractFolder)

            m_asyncFtpClient.BeginChangeCurrentFolder(
              folder.Name,
              New AsyncCallback(AddressOf Me.SendMultipleFilesChangeFolderCompleted),
              result.AsyncState)
        End Try

    End Sub

    Private Sub SendMultipleFilesChangeFolderCompleted(ByVal result As IAsyncResult)
        Try
            m_asyncFtpClient.EndChangeCurrentFolder(result)

            Dim state As Object() = CType(result.AsyncState, Object())

            Dim folder As AbstractFolder = CType(state(0), AbstractFolder)

            m_asyncFtpClient.BeginSendMultipleFiles(folder.FullName & "*", True, True, New AsyncCallback(AddressOf Me.SendMultipleFilesReturnToParent), state(1))
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try
    End Sub

    Private Sub SendMultipleFilesReturnToParent(ByVal result As IAsyncResult)
        Try
            m_asyncFtpClient.EndSendMultipleFiles(result)

            m_asyncFtpClient.BeginChangeToParentFolder(New AsyncCallback(AddressOf Me.SendMultipleFilesCompleted), result.AsyncState)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try
    End Sub

    Private Sub SendMultipleFilesCompleted(ByVal result As IAsyncResult)
        Try
            m_asyncFtpClient.EndChangeToParentFolder(result)

            Dim index As Integer = CInt(result.AsyncState)
            Me.TransferLocalSelectedItem(index + 1)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.RemoteChangesCompleted()
        End Try
    End Sub

    Private Sub TransferLocalSelectedItems()

        ' We start with the first selected item and we will transfer each item
        ' asynchroneously. But we don't want the remote nor local selections and
        ' current folders to change, so we disable the two panels.

        If lsvLocalFiles.SelectedItems.Count > 0 Then
            pnlLocal.Enabled = False
            pnlRemote.Enabled = False

            Me.TransferLocalSelectedItem(0)
        End If

    End Sub

    Private Sub TransferLocalSelectedItem(ByVal index As Integer)

        If index >= lsvLocalFiles.SelectedItems.Count Then
            Me.RemoteChangesCompleted()
        Else
            Dim item As LocalListViewItem = lsvLocalFiles.SelectedItems(index)

            If TypeOf item.Item Is AbstractFolder Then
                Me.SendMultipleFiles(item.Item, index)
            Else
                Me.SendFile(item.Item, index)
            End If
        End If

    End Sub

#End Region

#Region "PRIVATE MEMBERS"

    Private WithEvents m_asyncFtpClient As AsyncFtpClient
    Private m_hostAddress As String = "localhost"
    Private m_hostPort As Integer = 21
    Private m_anonymousConnection As Boolean = True
    Private m_userName As String = "anonymous"
    Private m_password As String = "guest"
    Private m_proxyAddress As String = String.Empty
    Private m_proxyPort As Integer = 8080
    Private m_proxyUserName As String = String.Empty
    Private m_proxyPassword As String = String.Empty
    Friend WithEvents mnuOptionModeZ As System.Windows.Forms.MenuItem
    Private m_preventUpdate As Boolean = False

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
    Friend WithEvents mainMenu As System.Windows.Forms.MainMenu
    Friend WithEvents mnuFile As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFileExit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAction As System.Windows.Forms.MenuItem
    Friend WithEvents mnuActionConnect As System.Windows.Forms.MenuItem
    Friend WithEvents mnuActionDisconnect As System.Windows.Forms.MenuItem
    Friend WithEvents mnuActionChangeCurrentUser As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemote As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemoteDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemoteRename As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemoteCreateFolder As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemoteTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocal As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocalDelete As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocalRename As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocalCreateFolder As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLocalTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOption As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionPassiveTransfer As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionPreAllocateStorage As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionRepresentationType As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionRepresentationTypeAscii As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionRepresentationTypeBinary As System.Windows.Forms.MenuItem
    Friend WithEvents imgFtpItemType As System.Windows.Forms.ImageList
    Friend WithEvents imgMain As System.Windows.Forms.ImageList
    Friend WithEvents imgFolderContentToolbar As System.Windows.Forms.ImageList
    Friend WithEvents tlbMain As System.Windows.Forms.ToolBar
    Friend WithEvents btnConnect As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnDisconnect As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnChangeUser As System.Windows.Forms.ToolBarButton
    Friend WithEvents pnlRemote As System.Windows.Forms.Panel
    Friend WithEvents lsvRemoteFiles As System.Windows.Forms.ListView
    Friend WithEvents tlbRemoteFolderContent As System.Windows.Forms.ToolBar
    Friend WithEvents splRemote As System.Windows.Forms.Splitter
    Friend WithEvents trvRemoteDir As System.Windows.Forms.TreeView
    Friend WithEvents pnlRemoteTitle As System.Windows.Forms.Panel
    Friend WithEvents lblRemoteTitle As System.Windows.Forms.Label
    Friend WithEvents colRemoteName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRemoteSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRemoteDateTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnRemoteDelete As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRemoteRename As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRemoteCreateFolder As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRemoteTransfer As System.Windows.Forms.ToolBarButton
    Friend WithEvents splMain As System.Windows.Forms.Splitter
    Friend WithEvents pnlLocal As System.Windows.Forms.Panel
    Friend WithEvents lsvLocalFiles As System.Windows.Forms.ListView
    Friend WithEvents tlbLocalFolderContent As System.Windows.Forms.ToolBar
    Friend WithEvents splLocal As System.Windows.Forms.Splitter
    Friend WithEvents trvLocalDir As System.Windows.Forms.TreeView
    Friend WithEvents pnlLocalTitle As System.Windows.Forms.Panel
    Friend WithEvents lblLocalTitle As System.Windows.Forms.Label
    Friend WithEvents colLocalName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLocalSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents colLocalDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnLocalDelete As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLocalRename As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLocalCreateFolder As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLocalTransfer As System.Windows.Forms.ToolBarButton
    Friend WithEvents stbMain As System.Windows.Forms.StatusBar
    Friend WithEvents pnlConnectionStatus As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlProgressInfo As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlSpeed As System.Windows.Forms.StatusBarPanel
    Friend WithEvents pnlProgress As System.Windows.Forms.StatusBarPanel
    Friend WithEvents txtConnectionLogInformation As System.Windows.Forms.TextBox
    Friend WithEvents pgbTransfer As System.Windows.Forms.ProgressBar
    Friend WithEvents btnAbort As System.Windows.Forms.ToolBarButton
    Friend WithEvents mnuActionAbort As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionSecure As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionSecureNone As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionSecureExplicit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuOptionSecureImplicit As System.Windows.Forms.MenuItem
    Friend WithEvents pnlSecure As System.Windows.Forms.StatusBarPanel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.mainMenu = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuFile = New System.Windows.Forms.MenuItem
        Me.mnuFileExit = New System.Windows.Forms.MenuItem
        Me.mnuAction = New System.Windows.Forms.MenuItem
        Me.mnuActionConnect = New System.Windows.Forms.MenuItem
        Me.mnuActionDisconnect = New System.Windows.Forms.MenuItem
        Me.mnuActionChangeCurrentUser = New System.Windows.Forms.MenuItem
        Me.mnuActionAbort = New System.Windows.Forms.MenuItem
        Me.mnuRemote = New System.Windows.Forms.MenuItem
        Me.mnuRemoteDelete = New System.Windows.Forms.MenuItem
        Me.mnuRemoteRename = New System.Windows.Forms.MenuItem
        Me.mnuRemoteCreateFolder = New System.Windows.Forms.MenuItem
        Me.mnuRemoteTransfer = New System.Windows.Forms.MenuItem
        Me.mnuLocal = New System.Windows.Forms.MenuItem
        Me.mnuLocalDelete = New System.Windows.Forms.MenuItem
        Me.mnuLocalRename = New System.Windows.Forms.MenuItem
        Me.mnuLocalCreateFolder = New System.Windows.Forms.MenuItem
        Me.mnuLocalTransfer = New System.Windows.Forms.MenuItem
        Me.mnuOption = New System.Windows.Forms.MenuItem
        Me.mnuOptionPassiveTransfer = New System.Windows.Forms.MenuItem
        Me.mnuOptionPreAllocateStorage = New System.Windows.Forms.MenuItem
        Me.mnuOptionRepresentationType = New System.Windows.Forms.MenuItem
        Me.mnuOptionRepresentationTypeAscii = New System.Windows.Forms.MenuItem
        Me.mnuOptionRepresentationTypeBinary = New System.Windows.Forms.MenuItem
        Me.mnuOptionSecure = New System.Windows.Forms.MenuItem
        Me.mnuOptionSecureNone = New System.Windows.Forms.MenuItem
        Me.mnuOptionSecureExplicit = New System.Windows.Forms.MenuItem
        Me.mnuOptionSecureImplicit = New System.Windows.Forms.MenuItem
        Me.imgFtpItemType = New System.Windows.Forms.ImageList(Me.components)
        Me.imgMain = New System.Windows.Forms.ImageList(Me.components)
        Me.imgFolderContentToolbar = New System.Windows.Forms.ImageList(Me.components)
        Me.tlbMain = New System.Windows.Forms.ToolBar
        Me.btnConnect = New System.Windows.Forms.ToolBarButton
        Me.btnDisconnect = New System.Windows.Forms.ToolBarButton
        Me.btnChangeUser = New System.Windows.Forms.ToolBarButton
        Me.btnAbort = New System.Windows.Forms.ToolBarButton
        Me.pnlRemote = New System.Windows.Forms.Panel
        Me.lsvRemoteFiles = New System.Windows.Forms.ListView
        Me.colRemoteName = New System.Windows.Forms.ColumnHeader
        Me.colRemoteSize = New System.Windows.Forms.ColumnHeader
        Me.colRemoteDateTime = New System.Windows.Forms.ColumnHeader
        Me.tlbRemoteFolderContent = New System.Windows.Forms.ToolBar
        Me.btnRemoteDelete = New System.Windows.Forms.ToolBarButton
        Me.btnRemoteRename = New System.Windows.Forms.ToolBarButton
        Me.btnRemoteCreateFolder = New System.Windows.Forms.ToolBarButton
        Me.btnRemoteTransfer = New System.Windows.Forms.ToolBarButton
        Me.splRemote = New System.Windows.Forms.Splitter
        Me.trvRemoteDir = New System.Windows.Forms.TreeView
        Me.pnlRemoteTitle = New System.Windows.Forms.Panel
        Me.lblRemoteTitle = New System.Windows.Forms.Label
        Me.splMain = New System.Windows.Forms.Splitter
        Me.pnlLocal = New System.Windows.Forms.Panel
        Me.lsvLocalFiles = New System.Windows.Forms.ListView
        Me.colLocalName = New System.Windows.Forms.ColumnHeader
        Me.colLocalSize = New System.Windows.Forms.ColumnHeader
        Me.colLocalDate = New System.Windows.Forms.ColumnHeader
        Me.tlbLocalFolderContent = New System.Windows.Forms.ToolBar
        Me.btnLocalDelete = New System.Windows.Forms.ToolBarButton
        Me.btnLocalRename = New System.Windows.Forms.ToolBarButton
        Me.btnLocalCreateFolder = New System.Windows.Forms.ToolBarButton
        Me.btnLocalTransfer = New System.Windows.Forms.ToolBarButton
        Me.splLocal = New System.Windows.Forms.Splitter
        Me.trvLocalDir = New System.Windows.Forms.TreeView
        Me.pnlLocalTitle = New System.Windows.Forms.Panel
        Me.lblLocalTitle = New System.Windows.Forms.Label
        Me.stbMain = New System.Windows.Forms.StatusBar
        Me.pnlConnectionStatus = New System.Windows.Forms.StatusBarPanel
        Me.pnlProgressInfo = New System.Windows.Forms.StatusBarPanel
        Me.pnlSpeed = New System.Windows.Forms.StatusBarPanel
        Me.pnlSecure = New System.Windows.Forms.StatusBarPanel
        Me.pnlProgress = New System.Windows.Forms.StatusBarPanel
        Me.txtConnectionLogInformation = New System.Windows.Forms.TextBox
        Me.pgbTransfer = New System.Windows.Forms.ProgressBar
        Me.mnuOptionModeZ = New System.Windows.Forms.MenuItem
        Me.pnlRemote.SuspendLayout()
        Me.pnlRemoteTitle.SuspendLayout()
        Me.pnlLocal.SuspendLayout()
        Me.pnlLocalTitle.SuspendLayout()
        CType(Me.pnlConnectionStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlProgressInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlSecure, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnlProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mainMenu
        '
        Me.mainMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFile, Me.mnuAction, Me.mnuRemote, Me.mnuLocal, Me.mnuOption})
        '
        'mnuFile
        '
        Me.mnuFile.Index = 0
        Me.mnuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuFileExit})
        Me.mnuFile.Text = "&File"
        '
        'mnuFileExit
        '
        Me.mnuFileExit.Index = 0
        Me.mnuFileExit.Text = "E&xit"
        '
        'mnuAction
        '
        Me.mnuAction.Index = 1
        Me.mnuAction.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuActionConnect, Me.mnuActionDisconnect, Me.mnuActionChangeCurrentUser, Me.mnuActionAbort})
        Me.mnuAction.Text = "&Actions"
        '
        'mnuActionConnect
        '
        Me.mnuActionConnect.Index = 0
        Me.mnuActionConnect.Text = "&Connect..."
        '
        'mnuActionDisconnect
        '
        Me.mnuActionDisconnect.Index = 1
        Me.mnuActionDisconnect.Text = "&Disconnect"
        '
        'mnuActionChangeCurrentUser
        '
        Me.mnuActionChangeCurrentUser.Index = 2
        Me.mnuActionChangeCurrentUser.Text = "&Change current user..."
        '
        'mnuActionAbort
        '
        Me.mnuActionAbort.Index = 3
        Me.mnuActionAbort.Text = "&Abort"
        '
        'mnuRemote
        '
        Me.mnuRemote.Index = 2
        Me.mnuRemote.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuRemoteDelete, Me.mnuRemoteRename, Me.mnuRemoteCreateFolder, Me.mnuRemoteTransfer})
        Me.mnuRemote.Text = "&Remote"
        '
        'mnuRemoteDelete
        '
        Me.mnuRemoteDelete.Index = 0
        Me.mnuRemoteDelete.Text = "&Delete..."
        '
        'mnuRemoteRename
        '
        Me.mnuRemoteRename.Index = 1
        Me.mnuRemoteRename.Text = "&Rename..."
        '
        'mnuRemoteCreateFolder
        '
        Me.mnuRemoteCreateFolder.Index = 2
        Me.mnuRemoteCreateFolder.Text = "&Create folder..."
        '
        'mnuRemoteTransfer
        '
        Me.mnuRemoteTransfer.Index = 3
        Me.mnuRemoteTransfer.Text = "&Transfer"
        '
        'mnuLocal
        '
        Me.mnuLocal.Index = 3
        Me.mnuLocal.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuLocalDelete, Me.mnuLocalRename, Me.mnuLocalCreateFolder, Me.mnuLocalTransfer})
        Me.mnuLocal.Text = "&Local"
        '
        'mnuLocalDelete
        '
        Me.mnuLocalDelete.Index = 0
        Me.mnuLocalDelete.Text = "&Delete..."
        '
        'mnuLocalRename
        '
        Me.mnuLocalRename.Index = 1
        Me.mnuLocalRename.Text = "&Rename..."
        '
        'mnuLocalCreateFolder
        '
        Me.mnuLocalCreateFolder.Index = 2
        Me.mnuLocalCreateFolder.Text = "&Create folder..."
        '
        'mnuLocalTransfer
        '
        Me.mnuLocalTransfer.Index = 3
        Me.mnuLocalTransfer.Text = "&Trasnfer"
        '
        'mnuOption
        '
        Me.mnuOption.Index = 4
        Me.mnuOption.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOptionPassiveTransfer, Me.mnuOptionPreAllocateStorage, Me.mnuOptionModeZ, Me.mnuOptionRepresentationType, Me.mnuOptionSecure})
        Me.mnuOption.Text = "&Options"
        '
        'mnuOptionPassiveTransfer
        '
        Me.mnuOptionPassiveTransfer.Checked = True
        Me.mnuOptionPassiveTransfer.Index = 0
        Me.mnuOptionPassiveTransfer.Text = "&Passive transfer"
        '
        'mnuOptionPreAllocateStorage
        '
        Me.mnuOptionPreAllocateStorage.Index = 1
        Me.mnuOptionPreAllocateStorage.Text = "Pr&e allocate storage"
        '
        'mnuOptionRepresentationType
        '
        Me.mnuOptionRepresentationType.Index = 3
        Me.mnuOptionRepresentationType.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOptionRepresentationTypeAscii, Me.mnuOptionRepresentationTypeBinary})
        Me.mnuOptionRepresentationType.Text = "&Representation type"
        '
        'mnuOptionRepresentationTypeAscii
        '
        Me.mnuOptionRepresentationTypeAscii.Index = 0
        Me.mnuOptionRepresentationTypeAscii.RadioCheck = True
        Me.mnuOptionRepresentationTypeAscii.Text = "&Ascii"
        '
        'mnuOptionRepresentationTypeBinary
        '
        Me.mnuOptionRepresentationTypeBinary.Checked = True
        Me.mnuOptionRepresentationTypeBinary.Index = 1
        Me.mnuOptionRepresentationTypeBinary.RadioCheck = True
        Me.mnuOptionRepresentationTypeBinary.Text = "&Binary"
        '
        'mnuOptionSecure
        '
        Me.mnuOptionSecure.Index = 4
        Me.mnuOptionSecure.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuOptionSecureNone, Me.mnuOptionSecureExplicit, Me.mnuOptionSecureImplicit})
        Me.mnuOptionSecure.Text = "&Secure connection"
        '
        'mnuOptionSecureNone
        '
        Me.mnuOptionSecureNone.Checked = True
        Me.mnuOptionSecureNone.Index = 0
        Me.mnuOptionSecureNone.RadioCheck = True
        Me.mnuOptionSecureNone.Text = "&None"
        '
        'mnuOptionSecureExplicit
        '
        Me.mnuOptionSecureExplicit.Index = 1
        Me.mnuOptionSecureExplicit.RadioCheck = True
        Me.mnuOptionSecureExplicit.Text = "&Explicit TLS authentication"
        '
        'mnuOptionSecureImplicit
        '
        Me.mnuOptionSecureImplicit.Index = 2
        Me.mnuOptionSecureImplicit.RadioCheck = True
        Me.mnuOptionSecureImplicit.Text = "&Implicit TLS authentication"
        '
        'imgFtpItemType
        '
        Me.imgFtpItemType.ImageStream = CType(resources.GetObject("imgFtpItemType.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFtpItemType.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFtpItemType.Images.SetKeyName(0, "")
        Me.imgFtpItemType.Images.SetKeyName(1, "")
        Me.imgFtpItemType.Images.SetKeyName(2, "")
        Me.imgFtpItemType.Images.SetKeyName(3, "")
        Me.imgFtpItemType.Images.SetKeyName(4, "")
        '
        'imgMain
        '
        Me.imgMain.ImageStream = CType(resources.GetObject("imgMain.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMain.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMain.Images.SetKeyName(0, "")
        Me.imgMain.Images.SetKeyName(1, "")
        Me.imgMain.Images.SetKeyName(2, "")
        Me.imgMain.Images.SetKeyName(3, "")
        '
        'imgFolderContentToolbar
        '
        Me.imgFolderContentToolbar.ImageStream = CType(resources.GetObject("imgFolderContentToolbar.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFolderContentToolbar.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFolderContentToolbar.Images.SetKeyName(0, "")
        Me.imgFolderContentToolbar.Images.SetKeyName(1, "")
        Me.imgFolderContentToolbar.Images.SetKeyName(2, "")
        Me.imgFolderContentToolbar.Images.SetKeyName(3, "")
        '
        'tlbMain
        '
        Me.tlbMain.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnConnect, Me.btnDisconnect, Me.btnChangeUser, Me.btnAbort})
        Me.tlbMain.DropDownArrows = True
        Me.tlbMain.ImageList = Me.imgMain
        Me.tlbMain.Location = New System.Drawing.Point(0, 0)
        Me.tlbMain.Name = "tlbMain"
        Me.tlbMain.ShowToolTips = True
        Me.tlbMain.Size = New System.Drawing.Size(616, 42)
        Me.tlbMain.TabIndex = 9
        '
        'btnConnect
        '
        Me.btnConnect.ImageIndex = 0
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Text = "Connect"
        '
        'btnDisconnect
        '
        Me.btnDisconnect.ImageIndex = 1
        Me.btnDisconnect.Name = "btnDisconnect"
        Me.btnDisconnect.Text = "Disconnect"
        '
        'btnChangeUser
        '
        Me.btnChangeUser.ImageIndex = 2
        Me.btnChangeUser.Name = "btnChangeUser"
        Me.btnChangeUser.Text = "Change current user"
        '
        'btnAbort
        '
        Me.btnAbort.ImageIndex = 3
        Me.btnAbort.Name = "btnAbort"
        Me.btnAbort.Text = "Abort!"
        '
        'pnlRemote
        '
        Me.pnlRemote.Controls.Add(Me.lsvRemoteFiles)
        Me.pnlRemote.Controls.Add(Me.tlbRemoteFolderContent)
        Me.pnlRemote.Controls.Add(Me.splRemote)
        Me.pnlRemote.Controls.Add(Me.trvRemoteDir)
        Me.pnlRemote.Controls.Add(Me.pnlRemoteTitle)
        Me.pnlRemote.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRemote.Location = New System.Drawing.Point(0, 42)
        Me.pnlRemote.Name = "pnlRemote"
        Me.pnlRemote.Size = New System.Drawing.Size(616, 161)
        Me.pnlRemote.TabIndex = 10
        '
        'lsvRemoteFiles
        '
        Me.lsvRemoteFiles.AllowColumnReorder = True
        Me.lsvRemoteFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colRemoteName, Me.colRemoteSize, Me.colRemoteDateTime})
        Me.lsvRemoteFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvRemoteFiles.Location = New System.Drawing.Point(163, 66)
        Me.lsvRemoteFiles.Name = "lsvRemoteFiles"
        Me.lsvRemoteFiles.Size = New System.Drawing.Size(453, 95)
        Me.lsvRemoteFiles.SmallImageList = Me.imgFtpItemType
        Me.lsvRemoteFiles.TabIndex = 3
        Me.lsvRemoteFiles.UseCompatibleStateImageBehavior = False
        Me.lsvRemoteFiles.View = System.Windows.Forms.View.Details
        '
        'colRemoteName
        '
        Me.colRemoteName.Text = "Name"
        Me.colRemoteName.Width = 240
        '
        'colRemoteSize
        '
        Me.colRemoteSize.Text = "Size"
        Me.colRemoteSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRemoteSize.Width = 129
        '
        'colRemoteDateTime
        '
        Me.colRemoteDateTime.Text = "Date"
        Me.colRemoteDateTime.Width = 135
        '
        'tlbRemoteFolderContent
        '
        Me.tlbRemoteFolderContent.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnRemoteDelete, Me.btnRemoteRename, Me.btnRemoteCreateFolder, Me.btnRemoteTransfer})
        Me.tlbRemoteFolderContent.DropDownArrows = True
        Me.tlbRemoteFolderContent.ImageList = Me.imgFolderContentToolbar
        Me.tlbRemoteFolderContent.Location = New System.Drawing.Point(163, 24)
        Me.tlbRemoteFolderContent.Name = "tlbRemoteFolderContent"
        Me.tlbRemoteFolderContent.ShowToolTips = True
        Me.tlbRemoteFolderContent.Size = New System.Drawing.Size(453, 42)
        Me.tlbRemoteFolderContent.TabIndex = 2
        '
        'btnRemoteDelete
        '
        Me.btnRemoteDelete.ImageIndex = 0
        Me.btnRemoteDelete.Name = "btnRemoteDelete"
        Me.btnRemoteDelete.Text = "Delete"
        '
        'btnRemoteRename
        '
        Me.btnRemoteRename.ImageIndex = 1
        Me.btnRemoteRename.Name = "btnRemoteRename"
        Me.btnRemoteRename.Text = "Rename"
        '
        'btnRemoteCreateFolder
        '
        Me.btnRemoteCreateFolder.ImageIndex = 3
        Me.btnRemoteCreateFolder.Name = "btnRemoteCreateFolder"
        Me.btnRemoteCreateFolder.Text = "Create folder"
        '
        'btnRemoteTransfer
        '
        Me.btnRemoteTransfer.ImageIndex = 2
        Me.btnRemoteTransfer.Name = "btnRemoteTransfer"
        Me.btnRemoteTransfer.Text = "Transfer"
        '
        'splRemote
        '
        Me.splRemote.Location = New System.Drawing.Point(160, 24)
        Me.splRemote.Name = "splRemote"
        Me.splRemote.Size = New System.Drawing.Size(3, 137)
        Me.splRemote.TabIndex = 2
        Me.splRemote.TabStop = False
        '
        'trvRemoteDir
        '
        Me.trvRemoteDir.Dock = System.Windows.Forms.DockStyle.Left
        Me.trvRemoteDir.HideSelection = False
        Me.trvRemoteDir.ImageIndex = 0
        Me.trvRemoteDir.ImageList = Me.imgFtpItemType
        Me.trvRemoteDir.Location = New System.Drawing.Point(0, 24)
        Me.trvRemoteDir.Name = "trvRemoteDir"
        Me.trvRemoteDir.SelectedImageIndex = 1
        Me.trvRemoteDir.ShowLines = False
        Me.trvRemoteDir.Size = New System.Drawing.Size(160, 137)
        Me.trvRemoteDir.Sorted = True
        Me.trvRemoteDir.TabIndex = 1
        '
        'pnlRemoteTitle
        '
        Me.pnlRemoteTitle.BackColor = System.Drawing.SystemColors.Highlight
        Me.pnlRemoteTitle.Controls.Add(Me.lblRemoteTitle)
        Me.pnlRemoteTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRemoteTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlRemoteTitle.Name = "pnlRemoteTitle"
        Me.pnlRemoteTitle.Padding = New System.Windows.Forms.Padding(4)
        Me.pnlRemoteTitle.Size = New System.Drawing.Size(616, 24)
        Me.pnlRemoteTitle.TabIndex = 0
        '
        'lblRemoteTitle
        '
        Me.lblRemoteTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblRemoteTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemoteTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblRemoteTitle.Location = New System.Drawing.Point(4, 4)
        Me.lblRemoteTitle.Name = "lblRemoteTitle"
        Me.lblRemoteTitle.Size = New System.Drawing.Size(608, 16)
        Me.lblRemoteTitle.TabIndex = 0
        Me.lblRemoteTitle.Text = "Remote"
        '
        'splMain
        '
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Top
        Me.splMain.Location = New System.Drawing.Point(0, 203)
        Me.splMain.Name = "splMain"
        Me.splMain.Size = New System.Drawing.Size(616, 3)
        Me.splMain.TabIndex = 11
        Me.splMain.TabStop = False
        '
        'pnlLocal
        '
        Me.pnlLocal.Controls.Add(Me.lsvLocalFiles)
        Me.pnlLocal.Controls.Add(Me.tlbLocalFolderContent)
        Me.pnlLocal.Controls.Add(Me.splLocal)
        Me.pnlLocal.Controls.Add(Me.trvLocalDir)
        Me.pnlLocal.Controls.Add(Me.pnlLocalTitle)
        Me.pnlLocal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLocal.Location = New System.Drawing.Point(0, 206)
        Me.pnlLocal.Name = "pnlLocal"
        Me.pnlLocal.Size = New System.Drawing.Size(616, 138)
        Me.pnlLocal.TabIndex = 12
        '
        'lsvLocalFiles
        '
        Me.lsvLocalFiles.AllowColumnReorder = True
        Me.lsvLocalFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colLocalName, Me.colLocalSize, Me.colLocalDate})
        Me.lsvLocalFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvLocalFiles.Location = New System.Drawing.Point(163, 66)
        Me.lsvLocalFiles.Name = "lsvLocalFiles"
        Me.lsvLocalFiles.Size = New System.Drawing.Size(453, 72)
        Me.lsvLocalFiles.SmallImageList = Me.imgFtpItemType
        Me.lsvLocalFiles.TabIndex = 7
        Me.lsvLocalFiles.UseCompatibleStateImageBehavior = False
        Me.lsvLocalFiles.View = System.Windows.Forms.View.Details
        '
        'colLocalName
        '
        Me.colLocalName.Text = "Name"
        Me.colLocalName.Width = 240
        '
        'colLocalSize
        '
        Me.colLocalSize.Text = "Size"
        Me.colLocalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colLocalSize.Width = 129
        '
        'colLocalDate
        '
        Me.colLocalDate.Text = "Date"
        Me.colLocalDate.Width = 135
        '
        'tlbLocalFolderContent
        '
        Me.tlbLocalFolderContent.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnLocalDelete, Me.btnLocalRename, Me.btnLocalCreateFolder, Me.btnLocalTransfer})
        Me.tlbLocalFolderContent.DropDownArrows = True
        Me.tlbLocalFolderContent.ImageList = Me.imgFolderContentToolbar
        Me.tlbLocalFolderContent.Location = New System.Drawing.Point(163, 24)
        Me.tlbLocalFolderContent.Name = "tlbLocalFolderContent"
        Me.tlbLocalFolderContent.ShowToolTips = True
        Me.tlbLocalFolderContent.Size = New System.Drawing.Size(453, 42)
        Me.tlbLocalFolderContent.TabIndex = 6
        '
        'btnLocalDelete
        '
        Me.btnLocalDelete.ImageIndex = 0
        Me.btnLocalDelete.Name = "btnLocalDelete"
        Me.btnLocalDelete.Text = "Delete"
        '
        'btnLocalRename
        '
        Me.btnLocalRename.ImageIndex = 1
        Me.btnLocalRename.Name = "btnLocalRename"
        Me.btnLocalRename.Text = "Rename"
        '
        'btnLocalCreateFolder
        '
        Me.btnLocalCreateFolder.ImageIndex = 3
        Me.btnLocalCreateFolder.Name = "btnLocalCreateFolder"
        Me.btnLocalCreateFolder.Text = "Create folder"
        '
        'btnLocalTransfer
        '
        Me.btnLocalTransfer.ImageIndex = 2
        Me.btnLocalTransfer.Name = "btnLocalTransfer"
        Me.btnLocalTransfer.Text = "Transfer"
        '
        'splLocal
        '
        Me.splLocal.Location = New System.Drawing.Point(160, 24)
        Me.splLocal.Name = "splLocal"
        Me.splLocal.Size = New System.Drawing.Size(3, 114)
        Me.splLocal.TabIndex = 2
        Me.splLocal.TabStop = False
        '
        'trvLocalDir
        '
        Me.trvLocalDir.Dock = System.Windows.Forms.DockStyle.Left
        Me.trvLocalDir.HideSelection = False
        Me.trvLocalDir.ImageIndex = 0
        Me.trvLocalDir.ImageList = Me.imgFtpItemType
        Me.trvLocalDir.Location = New System.Drawing.Point(0, 24)
        Me.trvLocalDir.Name = "trvLocalDir"
        Me.trvLocalDir.SelectedImageIndex = 0
        Me.trvLocalDir.ShowLines = False
        Me.trvLocalDir.Size = New System.Drawing.Size(160, 114)
        Me.trvLocalDir.Sorted = True
        Me.trvLocalDir.TabIndex = 5
        '
        'pnlLocalTitle
        '
        Me.pnlLocalTitle.BackColor = System.Drawing.SystemColors.Highlight
        Me.pnlLocalTitle.Controls.Add(Me.lblLocalTitle)
        Me.pnlLocalTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLocalTitle.Location = New System.Drawing.Point(0, 0)
        Me.pnlLocalTitle.Name = "pnlLocalTitle"
        Me.pnlLocalTitle.Padding = New System.Windows.Forms.Padding(4)
        Me.pnlLocalTitle.Size = New System.Drawing.Size(616, 24)
        Me.pnlLocalTitle.TabIndex = 0
        '
        'lblLocalTitle
        '
        Me.lblLocalTitle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLocalTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalTitle.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.lblLocalTitle.Location = New System.Drawing.Point(4, 4)
        Me.lblLocalTitle.Name = "lblLocalTitle"
        Me.lblLocalTitle.Size = New System.Drawing.Size(608, 16)
        Me.lblLocalTitle.TabIndex = 0
        Me.lblLocalTitle.Text = "Local"
        '
        'stbMain
        '
        Me.stbMain.Location = New System.Drawing.Point(0, 424)
        Me.stbMain.Name = "stbMain"
        Me.stbMain.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.pnlConnectionStatus, Me.pnlProgressInfo, Me.pnlSpeed, Me.pnlSecure, Me.pnlProgress})
        Me.stbMain.ShowPanels = True
        Me.stbMain.Size = New System.Drawing.Size(616, 22)
        Me.stbMain.TabIndex = 13
        '
        'pnlConnectionStatus
        '
        Me.pnlConnectionStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.pnlConnectionStatus.Icon = CType(resources.GetObject("pnlConnectionStatus.Icon"), System.Drawing.Icon)
        Me.pnlConnectionStatus.Name = "pnlConnectionStatus"
        Me.pnlConnectionStatus.Text = "Disconnected"
        Me.pnlConnectionStatus.Width = 424
        '
        'pnlProgressInfo
        '
        Me.pnlProgressInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.pnlProgressInfo.MinWidth = 25
        Me.pnlProgressInfo.Name = "pnlProgressInfo"
        Me.pnlProgressInfo.Width = 25
        '
        'pnlSpeed
        '
        Me.pnlSpeed.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.pnlSpeed.MinWidth = 25
        Me.pnlSpeed.Name = "pnlSpeed"
        Me.pnlSpeed.Width = 25
        '
        'pnlSecure
        '
        Me.pnlSecure.Icon = CType(resources.GetObject("pnlSecure.Icon"), System.Drawing.Icon)
        Me.pnlSecure.MinWidth = 0
        Me.pnlSecure.Name = "pnlSecure"
        Me.pnlSecure.ToolTipText = "An SSL connection is active"
        Me.pnlSecure.Width = 25
        '
        'pnlProgress
        '
        Me.pnlProgress.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.pnlProgress.Name = "pnlProgress"
        Me.pnlProgress.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw
        '
        'txtConnectionLogInformation
        '
        Me.txtConnectionLogInformation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.txtConnectionLogInformation.Location = New System.Drawing.Point(0, 344)
        Me.txtConnectionLogInformation.MaxLength = 0
        Me.txtConnectionLogInformation.Multiline = True
        Me.txtConnectionLogInformation.Name = "txtConnectionLogInformation"
        Me.txtConnectionLogInformation.ReadOnly = True
        Me.txtConnectionLogInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtConnectionLogInformation.Size = New System.Drawing.Size(616, 80)
        Me.txtConnectionLogInformation.TabIndex = 14
        '
        'pgbTransfer
        '
        Me.pgbTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbTransfer.Location = New System.Drawing.Point(504, 428)
        Me.pgbTransfer.Name = "pgbTransfer"
        Me.pgbTransfer.Size = New System.Drawing.Size(96, 14)
        Me.pgbTransfer.TabIndex = 15
        '
        'mnuOptionModeZ
        '
        Me.mnuOptionModeZ.Index = 2
        Me.mnuOptionModeZ.Text = "Use Mode &Z"
        '
        'MainForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(616, 446)
        Me.Controls.Add(Me.pgbTransfer)
        Me.Controls.Add(Me.pnlLocal)
        Me.Controls.Add(Me.txtConnectionLogInformation)
        Me.Controls.Add(Me.stbMain)
        Me.Controls.Add(Me.splMain)
        Me.Controls.Add(Me.pnlRemote)
        Me.Controls.Add(Me.tlbMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mainMenu
        Me.Name = "MainForm"
        Me.Text = "Client Ftp"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlRemote.ResumeLayout(False)
        Me.pnlRemote.PerformLayout()
        Me.pnlRemoteTitle.ResumeLayout(False)
        Me.pnlLocal.ResumeLayout(False)
        Me.pnlLocal.PerformLayout()
        Me.pnlLocalTitle.ResumeLayout(False)
        CType(Me.pnlConnectionStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlProgressInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlSecure, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnlProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

End Class
