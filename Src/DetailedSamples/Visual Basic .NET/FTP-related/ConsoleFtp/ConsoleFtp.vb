' 
' Xceed FTP for .NET - ConsoleFtp Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [ConsoleFtp.vb]
'  
' This application demonstrates how to use the Xceed FTP Object model
' in a generic way.
'  
' This file is part of Xceed Ftp for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Imports Xceed.Ftp
Imports Xceed.FileSystem

Public Class ConsoleFtp
#Region "CONSTRUCTORS.........................................................."

    Sub New()
        ' Subscribe to the events of the FTP engine.
        AddHandler m_client.CertificateReceived, AddressOf Me.OnCertificateReceived
        AddHandler m_client.CommandSent, AddressOf Me.OnClientCommandSent
        AddHandler m_client.Disconnected, AddressOf Me.OnDisconnected
        AddHandler m_client.FileTransferStatus, AddressOf Me.OnFileTransferStatus
        AddHandler m_client.MultipleFileTransferError, AddressOf Me.OnMultipleFileTransferError
        AddHandler m_client.ReceivingFile, AddressOf Me.OnReceivingFile
        AddHandler m_client.ReplyReceived, AddressOf Me.OnServerReplyReceived
        AddHandler m_client.SendingFile, AddressOf Me.OnSendingFile
    End Sub

#End Region

#Region "EVENT HANDLERS........................................................"

    Private Sub OnCertificateReceived(ByVal sender As Object, ByVal e As CertificateReceivedEventArgs)
        Console.WriteLine("The following certificate was received:")
        Console.WriteLine(e.ServerCertificate.ToString())
        Console.WriteLine("It has been verified as {0}.", e.Status.ToString())

        Console.WriteLine()
        Console.WriteLine("Do you want to accept this certifinate? [Y/N]")
        Dim answer As String = Console.ReadLine()

        If answer.ToUpper().StartsWith("Y") Then
            e.Action = VerificationAction.Accept
        Else
            e.Action = VerificationAction.Reject
        End If
    End Sub

    Private Sub OnClientCommandSent(ByVal sender As Object, ByVal e As CommandSentEventArgs)
        If m_debug Then
            Console.WriteLine(" -> {0,-70}", e.Command)
        End If
    End Sub

    Private Sub OnDisconnected(ByVal sender As Object, ByVal e As EventArgs)
        If m_verbose Then
            Console.WriteLine("Disconnected from server.")
        End If
    End Sub

    Private Sub OnFileTransferStatus(ByVal sender As Object, ByVal e As FileTransferStatusEventArgs)
        If m_verbose Then
            Console.Write("{0}% ({1:F3}kb/s)     " + vbCr, e.BytesPercent.ToString(), (e.BytesPerSecond / 1024))

            If e.BytesPercent = 100 Then
                Console.WriteLine()
            End If
        End If
    End Sub

    Private Sub OnMultipleFileTransferError(ByVal sender As Object, ByVal e As MultipleFileTransferErrorEventArgs)
        If m_verbose Then
            Console.WriteLine("Skipping file '{0}'" + vbCrLf + "  {1}", e.LocalItemName, e.Exception.Message)
        End If

        e.Action = MultipleFileTransferErrorAction.Ignore
    End Sub

    Private Sub OnReceivingFile(ByVal sender As Object, ByVal e As TransferringFileEventArgs)
        If m_verbose Then
            Console.WriteLine("Receiving file '{0}'", e.RemoteFilename)
        End If
    End Sub

    Private Sub OnSendingFile(ByVal sender As Object, ByVal e As TransferringFileEventArgs)
        If m_verbose Then
            Console.WriteLine("Sending file '{0}'", e.LocalFilename)
        End If
    End Sub

    Private Sub OnServerReplyReceived(ByVal sender As Object, ByVal e As ReplyReceivedEventArgs)
        If m_debug Then
            Dim replyLine As String

            ' Since we want to prepend "<-" in front of each line, we'll use
            ' the reply's Lines property.
            For Each replyLine In e.Reply.Lines
                Console.WriteLine(" <- {0,-70}", replyLine)
            Next
        End If
    End Sub

#End Region

#Region "PUBLIC METHODS........................................................"

    Public Sub Run(ByVal hostName As String)
        ' We open a session on the specified hostName

        If hostName.Length > 0 Then
            Me.ProcessCommand("open " + hostName)
        End If

        Me.Run()
    End Sub

    Public Sub Run()
        Me.ProcessInput()
    End Sub

#End Region

#Region "PRIVATE METHODS......................................................."

    Private Sub Authenticate(ByVal methodName As String)
        ' Change the authentication method to apply when connecting.

        If methodName.Length = 0 Then
            Console.WriteLine("You must provide one of the three following authentication methods: none, ssl, tls.")
        Else
            methodName = methodName.ToLower()

            If methodName = "none" Then
                m_method = AuthenticationMethod.None
            ElseIf methodName = "ssl" Then
                m_method = AuthenticationMethod.Ssl
            ElseIf methodName = "tls" Then
                m_method = AuthenticationMethod.Tls
            Else
                Console.WriteLine("Unknown authentication method.")
                Console.WriteLine("You must provide one of the three following authentication methods: none, ssl, tls.")
                Return
            End If

            If m_verbose Then
                Console.WriteLine("Authentication method changed to {0}.", m_method.ToString())

                If m_client.Connected Then
                    Console.WriteLine("These changes will apply to the next connection or login.")
                End If
            End If
        End If
    End Sub

    Private Sub ChangeCurrentFolder(ByVal folderName As String)
        ' Change the current folder on the remote side by calling 
        ' the ChangeCurrentFolder method of the FtpClient object.

        If folderName.Length = 0 Then
            Me.DisplayCurrentFolder(folderName)
        Else
            Try
                m_client.ChangeCurrentFolder(folderName)

                If m_verbose Then
                    Console.WriteLine("Current folder changed to '{0}'", folderName)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub ChangeLocalFolder(ByVal folderName As String)
        ' Change the current local folder.

        If folderName.Length = 0 Then
            Console.WriteLine("The active local folder is: {0}", m_localFolder.FullName)
        Else
            Try
                ' Try to get a folder from a relative path.
                Dim newFolder As AbstractFolder

                newFolder = m_localFolder.GetFolder(folderName)

                If newFolder.Exists Then
                    m_localFolder = newFolder
                Else
                    Console.WriteLine("The folder '{0}' does not exits.", folderName)
                End If
            Catch
                ' Try to get a folder from an absolute path.
                Try
                    Dim newFolder As AbstractFolder

                    newFolder = New DiskFolder(folderName)

                    If newFolder.Exists Then
                        m_localFolder = newFolder
                    Else
                        Console.WriteLine("The folder '{0}' does not exits.", folderName)
                    End If
                Catch
                    Console.WriteLine("Cannot change current local folder to '{0}'", folderName)
                Finally
                    If m_verbose Then
                        Console.WriteLine("The active local folder is: {0}", m_localFolder.FullName)
                    End If
                End Try
            End Try
        End If
    End Sub

    Private Sub ChangeProxy(ByVal param As String)
        Dim parts(-1) As String

        If param.Length > 0 Then
            parts = param.Split(" "c)
        End If

        Try
            Select Case parts.Length
                Case 0
                    m_client.Proxy = Nothing

                    If m_verbose Then
                        Console.WriteLine("Not using HTTP proxies.")
                    End If


                Case 1
                    m_client.Proxy = New HttpProxyClient(parts(0))

                    If m_verbose Then
                        Console.WriteLine("Using HTTP proxy {0}:{1} without credentials.", m_client.Proxy.HostName, m_client.Proxy.Port)
                    End If


                Case 2
                    m_client.Proxy = New HttpProxyClient(parts(0), parts(1), String.Empty)

                    If m_verbose Then
                        Console.WriteLine("Using HTTP proxy {0}:{1} with user {2}.", m_client.Proxy.HostName, m_client.Proxy.Port, parts(1))
                    End If


                Case 3
                    m_client.Proxy = New HttpProxyClient(parts(0), parts(1), parts(2))

                    If m_verbose Then
                        Console.WriteLine("Using HTTP proxy {0}:{1} with user {2} and a password.", m_client.Proxy.HostName, m_client.Proxy.Port, parts(1))
                    End If


                Case Else
                    Throw New ArgumentException("Invalid number of parameters.")
            End Select
        Catch except As Exception
            Console.WriteLine(except.Message)
            Console.WriteLine("The format for the 'proxy' command is:")
            Console.WriteLine("  proxy [proxy_address[:port] [username [password]]")
        End Try
    End Sub

    Private Sub ChangeRepresentationType(ByVal param As String)
        ' Change the representation type depending on the specified parameter.

        param = param.ToLower()

        If (param = "a") Or (param = "ascii") Then
            Me.ChangeRepresentationTypeToAscii(String.Empty)
        ElseIf (param = "i") Or (param = "image") Or (param = "bin") Or (param = "binary") Then
            Me.ChangeRepresentationTypeToBinary(String.Empty)
        Else
            Console.WriteLine("Unknown type format")
        End If
    End Sub

    Private Sub ChangeRepresentationTypeToAscii(ByVal param As String)
        ' Change the RepresentationType property of the FtpClient object to ASCII.

        Me.UselessParam(param)

        m_client.RepresentationType = RepresentationType.Ascii

        If m_verbose Then
            Console.WriteLine("Type changed to ASCII")
        End If
    End Sub

    Private Sub ChangeRepresentationTypeToBinary(ByVal param As String)
        ' Change the RepresentationType property of the FtpClient object to BINARY.

        Me.UselessParam(param)

        m_client.RepresentationType = RepresentationType.Binary

        If m_verbose Then
            Console.WriteLine("Type changed to BINARY")
        End If
    End Sub

    Private Sub ChangeToParentFolder(ByVal param As String)
        ' Change the current folder on the remote side to the parent folder
        ' by calling the ChangeToParentFolder method of the FtpClient object.

        Me.UselessParam(param)

        Try
            m_client.ChangeToParentFolder()

            If m_verbose Then
                Console.WriteLine("Current folder changed to parent folder")
            End If
        Catch except As Exception
            Console.WriteLine(except.Message)
        End Try
    End Sub

    Private Sub ChangeTransferMode(ByVal param As String)
        ' Change the data transfer mode on the server
        Dim transferMode As TransferMode

        Select Case param
            Case "S"
            Case "s"
                transferMode = transferMode.Stream
            Case "Z"
            Case "z"
                transferMode = transferMode.ZLibCompressed
            Case Else
                Console.WriteLine("Unknown transfer mode : " + param)
                Return
        End Select

        m_client.ChangeTransferMode(transferMode)

    End Sub

    Private Sub ChangeUser(ByVal userName As String)
        ' Change the currently logged user by calling the 
        ' ChangeUser method of the FtpClient object.

        If userName.Length = 0 Then
            Console.WriteLine("You must provide the new username to login with.")
        Else
            Try
                ' Authenticate if required
                If m_method <> AuthenticationMethod.None Then
                    m_client.Authenticate( _
                      m_method, VerificationFlags.None, Nothing, DataChannelProtection.Private)
                End If

                Console.Write("Please enter password:")
                Dim password As String = Console.ReadLine()

                m_client.ChangeUser(userName, password)

                If m_verbose Then
                    Console.WriteLine("User changed successfully")
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub Connect(ByVal hostName As String)
        ' Connect to the specified host by calling the 
        ' Connect and Login method of the FtpClient object.

        If hostName.Length = 0 Then
            Console.WriteLine("You must provide the FTP server address to connect to.")
        Else
            Try
                Dim userName As String

                ' Connect to the host with the specified host.
                m_client.Connect(hostName)

                ' Authenticate if required
                If m_method <> AuthenticationMethod.None Then
                    m_client.Authenticate( _
                      m_method, VerificationFlags.None, Nothing, DataChannelProtection.Private)
                End If

                Console.Write("Please enter username (none for anonymous login):")
                userName = Console.ReadLine()

                ' Ask for credential.
                If userName.Length = 0 Then
                    Console.WriteLine("Logging-in as anonymous user...")
                    m_client.Login()
                Else
                    Dim password As String

                    Console.Write("Please enter password:")
                    password = Console.ReadLine()

                    m_client.Login(userName, password)
                End If

                If m_verbose Then
                    Console.WriteLine("Login successfully")
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub CreateFolder(ByVal folder As String)
        ' Create a folder in the current folder of the host by calling 
        ' the CreateFolder method of the FtpClient object.

        If folder.Length = 0 Then
            Console.WriteLine("You must provide the remote folder name to create.")
        Else
            Try
                m_client.CreateFolder(folder)

                If m_verbose Then
                    Console.WriteLine("Created folder '{0}'", folder)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub DeleteFile(ByVal fileName As String)
        ' Delete the specified file by calling the 
        ' DeleteFile method of the FtpClient object.

        If fileName.Length = 0 Then
            Console.WriteLine("You must provide the filename to delete.")
        Else
            Try
                m_client.DeleteFile(fileName)

                If m_verbose Then
                    Console.WriteLine("Deleted file {0}", fileName)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub DeleteFolder(ByVal folderName As String)
        ' Delete the specified folder by calling the 
        ' DeleteFolder method of the FtpClient object.

        If folderName.Length = 0 Then
            Console.WriteLine("You must provide the remote folder name to delete.")
        Else
            Try
                m_client.DeleteFolder(folderName)

                If m_verbose Then
                    Console.WriteLine("Deleted folder '{0}'", folderName)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub Disconnect(ByVal param As String)
        ' Disconnect the client from the host by calling 
        ' the Disconnect method of the FtpClient object.

        Me.UselessParam(param)

        If m_client.State = FtpClientState.Connected Then
            Try
                m_client.Disconnect()
                ' OnDisconnected will display message.
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub DisplayCurrentFolder(ByVal param As String)
        ' Display the host's current folder by calling the 
        ' GetCurrentFolder method of the FtpClient object.

        Me.UselessParam(param)

        Try
            Dim current As String

            current = m_client.GetCurrentFolder()

            ' Verbose or not
            Console.WriteLine("Current folder on FTP server is ""{0}""", current)
        Catch except As Exception
            Console.WriteLine(except.Message)
        End Try
    End Sub

    Private Sub DisplayLocalFolderContents(ByVal fileMask As String, ByVal namesOnly As Boolean)
        Try
            ' Get a list of all the folders and display it.
            Dim folders As AbstractFolder()
            Dim folder As AbstractFolder

            If fileMask.Length > 0 Then
                folders = m_localFolder.GetFolders(False, New NameFilter(fileMask, FilterScope.Folder))
            Else
                folders = m_localFolder.GetFolders(False)
            End If

            For Each folder In folders
                If namesOnly Then
                    ' We append a backslash to show it's a folder.
                    Console.WriteLine("{0}\", folder.Name)
                Else
                    ' Date; Time; DIR; Size; Name
                    Console.WriteLine( _
                      "{0,10} {1,8} {2,8} {3,14} {4}", _
                      folder.LastWriteDateTime.ToString("d"), _
                      folder.LastWriteDateTime.ToString("t"), _
                      "<DIR>", _
                      "", _
                      folder.Name)
                End If
            Next

            ' Get a list of all the files and display it.
            Dim files As AbstractFile()
            Dim file As AbstractFile

            If fileMask.Length > 0 Then
                files = m_localFolder.GetFiles(False, fileMask)
            Else
                files = m_localFolder.GetFiles(False)
            End If

            For Each file In files
                If namesOnly Then
                    ' Name
                    Console.WriteLine("{0}", file.Name)
                Else
                    ' Date; Time; DIR; Size; Name
                    Console.WriteLine( _
                      "{0,10} {1,8} {2,8} {3,14} {4}", _
                      file.LastWriteDateTime.ToString("d"), _
                      file.LastWriteDateTime.ToString("t"), _
                      "", _
                      file.Size.ToString("n0"), _
                      file.Name)
                End If
            Next
        Catch except As Exception
            Console.WriteLine(except.Message)
        End Try
    End Sub

    Private Sub DisplayFolderContents(ByVal fileMask As String, ByVal namesOnly As Boolean)
        ' Display a listing of the host's current folder by 
        ' calling the GetRawFolderContents method of the FtpClient object.

        Try
            Dim lines As StringList

            lines = m_client.GetRawFolderContents(fileMask, namesOnly)
            Console.Write(lines.ToString())
        Catch except As Exception
            Console.WriteLine(except.Message)
        End Try
    End Sub

    Private Sub DisplayHelp(ByVal command As String)
        ' Display all the available commands or help on a specified one.

        command = command.ToLower()

        If command.Length = 0 Then
            Dim help As CommandHelp

            For Each help In mg_commands
                Console.Write("{0,-20}", help.Command)
            Next

            Console.WriteLine()
        Else
            Dim help As CommandHelp
            Dim found As Boolean = False

            For Each help In mg_commands
                If help.Command = command Then
                    Console.WriteLine("{0}: {1}", help.Command, help.Help)
                    found = True
                    Exit For
                End If
            Next

            If Not found Then
                Console.WriteLine("Unknown command.")
            End If
        End If
    End Sub

    Private Sub DisplayRemoteHelp(ByVal command As String)
        ' Send custom command "HELP" to get help on a specified command from the server.

        Dim help As String = "HELP"

        If command.Length > 0 Then
            help = help + " " + command
        End If

        Me.SendCustomCommand(help, True)
    End Sub

    Private Sub DisplayStatus(ByVal param As String)
        ' Display the status of the current session.

        Me.UselessParam(param)

        Console.WriteLine("Connected: {0}", m_client.Connected.ToString())
        Console.WriteLine("Transfer mode: {0}", IIf(m_client.RepresentationType = RepresentationType.Ascii, "Ascii", "Image"))
        Console.WriteLine("Debug mode: {0}", m_debug.ToString())
        Console.WriteLine("Prompting: {0}", m_prompt.ToString())
        Console.WriteLine("Verbose mode: {0}", m_verbose.ToString())
        Console.WriteLine("Active local folder: {0}", m_localFolder.FullName)
    End Sub

    Private Sub NotSupportedCommand(ByVal command As String)
        ' Inform the user that the command is not supported.

        Console.WriteLine("The '" + command + "' command is not supported.")
    End Sub

    Private Sub PassiveTransfer(ByVal param As String)
        ' Change data transfer to passive mode.

        Me.UselessParam(param)

        m_client.PassiveTransfer = True

        If m_verbose Then
            Console.WriteLine("Data transfer changed to passive mode.")
        End If
    End Sub

    Private Sub PortTransfer(ByVal param As String)
        ' Change data transfer to non-passive mode (port mode).

        Me.UselessParam(param)

        m_client.PassiveTransfer = False

        If m_verbose Then
            Console.WriteLine("Data transfer changed to port mode.")
        End If
    End Sub

    Private Function ProcessCommand(ByVal line As String) As Boolean
        ' Do the appropriate action depending on the specified command.

        ' First, trap exceptions
        line = line.Trim()

        If line.Length = 0 Then
            Return False
        End If

        ' We only accept commands with a single parameter, or none!
        ' I don't like Split, as it considers consecutive separators
        ' as distinct separators, thus returning me empty parts.
        Dim space As Integer
        space = line.IndexOf(" ")

        Dim command As String = String.Empty
        Dim param As String = String.Empty

        If space >= 0 Then
            command = line.Substring(0, space).ToLower()
            param = line.Substring(space + 1).TrimStart(" ")
        Else
            command = line.ToLower()
        End If

        If command = "append" Then
            Me.SendFile(param, True)
        ElseIf command = "auth" Then
            Me.Authenticate(param)
        ElseIf command = "bell" Then
            Me.NotSupportedCommand(command)
        ElseIf (command = "ascii") Or (command = "asc") Then
            Me.ChangeRepresentationTypeToAscii(param)
        ElseIf (command = "binary") Or (command = "bin") Then
            Me.ChangeRepresentationTypeToBinary(param)
        ElseIf (command = "bye") Or (command = "quit") Then
            Me.Disconnect(param)

            ' When returning true, it means we must quit.
            Return True
        ElseIf command = "cd" Then
            Me.ChangeCurrentFolder(param)
        ElseIf command = "cdup" Then
            Me.ChangeToParentFolder(param)
        ElseIf (command = "close") Or (command = "disconnect") Then
            Me.Disconnect(param)
        ElseIf command = "debug" Then
            m_debug = Not m_debug

            If m_verbose Then
                Console.WriteLine("Debug mode: {0}", m_debug.ToString())
            End If
        ElseIf (command = "delete") Or (command = "del") Then
            Me.DeleteFile(param)
        ElseIf command = "dir" Then
            Me.DisplayFolderContents(param, False)
        ElseIf command = "ldir" Then
            Me.DisplayLocalFolderContents(param, False)
        ElseIf (command = "get") Or (command = "recv") Then
            Me.ReceiveFile(param)
        ElseIf command = "glob" Then
            Me.NotSupportedCommand(command)
        ElseIf command = "hash" Then
            Me.NotSupportedCommand(command)
        ElseIf command = "help" Then
            Me.DisplayHelp(param)
        ElseIf command = "lcd" Then
            Me.ChangeLocalFolder(param)
        ElseIf (command = "literal") Or (command = "quote") Then
            Me.SendCustomCommand(param, False)
        ElseIf command = "ls" Then
            Me.DisplayFolderContents(param, True)
        ElseIf command = "lls" Then
            Me.DisplayLocalFolderContents(param, True)
        ElseIf (command = "mdelete") Or (command = "mdel") Then
            Me.NotSupportedCommand(command)
        ElseIf command = "mdir" Then
            Me.NotSupportedCommand(command)
        ElseIf command = "mget" Then
            Me.ReceiveMultipleFiles(param)
        ElseIf (command = "mkdir") Or (command = "md") Then
            Me.CreateFolder(param)
        ElseIf command = "mls" Then
            Me.NotSupportedCommand(command)
        ElseIf command = "mode" Then
            Me.ChangeTransferMode(param)
        ElseIf command = "mput" Then
            Me.SendMultipleFiles(param)
        ElseIf command = "open" Then
            Me.Connect(param)
        ElseIf command = "passive" Then
            Me.PassiveTransfer(param)
        ElseIf command = "port" Then
            Me.PortTransfer(param)
        ElseIf command = "prompt" Then
            m_prompt = Not m_prompt

            If m_verbose Then
                Console.WriteLine("Prompting on multi-commands: {0}", m_prompt.ToString())
            End If
        ElseIf command = "proxy" Then
            Me.ChangeProxy(param)
        ElseIf (command = "put") Or (command = "send") Then
            Me.SendFile(param, False)
        ElseIf command = "putunique" Then
            Me.SendFileToUniqueName(param)
        ElseIf command = "pwd" Then
            Me.DisplayCurrentFolder(param)
        ElseIf command = "remotehelp" Then
            Me.DisplayRemoteHelp(param)
        ElseIf (command = "rename") Or (command = "ren") Then
            Me.RenameFile(param)
        ElseIf (command = "rmdir") Or (command = "rd") Then
            Me.DeleteFolder(param)
        ElseIf (command = "rrmdir") Or (command = "rrd") Then
            Me.RecursiveDeleteFolder(param)
        ElseIf command = "status" Then
            Me.DisplayStatus(param)
        ElseIf command = "trace" Then
            Me.NotSupportedCommand(command)
        ElseIf command = "type" Then
            Me.ChangeRepresentationType(param)
        ElseIf command = "user" Then
            Me.ChangeUser(param)
        ElseIf command = "verbose" Then
            m_verbose = Not m_verbose

            Console.WriteLine("Verbose mode: {0}", m_verbose.ToString())
        Else
            Console.WriteLine("The '" + command + "' command is unknown.")
        End If

        Return False
    End Function

    Private Sub ProcessInput()
        ' Process all the inputs made by the user.

        Do
            Dim line As String

            Console.Write("ftp> ")

            line = Console.ReadLine()

            If Not line Is Nothing Then
                If Me.ProcessCommand(line) Then
                    Exit Do
                End If
            End If
        Loop
    End Sub

    Private Sub ReceiveFile(ByVal fileName As String)
        ' Download the specified file by calling the ReceiveFile method of the FtpClient object.

        If fileName.Length = 0 Then
            Console.WriteLine("You must provide the remote filename to download.")
        Else
            ' Check local file first
            Try
                Dim file As AbstractFile

                file = m_localFolder.GetFile(fileName)

                If file.Exists Then
                    Dim answer As String

                    Console.Write("File '{0}' already exists. Do you want to replace it? [y/n]", file.FullName)
                    answer = Console.ReadLine()

                    If Not ((answer = "y") Or (answer = "Y")) Then
                        file = Nothing
                    End If
                End If

                Try
                    If Not (file Is Nothing) Then
                        m_client.ReceiveFile(fileName, 0, file.FullName)

                        If m_verbose Then
                            Console.WriteLine("Received file '{0}'", file.FullName)
                        End If
                    End If
                Catch except As Exception
                    Console.WriteLine(except.Message)
                End Try
            Catch
                Console.WriteLine("The provided filename is invalid locally.")
            End Try
        End If
    End Sub

    Private Sub ReceiveMultipleFiles(ByVal fileMask As String)
        ' Download multiple files matching the specified file mask by 
        ' calling the ReceiveMultipleFiles method of the FtpClient object.

        If fileMask.Length = 0 Then
            Console.WriteLine("You must provide the remote filemask to receive.")
        Else
            Try
                m_client.ReceiveMultipleFiles(fileMask, m_localFolder.FullName, True, True)

                If m_verbose Then
                    Console.WriteLine("Multiple files received successfully")
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub RecursiveDeleteFolder(ByVal folderName As String)
        ' Delete the specified folder and all files and subfolder 
        ' by calling the second overload of the DeleteFolder method.

        If folderName.Length = 0 Then
            Console.WriteLine("You must provide the remote folder name to delete.")
        Else
            Try
                m_client.DeleteFolder(folderName, True)

                If m_verbose Then
                    Console.WriteLine("Deleted folder '{0}'", folderName)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub RenameFile(ByVal fileName As String)
        ' Rename the specified file by calling the RenameFile method of the FtpClient object.

        If fileName.Length = 0 Then
            Console.WriteLine("You must provide the remote filename you wish to rename.")
        Else
            Dim newName As String

            Console.Write("Please enter the new name:")
            newName = Console.ReadLine()

            If newName.Length > 0 Then
                Try
                    m_client.RenameFile(fileName, newName)

                    If m_verbose Then
                        Console.WriteLine("The file '{0}' was renamed to '{1}'", fileName, newName)
                    End If
                Catch except As Exception
                    Console.WriteLine(except.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub SendCustomCommand(ByVal command As String, ByVal alwaysShowReply As Boolean)
        ' Send a custom command to the server by calling the 
        ' SendCustomCommand of the FtpClient object.

        If command.Length = 0 Then
            Console.WriteLine("You must provide the custom command to send.")
        Else
            Try
                Dim reply As String

                reply = m_client.SendCustomCommand(command)

                ' This method is also called by the DisplayRemoteHelp method and in that case,
                ' we want to show the reply no matter what!
                If m_verbose Or alwaysShowReply Then
                    Console.WriteLine(reply)
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub SendFile(ByVal fileName As String, ByVal append As Boolean)
        ' Upload the specified file by calling the SendFile method of the FtpClient object.

        If fileName.Length = 0 Then
            Console.WriteLine("You must provide the local filename to upload")
        Else
            Try
                Dim file As AbstractFile

                file = m_localFolder.GetFile(fileName)

                If Not file.Exists Then
                    Console.WriteLine("The provided filename does not exist.")
                Else
                    Try
                        m_client.SendFile(file.FullName, file.Name, append)

                        If m_verbose Then
                            Console.WriteLine("File sent succesfully.")
                        End If
                    Catch except As Exception
                        Console.WriteLine(except.Message)
                    End Try
                End If
            Catch
                Console.WriteLine("The provided filename is invalid.")
            End Try
        End If
    End Sub

    Private Sub SendFileToUniqueName(ByVal fileName As String)
        ' Upload the specified file letting the server generate a name for 
        ' the file by calling the SendFileToUniqueName method of the FtpClient object.

        If fileName.Length = 0 Then
            Console.WriteLine("You must provide the local filename to upload.")
        Else
            Try
                Dim file As AbstractFile

                file = m_localFolder.GetFile(fileName)

                If Not file.Exists Then
                    Console.WriteLine("The provided filename does not exist.")
                Else
                    Try
                        Dim uniqueName As String

                        uniqueName = m_client.SendFileToUniqueName(file.FullName)

                        If m_verbose Then
                            Console.WriteLine("File sent successfully to unique name: {0}", uniqueName)
                        End If
                    Catch except As Exception
                        Console.WriteLine(except.Message)
                    End Try
                End If
            Catch
                Console.WriteLine("The provided filename is invalid.")
            End Try
        End If
    End Sub

    Private Sub SendMultipleFiles(ByVal fileMask As String)
        ' Upload files corresponding to the specified file mask by calling the
        ' SendMultipleFiles method of the FtpClient object.

        If fileMask.Length = 0 Then
            Console.WriteLine("You must provide the local filemask to send.")
        Else
            Try
                m_client.SendMultipleFiles(m_localFolder.FullName + fileMask, True, True)

                If m_verbose Then
                    Console.WriteLine("Multiple files sent successfully")
                End If
            Catch except As Exception
                Console.WriteLine(except.Message)
            End Try
        End If
    End Sub

    Private Sub UselessParam(ByVal param As String)
        ' Inform the user that the parameter was useless.

        If param.Length > 0 Then
            Console.WriteLine("Parameter '{0}' is ignored.", param)
        End If
    End Sub

#End Region

#Region "PRIVATE FIELDS........................................................"
    ' We could have made a nice command processing with objects
    ' but for the moment, command processing is hardcoded, and
    ' command help is here.
    Private Class CommandHelp
        Public Sub New(ByVal _command As String, ByVal _help As String)
            Command = _command
            Help = _help
        End Sub

        Public Command As String
        Public Help As String
    End Class

    Private mg_commands() As CommandHelp = { _
        New CommandHelp("append", "Append a local file at the end of a remote file."), _
        New CommandHelp("auth", "Secure the command connection. Valid parameters are ""none"", ""ssl"" or ""tls""."), _
        New CommandHelp("bell", "This command is not supported."), _
        New CommandHelp("ascii", "Transfer files in ASCII mode."), _
        New CommandHelp("binary", "Transfer files in IMAGE mode."), _
        New CommandHelp("bye", "Disconnect from the FTP server and quit this application."), _
        New CommandHelp("quit", "Disconnect from the FTP server and quit this application."), _
        New CommandHelp("cd", "Change the current folder on the FTP server."), _
        New CommandHelp("cdup", "Change the current folder to the parent folder on the FTP server."), _
        New CommandHelp("close", "Disconnect from the FTP server."), _
        New CommandHelp("disconnect", "Disconnect from the FTP server."), _
        New CommandHelp("debug", "Turn ON/OFF display of sent commands and received replies."), _
        New CommandHelp("delete", "Delete a file on the FTP server."), _
        New CommandHelp("dir", "Get a complete listing of a folder's contents on the FTP server."), _
        New CommandHelp("ldir", "Get a complete listing of a local folder's contents."), _
        New CommandHelp("get", "Download a file from the FTP server."), _
        New CommandHelp("recv", "Download a file from the FTP server."), _
        New CommandHelp("glob", "This command is not supported."), _
        New CommandHelp("hash", "This command is not supported."), _
        New CommandHelp("help", "Display all available commands with this application."), _
        New CommandHelp("lcd", "Change the local current folder."), _
        New CommandHelp("literal", "Send a custom command to the FTP server."), _
        New CommandHelp("quote", "Send a custom command to the FTP server."), _
        New CommandHelp("ls", "Get filenames only of a folder's contents on the FTP server."), _
        New CommandHelp("lls", "Get filenames only of a local folder's."), _
        New CommandHelp("mdelete", "This command is not supported."), _
        New CommandHelp("mdir", "This command is not supported."), _
        New CommandHelp("mget", "Download multiple files from the FTP server."), _
        New CommandHelp("mkdir", "Create a new folder on the FTP server."), _
        New CommandHelp("md", "Create a new folder on the FTP server."), _
        New CommandHelp("mls", "This command is not supported."), _
        New CommandHelp("mode", "Change the data transfer mode."), _
        New CommandHelp("mput", "Upload multiple files to the FTP server."), _
        New CommandHelp("open", "Open a new connection to an FTP server."), _
        New CommandHelp("prompt", "Toggle prompting ON/OFF for multiple-type commands."), _
        New CommandHelp("proxy", "Tell the application to connect via a specified HTTP proxy address and port."), _
        New CommandHelp("put", "Upload a file to the FTP server."), _
        New CommandHelp("putunique", "Upload a file to the FTP server into a unique filename."), _
        New CommandHelp("send", "Upload a file to the FTP server."), _
        New CommandHelp("pwd", "Get the current active folder on the FTP server."), _
        New CommandHelp("remotehelp", "Get a list of supported FTP commands on the FTP server. Don't confuse this with the commands this application accepts."), _
        New CommandHelp("rename", "Rename a file on the FTP server."), _
        New CommandHelp("rmdir", "Remove a folder on the FTP server."), _
        New CommandHelp("rrmdir", "Recursively remove a folder and all files and subfolders within that folder."), _
        New CommandHelp("rd", "Remove a folder on the FTP server."), _
        New CommandHelp("rrd", "Recursively remove a folder and all files and subfolders within that folder."), _
        New CommandHelp("status", "Get the values of available options with this application."), _
        New CommandHelp("trace", "This command is not supported."), _
        New CommandHelp("type", "Change the transfer mode of files to the FTP server (Ascii or Image)."), _
        New CommandHelp("user", "Change the currently logged-in user on the FTP server."), _
        New CommandHelp("verbose", "Display messages after each command.")}

    Private WithEvents m_client As New FtpClient()

    Private m_localFolder As AbstractFolder = New DiskFolder(".")

    Private m_debug As Boolean = True
    Private m_prompt As Boolean = True
    Private m_verbose As Boolean = True
    Private m_method As AuthenticationMethod = AuthenticationMethod.None

#End Region

End Class
