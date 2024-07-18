'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [TreeViewIconUpdater.vb]
 '*
 '* Class use to update the icons of a node's children on a seperate thread.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Threading

Namespace Xceed.FileSystem.Samples.Utils.TreeView
  Public Class TreeViewIconUpdater
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal node As AbstractTreeViewNode)
      If node Is Nothing Then
        Throw New ArgumentNullException("node")
      End If

      m_node = node
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Start refreshing icons.
    ''' </summary>
    Public Sub StartUpdate()
      m_stopUpdate = False

      ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.UpdateTreeViewNode))
    End Sub

    ''' <summary>
    ''' Stop the current refresh process.
    ''' </summary>
    Public Sub StopUpdate()
      m_stopUpdate = True
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "PRIVATE METHODS"

    Private Sub UpdateTreeViewNode(ByVal stateInfo As Object)
      Dim nodesCount As Integer = m_node.Nodes.Count

      Dim i As Integer = 0
      Do While i < nodesCount
        If m_stopUpdate OrElse m_node.Nodes.Count <> nodesCount Then
          Exit Do
        End If

        Try
          Dim node As AbstractTreeViewNode = CType(IIf(TypeOf m_node.Nodes(i) Is AbstractTreeViewNode, m_node.Nodes(i), Nothing), AbstractTreeViewNode)

          If Not node Is Nothing Then
            node.RefreshIcon(True)
          End If
        Catch
        End Try
        i += 1
      Loop
    End Sub

    #End Region ' PRIVATE METHODS

    #Region "PRIVATE FIELDS"

    Private m_node As AbstractTreeViewNode '= null
    Private m_stopUpdate As Boolean '= false

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
