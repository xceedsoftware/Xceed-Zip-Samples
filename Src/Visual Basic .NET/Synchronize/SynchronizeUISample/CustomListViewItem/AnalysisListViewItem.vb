'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [AnalsisListViewItem.vb]
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
Imports System.Text
Imports System.Windows.Forms
Imports Xceed.Synchronization

Namespace SynchronizeUISample
    ''' <summary>
    ''' Defines a ListViewItem to display informations about AnalysisSynchronizationEventhArgs
    ''' </summary>
    Public Class AnalysisListViewItem
        Inherits ListViewItem
#Region "PUBLIC CONSTRUCTORS"

        '/// <summary>
        '/// Constructor
        '/// </summary>
        '/// <param name="masterItemPath">The path of the master item</param>
        '/// <param name="action">The action to process for synchronization</param>
        '/// <param name="itemPath">The path of the target item</param>
        '/// <param name="selected">Defines if the item is selected or not</param>
        'public AnalysisListViewItem( string masterItemPath, string action, string itemPath, bool selected )
        '{
        '  this.Text = masterItemPath;
        '  this.SubItems.Add( action );
        '  this.SubItems.Add( itemPath );
        '  this.Checked = selected;
        '}

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="masterItemPath">The path of the master item</param>
        ''' <param name="action">The action to process for synchronization</param>
        ''' <param name="itemPath">The path of the target item</param>
        ''' <param name="selected">Defines if the item is selected or not</param>
        Public Sub New(ByVal masterItemPath As String, ByVal action As SynchronizationAction, ByVal itemPath As String, ByVal selected As Boolean)
            Me.Checked = selected

            ' Get action for current item
            Dim sourceItem As String = String.Empty
            Dim actionItem As String = String.Empty

            Select Case action
                Case SynchronizationAction.SuspendedCreateOrOverwrite
                    sourceItem = masterItemPath
                    actionItem = "should be used to create"
                Case SynchronizationAction.OverwriteAttributesOnly
                    sourceItem = masterItemPath
                    actionItem = "used to overwrite attributes"
                Case SynchronizationAction.CreateOrOverwriteWithMaster
                    sourceItem = masterItemPath
                    actionItem = "will replace"
                Case SynchronizationAction.Delete
                    actionItem = "delete"
                Case SynchronizationAction.SuspendedDelete
                    actionItem = "should delete"
            End Select

            Me.Text = sourceItem
            Me.SubItems.Add(actionItem)
            Me.SubItems.Add(itemPath)
        End Sub

#End Region
    End Class
End Namespace
