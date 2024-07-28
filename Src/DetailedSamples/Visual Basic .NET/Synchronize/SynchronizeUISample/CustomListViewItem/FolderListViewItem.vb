'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [FolderListViewItem.vb]
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
Imports System.IO

Namespace SynchronizeUISample.CustomListViewItem
    ''' <summary>
    ''' Defines a ListViewItem to display informations about a folder
    ''' </summary>
    Public Class FolderListViewItem
        Inherits AbstractPathListViewItem
#Region "PUBLIC CONSTRUCTORS"

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        ''' <param name="folderPath">The full path of the folder</param>
        Public Sub New(ByVal folderPath As String)
            MyBase.New(folderPath, 0)
            Me.Text = Path.GetDirectoryName(Me.m_fullPath)
            Me.SubItems.Add(Me.m_fullPath)
        End Sub

#End Region
    End Class
End Namespace
