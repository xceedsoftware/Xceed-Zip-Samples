' Xceed Streaming Compression Library - Compression Manager sample
' Copyright (c) 2002 Xceed Software Inc.
' 
' [Manager.vb]
' 
' This sample demonstrates how to compress and decompress a file using 
' different kinds of Compression formats. 
'
' This file is part of the Xceed Streaming Compression Library sample applications.
' The source code in this file is only intended as a supplement to Xceed
' Streaming Compression Library's documentation, and is provided "as is", without
' warranty of any kind, either expressed or implied.
'
Imports System.IO
Imports Xceed.Compression
Imports Xceed.Compression.Formats

Namespace Xceed.Compression.Samples
  Public Class Manager
    Inherits System.Windows.Forms.Form

    Public Sub New()
      MyBase.New()

      ' ================================
      ' How to license Xceed components 
      ' ================================       
      ' To license your product, set the LicenseKey property to a valid trial or registered license key 
      ' in the main entry point of the application to ensure components are licensed before any of the 
      ' component methods are called.      
      ' 
      ' If the component is used in a DLL project (no entry point available), it is 
      ' recommended that the LicenseKey property be set in a static constructor of a 
      ' class that will be accessed systematically before any component is instantiated or, 
      ' you can simply set the LicenseKey property immediately BEFORE you instantiate 
      ' an instance of the component.
      ' 
      ' For instance, if you wanted to deploy this sample, the license key needs to be set here.
      ' If your trial period has expired, you must purchase a registered license key,
      ' uncomment the next line of code, and insert your registerd license key.
      ' For more information, consult the "How the 45-day trial works" and the 
      ' "How to license the component once you purchase" topics in the documentation of this product.

      ' Xceed.Compression.Formats.Licenser.LicenseKey = "SCNXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

    End Sub

#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
        If Not (components Is Nothing) Then
          components.Dispose()
        End If
      End If
      MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lblErrorWarnings As System.Windows.Forms.Label
    Friend WithEvents lblCompressionFormat As System.Windows.Forms.Label
    Friend WithEvents mainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents menuFile As System.Windows.Forms.MenuItem
    Friend WithEvents menuFileQuit As System.Windows.Forms.MenuItem
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents btnDecompress As System.Windows.Forms.Button
    Friend WithEvents txtDestinationFile As System.Windows.Forms.TextBox
    Friend WithEvents saveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnCompress As System.Windows.Forms.Button
    Friend WithEvents lblDestinationFile As System.Windows.Forms.Label
    Friend WithEvents lblSourceFile As System.Windows.Forms.Label
    Friend WithEvents openFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnSelectDestinationFile As System.Windows.Forms.Button
    Friend WithEvents cboCompressionFormat As System.Windows.Forms.ComboBox
    Friend WithEvents btnSelectSourceFile As System.Windows.Forms.Button
    Friend WithEvents txtSourceFile As System.Windows.Forms.TextBox
    Friend WithEvents cboCompressionMethod As System.Windows.Forms.ComboBox
    Friend WithEvents lblCompressionMethod As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.lblErrorWarnings = New System.Windows.Forms.Label()
      Me.lblCompressionFormat = New System.Windows.Forms.Label()
      Me.mainMenu1 = New System.Windows.Forms.MainMenu()
      Me.menuFile = New System.Windows.Forms.MenuItem()
      Me.menuFileQuit = New System.Windows.Forms.MenuItem()
      Me.txtMessage = New System.Windows.Forms.TextBox()
      Me.btnDecompress = New System.Windows.Forms.Button()
      Me.txtDestinationFile = New System.Windows.Forms.TextBox()
      Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
      Me.btnCompress = New System.Windows.Forms.Button()
      Me.lblDestinationFile = New System.Windows.Forms.Label()
      Me.lblSourceFile = New System.Windows.Forms.Label()
      Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog()
      Me.btnSelectDestinationFile = New System.Windows.Forms.Button()
      Me.cboCompressionFormat = New System.Windows.Forms.ComboBox()
      Me.btnSelectSourceFile = New System.Windows.Forms.Button()
      Me.txtSourceFile = New System.Windows.Forms.TextBox()
      Me.cboCompressionMethod = New System.Windows.Forms.ComboBox()
      Me.lblCompressionMethod = New System.Windows.Forms.Label()
      Me.SuspendLayout()
      '
      'lblErrorWarnings
      '
      Me.lblErrorWarnings.AutoSize = True
      Me.lblErrorWarnings.Location = New System.Drawing.Point(8, 128)
      Me.lblErrorWarnings.Name = "lblErrorWarnings"
      Me.lblErrorWarnings.Size = New System.Drawing.Size(130, 13)
      Me.lblErrorWarnings.TabIndex = 41
      Me.lblErrorWarnings.Text = "Error / Warning message"
      '
      'lblCompressionFormat
      '
      Me.lblCompressionFormat.AutoSize = True
      Me.lblCompressionFormat.Location = New System.Drawing.Point(8, 8)
      Me.lblCompressionFormat.Name = "lblCompressionFormat"
      Me.lblCompressionFormat.Size = New System.Drawing.Size(106, 13)
      Me.lblCompressionFormat.TabIndex = 31
      Me.lblCompressionFormat.Text = "Compression format"
      '
      'mainMenu1
      '
      Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile})
      '
      'menuFile
      '
      Me.menuFile.Index = 0
      Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFileQuit})
      Me.menuFile.Text = "File"
      '
      'menuFileQuit
      '
      Me.menuFileQuit.Index = 0
      Me.menuFileQuit.Text = "Quit"
      '
      'txtMessage
      '
      Me.txtMessage.Location = New System.Drawing.Point(8, 152)
      Me.txtMessage.Multiline = True
      Me.txtMessage.Name = "txtMessage"
      Me.txtMessage.Size = New System.Drawing.Size(480, 104)
      Me.txtMessage.TabIndex = 40
      Me.txtMessage.Text = ""
      '
      'btnDecompress
      '
      Me.btnDecompress.Location = New System.Drawing.Point(400, 112)
      Me.btnDecompress.Name = "btnDecompress"
      Me.btnDecompress.Size = New System.Drawing.Size(82, 23)
      Me.btnDecompress.TabIndex = 39
      Me.btnDecompress.Text = "Decompress"
      '
      'txtDestinationFile
      '
      Me.txtDestinationFile.Location = New System.Drawing.Point(128, 80)
      Me.txtDestinationFile.Name = "txtDestinationFile"
      Me.txtDestinationFile.Size = New System.Drawing.Size(320, 20)
      Me.txtDestinationFile.TabIndex = 35
      Me.txtDestinationFile.Text = ""
      '
      'saveFileDialog1
      '
      Me.saveFileDialog1.FileName = "doc1"
      '
      'btnCompress
      '
      Me.btnCompress.Location = New System.Drawing.Point(304, 112)
      Me.btnCompress.Name = "btnCompress"
      Me.btnCompress.Size = New System.Drawing.Size(82, 23)
      Me.btnCompress.TabIndex = 38
      Me.btnCompress.Text = "Compress"
      '
      'lblDestinationFile
      '
      Me.lblDestinationFile.AutoSize = True
      Me.lblDestinationFile.Location = New System.Drawing.Point(8, 80)
      Me.lblDestinationFile.Name = "lblDestinationFile"
      Me.lblDestinationFile.Size = New System.Drawing.Size(79, 13)
      Me.lblDestinationFile.TabIndex = 33
      Me.lblDestinationFile.Text = "Destination file"
      '
      'lblSourceFile
      '
      Me.lblSourceFile.AutoSize = True
      Me.lblSourceFile.Location = New System.Drawing.Point(8, 56)
      Me.lblSourceFile.Name = "lblSourceFile"
      Me.lblSourceFile.Size = New System.Drawing.Size(58, 13)
      Me.lblSourceFile.TabIndex = 32
      Me.lblSourceFile.Text = "Source file"
      '
      'btnSelectDestinationFile
      '
      Me.btnSelectDestinationFile.Location = New System.Drawing.Point(456, 80)
      Me.btnSelectDestinationFile.Name = "btnSelectDestinationFile"
      Me.btnSelectDestinationFile.Size = New System.Drawing.Size(24, 23)
      Me.btnSelectDestinationFile.TabIndex = 37
      Me.btnSelectDestinationFile.Text = "..."
      '
      'cboCompressionFormat
      '
      Me.cboCompressionFormat.Items.AddRange(New Object() {"Standard", "GZip", "ZLib"})
      Me.cboCompressionFormat.Location = New System.Drawing.Point(128, 8)
      Me.cboCompressionFormat.Name = "cboCompressionFormat"
      Me.cboCompressionFormat.Size = New System.Drawing.Size(200, 21)
      Me.cboCompressionFormat.TabIndex = 30
      '
      'btnSelectSourceFile
      '
      Me.btnSelectSourceFile.Location = New System.Drawing.Point(456, 56)
      Me.btnSelectSourceFile.Name = "btnSelectSourceFile"
      Me.btnSelectSourceFile.Size = New System.Drawing.Size(24, 23)
      Me.btnSelectSourceFile.TabIndex = 36
      Me.btnSelectSourceFile.Text = "..."
      '
      'txtSourceFile
      '
      Me.txtSourceFile.AllowDrop = True
      Me.txtSourceFile.Location = New System.Drawing.Point(128, 56)
      Me.txtSourceFile.Name = "txtSourceFile"
      Me.txtSourceFile.Size = New System.Drawing.Size(320, 20)
      Me.txtSourceFile.TabIndex = 34
      Me.txtSourceFile.Text = ""
      '
      'cboCompressionMethod
      '
      Me.cboCompressionMethod.Items.AddRange(New Object() {"Standard", "GZip", "ZLib"})
      Me.cboCompressionMethod.Location = New System.Drawing.Point(128, 32)
      Me.cboCompressionMethod.Name = "cboCompressionMethod"
      Me.cboCompressionMethod.Size = New System.Drawing.Size(200, 21)
      Me.cboCompressionMethod.TabIndex = 42
      '
      'lblCompressionMethod
      '
      Me.lblCompressionMethod.AutoSize = True
      Me.lblCompressionMethod.Location = New System.Drawing.Point(8, 32)
      Me.lblCompressionMethod.Name = "lblCompressionMethod"
      Me.lblCompressionMethod.Size = New System.Drawing.Size(112, 13)
      Me.lblCompressionMethod.TabIndex = 43
      Me.lblCompressionMethod.Text = "Compression method"
      '
      'Manager
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(504, 275)
      Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cboCompressionMethod, Me.lblCompressionMethod, Me.btnDecompress, Me.txtDestinationFile, Me.btnCompress, Me.lblDestinationFile, Me.lblSourceFile, Me.btnSelectDestinationFile, Me.cboCompressionFormat, Me.btnSelectSourceFile, Me.txtSourceFile, Me.lblErrorWarnings, Me.lblCompressionFormat, Me.txtMessage})
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
      Me.MaximizeBox = False
      Me.Menu = Me.mainMenu1
      Me.Name = "Manager"
      Me.Text = "Compress / Decompress manager"
      Me.ResumeLayout(False)

    End Sub

#End Region

    ' ------------------------------------------------------------------------------------
    ' Initialize the default or last saved options
    ' ------------------------------------------------------------------------------------
    Private Sub Manager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

      cboCompressionFormat.SelectedIndex = 0

    End Sub

    ' -----------------------------------------------------------------------------------
    ' Let the user select a source file name and path using a file open
    ' dialog box
    ' -----------------------------------------------------------------------------------
    Private Sub btnSelectSourceFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectSourceFile.Click
      openFileDialog1.FileName = ""
      openFileDialog1.Title = "Source file"
      openFileDialog1.Filter = "All type (*.*)|*.*"
      openFileDialog1.FilterIndex = 0
      Dim result As DialogResult = openFileDialog1.ShowDialog(Me)

      If (result = System.Windows.Forms.DialogResult.OK) Then
        txtSourceFile.Text = openFileDialog1.FileName
        SetDestinationFileName()
      End If
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Let the user select a destination file name and path using a file save
    ' dialog box
    ' ------------------------------------------------------------------------------------
    Private Sub btnSelectDestinationFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectDestinationFile.Click
      openFileDialog1.FileName = ""
      openFileDialog1.Title = "Destination file"
      openFileDialog1.Filter = "Compressed (*.bz2;*.gz;*.std;*.zp3;*.zl;*.bwt;*.dfl;*.sto)|*.bz;*.gz;*.std;*.zip;*.zl;*.bwt;*.dfl;*.sto|" & _
          "All type (*.*)|*.*"
      openFileDialog1.FilterIndex = 0
      Dim result As DialogResult = saveFileDialog1.ShowDialog(Me)

      If (result = System.Windows.Forms.DialogResult.OK) Then
        txtDestinationFile.Text = saveFileDialog1.FileName
      End If
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Compress the selected source file to the specified destination file
    ' ------------------------------------------------------------------------------------
    Private Sub btnCompress_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompress.Click
      If (CompressFile(txtSourceFile.Text, txtDestinationFile.Text)) Then
        ' If the Compression is successful, empty the source and destination
        ' text box to simplify subsequent Compression/Decompression.
        txtSourceFile.Text = ""
        txtDestinationFile.Text = ""
      End If
    End Sub

    ' -----------------------------------------------------------------------------------
    ' Decompress the selected source file to the specified destination file
    ' ------------------------------------------------------------------------------------
    Private Sub btnDecompress_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDecompress.Click
      If (DecompressFile(txtSourceFile.Text, txtDestinationFile.Text)) Then
        ' If the Decompression is successful, empty the source and destination
        ' text box to simplify subsequent Compression/Decompression.
        txtSourceFile.Text = ""
        txtDestinationFile.Text = ""
      End If
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Allow a drag and drop of a file in the form
    ' ------------------------------------------------------------------------------------
    Private Sub Manager_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop
      CompressDragDrop(e)
    End Sub

    Private Sub Manager_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragOver
      CompressDragOver(e)
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Quit the sample application
    ' ------------------------------------------------------------------------------------
    Private Sub menuFileQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuFileQuit.Click
      Me.Close()
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Initialize the destination file to a default value if the destination
    ' text box is empty.
    ' ------------------------------------------------------------------------------------
    Private Sub txtDestinationFile_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDestinationFile.Leave
      SetDestinationFileName()
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Allow a drag and drop of a file in the source text box
    ' ------------------------------------------------------------------------------------
    Private Sub txtSourceFile_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSourceFile.DragDrop
      CompressDragDrop(e)
    End Sub

    Private Sub txtSourceFile_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles txtSourceFile.DragOver
      CompressDragOver(e)
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Method that perform the actual Compression of a source file to a destination file
    ' ------------------------------------------------------------------------------------
    Private Function CompressFile(ByVal sourceFileName As String, ByVal compressedFileName As String) As Boolean

      Dim compressedFile As Boolean = False

      Try
        Me.Cursor = Cursors.WaitCursor

        Dim sourceFile As Stream = New FileStream(sourceFileName, FileMode.Open)

        Dim destinationFile As Stream = New FileStream(compressedFileName, FileMode.Create)

        Dim buffer(32768) As Byte
        Dim read As Integer = 0
        Dim method As CompressionMethod

        If (cboCompressionMethod.SelectedItem Is Nothing) Then
          method = CompressionMethod.Deflated
        Else
          If (cboCompressionMethod.SelectedItem.ToString() = "Deflated") Then
            method = CompressionMethod.Deflated
          ElseIf (cboCompressionMethod.SelectedItem.ToString() = "Deflated64") Then
            method = CompressionMethod.Deflated64
          ElseIf (cboCompressionMethod.SelectedItem.ToString() = "Stored") Then
            method = CompressionMethod.Stored
          End If
        End If

        Dim format As String = cboCompressionFormat.SelectedItem.ToString()
        Select Case format
          Case "Standard"

            Dim standard As XceedCompressedStream = New XceedCompressedStream(destinationFile, method, CompressionLevel.Highest)

            read = sourceFile.Read(buffer, 0, buffer.Length)
            While (read > 0)
              standard.Write(buffer, 0, read)
              read = sourceFile.Read(buffer, 0, buffer.Length)
            End While

            standard.Close()

          Case "GZip"

            Dim gzip As GZipCompressedStream = New GZipCompressedStream(destinationFile)

            read = sourceFile.Read(buffer, 0, buffer.Length)
            While (read > 0)
              gzip.Write(buffer, 0, read)
              read = sourceFile.Read(buffer, 0, buffer.Length)
            End While

            gzip.Close()

          Case "ZLib"

            Dim zlib As ZLibCompressedStream = New ZLibCompressedStream(destinationFile, method, CompressionLevel.Highest)

            read = sourceFile.Read(buffer, 0, buffer.Length)
            While (read > 0)
              zlib.Write(buffer, 0, read)
              read = sourceFile.Read(buffer, 0, buffer.Length)
            End While

            zlib.Close()

        End Select

        destinationFile.Close()

        sourceFile.Close()

        txtMessage.Text = sourceFileName + " successfully compressed in " + compressedFileName
        compressedFile = True

      Catch exception As exception

        txtMessage.Text = "Failed to compress file " & sourceFileName & Environment.NewLine & exception.Message

      Finally

        Me.Cursor = Cursors.Default

      End Try

      CompressFile = compressedFile

    End Function

    ' ------------------------------------------------------------------------------------
    ' Function that perform the actual Decompression of a source file to a destination file
    ' ------------------------------------------------------------------------------------
    Private Function DecompressFile(ByVal sourceFileName As String, ByVal decompressedFileName As String) As Boolean

      Dim decompressedFile As Boolean = False

      Try
        Me.Cursor = Cursors.WaitCursor

        Dim sourceFile As Stream = New FileStream(sourceFileName, FileMode.Open)

        Dim destinationFile As Stream = New FileStream(decompressedFileName, FileMode.Create)

        Dim buffer(32768) As Byte
        Dim read As Integer = 0

        Dim format As String = cboCompressionFormat.SelectedItem.ToString()
        Select Case format
          Case "Standard"

            Dim standard As XceedCompressedStream = New XceedCompressedStream(sourceFile)

            read = standard.Read(buffer, 0, buffer.Length)
            While (read > 0)
              destinationFile.Write(buffer, 0, read)
              read = standard.Read(buffer, 0, buffer.Length)
            End While

            standard.Close()

          Case "GZip"

            Dim gzip As GZipCompressedStream = New GZipCompressedStream(sourceFile)

            read = gzip.Read(buffer, 0, buffer.Length)
            While (read > 0)
              destinationFile.Write(buffer, 0, read)
              read = gzip.Read(buffer, 0, buffer.Length)
            End While

            gzip.Close()

          Case "ZLib"

            Dim zlib As ZLibCompressedStream = New ZLibCompressedStream(sourceFile)

            read = zlib.Read(buffer, 0, buffer.Length)
            While (read > 0)
              destinationFile.Write(buffer, 0, read)
              read = zlib.Read(buffer, 0, buffer.Length)
            End While

            zlib.Close()

        End Select

        destinationFile.Close()

        sourceFile.Close()

        txtMessage.Text = sourceFileName + " successfully decompressed in " + decompressedFileName
        decompressedFile = True

      Catch exception As exception

        txtMessage.Text = "Failed to decompress file " & sourceFileName & Environment.NewLine & exception.Message

      Finally

        Me.Cursor = Cursors.Default

      End Try

      DecompressFile = decompressedFile

    End Function

    ' ------------------------------------------------------------------------------------
    ' Method that perform the actual drop of a file on the form or the source text box
    ' ------------------------------------------------------------------------------------
    Private Sub CompressDragDrop(ByVal e As DragEventArgs)

      If e.Data.GetDataPresent(DataFormats.FileDrop) Then
        Dim file As System.Array = CType(e.Data.GetData(DataFormats.FileDrop), System.Array)
        txtSourceFile.Text = CType(file.GetValue(0), String)
        SetDestinationFileName()
      End If

    End Sub

    Private Sub CompressDragOver(ByVal e As DragEventArgs)
      If e.Data.GetDataPresent(DataFormats.FileDrop) Then
        e.Effect = DragDropEffects.Copy
      Else
        e.Effect = DragDropEffects.None
      End If
    End Sub

    ' ------------------------------------------------------------------------------------
    ' Assign a default value to the destination file name if the destination text box
    ' is empty.
    ' ------------------------------------------------------------------------------------
    Private Sub SetDestinationFileName()
      Dim compressedFilename As String = txtDestinationFile.Text

      If compressedFilename = String.Empty Then
        compressedFilename = RemoveFileExtension(txtSourceFile.Text)

        Select Case cboCompressionFormat.SelectedItem.ToString()
          Case "Standard"
            txtDestinationFile.Text = compressedFilename + ".std"
            Exit Sub

          Case "GZip"
            txtDestinationFile.Text = compressedFilename + ".gz"
            Exit Sub

          Case "ZLib"
            txtDestinationFile.Text = compressedFilename + ".zl"
            Exit Sub
        End Select
      End If

    End Sub

    ' ------------------------------------------------------------------------------------
    ' Returns the path and file name without its extension
    ' ------------------------------------------------------------------------------------
    Private Function RemoveFileExtension(ByVal fileName As String) As String

      Dim NewFileName As String = fileName
      Dim theEnd As Integer = fileName.LastIndexOf(".")

      If theEnd <> 0 Then
        NewFileName = fileName.Substring(0, theEnd)
      End If

      RemoveFileExtension = NewFileName

    End Function

    ' ------------------------------------------------------------------------------------
    ' Selects the proper compression methods.
    ' ------------------------------------------------------------------------------------
    Private Sub cboCompressionFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompressionFormat.SelectedIndexChanged
      cboCompressionMethod.Items.Clear()

      Select Case cboCompressionFormat.SelectedItem.ToString()

        Case "Standard"
          cboCompressionMethod.Enabled = True
          cboCompressionMethod.Items.Add("Deflated")
          cboCompressionMethod.Items.Add("Deflated64")
          cboCompressionMethod.Items.Add("Stored")
          cboCompressionMethod.SelectedIndex = 0
          Exit Sub
        Case "GZip"
          cboCompressionMethod.Text = String.Empty
          cboCompressionMethod.Enabled = False
          Exit Sub
        Case "ZLib"
          cboCompressionMethod.Enabled = True
          cboCompressionMethod.Items.Add("Deflated")
          cboCompressionMethod.Items.Add("Deflated64")
          cboCompressionMethod.SelectedIndex = 0
          Exit Sub
      End Select
    End Sub

  End Class
End Namespace