/*
 * Xceed FTP for .NET - ConsoleFtp Sample Application
 * Copyright (c) 2003 - Xceed Software Inc.
 * 
 * [Application.cs]
 * 
 * This application demonstrate how to use the Xceed FTP object model
 * in a generic way.
 * 
 * This file is part of Xceed FTP for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using Xceed.Ftp;
using Xceed.Ftp.Engine;
using Xceed.FileSystem;
using Xceed.Utils.Collections;

namespace Xceed.Ftp.Samples.ConsoleFtp
{
  class Application
  {
    //=========================================================================
    #region CONSTRUCTORS

    private Application()
    {
      // Subscribe to the events of the FTP engine.
      m_client.CertificateReceived += new CertificateReceivedEventHandler( this.OnCertificateReceived );
      m_client.CommandSent += new CommandSentEventHandler( this.OnClientCommandSent );
      m_client.Disconnected += new EventHandler( this.OnDisconnected );
      m_client.FileTransferStatus += new FileTransferStatusEventHandler( this.OnFileTransferStatus );
      m_client.MultipleFileTransferError += new MultipleFileTransferErrorEventHandler( this.OnMultipleFileTransferError );
      m_client.ReceivingFile += new TransferringFileEventHandler( this.OnReceivingFile );
      m_client.ReplyReceived += new ReplyReceivedEventHandler( this.OnServerReplyReceived );
      m_client.SendingFile += new TransferringFileEventHandler( this.OnSendingFile );
    }

    #endregion CONSTRUCTORS
    //=========================================================================
    #region EVENT HANDLERS

    private void OnCertificateReceived( object sender, CertificateReceivedEventArgs e )
    {
      Console.WriteLine( "The following certificate was received:" );
      Console.WriteLine( e.ServerCertificate.ToString() );
      Console.WriteLine( "It has been verified as {0}.", e.Status.ToString() );

      Console.WriteLine( "\nDo you want to accept this certifinate? [Y/N]" );
      string answer = Console.ReadLine();

      if( answer.ToUpper().StartsWith( "Y" ) )
      {
        e.Action = VerificationAction.Accept;
      }
      else
      {
        e.Action = VerificationAction.Reject;
      }
    }

    private void OnClientCommandSent( object sender, CommandSentEventArgs e )
    {
      if( m_debug )
      {
        Console.WriteLine( " -> {0,-70}", e.Command );
      }
    }

    private void OnDisconnected( object sender, EventArgs e )
    {
      if( m_verbose )
      {
        Console.WriteLine( "Disconnected from server." );
      }
    }

    private void OnFileTransferStatus( object sender, FileTransferStatusEventArgs e )
    {
      if( m_verbose )
      {
        Console.Write( "{0}% ({1}kb/s)     \r", e.BytesPercent.ToString(), ( e.BytesPerSecond / 1024 ).ToString() );

        if( e.BytesPercent == 100 )
        {
          Console.WriteLine();
        }
      }
    }

    private void OnMultipleFileTransferError( object sender, MultipleFileTransferErrorEventArgs e )
    {
      if( m_verbose )
      {
        Console.WriteLine( "Skipping file '{0}'\n  {1}", 
          e.LocalItemName, e.Exception.Message );
      }

      e.Action = MultipleFileTransferErrorAction.Ignore;
    }

    private void OnReceivingFile( object sender, TransferringFileEventArgs e )
    {
      if( m_verbose )
      {
        Console.WriteLine( "Receiving file '{0}'", e.RemoteFilename );
      }
    }

    private void OnSendingFile( object sender, TransferringFileEventArgs e )
    {
      if( m_verbose )
      {
        Console.WriteLine( "Sending file '{0}'", e.LocalFilename );
      }
    }

    private void OnServerReplyReceived( object sender, ReplyReceivedEventArgs e )
    {
      if( m_debug )
      {
        // Since we want to prepend "<-" in front of each line, we'll use
        // the reply's Lines property.
        foreach( string line in e.Reply.Lines )
        {
          Console.WriteLine( " <- {0,-70}", line );
        }
      }
    }

    #endregion EVENT HANDLERS
    //=========================================================================
    #region PRIVATE METHODS

    private void Authenticate( string methodName )
    {
      // Change the authentication method to apply when connecting.

      if( methodName.Length == 0 )
      {
        Console.WriteLine( "You must provide one of the three following authentication methods: none, ssl, tls." );
      }
      else
      {
        methodName = methodName.ToLower();

        if( methodName == "none" )
        {
          m_method = AuthenticationMethod.None;
        }
        else if( methodName == "ssl" )
        {
          m_method = AuthenticationMethod.Ssl;
        }
        else if( methodName == "tls" )
        {
          m_method = AuthenticationMethod.Tls;
        }
        else
        {
          Console.WriteLine( "Unknown authentication method." );
          Console.WriteLine( "You must provide one of the three following authentication methods: none, ssl, tls." );
          return;
        }

        if( m_verbose )
        {
          Console.WriteLine( "Authentication method changed to {0}.", m_method.ToString() );

          if( m_client.Connected )
          {
            Console.WriteLine( "These changes will apply to the next connection or login." );
          }
        }
      }
    }

    private void ChangeCurrentFolder( string folderName )
    {
      // Change the current folder on the remote side by calling 
      // the ChangeCurrentFolder method of the FtpClient object.

      if( folderName.Length == 0 )
      {
        this.DisplayCurrentFolder( folderName );
      }
      else
      {
        try
        {
          m_client.ChangeCurrentFolder( folderName );

          if( m_verbose )
            Console.WriteLine( "Current folder changed to '{0}'", folderName );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void ChangeLocalFolder( string folderName )
    {
      // Change the current local folder.

      if( folderName.Length == 0 )
      {
        Console.WriteLine( "The active local folder is: {0}", m_localFolder.FullName );
      }
      else
      {
        try
        {
          // Try to get a folder from a relative path.
          AbstractFolder newFolder = m_localFolder.GetFolder( folderName );

          if( newFolder.Exists )
          {
            m_localFolder = newFolder;
          }
          else
          {
            Console.WriteLine( "The folder '{0}' does not exits.", folderName );
          }
        }
        catch
        {
          // Try to get a folder from an absolute path.
          try
          {
            AbstractFolder newFolder = new DiskFolder( folderName );

            if( newFolder.Exists )
            {
              m_localFolder = newFolder;
            }
            else
            {
              Console.WriteLine( "The folder '{0}' does not exits.", folderName );
            }
          }
          catch
          {
            Console.WriteLine( "Cannot change current local folder to '{0}'", folderName );
          }
        }
        finally
        {
          if( m_verbose )
            Console.WriteLine( "The active local folder is: {0}", m_localFolder.FullName );
        }
      }
    }

    private void ChangeProxy( string param )
    {
      string[] parts = ( param.Length == 0 ) ? new string[ 0 ] : param.Split( ' ' );

      try
      {
        switch( parts.Length )
        {
          case 0:
            m_client.Proxy = null;

            if( m_verbose )
              Console.WriteLine( "Not using HTTP proxies." );

            break;

          case 1:
            m_client.Proxy = new HttpProxyClient( parts[ 0 ] );

            if( m_verbose )
              Console.WriteLine( "Using HTTP proxy {0}:{1} without credentials.", 
                m_client.Proxy.HostName, m_client.Proxy.Port );

            break;

          case 2:
            m_client.Proxy = new HttpProxyClient( parts[ 0 ], parts[ 1 ], string.Empty );

            if( m_verbose )
              Console.WriteLine( "Using HTTP proxy {0}:{1} with user {2}.", 
                m_client.Proxy.HostName, m_client.Proxy.Port, parts[ 1 ] );

            break;

          case 3:
            m_client.Proxy = new HttpProxyClient( parts[ 0 ], parts[ 1 ], parts[ 2 ] );

            if( m_verbose )
              Console.WriteLine( "Using HTTP proxy {0}:{1} with user {2} and a password.", 
                m_client.Proxy.HostName, m_client.Proxy.Port, parts[ 1 ] );

            break;

          default:
            throw new ArgumentException( "Invalid number of parameters." );
        }
      }
      catch( Exception except )
      {
        Console.WriteLine( except.Message );
        Console.WriteLine( "The format for the 'proxy' command is:" );
        Console.WriteLine( "  proxy [proxy_address[:port] [username [password]]" );
      }
    }

    private void ChangeRepresentationType( string param )
    {
      // Change the representation type depending on the specified parameter.

      param = param.ToLower();

      if( param == "a" || param == "ascii" )
      {
        this.ChangeRepresentationTypeToAscii( "" );
      }
      else if( param == "i" || param == "image" || param == "bin" || param == "binary" )
      {
        this.ChangeRepresentationTypeToBinary( "" );
      }
      else
      {
        Console.WriteLine( "Unknown type format" );
      }
    }

    private void ChangeRepresentationTypeToAscii( string param )
    {
      // Change the RepresentationType property of the FtpClient object to ASCII.

      this.UselessParam( param );

      m_client.RepresentationType = RepresentationType.Ascii;

      if( m_verbose )
        Console.WriteLine( "Type changed to ASCII" );
    }

    private void ChangeRepresentationTypeToBinary( string param )
    {
      // Change the RepresentationType property of the FtpClient object to BINARY.

      this.UselessParam( param );

      m_client.RepresentationType = RepresentationType.Binary;

      if( m_verbose )
        Console.WriteLine( "Type changed to BINARY" );
    }

    private void ChangeTransferMode( string param )
    {
      // Change the data transfer mode on the server
      TransferMode transferMode = TransferMode.Stream;

      switch( param )
      {
        case "S":
        case "s":
          transferMode = TransferMode.Stream;
          break;
        case "Z":
        case "z":
          transferMode = TransferMode.ZLibCompressed;
          break;
        default:
          Console.WriteLine( "Unknown transfer mode : " + param );
          return;
      }
      
      m_client.ChangeTransferMode( transferMode );
    }

    private void ChangeToParentFolder( string param )
    {
      // Change the current folder on the remote side to the parent folder
      // by calling the ChangeToParentFolder method of the FtpClient object.

      this.UselessParam( param );

      try
      {
        m_client.ChangeToParentFolder();

        if( m_verbose )
          Console.WriteLine( "Current folder changed to parent folder" );
      }
      catch( Exception except )
      {
        Console.WriteLine( except.Message );
      }
    }

    private void ChangeUser( string userName )
    {
      // Change the currently logged user by calling the 
      // ChangeUser method of the FtpClient object.

      if( userName.Length == 0 )
      {
        Console.WriteLine( "You must provide the new username to login with." );
      }
      else
      {
        try
        {
          // Authenticate if required
          if( m_method != AuthenticationMethod.None )
          {
            m_client.Authenticate( 
              m_method, 
              VerificationFlags.None, 
              null, 
              DataChannelProtection.Private );
          }

          Console.Write( "Please enter password:" );
          string password = Console.ReadLine();

          m_client.ChangeUser( userName, password );

          if( m_verbose )
            Console.WriteLine( "User changed successfully" );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void Connect( string hostName )
    {
      // Connect to the specified host by calling the 
      // Connect and Login method of the FtpClient object.

      if( hostName.Length == 0 )
      {
        Console.WriteLine( "You must provide the FTP server address to connect to." );
      }
      else
      {
        try
        {
          // Connect to the host with the specified host.
          m_client.Connect( hostName );

          // Authenticate if required
          if( m_method != AuthenticationMethod.None )
          {
            m_client.Authenticate( 
              m_method, 
              VerificationFlags.None, 
              null,
              DataChannelProtection.Private );
          }

          Console.Write( "Please enter username (none for anonymous login):" );
          string userName = Console.ReadLine();

          // Ask for credential.
          if( userName.Length == 0 )
          {
            Console.WriteLine( "Logging-in as anonymous user..." );
            m_client.Login();
          }
          else
          {
            Console.Write( "Please enter password:" );
            string password = Console.ReadLine();

            m_client.Login( userName, password );
          }

          if( m_verbose )
            Console.WriteLine( "Login successfully" );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void CreateFolder( string folder )
    {
      // Create a folder in the current folder of the host by calling 
      // the CreateFolder method of the FtpClient object.

      if( folder.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote folder name to create." );
      }
      else
      {
        try
        {
          m_client.CreateFolder( folder );

          if( m_verbose )
            Console.WriteLine( "Created folder '{0}'", folder );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void DeleteFile( string fileName )
    {
      // Delete the specified file by calling the 
      // DeleteFile method of the FtpClient object.

      if( fileName.Length == 0 )
      {
        Console.WriteLine( "You must provide the filename to delete." );
      }
      else
      {
        try
        {
          m_client.DeleteFile( fileName );

          if( m_verbose )
            Console.WriteLine( "Deleted file {0}", fileName );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void DeleteFolder( string folderName )
    {
      // Delete the specified folder by calling the 
      // DeleteFolder method of the FtpClient object.

      if( folderName.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote folder name to delete." );
      }
      else
      {
        try
        {
          m_client.DeleteFolder( folderName );

          if( m_verbose )
            Console.WriteLine( "Deleted folder '{0}'", folderName );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void Disconnect( string param )
    {
      // Disconnect the client from the host by calling 
      // the Disconnect method of the FtpClient object.

      this.UselessParam( param );

      if( m_client.State == FtpClientState.Connected )
      {
        try
        {
          m_client.Disconnect();
          // OnDisconnected will display message.
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void DisplayCurrentFolder( string param )
    {
      // Display the host's current folder by calling the 
      // GetCurrentFolder method of the FtpClient object.

      this.UselessParam( param );

      try
      {
        string current = m_client.GetCurrentFolder();

        // Verbose or not
        Console.WriteLine( "Current folder on FTP server is \"{0}\"", current );
      }
      catch( Exception except )
      {
        Console.WriteLine( except.Message );
      }
    }

    private void DisplayLocalFolderContents( string fileMask, bool namesOnly )
    {
      try
      {
        // Get a list of all the folders and display it.
        AbstractFolder[] folders = null;

        if( fileMask.Length > 0 )
        {
          folders = m_localFolder.GetFolders( false, new NameFilter( fileMask, FilterScope.Folder ) );
        }
        else
        {
          folders = m_localFolder.GetFolders( false );
        }

        foreach( AbstractFolder folder in folders )
        {
          if( namesOnly )
          {
            // We append a backslash to show it's a folder.
            Console.WriteLine( @"{0}\", folder.Name );
          }
          else
          {
            // Date; Time; DIR; Size; Name
            Console.WriteLine( 
              "{0,10} {1,8} {2,8} {3,14} {4}", 
              folder.LastWriteDateTime.ToString( "d" ), 
              folder.LastWriteDateTime.ToString( "t" ), 
              "<DIR>",
              "", 
              folder.Name );
          }
        }

        // Get a list of all the files and display it.
        AbstractFile[] files = null;

        if( fileMask.Length > 0 )
        {
          files = m_localFolder.GetFiles( false, fileMask );
        }
        else
        {
          files = m_localFolder.GetFiles( false );
        }

        foreach( AbstractFile file in files )
        {
          if( namesOnly )
          {
            // Name
            Console.WriteLine( "{0}", file.Name );
          }
          else
          {
            // Date; Time; DIR; Size; Name
            Console.WriteLine( 
              "{0,10} {1,8} {2,8} {3,14} {4}", 
              file.LastWriteDateTime.ToString( "d" ), 
              file.LastWriteDateTime.ToString( "t" ), 
              "",
              file.Size.ToString( "n0" ), 
              file.Name );
          }
        }
      }
      catch( Exception except )
      {
        Console.WriteLine( except.Message );
      }
    }

    private void DisplayFolderContents( string fileMask, bool namesOnly )
    {
      // Display a listing of the host's current folder by 
      // calling the GetRawFolderContents method of the FtpClient object.

      try
      {
        StringList lines = m_client.GetRawFolderContents( fileMask, namesOnly );
        Console.Write( lines.ToString() );
      }
      catch( Exception except )
      {
        Console.WriteLine( except.Message );
      }
    }

    private void DisplayHelp( string command )
    {
      // Display all the available commands or help on a specified one.

      command = command.ToLower();

      if( command.Length == 0 )
      {
        foreach( CommandHelp help in mg_commands )
        {
          Console.Write( "{0,-20}", help.Command );
        }

        Console.WriteLine();
      }
      else
      {
        bool found = false;

        foreach( CommandHelp help in mg_commands )
        {
          if( help.Command == command )
          {
            Console.WriteLine( "{0}: {1}", help.Command, help.Help );
            found = true;
            break;
          }
        }

        if( !found )
        {
          Console.WriteLine( "Unknown command." );
        }
      }
    }

    private void DisplayRemoteHelp( string command )
    {
      // Send custom command "HELP" to get help on a specified command from the server.

      string help = "HELP";

      if( command.Length > 0 )
        help += " " + command;

      this.SendCustomCommand( help, true );
    }

    private void DisplayStatus( string param )
    {
      // Display the status of the current session.

      this.UselessParam( param );

      Console.WriteLine( "Connected: {0}", m_client.Connected.ToString() );
      Console.WriteLine( "Transfer mode: {0}", ( m_client.RepresentationType == RepresentationType.Ascii ) ? "Ascii" : "Image" );
      Console.WriteLine( "Debug mode: {0}", m_debug.ToString() );
      Console.WriteLine( "Prompting: {0}", m_prompt.ToString() );
      Console.WriteLine( "Verbose mode: {0}", m_verbose.ToString() );
      Console.WriteLine( "Active local folder: {0}", m_localFolder.FullName );
    }

    private void NotSupportedCommand( string command )
    {
      // Inform the user that the command is not supported.

      Console.WriteLine( "The '" + command + "' command is not supported." );
    }

    private void PassiveTransfer( string param )
    {
      // Change data transfer to passive mode.

      this.UselessParam( param );

      m_client.PassiveTransfer = true;

      if( m_verbose )
        Console.WriteLine( "Data transfer changed to passive mode." );
    }

    private void PortTransfer( string param )
    {
      // Change data transfer to non-passive mode (port mode).

      this.UselessParam( param );

      m_client.PassiveTransfer = false;

      if( m_verbose )
        Console.WriteLine( "Data transfer changed to port mode." );
    }

    private bool ProcessCommand( string line )
    {
      // Do the appropriate action depending on the specified command.

      // First, trap exceptions
      line = line.Trim();

      if( line.Length == 0 )
        return false;

      // We only accept commands with a single parameter, or none!
      // I don't like Split, as it considers consecutive separators
      // as distinct separators, thus returning me empty parts.
      int space = line.IndexOf( ' ' );
      string command = string.Empty;
      string param = string.Empty;

      if( space >= 0 )
      {
        command = line.Substring( 0, space ).ToLower();
        param = line.Substring( space + 1 ).TrimStart( ' ' );
      }
      else
      {
        command = line.ToLower();
      }

      if( command == "append" )
      {
        this.SendFile( param, true );
      }
      else if( command == "auth" )
      {
        this.Authenticate( param );
      }
      else if( command == "bell" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "ascii" || command == "asc" )
      {
        this.ChangeRepresentationTypeToAscii( param );
      }
      else if( command == "binary" || command == "bin" )
      {
        this.ChangeRepresentationTypeToBinary( param );
      }
      else if( command == "bye" || command == "quit" )
      {
        this.Disconnect( param );

        // When returning true, it means we must quit.
        return true;
      }
      else if( command == "cd" )
      {
        this.ChangeCurrentFolder( param );
      }
      else if( command == "cdup" )
      {
        this.ChangeToParentFolder( param );
      }
      else if( command == "close" || command == "disconnect" )
      {
        this.Disconnect( param );
      }
      else if( command == "debug" )
      {
        m_debug = !m_debug;

        if( m_verbose )
          Console.WriteLine( "Debug mode: {0}", m_debug.ToString() );
      }
      else if( command == "delete" || command == "del" )
      {
        this.DeleteFile( param );
      }
      else if( command == "dir" )
      {
        this.DisplayFolderContents( param, false );
      }
      else if( command == "ldir" )
      {
        this.DisplayLocalFolderContents( param, false );
      }
      else if( command == "get" || command == "recv" )
      {
        this.ReceiveFile( param );
      }
      else if( command == "glob" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "hash" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "help" )
      {
        this.DisplayHelp( param );
      }
      else if( command == "lcd" )
      {
        this.ChangeLocalFolder( param );
      }
      else if( command == "literal" || command == "quote" )
      {
        this.SendCustomCommand( param, false );
      }
      else if( command == "ls" )
      {
        this.DisplayFolderContents( param, true );
      }
      else if( command == "lls" )
      {
        this.DisplayLocalFolderContents( param, true );
      }
      else if( command == "mdelete" || command == "mdel" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "mdir" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "mget" )
      {
        this.ReceiveMultipleFiles( param );
      }
      else if( command == "mkdir" || command == "md" )
      {
        this.CreateFolder( param );
      }
      else if( command == "mls" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "mode" )
      {
        this.ChangeTransferMode( param );
      }
      else if( command == "mput" )
      {
        this.SendMultipleFiles( param );
      }
      else if( command == "open" )
      {
        this.Connect( param );
      }
      else if( command == "passive" )
      {
        this.PassiveTransfer( param );
      }
      else if( command == "port" )
      {
        this.PortTransfer( param );
      }
      else if( command == "prompt" )
      {
        m_prompt = !m_prompt;

        if( m_verbose )
          Console.WriteLine( "Prompting on multi-commands: {0}", m_prompt.ToString() );
      }
      else if( command == "proxy" )
      {
        this.ChangeProxy( param );
      }
      else if( command == "put" || command == "send" )
      {
        this.SendFile( param, false );
      }
      else if( command == "putunique" )
      {
        this.SendFileToUniqueName( param );
      }
      else if( command == "pwd" )
      {
        this.DisplayCurrentFolder( param );
      }
      else if( command == "remotehelp" )
      {
        this.DisplayRemoteHelp( param );
      }
      else if( command == "rename" || command == "ren" )
      {
        this.RenameFile( param );
      }
      else if( command == "rmdir" || command == "rd" )
      {
        this.DeleteFolder( param );
      }
      else if( command == "rrmdir" || command == "rrd" )
      {
        this.RecursiveDeleteFolder( param );
      }
      else if( command == "status" )
      {
        this.DisplayStatus( param );
      }
      else if( command == "trace" )
      {
        this.NotSupportedCommand( command );
      }
      else if( command == "type" )
      {
        this.ChangeRepresentationType( param );
      }
      else if( command == "user" )
      {
        this.ChangeUser( param );
      }
      else if( command == "verbose" )
      {
        m_verbose = !m_verbose;

        Console.WriteLine( "Verbose mode: {0}", m_verbose.ToString() );
      }
      else
      {
        Console.WriteLine( "The '" + command + "' command is unknown." );
      }

      return false;
    }

    private void ProcessInput()
    {
      // Process all the inputs made by the user.

      do
      {
        Console.Write( "ftp> " );

        string line = Console.ReadLine();

        if( line != null )
        {
          if( this.ProcessCommand( line ) )
            break;
        }

      } while( true );
    }

    private void ReceiveFile( string fileName )
    {
      // Download the specified file by calling the ReceiveFile method of the FtpClient object.

      if( fileName.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote filename to download." );
      }
      else
      {
        // Check local file first
        try
        {
          AbstractFile file = m_localFolder.GetFile( fileName );

          if( file.Exists )
          {
            Console.Write( "File '{0}' already exists. Do you want to replace it? [y/n]", file.FullName );
            int answer = Console.Read();

            if( answer != 'y' && answer != 'Y' )
              file = null;
          }

          if( file != null )
          {
            try
            {
              m_client.ReceiveFile( fileName, 0, file.FullName );

              if( m_verbose )
                Console.WriteLine( "Received file '{0}'", file.FullName );
            }
            catch( Exception except )
            {
              Console.WriteLine( except.Message );
            }
          }
        }
        catch
        {
          Console.WriteLine( "The provided filename is invalid locally." );
        }
      }
    }

    private void ReceiveMultipleFiles( string fileMask )
    {
      // Download multiple files matching the specified file mask by 
      // calling the ReceiveMultipleFiles method of the FtpClient object.

      if( fileMask.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote filemask to receive." );
      }
      else
      {
        try
        {
          m_client.ReceiveMultipleFiles( fileMask, m_localFolder.FullName, true, true );

          if( m_verbose )
            Console.WriteLine( "Multiple files received successfully" );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void RecursiveDeleteFolder( string folderName )
    {
      // Delete the specified folder and all files and subfolder 
      // by calling the second overload of the DeleteFolder method.

      if( folderName.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote folder name to delete." );
      }
      else
      {
        try
        {
          m_client.DeleteFolder( folderName, true );

          if( m_verbose )
            Console.WriteLine( "Deleted folder '{0}'", folderName );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void RenameFile( string fileName )
    {
      // Rename the specified file by calling the RenameFile method of the FtpClient object.

      if( fileName.Length == 0 )
      {
        Console.WriteLine( "You must provide the remote filename you wish to rename." );
      }
      else
      {
        Console.Write( "Please enter the new name:" );
        string newName = Console.ReadLine();

        if( newName.Length > 0 )
        {
          try
          {
            m_client.RenameFile( fileName, newName );

            if( m_verbose )
              Console.WriteLine( "The file '{0}' was renamed to '{1}'", fileName, newName );
          }
          catch( Exception except )
          {
            Console.WriteLine( except.Message );
          }
        }
      }
    }

    private void Run( string hostName )
    {
      // If a host address is specified, we open a session on it. 
      // We then start pumping the user inputs.

      if( hostName.Length > 0 )
      {
        this.ProcessCommand( "open " + hostName );
      }

      this.ProcessInput();
    }

    private void SendCustomCommand( string command, bool alwaysShowReply )
    {
      // Send a custom command to the server by calling the 
      // SendCustomCommand of the FtpClient object.

      if( command.Length == 0 )
      {
        Console.WriteLine( "You must provide the custom command to send." );
      }
      else
      {
        try
        {
          string reply = m_client.SendCustomCommand( command );

          // This method is also called by the DisplayRemoteHelp method and in that case,
          // we want to show the reply no matter what!
          if( m_verbose || alwaysShowReply )
            Console.WriteLine( reply );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void SendFile( string fileName, bool append )
    {
      // Upload the specified file by calling the SendFile method of the FtpClient object.

      if( fileName.Length == 0 )
      {
        Console.WriteLine( "You must provide the local filename to upload" );
      }
      else
      {
        try
        {
          AbstractFile file = m_localFolder.GetFile( fileName );

          if( !file.Exists )
          {
            Console.WriteLine( "The provided filename does not exist." );
          }
          else
          {
            try
            {
              m_client.SendFile( file.FullName, file.Name, append );

              if( m_verbose )
                Console.WriteLine( "File sent succesfully." );
            }
            catch( Exception except )
            {
              Console.WriteLine( except.Message );
            }
          }
        }
        catch
        {
          Console.WriteLine( "The provided filename is invalid." );
        }
      }
    }

    private void SendFileToUniqueName( string fileName )
    {
      // Upload the specified file letting the server generate a name for 
      // the file by calling the SendFileToUniqueName method of the FtpClient object.

      if( fileName.Length == 0 )
      {
        Console.WriteLine( "You must provide the local filename to upload." );
      }
      else
      {
        try
        {
          AbstractFile file = m_localFolder.GetFile( fileName );

          if( !file.Exists )
          {
            Console.WriteLine( "The provided filename does not exist." );
          }
          else
          {
            try
            {
              string uniqueName = m_client.SendFileToUniqueName( file.FullName );

              if( m_verbose )
                Console.WriteLine( "File sent successfully to unique name: {0}", uniqueName );
            }
            catch( Exception except )
            {
              Console.WriteLine( except.Message );
            }
          }
        }
        catch
        {
          Console.WriteLine( "The provided filename is invalid." );
        }
      }
    }

    private void SendMultipleFiles( string fileMask )
    {
      // Upload files corresponding to the specified file mask by calling the
      // SendMultipleFiles method of the FtpClient object.

      if( fileMask.Length == 0 )
      {
        Console.WriteLine( "You must provide the local filemask to send." );
      }
      else
      {
        try
        {
          m_client.SendMultipleFiles( m_localFolder.FullName + fileMask, true, true );

          if( m_verbose )
            Console.WriteLine( "Multiple files sent successfully" );
        }
        catch( Exception except )
        {
          Console.WriteLine( except.Message );
        }
      }
    }

    private void UselessParam( string param )
    {
      // Inform the user that the parameter was useless.

      if( param.Length > 0 )
        Console.WriteLine( "Parameter '{0}' is ignored.", param );
    }

    #endregion PRIVATE METHODS
    //=========================================================================
    #region STATIC METHODS

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      /* ================================
       * How to license Xceed components 
       * ================================       
       * To license your product, set the LicenseKey property to a valid trial or registered license key 
       * in the main entry point of the application to ensure components are licensed before any of the 
       * component methods are called.      
       * 
       * If the component is used in a DLL project (no entry point available), it is 
       * recommended that the LicenseKey property be set in a static constructor of a 
       * class that will be accessed systematically before any component is instantiated or, 
       * you can simply set the LicenseKey property immediately BEFORE you instantiate 
       * an instance of the component.
       * 
       * For instance, if you wanted to deploy this sample, the license key needs to be set in the Main() method.
       * If your trial period has expired, you must purchase a registered license key,
       * uncomment the next line of code, and insert your registerd license key.
       * For more information, consult the "How the 45-day trial works" and the 
       * "How to license the component once you purchase" topics in the documentation of this product.
       */
        
      // Xceed.Ftp.Licenser.LicenseKey = "FTNXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 

      if( args.Length > 1 )
      {
        // We only accept the server address as a parameter.
        Console.WriteLine( "Invalid command-line arguments." );
        Console.WriteLine( "Format: CONSOLEFTP {ftp_server_address}" );
        return;
      }

      ShowWelcomeMessage();

      Application app = new Application();
      app.Run( ( args.Length == 0 ) ? string.Empty : args[0] );

      ShowGoodbyeMessage();
    }

    private static void ShowWelcomeMessage()
    {
      Console.WriteLine( "Welcome to Xceed Console FTP sample application." );
    }

    private static void ShowGoodbyeMessage()
    {
      Console.WriteLine( "Thank you for trying Xceed FTP for .NET" );
    }

    #endregion STATIC METHODS
    //=========================================================================
    #region PRIVATE FIELDS

    // We could have made a nice command processing with objects
    // but for the moment, command processing is hardcoded, and
    // command help is here.
    private struct CommandHelp
    {
      public CommandHelp( string command, string help )
      {
        Command = command;
        Help = help;
      }

      public string Command;
      public string Help;
    }

    private static CommandHelp[] mg_commands = new CommandHelp[]
      {
        new CommandHelp( "append", "Append a local file at the end of a remote file." ),
        new CommandHelp( "auth", "Secure the command connection. Valid parameters are \"none\", \"ssl\" or \"tls\"." ), 
        new CommandHelp( "bell", "This command is not supported." ),
        new CommandHelp( "ascii", "Transfer files in ASCII mode." ),
        new CommandHelp( "binary", "Transfer files in IMAGE mode." ),
        new CommandHelp( "bye", "Disconnect from the FTP server and quit this application." ),
        new CommandHelp( "quit", "Disconnect from the FTP server and quit this application." ),
        new CommandHelp( "cd", "Change the current folder on the FTP server." ),
        new CommandHelp( "cdup", "Change the current folder to the parent folder on the FTP server." ),
        new CommandHelp( "close", "Disconnect from the FTP server." ),
        new CommandHelp( "disconnect", "Disconnect from the FTP server." ),
        new CommandHelp( "debug", "Turn ON/OFF display of sent commands and received replies." ),
        new CommandHelp( "delete", "Delete a file on the FTP server." ),
        new CommandHelp( "dir", "Get a complete listing of a folder's contents on the FTP server." ),
        new CommandHelp( "ldir", "Get a complete listing of a local folder's contents." ),
        new CommandHelp( "get", "Download a file from the FTP server." ),
        new CommandHelp( "recv", "Download a file from the FTP server." ),
        new CommandHelp( "glob", "This command is not supported." ),
        new CommandHelp( "hash", "This command is not supported." ),
        new CommandHelp( "help", "Display all available commands with this application." ),
        new CommandHelp( "lcd", "Change the local current folder." ),
        new CommandHelp( "literal", "Send a custom command to the FTP server." ),
        new CommandHelp( "quote", "Send a custom command to the FTP server." ),
        new CommandHelp( "ls", "Get filenames only of a folder's contents on the FTP server." ),
        new CommandHelp( "lls", "Get filenames only of a local folder's." ),
        new CommandHelp( "mdelete", "This command is not supported." ),
        new CommandHelp( "mdir", "This command is not supported." ),
        new CommandHelp( "mget", "Download multiple files from the FTP server." ),
        new CommandHelp( "mkdir", "Create a new folder on the FTP server." ),
        new CommandHelp( "md", "Create a new folder on the FTP server." ),
        new CommandHelp( "mls", "This command is not supported." ),
        new CommandHelp( "mode", "Change the data transfer mode." ),
        new CommandHelp( "mput", "Upload multiple files to the FTP server." ),
        new CommandHelp( "open", "Open a new connection to an FTP server." ),
        new CommandHelp( "passive", "Toggle data transfer to use a passive connection (PASV), established by the client." ),
        new CommandHelp( "port", "Toggle data transfer to use an active connection (PORT), established by the server." ),
        new CommandHelp( "prompt", "Toggle prompting ON/OFF for multiple-type commands." ),
        new CommandHelp( "proxy", "Tell the application to connect via a specified HTTP proxy address and port." ),
        new CommandHelp( "put", "Upload a file to the FTP server." ),
        new CommandHelp( "putunique", "Upload a file to the FTP server into a unique filename." ),
        new CommandHelp( "send", "Upload a file to the FTP server." ),
        new CommandHelp( "pwd", "Get the current active folder on the FTP server." ),
        new CommandHelp( "remotehelp", "Get a list of supported FTP commands on the FTP server. Don't confuse this with the commands this application accepts." ),
        new CommandHelp( "rename", "Rename a file on the FTP server." ),
        new CommandHelp( "rmdir", "Remove a folder on the FTP server." ),
        new CommandHelp( "rrmdir", "Recursively remove a folder and all files and subfolders within that folder." ),
        new CommandHelp( "rd", "Remove a folder on the FTP server." ),
        new CommandHelp( "rrd", "Recursively remove a folder and all files and subfolders within that folder." ),
        new CommandHelp( "status", "Get the values of available options with this application." ),
        new CommandHelp( "trace", "This command is not supported." ),
        new CommandHelp( "type", "Change the transfer mode of files to the FTP server (Ascii or Image)." ),
        new CommandHelp( "user", "Change the currently logged-in user on the FTP server." ),
        new CommandHelp( "verbose", "Display messages after each command." )
      };

    private FtpClient m_client = new FtpClient();

    private AbstractFolder m_localFolder = new DiskFolder( "." );

    private bool m_debug = true;
    private bool m_prompt = true;
    private bool m_verbose = true;
    private AuthenticationMethod m_method = AuthenticationMethod.None;

    #endregion PRIVATE FIELDS
    //=========================================================================
  }
}
