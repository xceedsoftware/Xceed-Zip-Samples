Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Xceed.FileSystem
Imports Xceed.Zip
Imports Xceed.FileSystem.Samples.Utils.Icons

Namespace Xceed.FileSystem.Samples
  Public Class PropertyForm : Inherits System.Windows.Forms.Form
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal item As FileSystemItem)
      If item Is Nothing Then
        Throw New ArgumentNullException("item")
      End If

      m_item = item

      InitializeComponent()

      ' Toggle custom panel.
      Me.ToggleCustomPanels()

      ' Enable options.
      Me.EnableOptions()

      ' Initialize the values.
      Me.InitializeValues()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PRIVATE METHODS"

    Private Sub EnableOptions()
      Dim item As FileSystemItem = m_item

      If (Not item.HostFile Is Nothing) AndAlso (item.RootFolder Is item) Then
        item = item.HostFile
      End If

      ' Atributes
      SystemAttributeCheck.Enabled = item.HasAttributes
      HiddenAttributeCheck.Enabled = item.HasAttributes
      ReadOnlyAttributeCheck.Enabled = item.HasAttributes
      ArchiveAttributeCheck.Enabled = item.HasAttributes

      ' Name
      NameTextBox.ReadOnly = (item.RootFolder Is item)
    End Sub

    Private Sub InitializeValues()
      Dim item As FileSystemItem = m_item

      ' Are we working with the archive file itself?
      If (Not item.HostFile Is Nothing) AndAlso (item.RootFolder Is item) Then
        item = item.HostFile
      End If

      ' Name
      NameTextBox.Text = item.Name

      If item.RootFolder Is item Then
        NameTextBox.Text = item.FullName
      End If

      ' Sizes
      Dim file As AbstractFile = CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile)

      If Not file Is Nothing Then
        SizeLabel.Text = Me.GetFormattedSize(file.Size)

        Dim zippedFile As ZippedFile = CType(IIf(TypeOf item Is ZippedFile, item, Nothing), ZippedFile)

        If Not zippedFile Is Nothing Then
          CompressedSizeLabel.Text = Me.GetFormattedSize(zippedFile.CompressedSize)
        Else
          CompressedSizeLabel.Text = "N/A"
        End If
      Else
        SizeLabel.Text = "N/A"
        CompressedSizeLabel.Text = "N/A"
      End If

      ' Creation date
      CreationDateTimeLabel.Text = "N/A"

      If item.HasCreationDateTime Then
        CreationDateTimeLabel.Text = item.CreationDateTime.ToString()
      End If

      ' Modification date
      ModificationDateTimeLabel.Text = "N/A"

      If item.HasLastWriteDateTime Then
        ModificationDateTimeLabel.Text = item.LastWriteDateTime.ToString()
      End If

      ' Last access date
      LastAccessDateTimeLabel.Text = "N/A"

      If item.HasLastAccessDateTime Then
        LastAccessDateTimeLabel.Text = item.LastAccessDateTime.ToString()
      End If

      ' Attributes
      If item.HasAttributes Then
        HiddenAttributeCheck.Checked = ((item.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden)
        SystemAttributeCheck.Checked = ((item.Attributes And FileAttributes.System) = FileAttributes.System)
        ReadOnlyAttributeCheck.Checked = ((item.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly)
        ArchiveAttributeCheck.Checked = ((item.Attributes And FileAttributes.Archive) = FileAttributes.Archive)
      End If

      ' Zip comments
      If TypeOf item Is ZippedFile Then
        CommentsText.Text = (CType(item, ZippedFile)).Comment
      Else If TypeOf item Is ZippedFolder Then
        CommentsText.Text = (CType(item, ZippedFolder)).Comment
      Else If TypeOf m_item Is ZippedFolder Then
        CommentsText.Text = (CType(m_item, ZippedFolder)).Comment
      End If
    End Sub

    Private Sub SaveValues()
      Dim item As FileSystemItem = m_item

      ' Are we working with the archive file itself?
      If (Not item.HostFile Is Nothing) AndAlso (item.RootFolder Is item) Then
        item = item.HostFile
      End If

      ' To avoid rebuilding archives for nothing, we will using BatchUpdate on the root.
      Dim autoBatchUpdateObject As AutoBatchUpdate = New AutoBatchUpdate(item.RootFolder)
      Try
        ' Name
        If (Not item.RootFolder Is item) AndAlso (item.Name <> NameTextBox.Text) Then
          item.Name = NameTextBox.Text
        End If

        ' Attributes
        If item.HasAttributes Then
          ' System
          If SystemAttributeCheck.Checked Then
            ' Add the attribute if not already set.
            If (item.Attributes And FileAttributes.System) <> FileAttributes.System Then
              item.Attributes = item.Attributes Or FileAttributes.System
            End If
          Else
            ' Remove the attribute if already set.
            If (item.Attributes And FileAttributes.System) = FileAttributes.System Then
              item.Attributes = item.Attributes And Not FileAttributes.System
            End If
          End If

          ' Read-only
          If ReadOnlyAttributeCheck.Checked Then
            ' Add the attribute if not already set.
            If (item.Attributes And FileAttributes.ReadOnly) <> FileAttributes.ReadOnly Then
              item.Attributes = item.Attributes Or FileAttributes.ReadOnly
            End If
          Else
            ' Remove the attribute if already set.
            If (item.Attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
              item.Attributes = item.Attributes And Not FileAttributes.ReadOnly
            End If
          End If

          ' Hidden
          If HiddenAttributeCheck.Checked Then
            ' Add the attribute if not already set.
            If (item.Attributes And FileAttributes.Hidden) <> FileAttributes.Hidden Then
              item.Attributes = item.Attributes Or FileAttributes.Hidden
            End If
          Else
            ' Remove the attribute if already set.
            If (item.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
              item.Attributes = item.Attributes And Not FileAttributes.Hidden
            End If
          End If

          ' Archive
          If ArchiveAttributeCheck.Checked Then
            ' Add the attribute if not already set.
            If (item.Attributes And FileAttributes.Archive) <> FileAttributes.Archive Then
              item.Attributes = item.Attributes Or FileAttributes.Archive
            End If
          Else
            ' Remove the attribute if already set.
            If (item.Attributes And FileAttributes.Archive) = FileAttributes.Archive Then
              item.Attributes = item.Attributes And Not FileAttributes.Archive
            End If
          End If
        End If

        ' Zip Comments
        Dim zippedFile As ZippedFile = CType(IIf(TypeOf item Is ZippedFile, item, Nothing), ZippedFile)
        If Not zippedFile Is Nothing Then
          If zippedFile.Comment <> CommentsText.Text Then
            zippedFile.Comment = CommentsText.Text
          End If
        End If

        Dim zippedFolder As ZippedFolder = CType(IIf(TypeOf item Is ZippedFolder, item, Nothing), ZippedFolder)
        If Not zippedFolder Is Nothing Then
          If zippedFolder.Comment <> CommentsText.Text Then
            zippedFolder.Comment = CommentsText.Text
          End If
        End If

        Dim zipFile As ZippedFolder = CType(IIf(TypeOf m_item Is ZippedFolder, m_item, Nothing), ZippedFolder)
        If Not zipFile Is Nothing Then
          If zipFile.Comment <> CommentsText.Text Then
            zipFile.Comment = CommentsText.Text
          End If
        End If
      Finally
        CType(autoBatchUpdateObject, IDisposable).Dispose()
      End Try
    End Sub

    Private Function ValidateValues() As Boolean
      Dim item As FileSystemItem = m_item

      ' Are we working with the archive file itself?
      If (Not item.HostFile Is Nothing) AndAlso (item.RootFolder Is item) Then
        item = item.HostFile
      End If

      ' Make sure the new name does not already exist.
      Dim exists As Boolean = False

      If (Not item.RootFolder Is item) AndAlso (NameTextBox.Text <> item.Name) Then
        If Not (CType(IIf(TypeOf item Is AbstractFile, item, Nothing), AbstractFile)) Is Nothing Then
          exists = (item.ParentFolder.GetFile(NameTextBox.Text).Exists)
        Else
          exists = (item.ParentFolder.GetFolder(NameTextBox.Text).Exists)
        End If

        If exists Then
          MessageBox.Show(Me, "Cannot rename " & item.Name & ": An item with the name you specified already exists. Specify a different name.", "Error renamming item", MessageBoxButtons.OK, MessageBoxIcon.Error)

          Return False
        End If
      End If

      Return True
    End Function

    Private Sub ToggleCustomPanels()
      ' The panel will be visible when:
      '    1. The item is a ZippedFile or ZippedFolder
      '    2. The item is another archive inside this one.

      MainTabControl.TabPages.Remove(ZipPage)

      If Not (CType(IIf(TypeOf m_item.RootFolder Is ZippedFolder, m_item.RootFolder, Nothing), ZippedFolder)) Is Nothing Then
        MainTabControl.TabPages.Add(ZipPage)
      End If

      If (Not m_item.HostFile Is Nothing) AndAlso (TypeOf m_item.HostFile.RootFolder Is ZippedFolder) AndAlso (m_item.RootFolder Is m_item) Then
        MainTabControl.TabPages.Add(ZipPage)
      End If
    End Sub

    Private Function GetFormattedSize(ByVal size As Long) As String
      Return size.ToString("#,##0") & " bytes"
    End Function

    Private Sub ItemPicture_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles ItemPicture.Paint
      ' We draw the icon this way to keep the alpha blending of the icon.
      IconCache.LargeIconList.Draw(e.Graphics, 0, 0, IconCache.GetIconIndex(m_item, False, True))
    End Sub

    Private Sub PropertyForm_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
      If Me.DialogResult = System.Windows.Forms.DialogResult.Cancel Then
        Return
      End If

      If (Not Me.ValidateValues()) Then
        e.Cancel = True
        Return
      End If

      Me.SaveValues()
    End Sub

#End Region     ' PRIVATE METHODS

    #Region "PROTECTED METHODS"

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Overloads Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not components Is Nothing Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

    #End Region ' PROTECTED METHODS

    #Region "PRIVATE FIELDS"

    Private m_item As FileSystemItem ' = null

    #End Region ' PRIVATE FIELDS

    #Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(PropertyForm))
      Me.MainTabControl = New System.Windows.Forms.TabControl()
      Me.GeneralPage = New System.Windows.Forms.TabPage()
      Me.ArchiveAttributeCheck = New System.Windows.Forms.CheckBox()
      Me.ItemPicture = New System.Windows.Forms.PictureBox()
      Me.label9 = New System.Windows.Forms.Label()
      Me.HiddenAttributeCheck = New System.Windows.Forms.CheckBox()
      Me.ReadOnlyAttributeCheck = New System.Windows.Forms.CheckBox()
      Me.SystemAttributeCheck = New System.Windows.Forms.CheckBox()
      Me.groupBox3 = New System.Windows.Forms.GroupBox()
      Me.LastAccessDateTimeLabel = New System.Windows.Forms.Label()
      Me.ModificationDateTimeLabel = New System.Windows.Forms.Label()
      Me.CreationDateTimeLabel = New System.Windows.Forms.Label()
      Me.label6 = New System.Windows.Forms.Label()
      Me.label5 = New System.Windows.Forms.Label()
      Me.label4 = New System.Windows.Forms.Label()
      Me.groupBox2 = New System.Windows.Forms.GroupBox()
      Me.CompressedSizeLabel = New System.Windows.Forms.Label()
      Me.label3 = New System.Windows.Forms.Label()
      Me.SizeLabel = New System.Windows.Forms.Label()
      Me.label2 = New System.Windows.Forms.Label()
      Me.groupBox1 = New System.Windows.Forms.GroupBox()
      Me.NameTextBox = New System.Windows.Forms.TextBox()
      Me.ZipPage = New System.Windows.Forms.TabPage()
      Me.CommentBox = New System.Windows.Forms.GroupBox()
      Me.CommentsText = New System.Windows.Forms.TextBox()
      Me.CancelBtn = New System.Windows.Forms.Button()
      Me.OkBtn = New System.Windows.Forms.Button()
      Me.MainTabControl.SuspendLayout()
      Me.GeneralPage.SuspendLayout()
      Me.ZipPage.SuspendLayout()
      Me.CommentBox.SuspendLayout()
      Me.SuspendLayout()
      ' 
      ' MainTabControl
      ' 
      Me.MainTabControl.Controls.Add(Me.GeneralPage)
      Me.MainTabControl.Controls.Add(Me.ZipPage)
      Me.MainTabControl.Location = New System.Drawing.Point(8, 8)
      Me.MainTabControl.Name = "MainTabControl"
      Me.MainTabControl.SelectedIndex = 0
      Me.MainTabControl.Size = New System.Drawing.Size(320, 360)
      Me.MainTabControl.TabIndex = 0
      ' 
      ' GeneralPage
      ' 
      Me.GeneralPage.Controls.Add(Me.ArchiveAttributeCheck)
      Me.GeneralPage.Controls.Add(Me.ItemPicture)
      Me.GeneralPage.Controls.Add(Me.label9)
      Me.GeneralPage.Controls.Add(Me.HiddenAttributeCheck)
      Me.GeneralPage.Controls.Add(Me.ReadOnlyAttributeCheck)
      Me.GeneralPage.Controls.Add(Me.SystemAttributeCheck)
      Me.GeneralPage.Controls.Add(Me.groupBox3)
      Me.GeneralPage.Controls.Add(Me.LastAccessDateTimeLabel)
      Me.GeneralPage.Controls.Add(Me.ModificationDateTimeLabel)
      Me.GeneralPage.Controls.Add(Me.CreationDateTimeLabel)
      Me.GeneralPage.Controls.Add(Me.label6)
      Me.GeneralPage.Controls.Add(Me.label5)
      Me.GeneralPage.Controls.Add(Me.label4)
      Me.GeneralPage.Controls.Add(Me.groupBox2)
      Me.GeneralPage.Controls.Add(Me.CompressedSizeLabel)
      Me.GeneralPage.Controls.Add(Me.label3)
      Me.GeneralPage.Controls.Add(Me.SizeLabel)
      Me.GeneralPage.Controls.Add(Me.label2)
      Me.GeneralPage.Controls.Add(Me.groupBox1)
      Me.GeneralPage.Controls.Add(Me.NameTextBox)
      Me.GeneralPage.Location = New System.Drawing.Point(4, 22)
      Me.GeneralPage.Name = "GeneralPage"
      Me.GeneralPage.Size = New System.Drawing.Size(312, 334)
      Me.GeneralPage.TabIndex = 0
      Me.GeneralPage.Text = "General"
      ' 
      ' ArchiveAttributeCheck
      ' 
      Me.ArchiveAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ArchiveAttributeCheck.Location = New System.Drawing.Point(96, 304)
      Me.ArchiveAttributeCheck.Name = "ArchiveAttributeCheck"
      Me.ArchiveAttributeCheck.Size = New System.Drawing.Size(200, 16)
      Me.ArchiveAttributeCheck.TabIndex = 5
      Me.ArchiveAttributeCheck.Text = "Archive"
      ' 
      ' ItemPicture
      ' 
      Me.ItemPicture.Location = New System.Drawing.Point(16, 8)
      Me.ItemPicture.Name = "ItemPicture"
      Me.ItemPicture.Size = New System.Drawing.Size(48, 48)
      Me.ItemPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
      Me.ItemPicture.TabIndex = 19
      Me.ItemPicture.TabStop = False
'      Me.ItemPicture.Paint += New System.Windows.Forms.PaintEventHandler(Me.ItemPicture_Paint);
      ' 
      ' label9
      ' 
      Me.label9.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label9.Location = New System.Drawing.Point(8, 232)
      Me.label9.Name = "label9"
      Me.label9.Size = New System.Drawing.Size(80, 16)
      Me.label9.TabIndex = 18
      Me.label9.Text = "Attributes:"
      ' 
      ' HiddenAttributeCheck
      ' 
      Me.HiddenAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.HiddenAttributeCheck.Location = New System.Drawing.Point(96, 256)
      Me.HiddenAttributeCheck.Name = "HiddenAttributeCheck"
      Me.HiddenAttributeCheck.Size = New System.Drawing.Size(200, 16)
      Me.HiddenAttributeCheck.TabIndex = 3
      Me.HiddenAttributeCheck.Text = "Hidden"
      ' 
      ' ReadOnlyAttributeCheck
      ' 
      Me.ReadOnlyAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ReadOnlyAttributeCheck.Location = New System.Drawing.Point(96, 232)
      Me.ReadOnlyAttributeCheck.Name = "ReadOnlyAttributeCheck"
      Me.ReadOnlyAttributeCheck.Size = New System.Drawing.Size(200, 16)
      Me.ReadOnlyAttributeCheck.TabIndex = 2
      Me.ReadOnlyAttributeCheck.Text = "Read-only"
      ' 
      ' SystemAttributeCheck
      ' 
      Me.SystemAttributeCheck.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.SystemAttributeCheck.Location = New System.Drawing.Point(96, 280)
      Me.SystemAttributeCheck.Name = "SystemAttributeCheck"
      Me.SystemAttributeCheck.Size = New System.Drawing.Size(200, 16)
      Me.SystemAttributeCheck.TabIndex = 4
      Me.SystemAttributeCheck.Text = "System"
      ' 
      ' groupBox3
      ' 
      Me.groupBox3.Location = New System.Drawing.Point(8, 216)
      Me.groupBox3.Name = "groupBox3"
      Me.groupBox3.Size = New System.Drawing.Size(296, 4)
      Me.groupBox3.TabIndex = 14
      Me.groupBox3.TabStop = False
      ' 
      ' LastAccessDateTimeLabel
      ' 
      Me.LastAccessDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.LastAccessDateTimeLabel.Location = New System.Drawing.Point(96, 192)
      Me.LastAccessDateTimeLabel.Name = "LastAccessDateTimeLabel"
      Me.LastAccessDateTimeLabel.Size = New System.Drawing.Size(200, 16)
      Me.LastAccessDateTimeLabel.TabIndex = 13
      Me.LastAccessDateTimeLabel.Text = "2006-03-02"
      ' 
      ' ModificationDateTimeLabel
      ' 
      Me.ModificationDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.ModificationDateTimeLabel.Location = New System.Drawing.Point(96, 168)
      Me.ModificationDateTimeLabel.Name = "ModificationDateTimeLabel"
      Me.ModificationDateTimeLabel.Size = New System.Drawing.Size(200, 16)
      Me.ModificationDateTimeLabel.TabIndex = 12
      Me.ModificationDateTimeLabel.Text = "2006-03-02"
      ' 
      ' CreationDateTimeLabel
      ' 
      Me.CreationDateTimeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CreationDateTimeLabel.Location = New System.Drawing.Point(96, 144)
      Me.CreationDateTimeLabel.Name = "CreationDateTimeLabel"
      Me.CreationDateTimeLabel.Size = New System.Drawing.Size(200, 16)
      Me.CreationDateTimeLabel.TabIndex = 11
      Me.CreationDateTimeLabel.Text = "2006-03-02"
      ' 
      ' label6
      ' 
      Me.label6.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label6.Location = New System.Drawing.Point(8, 192)
      Me.label6.Name = "label6"
      Me.label6.Size = New System.Drawing.Size(88, 16)
      Me.label6.TabIndex = 10
      Me.label6.Text = "Accessed:"
      ' 
      ' label5
      ' 
      Me.label5.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label5.Location = New System.Drawing.Point(8, 168)
      Me.label5.Name = "label5"
      Me.label5.Size = New System.Drawing.Size(88, 16)
      Me.label5.TabIndex = 9
      Me.label5.Text = "Modified:"
      ' 
      ' label4
      ' 
      Me.label4.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label4.Location = New System.Drawing.Point(8, 144)
      Me.label4.Name = "label4"
      Me.label4.Size = New System.Drawing.Size(88, 16)
      Me.label4.TabIndex = 8
      Me.label4.Text = "Created:"
      ' 
      ' groupBox2
      ' 
      Me.groupBox2.Location = New System.Drawing.Point(8, 128)
      Me.groupBox2.Name = "groupBox2"
      Me.groupBox2.Size = New System.Drawing.Size(296, 4)
      Me.groupBox2.TabIndex = 7
      Me.groupBox2.TabStop = False
      ' 
      ' CompressedSizeLabel
      ' 
      Me.CompressedSizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CompressedSizeLabel.Location = New System.Drawing.Point(96, 104)
      Me.CompressedSizeLabel.Name = "CompressedSizeLabel"
      Me.CompressedSizeLabel.Size = New System.Drawing.Size(200, 16)
      Me.CompressedSizeLabel.TabIndex = 6
      Me.CompressedSizeLabel.Text = "0 KB"
      ' 
      ' label3
      ' 
      Me.label3.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label3.Location = New System.Drawing.Point(8, 104)
      Me.label3.Name = "label3"
      Me.label3.Size = New System.Drawing.Size(80, 16)
      Me.label3.TabIndex = 5
      Me.label3.Text = "Compressed:"
      ' 
      ' SizeLabel
      ' 
      Me.SizeLabel.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.SizeLabel.Location = New System.Drawing.Point(96, 80)
      Me.SizeLabel.Name = "SizeLabel"
      Me.SizeLabel.Size = New System.Drawing.Size(200, 16)
      Me.SizeLabel.TabIndex = 4
      Me.SizeLabel.Text = "0 KB"
      ' 
      ' label2
      ' 
      Me.label2.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.label2.Location = New System.Drawing.Point(8, 80)
      Me.label2.Name = "label2"
      Me.label2.Size = New System.Drawing.Size(72, 16)
      Me.label2.TabIndex = 3
      Me.label2.Text = "Size:"
      ' 
      ' groupBox1
      ' 
      Me.groupBox1.Location = New System.Drawing.Point(8, 64)
      Me.groupBox1.Name = "groupBox1"
      Me.groupBox1.Size = New System.Drawing.Size(296, 4)
      Me.groupBox1.TabIndex = 2
      Me.groupBox1.TabStop = False
      ' 
      ' NameTextBox
      ' 
      Me.NameTextBox.Location = New System.Drawing.Point(96, 16)
      Me.NameTextBox.Name = "NameTextBox"
      Me.NameTextBox.Size = New System.Drawing.Size(200, 20)
      Me.NameTextBox.TabIndex = 1
      Me.NameTextBox.Text = ""
      ' 
      ' ZipPage
      ' 
      Me.ZipPage.Controls.Add(Me.CommentBox)
      Me.ZipPage.Location = New System.Drawing.Point(4, 22)
      Me.ZipPage.Name = "ZipPage"
      Me.ZipPage.Size = New System.Drawing.Size(312, 334)
      Me.ZipPage.TabIndex = 1
      Me.ZipPage.Text = "Zip properties"
      ' 
      ' CommentBox
      ' 
      Me.CommentBox.Controls.Add(Me.CommentsText)
      Me.CommentBox.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CommentBox.Location = New System.Drawing.Point(8, 8)
      Me.CommentBox.Name = "CommentBox"
      Me.CommentBox.Size = New System.Drawing.Size(296, 320)
      Me.CommentBox.TabIndex = 0
      Me.CommentBox.TabStop = False
      Me.CommentBox.Text = "Comments"
      ' 
      ' CommentsText
      ' 
      Me.CommentsText.AutoSize = False
      Me.CommentsText.Location = New System.Drawing.Point(8, 16)
      Me.CommentsText.Multiline = True
      Me.CommentsText.Name = "CommentsText"
      Me.CommentsText.Size = New System.Drawing.Size(280, 296)
      Me.CommentsText.TabIndex = 6
      Me.CommentsText.Text = ""
      ' 
      ' CancelBtn
      ' 
      Me.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.CancelBtn.Location = New System.Drawing.Point(248, 376)
      Me.CancelBtn.Name = "CancelBtn"
      Me.CancelBtn.Size = New System.Drawing.Size(80, 23)
      Me.CancelBtn.TabIndex = 8
      Me.CancelBtn.Text = "&Cancel"
      ' 
      ' OkBtn
      ' 
      Me.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK
      Me.OkBtn.FlatStyle = System.Windows.Forms.FlatStyle.System
      Me.OkBtn.Location = New System.Drawing.Point(160, 376)
      Me.OkBtn.Name = "OkBtn"
      Me.OkBtn.Size = New System.Drawing.Size(80, 23)
      Me.OkBtn.TabIndex = 7
      Me.OkBtn.Text = "&Ok"
      ' 
      ' PropertyForm
      ' 
      Me.AcceptButton = Me.OkBtn
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.CancelButton = Me.CancelBtn
      Me.ClientSize = New System.Drawing.Size(336, 408)
      Me.Controls.Add(Me.OkBtn)
      Me.Controls.Add(Me.CancelBtn)
      Me.Controls.Add(Me.MainTabControl)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
      Me.MaximizeBox = False
      Me.Name = "PropertyForm"
      Me.Text = "Properties"
'      Me.Closing += New System.ComponentModel.CancelEventHandler(Me.PropertyForm_Closing);
      Me.MainTabControl.ResumeLayout(False)
      Me.GeneralPage.ResumeLayout(False)
      Me.ZipPage.ResumeLayout(False)
      Me.CommentBox.ResumeLayout(False)
      Me.ResumeLayout(False)

    End Sub
    #End Region

    #Region "Windows Form Designer generated fields"

    Private ArchiveAttributeCheck As System.Windows.Forms.CheckBox
    Private WithEvents ItemPicture As System.Windows.Forms.PictureBox
    Private ZipPage As System.Windows.Forms.TabPage
    Private CommentBox As System.Windows.Forms.GroupBox
    Private CommentsText As System.Windows.Forms.TextBox
    Private MainTabControl As System.Windows.Forms.TabControl
    Private ModificationDateTimeLabel As System.Windows.Forms.Label
    Private LastAccessDateTimeLabel As System.Windows.Forms.Label
    Private GeneralPage As System.Windows.Forms.TabPage
    Private NameTextBox As System.Windows.Forms.TextBox
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private label2 As System.Windows.Forms.Label
    Private SizeLabel As System.Windows.Forms.Label
    Private label3 As System.Windows.Forms.Label
    Private CompressedSizeLabel As System.Windows.Forms.Label
    Private groupBox2 As System.Windows.Forms.GroupBox
    Private label4 As System.Windows.Forms.Label
    Private label5 As System.Windows.Forms.Label
    Private label6 As System.Windows.Forms.Label
    Private CreationDateTimeLabel As System.Windows.Forms.Label
    Private groupBox3 As System.Windows.Forms.GroupBox
    Private CancelBtn As System.Windows.Forms.Button
    Private OkBtn As System.Windows.Forms.Button
    Private SystemAttributeCheck As System.Windows.Forms.CheckBox
    Private ReadOnlyAttributeCheck As System.Windows.Forms.CheckBox
    Private HiddenAttributeCheck As System.Windows.Forms.CheckBox
    Private label9 As System.Windows.Forms.Label
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.Container = Nothing

    #End Region ' Windows Form Designer generated fields
  End Class
End Namespace
