'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [MainForm.vb]
 '*
 '* This is the main form of the application. All the user interaction
 '* is handled here.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Management
Imports System.Windows.Forms
Imports System.Data
Imports System.Threading
Imports System.IO
Imports Xceed.Compression
Imports Xceed.FileSystem
Imports Xceed.Ftp
Imports Xceed.Tar
Imports Xceed.GZip
Imports Xceed.Zip
Imports Xceed.FileSystem.Samples
Imports Xceed.FileSystem.Samples.Utils
Imports Xceed.FileSystem.Samples.Utils.API
Imports Xceed.FileSystem.Samples.Utils.FileSystem
Imports Xceed.FileSystem.Samples.Utils.Icons
Imports Xceed.FileSystem.Samples.Utils.ListView
Imports Xceed.FileSystem.Samples.Utils.TreeView

Namespace Xceed.FileSystem.Samples
  Public Class MainForm : Inherits System.Windows.Forms.Form
    #Region "CONSTRUCTORS"

    Public Sub New()
      InitializeComponent()

      ' Set the image list on the controls.
      FileView.SmallImageList = IconCache.SmallIconList
      FileView.LargeImageList = IconCache.LargeIconList
      FolderTree.ImageList = IconCache.SmallIconList
      System.Windows.Forms.TreeView.CheckForIllegalCrossThreadCalls = False
      MainStatusBar.ItemsImageList = IconCache.SmallIconList

      ' Load the treeview with the "My Computer" node.
      FolderTree.Nodes.Add(New MyComputerTreeViewNode(New MyComputerFolder(), FileView))

      ' Load the Ftp connections folder and add a connection to ftp.xceed.com
      Dim ftpConnections As FtpConnectionsFolder = New FtpConnectionsFolder()
      Dim node As AbstractTreeViewNode = New FtpConnectionsTreeViewNode(ftpConnections, FileView)
      FolderTree.Nodes.Add(node)

      Try
        Dim connection As New FtpConnection("ftp.xceed.com")

        AddHandler connection.CertificateReceived, AddressOf FtpConnection_CertificateReceived
        connection.SynchronizingObject = Me

        ftpConnections.CreateConnectionFolder(connection)
      Catch
      End Try

      ' Let's also add a virtual RAM drive just for fun!
      ' (and to show you how amasing are the MemoryFolder and MemoryFile classes!)
      Dim ramDrive As MemoryFolder = New MemoryFolder("RAM", "\")
      FolderTree.Nodes.Add(New FolderTreeViewNode(ramDrive, FileView, "Memory folder"))

      ' Let's add a user store drive
      Dim isoDrive As IsolatedFolder = New IsolatedFolder("\")
      FolderTree.Nodes.Add(New FolderTreeViewNode(isoDrive, FileView, "User's folder"))

      ' Set the second node as the starting point, to avoid exception on empty floppy      
      FolderTree.SelectedNode = FolderTree.Nodes(0)

      ' Initialize the clipboard.
      m_clipBoard = New DataObject
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PRIVATE METHODS"

    Private Sub CreateFile()
      Dim fileName As String = "New file"
      Dim fileExtension As String = ".txt"
      Dim fileNameTemplate As String = fileName & " ({0})" & fileExtension
      Dim fileNameIndex As Integer = 2

      ' Get a reference on the selected node.
      Dim parentNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)

      ' The folder might not support file creation.
      Dim file As AbstractFile = Nothing

      Try
        file = parentNode.Folder.GetFile(fileName & fileExtension)

        ' We want a file that does not already exists.
        Do While file.Exists
          file = parentNode.Folder.GetFile(String.Format(fileNameTemplate, fileNameIndex))
          fileNameIndex += 1
        Loop
      Catch
        MessageBox.Show(Me, "Cannot create files in this folder.")
        Return
      End Try

      ' Create an AbstractListViewItem and put it in edit mode. (a flag must
      ' be raised so that when edition is done, we actually create the file.
      m_creatingItem = True

      Dim item As AbstractListViewItem = New FileListViewItem(file)
      FileView.Items.Add(item)

      item.BeginEdit()
    End Sub

    Private Sub CreateFolder()
      Dim folderName As String = "New folder"
      Dim folderNameTemplate As String = folderName & " ({0})"
      Dim folderNameIndex As Integer = 2

      ' Get a reference on the selected node.
      Dim parentNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)

      ' The folder might not support folder creation.
      Dim folder As AbstractFolder = Nothing

      Try
        folder = parentNode.Folder.GetFolder(folderName)

        ' We want a folder that does not already exists.
        Do While folder.Exists
          folder = parentNode.Folder.GetFolder(String.Format(folderNameTemplate, folderNameIndex))
          folderNameIndex += 1
        Loop
      Catch
        MessageBox.Show(Me, "Cannot create folders in this folder.")
        Return
      End Try

      ' Create an AbstractListViewItem and put it in edit mode. (a flag must
      ' be raised so that when edition is done, we actually create the folder.
      m_creatingItem = True

      Dim item As AbstractListViewItem = New FolderListViewItem(folder, parentNode)
      FileView.Items.Add(item)

      item.BeginEdit()
    End Sub

    Private Sub CreateGZipArchive()
      Dim archiveName As String = "New GZip archive"
      Dim archiveExtension As String = ".gz"
      Dim archiveNameTemplate As String = archiveName & " ({0})" & archiveExtension
      Dim archiveNameIndex As Integer = 2

      ' Get a reference on the selected node.
      Dim parentNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)

      ' The folder might not support file creation.
      Dim gzipFile As AbstractFile = Nothing

      Try
        gzipFile = parentNode.Folder.GetFile(archiveName & archiveExtension)

        ' We want a file that does not already exists.
        Do While gzipFile.Exists
          gzipFile = parentNode.Folder.GetFile(String.Format(archiveNameTemplate, archiveNameIndex))
          archiveNameIndex += 1
        Loop
      Catch
        MessageBox.Show(Me, "Cannot create files in this folder.")
        Return
      End Try

      ' Create an AbstractListViewItem and put it in edit mode. (a flag must
      ' be raised so that when edition is done, we actually create the folder.
      m_creatingItem = True

      Dim item As AbstractListViewItem = New GZipArchiveListViewItem(gzipFile, parentNode)
      FileView.Items.Add(item)

      item.BeginEdit()
    End Sub

    Private Sub CreateTarArchive()
      Dim archiveName As String = "New Tar archive"
      Dim archiveExtension As String = ".tar"
      Dim archiveNameTemplate As String = archiveName & " ({0})" & archiveExtension
      Dim archiveNameIndex As Integer = 2

      ' Get a reference on the selected node.
      Dim parentNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)

      ' The folder might not support file creation.
      Dim tarFile As AbstractFile = Nothing

      Try
        tarFile = parentNode.Folder.GetFile(archiveName & archiveExtension)

        ' We want a file that does not already exists.
        Do While tarFile.Exists
          tarFile = parentNode.Folder.GetFile(String.Format(archiveNameTemplate, archiveNameIndex))
          archiveNameIndex += 1
        Loop
      Catch
        MessageBox.Show(Me, "Cannot create files in this folder.")
        Return
      End Try

      ' Create an AbstractListViewItem and put it in edit mode. (a flag must
      ' be raised so that when edition is done, we actually create the folder.
      m_creatingItem = True

      Dim item As AbstractListViewItem = New TarArchiveListViewItem(tarFile, parentNode)
      FileView.Items.Add(item)

      item.BeginEdit()
    End Sub

    Private Sub CreateZipArchive()
      Dim archiveName As String = "New Zip archive"
      Dim archiveExtension As String = ".zip"
      Dim archiveNameTemplate As String = archiveName & " ({0})" & archiveExtension
      Dim archiveNameIndex As Integer = 2

      ' Get a reference on the selected node.
      Dim parentNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)

      ' The folder might not support file creation.
      Dim zipFile As AbstractFile = Nothing

      Try
        zipFile = parentNode.Folder.GetFile(archiveName & archiveExtension)

        ' We want a file that does not already exists.
        Do While zipFile.Exists
          zipFile = parentNode.Folder.GetFile(String.Format(archiveNameTemplate, archiveNameIndex))
          archiveNameIndex += 1
        Loop
      Catch
        MessageBox.Show(Me, "Cannot create files in this folder.")
        Return
      End Try

      ' Create an AbstractListViewItem and put it in edit mode. (a flag must
      ' be raised so that when edition is done, we actually create the folder.
      m_creatingItem = True

      Dim item As AbstractListViewItem = New ZipArchiveListViewItem(zipFile, parentNode)
      FileView.Items.Add(item)

      item.BeginEdit()
    End Sub

    Private Sub CreateFtpConnection()
      Dim ftpConnectionInfo As FtpConnectionInformationForm = New FtpConnectionInformationForm
      Try
        Dim ftpConnection As FtpConnection = Nothing

        ' We need information from the user.
        If ftpConnectionInfo.ShowDialog(Me, ftpConnection) = System.Windows.Forms.DialogResult.OK Then
          Dim node As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)
          Dim ftpFolders As FtpConnectionsFolder = CType(IIf(TypeOf node.Folder Is FtpConnectionsFolder, node.Folder, Nothing), FtpConnectionsFolder)

          AddHandler ftpConnection.CertificateReceived, AddressOf FtpConnection_CertificateReceived
          ftpConnection.SynchronizingObject = Me

          Try
            ' Create the ftp connection. 
            Dim ftpFolder As FtpConnectionFolder = ftpFolders.CreateConnectionFolder(ftpConnection)

            ' Add the connection in the FileView.
            FileView.Items.Add(New FolderListViewItem(ftpFolder, node))

            ' Add the connection in the FolderTree.
            node.Nodes.Add(New FolderTreeViewNode(ftpFolder, FileView))

            If (Not node.IsExpanded) Then
              node.Expand()
            End If
          Catch
            MessageBox.Show(Me, "Can't create ftp connection.", "Ftp connection error", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End Try
        End If
      Finally
        CType(ftpConnectionInfo, IDisposable).Dispose()
      End Try
    End Sub

    Private Sub CopyItems()
      m_clipboardCutItems = False

      Me.FillClipboard()
    End Sub

    Private Sub CutItems()
      m_clipboardCutItems = True

      Me.FillClipboard()
    End Sub

    Private Sub DeleteItems()
      If FileView.SelectedItems.Count > 0 Then
        Dim deleteConfirmed As Boolean = False

        ' When there are multiple items to delete, we only want to show one message.
        If FileView.SelectedItems.Count > 1 Then
          Dim message As String = String.Format("Are you sure you want to delete these {0} items?", FileView.SelectedItems.Count)
          Dim caption As String = "Confirm Multiple File Delete"

          deleteConfirmed = True

          If MessageBox.Show(Me, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.No Then
            Return
          End If
        End If

        FileView.BeginUpdate()

        For Each item As AbstractListViewItem In FileView.SelectedItems
          Try
            item.Delete((Not deleteConfirmed))
          Catch
            MessageBox.Show(Me, String.Format("The item {0} could not be deleted.", item.Text), "Error deleting item", MessageBoxButtons.OK, MessageBoxIcon.Error)
          End Try
        Next item

        FileView.EndUpdate()
      End If
    End Sub

    Private Sub FillClipboard()
      If FileView.SelectedItems.Count > 0 Then
        Dim items As AbstractListViewItem() = New AbstractListViewItem(FileView.SelectedItems.Count - 1) {}

        Dim i As Integer = 0
        Do While i < FileView.SelectedItems.Count
          items(i) = CType(FileView.SelectedItems(i), AbstractListViewItem)
          i += 1
        Loop

        m_clipBoard = New DataObject(items)
      End If

      Me.UpdateToolbarState()
    End Sub

    Private Sub OpenItem()
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

      If FileView.SelectedItems.Count > 0 Then
        Dim selectedItem As AbstractListViewItem = CType(FileView.SelectedItems(0), AbstractListViewItem)

        If selectedItem Is Nothing Then
          Return
        End If

        If Not selectedItem.ParentNode Is Nothing Then
          ' Dealing with a folder, we will navigate to it in the navigation treeview.
          Dim matchingNode As AbstractTreeViewNode = Me.FindMatchingNode(selectedItem.ParentNode, selectedItem.FileSystemItem)

          If Not matchingNode Is Nothing Then
            matchingNode.EnsureVisible()
            FolderTree.SelectedNode = matchingNode
            matchingNode.Expand()
          End If
        Else
          Dim fileName As String = selectedItem.FileSystemItem.FullName

          If Not (TypeOf selectedItem.FileSystemItem Is DiskFile) Then
            ' If the file is not already a local file, we need to copy that 
            ' file to a temporary directory before launching the process. 

            Dim actionForm As TemporaryFileCopyActionForm = New TemporaryFileCopyActionForm
            Try
              Dim fileOnly As Boolean
              Dim recursive As Boolean

              ' Ask the user what files should be copied locally.
              If actionForm.ShowDialog(Me, fileOnly, recursive) = System.Windows.Forms.DialogResult.Cancel Then
                Return
              End If

              Dim events As FileSystemEvents = New FileSystemEvents
              AddHandler events.ByteProgression, AddressOf Copy_ByteProgression
              AddHandler events.ItemException, AddressOf Copy_ItemException

              Try
                Dim tempFolderName As String = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString()
                Dim tempFolder As DiskFolder = New DiskFolder(tempFolderName)

                ' This will make the file to be destroyed when the application closes.
                Dim autoDeleter As autoDeleter = New autoDeleter(tempFolder)

                Dim progression As ProgressionForm = New ProgressionForm
                Try
                  progression.ActionText = "Copying file..."
                  progression.Show()

                  If fileOnly Then
                    ' Only the selected file. Just copy it to the temp directory.
                    fileName = selectedItem.FileSystemItem.CopyTo(events, progression, tempFolder, True).FullName
                  Else
                    ' Copy the files to the temporary folder.
                    selectedItem.FileSystemItem.ParentFolder.CopyFilesTo(events, progression, tempFolder, recursive, True)

                    ' We need to have the new fullname of the file to execute.
                    fileName = tempFolder.GetFile(selectedItem.FileSystemItem.Name).FullName
                  End If
                Finally
                  CType(progression, IDisposable).Dispose()
                End Try
              Catch e1 As System.Reflection.TargetInvocationException
                ' Operation aborted.
              Catch
                MessageBox.Show(Me, "An error occured while copying files.", "Error copying files", MessageBoxButtons.OK, MessageBoxIcon.Error)
              Finally
                RemoveHandler events.ByteProgression, AddressOf Copy_ByteProgression
                RemoveHandler events.ItemException, AddressOf Copy_ItemException
              End Try
            Finally
              CType(actionForm, IDisposable).Dispose()
            End Try
          End If

          Try
            ' Start the process.
            System.Diagnostics.Process.Start(fileName)
          Catch
          End Try
        End If
      End If

      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub PasteItems()
      ' Get the destination.
      Dim destination As AbstractFolder = (CType(FolderTree.SelectedNode, AbstractTreeViewNode)).Folder

      Me.PasteItems(destination)
    End Sub

    Private Sub PasteItems(ByVal destination As AbstractFolder)
      If destination Is Nothing Then
        Return
      End If

      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

      ' If destination is a ZIP, we need to refresh the options since they might
      ' have been changed.
      Dim archive As ZipArchive = CType(IIf(TypeOf destination Is ZipArchive, destination, Nothing), ZipArchive)

      If Not archive Is Nothing Then
        archive.DefaultCompressionLevel = Options.ZipDefaultCompressionLevel
        archive.DefaultCompressionMethod = Options.ZipDefaultCompressionMethod
        archive.DefaultEncryptionPassword = Options.ZipDefaultEncryptionPassword
        archive.DefaultEncryptionMethod = Options.ZipDefaultEncryptionMethod
        archive.DefaultEncryptionStrength = Options.ZipDefaultEncryptionStrength
      End If

      Dim needRefresh As Boolean = False

      ' Check to see if the clipboard contains valid objects.
      If (Not m_clipBoard.GetDataPresent(GetType(AbstractListViewItem()))) Then
        Return
      End If

      ' Initialize the FileSystem events.
      Dim copyEvents As FileSystemEvents = New FileSystemEvents
      AddHandler copyEvents.ItemException, AddressOf Copy_ItemException
      AddHandler copyEvents.ByteProgression, AddressOf Copy_ByteProgression

      Dim moveEvents As FileSystemEvents = New FileSystemEvents
      AddHandler moveEvents.ItemException, AddressOf Move_ItemException
      AddHandler moveEvents.ByteProgression, AddressOf Move_ByteProgression

      Try
        Dim items As AbstractListViewItem() = CType(m_clipBoard.GetData(GetType(AbstractListViewItem())), AbstractListViewItem())

        ' Paste each items in the destination. 
        For Each item As AbstractListViewItem In items
          Dim exists As Boolean = False
          Dim sourceEqualsDestination As Boolean = (item.FileSystemItem.ParentFolder.FullName.ToLower() = destination.FullName.ToLower())

          ' Check to see if the item is already present.
          Try
            exists = (destination.GetFile(item.FileSystemItem.Name).Exists OrElse destination.GetFolder(item.FileSystemItem.Name).Exists)
          Catch
          End Try

          If exists Then
            ' We don't ask a question for replacing the item if the source and destination folder is the same.
            ' When cutting and pasting over ourself, we do nothing and proceed to the next item.
            If sourceEqualsDestination Then
              If m_clipboardCutItems Then
                Exit For
              End If
            Else
              Dim message As String = "Do you want to replace the existing item?"
              Dim caption As String = "Replace confirmation"

              If MessageBox.Show(Me, message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = System.Windows.Forms.DialogResult.No Then
                Exit For
              End If
            End If
          End If

          If m_clipboardCutItems Then
            Dim progression As ProgressionForm = New ProgressionForm
            Try
              progression.ActionText = "Moving file..."
              progression.Show()

              Try
                item.FileSystemItem.MoveTo(moveEvents, progression, destination, True)
              Catch e1 As System.Reflection.TargetInvocationException
                ' Operation aborted.
              Catch
                MessageBox.Show(Me, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
              End Try
            Finally
              CType(progression, IDisposable).Dispose()
            End Try
          Else
            ' If the item is copied and pasted in place, we will add "Copy of" as prefix for the name.
            If exists AndAlso sourceEqualsDestination Then
              Dim prefix As String = "Copy of "
              Dim prefixIndex As Integer = 1
              Dim name As String = prefix & item.FileSystemItem.Name

              Do While destination.GetFile(name).Exists OrElse destination.GetFolder(name).Exists
                prefixIndex += 1
                prefix = String.Format("Copy ({0}) of ", prefixIndex)
                name = prefix & item.FileSystemItem.Name
              Loop

              Dim currentFile As AbstractFile = CType(IIf(TypeOf item.FileSystemItem Is AbstractFile, item.FileSystemItem, Nothing), AbstractFile)

              If Not currentFile Is Nothing Then
                Dim newFile As AbstractFile = destination.GetFile(name)

                Dim progression As ProgressionForm = New ProgressionForm
                Try
                  progression.ActionText = "Copying file..."
                  progression.Show()

                  Try
                    currentFile.CopyTo(copyEvents, progression, newFile, False)
                  Catch e2 As System.Reflection.TargetInvocationException
                    ' Operation aborted.
                  Catch
                    MessageBox.Show(Me, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                  End Try
                Finally
                  CType(progression, IDisposable).Dispose()
                End Try
              Else
                Dim currentFolder As AbstractFolder = CType(IIf(TypeOf item.FileSystemItem Is AbstractFolder, item.FileSystemItem, Nothing), AbstractFolder)
                Dim newFolder As AbstractFolder = destination.GetFolder(name)

                Dim progression As ProgressionForm = New ProgressionForm
                Try
                  progression.ActionText = "Copying file..."
                  progression.Show()

                  Try
                    currentFolder.CopyTo(copyEvents, progression, newFolder, False)
                  Catch e3 As System.Reflection.TargetInvocationException
                    ' Operation aborted.
                  Catch e4 As Exception
                    MessageBox.Show(Me, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                  End Try
                Finally
                  CType(progression, IDisposable).Dispose()
                End Try
              End If
            Else
              Dim progression As ProgressionForm = New ProgressionForm
              Try
                progression.ActionText = "Copying file..."
                progression.Show()

                Try
                  item.FileSystemItem.CopyTo(copyEvents, progression, destination, True)
                Catch e5 As System.Reflection.TargetInvocationException
                  ' Operation aborted.
                Catch e6 As Exception
                  MessageBox.Show(Me, "An error occured while copying files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
              Finally
                CType(progression, IDisposable).Dispose()
              End Try
            End If
          End If

          needRefresh = True
        Next item

        If m_clipboardCutItems Then
          m_clipBoard = Nothing
          m_clipboardCutItems = False
        End If

        If needRefresh Then
          Me.FillFolderTreeNode(CType(FolderTree.SelectedNode, AbstractTreeViewNode))
          Me.FillListView(CType(FolderTree.SelectedNode, AbstractTreeViewNode))
        End If
      Finally
        RemoveHandler copyEvents.ByteProgression, AddressOf Copy_ByteProgression
        RemoveHandler copyEvents.ItemException, AddressOf Copy_ItemException

        RemoveHandler moveEvents.ByteProgression, AddressOf Move_ByteProgression
        RemoveHandler moveEvents.ItemException, AddressOf Move_ItemException
      End Try

      System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub FillListView(ByVal parentNode As AbstractTreeViewNode)
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

      FileView.BeginUpdate()

      FileView.RemoveSort()

      ' Clear the actual list.
      FileView.Items.Clear()

      Dim parentFolder As AbstractFolder = parentNode.Folder

      If Not parentFolder Is Nothing Then
        ' Show/hide the CompressedSize column.
        Dim compressedSizeColumnSize As Integer = 0

        If Not parentFolder.RootFolder.HostFile Is Nothing Then
          compressedSizeColumnSize = FileView.Columns(2).Width

          If compressedSizeColumnSize = 0 Then
            compressedSizeColumnSize = 100
          End If
        End If

        FileView.Columns(2).Width = compressedSizeColumnSize

        Try
          ' Get a list of items from the parent folder.
          Dim items As FileSystemItem() = parentFolder.GetItems(False)

          ' Set defaults settings
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles
          zipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders

          ' Loop through the items and create an AbstractListViewItem for each.
          For Each item As FileSystemItem In items
            Dim listViewItem As AbstractListViewItem = Nothing
            Dim itemCreated As Boolean = False

            ' Folders are created as is. Nothing special to do.
            Dim folder As AbstractFolder = CType(IIf(TypeOf item Is AbstractFolder, item, Nothing), AbstractFolder)

            If Not folder Is Nothing Then
              listViewItem = New FolderListViewItem(folder, parentNode)
              itemCreated = True
            Else
              ' We are obviously a file. We must create a specialized AbstractFileListViewItem
              ' for archives. Those files can also be encrypted inside a Zip archive. We must
              ' handle those encrypted files and ask the user for a password if needed.
              Dim exceptionAction As ItemExceptionAction = ItemExceptionAction.Retry

              Do While (exceptionAction = ItemExceptionAction.Retry) AndAlso ((Not itemCreated))
                Try
                  Dim extension As String = Path.GetExtension(item.FullName)
                  Dim matchingNode As AbstractTreeViewNode = Me.FindMatchingNode(parentNode, item)
                  Dim file As AbstractFile = CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile)

                  Select Case extension.ToUpper()
                    ' GZip archive
                  Case ".GZ", ".TGZ"
                      listViewItem = New GZipArchiveListViewItem(file, parentNode)
                      itemCreated = True

                      ' Tar archive
                    Case ".TAR"
                      listViewItem = New TarArchiveListViewItem(file, parentNode)
                      itemCreated = True

                      ' Zip archive
                    Case ".ZIP"
                      listViewItem = New ZipArchiveListViewItem(file, parentNode)
                      itemCreated = True

                      ' Regular file
                    Case Else
                      listViewItem = New FileListViewItem(file)
                      itemCreated = True
                  End Select
                Catch decryptionPasswordExcept As InvalidDecryptionPasswordException
                  ' Make sure we are dealing with a ZippedFile.
                  If TypeOf item Is ZippedFile Then
                    ' We need a decryption password. Ask the user.
                    Dim passwordForm As InputPasswordForm = New InputPasswordForm
                    Try
                      If passwordForm.ShowDialog(Me, decryptionPasswordExcept.Item.FullName) = System.Windows.Forms.DialogResult.OK Then
                        ' Set the decryption password on the ZipArchive instance and retry the operation.
                        Dim zipArchive As zipArchive = CType(IIf(TypeOf item.RootFolder Is zipArchive, item.RootFolder, Nothing), zipArchive)
                        If Not zipArchive Is Nothing Then
                          zipArchive.DefaultDecryptionPassword = passwordForm.Password
                          exceptionAction = ItemExceptionAction.Retry
                        End If
                      Else
                        ' User cancelled, we just ignore this item and proceed to the next one.
                        exceptionAction = ItemExceptionAction.Ignore
                      End If
                    Finally
                      CType(passwordForm, IDisposable).Dispose()
                    End Try
                  End If
                End Try
              Loop
            End If

            ' Add the AbstractListViewItem item to the list.
            If itemCreated Then
              FileView.Items.Add(listViewItem)
            End If
          Next item
        Catch except As Exception
          System.Diagnostics.Debug.WriteLine(except.Message)
        End Try
      End If

      FileView.Sort(0)

      FileView.EndUpdate()

      System.Windows.Forms.Cursor.Current = Cursors.Default

      ' Update the toolbar state.
      Me.UpdateToolbarState()

      ' Refresh the status bar information
      Me.UpdateStatusBarInformation(parentNode)

      ' Start refreshing the icons.
      FileView.RefreshIcons()
    End Sub

    Private Sub FillFolderTreeNode(ByVal node As AbstractTreeViewNode)
      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

      FolderTree.BeginUpdate()

      ' Remove any child nodes.
      node.Nodes.Clear()

      ' Fill the node with child items.

      Try
        node.InitializeFolder()

        If Not node.Folder Is Nothing Then
          ' Get a list of items from the parent folder.
          Dim items As FileSystemItem() = node.Folder.GetItems(False)

          ' Set defaults settings
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles
          zipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders

          ' Loop through the items and create an AbstractListViewItem for each.
          For Each item As FileSystemItem In items
            Dim folderNode As AbstractTreeViewNode = Nothing
            Dim itemCreated As Boolean = False

            ' Folders are created as is. Nothing special to do.
            Dim folder As AbstractFolder = CType(IIf(TypeOf item Is AbstractFolder, item, Nothing), AbstractFolder)

            If Not folder Is Nothing Then
              folderNode = New FolderTreeViewNode(folder, FileView)
              itemCreated = True
            Else
              ' We are obviously a file. The only files that interests us are archives.
              ' A regular file is not represented in the FolderTree. Those files can also 
              ' be encrypted inside a Zip archive. We must handle those encrypted files and 
              'ask the user for a password if needed.
              Dim exceptionAction As ItemExceptionAction = ItemExceptionAction.Retry

              Do While (exceptionAction = ItemExceptionAction.Retry) AndAlso ((Not itemCreated))
                Try
                  Dim extension As String = Path.GetExtension(item.FullName)
                  Dim file As AbstractFile = CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile)

                  Select Case extension.ToUpper()
                    ' GZip archive
                  Case ".GZ", ".TGZ"
                      folderNode = New GZipArchiveTreeViewNode(file, FileView)
                      itemCreated = True


                      ' Tar archive
                    Case ".TAR"
                      folderNode = New TarArchiveTreeViewNode(file, FileView)
                      itemCreated = True


                      ' Zip archive
                    Case ".ZIP"
                      folderNode = New ZipArchiveTreeViewNode(file, FileView)
                      itemCreated = True


                      ' Regular file
                    Case Else
                      ' Skip the file
                      exceptionAction = ItemExceptionAction.Ignore
                  End Select
                Catch decryptionPasswordExcept As InvalidDecryptionPasswordException
                  ' Make sure we are dealing with a ZippedFile.
                  If TypeOf item Is ZippedFile Then
                    ' We need a decryption password. Ask the user.
                    Dim passwordForm As InputPasswordForm = New InputPasswordForm
                    Try
                      If passwordForm.ShowDialog(Me, decryptionPasswordExcept.Item.FullName) = System.Windows.Forms.DialogResult.OK Then
                        ' Set the decryption password on the ZipArchive instance and retry the operation.
                        Dim zipArchive As zipArchive = CType(IIf(TypeOf item.RootFolder Is zipArchive, item.RootFolder, Nothing), zipArchive)
                        If Not zipArchive Is Nothing Then
                          zipArchive.DefaultDecryptionPassword = passwordForm.Password
                          exceptionAction = ItemExceptionAction.Retry
                        End If
                      Else
                        ' User cancelled, we just ignore this item and proceed to the next one.
                        exceptionAction = ItemExceptionAction.Ignore
                      End If
                    Finally
                      CType(passwordForm, IDisposable).Dispose()
                    End Try
                  End If
                End Try
              Loop
            End If

            ' Add the AbstractTreeViewNode item to the list.
            If itemCreated Then
              node.Nodes.Add(folderNode)
            End If
          Next item
        End If
      Catch except As Exception
        System.Diagnostics.Debug.WriteLine(except.Message)
      Finally
        FolderTree.EndUpdate()

        System.Windows.Forms.Cursor.Current = Cursors.Default
      End Try

      node.RefreshIcons()
    End Sub

    Private Function FindMatchingNode(ByVal parentNode As AbstractTreeViewNode, ByVal item As FileSystemItem) As AbstractTreeViewNode
      Dim name As String = item.Name

      Dim folder As AbstractFolder = CType(IIf(TypeOf item Is AbstractFolder, item, Nothing), AbstractFolder)

      If (Not folder Is Nothing) AndAlso (folder.IsRoot) Then
        name = item.FullName
      End If

      Dim i As Integer = 0
      Do While i < parentNode.Nodes.Count
        If parentNode.Nodes(i).Text = name Then
          Return CType(IIf(TypeOf parentNode.Nodes(i) Is AbstractTreeViewNode, parentNode.Nodes(i), Nothing), AbstractTreeViewNode)
        End If
        i += 1
      Loop

      Return Nothing
    End Function

    Private Sub RenameItem()
      ' Rename the selected item.
      If FileView.SelectedItems.Count > 0 Then
        FileView.SelectedItems(0).BeginEdit()
      End If
    End Sub

    Private Sub ToggleFolderView()
      ' Show/hide the folder treeview.

      Dim visible As Boolean = FoldersTool.Checked

      FolderTree.Visible = visible
      splitter1.Visible = visible
    End Sub

    Private Sub UpdateStatusBarInformation(ByVal node As AbstractTreeViewNode)
      Try
        ' Objects
        Dim items As FileSystemItem() = node.Folder.GetItems(False)
        ObjectPanel.Text = items.Length & " object" & (IIf(items.Length > 0, "s", ""))

        ' Total size
        Dim files As AbstractFile() = node.Folder.GetFiles(False)

        Dim totalSize As Double = 0
        For Each file As AbstractFile In files
          totalSize += file.Size
        Next file

        Dim formattedSize As String = String.Empty

        If totalSize > 1073741824 Then ' GB
          totalSize = System.Math.Round(totalSize / 1073741824, 2)
          formattedSize = totalSize.ToString("#,##0.##") & " GB"
        ElseIf totalSize > 1048576 Then  ' MB
          totalSize = System.Math.Round(totalSize / 1048576, 2)
          formattedSize = totalSize.ToString("#,##0.##") & " MB"
        ElseIf totalSize > 1024 Then  ' KB
          totalSize = System.Math.Round(totalSize / 1024, 2)
          formattedSize = totalSize.ToString("#,##0.##") & " KB"
        Else ' bytes
          totalSize = System.Math.Round(totalSize, 0)
          formattedSize = totalSize.ToString("#,##0") & " bytes"
        End If

        SizePanel.Text = formattedSize

        ' Location
        Dim rootNode As AbstractTreeViewNode = node.RootNode
        LocationPanel.Text = rootNode.Text
        LocationPanel.ImageIndex = rootNode.ImageIndex
      Catch
      End Try
    End Sub

    Private Sub UpdateToolbarState()
      ' Up tool and new menu button
      UpTool.Enabled = False

      NewFileMenu.Enabled = False
      NewFolderMenu.Enabled = False
      NewZipArchiveMenu.Enabled = False
      NewTarArchiveMenu.Enabled = False
      NewGZipArchiveMenu.Enabled = False

      NewFtpConnectionMenu.Enabled = False

      Dim selectedNode As AbstractTreeViewNode = CType(FolderTree.SelectedNode, AbstractTreeViewNode)
      If Not selectedNode Is Nothing Then
        UpTool.Enabled = selectedNode.UpToolEnabled

        NewFileMenu.Enabled = selectedNode.NewToolEnabled
        NewFolderMenu.Enabled = selectedNode.NewToolEnabled
        NewZipArchiveMenu.Enabled = selectedNode.NewToolEnabled
        NewTarArchiveMenu.Enabled = selectedNode.NewToolEnabled
        NewGZipArchiveMenu.Enabled = selectedNode.NewToolEnabled

        NewFtpConnectionMenu.Enabled = selectedNode.NewFtpConnectionToolEnabled
      End If

      ' Copy, Cut, Paste tool and Delete button
      CopyTool.Enabled = False
      CopyMenu.Enabled = False

      CutTool.Enabled = False
      CutMenu.Enabled = False

      PasteTool.Enabled = False
      PasteMenu.Enabled = False

      DeleteTool.Enabled = False
      DeleteMenu.Enabled = False

      RenameTool.Enabled = False
      RenameMenu.Enabled = False

      PropertiesMenu.Enabled = False

      OpenMenu.Enabled = False

      If FileView.SelectedItems.Count > 0 Then
        Dim item As AbstractListViewItem = CType(IIf(TypeOf FileView.SelectedItems(0) Is AbstractListViewItem, FileView.SelectedItems(0), Nothing), AbstractListViewItem)

        CopyTool.Enabled = item.CopyToolEnabled
        CopyMenu.Enabled = CopyTool.Enabled

        CutTool.Enabled = item.CutToolEnabled
        CutMenu.Enabled = CutTool.Enabled

        DeleteTool.Enabled = item.DeleteToolEnabled
        DeleteMenu.Enabled = DeleteTool.Enabled

        RenameTool.Enabled = item.RenameToolEnabled
        RenameMenu.Enabled = RenameTool.Enabled

        PropertiesMenu.Enabled = True

        OpenMenu.Enabled = True
      End If

      If Not m_clipBoard Is Nothing AndAlso m_clipBoard.GetDataPresent(GetType(AbstractListViewItem())) AndAlso Not FolderTree.SelectedNode Is Nothing Then
        PasteTool.Enabled = (CType(FolderTree.SelectedNode, AbstractTreeViewNode)).PasteToolEnabled
        PasteMenu.Enabled = PasteTool.Enabled
      End If
    End Sub

    Private Sub ShowItemProperties()
      ' Show the FileSystemItem item's properties.

      Dim item As FileSystemItem = (CType(FileView.SelectedItems(0), AbstractListViewItem)).FileSystemItem

      Dim propertyForm As propertyForm = New propertyForm(item)
      Try
        If propertyForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
          CType(FileView.SelectedItems(0), AbstractListViewItem).Refresh(item)
        End If
      Finally
        CType(propertyForm, IDisposable).Dispose()
      End Try
    End Sub

#End Region     ' PRIVATE METHODS

    #Region "FILESYSTEM EVENTS"

    Private Sub Copy_ByteProgression(ByVal sender As Object, ByVal e As ByteProgressionEventArgs)
      ' Increment the copy progression bar.

      Dim progression As ProgressionForm = CType(IIf(TypeOf e.UserData Is ProgressionForm, e.UserData, Nothing), ProgressionForm)

      If progression Is Nothing Then
        Return
      End If

      If progression.UserCancelled Then
        Throw New ApplicationException("The user cancelled the operation.")
      End If

      If (e.AllFilesBytes.Percent = 0) Then
        m_previousProgressionPct = 0
      End If

      If (e.AllFilesBytes.Percent - m_previousProgressionPct) > 0 Then
        progression.CurrentProgressValue = e.CurrentFileBytes.Percent
        progression.TotalProgressValue = e.AllFilesBytes.Percent
        progression.FromText = e.CurrentItem.FullName
        progression.ToText = e.TargetItem.FullName

        m_previousProgressionPct = e.AllFilesBytes.Percent
      End If

      Application.DoEvents()
    End Sub

    Private Sub Copy_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs)
      ' Operation aborted.
      If TypeOf e.Exception Is System.Reflection.TargetInvocationException Then
        Return
      End If

      ' When overwriting a item that have the "system" attribute set, the system will throw an exception.
      ' Therefore, we will delete the target item before retrying the copy.
      If TypeOf e.Exception Is System.UnauthorizedAccessException Then
        If e.CurrentItem.HasAttributes AndAlso ((e.CurrentItem.Attributes And FileAttributes.System) = FileAttributes.System) Then
          e.TargetItem.Delete()
          e.Action = ItemExceptionAction.Retry
          Return
        End If
      End If

      ' ZipArchive can be password protected. We need to ask the user to input that password.
      If TypeOf e.Exception Is Xceed.Zip.InvalidDecryptionPasswordException Then
        Dim passwordForm As InputPasswordForm = New InputPasswordForm
        Try
          If passwordForm.ShowDialog(Me, e.CurrentItem.FullName) = System.Windows.Forms.DialogResult.OK Then
            If (TypeOf e.CurrentItem Is ZippedFile) OrElse (TypeOf e.CurrentItem Is ZippedFolder) Then
              Dim archive As ZipArchive = CType(IIf(TypeOf e.CurrentItem.RootFolder Is ZipArchive, e.CurrentItem.RootFolder, Nothing), ZipArchive)

              If Not archive Is Nothing Then
                archive.DefaultDecryptionPassword = passwordForm.Password
                e.Action = ItemExceptionAction.Retry
                Return
              End If

              ' Set the last decryption password used for future use.
              Options.ZipLastDecryptionPasswordUsed = passwordForm.Password
            End If
          End If
        Finally
          CType(passwordForm, IDisposable).Dispose()
        End Try
      End If

      Dim message As String = String.Format("An error occured while copying the item {0}.", e.CurrentItem.FullName)

      Dim result As DialogResult = MessageBox.Show(Me, message, "Error Copying Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)

      Select Case result
        Case System.Windows.Forms.DialogResult.Ignore
          e.Action = ItemExceptionAction.Ignore

        Case System.Windows.Forms.DialogResult.Retry
          e.Action = ItemExceptionAction.Retry

        Case System.Windows.Forms.DialogResult.Abort
          e.Action = ItemExceptionAction.Abort
      End Select
    End Sub

    Private Sub Move_ByteProgression(ByVal sender As Object, ByVal e As ByteProgressionEventArgs)
      ' Increment the copy progression bar.

      Dim progression As ProgressionForm = CType(IIf(TypeOf e.UserData Is ProgressionForm, e.UserData, Nothing), ProgressionForm)

      If progression Is Nothing Then
        Return
      End If

      If progression.UserCancelled Then
        Throw New ApplicationException("The user cancelled the operation.")
      End If

      If (e.AllFilesBytes.Percent = 0) Then
        m_previousProgressionPct = 0
      End If

      If (e.AllFilesBytes.Percent - m_previousProgressionPct) > 0 Then
        progression.CurrentProgressValue = e.CurrentFileBytes.Percent
        progression.TotalProgressValue = e.AllFilesBytes.Percent
        progression.FromText = e.CurrentItem.FullName
        progression.ToText = e.TargetItem.FullName

        m_previousProgressionPct = e.AllFilesBytes.Percent
      End If

      Application.DoEvents()
    End Sub

    Private Sub Move_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs)
      ' Operation aborted.
      If TypeOf e.Exception Is System.Reflection.TargetInvocationException Then
        Return
      End If

      ' When overwriting a item that have the "system" attribute set, the system will throw an exception.
      ' Therefore, we will delete the target item before retrying the copy.
      If TypeOf e.Exception Is System.UnauthorizedAccessException Then
        If e.CurrentItem.HasAttributes AndAlso ((e.CurrentItem.Attributes And FileAttributes.System) = FileAttributes.System) Then
          e.TargetItem.Delete()
          e.Action = ItemExceptionAction.Retry
          Return
        End If
      End If

      ' ZipArchive can be password protected. We need to ask the user to input that password.
      If TypeOf e.Exception Is Xceed.Zip.InvalidDecryptionPasswordException Then
        Dim passwordForm As InputPasswordForm = New InputPasswordForm
        Try
          If passwordForm.ShowDialog(Me, e.CurrentItem.FullName) = System.Windows.Forms.DialogResult.OK Then
            If (TypeOf e.CurrentItem Is ZippedFile) OrElse (TypeOf e.CurrentItem Is ZippedFolder) Then
              Dim archive As ZipArchive = CType(IIf(TypeOf e.CurrentItem.RootFolder Is ZipArchive, e.CurrentItem.RootFolder, Nothing), ZipArchive)

              If Not archive Is Nothing Then
                archive.DefaultDecryptionPassword = passwordForm.Password
                e.Action = ItemExceptionAction.Retry
                Return
              End If

              ' Set the last decryption password used for future use.
              Options.ZipLastDecryptionPasswordUsed = passwordForm.Password
            End If
          End If
        Finally
          CType(passwordForm, IDisposable).Dispose()
        End Try
      End If

      Dim message As String = String.Format("An error occured while moving the item {0}.", e.CurrentItem.FullName)

      Dim result As DialogResult = MessageBox.Show(Me, message, "Error Moving Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)

      Select Case result
        Case System.Windows.Forms.DialogResult.Ignore
          e.Action = ItemExceptionAction.Ignore

        Case System.Windows.Forms.DialogResult.Retry
          e.Action = ItemExceptionAction.Retry

        Case System.Windows.Forms.DialogResult.Abort
          e.Action = ItemExceptionAction.Abort
      End Select
    End Sub

#End Region     ' FILESYSTEM EVENTS

    #Region "FORM EVENTS"

    Private Sub MainForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
      ' Clear the icon cahce.
      IconCache.ClearCache()
    End Sub

    #End Region ' FORM EVENTS

    #Region "MENU EVENTS"

    Private Sub CopyMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles CopyMenu.Click
      ' Copy the selected items.
      Me.CopyItems()
    End Sub

    Private Sub CutMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles CutMenu.Click
      ' Cut the selected items.
      Me.CutItems()
    End Sub

    Private Sub DeleteMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles DeleteMenu.Click
      ' Delete the selected items.
      Me.DeleteItems()
    End Sub

    Private Sub PasteMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles PasteMenu.Click
      ' Paste the items from the clipboard.
      Me.PasteItems()
    End Sub

    Private Sub RenameMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles RenameMenu.Click
      ' Rename the selected item.
      Me.RenameItem()
    End Sub

    Private Sub OpenMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles OpenMenu.Click
      ' Opent the selected item.
      Me.OpenItem()
    End Sub

    Private Sub PropertiesMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles PropertiesMenu.Click
      ' Show the item's properties.
      Me.ShowItemProperties()
    End Sub

    Private Sub DetailsMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles DetailsToolbarMenu.Click, DetailsMenu.Click
      ' Update the view menus check state to "Details".
      IconsMenu.Checked = False
      IconsToolbarMenu.Checked = False

      ListMenu.Checked = False
      ListToolbarMenu.Checked = False

      DetailsMenu.Checked = True
      DetailsToolbarMenu.Checked = True

      ' Change the view state of the ListView to "Details".
      FileView.View = View.Details
    End Sub

    Private Sub IconsMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles IconsToolbarMenu.Click, IconsMenu.Click
      ' Update the view menus check state to "Icons".
      IconsMenu.Checked = True
      IconsToolbarMenu.Checked = True

      ListMenu.Checked = False
      ListToolbarMenu.Checked = False

      DetailsMenu.Checked = False
      DetailsToolbarMenu.Checked = False

      ' Change the view state of the ListView to "Icons".
      FileView.View = View.LargeIcon
    End Sub

    Private Sub ListMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles ListToolbarMenu.Click, ListMenu.Click
      ' Update the view menus check state to "List".
      IconsMenu.Checked = False
      IconsToolbarMenu.Checked = False

      ListMenu.Checked = True
      ListToolbarMenu.Checked = True

      DetailsMenu.Checked = False
      DetailsToolbarMenu.Checked = False

      ' Change the view state of the ListView to "List".
      FileView.View = View.List
    End Sub

    Private Sub NewFileMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewFileMenu.Click
      ' Create a new text file.
      Me.CreateFile()
    End Sub

    Private Sub NewFolderMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewFolderMenu.Click
      ' Create a new folder.
      Me.CreateFolder()
    End Sub

    Private Sub NewGZipArchiveMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewGZipArchiveMenu.Click
      ' Create a new GZip archive.
      Me.CreateGZipArchive()
    End Sub

    Private Sub NewTarArchiveMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewTarArchiveMenu.Click
      ' Create a new Tar archive.
      Me.CreateTarArchive()
    End Sub

    Private Sub NewZipArchiveMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewZipArchiveMenu.Click
      ' Create a new Zip archive.
      Me.CreateZipArchive()
    End Sub

    Private Sub NewFtpConnectionMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles NewFtpConnectionMenu.Click
      ' Create a new Ftp connection.
      Me.CreateFtpConnection()
    End Sub

    Private Sub OptionsMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles OptionsMenu.Click
      ' Show the application's options.
      Dim optionsForm As optionsForm = New optionsForm
      Try
        If optionsForm.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
          GZipArchive.AllowMultipleFiles = Options.GZipAllowMultipleFiles
          ZipArchive.DefaultExtraHeaders = Options.ZipDefaultExtraHeaders
        End If
      Finally
        CType(optionsForm, IDisposable).Dispose()
      End Try
    End Sub

    Private Sub RefreshMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles RefreshMenu.Click
      Dim selectedNode As AbstractTreeViewNode = CType(IIf(TypeOf FolderTree.SelectedNode Is AbstractTreeViewNode, FolderTree.SelectedNode, Nothing), AbstractTreeViewNode)

      If Not selectedNode Is Nothing Then
        ' Refresh the folder tree and the file list.
        Me.FillFolderTreeNode(selectedNode)
        Me.FillListView(selectedNode)
      End If
    End Sub

    Private Sub ExitMenu_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles ExitMenu.Click
      ' Quit.
      Application.Exit()
    End Sub

#End Region     ' MENU EVENTS

    #Region "CONTEXT MENU EVENTS"

    Private Sub CopyContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CopyContextMenu.Click
      ' Copy the selected items.
      Me.CopyItems()
    End Sub

    Private Sub CutContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CutContextMenu.Click
      ' Cut the selected items.
      Me.CutItems()
    End Sub

    Private Sub DeleteContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteContextMenu.Click
      ' Delete the selected items.
      Me.DeleteItems()
    End Sub

    Private Sub PasteContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasteContextMenu.Click
      ' Paste the items from the clipboard.
      Me.PasteItems()
    End Sub

    Private Sub RenameContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RenameContextMenu.Click
      ' Rename the selected item.
      Me.RenameItem()
    End Sub

    Private Sub OpenContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenContextMenu.Click
      ' Open the selected item.
      Me.OpenItem()
    End Sub

    Private Sub PropertiesContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PropertiesContextMenu.Click
      ' Show the item's properties.
      Me.ShowItemProperties()
    End Sub

    Private Sub NewFileContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewFileContextMenu.Click
      ' Create a new text file.
      Me.CreateFile()
    End Sub

    Private Sub NewFolderContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewFolderContextMenu.Click
      ' Create a new folder.
      Me.CreateFolder()
    End Sub

    Private Sub NewGZipArchiveContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewGZipArchiveContextMenu.Click
      ' Create a new GZip archive.
      Me.CreateGZipArchive()
    End Sub

    Private Sub NewTarArchiveContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewTarArchiveContextMenu.Click
      ' Create a new Tar archive.
      Me.CreateTarArchive()
    End Sub

    Private Sub NewZipArchiveContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewZipArchiveContextMenu.Click
      ' Create a new Zip archive.
      Me.CreateZipArchive()
    End Sub

    Private Sub NewFtpConnectionContextMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewFtpConnectionContextMenu.Click
      ' Create a new Ftp connection.
      Me.CreateFtpConnection()
    End Sub

    Private Sub EditContextMenu_Popup(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditContextMenu.Popup
      ' Enabled or disabled context menus depending on the selected item.

      CopyContextMenu.Enabled = False
      CutContextMenu.Enabled = False
      DeleteContextMenu.Enabled = False
      PasteContextMenu.Enabled = False
      RenameContextMenu.Enabled = False
      PropertiesContextMenu.Enabled = False
      OpenContextMenu.Enabled = False

      NewFileContextMenu.Enabled = False
      NewFolderContextMenu.Enabled = False
      NewZipArchiveContextMenu.Enabled = False
      NewTarArchiveContextMenu.Enabled = False
      NewGZipArchiveContextMenu.Enabled = False

      NewFtpConnectionContextMenu.Enabled = False

      Dim node As AbstractTreeViewNode = CType(IIf(TypeOf FolderTree.SelectedNode Is AbstractTreeViewNode, FolderTree.SelectedNode, Nothing), AbstractTreeViewNode)

      If (Not m_clipBoard Is Nothing) AndAlso m_clipBoard.GetDataPresent(GetType(AbstractListViewItem() )) Then
        PasteContextMenu.Enabled = True
      End If

      If FileView.SelectedItems.Count > 0 Then
        Dim item As AbstractListViewItem = CType(IIf(TypeOf FileView.SelectedItems(0) Is AbstractListViewItem, FileView.SelectedItems(0), Nothing), AbstractListViewItem)

        If Not item Is Nothing Then
          CopyContextMenu.Enabled = item.CopyToolEnabled
          CutContextMenu.Enabled = item.CutToolEnabled
          DeleteContextMenu.Enabled = item.DeleteToolEnabled
          RenameContextMenu.Enabled = item.RenameToolEnabled
          PropertiesContextMenu.Enabled = True
          OpenContextMenu.Enabled = True

          If (CType(IIf(TypeOf item.FileSystemItem Is AbstractFolder, item.FileSystemItem, Nothing), AbstractFolder)) Is Nothing Then
            PasteContextMenu.Enabled = False
          End If
        End If
      End If

      If PasteContextMenu.Enabled Then
        If Not node Is Nothing AndAlso (Not node.PasteToolEnabled) Then
          PasteContextMenu.Enabled = False
        End If
      End If

      If Not node Is Nothing Then
        NewFileContextMenu.Enabled = node.NewToolEnabled
        NewFolderContextMenu.Enabled = node.NewToolEnabled
        NewZipArchiveContextMenu.Enabled = node.NewToolEnabled
        NewTarArchiveContextMenu.Enabled = node.NewToolEnabled
        NewGZipArchiveContextMenu.Enabled = node.NewToolEnabled

        NewFtpConnectionContextMenu.Enabled = node.NewFtpConnectionToolEnabled
      End If
    End Sub

    #End Region ' CONTEXT MENU EVENTS

    #Region "TOOLBAR EVENTS"

    Private Sub CopyTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles CopyTool.Click
      ' Copy the selected items.
      Me.CopyItems()
    End Sub

    Private Sub CutTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles CutTool.Click
      ' Cut the selected items.
      Me.CutItems()
    End Sub

    Private Sub DeleteTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles DeleteTool.Click
      ' Delete the selected items.
      Me.DeleteItems()
    End Sub

    Private Sub PasteTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles PasteTool.Click
      ' Paste teh items from the clipboard.
      Me.PasteItems()
    End Sub

    Private Sub RenameTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles RenameTool.Click
      ' Rename the selected item.
      Me.RenameItem()
    End Sub

    Private Sub UpTool_Click(ByVal sender As Object, ByVal e As Xceed.SmartUI.SmartItemClickEventArgs) Handles UpTool.Click
      ' Go back to the parent node.

      Dim parentNode As TreeNode = FolderTree.SelectedNode.Parent

      parentNode.EnsureVisible()
      FolderTree.SelectedNode = parentNode
      parentNode.Expand()
    End Sub

    Private Sub FoldersTool_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FoldersTool.CheckedChanged
      ' Show/hide the FolderTree.
      Me.ToggleFolderView()
    End Sub

    #End Region ' TOOLBAR EVENTS

    #Region "LISTVIEW EVENTS"

    Private Sub FileView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FileView.KeyDown
      ' The Enter key will open the selected item unless we are editing an item.

      If e.KeyCode = Keys.Enter Then
        If (Not m_editingItem) Then
          Me.OpenItem()
        End If
      End If
    End Sub

    Private Sub FileView_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles FileView.AfterLabelEdit
      m_editingItem = False

      Dim currentItem As AbstractListViewItem = CType(IIf(TypeOf FileView.Items(e.Item) Is AbstractListViewItem, FileView.Items(e.Item), Nothing), AbstractListViewItem)

      If currentItem Is Nothing Then
        Return
      End If

      ' No changes were made.
      If Not e.Label Is Nothing Then
        Dim invalidPathChars As Char() = Path.GetInvalidPathChars()

        ' Validate the new name.
        If e.Label.IndexOfAny(invalidPathChars) > -1 Then
          Dim message As String = "The filename is invalid. It cannot contain any of the following characters: " & Constants.vbLf + Constants.vbLf

          Dim i As Integer = 0
          Do While i < invalidPathChars.Length
            If i > 0 Then
              message &= " "
            End If

            message &= invalidPathChars(i).ToString()
            i += 1
          Loop

          Dim caption As String = "Error Renamming File or Folder"

          MessageBox.Show(Me, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
          e.CancelEdit = True
          currentItem.BeginEdit()
          Return
        End If

        ' Check if the file already exists
        If Not e.Label Is Nothing Then
          For Each item As AbstractListViewItem In FileView.Items
            If item.Index <> e.Item AndAlso item.Text = e.Label Then
              Dim message As String = "Cannot rename " & currentItem.Text & ": A file with the name you specified already exists. Specify a different file name."
              Dim caption As String = "Error Renamming File or Folder"

              MessageBox.Show(Me, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
              e.CancelEdit = True
              currentItem.BeginEdit()
              Return
            End If
          Next item
        End If

        Try
          ' Rename the item.
          Dim originalFullName As String = currentItem.FileSystemItem.FullName
          currentItem.Rename(e.Label)

          ' Update the treeview.
          If Not currentItem.ParentNode Is Nothing Then
            For Each node As AbstractTreeViewNode In currentItem.ParentNode.Nodes
              If node.Item.FullName = originalFullName Then
                node.Refresh(currentItem.FileSystemItem)
                Exit For
              End If
            Next node
          End If
        Catch
          MessageBox.Show(Me, "The item could not be renamed. You might not have sufficient rights to do this operation.", "Error renaming item.", MessageBoxButtons.OK, MessageBoxIcon.Error)
          e.CancelEdit = True
        End Try
      End If

      If m_creatingItem Then
        ' Create the item physically.
        Try
          currentItem.Create()
        Catch
          MessageBox.Show(Me, "An error occured while creating the item. You might not have sufficient rights to do this operation.", "Creation error", MessageBoxButtons.OK, MessageBoxIcon.Error)
          FileView.Items(e.Item).Remove()
        Finally
          m_creatingItem = False
        End Try
      End If
    End Sub

    Private Sub FileView_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles FileView.BeforeLabelEdit
      m_editingItem = True

      ' Check if we can rename.
      Dim item As AbstractListViewItem = CType(IIf(TypeOf FileView.Items(e.Item) Is AbstractListViewItem, FileView.Items(e.Item), Nothing), AbstractListViewItem)

      If (Not item Is Nothing) AndAlso ((Not item.RenameToolEnabled)) Then
        e.CancelEdit = True
        m_editingItem = False
      End If
    End Sub

    Private Sub FileView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileView.DoubleClick
      ' Open the selected item.
      Me.OpenItem()
    End Sub

    Private Sub FileView_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileView.Enter
      Me.UpdateToolbarState()
    End Sub

    Private Sub FileView_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileView.Leave
      Me.UpdateToolbarState()
    End Sub

    Private Sub FileView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileView.SelectedIndexChanged
      Me.UpdateToolbarState()
    End Sub

    #End Region ' LISTVIEW EVENTS

    #Region "TREEVIEW EVENTS"

    Private Sub FolderTree_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles FolderTree.BeforeExpand
      If m_folderSelecting Then
        Return
      End If

      ' Fill the nodes with child nodes.
      Me.FillFolderTreeNode(CType(e.Node, AbstractTreeViewNode))

      ' We only need to refresh the file list if the expanded node is the currently selected node.
      If e.Node Is FolderTree.SelectedNode Then
        ' Fill the list with files and folders.
        Me.FillListView(CType(e.Node, AbstractTreeViewNode))
      End If
    End Sub

    Private Sub FolderTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterSelect
      m_folderSelecting = True

      ' Fill the nodes with child nodes.
      Me.FillFolderTreeNode(CType(e.Node, AbstractTreeViewNode))

      ' Fill the list with files and folders.
      Me.FillListView(CType(e.Node, AbstractTreeViewNode))

      ' Ensure the node is expanded unless we were collapsing it.
      If (Not m_folderTreeCollapsing) Then
        e.Node.Expand()
      End If

      m_folderSelecting = False
    End Sub

    Private Sub FolderTree_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles FolderTree.BeforeCollapse
      m_folderTreeCollapsing = True
    End Sub

    Private Sub FolderTree_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterCollapse
      m_folderTreeCollapsing = False
    End Sub

    Private Sub FolderTree_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.Enter
      Me.UpdateToolbarState()
    End Sub

    Private Sub FolderTree_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.Leave
      Me.UpdateToolbarState()
    End Sub

#End Region     ' TREEVIEW EVENTS

#Region "FTP SSL EVENTS"

    Private Sub OnCertificateReceived(ByVal sender As Object, ByVal e As CertificateReceivedEventArgs)
      Dim message As String = String.Empty

      If e.Status = VerificationStatus.ValidCertificate Then
        message = "A valid certificate was received from the server." & Constants.vbLf + Constants.vbLf
      Else
        message = "An invalid certificate was received from the server." & Constants.vbLf & "The error is: " & e.Status.ToString() & Constants.vbLf + Constants.vbLf
      End If

      message &= e.ServerCertificate.ToString() & Constants.vbLf + Constants.vbLf & "Do you want to accept this certificate?"

      Dim answer As DialogResult = MessageBox.Show(Me, message, "Server Certificate", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

      e.Action = IIf((answer = System.Windows.Forms.DialogResult.Yes), VerificationAction.Accept, VerificationAction.Reject)
    End Sub

    Private Sub FtpConnection_CertificateReceived(ByVal sender As Object, ByVal e As CertificateReceivedEventArgs)
      ' This event is usually raised from one of the I/O threads, not the main 
      ' thread. We must make sure to perform any GUI operations on the main thread.
      ' BUT WATCH OUT: For this to work, the main thread MUST be pumping messages.
      ' That's why we also handle the WaitingForAsyncOperation event below, and
      ' pump messages there.
      If Me.InvokeRequired Then
        Me.Invoke(New CertificateReceivedEventHandler(AddressOf Me.OnCertificateReceived), New Object() {sender, e})
      Else
        Me.OnCertificateReceived(sender, e)
      End If
    End Sub

#End Region  ' FTP SSL EVENTS

#Region "IDisposable OVERRIDES"

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

#End Region     ' IDisposable OVERRIDES

#Region "PRIVATE FIELDS"

    ' Clipboard
    Private m_clipBoard As DataObject ' = null
    Private m_clipboardCutItems As Boolean ' = false

    ' Flags
    Private m_folderTreeCollapsing As Boolean ' = false
    Private m_folderSelecting As Boolean ' = false
    Private m_creatingItem As Boolean ' = false
    Private m_editingItem As Boolean
    Private separatorMenuItem9 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private WithEvents RefreshMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem ' = false

    ' Progression
    Private m_previousProgressionPct As Integer ' = 0;

#End Region     ' PRIVATE FIELDS

#Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainForm))
      Me.FolderTree = New System.Windows.Forms.TreeView
      Me.EditContextMenu = New System.Windows.Forms.ContextMenu
      Me.OpenContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem6 = New System.Windows.Forms.MenuItem
      Me.NewContextMenu = New System.Windows.Forms.MenuItem
      Me.NewFileContextMenu = New System.Windows.Forms.MenuItem
      Me.NewFolderContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem3 = New System.Windows.Forms.MenuItem
      Me.NewGZipArchiveContextMenu = New System.Windows.Forms.MenuItem
      Me.NewTarArchiveContextMenu = New System.Windows.Forms.MenuItem
      Me.NewZipArchiveContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem5 = New System.Windows.Forms.MenuItem
      Me.NewFtpConnectionContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem2 = New System.Windows.Forms.MenuItem
      Me.CopyContextMenu = New System.Windows.Forms.MenuItem
      Me.CutContextMenu = New System.Windows.Forms.MenuItem
      Me.PasteContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem1 = New System.Windows.Forms.MenuItem
      Me.DeleteContextMenu = New System.Windows.Forms.MenuItem
      Me.RenameContextMenu = New System.Windows.Forms.MenuItem
      Me.menuItem4 = New System.Windows.Forms.MenuItem
      Me.PropertiesContextMenu = New System.Windows.Forms.MenuItem
      Me.splitter1 = New System.Windows.Forms.Splitter
      Me.FileView = New Xceed.FileSystem.Samples.Utils.ListView.CustomListView
      Me.NameColumn = New System.Windows.Forms.ColumnHeader
      Me.SizeColumn = New System.Windows.Forms.ColumnHeader
      Me.CompressedSize = New System.Windows.Forms.ColumnHeader
      Me.TypeColumn = New System.Windows.Forms.ColumnHeader
      Me.DateModifiedColumn = New System.Windows.Forms.ColumnHeader
      Me.MainStatusBar = New Xceed.SmartUI.Controls.StatusBar.SmartStatusBar(Me.components)
      Me.ObjectPanel = New Xceed.SmartUI.Controls.StatusBar.SpringPanel
      Me.SizePanel = New Xceed.SmartUI.Controls.StatusBar.Panel
      Me.LocationPanel = New Xceed.SmartUI.Controls.StatusBar.Panel
      Me.GoTool = New Xceed.SmartUI.Controls.ToolBar.Tool("Go")
      Me.MainToolbar = New Xceed.SmartUI.Controls.ToolBar.SmartToolBar(Me.components)
      Me.UpTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.separatorTool1 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.CopyTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.CutTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.PasteTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.separatorTool5 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.DeleteTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.RenameTool = New Xceed.SmartUI.Controls.ToolBar.Tool
      Me.separatorTool3 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.FoldersTool = New Xceed.SmartUI.Controls.ToolBar.CheckTool("Folders")
      Me.separatorTool2 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.ViewsTool = New Xceed.SmartUI.Controls.ToolBar.MenuTool
      Me.IconsToolbarMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Icons")
      Me.ListToolbarMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("List")
      Me.DetailsToolbarMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Details")
      Me.BackTool = New Xceed.SmartUI.Controls.ToolBar.MixedTool("Back")
      Me.NextTool = New Xceed.SmartUI.Controls.ToolBar.MixedTool("Forward")
      Me.separatorTool4 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.MainMenuBar = New Xceed.SmartUI.Controls.MenuBar.SmartMenuBar(Me.components)
      Me.FileMenu = New Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&File")
      Me.NewMenu = New Xceed.SmartUI.Controls.MenuBar.PopupMenuItem("New")
      Me.NewFileMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Text file")
      Me.NewFolderMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Folder")
      Me.separatorMenuItem4 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.NewGZipArchiveMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("GZip archive")
      Me.NewTarArchiveMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Tar archive")
      Me.NewZipArchiveMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Zip archive")
      Me.separatorMenuItem6 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.NewFtpConnectionMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Ftp connection...")
      Me.separatorMenuItem3 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.ExitMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("E&xit")
      Me.EditMenu = New Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&Edit")
      Me.OpenMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Open")
      Me.separatorMenuItem8 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.CopyMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("&Copy")
      Me.CutMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Cu&t")
      Me.PasteMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("&Paste")
      Me.separatorMenuItem1 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.DeleteMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("&Delete")
      Me.RenameMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("&Rename")
      Me.separatorMenuItem5 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.PropertiesMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Properties...")
      Me.ViewMenu = New Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&View")
      Me.IconsMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Icons")
      Me.ListMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("List")
      Me.DetailsMenu = New Xceed.SmartUI.Controls.MenuBar.CheckMenuItem("Details")
      Me.ToolsMenu = New Xceed.SmartUI.Controls.MenuBar.MainMenuItem("&Tools")
      Me.OptionsMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("&Options...")
      Me.separatorMenuItem2 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.comboBoxItem1 = New Xceed.SmartUI.Controls.ToolBar.ComboBoxItem("comboBoxItem1")
      Me.node1 = New Xceed.SmartUI.Controls.TreeView.Node("node1")
      Me.mixedTool1 = New Xceed.SmartUI.Controls.ToolBar.MixedTool("mixedTool1")
      Me.NewTool = New Xceed.SmartUI.Controls.ToolBar.MenuTool("New")
      Me.separatorTool6 = New Xceed.SmartUI.Controls.ToolBar.SeparatorTool
      Me.separatorMenuItem7 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.separatorMenuItem9 = New Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
      Me.RefreshMenu = New Xceed.SmartUI.Controls.MenuBar.MenuItem("Refresh")
      CType(Me.MainStatusBar, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.MainToolbar, System.ComponentModel.ISupportInitialize).BeginInit()
      CType(Me.MainMenuBar, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      ' 
      ' FolderTree
      ' 
      Me.FolderTree.Dock = System.Windows.Forms.DockStyle.Left
      Me.FolderTree.HideSelection = False
      Me.FolderTree.HotTracking = True
      Me.FolderTree.ImageIndex = -1
      Me.FolderTree.Location = New System.Drawing.Point(0, 58)
      Me.FolderTree.Name = "FolderTree"
      Me.FolderTree.SelectedImageIndex = -1
      Me.FolderTree.ShowLines = False
      Me.FolderTree.Size = New System.Drawing.Size(248, 390)
      Me.FolderTree.TabIndex = 2
      '      Me.FolderTree.AfterCollapse += New System.Windows.Forms.TreeViewEventHandler(Me.FolderTree_AfterCollapse);
      '      Me.FolderTree.AfterSelect += New System.Windows.Forms.TreeViewEventHandler(Me.FolderTree_AfterSelect);
      '      Me.FolderTree.Leave += New System.EventHandler(Me.FolderTree_Leave);
      '      Me.FolderTree.BeforeCollapse += New System.Windows.Forms.TreeViewCancelEventHandler(Me.FolderTree_BeforeCollapse);
      '      Me.FolderTree.BeforeExpand += New System.Windows.Forms.TreeViewCancelEventHandler(Me.FolderTree_BeforeExpand);
      '      Me.FolderTree.Enter += New System.EventHandler(Me.FolderTree_Enter);
      ' 
      ' EditContextMenu
      ' 
      Me.EditContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.OpenContextMenu, Me.menuItem6, Me.NewContextMenu, Me.menuItem2, Me.CopyContextMenu, Me.CutContextMenu, Me.PasteContextMenu, Me.menuItem1, Me.DeleteContextMenu, Me.RenameContextMenu, Me.menuItem4, Me.PropertiesContextMenu})
      '      Me.EditContextMenu.Popup += New System.EventHandler(Me.EditContextMenu_Popup);
      ' 
      ' OpenContextMenu
      ' 
      Me.OpenContextMenu.DefaultItem = True
      Me.OpenContextMenu.Index = 0
      Me.OpenContextMenu.Text = "Open"
      '      Me.OpenContextMenu.Click += New System.EventHandler(Me.OpenContextMenu_Click);
      ' 
      ' menuItem6
      ' 
      Me.menuItem6.Index = 1
      Me.menuItem6.Text = "-"
      ' 
      ' NewContextMenu
      ' 
      Me.NewContextMenu.Index = 2
      Me.NewContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.NewFileContextMenu, Me.NewFolderContextMenu, Me.menuItem3, Me.NewGZipArchiveContextMenu, Me.NewTarArchiveContextMenu, Me.NewZipArchiveContextMenu, Me.menuItem5, Me.NewFtpConnectionContextMenu})
      Me.NewContextMenu.Text = "New"
      ' 
      ' NewFileContextMenu
      ' 
      Me.NewFileContextMenu.Index = 0
      Me.NewFileContextMenu.Text = "Text file"
      '      Me.NewFileContextMenu.Click += New System.EventHandler(Me.NewFileContextMenu_Click);
      ' 
      ' NewFolderContextMenu
      ' 
      Me.NewFolderContextMenu.Index = 1
      Me.NewFolderContextMenu.Text = "Folder"
      '      Me.NewFolderContextMenu.Click += New System.EventHandler(Me.NewFolderContextMenu_Click);
      ' 
      ' menuItem3
      ' 
      Me.menuItem3.Index = 2
      Me.menuItem3.Text = "-"
      ' 
      ' NewGZipArchiveContextMenu
      ' 
      Me.NewGZipArchiveContextMenu.Index = 3
      Me.NewGZipArchiveContextMenu.Text = "GZip archive"
      '      Me.NewGZipArchiveContextMenu.Click += New System.EventHandler(Me.NewGZipArchiveContextMenu_Click);
      ' 
      ' NewTarArchiveContextMenu
      ' 
      Me.NewTarArchiveContextMenu.Index = 4
      Me.NewTarArchiveContextMenu.Text = "Tar archive"
      '      Me.NewTarArchiveContextMenu.Click += New System.EventHandler(Me.NewTarArchiveContextMenu_Click);
      ' 
      ' NewZipArchiveContextMenu
      ' 
      Me.NewZipArchiveContextMenu.Index = 5
      Me.NewZipArchiveContextMenu.Text = "Zip archive"
      '      Me.NewZipArchiveContextMenu.Click += New System.EventHandler(Me.NewZipArchiveContextMenu_Click);
      ' 
      ' menuItem5
      ' 
      Me.menuItem5.Index = 6
      Me.menuItem5.Text = "-"
      ' 
      ' NewFtpConnectionContextMenu
      ' 
      Me.NewFtpConnectionContextMenu.Index = 7
      Me.NewFtpConnectionContextMenu.Text = "Ftp connection..."
      '      Me.NewFtpConnectionContextMenu.Click += New System.EventHandler(Me.NewFtpConnectionContextMenu_Click);
      ' 
      ' menuItem2
      ' 
      Me.menuItem2.Index = 3
      Me.menuItem2.Text = "-"
      ' 
      ' CopyContextMenu
      ' 
      Me.CopyContextMenu.Index = 4
      Me.CopyContextMenu.Text = "Copy"
      '      Me.CopyContextMenu.Click += New System.EventHandler(Me.CopyContextMenu_Click);
      ' 
      ' CutContextMenu
      ' 
      Me.CutContextMenu.Index = 5
      Me.CutContextMenu.Text = "Cut"
      '      Me.CutContextMenu.Click += New System.EventHandler(Me.CutContextMenu_Click);
      ' 
      ' PasteContextMenu
      ' 
      Me.PasteContextMenu.Index = 6
      Me.PasteContextMenu.Text = "Paste"
      '      Me.PasteContextMenu.Click += New System.EventHandler(Me.PasteContextMenu_Click);
      ' 
      ' menuItem1
      ' 
      Me.menuItem1.Index = 7
      Me.menuItem1.Text = "-"
      ' 
      ' DeleteContextMenu
      ' 
      Me.DeleteContextMenu.Index = 8
      Me.DeleteContextMenu.Text = "Delete"
      '      Me.DeleteContextMenu.Click += New System.EventHandler(Me.DeleteContextMenu_Click);
      ' 
      ' RenameContextMenu
      ' 
      Me.RenameContextMenu.Index = 9
      Me.RenameContextMenu.Text = "Rename"
      '      Me.RenameContextMenu.Click += New System.EventHandler(Me.RenameContextMenu_Click);
      ' 
      ' menuItem4
      ' 
      Me.menuItem4.Index = 10
      Me.menuItem4.Text = "-"
      ' 
      ' PropertiesContextMenu
      ' 
      Me.PropertiesContextMenu.Index = 11
      Me.PropertiesContextMenu.Text = "Properties..."
      '      Me.PropertiesContextMenu.Click += New System.EventHandler(Me.PropertiesContextMenu_Click);
      ' 
      ' splitter1
      ' 
      Me.splitter1.Location = New System.Drawing.Point(248, 58)
      Me.splitter1.Name = "splitter1"
      Me.splitter1.Size = New System.Drawing.Size(3, 390)
      Me.splitter1.TabIndex = 3
      Me.splitter1.TabStop = False
      ' 
      ' FileView
      ' 
      Me.FileView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumn, Me.SizeColumn, Me.CompressedSize, Me.TypeColumn, Me.DateModifiedColumn})
      Me.FileView.ContextMenu = Me.EditContextMenu
      Me.FileView.Dock = System.Windows.Forms.DockStyle.Fill
      Me.FileView.HideSelection = False
      Me.FileView.LabelEdit = True
      Me.FileView.Location = New System.Drawing.Point(251, 58)
      Me.FileView.Name = "FileView"
      Me.FileView.Size = New System.Drawing.Size(491, 390)
      Me.FileView.TabIndex = 4
      Me.FileView.View = System.Windows.Forms.View.Details
      '      Me.FileView.BeforeLabelEdit += New System.Windows.Forms.LabelEditEventHandler(Me.FileView_BeforeLabelEdit);
      '      Me.FileView.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.FileView_KeyDown);
      '      Me.FileView.DoubleClick += New System.EventHandler(Me.FileView_DoubleClick);
      '      Me.FileView.AfterLabelEdit += New System.Windows.Forms.LabelEditEventHandler(Me.FileView_AfterLabelEdit);
      '      Me.FileView.SelectedIndexChanged += New System.EventHandler(Me.FileView_SelectedIndexChanged);
      '      Me.FileView.Leave += New System.EventHandler(Me.FileView_Leave);
      '      Me.FileView.Enter += New System.EventHandler(Me.FileView_Enter);
      ' 
      ' NameColumn
      ' 
      Me.NameColumn.Text = "Name"
      Me.NameColumn.Width = 209
      ' 
      ' SizeColumn
      ' 
      Me.SizeColumn.Text = "Size"
      Me.SizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      Me.SizeColumn.Width = 85
      ' 
      ' CompressedSize
      ' 
      Me.CompressedSize.Text = "Compressed size"
      Me.CompressedSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      Me.CompressedSize.Width = 100
      ' 
      ' TypeColumn
      ' 
      Me.TypeColumn.Text = "Type"
      Me.TypeColumn.Width = 157
      ' 
      ' DateModifiedColumn
      ' 
      Me.DateModifiedColumn.Text = "Date Modified"
      Me.DateModifiedColumn.Width = 132
      ' 
      ' MainStatusBar
      ' 
      Me.MainStatusBar.Items.AddRange(New Object() {Me.ObjectPanel, Me.SizePanel, Me.LocationPanel})
      Me.MainStatusBar.Location = New System.Drawing.Point(0, 448)
      Me.MainStatusBar.Name = "MainStatusBar"
      Me.MainStatusBar.Size = New System.Drawing.Size(742, 23)
      Me.MainStatusBar.TabIndex = 5
      Me.MainStatusBar.Text = "MainStatusBar"
      ' 
      ' GoTool
      ' 
      Me.GoTool.Image = (CType(resources.GetObject("GoTool.Image"), System.Drawing.Image))
      Me.GoTool.Text = "Go"
      ' 
      ' MainToolbar
      ' 
      Me.MainToolbar.Items.AddRange(New Object() {Me.UpTool, Me.separatorTool1, Me.CopyTool, Me.CutTool, Me.PasteTool, Me.separatorTool5, Me.DeleteTool, Me.RenameTool, Me.separatorTool3, Me.FoldersTool, Me.separatorTool2, Me.ViewsTool})
      Me.MainToolbar.Location = New System.Drawing.Point(0, 24)
      Me.MainToolbar.Name = "MainToolbar"
      Me.MainToolbar.Size = New System.Drawing.Size(742, 34)
      Me.MainToolbar.TabIndex = 7
      ' 
      ' UpTool
      ' 
      Me.UpTool.Enabled = False
      Me.UpTool.Image = (CType(resources.GetObject("UpTool.Image"), System.Drawing.Image))
      Me.UpTool.ToolTipText = "Up"
      '      Me.UpTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.UpTool_Click);
      ' 
      ' CopyTool
      ' 
      Me.CopyTool.Enabled = False
      Me.CopyTool.Image = (CType(resources.GetObject("CopyTool.Image"), System.Drawing.Image))
      Me.CopyTool.ToolTipText = "Copy"
      '      Me.CopyTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.CopyTool_Click);
      ' 
      ' CutTool
      ' 
      Me.CutTool.Enabled = False
      Me.CutTool.Image = (CType(resources.GetObject("CutTool.Image"), System.Drawing.Image))
      Me.CutTool.ToolTipText = "Cut"
      '      Me.CutTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.CutTool_Click);
      ' 
      ' PasteTool
      ' 
      Me.PasteTool.Enabled = False
      Me.PasteTool.Image = (CType(resources.GetObject("PasteTool.Image"), System.Drawing.Image))
      Me.PasteTool.ToolTipText = "Paste"
      '      Me.PasteTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.PasteTool_Click);
      ' 
      ' DeleteTool
      ' 
      Me.DeleteTool.Enabled = False
      Me.DeleteTool.Image = (CType(resources.GetObject("DeleteTool.Image"), System.Drawing.Image))
      Me.DeleteTool.ToolTipText = "Delete"
      '      Me.DeleteTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.DeleteTool_Click);
      ' 
      ' RenameTool
      ' 
      Me.RenameTool.Enabled = False
      Me.RenameTool.Image = (CType(resources.GetObject("RenameTool.Image"), System.Drawing.Image))
      Me.RenameTool.ToolTipText = "Rename"
      '      Me.RenameTool.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.RenameTool_Click);
      ' 
      ' FoldersTool
      ' 
      Me.FoldersTool.Checked = True
      Me.FoldersTool.Image = (CType(resources.GetObject("FoldersTool.Image"), System.Drawing.Image))
      Me.FoldersTool.Text = "Folders"
      '      Me.FoldersTool.CheckedChanged += New System.EventHandler(Me.FoldersTool_CheckedChanged);
      ' 
      ' ViewsTool
      ' 
      Me.ViewsTool.Image = (CType(resources.GetObject("ViewsTool.Image"), System.Drawing.Image))
      Me.ViewsTool.Items.AddRange(New Object() {Me.IconsToolbarMenu, Me.ListToolbarMenu, Me.DetailsToolbarMenu})
      Me.ViewsTool.ToolTipText = "Views"
      ' 
      ' IconsToolbarMenu
      ' 
      Me.IconsToolbarMenu.Text = "Icons"
      '      Me.IconsToolbarMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.IconsMenu_Click);
      ' 
      ' ListToolbarMenu
      ' 
      Me.ListToolbarMenu.Text = "List"
      '      Me.ListToolbarMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.ListMenu_Click);
      ' 
      ' DetailsToolbarMenu
      ' 
      Me.DetailsToolbarMenu.Checked = True
      Me.DetailsToolbarMenu.Text = "Details"
      '      Me.DetailsToolbarMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.DetailsMenu_Click);
      ' 
      ' BackTool
      ' 
      Me.BackTool.Enabled = False
      Me.BackTool.Image = (CType(resources.GetObject("BackTool.Image"), System.Drawing.Image))
      Me.BackTool.Text = "Back"
      Me.BackTool.Visible = False
      ' 
      ' NextTool
      ' 
      Me.NextTool.Enabled = False
      Me.NextTool.Image = (CType(resources.GetObject("NextTool.Image"), System.Drawing.Image))
      Me.NextTool.Text = "Forward"
      Me.NextTool.Visible = False
      ' 
      ' MainMenuBar
      ' 
      Me.MainMenuBar.Items.AddRange(New Object() {Me.FileMenu, Me.EditMenu, Me.ViewMenu, Me.ToolsMenu})
      Me.MainMenuBar.Location = New System.Drawing.Point(0, 0)
      Me.MainMenuBar.Name = "MainMenuBar"
      Me.MainMenuBar.Size = New System.Drawing.Size(742, 24)
      Me.MainMenuBar.TabIndex = 8
      Me.MainMenuBar.Text = "MainMenuBar"
      Me.MainMenuBar.UIStyle = Xceed.SmartUI.UIStyle.UIStyle.WindowsXP
      ' 
      ' FileMenu
      ' 
      Me.FileMenu.Items.AddRange(New Object() {Me.NewMenu, Me.separatorMenuItem3, Me.ExitMenu})
      Me.FileMenu.Text = "&File"
      ' 
      ' NewMenu
      ' 
      Me.NewMenu.Items.AddRange(New Object() {Me.NewFileMenu, Me.NewFolderMenu, Me.separatorMenuItem4, Me.NewGZipArchiveMenu, Me.NewTarArchiveMenu, Me.NewZipArchiveMenu, Me.separatorMenuItem6, Me.NewFtpConnectionMenu})
      Me.NewMenu.Text = "New"
      ' 
      ' NewFileMenu
      ' 
      Me.NewFileMenu.Text = "Text file"
      '      Me.NewFileMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewFileMenu_Click);
      ' 
      ' NewFolderMenu
      ' 
      Me.NewFolderMenu.Text = "Folder"
      '      Me.NewFolderMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewFolderMenu_Click);
      ' 
      ' NewGZipArchiveMenu
      ' 
      Me.NewGZipArchiveMenu.Text = "GZip archive"
      '      Me.NewGZipArchiveMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewGZipArchiveMenu_Click);
      ' 
      ' NewTarArchiveMenu
      ' 
      Me.NewTarArchiveMenu.Text = "Tar archive"
      '      Me.NewTarArchiveMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewTarArchiveMenu_Click);
      ' 
      ' NewZipArchiveMenu
      ' 
      Me.NewZipArchiveMenu.Text = "Zip archive"
      '      Me.NewZipArchiveMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewZipArchiveMenu_Click);
      ' 
      ' NewFtpConnectionMenu
      ' 
      Me.NewFtpConnectionMenu.Text = "Ftp connection..."
      '      Me.NewFtpConnectionMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.NewFtpConnectionMenu_Click);
      ' 
      ' ExitMenu
      ' 
      Me.ExitMenu.Text = "E&xit"
      '      Me.ExitMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.ExitMenu_Click);
      ' 
      ' EditMenu
      ' 
      Me.EditMenu.Items.AddRange(New Object() {Me.OpenMenu, Me.separatorMenuItem8, Me.CopyMenu, Me.CutMenu, Me.PasteMenu, Me.separatorMenuItem1, Me.DeleteMenu, Me.RenameMenu, Me.separatorMenuItem5, Me.PropertiesMenu})
      Me.EditMenu.Text = "&Edit"
      ' 
      ' OpenMenu
      ' 
      Me.OpenMenu.ShortcutText = "Enter"
      Me.OpenMenu.Text = "Open"
      '      Me.OpenMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.OpenMenu_Click);
      ' 
      ' CopyMenu
      ' 
      Me.CopyMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlC
      Me.CopyMenu.Text = "&Copy"
      '      Me.CopyMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.CopyMenu_Click);
      ' 
      ' CutMenu
      ' 
      Me.CutMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlX
      Me.CutMenu.Text = "Cu&t"
      '      Me.CutMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.CutMenu_Click);
      ' 
      ' PasteMenu
      ' 
      Me.PasteMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlV
      Me.PasteMenu.Text = "&Paste"
      '      Me.PasteMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.PasteMenu_Click);
      ' 
      ' DeleteMenu
      ' 
      Me.DeleteMenu.Shortcut = System.Windows.Forms.Shortcut.Del
      Me.DeleteMenu.Text = "&Delete"
      '      Me.DeleteMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.DeleteMenu_Click);
      ' 
      ' RenameMenu
      ' 
      Me.RenameMenu.Shortcut = System.Windows.Forms.Shortcut.F2
      Me.RenameMenu.Text = "&Rename"
      '      Me.RenameMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.RenameMenu_Click);
      ' 
      ' PropertiesMenu
      ' 
      Me.PropertiesMenu.Text = "Properties..."
      '      Me.PropertiesMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.PropertiesMenu_Click);
      ' 
      ' ViewMenu
      ' 
      Me.ViewMenu.Items.AddRange(New Object() {Me.IconsMenu, Me.ListMenu, Me.DetailsMenu, Me.separatorMenuItem9, Me.RefreshMenu})
      Me.ViewMenu.Text = "&View"
      ' 
      ' IconsMenu
      ' 
      Me.IconsMenu.Text = "Icons"
      '      Me.IconsMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.IconsMenu_Click);
      ' 
      ' ListMenu
      ' 
      Me.ListMenu.Text = "List"
      '      Me.ListMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.ListMenu_Click);
      ' 
      ' DetailsMenu
      ' 
      Me.DetailsMenu.Checked = True
      Me.DetailsMenu.Text = "Details"
      '      Me.DetailsMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.DetailsMenu_Click);
      ' 
      ' ToolsMenu
      ' 
      Me.ToolsMenu.Items.AddRange(New Object() {Me.OptionsMenu})
      Me.ToolsMenu.Text = "&Tools"
      ' 
      ' OptionsMenu
      ' 
      Me.OptionsMenu.Text = "&Options..."
      '      Me.OptionsMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.OptionsMenu_Click);
      ' 
      ' comboBoxItem1
      ' 
      Me.comboBoxItem1.Text = "comboBoxItem1"
      ' 
      ' node1
      ' 
      Me.node1.Text = "node1"
      ' 
      ' mixedTool1
      ' 
      Me.mixedTool1.Text = "mixedTool1"
      ' 
      ' NewTool
      ' 
      Me.NewTool.Text = "New"
      ' 
      ' RefreshMenu
      ' 
      Me.RefreshMenu.Shortcut = System.Windows.Forms.Shortcut.F5
      Me.RefreshMenu.Text = "Refresh"
      '      Me.RefreshMenu.Click += New Xceed.SmartUI.SmartItemClickEventHandler(Me.RefreshMenu_Click);
      ' 
      ' MainForm
      ' 
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(742, 471)
      Me.Controls.Add(Me.FileView)
      Me.Controls.Add(Me.splitter1)
      Me.Controls.Add(Me.FolderTree)
      Me.Controls.Add(Me.MainStatusBar)
      Me.Controls.Add(Me.MainToolbar)
      Me.Controls.Add(Me.MainMenuBar)
      Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
      Me.Name = "MainForm"
      Me.Text = "Xceed File System Explorer for .NET"
      '      Me.Closing += New System.ComponentModel.CancelEventHandler(Me.MainForm_Closing);
      CType(Me.MainStatusBar, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.MainToolbar, System.ComponentModel.ISupportInitialize).EndInit()
      CType(Me.MainMenuBar, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)

    End Sub
#End Region

#Region "Windows Form Designer generated fields"

    Private components As System.ComponentModel.IContainer
    Private WithEvents FolderTree As System.Windows.Forms.TreeView
    Private splitter1 As System.Windows.Forms.Splitter
    Private TypeColumn As System.Windows.Forms.ColumnHeader
    Private SizeColumn As System.Windows.Forms.ColumnHeader
    Private DateModifiedColumn As System.Windows.Forms.ColumnHeader
    Private NameColumn As System.Windows.Forms.ColumnHeader
    Private MainStatusBar As Xceed.SmartUI.Controls.StatusBar.SmartStatusBar
    Private ObjectPanel As Xceed.SmartUI.Controls.StatusBar.SpringPanel
    Private SizePanel As Xceed.SmartUI.Controls.StatusBar.Panel
    Private LocationPanel As Xceed.SmartUI.Controls.StatusBar.Panel
    Private GoTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private separatorTool1 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private separatorTool2 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private BackTool As Xceed.SmartUI.Controls.ToolBar.MixedTool
    Private FileMenu As Xceed.SmartUI.Controls.MenuBar.MainMenuItem
    Private NextTool As Xceed.SmartUI.Controls.ToolBar.MixedTool
    Private WithEvents ExitMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private MainToolbar As Xceed.SmartUI.Controls.ToolBar.SmartToolBar
    Private ViewsTool As Xceed.SmartUI.Controls.ToolBar.MenuTool
    Private WithEvents UpTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private MainMenuBar As Xceed.SmartUI.Controls.MenuBar.SmartMenuBar
    Private WithEvents FoldersTool As Xceed.SmartUI.Controls.ToolBar.CheckTool
    Private ViewMenu As Xceed.SmartUI.Controls.MenuBar.MainMenuItem
    Private WithEvents IconsMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private WithEvents ListMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private WithEvents DetailsMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private EditMenu As Xceed.SmartUI.Controls.MenuBar.MainMenuItem
    Private WithEvents RenameMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents ListToolbarMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private WithEvents DetailsToolbarMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private WithEvents IconsToolbarMenu As Xceed.SmartUI.Controls.MenuBar.CheckMenuItem
    Private WithEvents CopyTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private separatorTool3 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private WithEvents CutTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private WithEvents PasteTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private separatorTool4 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private WithEvents DeleteTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private WithEvents CopyContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents CutContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents PasteContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents DeleteContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents EditContextMenu As System.Windows.Forms.ContextMenu
    Private menuItem1 As System.Windows.Forms.MenuItem
    Private WithEvents RenameContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents CopyMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents CutMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents PasteMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private separatorMenuItem1 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private WithEvents DeleteMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents RenameTool As Xceed.SmartUI.Controls.ToolBar.Tool
    Private separatorTool5 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private comboBoxItem1 As Xceed.SmartUI.Controls.ToolBar.ComboBoxItem
    Private node1 As Xceed.SmartUI.Controls.TreeView.Node
    Private CompressedSize As System.Windows.Forms.ColumnHeader
    Private separatorMenuItem2 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private mixedTool1 As Xceed.SmartUI.Controls.ToolBar.MixedTool
    Private NewTool As Xceed.SmartUI.Controls.ToolBar.MenuTool
    Private separatorTool6 As Xceed.SmartUI.Controls.ToolBar.SeparatorTool
    Private separatorMenuItem3 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private NewMenu As Xceed.SmartUI.Controls.MenuBar.PopupMenuItem
    Private WithEvents NewFileMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents NewFolderMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents NewTarArchiveMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents NewZipArchiveMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private NewContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents NewFileContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents NewFolderContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents NewTarArchiveContextMenu As System.Windows.Forms.MenuItem
    Private WithEvents NewZipArchiveContextMenu As System.Windows.Forms.MenuItem
    Private menuItem2 As System.Windows.Forms.MenuItem
    Private ToolsMenu As Xceed.SmartUI.Controls.MenuBar.MainMenuItem
    Private WithEvents OptionsMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents NewGZipArchiveMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents NewGZipArchiveContextMenu As System.Windows.Forms.MenuItem
    Private separatorMenuItem4 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private menuItem3 As System.Windows.Forms.MenuItem
    Private separatorMenuItem5 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private WithEvents PropertiesMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private menuItem4 As System.Windows.Forms.MenuItem
    Private WithEvents PropertiesContextMenu As System.Windows.Forms.MenuItem
    Private menuItem5 As System.Windows.Forms.MenuItem
    Private WithEvents NewFtpConnectionContextMenu As System.Windows.Forms.MenuItem
    Private separatorMenuItem6 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private WithEvents NewFtpConnectionMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private separatorMenuItem7 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private separatorMenuItem8 As Xceed.SmartUI.Controls.MenuBar.SeparatorMenuItem
    Private WithEvents OpenMenu As Xceed.SmartUI.Controls.MenuBar.MenuItem
    Private WithEvents OpenContextMenu As System.Windows.Forms.MenuItem
    Private menuItem6 As System.Windows.Forms.MenuItem
    Private WithEvents FileView As Xceed.FileSystem.Samples.Utils.ListView.CustomListView

#End Region     ' Windows Form Designer generated fields
  End Class
End Namespace
