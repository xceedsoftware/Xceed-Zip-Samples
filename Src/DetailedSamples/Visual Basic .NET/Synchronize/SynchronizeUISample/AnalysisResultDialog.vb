'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [AnalysisResultDialog.vb]
'*
'* This application demonstrate how to use the Xceed Synchronize
'* functionnality.
'*
'* This file is part of Xceed FileSystem for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.'


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Xceed.Synchronization

Namespace SynchronizeUISample
    ''' <summary>
    ''' Class defining a Dialog displaying the actions to be
    ''' taken on synchronized items. This dialog allows approving
    ''' or not actions to be performed by synchronization.
    ''' </summary>
    Partial Public Class AnalysisResultDialog
        Inherits Form
#Region "PUBLIC CONSTRUCTORS"

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="synchronizationAnalysisEventArgs">The SynchronizationAnalysisEventArgs containing 
        ''' informations about the current synchronization process</param>
        Public Sub New(ByVal synchronizationAnalysisEventArgs As SynchronizationAnalysisEventArgs)
            InitializeComponent()
            AnalyseSynchronizationEventArgs(synchronizationAnalysisEventArgs)
        End Sub

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="synchronizationFolderAnalysisEventArgs">The SynchronizationFolderAnalysisEventArgs containing 
        ''' informations about the current synchronization process</param>
        Public Sub New(ByVal synchronizationFolderAnalysisEventArgs As SynchronizationFolderAnalysisEventArgs)
            InitializeComponent()
            AnalyseSynchronizationFolderAnalysisEventArgs(synchronizationFolderAnalysisEventArgs)
        End Sub

#End Region

#Region "PUBLIC FIELDS"

        ''' <summary>
        ''' A bool array defining if action at the 
        ''' specified index must be taken or not
        ''' </summary>
        Public Property ApprovedActions() As Boolean()
            Get
                Return m_approvedActions
            End Get
            Set(ByVal value As Boolean())
                m_approvedActions = value
            End Set
        End Property

        ''' <summary>
        ''' Defines if all the future synchronization analysis should be accepted without
        ''' displaying the AnalysisResultDialog
        ''' </summary>
        Public Property ApproveAll() As Boolean
            Get
                Return m_sameActionForAll
            End Get
            Set(ByVal value As Boolean)
                m_sameActionForAll = value
            End Set
        End Property

#End Region

#Region "PRIVATE METHODS"

        ''' <summary>
        ''' Fill the AnalysisListView with items synchronize
        ''' displaying: 
        ''' -the source item (if needed)
        ''' -the action to perform
        ''' -the destination item
        ''' </summary>
        Private Sub AnalyseSynchronizationEventArgs(ByVal synchronizationAnalysisEventArgs As SynchronizationAnalysisEventArgs)
            If synchronizationAnalysisEventArgs Is Nothing Then
                Throw New Exception("SynchronizationEventArgs must exist")
            End If

            ' Get master item index
            Dim masterIndex As Integer = synchronizationAnalysisEventArgs.MasterFileIndex

            Dim i As Integer = 0
            Do While i < synchronizationAnalysisEventArgs.Files.Length
                ' Do not consider master file in action
                If i = synchronizationAnalysisEventArgs.MasterFileIndex Then
                    i += 1
                    Continue Do
                End If

                ' Add every item to synchronize
                Dim insertedItem As ListViewItem = Me.AnalysisListView.Items.Add(New AnalysisListViewItem(synchronizationAnalysisEventArgs.Files(masterIndex).FullName, synchronizationAnalysisEventArgs.Actions(i), synchronizationAnalysisEventArgs.Files(i).FullName, True))
                i += 1
            Loop
        End Sub

        ''' <summary>
        ''' Fill the AnalysisListView with items synchronize
        ''' displaying: 
        ''' -the source item (if needed)
        ''' -the action to perform
        ''' -the destination item
        ''' </summary>
        Private Sub AnalyseSynchronizationFolderAnalysisEventArgs(ByVal synchronizationFolderAnalysisEventArgs As SynchronizationFolderAnalysisEventArgs)
            If synchronizationFolderAnalysisEventArgs Is Nothing Then
                Throw New Exception("SynchronizationFolderAnalysisEventArgs must exist")
            End If

            ' Get master item index
            Dim masterIndex As Integer = synchronizationFolderAnalysisEventArgs.MasterFolderIndex

            Dim i As Integer = 0
            Do While i < synchronizationFolderAnalysisEventArgs.Folders.Length
                ' Do not consider master file in action
                If i = synchronizationFolderAnalysisEventArgs.MasterFolderIndex Then
                    i += 1
                    Continue Do
                End If

                ' Add every item to synchronize
                Dim insertedItem As ListViewItem = Me.AnalysisListView.Items.Add(New AnalysisListViewItem(synchronizationFolderAnalysisEventArgs.Folders(masterIndex).FullName, synchronizationFolderAnalysisEventArgs.Actions(i), synchronizationFolderAnalysisEventArgs.Folders(i).FullName, True))
                i += 1
            Loop
        End Sub

#End Region

#Region "PRIVATE EVENT HANDLERS"

        ''' <summary>
        ''' Event handler for OkButton
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub OkButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles OkButton.Click
            FillApprovedActionsArray()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        ''' <summary>
        ''' Set true every index selected in AnalysisListView in ApprovedActions array
        ''' </summary>
        Private Sub FillApprovedActionsArray()
            Dim checkedIndexCollection As ListView.CheckedIndexCollection = Me.AnalysisListView.CheckedIndices

            Me.m_approvedActions = New Boolean(Me.AnalysisListView.Items.Count - 1) {}
            For Each index As Integer In checkedIndexCollection
                Me.m_approvedActions(index) = True
            Next index
        End Sub

        ''' <summary>
        ''' Event handler for ApproveAllCheckBox. This changes the m_sameActionForAll private
        ''' field to avoid to be notified of future analysis events.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub ApproveAllCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AllSameActionCheckBox.CheckedChanged
            m_sameActionForAll = Me.AllSameActionCheckBox.Checked
        End Sub

#End Region

#Region "PRIVATE FIELDS"

        Private m_approvedActions As Boolean()
        Private m_sameActionForAll As Boolean = False

#End Region
    End Class
End Namespace