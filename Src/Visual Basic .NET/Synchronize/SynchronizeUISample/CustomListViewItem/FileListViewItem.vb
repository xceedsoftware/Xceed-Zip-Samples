'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [FileListViewItem.vb]
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
    ''' Defines a ListViewItem to display informations about a file
    ''' </summary>
    Public Class FileListViewItem
        Inherits AbstractPathListViewItem
#Region "PUBLIC CONSTRUCTORS"

        ''' <summary>
        ''' Default constructor
        ''' </summary>
        ''' <param name="filePath">The full path of the file</param>
        Public Sub New(ByVal filePath As String)
            MyBase.New(filePath, 1)
            Me.Text = Path.GetFileName(Me.m_fullPath)
            Me.SubItems.Add(Me.m_fullPath)
        End Sub

#End Region
    End Class
End Namespace
