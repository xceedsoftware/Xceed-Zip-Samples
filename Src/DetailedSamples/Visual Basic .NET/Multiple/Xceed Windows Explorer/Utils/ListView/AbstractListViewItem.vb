'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [AbstractListViewItem.vb]
 '*
 '* Abstract ListViewItem that expose a FileSystemItem and properties
 '* to control the main toolbar's actions. It also allow to manipulate the
 '* item. (copy, paste, delete, rename, etc.)
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.FileSystem.Samples.Utils.Icons
Imports Xceed.FileSystem.Samples.Utils.TreeView

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public MustInherit Class AbstractListViewItem : Inherits ListViewItem
    #Region "PUBLIC PROPERTIES"

    Public Overridable ReadOnly Property ParentNode() As AbstractTreeViewNode
      Get
        Return Nothing
      End Get
    End Property

    ''' <summary>
    ''' The FileSystemItem this ListViewItem represent.
    ''' </summary>
    Public ReadOnly Property FileSystemItem() As FileSystemItem
      Get
        Return m_item
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Copy" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property CopyToolEnabled() As Boolean
      Get
        Dim folder As AbstractFolder = CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)

        ' Copy is available for a file. 
        If folder Is Nothing Then
          Return True
        End If

        ' Copy is available for folder if they are 
        ' not the root folder.
        Return ((Not folder.IsRoot))
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Cut" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property CutToolEnabled() As Boolean
      Get
        Dim folder As AbstractFolder = CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)

        ' Cut is available for a file. 
        If folder Is Nothing Then
          Return True
        End If

        ' Cut is available for folder if they are 
        ' not the root folder.
        Return ((Not folder.IsRoot))
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Delete" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property DeleteToolEnabled() As Boolean
      Get
        Dim folder As AbstractFolder = CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)

        ' Delete is available for a file. 
        If folder Is Nothing Then
          Return True
        End If

        ' Cut is available for folder if they are 
        ' not the root folder.
        Return ((Not folder.IsRoot))
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Rename" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property RenameToolEnabled() As Boolean
      Get
        Dim folder As AbstractFolder = CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)

        ' Rename is available for a file. 
        If folder Is Nothing Then
          Return True
        End If

        ' Rename is available for folder if they are 
        ' not the root folder.
        Return ((Not folder.IsRoot))
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Create the item physically and add it to the FolderTree if applicable.
    ''' </summary>
    Public Overridable Sub Create()
      m_item.Create()
    End Sub

    ''' <summary>
    ''' Delete the item physically and remove it from the FolderTree if applicable.
    ''' </summary>
    ''' <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    Public MustOverride Sub Delete(ByVal confirmDelete As Boolean)

    ''' <summary>
    ''' Refresh the current item with this new FileSystemItem. A refresh to the matching
    ''' FolderTree node will be made if applicable.
    ''' </summary>
    ''' <param name="item">The new FileSystemItem that this item represent.</param>
    Public MustOverride Sub Refresh(ByVal item As FileSystemItem)

    ''' <summary>
    ''' Rename the item physically and the matching FolderTree node if applicable.
    ''' </summary>
    ''' <param name="name">The new name for the item.</param>
    Public MustOverride Sub Rename(ByVal name As String)

    ''' <summary>
    ''' Refresh the icon.
    ''' </summary>
    ''' <param name="forceRefresh">Determine if we force a cache refresh of the icon.</param>
    Public Sub RefreshIcon(ByVal forceRefresh As Boolean)
      ' Set the item icon index. 
      Dim index As Integer = IconCache.GetIconIndex(m_item, False, forceRefresh)

      If Not Me.ListView Is Nothing AndAlso Me.ListView.InvokeRequired Then
        Me.ListView.Invoke(New RefreshIconIndexDelegate(AddressOf Me.RefreshIconIndex), New Object(){ index })
      Else
        Me.RefreshIconIndex(index)
      End If
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "PROTECTED DELEGATES"

    ''' <summary>
    ''' Delegate for the RefreshIconIndex method when it is called from a dirrefent thread.
    ''' </summary>
    Protected Delegate Sub RefreshIconIndexDelegate(ByVal index As Integer)

    #End Region ' PROTECTED DELEGATES

    #Region "PROTECTED METHODS"

    ''' <summary>
    ''' Set the item and subitems text.
    ''' </summary>
    Protected MustOverride Sub FillData()

    ''' <summary>
    ''' Return a string representing the formatted compressed size.
    ''' </summary>
    Protected MustOverride Function GetFormattedCompressedSize() As String

    ''' <summary>
    ''' Return a string representing the formatted size.
    ''' </summary>
    Protected MustOverride Function GetFormattedSize() As String

    ''' <summary>
    ''' Change the icon index of the item.
    ''' </summary>
    ''' <param name="index">The new icon index.</param>
    Protected Sub RefreshIconIndex(ByVal index As Integer)
      If index > -1 Then
        Me.ImageIndex = index
      End If
    End Sub

    #End Region ' PROTECTED METHODS

    #Region "PROTECTED FIELDS"

    Protected m_item As FileSystemItem '= null
    Protected m_displayText As String = String.Empty

    #End Region ' PROTECTED FIELDS
  End Class
End Namespace
