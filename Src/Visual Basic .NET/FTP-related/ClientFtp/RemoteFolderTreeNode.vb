Imports System.Collections
Imports System.Windows.Forms
Imports Xceed.Ftp

Public Class RemoteFolderTreeNode
  Inherits TreeNode

  Private Sub New(ByVal parentFullPath As String, ByVal name As String, ByVal client As AsyncFtpClient)
    MyBase.New(name)

    If parentFullPath Is Nothing Then
      Throw New ArgumentNullException("parentFullPath")
    End If

    If name Is Nothing Then
      Throw New ArgumentNullException("name")
    End If

    If client Is Nothing Then
      Throw New ArgumentNullException("client")
    End If

    m_parentFullPath = parentFullPath
    m_client = client

    ' Make sure a [+] appears, using a dummy node.
    Me.Nodes.Add(New TreeNode("retrieving folder contents..."))
  End Sub


  Public Sub New(ByVal client As AsyncFtpClient)
    MyBase.New("root")

    If client Is Nothing Then
      Throw New ArgumentNullException("client")
    End If

    m_client = client

    ' Make sure a [+] appears, using a dummy node.
    Me.Nodes.Add(New TreeNode("retrieving folder contents..."))
  End Sub


  Public Sub SelectFolder(ByVal contents As ListView, ByVal refresh As Boolean)
    If contents Is Nothing Then
      Throw New ArgumentNullException("contents")
    End If

    contents.Enabled = False
    Me.TreeView.Enabled = False

    If (refresh) Or (m_items Is Nothing) Then
      Me.Refresh(contents)
    Else
      contents.Items.Clear()

      Dim item As FtpItemInfo

      For Each item In m_items
        contents.Items.Add(New RemoteListViewItem(item))
      Next item

      Try
        m_client.BeginChangeCurrentFolder(m_fullPath, New AsyncCallback(AddressOf Me.SelectFolderCompleted), contents)
      Catch except As Exception
        MessageBox.Show(except.Message, "Error")

        contents.Enabled = True
        Me.TreeView.Enabled = True
      End Try
    End If
  End Sub


  Private Sub SelectFolderCompleted(ByVal result As IAsyncResult)
    Dim contents As ListView = result.AsyncState

    Try
      m_client.EndChangeCurrentFolder(result)
    Catch except As Exception
      MessageBox.Show(except.Message, "Error")
    Finally
      contents.Enabled = True
      Me.TreeView.Enabled = True
    End Try
  End Sub


  Public Overloads Sub Refresh()
    Me.refresh(Nothing)
  End Sub

  Public Overloads Sub Refresh(ByVal contents As ListView)
    ' contents may be null.
    m_contents = contents

    ' Since we are modifying our parent treeview in an async fashion, 
    ' we disable it until completed to avoid user iteraction.
    Me.TreeView.Enabled = False

    ' This will update our internal list which we use in FillList.
    m_items = Nothing

    Try
      ' We must always return to the current folder, except if we have
      ' a listview to fill, which means we also get selected. If we don't
      ' know our parent's fullpath, it means we are the root folder and
      ' the current folder is our folder!
      If (m_contents Is Nothing) _
      Or (m_fullPath Is Nothing And m_parentFullPath Is Nothing) Then
        m_client.BeginGetCurrentFolder( _
          New AsyncCallback(AddressOf Me.GetFolderCompleted), _
          Nothing)
      Else
        ' The first time we get refreshed, we must determine our full path.
        If m_fullPath Is Nothing Then
          ' We must go into our parent folder, then change into ourself.
          m_client.BeginChangeCurrentFolder( _
            m_parentFullPath, _
            New AsyncCallback(AddressOf Me.ChangeParentFolderCompleted), _
            Nothing)
        Else
          ' Go directly in our folder.
          m_client.BeginChangeCurrentFolder( _
            m_fullPath, _
            New AsyncCallback(AddressOf Me.ChangeFolderCompleted), _
            Nothing)
        End If
      End If
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' Remove dummy child node.
      Me.Nodes.Clear()

      ' Enable controls
      Me.TreeView.Enabled = True

      If Not (m_contents Is Nothing) Then
        m_contents.Items.Clear()
        m_contents.Enabled = True
        m_contents = Nothing
      End If
    End Try
  End Sub


  Private Sub GetFolderCompleted(ByVal result As IAsyncResult)
    Try
      Dim currentFolder As String = m_client.EndGetCurrentFolder(result)

      If (m_fullPath Is Nothing) And (m_parentFullPath Is Nothing) Then
        m_fullPath = currentFolder

        ' Update the root label.
        Me.Text = m_fullPath
      End If

      If m_fullPath Is Nothing Then
        If currentFolder = m_parentFullPath Then
          ' We can change into ourself right away!
          m_client.BeginChangeCurrentFolder( _
            Me.Text, _
            New AsyncCallback(AddressOf Me.ChangeFolderCompleted), _
            currentFolder)
        Else
          ' We must go into our parent folder, then change into ourself.
          m_client.BeginChangeCurrentFolder( _
            m_parentFullPath, _
            New AsyncCallback(AddressOf Me.ChangeParentFolderCompleted), _
            currentFolder)
        End If
      ElseIf m_fullPath = currentFolder Then
        ' We can get the contents right away, and we don't need to
        ' pass currentFolder as the state, since we will end up in the
        ' same folder.
        m_client.BeginGetFolderContents( _
          New AsyncCallback(AddressOf Me.GetContentsCompleted), _
          Nothing)
      Else
        ' Go directly in our folder.
        m_client.BeginChangeCurrentFolder( _
          m_fullPath, _
          New AsyncCallback(AddressOf Me.ChangeFolderCompleted), _
          currentFolder)
      End If
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' Remove dummy child node.
      Me.Nodes.Clear()

      ' Enable treeview
      Me.TreeView.Enabled = True

      If Not (m_contents Is Nothing) Then
        m_contents.Items.Clear()
        m_contents.Enabled = True
        m_contents = Nothing
      End If
    End Try
  End Sub


  Private Sub ChangeParentFolderCompleted(ByVal result As IAsyncResult)
    Try
      m_client.EndChangeCurrentFolder(result)

      m_client.BeginChangeCurrentFolder( _
        Me.Text, _
        New AsyncCallback(AddressOf Me.ChangeFolderCompleted), _
        result.AsyncState)
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' Remove dummy child node.
      Me.Nodes.Clear()

      ' Go back into original folder
      Dim currentFolder As String = result.AsyncState

      If currentFolder Is Nothing Then
        ' Enable treeview
        Me.TreeView.Enabled = True
      Else
        Try
          m_client.BeginChangeCurrentFolder( _
            currentFolder, _
            New AsyncCallback(AddressOf Me.ChangeBackCurrentFolderCompleted), _
            Nothing)
        Catch
          Me.TreeView.Enabled = True
        End Try
      End If

      If Not (m_contents Is Nothing) Then
        m_contents.Items.Clear()
        m_contents.Enabled = True
        m_contents = Nothing
      End If
    End Try
  End Sub


  Private Sub ChangeFolderCompleted(ByVal result As IAsyncResult)
    Try
      m_client.EndChangeCurrentFolder(result)

      If m_fullPath Is Nothing Then
        ' We update our own full path the first time we get into it.
        m_client.BeginGetCurrentFolder( _
          New AsyncCallback(AddressOf Me.GetFullPathCompleted), _
          result.AsyncState)
      Else
        m_client.BeginGetFolderContents( _
          New AsyncCallback(AddressOf Me.GetContentsCompleted), _
          result.AsyncState)
      End If
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' Remove dummy child node.
      Me.Nodes.Clear()

      ' Go back into original folder
      Dim currentFolder As String = result.AsyncState

      If currentFolder Is Nothing Then
        ' Enable treeview
        Me.TreeView.Enabled = True
      Else
        Try
          m_client.BeginChangeCurrentFolder( _
            currentFolder, _
            New AsyncCallback(AddressOf Me.ChangeBackCurrentFolderCompleted), _
            Nothing)
        Catch
          Me.TreeView.Enabled = True
        End Try
      End If

      If Not (m_contents Is Nothing) Then
        m_contents.Items.Clear()
        m_contents.Enabled = True
        m_contents = Nothing
      End If
    End Try
  End Sub


  Private Sub GetFullPathCompleted(ByVal result As IAsyncResult)
    Try
      m_fullPath = m_client.EndGetCurrentFolder(result)

      m_client.BeginGetFolderContents( _
        New AsyncCallback(AddressOf Me.GetContentsCompleted), _
        result.AsyncState)
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

      ' Remove dummy child node.
      Me.Nodes.Clear()

      ' Go back into original folder
      Dim currentFolder As String = result.AsyncState

      If currentFolder Is Nothing Then
        ' Enable treeview
        Me.TreeView.Enabled = True
      Else
        Try
          m_client.BeginChangeCurrentFolder( _
            currentFolder, _
            New AsyncCallback(AddressOf Me.ChangeBackCurrentFolderCompleted), _
            Nothing)
        Catch
          Me.TreeView.Enabled = True
        End Try
      End If

      If Not (m_contents Is Nothing) Then
        m_contents.Items.Clear()
        m_contents.Enabled = True
        m_contents = Nothing
      End If
    End Try
  End Sub


  Private Sub GetContentsCompleted(ByVal result As IAsyncResult)
    Dim currentFolder As String = result.AsyncState

    Try
      Try
        m_items = m_client.EndGetFolderContents(result)
      Finally
        ' Remove dummy child node.
        Me.Nodes.Clear()

        If Not (m_items Is Nothing) Then
          Dim item As FtpItemInfo

          For Each item In m_items
            If item.Type = FtpItemType.Folder Or item.Type = FtpItemType.Link Then
              Me.Nodes.Add(New RemoteFolderTreeNode(m_fullPath, item.Name, m_client))
            End If
          Next item

          If Not (m_contents Is Nothing) Then
            Me.SelectFolder(m_contents, False)

            ' Since this changes the current folder to m_fullPath, we
            ' don't have to return to "currentFolder". It will also
            ' enable back the treeview.
            currentFolder = Nothing

            m_contents = Nothing
          Else
            ' We must enable back the treeview.
            Me.TreeView.Enabled = True
          End If
        Else
          If Not (m_contents Is Nothing) Then
            ' We can't call Select, since we would loop infinitely.
            m_contents.Items.Clear()
            m_contents.Enabled = True
            m_contents = Nothing
          End If

          If currentFolder Is Nothing Then
            ' We must enable back the treeview.
            Me.TreeView.Enabled = True
          End If
        End If
      End Try
    Catch except As Exception
      MessageBox.Show("An error occured while updating the contents of " + Me.Text + "." + ControlChars.Lf + ControlChars.Lf + ControlChars.Lf + except.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    Finally
      If Not (currentFolder Is Nothing) Then
        Try
          m_client.BeginChangeCurrentFolder( _
            currentFolder, _
            New AsyncCallback(AddressOf Me.ChangeBackCurrentFolderCompleted), _
            Nothing)
        Catch
          Me.TreeView.Enabled = True
        End Try
      End If
    End Try
  End Sub


  Private Sub ChangeBackCurrentFolderCompleted(ByVal result As IAsyncResult)
    Try
      m_client.EndChangeCurrentFolder(result)
    Catch
    Finally
      Me.TreeView.Enabled = True
    End Try
  End Sub


  Public Shared Function FormatSize(ByVal size As Long) As String
    ' Format the received size to a readable format.
    Dim formattedSize As String = String.Empty

    ' Formats the size in bytes.
    If Size = 0 Then
      formattedSize = Size.ToString("n0") + " byte"
    Else
      formattedSize = Size.ToString("n0") + " bytes"
    End If

    ' Format the size in kilobytes. (only if the size is at least 1 kb)
    If Size >= 1024 Then
      Size = Size / 1024

      formattedSize = Size.ToString("n0") + " KB"
    End If

    Return formattedSize
  End Function


  Private m_client As AsyncFtpClient = Nothing
  Private m_parentFullPath As String = Nothing
  Private m_fullPath As String = Nothing

  Private m_items As FtpItemInfoList = Nothing

  Private m_contents As ListView = Nothing
End Class
