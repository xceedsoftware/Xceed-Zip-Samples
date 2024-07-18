'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [SampleUI.vb]
'*
'* This application demonstrate how to use the Xceed Synchronize
'* functionnality.
'*
'* This file is part of Xceed FileSystem for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports SynchronizeUISample.CustomListViewItem
Imports System.IO
Imports Xceed.Synchronization

Namespace SynchronizeUISample
    ''' <summary>
    ''' Class defining the Main Form of the sample
    ''' </summary>
    Partial Public Class SampleUI
        Inherits Form
#Region "PUBLIC CONSTRUCTORS"

        Public Sub New()
            Me.InitializeComponent()
            Me.InitializeSynchronizationEvents()
            Me.InitializeSynchronizationOptions()
        End Sub

#End Region

#Region "PRIVATE EVENT HANDLERS - BUTTONS"

        ''' <summary>
        ''' Event handler for AddFileButton. Add a target item
        ''' in the SlaveItemsListView if the file path is valid.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnAddFileButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddFileButton.Click
            Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
            openFileDialog.Multiselect = True
            openFileDialog.Title = "Select file(s) to process"
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = False

            Dim result As DialogResult = openFileDialog.ShowDialog()

            If result = System.Windows.Forms.DialogResult.OK Then
                Dim selectedFilePaths As String() = openFileDialog.FileNames

                For Each filePath As String In selectedFilePaths
                    Dim file As DiskFile = New DiskFile(filePath)

                    Me.SlaveItemsListView.Items.Add(New FileListViewItem(filePath))
                Next filePath
            End If
        End Sub

        ''' <summary>
        ''' Event handler for AddFolderButton. Add a target item
        ''' in the SlaveItemsListView if the folder path is valid.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnAddFolderButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddFolderButton.Click
            Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
            folderBrowserDialog.Description = "Select folder to use as synchronization target"
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer

            Dim result As DialogResult = folderBrowserDialog.ShowDialog()

            If result = System.Windows.Forms.DialogResult.OK Then
                Dim selectedFolderPath As String = folderBrowserDialog.SelectedPath

                Dim folder As DiskFolder = New DiskFolder(selectedFolderPath)
                If folder.Exists Then
                    If (Not selectedFolderPath.EndsWith(Path.DirectorySeparatorChar.ToString())) Then
                        selectedFolderPath &= Path.DirectorySeparatorChar.ToString()
                    End If

                    Me.SlaveItemsListView.Items.Add(New FolderListViewItem(selectedFolderPath))
                Else
                    MessageBox.Show("Folder " & selectedFolderPath & " does not exist")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Event handler for RemoveButton. Remove the selected
        ''' item from the SlaveItemsListView if the selection is
        ''' valid.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnRemoveButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveButton.Click
            Dim selectedItems As System.Windows.Forms.ListView.SelectedListViewItemCollection = Me.SlaveItemsListView.SelectedItems

            For Each item As ListViewItem In selectedItems
                Me.SlaveItemsListView.Items.Remove(item)
            Next item
        End Sub

        ''' <summary>
        ''' Event handler for BrowseMasterFileButton. Set the selected
        ''' file path as the master item event if the file doesn't exist.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnBrowseMasterFileButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseMasterFilesterFileButton.Click
            Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
            openFileDialog.Multiselect = False
            openFileDialog.CheckFileExists = False
            openFileDialog.CheckPathExists = False
            openFileDialog.Title = "Select master file"

            Dim result As DialogResult = openFileDialog.ShowDialog()

            If result = System.Windows.Forms.DialogResult.OK Then
                ' The master file may not exist. If so, it will be deleted in every target items
                Me.MasterItemTextBox.Text = openFileDialog.FileName
                Me.m_masterItemType = MasterItemType.File
            End If
        End Sub

        ''' <summary>
        ''' Event handler for BrowseMasterFolderButton. Set the selected
        ''' folder path as the master item. The folder must exist.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnBrowseMasterFolderButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BrowseMasterFolderButton.Click
            Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
            folderBrowserDialog.Description = "Select folder to use as master folder"
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer

            Dim result As DialogResult = folderBrowserDialog.ShowDialog()

            If result = System.Windows.Forms.DialogResult.OK Then
                ' The master folder must exist.
                Me.MasterItemTextBox.Text = folderBrowserDialog.SelectedPath
                Me.m_masterItemType = MasterItemType.Folder
            End If
        End Sub

        ''' <summary>
        ''' Event handler for SynchronizeItemsButton. Starts the synchronization
        ''' between the master item and the target items
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnSynchronizeItemsButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SynchonizeItemsButton.Click
            Me.ResetUIControls()
            If (Not Me.ValidateParameters()) Then
                Return
            End If

            ' Create master item depending of its type
            Dim masterItem As FileSystemItem = Nothing
            Select Case m_masterItemType
                Case MasterItemType.File
                    masterItem = New DiskFile(Me.MasterItemTextBox.Text)
                Case MasterItemType.Folder
                    masterItem = New DiskFolder(Me.MasterItemTextBox.Text)
                Case Else
                    Me.AppendMessageToListBox("Unknow MasterItemType for path: " & Me.MasterItemTextBox.Text)
                    Return
            End Select

            Dim itemsList As List(Of FileSystemItem) = New List(Of FileSystemItem)()

            ' Add master item at index 0
            itemsList.Insert(0, masterItem)

            ' Fill the itemsList to pass to Synchronize
            For Each listViewItem As AbstractPathListViewItem In Me.SlaveItemsListView.Items
                If TypeOf listViewItem Is FileListViewItem Then
                    itemsList.Add(New DiskFile(listViewItem.ToString()))
                ElseIf TypeOf listViewItem Is FolderListViewItem Then
                    itemsList.Add(New DiskFolder(listViewItem.ToString()))
                End If
            Next listViewItem

            ' Set synchronization options with parameters defined by user
            Me.m_synchronizationOptions.AutoConflictResolution = Me.AutoConflictResolutionCheckBox.Checked
            Me.m_synchronizationOptions.AllowCreations = Me.AllowCreationsCheckBox.Checked
            Me.m_synchronizationOptions.AllowDeletions = Me.AllowDeletionsCheckBox.Checked
            Me.m_synchronizationOptions.CompareFileData = Me.CompareFileDataCheckBox.Checked
            Me.m_synchronizationOptions.PreviewOnly = Me.PreviewOnlyCheckBox.Checked

            Try
                Me.AbortButton.Enabled = True
                ' Master item is at index 0 of itemsList
                Synchronizer.EasySynchronize(m_synchronizationEvents, m_synchronizationOptions, 0, itemsList.ToArray())
            Catch exception As AbortException
                ' If abort was requested by user
                Me.AppendMessageToListBox("Abort requested, target item may be corrupted and/or unusable")
                Me.AppendMessageToListBox(exception.Message)

                ' Reset progression information
                Me.ProcessProgressBar.Value = 0
            Catch exception As Exception
                ' If abort was requested by user
                If TypeOf exception.InnerException Is AbortException Then
                    Me.AppendMessageToListBox("Abort requested, target item may be corrupted and/or unusable")
                    Me.AppendMessageToListBox(exception.InnerException.Message)

                    ' Reset progression information
                    Me.ProcessProgressBar.Value = 0
                Else
                    Me.AppendMessageToListBox("Exception occured:" & exception.Message)
                End If
            Finally
                Me.AbortButton.Enabled = False
                Me.m_abort = False
            End Try
        End Sub

        ''' <summary>
        ''' Event handler for AbortButton
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnAbortButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AbortButton.Click
            ' Do not display if m_abort already true

            Dim result As DialogResult = MessageBox.Show(Me, "If you abort, the target items will be left" & " in an unknown state. Are you sure you want to cancel?", "Cancel process", MessageBoxButtons.YesNo)

            If (result = Windows.Forms.DialogResult.Yes) Then
                Me.m_abort = True
            End If
        End Sub

#End Region

#Region "PRIVATE EVENT HANDLERS - CHECKBOXES"

        ''' <summary>
        ''' Event handler for CheckBox MouseHover event. Set the
        ''' description of the CheckBox option in the StatusLabel.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnCheckBoxMouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles PreviewOnlyCheckBox.MouseEnter, CompareFileDataCheckBox.MouseEnter, AllowDeletionsCheckBox.MouseEnter, AllowCreationsCheckBox.MouseEnter, AutoConflictResolutionCheckBox.MouseEnter, AutoConflictResolutionCheckBox.MouseHover
            If sender.Equals(Me.AllowCreationsCheckBox) Then
                Me.StatusLabel.Text = "Allow the creation of target items if they don't exist"
            ElseIf sender.Equals(Me.AllowDeletionsCheckBox) Then
                Me.StatusLabel.Text = "Allow the deletion of target items if they no more exist"
            ElseIf sender.Equals(Me.AutoConflictResolutionCheckBox) Then
                Me.StatusLabel.Text = "Allow conflicts to be resolved automatically without any modification on target items"
            ElseIf sender.Equals(Me.CompareFileDataCheckBox) Then
                Me.StatusLabel.Text = "Allow data comparison between synchronized items"
            ElseIf sender.Equals(Me.PreviewOnlyCheckBox) Then
                Me.StatusLabel.Text = "Perfoms only a preview of the synchronization without creating/deleting target items"
            End If
        End Sub

        ''' <summary>
        ''' Event handler for CheckBox MouseLeave event. This clears the
        ''' content of the StatusLabel.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnCheckBoxMouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles PreviewOnlyCheckBox.MouseLeave, CompareFileDataCheckBox.MouseLeave, AllowDeletionsCheckBox.MouseLeave, AllowCreationsCheckBox.MouseLeave, AutoConflictResolutionCheckBox.MouseLeave
            Me.StatusLabel.Text = String.Empty
        End Sub

#End Region

#Region "PRIVATE EVENT HANDLERS - SYNCHRONIZATION EVENTS"

        ''' <summary>
        ''' Event handler for SynchronizationEvents Analysis event. This event
        ''' is triggered before any processing and gives information about the
        ''' actions that will be performed by the synchronizer.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnAnalysis(ByVal sender As Object, ByVal e As SynchronizationAnalysisEventArgs)
            ' Display AnalysisResultDialog only if m_sameActionForAll is not set
            If (Not Me.m_approveAllFileAction) AndAlso Me.NeedActionConfirmation(e) Then

                Dim dialog As AnalysisResultDialog = New AnalysisResultDialog(e)
                Dim result As DialogResult = dialog.ShowDialog(Me)

                Me.m_approveAllFileAction = Me.m_approveAllFileAction Or dialog.ApproveAll

                If Me.m_approveAllFileAction Then
                    Me.m_defaultApproveFileAction = result
                End If

                If result = System.Windows.Forms.DialogResult.OK Then
                    Dim approvedActions As Boolean() = dialog.ApprovedActions

                    ' The master is always the first one and not displayed in the list
                    System.Diagnostics.Debug.Assert(e.Actions.Length = (approvedActions.Length + 1))

                    Dim i As Integer = 1
                    Do While i < e.Actions.Length
                        ' Action not approved at index i
                        If (Not approvedActions(i - 1)) Then
                            e.Actions(i) = SynchronizationAction.None
                        End If
                        i += 1
                    Loop
                Else
                    e.Cancel = True
                    Me.AppendMessageToListBox("Synchronization canceled")
                    Throw New AbortException("Operation aborted")
                End If
            Else
                Select Case m_defaultApproveFileAction
                    Case System.Windows.Forms.DialogResult.OK
                        e.Cancel = False
                    Case Else
                        e.Cancel = True
                End Select

            End If
        End Sub

        ''' <summary>
        ''' Event handler for SynchronizationEvents CompareFileData event. This event is 
        ''' triggered when comparing file data.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnCompareFileData(ByVal sender As Object, ByVal e As SynchronizationCompareFileDataEventArgs)
            If e.ComponentComparesFiles Then
                If e.MasterFile IsNot Nothing Then
                    Me.AppendMessageToListBox(String.Format("Comparing : '{0}' ...", e.MasterFile.FullName))
                End If

                Dim i As Integer = 0
                Do While i < e.FilesToCompare.Length
                    If e.FilesToCompare(i) IsNot Nothing Then
                        Me.AppendMessageToListBox(String.Format(" ... with '{0}'", e.FilesToCompare(i).FullName))
                    End If

                    i = i + 1
                Loop
            End If
        End Sub

        ''' <summary>
        ''' Event handler for SynchronizationEvents Conflict event. This event is 
        ''' triggered when conflict are detected during analysis process.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnConflict(ByVal sender As Object, ByVal e As SynchronizationConflictEventArgs)
            Dim i As Integer = 0
            Do While i < e.Files.Length
                Dim actionStringBuilder As StringBuilder = New StringBuilder()

                If e.ConflictReasons(i) <> SynchronizationConflictReason.NoConflict Then
                    actionStringBuilder.Append("Item: ")
                    actionStringBuilder.AppendLine(e.Files(i).FullName)

                    Select Case e.ConflictReasons(i)
                        Case SynchronizationConflictReason.MoreRecentThanMaster
                            actionStringBuilder.Append("is more recent than master item")
                        Case SynchronizationConflictReason.ModifiedAfterLastSynchronization
                            actionStringBuilder.Append("was modified after last synchronization")
                        Case SynchronizationConflictReason.SameDateAsMasterDifferentData
                            actionStringBuilder.Append("has the same date as the master item, but data differs")
                    End Select

                    actionStringBuilder.AppendLine(", would you like to use it as master?")

                    ' Display MessageBox of the action that will be taken
                    Dim result As DialogResult = MessageBox.Show(Me, actionStringBuilder.ToString(), "Conflict Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)

                    ' Empty and dispose of th actionStringBuilder
                    actionStringBuilder.Length = 0
                    actionStringBuilder = Nothing

                    If result = Windows.Forms.DialogResult.Yes Then
                        e.ChosenMasterFileIndex = i
                    Else
                        e.ChosenMasterFileIndex = e.OriginalMasterFileIndex
                    End If
                End If
                i += 1
            Loop
        End Sub

        ''' <summary>
        ''' Event handler for SynchronizationEvents FolderOperationAnalysis
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnFolderOperationAnalysis(ByVal sender As Object, ByVal e As SynchronizationFolderAnalysisEventArgs)
            ' Display AnalysisResultDialog only if m_approveAllFileAction is not set
            If (Not Me.m_approveAllFileAction) Then
                Dim dialog As AnalysisResultDialog = New AnalysisResultDialog(e)
                Dim result As DialogResult = dialog.ShowDialog(Me)

                Me.m_approveAllFileAction = Me.m_approveAllFileAction Or dialog.ApproveAll

                If Me.m_approveAllFileAction Then
                    m_defaultApproveFileAction = result
                End If

                If result = System.Windows.Forms.DialogResult.OK Then
                    Dim approvedActions As Boolean() = dialog.ApprovedActions

                    ' The master is always the first one and not displayed in the list
                    System.Diagnostics.Debug.Assert(e.Actions.Length = (approvedActions.Length + 1))

                    Dim i As Integer = 1
                    Do While i < e.Actions.Length
                        ' Action not approved at index i
                        If (Not approvedActions(i - 1)) Then
                            e.Actions(i) = SynchronizationAction.None
                        End If
                        i += 1
                    Loop
                End If
            Else
                If m_defaultApproveFileAction <> System.Windows.Forms.DialogResult.OK Then
                    Throw New AbortException("Operation aborted")
                End If
            End If
        End Sub

        ''' <summary>
        ''' Event handler for SynchronizationEvents SynchronizationProgression event.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OnSynchronizationProgression(ByVal sender As Object, ByVal e As SynchronizationProgressionEventArgs)
            If m_abort Then
                Throw New AbortException("Operation aborted")
            End If
            Me.ProcessProgressBar.Value = e.ByteProgression.Percent
            Application.DoEvents()
        End Sub

#End Region

#Region "PRIVATE METHODS"

        ''' <summary>
        ''' Used to display a message in the OuputListBox. It allows to customize
        ''' the way messages are displayed
        ''' </summary>
        ''' <param name="message"></param>
        Private Sub AppendMessageToListBox(ByVal message As String)
            Dim insertedIndex As Integer = Me.OutputListBox.Items.Add(message)
            Me.OutputListBox.SelectedIndex = insertedIndex
        End Sub

        ''' <summary>
        ''' Register to all events of Synchronization Events
        ''' </summary>
        Private Sub InitializeSynchronizationEvents()
            Me.m_synchronizationEvents = New SynchronizationEvents()
            AddHandler m_synchronizationEvents.Analysis, AddressOf OnAnalysis
            AddHandler m_synchronizationEvents.CompareFileData, AddressOf OnCompareFileData
            AddHandler m_synchronizationEvents.Conflict, AddressOf OnConflict
            AddHandler m_synchronizationEvents.FolderOperationAnalysis, AddressOf OnFolderOperationAnalysis
            AddHandler m_synchronizationEvents.SynchronizationProgression, AddressOf OnSynchronizationProgression
        End Sub

        ''' <summary>
        ''' Initialize synchronization options
        ''' </summary>
        Private Sub InitializeSynchronizationOptions()
            Me.m_synchronizationOptions = New SynchronizationOptions()
        End Sub


        ''' <summary>
        ''' Verify that actions or conflicts are present in 
        ''' SynchronizationAnalysisEventArgs. Use to determine
        ''' if the AnalysisResultDialog should be displayed or
        ''' not.
        ''' </summary>
        ''' <param name="e"></param>
        ''' <returns></returns>
        Private Function NeedActionConfirmation(ByVal e As SynchronizationAnalysisEventArgs) As Boolean
            ' Needed by default
            'INSTANT VB NOTE: The local variable needActionConfirmation was renamed since Visual Basic will not allow local variables with the same name as their method:
            Dim needActionConfirmation_Renamed As Boolean = True

            ' No action to be taken if master index is -1
            If e.MasterFileIndex = -1 Then
                Dim actionsMustBeTaken As Boolean = False
                Dim conflictDetected As Boolean = False

                ' Verify if actions have to be performed
                For Each action As SynchronizationAction In e.Actions
                    If action <> SynchronizationAction.None Then
                        actionsMustBeTaken = True
                        Exit For
                    End If
                Next action

                ' Verify if conflicts are detected
                For Each conflict As SynchronizationConflictReason In e.ConflictReasons
                    If conflict <> SynchronizationConflictReason.NoConflict Then
                        conflictDetected = True
                        Exit For
                    End If
                Next conflict

                ' Display the message and close the 
                If (Not actionsMustBeTaken) AndAlso (Not conflictDetected) Then
                    ' Cancel synchronization for this item
                    ' since no action has to be taken
                    ' or conflict were detected
                    e.Cancel = True

                    Application.DoEvents()

                    ' No confirmation needed
                    needActionConfirmation_Renamed = False
                End If
            End If

            Return needActionConfirmation_Renamed
        End Function

        ''' <summary>
        ''' Reset progression parameters and UI
        ''' </summary>
        Private Sub ResetUIControls()
            Me.ProcessProgressBar.Value = 0
            Me.OutputListBox.Items.Clear()
            Me.m_approveAllFileAction = False
            Me.m_defaultApproveFileAction = Windows.Forms.DialogResult.No
        End Sub

        ''' <summary>
        ''' Validates that the minimum needed parameters are defined and
        ''' there values are valid
        ''' </summary>
        ''' <returns>true if the parameters are correctly defined, else false</returns>
        Private Function ValidateParameters() As Boolean
            If String.IsNullOrEmpty(Me.MasterItemTextBox.Text) Then
                Me.AppendMessageToListBox("Master file must be set to synchronize")
                Return False
            End If

            If Me.SlaveItemsListView.Items.Count = 0 Then
                Me.AppendMessageToListBox("You must specify at least one target item before starting synchronization")
                Return False
            End If
            Return True
        End Function

#End Region

#Region "PRIVATE FIELDS"

        Private m_abort As Boolean ' = false;
        Private m_approveAllFileAction As Boolean ' = false;
        Private m_defaultApproveFileAction As DialogResult = Windows.Forms.DialogResult.No
        Private m_masterItemType As MasterItemType = MasterItemType.None
        Private m_synchronizationEvents As SynchronizationEvents ' = null;
        Private m_synchronizationOptions As SynchronizationOptions ' = null;

#End Region
    End Class
End Namespace