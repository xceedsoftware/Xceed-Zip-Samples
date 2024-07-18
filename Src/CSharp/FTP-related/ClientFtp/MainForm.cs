/*
 * Xceed FTP for .NET - ClientFTP Sample Application
 * Copyright (c) 2003 - Xceed Software Inc.
 * 
 * [MainForm.cs]
 * 
 * This application demonstrate how to use the Xceed FTP object model
 * in a generic way.
 * 
 * This file is part of Xceed FTP for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Xceed.Ftp;
using Xceed.FileSystem;

namespace ClientFtp
{
  public class MainForm : System.Windows.Forms.Form
  {
    //=========================================================================
    #region PUBLIC CONSTRUCTORS

    public MainForm()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      // Subscribe to the FTP events.
      m_asyncFtpClient.CertificateReceived += new CertificateReceivedEventHandler( this.AsyncFtpClient_CertificateReceived );
      m_asyncFtpClient.CommandSent += new CommandSentEventHandler( this.AsyncFtpClient_CommandSent );
      m_asyncFtpClient.Disconnected += new EventHandler( this.AsyncFtpClient_Disconnected );
      m_asyncFtpClient.FileTransferStatus += new FileTransferStatusEventHandler( this.AsyncFtpClient_FileTransferStatus );
      m_asyncFtpClient.MultipleFileTransferError += new MultipleFileTransferErrorEventHandler( this.AsyncFtpClient_MultipleFileTransferError );
      m_asyncFtpClient.ParsingListingLine += new ParsingListingLineEventHandler( this.AsyncFtpClient_ParsingListingLine );
      m_asyncFtpClient.ReceivingFile += new TransferringFileEventHandler( this.AsyncFtpClient_ReceivingFile );
      m_asyncFtpClient.ReplyReceived += new ReplyReceivedEventHandler( this.AsyncFtpClient_ReplyReceived );
      m_asyncFtpClient.SendingFile += new TransferringFileEventHandler( this.AsyncFtpClient_SendingFile );
      m_asyncFtpClient.StateChanged += new EventHandler( this.AsyncFtpClient_StateChanged );

      // Since this is a GUI application, we want events to be raised on the main thread.
      // This property only applies to async method calls.
      m_asyncFtpClient.SynchronizingObject = this;
    }

    #endregion PUBLIC CONSTRUCTORS
    //=========================================================================
    #region AsyncFtpClient EVENTS

    private void AsyncFtpClient_CertificateReceived( object sender, CertificateReceivedEventArgs e )
    {
      string message = string.Empty;

      if( e.Status == VerificationStatus.ValidCertificate )
      {
        message = "A valid certificate was received from the server.\n\n";
      }
      else
      {
        message = "An invalid certificate was received from the server.\n"
          + "The error is: " + e.Status.ToString() + "\n\n";
      }

      message += e.ServerCertificate.ToString() + "\n\n"
        + "Do you want to accept this certificate?";

      DialogResult answer = MessageBox.Show(
        this,
        message,
        "Server Certificate",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question );

      e.Action = ( answer == DialogResult.Yes )
        ? VerificationAction.Accept
        : VerificationAction.Reject;
    }

    private void AsyncFtpClient_CommandSent( object sender, CommandSentEventArgs e )
    {
      // We want to log every commands sent to the FTP server.

      this.AddConnectionLogInformation( "> " + e.Command );
    }

    private void AsyncFtpClient_Disconnected( object sender, EventArgs e )
    {
      // Reset the form to a disconnected state.

      this.ManageConnectionButtonsMenus();
      this.ClearRemoteFolderContent();
    }

    private void AsyncFtpClient_FileTransferStatus( object sender, FileTransferStatusEventArgs e )
    {
      // Update the progress bar and the speed panel.

      pgbTransfer.Value = e.BytesPercent;
      pnlSpeed.Text = this.FormatSpeed( e.AllBytesPerSecond );
    }

    private void AsyncFtpClient_MultipleFileTransferError( object sender, MultipleFileTransferErrorEventArgs e )
    {
      // This event occur when SendMultipleFiles or ReceiveMultipleFiles is called and
      // an error occured while transfering a file. We ask the user what to do. (Abort; Ignore; Retry)

      string message = "An error occured while ";

      if( m_asyncFtpClient.State == FtpClientState.ReceivingFile )
      {
        if( e.RemoteItemType == FtpItemType.Folder )
        {
          message += "receiving the folder '" + e.RemoteItemName + "'";
        }
        else
        {
          message += "receiving the file '" + e.RemoteItemName + "'";
        }
      }
      else
      {
        message += "sending the file '" + e.LocalItemName + "'";
      }

      message += "\n\n(" + e.Exception.Message + ").";

      DialogResult result = MessageBox.Show( this, message, "File transfer error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation );

      switch( result )
      {
        case DialogResult.Abort:
          e.Action = MultipleFileTransferErrorAction.Abort;
          break;

        case DialogResult.Ignore:
          e.Action = MultipleFileTransferErrorAction.Ignore;
          break;

        case DialogResult.Retry:
          e.Action = MultipleFileTransferErrorAction.Retry;
          break;
      }
    }

    private void AsyncFtpClient_ParsingListingLine( object sender, ParsingListingLineEventArgs e )
    {
      // If the server returns a . and/or a .. item, we don't want them in the list.

      if( e.Item.Name == "." || e.Item.Name == ".." )
        e.Valid = false;
    }

    private void AsyncFtpClient_ReceivingFile( object sender, TransferringFileEventArgs e )
    {
      pnlProgressInfo.Text = "Receiving : " + e.RemoteFilename;
      stbMain.Refresh();
    }

    private void AsyncFtpClient_ReplyReceived( object sender, ReplyReceivedEventArgs e )
    {
      // We want to log every reply received from the FTP server.

      this.AddConnectionLogInformation( e.Reply );
    }

    private void AsyncFtpClient_SendingFile( object sender, TransferringFileEventArgs e )
    {
      pnlProgressInfo.Text = "Sending : " + e.LocalFilename;
      stbMain.Refresh();
    }

    private void AsyncFtpClient_StateChanged( object sender, EventArgs e )
    {
      // Update the status shown in the status bar.

      this.ShowCurrentConnectionStatus();
    }

    #endregion AsyncFtpClient EVENTS
    //=========================================================================
    #region MainForm EVENTS

    private void MainForm_Closing( object sender, System.ComponentModel.CancelEventArgs e )
    {
      // If we are still connected to the server, we want to give the user 
      // a chance to cancel the closing process.

      if( m_asyncFtpClient.Connected )
      {
        if( MessageBox.Show( this, "You are still connected to the server. Do you wish to close the connection and exit the application?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.No )
        {
          e.Cancel = true;
        }
        else
        {
          // Disconnect silently.
          try
          {
            m_asyncFtpClient.BeginDisconnect(
              new AsyncCallback( this.DisconnectCompleted ),
              null );
          }
          catch
          {
          }
        }
      }

      if( !e.Cancel )
      {
        m_asyncFtpClient.TraceWriter.Close();
        m_asyncFtpClient.TraceWriter = null;
      }
    }

    private void MainForm_Load( object sender, System.EventArgs e )
    {
      // Initialize the local drives and the form's controls.
      this.LoadLocalDrives();
      this.ManageConnectionButtonsMenus();
      this.ShowCurrentConnectionStatus();

      // Set the file to save the trace in. Since you must provide an already open
      // TextWriter, it's also your responsability to close it when done (see MainForm_Closing).
      m_asyncFtpClient.TraceWriter = new StreamWriter( Application.StartupPath + @"\ftp.log" );

      // Set the timeout for the FTP Client object to 15 seconds.
      m_asyncFtpClient.Timeout = 15;
    }

    private void MainForm_Resize( object sender, System.EventArgs e )
    {
      // Put the splitter in the middle of the form.
      pnlRemote.Height = ( ( pnlRemote.Height + pnlLocal.Height + splMain.Height ) / 2 ) - ( splMain.Height / 2 );
    }

    #endregion MainForm EVENTS
    //=========================================================================
    #region MENU EVENTS

    private void MenuOptionRepresentationTypes_Click( object sender, System.EventArgs e )
    {
      MenuItem clickedMenu = ( MenuItem )sender;

      // Check/uncheck the menu items.
      foreach( MenuItem menuItem in mnuOptionRepresentationType.MenuItems )
      {
        menuItem.Checked = ( menuItem.Text == clickedMenu.Text );
      }

      // Set the RepresentationType depending on which menu was clicked.
      if( clickedMenu == mnuOptionRepresentationTypeAscii )
      {
        m_asyncFtpClient.RepresentationType = RepresentationType.Ascii;
      }
      else if( clickedMenu == mnuOptionRepresentationTypeBinary )
      {
        m_asyncFtpClient.RepresentationType = RepresentationType.Binary;
      }
    }

    private void MenuOptionSecure_Click( object sender, System.EventArgs e )
    {
      MenuItem clickedMenu = ( MenuItem )sender;

      // Check/uncheck the menu items.
      foreach( MenuItem menuItem in mnuOptionSecure.MenuItems )
      {
        menuItem.Checked = ( menuItem == clickedMenu );
      }

      // This change affects next connection.
    }

    private void mnuActionAbort_Click( object sender, System.EventArgs e )
    {
      this.AbortAction();
    }

    private void mnuActionChangeCurrentUser_Click( object sender, System.EventArgs e )
    {
      this.ChangeUserAction();
    }

    private void mnuActionConnect_Click( object sender, System.EventArgs e )
    {
      this.ConnectAction();
    }

    private void mnuActionDisconnect_Click( object sender, System.EventArgs e )
    {
      this.DisconnectAction();
    }

    private void mnuFileExit_Click( object sender, System.EventArgs e )
    {
      this.Close();
    }

    private void mnuLocalCreateFolder_Click( object sender, System.EventArgs e )
    {
      this.LocalCreateFolderAction();
    }

    private void mnuLocalDelete_Click( object sender, System.EventArgs e )
    {
      this.LocalDeleteAction();
    }

    private void mnuLocalRename_Click( object sender, System.EventArgs e )
    {
      this.LocalRenameAction();
    }

    private void mnuLocalTransfer_Click( object sender, System.EventArgs e )
    {
      this.LocalTransferAction();
    }

    private void mnuOptionPassiveTransfer_Click( object sender, System.EventArgs e )
    {
      // Set the FTP client to passive mode or not.
      mnuOptionPassiveTransfer.Checked = !mnuOptionPassiveTransfer.Checked;
      m_asyncFtpClient.PassiveTransfer = mnuOptionPassiveTransfer.Checked;
    }

    private void mnuOptionPreAllocateStorage_Click( object sender, System.EventArgs e )
    {
      // Set the FTP client to pre-allocate the storage or not.
      mnuOptionPreAllocateStorage.Checked = !mnuOptionPreAllocateStorage.Checked;
      m_asyncFtpClient.PreAllocateStorage = mnuOptionPreAllocateStorage.Checked;
    }

    private void mnuOptionModeZ_Click( object sender, EventArgs e )
    {
      mnuOptionModeZ.Checked = !mnuOptionModeZ.Checked;
      if( m_asyncFtpClient.Connected )
      {
        TransferMode transferMode = TransferMode.Stream;
        if( mnuOptionModeZ.Checked )
        {
          transferMode = TransferMode.ZLibCompressed;
        }

        IAsyncResult result = m_asyncFtpClient.BeginChangeTransferMode(
            transferMode,
            null,
            null );
        
        while( !result.IsCompleted )
        {
          Application.DoEvents();
        }

        m_asyncFtpClient.EndChangeTransferMode( result );
      }
    }

    private void mnuRemoteCreateFolder_Click( object sender, System.EventArgs e )
    {
      this.CreateRemoteFolder();
    }

    private void mnuRemoteDelete_Click( object sender, System.EventArgs e )
    {
      this.DeleteRemoteSelectedItems();
    }

    private void mnuRemoteRename_Click( object sender, System.EventArgs e )
    {
      this.RenameRemoteSelectedItems();
    }

    private void mnuRemoteTransfer_Click( object sender, System.EventArgs e )
    {
      this.TransferRemoteSelectedItems();
    }

    #endregion MENU EVENTS
    //=========================================================================
    #region OTHER EVENTS

    private void tlbMain_ButtonClick( object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e )
    {
      if( e.Button == btnConnect )
      {
        this.ConnectAction();
      }
      else if( e.Button == btnDisconnect )
      {
        this.DisconnectAction();
      }
      else if( e.Button == btnChangeUser )
      {
        this.ChangeUserAction();
      }
      else if( e.Button == btnAbort )
      {
        this.AbortAction();
      }
    }

    private void lsvRemoteFiles_DoubleClick( object sender, System.EventArgs e )
    {
      // Do the appropriate action depending on the type of the clicked item.

      if( lsvRemoteFiles.SelectedItems.Count > 0 )
      {
        RemoteListViewItem selectedItem = lsvRemoteFiles.SelectedItems[ 0 ] as RemoteListViewItem;

        switch( selectedItem.Info.Type )
        {
          case FtpItemType.File:
            // Transfer the file.
            this.TransferRemoteSelectedItems();
            break;

          case FtpItemType.Folder:
            // Open the folder.
            this.SelectRemoteFolderNode( selectedItem.Info );
            break;

          case FtpItemType.Link:
            // Open the link (which is the same as a folder).
            this.SelectRemoteFolderNode( selectedItem.Info );
            break;

          case FtpItemType.Unknown:
            // We consider the unknown type as a file.
            this.TransferRemoteSelectedItems();
            break;
        }
      }
    }

    private void tlbRemoteFolderContent_ButtonClick( object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e )
    {
      if( e.Button == btnRemoteDelete )
      {
        this.DeleteRemoteSelectedItems();
      }
      else if( e.Button == btnRemoteCreateFolder )
      {
        this.CreateRemoteFolder();
      }
      else if( e.Button == btnRemoteRename )
      {
        this.RenameRemoteSelectedItems();
      }
      else if( e.Button == btnRemoteTransfer )
      {
        this.TransferRemoteSelectedItems();
      }
    }

    private void trvRemoteDir_AfterSelect( object sender, System.Windows.Forms.TreeViewEventArgs e )
    {
      this.LoadRemoteFolderContent( e.Node as RemoteFolderTreeNode, false );
    }

    private void trvRemoteDir_BeforeExpand( object sender, System.Windows.Forms.TreeViewCancelEventArgs e )
    {
      if( !m_preventUpdate )
      {
        // We update this folder without filling the listview.
        this.UpdateRemoteFolder( e.Node as RemoteFolderTreeNode );
      }
    }

    private void lsvLocalFiles_DoubleClick( object sender, System.EventArgs e )
    {
      // Do the appropriate action depending on the type of the clicked item.

      if( lsvLocalFiles.SelectedItems.Count > 0 )
      {
        LocalListViewItem selectedItem = lsvLocalFiles.SelectedItems[ 0 ] as LocalListViewItem;
        AbstractFolder folder = selectedItem.Item as AbstractFolder;

        if( folder != null )
        {
          // Open the folder.
          this.SelectLocalFolderNode( folder );
        }
        else
        {
          // Transfer the file.
          this.LocalTransferAction();
        }
      }
    }

    private void tlbLocalFolderContent_ButtonClick( object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e )
    {
      if( e.Button == btnLocalDelete )
      {
        this.LocalDeleteAction();
      }
      else if( e.Button == btnLocalCreateFolder )
      {
        this.LocalCreateFolderAction();
      }
      else if( e.Button == btnLocalRename )
      {
        this.LocalRenameAction();
      }
      else if( e.Button == btnLocalTransfer )
      {
        this.LocalTransferAction();
      }
    }

    private void trvLocalDir_AfterSelect( object sender, System.Windows.Forms.TreeViewEventArgs e )
    {
      this.LoadLocalFolderContent( e.Node as LocalFolderTreeNode );
    }

    private void trvLocalDir_BeforeExpand( object sender, System.Windows.Forms.TreeViewCancelEventArgs e )
    {
      this.UpdateLocalFolder( e.Node as LocalFolderTreeNode );
    }

    #endregion OTHER EVENTS
    //=========================================================================
    #region GENERAL METHODS

    private void AbortAction()
    {
      // Tell the FTP server we wish to abort the current operation. It is safe
      // and acceptable to call Abort/BeginAbort even if no operation is in progress.

      try
      {
        m_asyncFtpClient.BeginAbort( new AsyncCallback( AbortCompleted ), null );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to abort the current operation.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );
      }
    }

    private void AbortCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndAbort( result );
      }
      catch
      {
      }
    }

    private void AddConnectionLogInformation( FtpReply reply )
    {
      // Add each reply line to the connection information log.
      foreach( string line in reply.Lines )
      {
        this.AddConnectionLogInformation( "< " + line );
      }
    }

    private void AddConnectionLogInformation( string info )
    {
      // Add an entry in the connection information log control and scroll to the end of the text.
      txtConnectionLogInformation.AppendText( info.Trim() + System.Environment.NewLine );
      txtConnectionLogInformation.SelectionStart = txtConnectionLogInformation.Text.Length;
      txtConnectionLogInformation.ScrollToCaret();
    }

    private void Authenticate()
    {
      // Secure the connection explicitly

      try
      {
        m_asyncFtpClient.BeginAuthenticate(
          AuthenticationMethod.TlsAuto,
          VerificationFlags.None,
          null,
          DataChannelProtection.Private,
          new AsyncCallback( this.AuthenticateCompleted ),
          null );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while authenticating.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void AuthenticateCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndAuthenticate( asyncResult );
        pnlSecure.Width = 25;

        if( m_asyncFtpClient.ActiveSecurityOptions.AuthenticationMethod != AuthenticationMethod.None )
        {
          this.AddConnectionLogInformation( "- " + m_asyncFtpClient.ActiveSecurityOptions.AuthenticationMethod.ToString() );
          this.AddConnectionLogInformation( "- Hash : " + m_asyncFtpClient.ActiveSecurityOptions.HashStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.HashAlgorithm.ToString() );
          this.AddConnectionLogInformation( "- Key Exchange : " + m_asyncFtpClient.ActiveSecurityOptions.KeyExchangeStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.KeyExchangeAlgorithm.ToString() );
          this.AddConnectionLogInformation( "- Session Cipher : " + m_asyncFtpClient.ActiveSecurityOptions.CipherStrength.ToString() + " bits  " + m_asyncFtpClient.ActiveSecurityOptions.CipherAlgorithm.ToString() );
        }

        this.Login();
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while authenticating.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }


    private void ClearConnectionLogInformation()
    {
      txtConnectionLogInformation.Clear();
    }

    private void ChangeUser()
    {
      // Change the currently logged user.

      try
      {
        this.ClearRemoteFolderContent();

        m_asyncFtpClient.BeginChangeUser(
          m_userName,
          m_password,
          new AsyncCallback( ChangeUserCompleted ),
          null );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to change the user.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void ChangeUserCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndChangeUser( asyncResult );

        this.AddRemoteRootTreeviewNode();
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to change the user.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void ChangeUserAction()
    {
      // Change the currently logged user. This method is used by the menu or the toolbar.

      Cursor = Cursors.WaitCursor;

      using( ConnectionInformation connectionInformation = new ConnectionInformation() )
      {
        DialogResult result = connectionInformation.ShowDialog(
          this,
          ref m_hostAddress,
          ref m_hostPort,
          ref m_anonymousConnection,
          ref m_userName,
          ref m_password,
          ref m_proxyAddress,
          ref m_proxyPort,
          ref m_proxyUserName,
          ref m_proxyPassword,
          true );

        if( result == DialogResult.OK )
        {
          this.ChangeUser();
        }
      }

      Cursor = Cursors.Default;
    }

    private void Connect()
    {
      // Connect the FTP client to the server.

      try
      {
        if( m_proxyAddress.Length == 0 )
        {
          m_asyncFtpClient.Proxy = null;
        }
        else
        {
          m_asyncFtpClient.Proxy = new HttpProxyClient(
            m_proxyAddress, m_proxyPort, m_proxyUserName, m_proxyPassword );
        }

        this.ClearConnectionLogInformation();

        if( mnuOptionSecureImplicit.Checked )
        {
          m_asyncFtpClient.BeginConnect(
            m_hostAddress,
            m_hostPort,
            AuthenticationMethod.TlsAuto,
            VerificationFlags.None,
            null,
            new AsyncCallback( this.ConnectCompleted ),
            null );
        }
        else
        {
          m_asyncFtpClient.BeginConnect(
            m_hostAddress,
            m_hostPort,
            new AsyncCallback( this.ConnectCompleted ),
            null );
        }
      }
      catch( Exception ex )
      {
        MessageBox.Show( this, "An error occured while trying to connect to host.\n\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    private void ConnectCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndConnect( asyncResult );

        if( mnuOptionSecureExplicit.Checked )
        {
          this.Authenticate();
        }
        else
        {
          this.Login();
        }
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to connect to host.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );
      }
    }

    private void ConnectAction()
    {
      // Ask the user the server's information and establish a connection to the server. 
      // This method is used by the menu or the toolbar.

      Cursor = Cursors.WaitCursor;

      using( ConnectionInformation connectionInformation = new ConnectionInformation() )
      {
        DialogResult result = connectionInformation.ShowDialog(
          this,
          ref m_hostAddress,
          ref m_hostPort,
          ref m_anonymousConnection,
          ref m_userName,
          ref m_password,
          ref m_proxyAddress,
          ref m_proxyPort,
          ref m_proxyUserName,
          ref m_proxyPassword );

        if( result == DialogResult.OK )
        {
          this.Connect();
        }
      }

      Cursor = Cursors.Default;
    }

    private void DisconnectAction()
    {
      // Disconnect the FTP client from the server.

      try
      {
        if( m_asyncFtpClient.Connected )
        {
          m_asyncFtpClient.BeginDisconnect( new AsyncCallback( this.DisconnectCompleted ), null );
        }
      }
      catch( Exception ex )
      {
        MessageBox.Show( this, "An error occured while trying to disconnect from host.\n\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    private void DisconnectCompleted( IAsyncResult asyncResult )
    {
      // Complete the disconnection. We reset our main form in OnDisconnected, since
      // disconnection can also occur without a call to Disconnect.

      try
      {
        m_asyncFtpClient.EndDisconnect( asyncResult );
      }
      catch( Exception ex )
      {
        MessageBox.Show( this, "An error occured while trying to disconnect from host.\n\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    private string FormatSpeed( long bytesPerSecond )
    {
      // Format the received speed to a readable format.

      double speed = bytesPerSecond;

      // Bytes per second.
      string formattedSpeed = speed.ToString( "n2" ) + " bytes/Sec";

      // KB per second. (only if at least 1 kb/sec)
      if( speed > 1024 )
      {
        speed /= 1024;
        formattedSpeed = speed.ToString( "n2" ) + " KB/Sec";

        // MB per second. (only if at least 1 mb/sec)
        if( speed > 1024 )
        {
          speed /= 1024;
          formattedSpeed = speed.ToString( "n2" ) + " MB/Sec";
        }
      }

      return formattedSpeed;
    }

    private string GetConnectionStatusText()
    {
      // Returns a formatted description of the current state of the FTP client.

      switch( m_asyncFtpClient.State )
      {
        case FtpClientState.ChangingCurrentFolder:
          return "Changing current folder";

        case FtpClientState.ChangingToParentFolder:
          return "Changing to parent folder";

        case FtpClientState.ChangingUser:
          return "Changing user";

        case FtpClientState.Connected:
          return "Connected" + " (" + m_asyncFtpClient.ServerAddress.ToString() + ")";

        case FtpClientState.Connecting:
          return "Connecting";

        case FtpClientState.CreatingFolder:
          return "Creating folder";

        case FtpClientState.DeletingFile:
          return "Deleting file";

        case FtpClientState.DeletingFolder:
          return "Changing folder";

        case FtpClientState.Disconnecting:
          return "Disconnecting";

        case FtpClientState.GettingCurrentFolder:
          return "Getting current folder";

        case FtpClientState.GettingFolderContents:
          return "Getting folder contents";

        case FtpClientState.NotConnected:
          return "Not connected";

        case FtpClientState.ReceivingFile:
          return "Receiving file";

        case FtpClientState.ReceivingMultipleFiles:
          return "Receiving multiple files";

        case FtpClientState.RenamingFile:
          return "Renaming a file or folder";

        case FtpClientState.SendingCustomCommand:
          return "Sending custom command";

        case FtpClientState.SendingFile:
          return "Sending file";

        case FtpClientState.SendingMultipleFiles:
          return "Sending multiple files";

        default:
          return "";
      }
    }

    private void Login()
    {
      // Initialize the login for a user.

      try
      {
        m_asyncFtpClient.BeginLogin(
          m_userName,
          m_password,
          new AsyncCallback( this.LoginCompleted ),
          null );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to login.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void LoginCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndLogin( asyncResult );
        this.ChangeTransferMode();
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to login.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void ChangeTransferMode()
    {
      try
      {
        TransferMode transferMode = TransferMode.Stream;
        if( mnuOptionModeZ.Checked )
        {
          transferMode = TransferMode.ZLibCompressed;
        }

        m_asyncFtpClient.BeginChangeTransferMode(
            transferMode,
            new AsyncCallback( this.ChangeTransferModeCompleted ),
            null );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
         this,
         "An error occured while trying to change transfer mode.\n\n\n" + ex.Message,
         "Error",
         MessageBoxButtons.OK,
         MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void ChangeTransferModeCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndChangeTransferMode( result );

        this.ManageConnectionButtonsMenus();
        this.AddRemoteRootTreeviewNode();
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while trying to change transfer mode.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.DisconnectAction();
      }
    }

    private void ManageConnectionButtonsMenus()
    {
      // Enable or disable controls depending if the FTP client is connected or not.

      btnConnect.Enabled = !m_asyncFtpClient.Connected;
      btnDisconnect.Enabled = m_asyncFtpClient.Connected;
      btnChangeUser.Enabled = m_asyncFtpClient.Connected;
      btnAbort.Enabled = m_asyncFtpClient.Connected;

      mnuActionConnect.Enabled = !m_asyncFtpClient.Connected;
      mnuActionDisconnect.Enabled = m_asyncFtpClient.Connected;
      mnuActionChangeCurrentUser.Enabled = m_asyncFtpClient.Connected;
      mnuActionAbort.Enabled = m_asyncFtpClient.Connected;
      mnuOptionSecure.Enabled = !m_asyncFtpClient.Connected;

      if( !m_asyncFtpClient.Connected )
      {
        pnlSecure.Width = 0;
      }
    }

    private void ShowCurrentConnectionStatus()
    {
      // Update the connection status information panel.

      try
      {
        pnlConnectionStatus.Text = this.GetConnectionStatusText();
      }
      catch
      {
      }
    }

    #endregion GENERAL METHODS
    //=========================================================================
    #region REMOTE-SIDE METHODS

    private void AddRemoteRootTreeviewNode()
    {
      // Clear the actual list.
      trvRemoteDir.Nodes.Clear();

      RemoteFolderTreeNode node = new RemoteFolderTreeNode( m_asyncFtpClient );
      trvRemoteDir.Nodes.Add( node );

      // Select the root node.
      trvRemoteDir.SelectedNode = node;
    }

    private void ClearRemoteFolderContent()
    {
      // Clear the treeview and the listview of the host.

      trvRemoteDir.Nodes.Clear();
      lsvRemoteFiles.Items.Clear();
    }

    private void CreateRemoteFolder()
    {
      // Create a folder in the current folder of the host.

      using( FtpItemName ftpItemName = new FtpItemName() )
      {
        string newFolderName = String.Empty;

        if( ftpItemName.ShowDialog( this, "Enter the name of the new folder", ref newFolderName ) == DialogResult.OK )
        {
          try
          {
            m_asyncFtpClient.BeginCreateFolder(
              newFolderName,
              new AsyncCallback( CreateFolderCompleted ),
              null );
          }
          catch( Exception ex )
          {
            MessageBox.Show(
              this,
              "An error occured while creating a folder.\n\n\n" + ex.Message,
              "Error",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error );
          }
        }
      }
    }

    private void CreateFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndCreateFolder( result );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while creating a folder.\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );
      }
    }

    private void DeleteRemoteSelectedItems()
    {
      // Deletes all the selected items from the host.

      if( lsvRemoteFiles.SelectedItems.Count > 0 )
      {
        // Build the confirmation message.
        string confirmMsg = String.Empty;
        string confirmCaption = String.Empty;

        if( lsvRemoteFiles.SelectedItems.Count > 1 )
        {
          confirmMsg = "Are you sure you want to delete these " + lsvRemoteFiles.SelectedItems.Count.ToString() + " items?\n(Deleting a folder will delete all of its contents)";
          confirmCaption = "Confirm Multiple File Delete";
        }
        else
        {
          RemoteListViewItem selectedItem = lsvRemoteFiles.SelectedItems[ 0 ] as RemoteListViewItem;

          if( ( selectedItem.Info.Type == FtpItemType.Folder ) || ( selectedItem.Info.Type == FtpItemType.Link ) )
          {
            confirmMsg = "Are you sure you want to delete the folder '" + selectedItem.Info.Name + "'?\n(Deleting a folder will delete all of its contents)";
            confirmCaption = "Confirm Folder Delete";
          }
          else
          {
            confirmMsg = "Are you sure you want to delete the file '" + selectedItem.Info.Name + "'?";
            confirmCaption = "Confirm File Delete";
          }
        }

        // Confirm the deletion.
        if( MessageBox.Show( this, confirmMsg, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
        {
          pnlLocal.Enabled = false;
          pnlRemote.Enabled = false;

          // Delete each selected item.

          this.DeleteRemoteSelectedItem( 0 );
        }
      }
    }

    private void DeleteRemoteSelectedItem( int index )
    {
      if( index >= lsvRemoteFiles.SelectedItems.Count )
      {
        // We completed.
        this.RemoteChangesCompleted();
      }
      else
      {
        RemoteListViewItem item = lsvRemoteFiles.SelectedItems[ index ] as RemoteListViewItem;

        try
        {
          if( ( item.Info.Type == FtpItemType.Folder ) || ( item.Info.Type == FtpItemType.Link ) )
          {
            m_asyncFtpClient.BeginDeleteFolder(
              item.Info.Name,
              true,
              new AsyncCallback( this.DeleteFolderCompleted ),
              index );
          }
          else
          {
            m_asyncFtpClient.BeginDeleteFile(
              item.Info.Name,
              new AsyncCallback( this.DeleteFileCompleted ),
              index );
          }
        }
        catch( Exception except )
        {
          MessageBox.Show(
            this,
            "An error occured while deleting '" + item.Info.Name + "'\n\n\n" + except.Message,
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error );

          this.RemoteChangesCompleted();
        }
      }
    }

    private void DeleteFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndDeleteFolder( result );

        int index = ( int )result.AsyncState;
        this.DeleteRemoteSelectedItem( index + 1 );
      }
      catch( Exception except )
      {
        MessageBox.Show(
          this,
          "An error occured while deleting a folder\n\n\n" + except.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void DeleteFileCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndDeleteFile( result );

        int index = ( int )result.AsyncState;
        this.DeleteRemoteSelectedItem( index + 1 );
      }
      catch( Exception except )
      {
        // We could try deleting this name as a folder, but let's simply
        // display an error.
        MessageBox.Show(
          this,
          "An error occured while deleting a file\n\n\n" + except.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void UpdateRemoteFolder( RemoteFolderTreeNode selectedNode )
    {
      try
      {
        selectedNode.Refresh();
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while getting the contents of folder '" + selectedNode.Text + "'\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );
      }
    }

    private void LoadRemoteFolderContent( RemoteFolderTreeNode selectedNode, bool refresh )
    {
      try
      {
        selectedNode.Select( lsvRemoteFiles, refresh );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while getting the contents of folder '" + selectedNode.Text + "'\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );
      }
    }

    private void ReceiveFile( FtpItemInfo itemInfo, int index )
    {
      // Receive a file from the host.

      try
      {
        LocalFolderTreeNode selectedNode = trvLocalDir.SelectedNode as LocalFolderTreeNode;

        m_asyncFtpClient.BeginReceiveFile(
          itemInfo.Name,
          selectedNode.Folder.FullName + itemInfo.Name,
          new AsyncCallback( this.ReceiveFileCompleted ),
          index );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while receiving the file '" + itemInfo.Name + "'\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.LocalChangesCompleted();
      }
    }

    private void ReceiveFileCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndReceiveFile( asyncResult );

        int index = ( int )asyncResult.AsyncState;
        this.TransferRemoteSelectedItem( index + 1 );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.LocalChangesCompleted();
      }
    }

    private void ReceiveMultipleFiles( FtpItemInfo itemInfo, int index )
    {
      // Receive a folder from the host. The ReceiveMultipleFiles can also be used 
      // to receive files matching a specified mask but we use it only to receive 
      // a folder (and it's subdirectories).

      // Notice that we don't use an "*" in the filemask, but simply make sure
      // the path ends with a folder separator. That's because some FTP servers
      // consider the "LIST *" command as a request to see a recursive listing of
      // everything. Since we only want "everything in the current folder", an 
      // empty mask is the best approach.

      try
      {
        LocalFolderTreeNode selectedNode = trvLocalDir.SelectedNode as LocalFolderTreeNode;

        m_asyncFtpClient.BeginReceiveMultipleFiles(
          itemInfo.Name + Path.DirectorySeparatorChar,
          selectedNode.Folder.FullName,
          true,
          true,
          new AsyncCallback( ReceiveMultipleFilesCompleted ),
          index );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          "An error occured while receiving the folder '" + itemInfo.Name + "'\n\n\n" + ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.LocalChangesCompleted();
      }
    }

    private void ReceiveMultipleFilesCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndReceiveMultipleFiles( asyncResult );

        int index = ( int )asyncResult.AsyncState;
        this.TransferRemoteSelectedItem( index + 1 );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.LocalChangesCompleted();
      }
    }

    /*#
    private void RemoteCreateFolderAction()
    {
      // Create a folder on the host. This method is intended to be used
      // by the menu or the toolbar.

      this.CreateRemoteFolder();
    }

    private void RemoteDeleteAction()
    {
      // Delete the selected items from the host. This method 
      // is intended to be used by the menu or the toolbar.

      this.DeleteRemoteSelectedItems();
    }

    private void RemoteRenameAction()
    {
      // Rename the selected items from the host. This method 
      // is intended to be used by the menu or the toolbar.

      this.RenameRemoteSelectedItems();
    }

    private void RemoteTransferAction()
    {
      // Transfer the selected items from the host. This method 
      // is intended to be used by the menu or the toolbar.

      this.TransferRemoteSelectedItems();
    }
    */

    private void RemoteChangesCompleted()
    {
      // Enable panels
      pnlLocal.Enabled = true;
      pnlRemote.Enabled = true;

      // Refresh selected node
      this.LoadRemoteFolderContent( trvRemoteDir.SelectedNode as RemoteFolderTreeNode, true );

      // Reset the progress bar.
      pgbTransfer.Value = 0;
      pnlProgressInfo.Text = "";
      pnlSpeed.Text = "";
    }

    private void RenameRemoteSelectedItems()
    {
      // Loop through all the selected items and for each, ask for a new name.

      if( lsvRemoteFiles.SelectedItems.Count > 0 )
      {
        pnlLocal.Enabled = false;
        pnlRemote.Enabled = false;

        this.RenameRemoteSelectedItem( 0 );
      }
    }

    private void RenameRemoteSelectedItem( int index )
    {
      if( index >= lsvRemoteFiles.SelectedItems.Count )
      {
        // We completed renaming
        this.RemoteChangesCompleted();
      }
      else
      {
        RemoteListViewItem item = lsvRemoteFiles.SelectedItems[ index ] as RemoteListViewItem;

        using( FtpItemName ftpItemName = new FtpItemName() )
        {
          string newItemName = item.Info.Name;
          string formCaption = "Enter new name for the ";

          switch( item.Info.Type )
          {
            case FtpItemType.File:
              formCaption += "file";
              break;

            case FtpItemType.Folder:
              formCaption += "folder";
              break;

            case FtpItemType.Link:
              formCaption += "link";
              break;

            case FtpItemType.Unknown:
              formCaption += "unknown item";
              break;
          }

          if( ftpItemName.ShowDialog( this, formCaption, ref newItemName ) == DialogResult.OK )
          {
            try
            {
              m_asyncFtpClient.BeginRenameFile(
                item.Info.Name,
                newItemName,
                new AsyncCallback( this.RenameFileCompleted ),
                index );
            }
            catch( Exception except )
            {
              MessageBox.Show(
                this,
                "An error occured while renaming '" + item.Info.Name + "'\n\n\n" + except.Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error );

              this.RemoteChangesCompleted();
            }
          }
        }
      }
    }

    private void RenameFileCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndRenameFile( result );

        int index = ( int )result.AsyncState;
        this.RenameRemoteSelectedItem( index + 1 );
      }
      catch( Exception except )
      {
        MessageBox.Show(
          this,
          "An error occured while renaming a file\n\n\n" + except.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SelectRemoteFolderNode( FtpItemInfo itemInfo )
    {
      // We find the corresponding folder in the treeview and simulate a click that will
      // load the content of the folder.

      if( !trvRemoteDir.SelectedNode.IsExpanded )
      {
        // We need to expand the node before selecting a child node. But we
        // don't want to generate another update.
        m_preventUpdate = true;
        trvRemoteDir.SelectedNode.Expand();
        m_preventUpdate = false;
      }

      foreach( RemoteFolderTreeNode node in trvRemoteDir.SelectedNode.Nodes )
      {
        if( node.Text == itemInfo.Name )
        {
          trvRemoteDir.SelectedNode = node;
          return;
        }
      }
    }

    private void TransferRemoteSelectedItems()
    {
      // We start with the first selected item and we will transfer each item
      // asynchroneously. But we don't want the remote nor local selections and
      // current folders to change, so we disable the two panels.

      if( lsvRemoteFiles.SelectedItems.Count > 0 )
      {
        pnlLocal.Enabled = false;
        pnlRemote.Enabled = false;

        this.TransferRemoteSelectedItem( 0 );
      }
    }

    private void TransferRemoteSelectedItem( int index )
    {
      if( index >= lsvRemoteFiles.SelectedItems.Count )
      {
        this.LocalChangesCompleted();
      }
      else
      {
        // For each selected remote items, transfer it by calling the appropriate method.

        RemoteListViewItem item = lsvRemoteFiles.SelectedItems[ index ] as RemoteListViewItem;

        switch( item.Info.Type )
        {
          case FtpItemType.File:
            this.ReceiveFile( item.Info, index );
            break;

          case FtpItemType.Folder:
            this.ReceiveMultipleFiles( item.Info, index );
            break;

          case FtpItemType.Link:
            // Links can either point to files or folders.
            // In this sample, we only support links to folders.
            this.ReceiveMultipleFiles( item.Info, index );
            break;

          case FtpItemType.Unknown:
            this.ReceiveFile( item.Info, index );
            break;
        }
      }
    }

    #endregion REMOTE-SIDE METHODS
    //=========================================================================
    #region LOCAL-SIDE METHODS

    private bool CreateLocalFolder()
    {
      // Create a folder on the local drive. (Depending on which folder
      // is currently selected in the treeview)

      FtpItemName ftpItemName = new FtpItemName();

      string newFolderName = String.Empty;

      if( ftpItemName.ShowDialog( this, "Enter the name of the new folder", ref newFolderName ) == DialogResult.OK )
      {
        try
        {
          LocalFolderTreeNode selectedNode = trvLocalDir.SelectedNode as LocalFolderTreeNode;
          selectedNode.Folder.CreateFolder( newFolderName );

          return true;
        }
        catch( Exception ex )
        {
          MessageBox.Show( this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    private bool DeleteLocalSelectedItems()
    {
      // Delete the selected local files and folders. 

      bool deleted = false;

      if( lsvLocalFiles.SelectedItems.Count == 0 )
        return false;

      // Build the confirmation message.
      string confirmMsg = String.Empty;
      string confirmCaption = String.Empty;

      if( lsvLocalFiles.SelectedItems.Count > 1 )
      {
        confirmMsg = "Are you sure you want to delete these " + lsvLocalFiles.SelectedItems.Count.ToString() + " items?\n(Deleting a folder will delete all of its contents)";
        confirmCaption = "Confirm Multiple File Delete";
      }
      else
      {
        LocalListViewItem selectedItem = lsvLocalFiles.SelectedItems[ 0 ] as LocalListViewItem;

        if( selectedItem.Item is AbstractFolder )
        {
          confirmMsg = "Are you sure you want to delete the folder '" + selectedItem.Item.Name + "'?\n(Deleting a folder will delete all of its contents)";
          confirmCaption = "Confirm Folder Delete";
        }
        else
        {
          confirmMsg = "Are you sure you want to delete the file '" + selectedItem.Item.Name + "'?";
          confirmCaption = "Confirm File Delete";
        }
      }

      if( MessageBox.Show( this, confirmMsg, confirmCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes )
      {
        // Delete each selected items.

        foreach( LocalListViewItem item in lsvLocalFiles.SelectedItems )
        {
          try
          {
            item.Item.Delete();
            deleted = true;
          }
          catch( Exception ex )
          {
            MessageBox.Show( this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
          }
        }
      }

      return deleted;
    }

    private void LoadLocalDrives()
    {
      // Clear the actual content.
      trvLocalDir.Nodes.Clear();

      // Get a list of all the local drive on the system.
      string[] drives = System.IO.Directory.GetLogicalDrives();

      foreach( string drive in drives )
      {
        LocalFolderTreeNode node = new LocalFolderTreeNode( new DiskFolder( drive ) );
        trvLocalDir.Nodes.Add( node );

        // Select the C drive by default.
        if( drive == "C:\\" )
        {
          trvLocalDir.SelectedNode = node;
        }
      }
    }

    private void UpdateLocalFolder( LocalFolderTreeNode selectedNode )
    {
      try
      {
        selectedNode.UpdateContents();
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    private void LoadLocalFolderContent( LocalFolderTreeNode selectedNode )
    {
      try
      {
        selectedNode.FillList( lsvLocalFiles );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    private void LocalChangesCompleted()
    {
      // Enable panels back
      pnlLocal.Enabled = true;
      pnlRemote.Enabled = true;

      // Refresh the local folder content.
      this.LoadLocalFolderContent( trvLocalDir.SelectedNode as LocalFolderTreeNode );

      // Reset the progress bar.
      pgbTransfer.Value = 0;
      pnlProgressInfo.Text = "";
      pnlSpeed.Text = "";
    }

    private void LocalCreateFolderAction()
    {
      // Create a folder on the local disk. This method is intended to be used
      // by the menu or the toolbar.

      Cursor = Cursors.WaitCursor;

      if( this.CreateLocalFolder() )
      {
        // Reload the current file list and the subfolders in the treeview.
        this.UpdateLocalFolder( trvLocalDir.SelectedNode as LocalFolderTreeNode );
        this.LoadLocalFolderContent( trvLocalDir.SelectedNode as LocalFolderTreeNode );
      }

      Cursor = Cursors.Default;
    }

    private void LocalDeleteAction()
    {
      // Delete the selected items from the local disk. This method 
      // is intended to be used by the menu or the toolbar.

      Cursor = Cursors.WaitCursor;

      if( this.DeleteLocalSelectedItems() )
      {
        // Reload the current file list and the subfolders in the treeview.
        this.UpdateLocalFolder( trvLocalDir.SelectedNode as LocalFolderTreeNode );
        this.LoadLocalFolderContent( trvLocalDir.SelectedNode as LocalFolderTreeNode );
      }

      Cursor = Cursors.Default;
    }

    private void LocalRenameAction()
    {
      // Rename the selected items from the local disk. This method 
      // is intended to be used by the menu or the toolbar.

      Cursor = Cursors.WaitCursor;

      if( this.RenameLocalSelectedItems() )
      {
        // Reload the current file list and the subfolders in the treeview.
        this.UpdateLocalFolder( trvLocalDir.SelectedNode as LocalFolderTreeNode );
        this.LoadLocalFolderContent( trvLocalDir.SelectedNode as LocalFolderTreeNode );
      }

      Cursor = Cursors.Default;
    }

    private void LocalTransferAction()
    {
      // Transfer the selected items from the local disk. This method 
      // is intended to be used by the menu or the toolbar.

      this.TransferLocalSelectedItems();
    }

    private bool RenameLocalSelectedItems()
    {
      // Rename all the selected items and for each, ask the user for a new name.

      bool renamed = false;

      if( lsvLocalFiles.SelectedItems.Count == 0 )
        return false;

      foreach( LocalListViewItem item in lsvLocalFiles.SelectedItems )
      {
        using( FtpItemName ftpItemName = new FtpItemName() )
        {
          string newItemName = item.Item.Name;
          string formCaption = "Enter new name for the ";

          if( item.Item is AbstractFolder )
          {
            formCaption += "folder";
          }
          else
          {
            formCaption += "file";
          }

          if( ftpItemName.ShowDialog( this, formCaption, ref newItemName ) == DialogResult.OK )
          {
            try
            {
              item.Item.Name = newItemName;
              renamed = true;
            }
            catch( Exception ex )
            {
              MessageBox.Show( this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
          }
        }
      }

      return renamed;
    }

    private void SelectLocalFolderNode( AbstractFolder folder )
    {
      // We find the corresponding folder in the treeview and simulate a click that will
      // load the content of the folder.

      if( !trvLocalDir.SelectedNode.IsExpanded )
      {
        // We need to expand the node before selecting a child node.
        trvLocalDir.SelectedNode.Expand();
      }

      foreach( LocalFolderTreeNode node in trvLocalDir.SelectedNode.Nodes )
      {
        if( node.Folder.Name == folder.Name )
        {
          trvLocalDir.SelectedNode = node;
          return;
        }
      }
    }

    private void SendFile( AbstractFile file, int index )
    {
      // Send a file to the host's current folder.

      try
      {
        m_asyncFtpClient.BeginSendFile(
          file.FullName,
          file.Name,
          false,
          new AsyncCallback( this.SendFileCompleted ),
          index );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SendFileCompleted( IAsyncResult asyncResult )
    {
      try
      {
        m_asyncFtpClient.EndSendFile( asyncResult );

        int index = ( int )asyncResult.AsyncState;
        this.TransferLocalSelectedItem( index + 1 );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SendMultipleFiles( AbstractFolder folder, int index )
    {
      // Send a folder to the host's current folder. The SendMultipleFiles can also be used to send
      // files matching a specified mask but we use it only to send a folder. (and it's subdirectories)

      try
      {
        // SendMultipleFiles transfers the contents of the source folder, but does
        // not create that folder remotely. It creates subfolders only, assuming the 
        // current remote working folder matches this local folder. Since we want to 
        // replicate this folder, we first create it remotely, then copy the local 
        // folder's contents.

        m_asyncFtpClient.BeginCreateFolder(
          folder.Name,
          new AsyncCallback( this.SendMultipleFilesCreateFolderCompleted ),
          new object[] { folder, index } );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SendMultipleFilesCreateFolderCompleted( IAsyncResult result )
    {
      try
      {
        // If we can't create that folder, we assume it's because it already exists.
        // We continue with the "ChangeCurrentFolder". If this one fails, we'll error out.
        m_asyncFtpClient.EndCreateFolder( result );
      }
      catch
      {
      }
      finally
      {
        AbstractFolder folder = ( AbstractFolder )( ( object[] )result.AsyncState )[ 0 ];

        m_asyncFtpClient.BeginChangeCurrentFolder(
          folder.Name,
          new AsyncCallback( this.SendMultipleFilesChangeFolderCompleted ),
          result.AsyncState );
      }
    }

    private void SendMultipleFilesChangeFolderCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndChangeCurrentFolder( result );

        object[] state = ( object[] )result.AsyncState;

        AbstractFolder folder = ( AbstractFolder )state[ 0 ];

        m_asyncFtpClient.BeginSendMultipleFiles(
          folder.FullName + "*",
          true,
          true,
          new AsyncCallback( this.SendMultipleFilesReturnToParent ),
          state[ 1 ] );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SendMultipleFilesReturnToParent( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndSendMultipleFiles( result );

        m_asyncFtpClient.BeginChangeToParentFolder(
          new AsyncCallback( this.SendMultipleFilesCompleted ),
          result.AsyncState );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void SendMultipleFilesCompleted( IAsyncResult result )
    {
      try
      {
        m_asyncFtpClient.EndChangeToParentFolder( result );

        int index = ( int )result.AsyncState;
        this.TransferLocalSelectedItem( index + 1 );
      }
      catch( Exception ex )
      {
        MessageBox.Show(
          this,
          ex.Message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error );

        this.RemoteChangesCompleted();
      }
    }

    private void TransferLocalSelectedItems()
    {
      // We start with the first selected item and we will transfer each item
      // asynchroneously. But we don't want the remote nor local selections and
      // current folders to change, so we disable the two panels.

      if( lsvLocalFiles.SelectedItems.Count > 0 )
      {
        pnlLocal.Enabled = false;
        pnlRemote.Enabled = false;

        this.TransferLocalSelectedItem( 0 );
      }
    }

    private void TransferLocalSelectedItem( int index )
    {
      if( index >= lsvLocalFiles.SelectedItems.Count )
      {
        this.RemoteChangesCompleted();
      }
      else
      {
        LocalListViewItem item = lsvLocalFiles.SelectedItems[ index ] as LocalListViewItem;
        AbstractFolder folder = item.Item as AbstractFolder;

        if( folder != null )
        {
          this.SendMultipleFiles( folder, index );
        }
        else
        {
          this.SendFile( item.Item as AbstractFile, index );
        }
      }
    }

    #endregion LOCAL-SIDE METHODS
    //=========================================================================
    #region IDispose METHODS

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if( components != null )
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #endregion IDispose METHODS
    //=========================================================================
    #region STATIC METHODS

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
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

      Xceed.Ftp.Licenser.LicenseKey = "FTN68-NET7U-6KZAN-13CA"; // Uncomment and set license key here to deploy 

      Application.EnableVisualStyles();
      Application.DoEvents();

      Application.Run( new MainForm() );
    }

    #endregion STATIC METHODS
    //=========================================================================
    #region PRIVATE MEMBERS

    // Since this is a GUI application, we use AsyncFtpClient, which will offer
    // us async method calls and automatic synchronization with the main thread.
    private AsyncFtpClient m_asyncFtpClient = new AsyncFtpClient();

    private string m_hostAddress = "localhost";
    private int m_hostPort = 21;
    private bool m_anonymousConnection = true;
    private string m_userName = "anonymous";
    private string m_password = "guest";
    private string m_proxyAddress = string.Empty;
    private int m_proxyPort = 8080;
    private string m_proxyUserName = string.Empty;
    private string m_proxyPassword = string.Empty;
    private bool m_preventUpdate = false;

    #endregion PRIVATE MEMBERS
    //=========================================================================
    #region Windows Form Designer generated members

    private System.Windows.Forms.ImageList imgMain;
    private System.ComponentModel.IContainer components;
    private System.Windows.Forms.MenuItem mnuFile;
    private System.Windows.Forms.MenuItem mnuFileExit;
    private System.Windows.Forms.StatusBar stbMain;
    private System.Windows.Forms.StatusBarPanel pnlConnectionStatus;
    private System.Windows.Forms.Panel pnlRemote;
    private System.Windows.Forms.Panel pnlRemoteTitle;
    private System.Windows.Forms.Label lblRemoteTitle;
    private System.Windows.Forms.TreeView trvRemoteDir;
    private System.Windows.Forms.Splitter splRemote;
    private System.Windows.Forms.Splitter splMain;
    private System.Windows.Forms.Panel pnlLocal;
    private System.Windows.Forms.TreeView trvLocalDir;
    private System.Windows.Forms.Panel pnlLocalTitle;
    private System.Windows.Forms.Label lblLocalTitle;
    private System.Windows.Forms.ToolBarButton btnConnect;
    private System.Windows.Forms.ToolBarButton btnDisconnect;
    private System.Windows.Forms.TextBox txtConnectionLogInformation;
    private System.Windows.Forms.ListView lsvRemoteFiles;
    private System.Windows.Forms.ColumnHeader colRemoteName;
    private System.Windows.Forms.ColumnHeader colRemoteSize;
    private System.Windows.Forms.ColumnHeader colRemoteDateTime;
    private System.Windows.Forms.ListView lsvLocalFiles;
    private System.Windows.Forms.ImageList imgFtpItemType;
    private System.Windows.Forms.ToolBar tlbRemoteFolderContent;
    private System.Windows.Forms.MenuItem mnuOptionPassiveTransfer;
    private System.Windows.Forms.MenuItem mnuOptionRepresentationType;
    private System.Windows.Forms.MenuItem mnuOptionRepresentationTypeAscii;
    private System.Windows.Forms.MenuItem mnuOptionModeZ;
    private System.Windows.Forms.ColumnHeader colLocalName;
    private System.Windows.Forms.ColumnHeader colLocalSize;
    private System.Windows.Forms.ColumnHeader colLocalDate;
    private System.Windows.Forms.ToolBarButton btnRemoteDelete;
    private System.Windows.Forms.ToolBarButton btnRemoteRename;
    private System.Windows.Forms.ToolBarButton btnRemoteCreateFolder;
    private System.Windows.Forms.ToolBar tlbLocalFolderContent;
    private System.Windows.Forms.Splitter splLocal;
    private System.Windows.Forms.ImageList imgFolderContentToolbar;
    private System.Windows.Forms.ToolBarButton btnLocalDelete;
    private System.Windows.Forms.ToolBarButton btnLocalRename;
    private System.Windows.Forms.ToolBarButton btnLocalCreateFolder;
    private System.Windows.Forms.ToolBarButton btnChangeUser;
    private System.Windows.Forms.StatusBarPanel pnlProgress;
    private System.Windows.Forms.StatusBarPanel pnlProgressInfo;
    private System.Windows.Forms.ProgressBar pgbTransfer;
    private System.Windows.Forms.StatusBarPanel pnlSpeed;
    private System.Windows.Forms.MenuItem mnuOption;
    private System.Windows.Forms.ToolBarButton btnRemoteTransfer;
    private System.Windows.Forms.ToolBarButton btnLocalTransfer;
    private System.Windows.Forms.MenuItem mnuAction;
    private System.Windows.Forms.MenuItem mnuActionConnect;
    private System.Windows.Forms.MenuItem mnuActionDisconnect;
    private System.Windows.Forms.MenuItem mnuActionChangeCurrentUser;
    private System.Windows.Forms.MenuItem mnuRemote;
    private System.Windows.Forms.MenuItem mnuRemoteDelete;
    private System.Windows.Forms.MenuItem mnuRemoteRename;
    private System.Windows.Forms.MenuItem mnuRemoteCreateFolder;
    private System.Windows.Forms.MenuItem mnuRemoteTransfer;
    private System.Windows.Forms.MenuItem mnuLocal;
    private System.Windows.Forms.MenuItem mnuLocalDelete;
    private System.Windows.Forms.MenuItem mnuLocalRename;
    private System.Windows.Forms.MenuItem mnuLocalCreateFolder;
    private System.Windows.Forms.MenuItem mnuLocalTransfer;
    private System.Windows.Forms.MenuItem mnuOptionPreAllocateStorage;
    private System.Windows.Forms.MenuItem mnuOptionRepresentationTypeBinary;
    private System.Windows.Forms.ToolBarButton btnAbort;
    private System.Windows.Forms.MenuItem mnuActionAbort;
    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.StatusBarPanel pnlSecure;
    private System.Windows.Forms.MenuItem mnuOptionSecure;
    private System.Windows.Forms.MenuItem mnuOptionSecureNone;
    private System.Windows.Forms.MenuItem mnuOptionSecureExplicit;
    private System.Windows.Forms.MenuItem mnuOptionSecureImplicit;
    private System.Windows.Forms.ToolBar tlbMain;

    #endregion Windows Form Designer generated members
    //=========================================================================
    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
      this.mnuFile = new System.Windows.Forms.MenuItem();
      this.mnuFileExit = new System.Windows.Forms.MenuItem();
      this.mnuAction = new System.Windows.Forms.MenuItem();
      this.mnuActionConnect = new System.Windows.Forms.MenuItem();
      this.mnuActionDisconnect = new System.Windows.Forms.MenuItem();
      this.mnuActionChangeCurrentUser = new System.Windows.Forms.MenuItem();
      this.mnuActionAbort = new System.Windows.Forms.MenuItem();
      this.mnuRemote = new System.Windows.Forms.MenuItem();
      this.mnuRemoteDelete = new System.Windows.Forms.MenuItem();
      this.mnuRemoteRename = new System.Windows.Forms.MenuItem();
      this.mnuRemoteCreateFolder = new System.Windows.Forms.MenuItem();
      this.mnuRemoteTransfer = new System.Windows.Forms.MenuItem();
      this.mnuLocal = new System.Windows.Forms.MenuItem();
      this.mnuLocalDelete = new System.Windows.Forms.MenuItem();
      this.mnuLocalRename = new System.Windows.Forms.MenuItem();
      this.mnuLocalCreateFolder = new System.Windows.Forms.MenuItem();
      this.mnuLocalTransfer = new System.Windows.Forms.MenuItem();
      this.mnuOption = new System.Windows.Forms.MenuItem();
      this.mnuOptionPassiveTransfer = new System.Windows.Forms.MenuItem();
      this.mnuOptionPreAllocateStorage = new System.Windows.Forms.MenuItem();
      this.mnuOptionModeZ = new System.Windows.Forms.MenuItem();
      this.mnuOptionRepresentationType = new System.Windows.Forms.MenuItem();
      this.mnuOptionRepresentationTypeAscii = new System.Windows.Forms.MenuItem();
      this.mnuOptionRepresentationTypeBinary = new System.Windows.Forms.MenuItem();
      this.mnuOptionSecure = new System.Windows.Forms.MenuItem();
      this.mnuOptionSecureNone = new System.Windows.Forms.MenuItem();
      this.mnuOptionSecureExplicit = new System.Windows.Forms.MenuItem();
      this.mnuOptionSecureImplicit = new System.Windows.Forms.MenuItem();
      this.stbMain = new System.Windows.Forms.StatusBar();
      this.pnlConnectionStatus = new System.Windows.Forms.StatusBarPanel();
      this.pnlProgressInfo = new System.Windows.Forms.StatusBarPanel();
      this.pnlSpeed = new System.Windows.Forms.StatusBarPanel();
      this.pnlSecure = new System.Windows.Forms.StatusBarPanel();
      this.pnlProgress = new System.Windows.Forms.StatusBarPanel();
      this.pnlRemote = new System.Windows.Forms.Panel();
      this.lsvRemoteFiles = new System.Windows.Forms.ListView();
      this.colRemoteName = new System.Windows.Forms.ColumnHeader();
      this.colRemoteSize = new System.Windows.Forms.ColumnHeader();
      this.colRemoteDateTime = new System.Windows.Forms.ColumnHeader();
      this.imgFtpItemType = new System.Windows.Forms.ImageList( this.components );
      this.tlbRemoteFolderContent = new System.Windows.Forms.ToolBar();
      this.btnRemoteDelete = new System.Windows.Forms.ToolBarButton();
      this.btnRemoteRename = new System.Windows.Forms.ToolBarButton();
      this.btnRemoteCreateFolder = new System.Windows.Forms.ToolBarButton();
      this.btnRemoteTransfer = new System.Windows.Forms.ToolBarButton();
      this.imgFolderContentToolbar = new System.Windows.Forms.ImageList( this.components );
      this.splRemote = new System.Windows.Forms.Splitter();
      this.trvRemoteDir = new System.Windows.Forms.TreeView();
      this.pnlRemoteTitle = new System.Windows.Forms.Panel();
      this.lblRemoteTitle = new System.Windows.Forms.Label();
      this.splMain = new System.Windows.Forms.Splitter();
      this.pnlLocal = new System.Windows.Forms.Panel();
      this.lsvLocalFiles = new System.Windows.Forms.ListView();
      this.colLocalName = new System.Windows.Forms.ColumnHeader();
      this.colLocalSize = new System.Windows.Forms.ColumnHeader();
      this.colLocalDate = new System.Windows.Forms.ColumnHeader();
      this.tlbLocalFolderContent = new System.Windows.Forms.ToolBar();
      this.btnLocalDelete = new System.Windows.Forms.ToolBarButton();
      this.btnLocalRename = new System.Windows.Forms.ToolBarButton();
      this.btnLocalCreateFolder = new System.Windows.Forms.ToolBarButton();
      this.btnLocalTransfer = new System.Windows.Forms.ToolBarButton();
      this.splLocal = new System.Windows.Forms.Splitter();
      this.trvLocalDir = new System.Windows.Forms.TreeView();
      this.pnlLocalTitle = new System.Windows.Forms.Panel();
      this.lblLocalTitle = new System.Windows.Forms.Label();
      this.tlbMain = new System.Windows.Forms.ToolBar();
      this.btnConnect = new System.Windows.Forms.ToolBarButton();
      this.btnDisconnect = new System.Windows.Forms.ToolBarButton();
      this.btnChangeUser = new System.Windows.Forms.ToolBarButton();
      this.btnAbort = new System.Windows.Forms.ToolBarButton();
      this.imgMain = new System.Windows.Forms.ImageList( this.components );
      this.txtConnectionLogInformation = new System.Windows.Forms.TextBox();
      this.pgbTransfer = new System.Windows.Forms.ProgressBar();
      this.mainMenu = new System.Windows.Forms.MainMenu( this.components );
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlConnectionStatus ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlProgressInfo ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlSpeed ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlSecure ) ).BeginInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlProgress ) ).BeginInit();
      this.pnlRemote.SuspendLayout();
      this.pnlRemoteTitle.SuspendLayout();
      this.pnlLocal.SuspendLayout();
      this.pnlLocalTitle.SuspendLayout();
      this.SuspendLayout();
      // 
      // mnuFile
      // 
      this.mnuFile.Index = 0;
      this.mnuFile.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuFileExit} );
      this.mnuFile.Text = "&File";
      // 
      // mnuFileExit
      // 
      this.mnuFileExit.Index = 0;
      this.mnuFileExit.Text = "E&xit";
      this.mnuFileExit.Click += new System.EventHandler( this.mnuFileExit_Click );
      // 
      // mnuAction
      // 
      this.mnuAction.Index = 1;
      this.mnuAction.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuActionConnect,
            this.mnuActionDisconnect,
            this.mnuActionChangeCurrentUser,
            this.mnuActionAbort} );
      this.mnuAction.Text = "&Actions";
      // 
      // mnuActionConnect
      // 
      this.mnuActionConnect.Index = 0;
      this.mnuActionConnect.Text = "&Connect...";
      this.mnuActionConnect.Click += new System.EventHandler( this.mnuActionConnect_Click );
      // 
      // mnuActionDisconnect
      // 
      this.mnuActionDisconnect.Index = 1;
      this.mnuActionDisconnect.Text = "&Disconnect";
      this.mnuActionDisconnect.Click += new System.EventHandler( this.mnuActionDisconnect_Click );
      // 
      // mnuActionChangeCurrentUser
      // 
      this.mnuActionChangeCurrentUser.Index = 2;
      this.mnuActionChangeCurrentUser.Text = "&Change current user...";
      this.mnuActionChangeCurrentUser.Click += new System.EventHandler( this.mnuActionChangeCurrentUser_Click );
      // 
      // mnuActionAbort
      // 
      this.mnuActionAbort.Index = 3;
      this.mnuActionAbort.Text = "&Abort!";
      this.mnuActionAbort.Click += new System.EventHandler( this.mnuActionAbort_Click );
      // 
      // mnuRemote
      // 
      this.mnuRemote.Index = 2;
      this.mnuRemote.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuRemoteDelete,
            this.mnuRemoteRename,
            this.mnuRemoteCreateFolder,
            this.mnuRemoteTransfer} );
      this.mnuRemote.Text = "&Remote";
      // 
      // mnuRemoteDelete
      // 
      this.mnuRemoteDelete.Index = 0;
      this.mnuRemoteDelete.Text = "&Delete...";
      this.mnuRemoteDelete.Click += new System.EventHandler( this.mnuRemoteDelete_Click );
      // 
      // mnuRemoteRename
      // 
      this.mnuRemoteRename.Index = 1;
      this.mnuRemoteRename.Text = "&Rename...";
      this.mnuRemoteRename.Click += new System.EventHandler( this.mnuRemoteRename_Click );
      // 
      // mnuRemoteCreateFolder
      // 
      this.mnuRemoteCreateFolder.Index = 2;
      this.mnuRemoteCreateFolder.Text = "&Create folder...";
      this.mnuRemoteCreateFolder.Click += new System.EventHandler( this.mnuRemoteCreateFolder_Click );
      // 
      // mnuRemoteTransfer
      // 
      this.mnuRemoteTransfer.Index = 3;
      this.mnuRemoteTransfer.Text = "&Transfer";
      this.mnuRemoteTransfer.Click += new System.EventHandler( this.mnuRemoteTransfer_Click );
      // 
      // mnuLocal
      // 
      this.mnuLocal.Index = 3;
      this.mnuLocal.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuLocalDelete,
            this.mnuLocalRename,
            this.mnuLocalCreateFolder,
            this.mnuLocalTransfer} );
      this.mnuLocal.Text = "&Local";
      // 
      // mnuLocalDelete
      // 
      this.mnuLocalDelete.Index = 0;
      this.mnuLocalDelete.Text = "&Delete...";
      this.mnuLocalDelete.Click += new System.EventHandler( this.mnuLocalDelete_Click );
      // 
      // mnuLocalRename
      // 
      this.mnuLocalRename.Index = 1;
      this.mnuLocalRename.Text = "&Rename...";
      this.mnuLocalRename.Click += new System.EventHandler( this.mnuLocalRename_Click );
      // 
      // mnuLocalCreateFolder
      // 
      this.mnuLocalCreateFolder.Index = 2;
      this.mnuLocalCreateFolder.Text = "&Create folder...";
      this.mnuLocalCreateFolder.Click += new System.EventHandler( this.mnuLocalCreateFolder_Click );
      // 
      // mnuLocalTransfer
      // 
      this.mnuLocalTransfer.Index = 3;
      this.mnuLocalTransfer.Text = "&Transfer";
      this.mnuLocalTransfer.Click += new System.EventHandler( this.mnuLocalTransfer_Click );
      // 
      // mnuOption
      // 
      this.mnuOption.Index = 4;
      this.mnuOption.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuOptionPassiveTransfer,
            this.mnuOptionPreAllocateStorage,
            this.mnuOptionModeZ,
            this.mnuOptionRepresentationType,
            this.mnuOptionSecure} );
      this.mnuOption.Text = "&Options";
      // 
      // mnuOptionPassiveTransfer
      // 
      this.mnuOptionPassiveTransfer.Checked = true;
      this.mnuOptionPassiveTransfer.Index = 0;
      this.mnuOptionPassiveTransfer.Text = "&Passive transfer";
      this.mnuOptionPassiveTransfer.Click += new System.EventHandler( this.mnuOptionPassiveTransfer_Click );
      // 
      // mnuOptionPreAllocateStorage
      // 
      this.mnuOptionPreAllocateStorage.Index = 1;
      this.mnuOptionPreAllocateStorage.Text = "Pr&e allocate storage";
      this.mnuOptionPreAllocateStorage.Click += new System.EventHandler( this.mnuOptionPreAllocateStorage_Click );
      // 
      // mnuOptionModeZ
      // 
      this.mnuOptionModeZ.Index = 2;
      this.mnuOptionModeZ.Text = "Use Mode &Z";
      this.mnuOptionModeZ.Click += new System.EventHandler( this.mnuOptionModeZ_Click );
      // 
      // mnuOptionRepresentationType
      // 
      this.mnuOptionRepresentationType.Index = 3;
      this.mnuOptionRepresentationType.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuOptionRepresentationTypeAscii,
            this.mnuOptionRepresentationTypeBinary} );
      this.mnuOptionRepresentationType.Text = "&Representation type";
      // 
      // mnuOptionRepresentationTypeAscii
      // 
      this.mnuOptionRepresentationTypeAscii.Index = 0;
      this.mnuOptionRepresentationTypeAscii.RadioCheck = true;
      this.mnuOptionRepresentationTypeAscii.Text = "&Ascii";
      this.mnuOptionRepresentationTypeAscii.Click += new System.EventHandler( this.MenuOptionRepresentationTypes_Click );
      // 
      // mnuOptionRepresentationTypeBinary
      // 
      this.mnuOptionRepresentationTypeBinary.Checked = true;
      this.mnuOptionRepresentationTypeBinary.Index = 1;
      this.mnuOptionRepresentationTypeBinary.RadioCheck = true;
      this.mnuOptionRepresentationTypeBinary.Text = "&Binary";
      this.mnuOptionRepresentationTypeBinary.Click += new System.EventHandler( this.MenuOptionRepresentationTypes_Click );
      // 
      // mnuOptionSecure
      // 
      this.mnuOptionSecure.Index = 4;
      this.mnuOptionSecure.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuOptionSecureNone,
            this.mnuOptionSecureExplicit,
            this.mnuOptionSecureImplicit} );
      this.mnuOptionSecure.Text = "&Secure connection";
      // 
      // mnuOptionSecureNone
      // 
      this.mnuOptionSecureNone.Checked = true;
      this.mnuOptionSecureNone.Index = 0;
      this.mnuOptionSecureNone.RadioCheck = true;
      this.mnuOptionSecureNone.Text = "&None";
      this.mnuOptionSecureNone.Click += new System.EventHandler( this.MenuOptionSecure_Click );
      // 
      // mnuOptionSecureExplicit
      // 
      this.mnuOptionSecureExplicit.Index = 1;
      this.mnuOptionSecureExplicit.RadioCheck = true;
      this.mnuOptionSecureExplicit.Text = "&Explicit TLS authentication";
      this.mnuOptionSecureExplicit.Click += new System.EventHandler( this.MenuOptionSecure_Click );
      // 
      // mnuOptionSecureImplicit
      // 
      this.mnuOptionSecureImplicit.Index = 2;
      this.mnuOptionSecureImplicit.RadioCheck = true;
      this.mnuOptionSecureImplicit.Text = "&Implicit TLS authentication";
      this.mnuOptionSecureImplicit.Click += new System.EventHandler( this.MenuOptionSecure_Click );
      // 
      // stbMain
      // 
      this.stbMain.Location = new System.Drawing.Point( 0, 459 );
      this.stbMain.Name = "stbMain";
      this.stbMain.Panels.AddRange( new System.Windows.Forms.StatusBarPanel[] {
            this.pnlConnectionStatus,
            this.pnlProgressInfo,
            this.pnlSpeed,
            this.pnlSecure,
            this.pnlProgress} );
      this.stbMain.ShowPanels = true;
      this.stbMain.Size = new System.Drawing.Size( 672, 22 );
      this.stbMain.TabIndex = 0;
      // 
      // pnlConnectionStatus
      // 
      this.pnlConnectionStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
      this.pnlConnectionStatus.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "pnlConnectionStatus.Icon" ) ) );
      this.pnlConnectionStatus.Name = "pnlConnectionStatus";
      this.pnlConnectionStatus.Text = "Disconnected";
      this.pnlConnectionStatus.Width = 480;
      // 
      // pnlProgressInfo
      // 
      this.pnlProgressInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
      this.pnlProgressInfo.MinWidth = 25;
      this.pnlProgressInfo.Name = "pnlProgressInfo";
      this.pnlProgressInfo.Width = 25;
      // 
      // pnlSpeed
      // 
      this.pnlSpeed.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
      this.pnlSpeed.MinWidth = 25;
      this.pnlSpeed.Name = "pnlSpeed";
      this.pnlSpeed.Width = 25;
      // 
      // pnlSecure
      // 
      this.pnlSecure.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "pnlSecure.Icon" ) ) );
      this.pnlSecure.MinWidth = 0;
      this.pnlSecure.Name = "pnlSecure";
      this.pnlSecure.ToolTipText = "An SSL connection is active";
      this.pnlSecure.Width = 25;
      // 
      // pnlProgress
      // 
      this.pnlProgress.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
      this.pnlProgress.Name = "pnlProgress";
      this.pnlProgress.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
      // 
      // pnlRemote
      // 
      this.pnlRemote.Controls.Add( this.lsvRemoteFiles );
      this.pnlRemote.Controls.Add( this.tlbRemoteFolderContent );
      this.pnlRemote.Controls.Add( this.splRemote );
      this.pnlRemote.Controls.Add( this.trvRemoteDir );
      this.pnlRemote.Controls.Add( this.pnlRemoteTitle );
      this.pnlRemote.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlRemote.Location = new System.Drawing.Point( 0, 42 );
      this.pnlRemote.Name = "pnlRemote";
      this.pnlRemote.Size = new System.Drawing.Size( 672, 161 );
      this.pnlRemote.TabIndex = 0;
      // 
      // lsvRemoteFiles
      // 
      this.lsvRemoteFiles.AllowColumnReorder = true;
      this.lsvRemoteFiles.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.colRemoteName,
            this.colRemoteSize,
            this.colRemoteDateTime} );
      this.lsvRemoteFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvRemoteFiles.Location = new System.Drawing.Point( 163, 66 );
      this.lsvRemoteFiles.Name = "lsvRemoteFiles";
      this.lsvRemoteFiles.Size = new System.Drawing.Size( 509, 95 );
      this.lsvRemoteFiles.SmallImageList = this.imgFtpItemType;
      this.lsvRemoteFiles.TabIndex = 3;
      this.lsvRemoteFiles.UseCompatibleStateImageBehavior = false;
      this.lsvRemoteFiles.View = System.Windows.Forms.View.Details;
      this.lsvRemoteFiles.DoubleClick += new System.EventHandler( this.lsvRemoteFiles_DoubleClick );
      // 
      // colRemoteName
      // 
      this.colRemoteName.Text = "Name";
      this.colRemoteName.Width = 240;
      // 
      // colRemoteSize
      // 
      this.colRemoteSize.Text = "Size";
      this.colRemoteSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.colRemoteSize.Width = 129;
      // 
      // colRemoteDateTime
      // 
      this.colRemoteDateTime.Text = "Date";
      this.colRemoteDateTime.Width = 135;
      // 
      // imgFtpItemType
      // 
      this.imgFtpItemType.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "imgFtpItemType.ImageStream" ) ) );
      this.imgFtpItemType.TransparentColor = System.Drawing.Color.Transparent;
      this.imgFtpItemType.Images.SetKeyName( 0, "" );
      this.imgFtpItemType.Images.SetKeyName( 1, "" );
      this.imgFtpItemType.Images.SetKeyName( 2, "" );
      this.imgFtpItemType.Images.SetKeyName( 3, "" );
      this.imgFtpItemType.Images.SetKeyName( 4, "" );
      // 
      // tlbRemoteFolderContent
      // 
      this.tlbRemoteFolderContent.Buttons.AddRange( new System.Windows.Forms.ToolBarButton[] {
            this.btnRemoteDelete,
            this.btnRemoteRename,
            this.btnRemoteCreateFolder,
            this.btnRemoteTransfer} );
      this.tlbRemoteFolderContent.DropDownArrows = true;
      this.tlbRemoteFolderContent.ImageList = this.imgFolderContentToolbar;
      this.tlbRemoteFolderContent.Location = new System.Drawing.Point( 163, 24 );
      this.tlbRemoteFolderContent.Name = "tlbRemoteFolderContent";
      this.tlbRemoteFolderContent.ShowToolTips = true;
      this.tlbRemoteFolderContent.Size = new System.Drawing.Size( 509, 42 );
      this.tlbRemoteFolderContent.TabIndex = 2;
      this.tlbRemoteFolderContent.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler( this.tlbRemoteFolderContent_ButtonClick );
      // 
      // btnRemoteDelete
      // 
      this.btnRemoteDelete.ImageIndex = 0;
      this.btnRemoteDelete.Name = "btnRemoteDelete";
      this.btnRemoteDelete.Text = "Delete";
      // 
      // btnRemoteRename
      // 
      this.btnRemoteRename.ImageIndex = 1;
      this.btnRemoteRename.Name = "btnRemoteRename";
      this.btnRemoteRename.Text = "Rename";
      // 
      // btnRemoteCreateFolder
      // 
      this.btnRemoteCreateFolder.ImageIndex = 3;
      this.btnRemoteCreateFolder.Name = "btnRemoteCreateFolder";
      this.btnRemoteCreateFolder.Text = "Create folder";
      // 
      // btnRemoteTransfer
      // 
      this.btnRemoteTransfer.ImageIndex = 2;
      this.btnRemoteTransfer.Name = "btnRemoteTransfer";
      this.btnRemoteTransfer.Text = "Transfer";
      // 
      // imgFolderContentToolbar
      // 
      this.imgFolderContentToolbar.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "imgFolderContentToolbar.ImageStream" ) ) );
      this.imgFolderContentToolbar.TransparentColor = System.Drawing.Color.Transparent;
      this.imgFolderContentToolbar.Images.SetKeyName( 0, "" );
      this.imgFolderContentToolbar.Images.SetKeyName( 1, "" );
      this.imgFolderContentToolbar.Images.SetKeyName( 2, "" );
      this.imgFolderContentToolbar.Images.SetKeyName( 3, "" );
      // 
      // splRemote
      // 
      this.splRemote.Location = new System.Drawing.Point( 160, 24 );
      this.splRemote.Name = "splRemote";
      this.splRemote.Size = new System.Drawing.Size( 3, 137 );
      this.splRemote.TabIndex = 2;
      this.splRemote.TabStop = false;
      // 
      // trvRemoteDir
      // 
      this.trvRemoteDir.Dock = System.Windows.Forms.DockStyle.Left;
      this.trvRemoteDir.HideSelection = false;
      this.trvRemoteDir.ImageIndex = 0;
      this.trvRemoteDir.ImageList = this.imgFtpItemType;
      this.trvRemoteDir.Location = new System.Drawing.Point( 0, 24 );
      this.trvRemoteDir.Name = "trvRemoteDir";
      this.trvRemoteDir.SelectedImageIndex = 1;
      this.trvRemoteDir.ShowLines = false;
      this.trvRemoteDir.Size = new System.Drawing.Size( 160, 137 );
      this.trvRemoteDir.Sorted = true;
      this.trvRemoteDir.TabIndex = 1;
      this.trvRemoteDir.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler( this.trvRemoteDir_BeforeExpand );
      this.trvRemoteDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.trvRemoteDir_AfterSelect );
      // 
      // pnlRemoteTitle
      // 
      this.pnlRemoteTitle.BackColor = System.Drawing.SystemColors.Highlight;
      this.pnlRemoteTitle.Controls.Add( this.lblRemoteTitle );
      this.pnlRemoteTitle.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlRemoteTitle.Location = new System.Drawing.Point( 0, 0 );
      this.pnlRemoteTitle.Name = "pnlRemoteTitle";
      this.pnlRemoteTitle.Padding = new System.Windows.Forms.Padding( 4 );
      this.pnlRemoteTitle.Size = new System.Drawing.Size( 672, 24 );
      this.pnlRemoteTitle.TabIndex = 0;
      // 
      // lblRemoteTitle
      // 
      this.lblRemoteTitle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblRemoteTitle.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.lblRemoteTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.lblRemoteTitle.Location = new System.Drawing.Point( 4, 4 );
      this.lblRemoteTitle.Name = "lblRemoteTitle";
      this.lblRemoteTitle.Size = new System.Drawing.Size( 664, 16 );
      this.lblRemoteTitle.TabIndex = 0;
      this.lblRemoteTitle.Text = "Remote";
      // 
      // splMain
      // 
      this.splMain.Dock = System.Windows.Forms.DockStyle.Top;
      this.splMain.Location = new System.Drawing.Point( 0, 203 );
      this.splMain.Name = "splMain";
      this.splMain.Size = new System.Drawing.Size( 672, 3 );
      this.splMain.TabIndex = 2;
      this.splMain.TabStop = false;
      // 
      // pnlLocal
      // 
      this.pnlLocal.Controls.Add( this.lsvLocalFiles );
      this.pnlLocal.Controls.Add( this.tlbLocalFolderContent );
      this.pnlLocal.Controls.Add( this.splLocal );
      this.pnlLocal.Controls.Add( this.trvLocalDir );
      this.pnlLocal.Controls.Add( this.pnlLocalTitle );
      this.pnlLocal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlLocal.Location = new System.Drawing.Point( 0, 206 );
      this.pnlLocal.Name = "pnlLocal";
      this.pnlLocal.Size = new System.Drawing.Size( 672, 173 );
      this.pnlLocal.TabIndex = 4;
      // 
      // lsvLocalFiles
      // 
      this.lsvLocalFiles.AllowColumnReorder = true;
      this.lsvLocalFiles.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.colLocalName,
            this.colLocalSize,
            this.colLocalDate} );
      this.lsvLocalFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lsvLocalFiles.Location = new System.Drawing.Point( 163, 66 );
      this.lsvLocalFiles.Name = "lsvLocalFiles";
      this.lsvLocalFiles.Size = new System.Drawing.Size( 509, 107 );
      this.lsvLocalFiles.SmallImageList = this.imgFtpItemType;
      this.lsvLocalFiles.TabIndex = 7;
      this.lsvLocalFiles.UseCompatibleStateImageBehavior = false;
      this.lsvLocalFiles.View = System.Windows.Forms.View.Details;
      this.lsvLocalFiles.DoubleClick += new System.EventHandler( this.lsvLocalFiles_DoubleClick );
      // 
      // colLocalName
      // 
      this.colLocalName.Text = "Name";
      this.colLocalName.Width = 240;
      // 
      // colLocalSize
      // 
      this.colLocalSize.Text = "Size";
      this.colLocalSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.colLocalSize.Width = 129;
      // 
      // colLocalDate
      // 
      this.colLocalDate.Text = "Date";
      this.colLocalDate.Width = 135;
      // 
      // tlbLocalFolderContent
      // 
      this.tlbLocalFolderContent.Buttons.AddRange( new System.Windows.Forms.ToolBarButton[] {
            this.btnLocalDelete,
            this.btnLocalRename,
            this.btnLocalCreateFolder,
            this.btnLocalTransfer} );
      this.tlbLocalFolderContent.DropDownArrows = true;
      this.tlbLocalFolderContent.ImageList = this.imgFolderContentToolbar;
      this.tlbLocalFolderContent.Location = new System.Drawing.Point( 163, 24 );
      this.tlbLocalFolderContent.Name = "tlbLocalFolderContent";
      this.tlbLocalFolderContent.ShowToolTips = true;
      this.tlbLocalFolderContent.Size = new System.Drawing.Size( 509, 42 );
      this.tlbLocalFolderContent.TabIndex = 6;
      this.tlbLocalFolderContent.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler( this.tlbLocalFolderContent_ButtonClick );
      // 
      // btnLocalDelete
      // 
      this.btnLocalDelete.ImageIndex = 0;
      this.btnLocalDelete.Name = "btnLocalDelete";
      this.btnLocalDelete.Text = "Delete";
      // 
      // btnLocalRename
      // 
      this.btnLocalRename.ImageIndex = 1;
      this.btnLocalRename.Name = "btnLocalRename";
      this.btnLocalRename.Text = "Rename";
      // 
      // btnLocalCreateFolder
      // 
      this.btnLocalCreateFolder.ImageIndex = 3;
      this.btnLocalCreateFolder.Name = "btnLocalCreateFolder";
      this.btnLocalCreateFolder.Text = "Create folder";
      // 
      // btnLocalTransfer
      // 
      this.btnLocalTransfer.ImageIndex = 2;
      this.btnLocalTransfer.Name = "btnLocalTransfer";
      this.btnLocalTransfer.Text = "Transfer";
      // 
      // splLocal
      // 
      this.splLocal.Location = new System.Drawing.Point( 160, 24 );
      this.splLocal.Name = "splLocal";
      this.splLocal.Size = new System.Drawing.Size( 3, 149 );
      this.splLocal.TabIndex = 2;
      this.splLocal.TabStop = false;
      // 
      // trvLocalDir
      // 
      this.trvLocalDir.Dock = System.Windows.Forms.DockStyle.Left;
      this.trvLocalDir.HideSelection = false;
      this.trvLocalDir.ImageIndex = 0;
      this.trvLocalDir.ImageList = this.imgFtpItemType;
      this.trvLocalDir.Location = new System.Drawing.Point( 0, 24 );
      this.trvLocalDir.Name = "trvLocalDir";
      this.trvLocalDir.SelectedImageIndex = 0;
      this.trvLocalDir.ShowLines = false;
      this.trvLocalDir.Size = new System.Drawing.Size( 160, 149 );
      this.trvLocalDir.Sorted = true;
      this.trvLocalDir.TabIndex = 5;
      this.trvLocalDir.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler( this.trvLocalDir_BeforeExpand );
      this.trvLocalDir.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.trvLocalDir_AfterSelect );
      // 
      // pnlLocalTitle
      // 
      this.pnlLocalTitle.BackColor = System.Drawing.SystemColors.Highlight;
      this.pnlLocalTitle.Controls.Add( this.lblLocalTitle );
      this.pnlLocalTitle.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlLocalTitle.Location = new System.Drawing.Point( 0, 0 );
      this.pnlLocalTitle.Name = "pnlLocalTitle";
      this.pnlLocalTitle.Padding = new System.Windows.Forms.Padding( 4 );
      this.pnlLocalTitle.Size = new System.Drawing.Size( 672, 24 );
      this.pnlLocalTitle.TabIndex = 0;
      // 
      // lblLocalTitle
      // 
      this.lblLocalTitle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblLocalTitle.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.lblLocalTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.lblLocalTitle.Location = new System.Drawing.Point( 4, 4 );
      this.lblLocalTitle.Name = "lblLocalTitle";
      this.lblLocalTitle.Size = new System.Drawing.Size( 664, 16 );
      this.lblLocalTitle.TabIndex = 0;
      this.lblLocalTitle.Text = "Local";
      // 
      // tlbMain
      // 
      this.tlbMain.Buttons.AddRange( new System.Windows.Forms.ToolBarButton[] {
            this.btnConnect,
            this.btnDisconnect,
            this.btnChangeUser,
            this.btnAbort} );
      this.tlbMain.DropDownArrows = true;
      this.tlbMain.ImageList = this.imgMain;
      this.tlbMain.Location = new System.Drawing.Point( 0, 0 );
      this.tlbMain.Name = "tlbMain";
      this.tlbMain.ShowToolTips = true;
      this.tlbMain.Size = new System.Drawing.Size( 672, 42 );
      this.tlbMain.TabIndex = 8;
      this.tlbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler( this.tlbMain_ButtonClick );
      // 
      // btnConnect
      // 
      this.btnConnect.ImageIndex = 0;
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Text = "Connect";
      // 
      // btnDisconnect
      // 
      this.btnDisconnect.ImageIndex = 1;
      this.btnDisconnect.Name = "btnDisconnect";
      this.btnDisconnect.Text = "Disconnect";
      // 
      // btnChangeUser
      // 
      this.btnChangeUser.ImageIndex = 2;
      this.btnChangeUser.Name = "btnChangeUser";
      this.btnChangeUser.Text = "Change current user";
      // 
      // btnAbort
      // 
      this.btnAbort.ImageIndex = 3;
      this.btnAbort.Name = "btnAbort";
      this.btnAbort.Text = "Abort!";
      // 
      // imgMain
      // 
      this.imgMain.ImageStream = ( ( System.Windows.Forms.ImageListStreamer )( resources.GetObject( "imgMain.ImageStream" ) ) );
      this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
      this.imgMain.Images.SetKeyName( 0, "" );
      this.imgMain.Images.SetKeyName( 1, "" );
      this.imgMain.Images.SetKeyName( 2, "" );
      this.imgMain.Images.SetKeyName( 3, "" );
      // 
      // txtConnectionLogInformation
      // 
      this.txtConnectionLogInformation.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.txtConnectionLogInformation.Location = new System.Drawing.Point( 0, 379 );
      this.txtConnectionLogInformation.MaxLength = 0;
      this.txtConnectionLogInformation.Multiline = true;
      this.txtConnectionLogInformation.Name = "txtConnectionLogInformation";
      this.txtConnectionLogInformation.ReadOnly = true;
      this.txtConnectionLogInformation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtConnectionLogInformation.Size = new System.Drawing.Size( 672, 80 );
      this.txtConnectionLogInformation.TabIndex = 5;
      // 
      // pgbTransfer
      // 
      this.pgbTransfer.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.pgbTransfer.Location = new System.Drawing.Point( 560, 464 );
      this.pgbTransfer.Name = "pgbTransfer";
      this.pgbTransfer.Size = new System.Drawing.Size( 96, 14 );
      this.pgbTransfer.TabIndex = 7;
      // 
      // mainMenu
      // 
      this.mainMenu.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuAction,
            this.mnuRemote,
            this.mnuLocal,
            this.mnuOption} );
      // 
      // MainForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size( 5, 14 );
      this.ClientSize = new System.Drawing.Size( 672, 481 );
      this.Controls.Add( this.pgbTransfer );
      this.Controls.Add( this.pnlLocal );
      this.Controls.Add( this.splMain );
      this.Controls.Add( this.pnlRemote );
      this.Controls.Add( this.tlbMain );
      this.Controls.Add( this.txtConnectionLogInformation );
      this.Controls.Add( this.stbMain );
      this.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte )( 0 ) ) );
      this.Icon = ( ( System.Drawing.Icon )( resources.GetObject( "$this.Icon" ) ) );
      this.Menu = this.mainMenu;
      this.Name = "MainForm";
      this.Text = "Client Ftp";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Resize += new System.EventHandler( this.MainForm_Resize );
      this.Closing += new System.ComponentModel.CancelEventHandler( this.MainForm_Closing );
      this.Load += new System.EventHandler( this.MainForm_Load );
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlConnectionStatus ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlProgressInfo ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlSpeed ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlSecure ) ).EndInit();
      ( ( System.ComponentModel.ISupportInitialize )( this.pnlProgress ) ).EndInit();
      this.pnlRemote.ResumeLayout( false );
      this.pnlRemote.PerformLayout();
      this.pnlRemoteTitle.ResumeLayout( false );
      this.pnlLocal.ResumeLayout( false );
      this.pnlLocal.PerformLayout();
      this.pnlLocalTitle.ResumeLayout( false );
      this.ResumeLayout( false );
      this.PerformLayout();

    }
    #endregion
  }
}
