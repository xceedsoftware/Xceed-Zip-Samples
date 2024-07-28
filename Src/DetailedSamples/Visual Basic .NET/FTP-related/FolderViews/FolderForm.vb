Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Xceed.FileSystem

Namespace FolderViews
  Public Class FolderForm
    Inherits System.Windows.Forms.Form

    Private splitter1 As System.Windows.Forms.Splitter
    Private folderTreeView As System.Windows.Forms.TreeView
    Private fileListView As System.Windows.Forms.ListView
    Private filenameColumn As System.Windows.Forms.ColumnHeader
    Private sizeColumn As System.Windows.Forms.ColumnHeader
    Private lastModifiedColumn As System.Windows.Forms.ColumnHeader
    Private mainMenu1 As System.Windows.Forms.MainMenu
    Private menuFile As System.Windows.Forms.MenuItem
    Private menuFileClose As System.Windows.Forms.MenuItem
    Private menuItem1 As System.Windows.Forms.MenuItem
    Private menuDelete As System.Windows.Forms.MenuItem
    Private menuCreateFolder As System.Windows.Forms.MenuItem
    Private menuCut As System.Windows.Forms.MenuItem
    Private menuCopy As System.Windows.Forms.MenuItem
    Private menuPaste As System.Windows.Forms.MenuItem
    Private menuItem2 As System.Windows.Forms.MenuItem
    Private components As System.ComponentModel.Container = Nothing

    Public Sub New()
      InitializeComponent()
    End Sub

    Public Sub New(ByVal title As String, ByVal ParamArray folders() As AbstractFolder)
      InitializeComponent()

      Me.Text = title

      Dim folder As AbstractFolder
      For Each folder In folders
        folderTreeView.Nodes.Add(New FolderTreeNode(folder))
      Next folder
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		  If (Disposing) Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
        MyBase.Dispose(disposing)
      End If
    End Sub

#Region "Windows Form Designer generated code"
    Private Sub InitializeComponent()
      Me.folderTreeView = New System.Windows.Forms.TreeView
      Me.splitter1 = New System.Windows.Forms.Splitter
      Me.fileListView = New System.Windows.Forms.ListView
      Me.filenameColumn = New System.Windows.Forms.ColumnHeader
      Me.sizeColumn = New System.Windows.Forms.ColumnHeader
      Me.lastModifiedColumn = New System.Windows.Forms.ColumnHeader
      Me.mainMenu1 = New System.Windows.Forms.MainMenu
      Me.menuFile = New System.Windows.Forms.MenuItem
      Me.menuFileClose = New System.Windows.Forms.MenuItem
      Me.menuItem1 = New System.Windows.Forms.MenuItem
      Me.menuCut = New System.Windows.Forms.MenuItem
      Me.menuCopy = New System.Windows.Forms.MenuItem
      Me.menuPaste = New System.Windows.Forms.MenuItem
      Me.menuItem2 = New System.Windows.Forms.MenuItem
      Me.menuCreateFolder = New System.Windows.Forms.MenuItem
      Me.menuDelete = New System.Windows.Forms.MenuItem
      Me.SuspendLayout()
      ' 
      ' folderTreeView
      ' 
      Me.folderTreeView.AllowDrop = True
      Me.folderTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.folderTreeView.Dock = System.Windows.Forms.DockStyle.Left
      Me.folderTreeView.FullRowSelect = True
      Me.folderTreeView.HideSelection = False
      Me.folderTreeView.HotTracking = True
      Me.folderTreeView.ImageIndex = -1
      Me.folderTreeView.Location = New System.Drawing.Point(0, 0)
      Me.folderTreeView.Name = "folderTreeView"
      Me.folderTreeView.SelectedImageIndex = -1
      Me.folderTreeView.Size = New System.Drawing.Size(168, 330)
      Me.folderTreeView.TabIndex = 0
      AddHandler Me.folderTreeView.DragOver, AddressOf Me.Any_DragOver
      AddHandler Me.folderTreeView.AfterSelect, AddressOf Me.folderTreeView_AfterSelect
      AddHandler Me.folderTreeView.BeforeExpand, AddressOf Me.folderTreeView_BeforeExpand
      AddHandler Me.folderTreeView.ItemDrag, AddressOf Me.folderTreeView_ItemDrag
      AddHandler Me.folderTreeView.DragDrop, AddressOf Me.Any_DragDrop
      ' 
      ' splitter1
      ' 
      Me.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption
      Me.splitter1.Location = New System.Drawing.Point(168, 0)
      Me.splitter1.Name = "splitter1"
      Me.splitter1.Size = New System.Drawing.Size(3, 330)
      Me.splitter1.TabIndex = 2
      Me.splitter1.TabStop = False
      ' 
      ' fileListView
      ' 
      Me.fileListView.AllowDrop = True
      Me.fileListView.BorderStyle = System.Windows.Forms.BorderStyle.None
      Me.fileListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.filenameColumn, Me.sizeColumn, Me.lastModifiedColumn})
      Me.fileListView.Dock = System.Windows.Forms.DockStyle.Fill
      Me.fileListView.FullRowSelect = True
      Me.fileListView.HideSelection = False
      Me.fileListView.Location = New System.Drawing.Point(171, 0)
      Me.fileListView.Name = "fileListView"
      Me.fileListView.Size = New System.Drawing.Size(421, 330)
      Me.fileListView.TabIndex = 3
      Me.fileListView.View = System.Windows.Forms.View.Details
      AddHandler Me.fileListView.DragOver, AddressOf Me.Any_DragOver
      AddHandler Me.fileListView.DragDrop, AddressOf Me.Any_DragDrop
      AddHandler Me.fileListView.ItemDrag, AddressOf Me.fileListView_ItemDrag
      ' 
      ' filenameColumn
      ' 
      Me.filenameColumn.Text = "Filename"
      Me.filenameColumn.Width = 180
      ' 
      ' sizeColumn
      ' 
      Me.sizeColumn.Text = "Size"
      Me.sizeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      Me.sizeColumn.Width = 80
      ' 
      ' lastModifiedColumn
      ' 
      Me.lastModifiedColumn.Text = "Last modified"
      Me.lastModifiedColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
      Me.lastModifiedColumn.Width = 140
      ' 
      ' mainMenu1 
      Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuItem1})
      ' 
      ' menuFile
      ' 
      Me.menuFile.Index = 0
      Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFileClose})
      Me.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems
      Me.menuFile.Text = "&File"
      ' 
      ' menuFileClose
      ' 
      Me.menuFileClose.Index = 0
      Me.menuFileClose.MergeOrder = 3
      Me.menuFileClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4
      Me.menuFileClose.Text = "&Close folder view"
      AddHandler Me.menuFileClose.Click, AddressOf Me.menuFileClose_Click
      ' 
      ' menuItem1
      ' 
      Me.menuItem1.Index = 1
      Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCut, Me.menuCopy, Me.menuPaste, Me.menuItem2, Me.menuCreateFolder, Me.menuDelete})
      Me.menuItem1.MergeOrder = 10
      Me.menuItem1.MergeType = System.Windows.Forms.MenuMerge.MergeItems
      Me.menuItem1.Text = "&Edit"
      ' 
      ' menuCut
      ' 
      Me.menuCut.Index = 0
      Me.menuCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX
      Me.menuCut.Text = "Cu&t"
      AddHandler Me.menuCut.Click, AddressOf Me.menuCut_Click
      ' 
      ' menuCopy
      ' 
      Me.menuCopy.Index = 1
      Me.menuCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC
      Me.menuCopy.Text = "&Copy"
      AddHandler Me.menuCopy.Click, AddressOf Me.menuCopy_Click
      ' 
      ' menuPaste
      ' 
      Me.menuPaste.Index = 2
      Me.menuPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV
      Me.menuPaste.Text = "&Paste"
      AddHandler Me.menuPaste.Click, AddressOf Me.menuPaste_Click
      ' 
      ' menuItem2
      ' 
      Me.menuItem2.Index = 3
      Me.menuItem2.Text = "-"
      ' 
      ' menuCreateFolder
      ' 
      Me.menuCreateFolder.Index = 4
      Me.menuCreateFolder.MergeOrder = 11
      Me.menuCreateFolder.Shortcut = System.Windows.Forms.Shortcut.Ins
      Me.menuCreateFolder.Text = "&Create folder..."
      AddHandler Me.menuCreateFolder.Click, AddressOf Me.menuCreateFolder_Click
      ' 
      ' menuDelete
      ' 
      Me.menuDelete.Index = 5
      Me.menuDelete.MergeOrder = 11
      Me.menuDelete.Shortcut = System.Windows.Forms.Shortcut.Del
      Me.menuDelete.Text = "&Delete"
      AddHandler Me.menuDelete.Click, AddressOf Me.menuDelete_Click
      ' 
      ' FolderForm
      ' 
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(592, 330)
      Me.Controls.Add(Me.fileListView)
      Me.Controls.Add(Me.splitter1)
      Me.Controls.Add(Me.folderTreeView)
      Me.Menu = Me.mainMenu1
      Me.Name = "FolderForm"
      AddHandler Me.Load, AddressOf Me.FolderForm_Load
      Me.ResumeLayout(False)
    End Sub
#End Region

    ' Can't use Clipboard, as items are not serializable.
    Private Shared mg_clipboard As New DataObject
    Private Shared mg_clipboardAction As DragDropEffects = DragDropEffects.None

    Private m_events As New FileSystemEvents

    Private ReadOnly Property MainForm() As MainForm
      Get
        Return Me.ParentForm
      End Get
    End Property

    Public Sub UpdateFileList()
      If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
        Dim selected As FolderTreeNode = folderTreeView.SelectedNode

        If selected Is Nothing Then
          fileListView.Items.Clear()
        Else
          selected.OnAfterSelect(fileListView)
        End If
      End If
    End Sub

    Private Sub folderTreeView_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs)
      If TypeOf e.Node Is FolderTreeNode Then
        Dim node As FolderTreeNode = e.Node

        If node Is Nothing Then
          System.Diagnostics.Debug.Fail("Unexpected node type expanded.")
          e.Node.Nodes.Clear()
        Else
          node.OnBeforeExpand()
        End If
      End If
    End Sub

    Private Sub folderTreeView_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs)
      If TypeOf e.Node Is FolderTreeNode Then
        Dim node As FolderTreeNode = e.Node

        If Not node Is Nothing Then
          node.OnAfterSelect(fileListView)
        End If
      End If
    End Sub

    Private Sub FolderForm_Load(ByVal sender As Object, ByVal e As System.EventArgs)
      AddHandler m_events.ByteProgression, AddressOf Me.m_events_ByteProgression
      If (folderTreeView.Nodes.Count > 0) Then
        folderTreeView.SelectedNode = folderTreeView.Nodes(0)
      End If
    End Sub

    Private Sub folderTreeView_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs)
      If TypeOf e.Item Is FolderTreeNode Then
        Dim node As FolderTreeNode = e.Item

        If Not node Is Nothing Then
          DoDragDrop(node, DragDropEffects.Copy Or DragDropEffects.Move)
        End If
      End If
    End Sub

    Private Sub fileListView_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs)
      ' We don't drag e.Item, but all selected items.
      If fileListView.SelectedItems.Count > 0 Then
        Dim files(fileListView.SelectedItems.Count) As FileListViewItem

        Dim index As Integer
        For index = 0 To fileListView.SelectedItems.Count - 1
          If TypeOf fileListView.SelectedItems(index) Is FileListViewItem Then
            files(index) = fileListView.SelectedItems(index)
          End If
        Next index

        DoDragDrop(files, DragDropEffects.Copy Or DragDropEffects.Move)
      End If
    End Sub

    Private Sub Any_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
      e.Effect = DragDropEffects.None

      If e.Data.GetDataPresent(GetType(FolderTreeNode)) Or e.Data.GetDataPresent(GetType(FileListViewItem())) Then
        If (e.AllowedEffect And DragDropEffects.Move) = DragDropEffects.Move And (e.KeyState And 4 + 8 + 32) = 4 Then
          ' Shift + drag
          e.Effect = DragDropEffects.Move
        ElseIf (e.AllowedEffect And DragDropEffects.Copy) = DragDropEffects.Copy Then
          e.Effect = DragDropEffects.Copy
        End If
      End If
    End Sub

    Private Sub Any_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
      ' We copy in the currently selected folder.
      If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
        Dim selected As FolderTreeNode = folderTreeView.SelectedNode

        If Not selected Is Nothing Then
          Me.CopyData(e.Data, e.Effect, selected)
        End If
      End If
    End Sub

    Private Sub menuFileClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      Me.Close()
    End Sub

    Private Sub menuCreateFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
        Dim selected As FolderTreeNode = folderTreeView.SelectedNode

        If Not selected Is Nothing Then
          If Not selected.IsExpanded Then
            selected.Expand()
          End If
          Dim newFolder As New TreeNode("new folder")

          selected.Nodes.Add(newFolder)
          selected.TreeView.LabelEdit = True
          AddHandler selected.TreeView.AfterLabelEdit, AddressOf Me.CreateFolder_AfterLabelEdit

          newFolder.BeginEdit()
        End If
      End If
    End Sub

    Private Sub CreateFolder_AfterLabelEdit(ByVal sender As Object, ByVal e As NodeLabelEditEventArgs)
      If TypeOf e.Node.Parent Is FolderTreeNode Then
        Dim parent As FolderTreeNode = e.Node.Parent

        ' We unadvise for this event and remove this dummy node.
        e.Node.TreeView.LabelEdit = False
        RemoveHandler e.Node.TreeView.AfterLabelEdit, AddressOf Me.CreateFolder_AfterLabelEdit
        e.Node.Remove()

        ' Then check if we need to create an actual folder.
        If (Not parent Is Nothing) And (Not e.CancelEdit) Then
          Dim newFolder As AbstractFolder = parent.Folder.CreateFolder(e.Label)
          Dim newNode As New FolderTreeNode(newFolder)

          parent.Nodes.Add(newNode)
          newNode.EnsureVisible()
          newNode.TreeView.SelectedNode = newNode
        End If
      End If
    End Sub

    Private Sub menuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      ' I don't like that, but heck...
      If fileListView.Focused Then
        Dim answer As DialogResult = System.Windows.Forms.DialogResult.No

        If fileListView.SelectedItems.Count = 1 Then
          answer = MessageBox.Show("Are you sure you wish to delete file " + fileListView.SelectedItems(0).Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        ElseIf fileListView.SelectedItems.Count > 1 Then
          answer = MessageBox.Show("Are you sure you wish to delete selected " + fileListView.SelectedItems.Count.ToString() + "files?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If

        If answer = System.Windows.Forms.DialogResult.Yes Then
          Dim item As FileListViewItem
          For Each item In fileListView.SelectedItems
            Try
              item.File.Delete()
              item.Remove()
            Catch except As Exception
              Me.MainForm.DisplayInformation(except.Message)
            End Try
          Next item
        End If
      Else
        If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
          Dim selected As FolderTreeNode = folderTreeView.SelectedNode

          If Not selected Is Nothing Then
            If (System.Windows.Forms.DialogResult.Yes = MessageBox.Show("Are you sure you wish to delete folder """ + selected.Folder.FullName + """?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) Then
              Try
                selected.Folder.Delete()
                selected.Remove()
              Catch except As Exception
                Me.MainForm.DisplayInformation(except.Message)
              End Try
            End If
          End If
        End If
      End If
    End Sub

    Private Sub menuCut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      If fileListView.Focused Then
        Dim files(fileListView.SelectedItems.Count) As FileListViewItem

        Dim index As Integer
        For index = 0 To fileListView.SelectedItems.Count - 1
          If TypeOf fileListView.SelectedItems(index) Is FileListViewItem Then
            files(index) = fileListView.SelectedItems(index)
          End If
        Next index

        mg_clipboard.SetData(files)
        mg_clipboardAction = DragDropEffects.Move
      Else
        If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
          Dim selected As FolderTreeNode = folderTreeView.SelectedNode

          If Not selected Is Nothing Then
            mg_clipboard.SetData(selected)
            mg_clipboardAction = DragDropEffects.Move
          End If
        End If
      End If
    End Sub

    Private Sub menuCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      If fileListView.Focused Then
        Dim files(fileListView.SelectedItems.Count) As FileListViewItem

        Dim index As Integer
        For index = 0 To fileListView.SelectedItems.Count - 1
          If TypeOf fileListView.SelectedItems(index) Is FileListViewItem Then
            files(index) = fileListView.SelectedItems(index)
          End If
        Next index

        mg_clipboard.SetData(files)
        mg_clipboardAction = DragDropEffects.Copy
      Else
        If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
          Dim selected As FolderTreeNode = folderTreeView.SelectedNode

          If Not selected Is Nothing Then
            mg_clipboard.SetData(selected)
            mg_clipboardAction = DragDropEffects.Copy
          End If
        End If
      End If
    End Sub

    Private Sub menuPaste_Click(ByVal sender As Object, ByVal e As System.EventArgs)
      If TypeOf folderTreeView.SelectedNode Is FolderTreeNode Then
        Dim selected As FolderTreeNode = folderTreeView.SelectedNode

        If Not selected Is Nothing Then
          Me.CopyData(mg_clipboard, mg_clipboardAction, selected)
        End If
      End If
    End Sub

    Private Sub CopyData(ByVal data As IDataObject, ByVal action As DragDropEffects, ByVal destination As FolderTreeNode)
      If data.GetDataPresent(GetType(FolderTreeNode)) Then
        If TypeOf data.GetData(GetType(FolderTreeNode)) Is FolderTreeNode Then
          Dim source As FolderTreeNode = data.GetData(GetType(FolderTreeNode))

          If Not source Is Nothing Then
            Me.Cursor = Cursors.WaitCursor

            Try
              If action = DragDropEffects.Copy Then
                Me.MainForm.DisplayAction("Copying " + source.Folder.FullName)
                source.Folder.CopyTo(m_events, Nothing, destination.Folder, True)
              Else
                System.Diagnostics.Debug.Assert(action = DragDropEffects.Move)

                Me.MainForm.DisplayAction("Moving " + source.Folder.FullName)
                source.Folder.MoveTo(m_events, Nothing, destination.Folder, True)

                source.Remove()
              End If

            Catch except As Exception
              Me.MainForm.DisplayInformation(except.Message)
            Finally
              Me.MainForm.DisplayAction(String.Empty)
              Me.Cursor = Cursors.Default
            End Try

            destination.Update(True)
            destination.FolderForm.UpdateFileList()
          End If
        End If
      Else
        If TypeOf data.GetData(GetType(FileListViewItem())) Is FileListViewItem() Then
          Dim files As FileListViewItem() = data.GetData(GetType(FileListViewItem()))

          If (Not files Is Nothing) And (files.Length > 0) Then
            Me.Cursor = Cursors.WaitCursor

            Try
              If action = DragDropEffects.Copy Then
                Dim file As FileListViewItem
                For Each file In files
                  Try
                    Me.MainForm.DisplayAction("Copying " + file.File.FullName)
                    file.File.CopyTo(m_events, Nothing, destination.Folder, True)
                  Catch except As Exception
                    Me.MainForm.DisplayInformation(except.Message)
                  End Try
                Next file
              Else
                System.Diagnostics.Debug.Assert(action = DragDropEffects.Move)

                ' We assume each file has the same root folder.              
                Dim file As FileListViewItem
                For Each file In files
                  Try
                    Me.MainForm.DisplayAction("Moving " + file.File.FullName)
                    file.File.MoveTo(m_events, Nothing, destination.Folder, True)
                    file.Remove()
                  Catch except As Exception
                    Me.MainForm.DisplayInformation(except.Message)
                  End Try
                Next file
              End If
              '# TODO: I would prefer refreshing the parent folder node.              
            Catch except As Exception
              Me.MainForm.DisplayInformation(except.Message)
            Finally
              Me.MainForm.DisplayAction(String.Empty)
              Me.Cursor = Cursors.Default
            End Try

            destination.Update(True)
            destination.FolderForm.UpdateFileList()
          End If
        End If
      End If
    End Sub

    Private Sub m_events_ByteProgression(ByVal sender As Object, ByVal e As ByteProgressionEventArgs)
      Me.MainForm.DisplayProgress(e.AllFilesBytes.Percent)
    End Sub
  End Class
End namespace
