'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [FileListViewItem.vb]
 '*
 '* Implementation of the AbstractListViewItem class and represent an AbstractFile.
 '* It expose methods to controls the action the user can make on this item. (copy, paste, delete...)
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.Zip
Imports Xceed.GZip
Imports Xceed.Tar

Namespace Xceed.FileSystem.Samples.Utils.ListView
  Public Class FileListViewItem : Inherits AbstractListViewItem
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal fileItem As AbstractFile)
      Me.New(fileItem, String.Empty)
    End Sub

    Public Sub New(ByVal fileItem As AbstractFile, ByVal displayText As String)
      If fileItem Is Nothing Then
        Throw New ArgumentNullException("file")
      End If

      m_item = fileItem
      m_displayText = displayText

      Me.RefreshIcon(False)
      Me.FillData()
    End Sub

#End Region     ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    ''' <summary>
    ''' Gets the internal FileSystemItem object as an AbstractFile.
    ''' </summary>
    Public ReadOnly Property File() As AbstractFile
      Get
        Return CType(IIf(TypeOf m_item Is AbstractFile, m_item, Nothing), AbstractFile)
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "PUBLIC METHODS"

    ''' <summary>
    ''' Delete the item physically and remove it from the FolderTree if applicable.
    ''' </summary>
    ''' <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    Public Overrides Sub Delete(ByVal confirmDelete As Boolean)
      ' Ensure that the attributes are up-to-date.
      m_item.Refresh()

      Dim itemIsReadonly As Boolean = ((m_item.Attributes And System.IO.FileAttributes.ReadOnly) = System.IO.FileAttributes.ReadOnly)

      ' Confirm deletion.
      If confirmDelete Then
        Dim message As String = String.Format("Are you sure you want to delete '{0}'?", m_item.Name)
        Dim caption As String = "Confirm File Delete"

        If itemIsReadonly Then
          message = String.Format("The file '{0}' is a read-only file. Are you sure you want to delete it?", m_item.Name)
        End If

        If MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.No Then
          Return
        End If
      End If

      ' Delete the physical item if existing.
      If m_item.Exists Then
        If itemIsReadonly Then
          m_item.Attributes = m_item.Attributes And Not System.IO.FileAttributes.ReadOnly
        End If

        Dim deleteEvents As FileSystemEvents = New FileSystemEvents()
        AddHandler deleteEvents.ItemException, AddressOf Delete_ItemException

        m_item.Delete(deleteEvents, Nothing)

        RemoveHandler deleteEvents.ItemException, AddressOf Delete_ItemException
      End If

      ' Remove ourself from the list.
      Me.Remove()
    End Sub

    ''' <summary>
    ''' Refresh the current item with this new FileSystemItem. A refresh to the matching
    ''' FolderTree node will be made if applicable.
    ''' </summary>
    ''' <param name="item">The new FileSystemItem that this item represent.</param>
    Public Overrides Sub Refresh(ByVal item As FileSystemItem)
      If item Is Nothing Then
        Throw New ArgumentNullException("item")
      End If

      If CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile) Is Nothing Then
        Throw New ArgumentException("The item must be an AbstractFile.", "item")
      End If

      m_item = item

      Me.FillData()

      Me.RefreshIcon(True)
    End Sub

    ''' <summary>
    ''' Rename the item physically and the matching FolderTree node if applicable.
    ''' </summary>
    ''' <param name="name">The new name for the item.</param>
    Public Overrides Sub Rename(ByVal name As String)
      If name Is Nothing Then
        Throw New ArgumentNullException("name")
      End If

      Dim originalName As String = m_item.Name

      If name <> originalName Then
        ' Rename the object.
        m_item.Name = name

        ' Make sure the text is ok.
        If Me.Text <> name Then
          Me.Text = name
        End If

        ' Refresh the icon.
        Me.RefreshIcon(True)
      End If
    End Sub

    #End Region ' PUBLIC METHODS

    #Region "FILESYSTEM EVENTS"

    Private Sub Delete_ItemException(ByVal sender As Object, ByVal e As ItemExceptionEventArgs)
      ' The item was probably deleted by some other application. We just skip this one then.
      If Not CType(IIf(TypeOf e.Exception Is ItemDoesNotExistException, e.Exception, Nothing), ItemDoesNotExistException) Is Nothing Then
        e.Action = ItemExceptionAction.Ignore
        Return
      End If

      ' At this point, we always want to delete read only items.
      If Not CType(IIf(TypeOf e.Exception Is ItemIsReadOnlyException, e.Exception, Nothing), ItemIsReadOnlyException) Is Nothing Then
        e.CurrentItem.Attributes = e.CurrentItem.Attributes And Not FileAttributes.ReadOnly
        e.Action = ItemExceptionAction.Retry
        Return
      End If

      ' Zipped file can be password protected. We need to ask the user to input that password.
      If Not CType(IIf(TypeOf e.Exception Is Xceed.Zip.InvalidDecryptionPasswordException, e.Exception, Nothing), Xceed.Zip.InvalidDecryptionPasswordException) Is Nothing Then
        Dim passwordForm As InputPasswordForm = New InputPasswordForm
        Try
          If passwordForm.ShowDialog(Nothing, e.CurrentItem.FullName) = System.Windows.Forms.DialogResult.OK Then
            If (Not CType(IIf(TypeOf e.CurrentItem Is ZippedFile, e.CurrentItem, Nothing), ZippedFile) Is Nothing) OrElse (CType(Not IIf(TypeOf e.CurrentItem Is ZippedFolder, e.CurrentItem, Nothing), ZippedFolder) Is Nothing) Then
              Dim archive As ZipArchive = CType(IIf(TypeOf e.CurrentItem.RootFolder Is ZipArchive, e.CurrentItem.RootFolder, Nothing), ZipArchive)

              If Not archive Is Nothing Then
                archive.DefaultDecryptionPassword = passwordForm.Password
                e.Action = ItemExceptionAction.Retry
                Return
              End If

              ' Set the last decryption password used for future use.
              Options.ZipLastDecryptionPasswordUsed = passwordForm.Password
            End If
          End If
        Finally
          CType(passwordForm, IDisposable).Dispose()
        End Try
      End If

      ' For any other exceptions, we show an error message and wait for the user action.
      Dim message As String = String.Format("An error occured while deleting the item {0}.", e.CurrentItem.FullName)

      Dim result As DialogResult = MessageBox.Show(message, "Error Deleting Item...", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error)

      Select Case result
        Case DialogResult.Abort
          e.Action = ItemExceptionAction.Abort

        Case DialogResult.Retry
          e.Action = ItemExceptionAction.Retry

        Case DialogResult.Ignore
          e.Action = ItemExceptionAction.Ignore
      End Select
    End Sub

    #End Region ' FILESYSTEM EVENTS

    #Region "PROTECTED METHODS"

    Protected Overrides Sub FillData()
      Me.SubItems.Clear()

      ' Name
      Dim name As String = m_item.Name

      If m_displayText.Length > 0 Then
        name = m_displayText
      End If

      Me.Text = name

      ' Size
      Dim formattedSize As String = Me.GetFormattedSize()
      Me.SubItems.Add(formattedSize)

      ' CompressedSize
      Dim formattedcompressedSize As String = Me.GetFormattedCompressedSize()
      Me.SubItems.Add(formattedcompressedSize)

      ' Type
      Dim type As String = m_item.GetType().ToString()
      Me.SubItems.Add(type)

      ' Modification date
      Dim modifiedDateFormatted As String = "N/A"

      If m_item.HasLastWriteDateTime Then
        modifiedDateFormatted = m_item.LastWriteDateTime.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern & " " & DateTimeFormatInfo.CurrentInfo.ShortTimePattern)
      End If

      Me.SubItems.Add(modifiedDateFormatted)

      ' Encryption (ZIP)
      Dim zippedFile As ZippedFile = CType(IIf(TypeOf m_item Is ZippedFile, m_item, Nothing), ZippedFile)

      If (Not zippedFile Is Nothing) AndAlso (zippedFile.Encrypted) Then
        Me.ForeColor = System.Drawing.Color.Green
      Else
        Me.ForeColor = System.Drawing.SystemColors.WindowText
      End If
    End Sub

    Protected Overrides Function GetFormattedCompressedSize() As String
      If m_item.RootFolder.HostFile Is Nothing Then
        Return String.Empty
      End If

      If TypeOf m_item Is ZippedFile Then
        Return Me.GetZipCompressedSize()
      Else If TypeOf m_item Is GZippedFile Then
        Return Me.GetGZipCompressedSize()
      Else If TypeOf m_item Is TarredFile Then
        Return Me.GetTarCompressedSize()
      Else
        Return "0 KB"
      End If
    End Function

    Protected Overrides Function GetFormattedSize() As String
      Dim formattedSize As String = "0 KB"

      ' KB
      If m_item.Exists AndAlso Me.File.Size > 0 Then
        formattedSize = (CLng((Me.File.Size + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return formattedSize
    End Function

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE METHODS"

    Private Function GetZipCompressedSize() As String
      Dim zippedFile As ZippedFile = CType(IIf(TypeOf m_item Is ZippedFile, m_item, Nothing), ZippedFile)

      zippedFile.Refresh()

      ' KB
      If zippedFile.Exists AndAlso zippedFile.CompressedSize > 0 Then
        Return (CLng((zippedFile.CompressedSize + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return "0 KB"
    End Function

    Private Function GetGZipCompressedSize() As String
      ' GZip does not expose CompressedSize.
      Return "N/A"
    End Function

    Private Function GetTarCompressedSize() As String
      Dim tarredFile As TarredFile = CType(IIf(TypeOf m_item Is TarredFile, m_item, Nothing), TarredFile)

      tarredFile.Refresh()

      ' KB
      If tarredFile.Exists AndAlso tarredFile.Size > 0 Then
        Return (CLng((tarredFile.Size + 1023) / 1024)).ToString("#,##0") & " KB"
      End If

      Return "0 KB"
    End Function

    #End Region ' PRIVATE METHODS
  End Class
End Namespace
