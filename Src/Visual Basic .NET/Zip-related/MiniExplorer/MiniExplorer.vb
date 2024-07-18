' 
' Xceed Zip for .NET - MiniExplorer Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [MiniExplorer.vb]
'  
' This application demonstrates how to use the Xceed FileSystem Object model
' in a generic way.
'  
' This file is part of Xceed Zip for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports Xceed.FileSystem
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples.MiniExplorer

  Public Class MiniExplorer
    Inherits System.Windows.Forms.Form

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

      ' Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      ' Retrieve all available disk drives
      Dim drives() As String = System.IO.Directory.GetLogicalDrives()

      Dim drive As String
      For Each drive In drives
        Dim folder As FolderTreeNode = New FolderTreeNode(New DiskFolder(drive), drive)
        FolderTree.Nodes.Add(folder)
      Next

      ' Let's also add a virtual RAM drive just for fun!
      ' (and to show you how amazing are the MemoryFolder and MemoryFile classes!)
      Dim ramDrive As MemoryFolder = New MemoryFolder("RAM", "")
      FolderTree.Nodes.Add(New FolderTreeNode(ramDrive, ramDrive.FullName))

      ' Let's add a user store drive
      Dim isoDrive As IsolatedFolder = New IsolatedFolder("\")
      FolderTree.Nodes.Add(New FolderTreeNode(isoDrive, "UserStore:\"))

      ' Set the second node as the starting point      
      FolderTree.SelectedNode = FolderTree.Nodes(1)

    End Sub

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
    Friend WithEvents EditNewZipFileMenu As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents EditDeleteMenu As System.Windows.Forms.MenuItem
    Friend WithEvents FileList As System.Windows.Forms.ListView
    Friend WithEvents FileListColName As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileListColSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileListColAttributes As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileSystemItemImages As System.Windows.Forms.ImageList
    Friend WithEvents EditRenameMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditNewFileMenu As System.Windows.Forms.MenuItem
    Friend WithEvents MiniExplorerMenu As System.Windows.Forms.MainMenu
    Friend WithEvents FileMenu As System.Windows.Forms.MenuItem
    Friend WithEvents FileQuitMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditCutMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditCopyMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditPasteMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditNewMenu As System.Windows.Forms.MenuItem
    Friend WithEvents EditNewFolderMenu As System.Windows.Forms.MenuItem
    Friend WithEvents menuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents EditRefreshMenu As System.Windows.Forms.MenuItem
    Friend WithEvents HelpMenu As System.Windows.Forms.MenuItem
    Friend WithEvents HelpAboutMenu As System.Windows.Forms.MenuItem
    Friend WithEvents splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents FolderTree As System.Windows.Forms.TreeView
    Friend WithEvents ResultList As System.Windows.Forms.ListBox
    Friend WithEvents ProgressPanel As System.Windows.Forms.Panel
    Friend WithEvents CurrentProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents AllProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents EditSplitMenu As System.Windows.Forms.MenuItem
    Friend WithEvents FileListColLastWrite As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileListColLastAccess As System.Windows.Forms.ColumnHeader
    Friend WithEvents FileListColCreation As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.components = New System.ComponentModel.Container
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MiniExplorer))
      Me.EditNewZipFileMenu = New System.Windows.Forms.MenuItem
      Me.menuItem5 = New System.Windows.Forms.MenuItem
      Me.EditDeleteMenu = New System.Windows.Forms.MenuItem
      Me.FileList = New System.Windows.Forms.ListView
      Me.FileListColName = New System.Windows.Forms.ColumnHeader
      Me.FileListColSize = New System.Windows.Forms.ColumnHeader
      Me.FileListColAttributes = New System.Windows.Forms.ColumnHeader
      Me.FileSystemItemImages = New System.Windows.Forms.ImageList(Me.components)
      Me.EditRenameMenu = New System.Windows.Forms.MenuItem
      Me.EditNewFileMenu = New System.Windows.Forms.MenuItem
      Me.MiniExplorerMenu = New System.Windows.Forms.MainMenu
      Me.FileMenu = New System.Windows.Forms.MenuItem
      Me.FileQuitMenu = New System.Windows.Forms.MenuItem
      Me.EditMenu = New System.Windows.Forms.MenuItem
      Me.EditCutMenu = New System.Windows.Forms.MenuItem
      Me.EditCopyMenu = New System.Windows.Forms.MenuItem
      Me.EditPasteMenu = New System.Windows.Forms.MenuItem
      Me.EditNewMenu = New System.Windows.Forms.MenuItem
      Me.EditNewFolderMenu = New System.Windows.Forms.MenuItem
      Me.menuItem1 = New System.Windows.Forms.MenuItem
      Me.EditRefreshMenu = New System.Windows.Forms.MenuItem
      Me.MenuItem2 = New System.Windows.Forms.MenuItem
      Me.EditSplitMenu = New System.Windows.Forms.MenuItem
      Me.HelpMenu = New System.Windows.Forms.MenuItem
      Me.HelpAboutMenu = New System.Windows.Forms.MenuItem
      Me.splitter1 = New System.Windows.Forms.Splitter
      Me.FolderTree = New System.Windows.Forms.TreeView
      Me.ResultList = New System.Windows.Forms.ListBox
      Me.ProgressPanel = New System.Windows.Forms.Panel
      Me.CurrentProgressBar = New System.Windows.Forms.ProgressBar
      Me.AllProgressBar = New System.Windows.Forms.ProgressBar
      Me.splitter2 = New System.Windows.Forms.Splitter
      Me.FileListColLastWrite = New System.Windows.Forms.ColumnHeader
      Me.FileListColLastAccess = New System.Windows.Forms.ColumnHeader
      Me.FileListColCreation = New System.Windows.Forms.ColumnHeader
      Me.ProgressPanel.SuspendLayout()
      Me.SuspendLayout()
      '
      'EditNewZipFileMenu
      '
      Me.EditNewZipFileMenu.Index = 2
      Me.EditNewZipFileMenu.Text = "Zip file..."
      '
      'menuItem5
      '
      Me.menuItem5.Index = 3
      Me.menuItem5.Text = "-"
      '
      'EditDeleteMenu
      '
      Me.EditDeleteMenu.Index = 6
      Me.EditDeleteMenu.Shortcut = System.Windows.Forms.Shortcut.Del
      Me.EditDeleteMenu.Text = "&Delete"
      '
      'FileList
      '
      Me.FileList.AllowDrop = True
      Me.FileList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.FileListColName, Me.FileListColSize, Me.FileListColAttributes, Me.FileListColLastWrite, Me.FileListColLastAccess, Me.FileListColCreation})
      Me.FileList.Dock = System.Windows.Forms.DockStyle.Fill
      Me.FileList.FullRowSelect = True
      Me.FileList.HideSelection = False
      Me.FileList.LabelEdit = True
      Me.FileList.Location = New System.Drawing.Point(203, 0)
      Me.FileList.Name = "FileList"
      Me.FileList.Size = New System.Drawing.Size(485, 363)
      Me.FileList.SmallImageList = Me.FileSystemItemImages
      Me.FileList.TabIndex = 11
      Me.FileList.View = System.Windows.Forms.View.Details
      '
      'FileListColName
      '
      Me.FileListColName.Text = "Name"
      Me.FileListColName.Width = 285
      '
      'FileListColSize
      '
      Me.FileListColSize.Text = "Size"
      '
      'FileListColAttributes
      '
      Me.FileListColAttributes.Text = "Attributes"
      Me.FileListColAttributes.Width = 85
      '
      'FileSystemItemImages
      '
      Me.FileSystemItemImages.ImageSize = New System.Drawing.Size(16, 16)
      Me.FileSystemItemImages.ImageStream = CType(resources.GetObject("FileSystemItemImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
      Me.FileSystemItemImages.TransparentColor = System.Drawing.Color.White
      '
      'EditRenameMenu
      '
      Me.EditRenameMenu.Index = 5
      Me.EditRenameMenu.Shortcut = System.Windows.Forms.Shortcut.F2
      Me.EditRenameMenu.Text = "&Rename"
      '
      'EditNewFileMenu
      '
      Me.EditNewFileMenu.Index = 1
      Me.EditNewFileMenu.Text = "Fil&e..."
      '
      'MiniExplorerMenu
      '
      Me.MiniExplorerMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileMenu, Me.EditMenu, Me.HelpMenu})
      '
      'FileMenu
      '
      Me.FileMenu.Index = 0
      Me.FileMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.FileQuitMenu})
      Me.FileMenu.Text = "&File"
      '
      'FileQuitMenu
      '
      Me.FileQuitMenu.Index = 0
      Me.FileQuitMenu.Text = "&Quit"
      '
      'EditMenu
      '
      Me.EditMenu.Index = 1
      Me.EditMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.EditCutMenu, Me.EditCopyMenu, Me.EditPasteMenu, Me.menuItem5, Me.EditNewMenu, Me.EditRenameMenu, Me.EditDeleteMenu, Me.menuItem1, Me.EditRefreshMenu, Me.MenuItem2, Me.EditSplitMenu})
      Me.EditMenu.Text = "&Edit"
      '
      'EditCutMenu
      '
      Me.EditCutMenu.Index = 0
      Me.EditCutMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlX
      Me.EditCutMenu.Text = "C&ut"
      '
      'EditCopyMenu
      '
      Me.EditCopyMenu.Index = 1
      Me.EditCopyMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlC
      Me.EditCopyMenu.Text = "C&opy"
      '
      'EditPasteMenu
      '
      Me.EditPasteMenu.Index = 2
      Me.EditPasteMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlV
      Me.EditPasteMenu.Text = "&Paste"
      '
      'EditNewMenu
      '
      Me.EditNewMenu.Index = 4
      Me.EditNewMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.EditNewFolderMenu, Me.EditNewFileMenu, Me.EditNewZipFileMenu})
      Me.EditNewMenu.Text = "&New"
      '
      'EditNewFolderMenu
      '
      Me.EditNewFolderMenu.Index = 0
      Me.EditNewFolderMenu.Text = "&Folder..."
      '
      'menuItem1
      '
      Me.menuItem1.Index = 7
      Me.menuItem1.Text = "-"
      '
      'EditRefreshMenu
      '
      Me.EditRefreshMenu.Index = 8
      Me.EditRefreshMenu.Shortcut = System.Windows.Forms.Shortcut.F5
      Me.EditRefreshMenu.Text = "R&efresh"
      '
      'MenuItem2
      '
      Me.MenuItem2.Index = 9
      Me.MenuItem2.Text = "-"
      '
      'EditSplitMenu
      '
      Me.EditSplitMenu.Index = 10
      Me.EditSplitMenu.Text = "Split / Unsplit zip file..."
      '
      'HelpMenu
      '
      Me.HelpMenu.Index = 2
      Me.HelpMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.HelpAboutMenu})
      Me.HelpMenu.Text = "&Help"
      '
      'HelpAboutMenu
      '
      Me.HelpAboutMenu.Index = 0
      Me.HelpAboutMenu.Text = "&About MiniExplorer"
      '
      'splitter1
      '
      Me.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.splitter1.Location = New System.Drawing.Point(0, 448)
      Me.splitter1.Name = "splitter1"
      Me.splitter1.Size = New System.Drawing.Size(688, 3)
      Me.splitter1.TabIndex = 13
      Me.splitter1.TabStop = False
      '
      'FolderTree
      '
      Me.FolderTree.AllowDrop = True
      Me.FolderTree.Dock = System.Windows.Forms.DockStyle.Left
      Me.FolderTree.HideSelection = False
      Me.FolderTree.ImageIndex = 1
      Me.FolderTree.ImageList = Me.FileSystemItemImages
      Me.FolderTree.LabelEdit = True
      Me.FolderTree.Location = New System.Drawing.Point(3, 0)
      Me.FolderTree.Name = "FolderTree"
      Me.FolderTree.Size = New System.Drawing.Size(200, 363)
      Me.FolderTree.TabIndex = 10
      '
      'ResultList
      '
      Me.ResultList.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.ResultList.Location = New System.Drawing.Point(3, 363)
      Me.ResultList.Name = "ResultList"
      Me.ResultList.Size = New System.Drawing.Size(685, 69)
      Me.ResultList.TabIndex = 12
      '
      'ProgressPanel
      '
      Me.ProgressPanel.Controls.Add(Me.CurrentProgressBar)
      Me.ProgressPanel.Controls.Add(Me.AllProgressBar)
      Me.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom
      Me.ProgressPanel.Location = New System.Drawing.Point(3, 432)
      Me.ProgressPanel.Name = "ProgressPanel"
      Me.ProgressPanel.Size = New System.Drawing.Size(685, 16)
      Me.ProgressPanel.TabIndex = 15
      '
      'CurrentProgressBar
      '
      Me.CurrentProgressBar.Dock = System.Windows.Forms.DockStyle.Left
      Me.CurrentProgressBar.Location = New System.Drawing.Point(0, 0)
      Me.CurrentProgressBar.Name = "CurrentProgressBar"
      Me.CurrentProgressBar.Size = New System.Drawing.Size(192, 16)
      Me.CurrentProgressBar.TabIndex = 8
      '
      'AllProgressBar
      '
      Me.AllProgressBar.Dock = System.Windows.Forms.DockStyle.Fill
      Me.AllProgressBar.Location = New System.Drawing.Point(0, 0)
      Me.AllProgressBar.Name = "AllProgressBar"
      Me.AllProgressBar.Size = New System.Drawing.Size(685, 16)
      Me.AllProgressBar.TabIndex = 7
      '
      'splitter2
      '
      Me.splitter2.Location = New System.Drawing.Point(0, 0)
      Me.splitter2.Name = "splitter2"
      Me.splitter2.Size = New System.Drawing.Size(3, 448)
      Me.splitter2.TabIndex = 14
      Me.splitter2.TabStop = False
      '
      'FileListColLastWrite
      '
      Me.FileListColLastWrite.Text = "Last modified"
      Me.FileListColLastWrite.Width = 125
      '
      'FileListColLastAccess
      '
      Me.FileListColLastAccess.Text = "Last accessed"
      Me.FileListColLastAccess.Width = 125
      '
      'FileListColCreation
      '
      Me.FileListColCreation.Text = "Created"
      Me.FileListColCreation.Width = 125
      '
      'MiniExplorer
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(688, 451)
      Me.Controls.Add(Me.FileList)
      Me.Controls.Add(Me.FolderTree)
      Me.Controls.Add(Me.ResultList)
      Me.Controls.Add(Me.ProgressPanel)
      Me.Controls.Add(Me.splitter2)
      Me.Controls.Add(Me.splitter1)
      Me.Menu = Me.MiniExplorerMenu
      Me.Name = "MiniExplorer"
      Me.Text = "Xceed MiniExplorer"
      Me.ProgressPanel.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub

#End Region

#Region "FOLDERTREE EVENTS"

    Private Sub FolderTree_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles FolderTree.BeforeExpand
      FillFolderTree(CType(e.Node, FolderTreeNode))
    End Sub

    Private Sub FolderTree_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles FolderTree.AfterSelect
      FillFileList(CType(e.Node, FolderTreeNode))
    End Sub

    Private Sub FolderTree_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles FolderTree.AfterLabelEdit
      ' We always remove the node that was used to rename this new folder.
      Dim node As FolderTreeNode = CType(e.Node.Parent, FolderTreeNode)

      Dim NewLabel As String = e.Label

      If NewLabel Is Nothing Then
        NewLabel = e.Node.Text
      End If

      If (Not e.CancelEdit) And (NewLabel <> Nothing) Then
        Try

          If ((CType(e.Node, FolderTreeNode).Folder Is Nothing) Or (NewLabel <> e.Node.Text)) And (node.Folder.GetFolder(NewLabel).Exists) Then
            MessageBox.Show("This folder already exists", "Error creating folder", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.CancelEdit = True
            e.Node.BeginEdit()
            Return
          End If

          If (CType(e.Node, FolderTreeNode)).Folder Is Nothing Then
            ' Create the REAL folder and the node for it.
            Dim NewFolder As AbstractFolder = node.Folder.CreateFolder(NewLabel)
            Dim NewNode As FolderTreeNode = New FolderTreeNode(NewFolder, NewLabel)

            node.Nodes.Add(NewNode)
          Else
            ' Rename
            CType(e.Node, FolderTreeNode).Folder.Name = NewLabel
          End If
        Catch except As Exception
          ResultList.Items.Add(except.Message)
        End Try
      End If

      ' We remove the dummy node only at the very end
      If TypeOf e.Node Is FolderTreeNode Then
        If (CType(e.Node, FolderTreeNode)).Folder Is Nothing Then
          ' This was a new folder and not a rename.
          e.Node.Remove()
        End If
      End If
    End Sub

    Private Sub FolderTree_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles FolderTree.ItemDrag
      ResultList.Items.Clear()

      Dim data As DataObject = New DataObject()
      data.SetData(GetType(FolderTreeNode), e.Item)
      DoDragDrop(data, DragDropEffects.Copy Or DragDropEffects.Move)
    End Sub

    Private Sub FolderTree_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FolderTree.DragOver
      Dim node As TreeNode = FolderTree.GetNodeAt(Me.PointToClient(New Point(e.X, e.Y)))
      Dim sourceFolder As AbstractFolder = Nothing

      e.Effect = DragDropEffects.None

      ' We don't allow to drop the file/folder in the source folder
      ' Check for this.
      If e.Data.GetDataPresent(GetType(FolderTreeNode)) Then
        ' Copy/move the folder. The source folder is provided in e.Data
        sourceFolder = (CType(e.Data.GetData(GetType(FolderTreeNode)), FolderTreeNode)).Folder
      Else
        ' Copy/move the file(s). The source folder is the selected one.
        sourceFolder = (CType(FolderTree.SelectedNode, FolderTreeNode)).Folder
      End If

      If Not (node Is Nothing) Then
        If Not (CType(node, FolderTreeNode)).Folder.Equals(sourceFolder) Then
          If Not node.Equals(m_previousDropNode) Then
            HighlightDropInNode(node)
          End If

          If ((e.KeyState And 4) > 0) And ((e.AllowedEffect Or DragDropEffects.Move) > 0) Then
            e.Effect = DragDropEffects.Move
          ElseIf (e.AllowedEffect Or DragDropEffects.Copy) > 0 Then
            e.Effect = DragDropEffects.Copy
          End If
        End If
      End If
    End Sub


    Private Sub FolderTree_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FolderTree.DragDrop
      Dim node As FolderTreeNode = CType(FolderTree.GetNodeAt(PointToClient(New Point(e.X, e.Y))), FolderTreeNode)

      HighlightDropInNode(Nothing)

      Try
        If e.Effect = DragDropEffects.Copy Then
          PasteFromDataObject(node, e.Data, False)
        ElseIf e.Effect = DragDropEffects.Move Then
          PasteFromDataObject(node, e.Data, True)
        End If
      Catch except As Exception
        ResultList.Items.Add(except.Message)
      End Try
    End Sub

    Private Sub FolderTree_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.DragLeave
      HighlightDropInNode(Nothing)
    End Sub

    Private Sub FolderTree_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.Enter
      EditNewFolderMenu.Enabled = True
    End Sub

    Private Sub FolderTree_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FolderTree.KeyDown
      ' Check if only the Ctrl key is pressed
      If e.Modifiers = Keys.Control Then
        Select Case e.KeyCode
          Case Keys.Insert
            CopyFolderInClipboard(False)
            Exit Sub
        End Select
      End If

      ' Check if only the Shift key is pressed
      If e.Modifiers = Keys.Shift Then
        Select Case e.KeyCode
          Case Keys.Insert
            PasteFromClipboard()
            Exit Sub

          Case Keys.Delete
            CopyFolderInClipboard(True)
            Exit Sub
        End Select
      End If
    End Sub

    Private Sub FolderTree_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FolderTree.Leave
      EditNewFolderMenu.Enabled = False
    End Sub

#End Region

#Region "FILELIST EVENTS"

    Private Sub FileList_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles FileList.AfterLabelEdit
      Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)
      Dim NewLabel As String = e.Label

      If NewLabel Is Nothing Then
        NewLabel = FileList.Items(e.Item).Text
      End If

      ' Check if the file already exists
      If Not NewLabel Is Nothing Then

        If (((CType(FileList.Items(e.Item), FileListItem)).File Is Nothing) Or (NewLabel <> FileList.Items(e.Item).Text)) And (node.Folder.GetFile(NewLabel).Exists) Then
          MessageBox.Show("This file already exists", "Error creating file", MessageBoxButtons.OK, MessageBoxIcon.Error)
          e.CancelEdit = True
          FileList.Items(e.Item).BeginEdit()
          Return
        End If
      End If

      If (Not e.CancelEdit) And (Not NewLabel Is Nothing) Then
        Try
          If (CType(FileList.Items(e.Item), FileListItem)).File Is Nothing Then
            ' We always remove the node that was used to rename this new zip file.
            FileList.Items(e.Item).Remove()

            Dim NewFile As AbstractFile = node.Folder.CreateFile(NewLabel, False)
            Dim NewItem As FileListItem = New FileListItem(NewFile)

            FileList.Items.Add(NewItem)
          Else
            ' Rename
            CType(FileList.Items(e.Item), FileListItem).File.Name = NewLabel
          End If

          ' We must also refresh the FolderTree since this file may end by ".zip"
          RefreshFolderTree(node)
        Catch except As Exception
          ResultList.Items.Add(except.Message)
        End Try
      End If
    End Sub

    Private Sub FileList_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles FileList.ItemDrag
      Dim files() As AbstractFile = New AbstractFile(FileList.SelectedItems.Count) {}
      Dim index As Integer = 0

      Dim item As FileListItem
      For Each item In FileList.SelectedItems
        files(index) = item.File
        ResultList.Items.Add("Dragging " + item.File.FullName)
        index = index + 1
      Next

      Dim data As DataObject = New DataObject(files)
      DoDragDrop(data, DragDropEffects.Copy Or DragDropEffects.Move)
    End Sub

    Private Sub FileList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FileList.KeyDown
      ' Check if only the Ctrl key is pressed
      If e.Modifiers = Keys.Control Then
        Select Case e.KeyCode
          Case Keys.Insert
            CopyFilesInClipboard(False)
            Exit Sub
        End Select
      End If

      ' Check if only the Shift key is pressed
      If e.Modifiers = Keys.Shift Then
        Select Case e.KeyCode
          Case Keys.Insert
            PasteFromClipboard()
            Exit Sub

          Case Keys.Delete
            CopyFilesInClipboard(True)
            Exit Sub
        End Select
      End If
    End Sub


#End Region

#Region "MENU EVENTS"

    Private Sub EditCutMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCutMenu.Click

      If (FolderTree.Focused) Then
        CopyFolderInClipboard(True)
      Else
        If (FileList.Focused) Then
          CopyFilesInClipboard(True)
        End If
      End If

    End Sub

    Private Sub EditCopyMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditCopyMenu.Click

      If (FolderTree.Focused) Then
        CopyFolderInClipboard(False)
      Else
        If (FileList.Focused) Then
          CopyFilesInClipboard(False)
        End If
      End If

    End Sub

    Private Sub EditPasteMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditPasteMenu.Click
      PasteFromClipboard()
    End Sub

    Private Sub EditDeleteMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditDeleteMenu.Click
      If (FolderTree.Focused) Then
        DeleteCurrentFolder()
      Else
        If (FileList.Focused) Then
          DeleteCurrentFile()
        End If
      End If

    End Sub

    Private Sub EditNewFileMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditNewFileMenu.Click
      Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

      If Not node Is Nothing Then
        Dim name As String = "New file"
        Dim number As Integer = 2

        While node.Folder.GetFile(name).Exists Or node.Folder.GetFolder(name).Exists
          name = "New file (" + number.ToString() + ")"
          number = number + 1
        End While

        Dim NewFile As FileListItem = New FileListItem(name)
        FileList.Items.Add(NewFile)

        NewFile.BeginEdit()
      End If
    End Sub

    Private Sub EditNewZipFileMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditNewZipFileMenu.Click
      Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

      If Not node Is Nothing Then
        Dim name As String = "New file.zip"
        Dim number As Integer = 2

        While node.Folder.GetFile(name).Exists Or node.Folder.GetFolder(name).Exists
          name = "New file (" + number.ToString() + ").zip"
          number = number + 1
        End While
        Dim NewFile As FileListItem = New FileListItem(name)
        FileList.Items.Add(NewFile)

        NewFile.BeginEdit()
      End If
    End Sub

    Private Sub EditRefreshMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditRefreshMenu.Click
      FillFileList(CType(FolderTree.SelectedNode, FolderTreeNode))
    End Sub

    Private Sub EditRenameMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditRenameMenu.Click

      If (FolderTree.Focused) Then
        RenameCurrentFolder()
      Else
        If (FileList.Focused) Then
          RenameCurrentFile()
        End If
      End If

    End Sub

    Private Sub EditSplitMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditSplitMenu.Click
      Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

      If ((Not node Is Nothing) And (TypeOf (node.Folder) Is ZipArchive)) Then
        Dim zip As ZipArchive = CType(node.Folder, ZipArchive)

        Dim splitSize As Long = zip.SplitSize

        Dim splitForm As SplitSizeForm = New SplitSizeForm()

        If (splitForm.ShowDialog(Me, splitSize) = System.Windows.Forms.DialogResult.OK) Then
          zip.BeginUpdate(m_zipEvents, Nothing)
          System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

          Try
            zip.SplitSize = splitSize
            zip.SplitNameFormat = SplitNameFormat.PkZip

            ' Setting the above properties does not flag the zip file to update itself.
            ' Since we have no other modification to make, we can use the Comment property
            ' as a way to tell the zip file to update itself!
            zip.Comment = "!"
            zip.Comment = ""

          Finally
            zip.EndUpdate(m_zipEvents, Nothing)
            System.Windows.Forms.Cursor.Current = Cursors.Default

            RefreshFolderTree(node)
            FillFileList(CType(FolderTree.SelectedNode, FolderTreeNode))
          End Try
        End If
      End If

    End Sub

    Private Sub EditNewFolderMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EditNewFolderMenu.Click
      If (FolderTree.Focused) Then
        Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

        If Not (node Is Nothing) Then
          node.Expand()

          Dim name As String = "New folder"
          Dim number As Integer = 2

          While ((node.Folder.GetFolder(name).Exists) Or (node.Folder.GetFile(name).Exists))
            name = "New folder (" + number.ToString() + ")"
            number = number + 1
          End While

          Dim newFolder As FolderTreeNode = New FolderTreeNode(name)
          node.Nodes.Add(newFolder)

          ' Expand again, without refreshing view, if newly added item is alone
          If (node.Nodes.Count = 1) Then
            m_preventTreeUpdate = True
            node.Expand()
            m_preventTreeUpdate = False
          End If

          newFolder.BeginEdit()
        End If
      End If

    End Sub

    Private Sub FileQuitMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FileQuitMenu.Click
      Me.Close()
    End Sub

    Private Sub HelpAboutMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles HelpAboutMenu.Click
      MessageBox.Show("Xceed Zip for .NET - MiniExplorer Sample Application" & Environment.NewLine & _
                      "Written in Visual Basic .NET" & Environment.NewLine & _
                      "Copyrights (c) 2001-2003 - Xceed Software Inc.", _
                      "About MiniExplorer...", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region

#Region "FILESYSTEM EVENTS"

    Private Sub m_zipEvents_ByteProgression(ByVal sender As Object, ByVal e As ByteProgressionEventArgs)
      CurrentProgressBar.Value = e.CurrentFileBytes.Percent
      AllProgressBar.Value = e.AllFilesBytes.Percent

      If e.AllFilesBytes.Percent = 100 Then
        CurrentProgressBar.Value = 0
        AllProgressBar.Value = 0
      End If
    End Sub

    Private Sub m_zipEvents_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs)
      If TypeOf e.Exception Is InvalidDecryptionPasswordException Then
        Try
          Dim rootZip As ZipArchive = CType((CType(e.CurrentItem, ZippedFile)).RootFolder, ZipArchive)
          If m_currentPassword = rootZip.DefaultDecryptionPassword Then
            Dim passwordDialog As PasswordForm = New PasswordForm()

            If passwordDialog.ShowDialog(Me, e.CurrentItem.FullName, m_currentPassword) = System.Windows.Forms.DialogResult.OK Then
              rootZip.DefaultDecryptionPassword = m_currentPassword
              e.Action = ItemExceptionAction.Retry
            End If
          Else
            rootZip.DefaultDecryptionPassword = m_currentPassword
            e.Action = ItemExceptionAction.Retry
          End If
        Catch except As Exception
          System.Diagnostics.Debug.WriteLine(except.Message)
        End Try

      ElseIf TypeOf (e.Exception) Is ItemIsReadOnlyException Then
        Dim answer As DialogResult = MessageBox.Show(e.Exception.Message & _
          "\n" + "\nDo you wish to remove the read-only attribute on this item?", e.Exception.GetType().ToString() & _
          "...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        Select Case (answer)
          Case System.Windows.Forms.DialogResult.Yes
            Try
              ' An ItemIsReadOnlyException exception may occur if overwriting an existing
              ' read-only item (then TargetItem is the one), or when deleting a read-only
              ' file (then CurrentItem is the one and TargetItem is null).
              If e.TargetItem Is Nothing Then
                e.CurrentItem.Attributes = (e.CurrentItem.Attributes And (Not System.IO.FileAttributes.ReadOnly))
              Else
                e.TargetItem.Attributes = (e.TargetItem.Attributes And (Not System.IO.FileAttributes.ReadOnly))
              End If

              e.Action = ItemExceptionAction.Retry
            Catch except As Exception
              System.Diagnostics.Debug.WriteLine(except.ToString())
            End Try

          Case System.Windows.Forms.DialogResult.No
            e.Action = ItemExceptionAction.Ignore

          Case System.Windows.Forms.DialogResult.Cancel
            e.Action = ItemExceptionAction.Abort

        End Select
      Else
        Dim answer As DialogResult = MessageBox.Show( _
          e.Exception.Message + "\n" + "\nWhat do you wish to do?", e.Exception.GetType().ToString() + "...", _
          MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question)

        Select Case answer
          Case System.Windows.Forms.DialogResult.Abort
            e.Action = ItemExceptionAction.Abort
            Exit Sub

          Case System.Windows.Forms.DialogResult.Retry
            e.Action = ItemExceptionAction.Retry
            Exit Sub

          Case System.Windows.Forms.DialogResult.Ignore
            e.Action = ItemExceptionAction.Ignore
            Exit Sub
        End Select
      End If
    End Sub

#End Region

#Region "ZIP EVENTS"

    Private Sub m_zipEvents_BuildingZipByteProgression(ByVal sender As Object, ByVal e As ByteProgressionEventArgs)
      If e.AllFilesBytes.Percent <> m_buildingProgression Then
        ResultList.Items.Add("Building Temporary Zip file " & e.AllFilesBytes.Percent & " % Done.")
        m_buildingProgression = e.AllFilesBytes.Percent
      End If

      If e.AllFilesBytes.Percent = 100 Then
        m_buildingProgression = 255
      End If
    End Sub

    Private Sub m_zipEvents_DiskRequired(ByVal sender As Object, ByVal e As DiskRequiredEventArgs)
      If (e.Action = DiskRequiredAction.Fail) Then
        ' The user must provide us with the required file part. The library
        ' cannot automatically find the required zip file part.

        ' Now if the reason is for deleting useless disks, we simply skip that
        ' step by setting the Action to Fail. This instructs the library to
        ' skip that step without error.
        If (e.Reason = DiskRequiredReason.Deleting) Then
          e.Action = DiskRequiredAction.Fail

        Else
          Dim diskForm As DiskRequiredForm = New DiskRequiredForm()

          Dim zipFile As AbstractFile = e.ZipFile

          If (diskForm.ShowDialog(Me, zipFile, e.DiskNumber, e.Reason) = System.Windows.Forms.DialogResult.OK) Then
            e.ZipFile = zipFile
            e.Action = DiskRequiredAction.[Continue]
          End If

        End If
      Else
        ' When the default action is to continue, we give Xceed Zip a chance with
        ' his split name formating.
        ResultList.Items.Add("Switching to file " + e.ZipFile.FullName)
      End If

    End Sub


#End Region

#Region "FORM EVENTS"

    Private Sub MiniExplorer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

      m_zipEvents = New ZipEvents()
      AddHandler m_zipEvents.ByteProgression, AddressOf m_zipEvents_ByteProgression
      AddHandler m_zipEvents.ItemException, AddressOf m_zipEvents_ItemException
      AddHandler m_zipEvents.BuildingZipByteProgression, AddressOf m_zipEvents_BuildingZipByteProgression
      AddHandler m_zipEvents.DiskRequired, AddressOf m_zipEvents_DiskRequired

    End Sub

#End Region

#Region "PRIVATE METHODS"

    Private Sub CopyFolderInClipboard(ByVal cut As Boolean)
      m_Clipboard = New DataObject()

      m_Clipboard.SetData(GetType(FolderTreeNode), CType(FolderTree.SelectedNode, FolderTreeNode))
      ' As a goodie, we provide a textual representation of the Folder in the Clipboard
      m_Clipboard.SetData(DataFormats.Text, (CType(FolderTree.SelectedNode, FolderTreeNode)).Folder.FullName)
      System.Windows.Forms.Clipboard.SetDataObject(m_Clipboard)

      m_cutClipboard = cut
    End Sub

    Private Sub CopyFilesInClipboard(ByVal cut As Boolean)
      Dim files() As AbstractFile = New AbstractFile(FileList.SelectedItems.Count) {}
      Dim index As Integer = 0
      Dim textContent As System.Text.StringBuilder = New System.Text.StringBuilder()

      Dim item As FileListItem
      For Each item In FileList.SelectedItems
        files(index) = item.File
        ' As a goodie, we provide a textual representation of each selected file in the Clipboard
        textContent.Append(item.File.FullName + "\r\n")
        index = index + 1
      Next

      m_Clipboard = New DataObject()

      m_Clipboard.SetData(GetType(AbstractFile()), files)
      m_Clipboard.SetData(DataFormats.Text, textContent)
      Clipboard.SetDataObject(m_Clipboard)

      m_cutClipboard = cut
    End Sub

    Private Sub PasteFromClipboard()
      If FolderTree.Focused Or FileList.Focused Then
        ' We use the m_Clipboard object because of a bug in the Clipboard.GetDataObject().
        PasteFromDataObject(CType(FolderTree.SelectedNode, FolderTreeNode), m_Clipboard, m_cutClipboard)

        ' The potential following "pastes" shall not "cut"
        m_cutClipboard = False
      End If
    End Sub


    Private Sub PasteFromDataObject(ByVal destinationNode As FolderTreeNode, ByVal dataObject As IDataObject, ByVal move As Boolean)
      If (move) Then
        ResultList.Items.Add("Moving to folder " + destinationNode.Folder.FullName + "...")
      Else
        ResultList.Items.Add("Copying to folder " + destinationNode.Folder.FullName + "...")
      End If

      Dim batch As IBatchUpdateable = Nothing
      If TypeOf destinationNode.Folder.RootFolder Is IBatchUpdateable Then
        batch = CType(destinationNode.Folder.RootFolder, IBatchUpdateable)
      End If

      If Not (batch Is Nothing) Then
        batch.BeginUpdate(m_zipEvents, Nothing)
      End If

      System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

      Try
        If (dataObject.GetDataPresent(GetType(FolderTreeNode))) Then

          Dim sourceNode As FolderTreeNode = CType(dataObject.GetData(GetType(FolderTreeNode)), FolderTreeNode)
          Dim folder As AbstractFolder = sourceNode.Folder

          ResultList.Items.Add(" folder " + folder.FullName)

          If (move) Then
            ' If we are attempting to move a zip file, we must move the
            ' file itself, not the ZipArchive representing the root folder
            ' of the zip file contents.
            If (TypeOf (folder) Is ZipArchive) Then
              CType(folder, ZipArchive).ZipFile.MoveTo(m_zipEvents, Nothing, destinationNode.Folder, False)
            Else
              folder.MoveTo(m_zipEvents, Nothing, destinationNode.Folder, False)
            End If

            ' We just moved the folder. Refresh its (previous) parent.
            RefreshFolderTree(CType(sourceNode.Parent, FolderTreeNode))
          Else
            ' If we are attempting to copy a zip file, we must copy the
            ' file itself, not the ZipArchive representing the root folder
            ' of the zip file contents.
            If (TypeOf (folder) Is ZipArchive) Then
              CType(folder, ZipArchive).ZipFile.CopyTo(m_zipEvents, Nothing, destinationNode.Folder, False)
            Else
              folder.CopyTo(m_zipEvents, Nothing, destinationNode.Folder, False)
            End If
          End If

        ElseIf dataObject.GetDataPresent(GetType(AbstractFile())) Then
          Dim file As AbstractFile
          Dim abstractfiles As AbstractFile() = dataObject.GetData(GetType(AbstractFile()))

          For Each file In abstractfiles
            If Not file Is Nothing Then
              ResultList.Items.Add(" file " + file.FullName)
              If (move) Then
                file.MoveTo(m_zipEvents, Nothing, destinationNode.Folder, True)
              Else
                file.CopyTo(m_zipEvents, Nothing, destinationNode.Folder, True)
              End If
            End If
          Next

        Else
          ResultList.Items.Add("Unknown data type.")
        End If

      Finally
        If Not batch Is Nothing Then
          batch.EndUpdate(m_zipEvents, Nothing)
        End If
      End Try

      System.Windows.Forms.Cursor.Current = Cursors.Default
      RefreshFolderTree(destinationNode)
      FillFileList(CType(FolderTree.SelectedNode, FolderTreeNode))

    End Sub

    Private Sub RefreshFolderTree(ByVal rootNode As FolderTreeNode)

      If (rootNode.IsExpanded) Then
        rootNode.Collapse()
        rootNode.Expand()
      ElseIf (rootNode.Nodes.Count = 0) Then
        ' This folder may have a newly copied/moved subfolder or zip file.
        ' Re-add the initial dummy child so it has a [+] sign.
        rootNode.Nodes.Add(String.Empty)
      End If

    End Sub

    Private Sub RenameCurrentFolder()
      If Not FolderTree.SelectedNode Is Nothing Then
        FolderTree.SelectedNode.BeginEdit()
      End If
    End Sub

    Private Sub RenameCurrentFile()
      If FileList.SelectedItems.Count > 0 Then
        FileList.SelectedItems(0).BeginEdit()
      End If
    End Sub


    Private Sub FillFolderTree(ByVal node As FolderTreeNode)

      If Not m_preventTreeUpdate Then
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        node.Nodes.Clear()

        Try
          ' We want to display zip files as folders
          Dim file As AbstractFile
          For Each file In node.Folder.GetFiles(False, "*.zip")
            Try
              Dim archive As ZipArchive = New ZipArchive(file)
              Dim subf As FolderTreeNode = New FolderTreeNode(archive, file.Name)
              subf.ForeColor = Color.Green
              node.Nodes.Add(subf)

              ' We always allow spanning of modified zip files
              CType(subf.Folder, ZipArchive).AllowSpanning = True
            Catch except As Exception
              ResultList.Items.Add(except.Message)
            End Try
          Next
        Catch except As Exception
          ResultList.Items.Add(except.Message)
        End Try

        Try
          ' We must not forget normal folders  
          Dim folder As AbstractFolder
          For Each folder In node.Folder.GetFolders(False)
            Try
              Dim subf As FolderTreeNode = New FolderTreeNode(folder, folder.Name)
              node.Nodes.Add(subf)
            Catch except As Exception
              ResultList.Items.Add(except.Message)
            End Try
          Next
        Catch except As Exception
          ResultList.Items.Add(except.Message)
        End Try

        System.Windows.Forms.Cursor.Current = Cursors.Default
      End If
    End Sub

    Private Sub FillFileList(ByVal node As FolderTreeNode)
      FileList.Items.Clear()

      If Not node Is Nothing Then
        System.Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Try
          Dim file As AbstractFile
          For Each file In node.Folder.GetFiles(False)
            Try
              Dim item As FileListItem = New FileListItem(CType(file, AbstractFile))
              FileList.Items.Add(item)
            Catch except As Exception
              ResultList.Items.Add(except.Message)
            End Try
          Next
        Catch except As Exception
          ResultList.Items.Add(except.Message)
        End Try

        System.Windows.Forms.Cursor.Current = Cursors.Default
      End If
    End Sub

    Private Sub HighlightDropInNode(ByVal node As TreeNode)
      If Not node Is Nothing Then
        node.BackColor = Color.FromKnownColor(KnownColor.Highlight)
        node.ForeColor = Color.FromKnownColor(KnownColor.HighlightText)
      End If

      If Not m_previousDropNode Is Nothing Then
        If TypeOf (CType(m_previousDropNode, FolderTreeNode)).Folder Is ZipArchive Then
          m_previousDropNode.BackColor = Color.FromKnownColor(KnownColor.Window)
          m_previousDropNode.ForeColor = Color.Green
        Else
          m_previousDropNode.BackColor = Color.FromKnownColor(KnownColor.Window)
          m_previousDropNode.ForeColor = Color.FromKnownColor(KnownColor.WindowText)
        End If
      End If

      m_previousDropNode = node
    End Sub

    Private Sub DeleteCurrentFolder()
      Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

      If Not node Is Nothing Then
        Dim folderName As String = String.Empty

        If TypeOf node.Folder Is ZipArchive Then
          ' The name of the "folder" is the name of the zip file
          folderName = (CType(node.Folder, ZipArchive)).ZipFile.FullName
        Else
          folderName = node.Folder.FullName
        End If

        Dim result As DialogResult = MessageBox.Show( _
          "Please confirm you wish to delete the following folder:\n" + folderName, _
          "Deleting folder...", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = System.Windows.Forms.DialogResult.Yes Then
          Try
            If TypeOf node.Folder Is ZipArchive Then
              ' Delete the zip file
              CType(node.Folder, ZipArchive).ZipFile.Delete(m_zipEvents, Nothing)
            Else
              ' Delete the folder
              node.Folder.Delete(m_zipEvents, Nothing)
            End If

            node.Remove()
          Catch except As Exception
            ResultList.Items.Add(except.Message)
          End Try
        End If
      End If
    End Sub

    Private Sub DeleteCurrentFile()
      Dim result As DialogResult = System.Windows.Forms.DialogResult.No

      If FileList.SelectedItems.Count = 1 Then
        Dim item As FileListItem = CType(FileList.SelectedItems(0), FileListItem)

        result = MessageBox.Show( _
          "Please confirm you wish to delete the following file:\n" + item.File.FullName, _
          "Deleting file...", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      ElseIf FileList.SelectedItems.Count > 1 Then
        result = MessageBox.Show( _
          "Please confirm you wish to delete all selected files.", _
          "Deleting files...", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      End If

      If result = System.Windows.Forms.DialogResult.Yes Then
        Dim node As FolderTreeNode = CType(FolderTree.SelectedNode, FolderTreeNode)

        Dim batch As IBatchUpdateable = Nothing
        If TypeOf node.Folder.RootFolder Is IBatchUpdateable Then
          batch = CType(node.Folder.RootFolder, IBatchUpdateable)
        End If

        If Not batch Is Nothing Then
          batch.BeginUpdate(m_zipEvents, Nothing)
        End If

        Try
          Dim item As FileListItem
          For Each item In FileList.SelectedItems
            Try
              item.File.Delete(m_zipEvents, Nothing)

              ' We delete the file entry in the list view
              FileList.Items.Remove(item)
            Catch except As Exception
              ResultList.Items.Add(except.Message)
            End Try
          Next
        Finally
          If Not batch Is Nothing Then
            batch.EndUpdate(m_zipEvents, Nothing)
          End If

          ' We must update the FolderTree since we may have deleted zip files.
          RefreshFolderTree(node)
        End Try
      End If
    End Sub


#End Region

#Region "PRIVATE FIELDS"

    Private m_preventTreeUpdate As Boolean = False
    Private m_previousDropNode As TreeNode = Nothing
    Private m_Clipboard As DataObject = Nothing
    Private m_cutClipboard As Boolean = False
    Private m_zipEvents As ZipEvents = Nothing
    Private m_buildingProgression As Byte = 255
    Private m_currentPassword As String = String.Empty

#End Region

  End Class

End Namespace