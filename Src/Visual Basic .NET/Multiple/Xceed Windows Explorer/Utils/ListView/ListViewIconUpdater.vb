'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [ListViewIconUpdater.vb]
 '*
 '* Class use to update the icons in a ListView on a seperate thread.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Threading
Imports System.Windows.Forms

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class ListViewIconUpdater
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal listView As System.Windows.Forms.ListView)
      If listView Is Nothing Then
        Throw New ArgumentNullException("listView")
      End If

      m_listView = listView
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Start refreshing icons.
    ''' </summary>
    Public Sub StartUpdate()
      m_stopUpdate = False

      ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf Me.UpdateListView))
    End Sub

    ''' <summary>
    ''' Stop the current refresh process.
    ''' </summary>
    Public Sub StopUpdate()
      m_stopUpdate = True
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "PRIVATE METHODS"

    Private Sub UpdateListView(ByVal stateInfo As Object)
      Dim itemsCount As Integer = m_listView.Items.Count

      Dim i As Integer = 0
      Do While i < itemsCount
        If m_stopUpdate OrElse m_listView.Items.Count <> itemsCount Then
          Exit Do
        End If

        Try
          Dim item As AbstractListViewItem = CType(IIf(TypeOf m_listView.Items(i) Is AbstractListViewItem, m_listView.Items(i), Nothing), AbstractListViewItem)

          If Not item Is Nothing Then
            item.RefreshIcon(True)
          End If
        Catch
        End Try
        i += 1
      Loop
    End Sub

    #End Region ' PRIVATE METHODS

    #Region "PRIVATE FIELDS"

    Private m_listView As System.Windows.Forms.ListView '= null
    Private m_stopUpdate As Boolean '= false

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
