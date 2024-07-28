'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [AbstractPathListView.vb]
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

Namespace SynchronizeUISample.CustomListViewItem
    ''' <summary>
    ''' Base class for FolderListViewItem and FileListViewItem
    ''' </summary>
    Public MustInherit Class AbstractPathListViewItem
        Inherits ListViewItem
#Region "PUBLIC CONTRUCTORS"

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        ''' <param name="path">The full path of the item</param>
        ''' <param name="imageIndex">The index of the image to use for this ListViewItem</param>
        Public Sub New(ByVal path As String, ByVal imageIndex As Integer)
            MyBase.New()
            Me.m_fullPath = path
            Me.ImageIndex = imageIndex
        End Sub

#End Region

#Region "PUBLIC OVERRIDES"

        ''' <summary>
        ''' Display the full path of the item
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return m_fullPath
        End Function

#End Region

#Region "PROTECTED FIELDS"

        Protected m_fullPath As String = String.Empty

#End Region
    End Class
End Namespace
