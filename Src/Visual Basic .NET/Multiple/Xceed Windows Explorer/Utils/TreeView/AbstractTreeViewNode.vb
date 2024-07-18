'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [AbstractTreeViewNode.vb]
 '*
 '* Custom TreeNode exposing method to controls the action the user
 '* can make on an item. (copy, paste, delete...)
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
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public MustInherit Class AbstractTreeViewNode : Inherits TreeNode
    #Region "CONSTRUCTORS"

    Protected Overrides Sub Finalize()
      If Not m_iconUpdater Is Nothing Then
        m_iconUpdater.StopUpdate()
      End If
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    ''' <summary>
    ''' Gets the AbstractFolder this TreeNode represent.
    ''' </summary>
    Public MustOverride ReadOnly Property Folder() As AbstractFolder

    ''' <summary>
    ''' Gets the FileSystemItem this TreeNode represent.
    ''' </summary>
    Public Overridable ReadOnly Property Item() As FileSystemItem
      Get
        Return m_item
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "New" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property NewToolEnabled() As Boolean
      Get
        Return True
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "New Ftp connection" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property NewFtpConnectionToolEnabled() As Boolean
      Get
        Return False
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Paste" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property PasteToolEnabled() As Boolean
      Get
        Return True
      End Get
    End Property

    ''' <summary>
    ''' Determine if the "Up" function should be enabled.
    ''' </summary>
    Public Overridable ReadOnly Property UpToolEnabled() As Boolean
      Get
        Return (Not Me.Parent Is Nothing)
      End Get
    End Property

    ''' <summary>
    ''' Gets the root node of this node.
    ''' </summary>
    Public ReadOnly Property RootNode() As AbstractTreeViewNode
      Get
        If Not Me.Parent Is Nothing Then
          Dim parentNode As AbstractTreeViewNode = CType(Me.Parent, AbstractTreeViewNode)
          Return parentNode.RootNode
        End If

        Return Me
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    Public Overridable Sub InitializeFolder()
      ' Let implementation do the job.
    End Sub

    ''' <summary>
    ''' Refresh the current item with this new FileSystemItem. 
    ''' </summary>
    ''' <param name="item">The new FileSystemItem that this item represent.</param>
    Public Overridable Sub Refresh(ByVal item As FileSystemItem)
      If item Is Nothing Then
        Throw New ArgumentNullException("item")
      End If

      m_item = item

      Me.RefreshText()
      Me.RefreshIcon(True)
    End Sub

    ''' <summary>
    ''' Refresh the icon.
    ''' </summary>
    ''' <param name="forceRefresh">Determine if we force a cache refresh of the icon.</param>
    Public Sub RefreshIcon(ByVal forceRefresh As Boolean)
      ' Get the indexes from the cache.
      Dim index As Integer = IconCache.GetIconIndex(m_item, False, forceRefresh)
      Dim selectedIndex As Integer = IconCache.GetIconIndex(m_item, True, forceRefresh)

      ' Update the icon indexes of the node.
      If Not Me.TreeView Is Nothing AndAlso Me.TreeView.InvokeRequired Then
        Me.TreeView.Invoke(New RefreshIconIndexDelegate(AddressOf Me.RefreshIconIndex), New Object() {index, selectedIndex})
      Else
        Me.RefreshIconIndex(index, selectedIndex)
      End If
    End Sub

    ''' <summary>
    ''' Refresh the icons of the node's children.
    ''' </summary>
    Public Sub RefreshIcons()
      ' To avoid multiple refresh process running at the same time, we stop
      ' any current refresh process and start a new one.
      m_iconUpdater.StopUpdate()
      m_iconUpdater.StartUpdate()
    End Sub

#End Region     ' PUBLIC METHODS

    #Region "PROTECTED DELEGATES"

    ''' <summary>
    ''' Delegate for the RefreshIconIndex method when it is called from a dirrefent thread.
    ''' </summary>
    Protected Delegate Sub RefreshIconIndexDelegate(ByVal index As Integer, ByVal selectedIndex As Integer)

    #End Region ' PROTECTED DELEGATES

    #Region "PROTECTED METHODS"

    ''' <summary>
    ''' Change the icon index of the item.
    ''' </summary>
    ''' <param name="index">The new icon index representing the normal state.</param>
    ''' <param name="selectedIndex">The new icon index representing the selected state.</param>
    Protected Sub RefreshIconIndex(ByVal index As Integer, ByVal selectedIndex As Integer)
      If index > -1 Then
        Me.ImageIndex = index
        Me.SelectedImageIndex = index ' In case there is no special icon for the selected state.
      End If

      If selectedIndex > -1 Then
        Me.SelectedImageIndex = selectedIndex
      End If
    End Sub

    ''' <summary>
    ''' Refresh the display text of the node.
    ''' </summary>
    Protected Overridable Sub RefreshText()
      Dim folder As AbstractFolder = CType(IIf(TypeOf m_item Is AbstractFolder, m_item, Nothing), AbstractFolder)

      If m_displayText.Length > 0 Then
        Me.Text = m_displayText
      Else If Not folder Is Nothing Then
        Me.Text = (IIf(folder.IsRoot, folder.FullName, folder.Name))
      Else
        Me.Text = m_item.Name
      End If

      ' Encryption (ZIP)
      Dim zippedFile As ZippedFile = CType(IIf(TypeOf m_item Is ZippedFile, m_item, Nothing), ZippedFile)

      If (Not zippedFile Is Nothing) AndAlso (zippedFile.Encrypted) Then
        Me.ForeColor = System.Drawing.Color.Green
      Else
        Me.ForeColor = System.Drawing.SystemColors.WindowText
      End If
    End Sub

    #End Region ' PROTECTED METHODS

    #Region "PROTECTED FIELDS"

    Protected m_item As FileSystemItem '= null
    Protected m_displayText As String = String.Empty
    Protected m_iconUpdater As TreeViewIconUpdater '= null
    Protected m_contentListView As System.Windows.Forms.ListView '= null

    #End Region ' PROTECTED FIELDS
  End Class
End Namespace