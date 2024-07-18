' Xceed Streaming Compression Library - Memory Compress sample
' Copyright (c) 2002 Xceed Software Inc.
'
' [MemoryCompress.vb]
'
' This sample demonstrates how to compress a chunk of memory data 
' using different kinds of compression formats, and decompress a 
' compressed memory data. 
'
' This file is part of the Xceed Streaming Compression Library sample applications.
' The source code in this file is only intended as a supplement to Xceed
' Streaming Compression Library's documentation, and is provided "as is", without
' warranty of any kind, either expressed or implied.
'

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Xceed.Compression
Imports Xceed.Compression.Formats

Namespace Xceed.Compression.Samples
  Public Class MemoryCompress
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
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents txtDecompressed As System.Windows.Forms.TextBox
    Friend WithEvents lblDecompressedText As System.Windows.Forms.Label
    Friend WithEvents lblOriginalSize As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents lblCompressionFormat As System.Windows.Forms.Label
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents lblOriginalSizeValue As System.Windows.Forms.Label
    Friend WithEvents lblCompressedSizeValue As System.Windows.Forms.Label
    Friend WithEvents btnDecompress As System.Windows.Forms.Button
    Friend WithEvents btnCompress As System.Windows.Forms.Button
    Friend WithEvents cboCompressionFormat As System.Windows.Forms.ComboBox
    Friend WithEvents txtTextToCompress As System.Windows.Forms.TextBox
    Friend WithEvents lblCompressionMethod As System.Windows.Forms.Label
    Friend WithEvents cboCompressionMethod As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
      Me.btnQuit = New System.Windows.Forms.Button()
      Me.txtDecompressed = New System.Windows.Forms.TextBox()
      Me.lblDecompressedText = New System.Windows.Forms.Label()
      Me.lblOriginalSize = New System.Windows.Forms.Label()
      Me.label1 = New System.Windows.Forms.Label()
      Me.lblCompressionFormat = New System.Windows.Forms.Label()
      Me.lblText = New System.Windows.Forms.Label()
      Me.lblOriginalSizeValue = New System.Windows.Forms.Label()
      Me.lblCompressedSizeValue = New System.Windows.Forms.Label()
      Me.btnDecompress = New System.Windows.Forms.Button()
      Me.btnCompress = New System.Windows.Forms.Button()
      Me.cboCompressionFormat = New System.Windows.Forms.ComboBox()
      Me.txtTextToCompress = New System.Windows.Forms.TextBox()
      Me.lblCompressionMethod = New System.Windows.Forms.Label()
      Me.cboCompressionMethod = New System.Windows.Forms.ComboBox()
      Me.SuspendLayout()
      '
      'btnQuit
      '
      Me.btnQuit.Location = New System.Drawing.Point(248, 440)
      Me.btnQuit.Name = "btnQuit"
      Me.btnQuit.Size = New System.Drawing.Size(80, 23)
      Me.btnQuit.TabIndex = 25
      Me.btnQuit.Text = "Quit"
      '
      'txtDecompressed
      '
      Me.txtDecompressed.Location = New System.Drawing.Point(8, 304)
      Me.txtDecompressed.Multiline = True
      Me.txtDecompressed.Name = "txtDecompressed"
      Me.txtDecompressed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.txtDecompressed.Size = New System.Drawing.Size(320, 120)
      Me.txtDecompressed.TabIndex = 24
      Me.txtDecompressed.Text = ""
      '
      'lblDecompressedText
      '
      Me.lblDecompressedText.AutoSize = True
      Me.lblDecompressedText.Location = New System.Drawing.Point(8, 288)
      Me.lblDecompressedText.Name = "lblDecompressedText"
      Me.lblDecompressedText.Size = New System.Drawing.Size(102, 13)
      Me.lblDecompressedText.TabIndex = 23
      Me.lblDecompressedText.Text = "Decompressed text"
      '
      'lblOriginalSize
      '
      Me.lblOriginalSize.AutoSize = True
      Me.lblOriginalSize.Location = New System.Drawing.Point(184, 152)
      Me.lblOriginalSize.Name = "lblOriginalSize"
      Me.lblOriginalSize.Size = New System.Drawing.Size(67, 13)
      Me.lblOriginalSize.TabIndex = 21
      Me.lblOriginalSize.Text = "Original size"
      '
      'label1
      '
      Me.label1.AutoSize = True
      Me.label1.Location = New System.Drawing.Point(96, 256)
      Me.label1.Name = "label1"
      Me.label1.Size = New System.Drawing.Size(92, 13)
      Me.label1.TabIndex = 19
      Me.label1.Text = "Compressed size"
      '
      'lblCompressionFormat
      '
      Me.lblCompressionFormat.AutoSize = True
      Me.lblCompressionFormat.Location = New System.Drawing.Point(8, 184)
      Me.lblCompressionFormat.Name = "lblCompressionFormat"
      Me.lblCompressionFormat.Size = New System.Drawing.Size(106, 13)
      Me.lblCompressionFormat.TabIndex = 15
      Me.lblCompressionFormat.Text = "Compression format"
      '
      'lblText
      '
      Me.lblText.AutoSize = True
      Me.lblText.Location = New System.Drawing.Point(8, 8)
      Me.lblText.Name = "lblText"
      Me.lblText.Size = New System.Drawing.Size(91, 13)
      Me.lblText.TabIndex = 13
      Me.lblText.Text = "Text to compress"
      '
      'lblOriginalSizeValue
      '
      Me.lblOriginalSizeValue.ForeColor = System.Drawing.SystemColors.HotTrack
      Me.lblOriginalSizeValue.Location = New System.Drawing.Point(272, 152)
      Me.lblOriginalSizeValue.Name = "lblOriginalSizeValue"
      Me.lblOriginalSizeValue.Size = New System.Drawing.Size(56, 13)
      Me.lblOriginalSizeValue.TabIndex = 22
      Me.lblOriginalSizeValue.Text = "0"
      '
      'lblCompressedSizeValue
      '
      Me.lblCompressedSizeValue.ForeColor = System.Drawing.SystemColors.HotTrack
      Me.lblCompressedSizeValue.Location = New System.Drawing.Point(200, 256)
      Me.lblCompressedSizeValue.Name = "lblCompressedSizeValue"
      Me.lblCompressedSizeValue.Size = New System.Drawing.Size(40, 13)
      Me.lblCompressedSizeValue.TabIndex = 20
      Me.lblCompressedSizeValue.Text = "0"
      '
      'btnDecompress
      '
      Me.btnDecompress.Location = New System.Drawing.Point(248, 248)
      Me.btnDecompress.Name = "btnDecompress"
      Me.btnDecompress.Size = New System.Drawing.Size(80, 23)
      Me.btnDecompress.TabIndex = 18
      Me.btnDecompress.Text = "Decompress"
      '
      'btnCompress
      '
      Me.btnCompress.Location = New System.Drawing.Point(8, 248)
      Me.btnCompress.Name = "btnCompress"
      Me.btnCompress.Size = New System.Drawing.Size(80, 23)
      Me.btnCompress.TabIndex = 17
      Me.btnCompress.Text = "Compress"
      '
      'cboCompressionFormat
      '
      Me.cboCompressionFormat.Items.AddRange(New Object() {"Standard", "GZip", "ZLib"})
      Me.cboCompressionFormat.Location = New System.Drawing.Point(128, 184)
      Me.cboCompressionFormat.Name = "cboCompressionFormat"
      Me.cboCompressionFormat.Size = New System.Drawing.Size(200, 21)
      Me.cboCompressionFormat.TabIndex = 16
      '
      'txtTextToCompress
      '
      Me.txtTextToCompress.Location = New System.Drawing.Point(8, 24)
      Me.txtTextToCompress.Multiline = True
      Me.txtTextToCompress.Name = "txtTextToCompress"
      Me.txtTextToCompress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
      Me.txtTextToCompress.Size = New System.Drawing.Size(320, 120)
      Me.txtTextToCompress.TabIndex = 14
      Me.txtTextToCompress.Text = "This is a little test to show you how the memory compression works." & Microsoft.VisualBasic.ChrW(13) & Microsoft.VisualBasic.ChrW(10) & "And it is ve" & _
      "ry easy to use." & Microsoft.VisualBasic.ChrW(13) & Microsoft.VisualBasic.ChrW(10) & "And there is some repeating " & Microsoft.VisualBasic.ChrW(13) & Microsoft.VisualBasic.ChrW(10) & "repeating repeating repeating rep" & _
      "eating repeating repeating " & Microsoft.VisualBasic.ChrW(13) & Microsoft.VisualBasic.ChrW(10) & "repeating text."
      '
      'lblCompressionMethod
      '
      Me.lblCompressionMethod.AutoSize = True
      Me.lblCompressionMethod.Location = New System.Drawing.Point(8, 208)
      Me.lblCompressionMethod.Name = "lblCompressionMethod"
      Me.lblCompressionMethod.Size = New System.Drawing.Size(112, 13)
      Me.lblCompressionMethod.TabIndex = 26
      Me.lblCompressionMethod.Text = "Compression method"
      '
      'cboCompressionMethod
      '
      Me.cboCompressionMethod.Items.AddRange(New Object() {"Standard", "GZip", "ZLib"})
      Me.cboCompressionMethod.Location = New System.Drawing.Point(128, 208)
      Me.cboCompressionMethod.Name = "cboCompressionMethod"
      Me.cboCompressionMethod.Size = New System.Drawing.Size(200, 21)
      Me.cboCompressionMethod.TabIndex = 27
      '
      'MemoryCompress
      '
      Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
      Me.ClientSize = New System.Drawing.Size(336, 471)
      Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblCompressionMethod, Me.cboCompressionMethod, Me.btnQuit, Me.txtDecompressed, Me.lblDecompressedText, Me.lblOriginalSize, Me.label1, Me.lblCompressionFormat, Me.lblText, Me.lblOriginalSizeValue, Me.lblCompressedSizeValue, Me.btnDecompress, Me.btnCompress, Me.cboCompressionFormat, Me.txtTextToCompress})
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "MemoryCompress"
      Me.Text = "Memory Compression"
      Me.ResumeLayout(False)

    End Sub

#End Region

    ' ------------------------------------------------------------------------------------
    ' Initialize the original size label and fill the combo box
    ' ------------------------------------------------------------------------------------
    Private Sub MemoryCompress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

      cboCompressionFormat.SelectedIndex = 0
      lblOriginalSizeValue.Text = txtTextToCompress.Text.Length.ToString()

    End Sub

    ' ------------------------------------------------------------------------------------
    ' Quit the sample application
    ' ------------------------------------------------------------------------------------
    Private Sub btnQuit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuit.Click

      Me.Close()

    End Sub

    ' ------------------------------------------------------------------------------------
    ' Update the original size label when the user modify the text to compress text box
    ' ------------------------------------------------------------------------------------
    Private Sub txtTextToCompress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTextToCompress.TextChanged

      lblOriginalSizeValue.Text = txtTextToCompress.Text.Length.ToString()

    End Sub

    ' ------------------------------------------------------------------------------------
    ' Do the compression of the text
    ' ------------------------------------------------------------------------------------
    Private Sub btnCompress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompress.Click

      If (txtTextToCompress.Text <> String.Empty) Then

        Try

          Me.Cursor = Cursors.WaitCursor

          Dim bytesToCompress As Byte() = System.Text.Encoding.Default.GetBytes(txtTextToCompress.Text)

          Dim destinationStream As MemoryStream = New MemoryStream()

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

          Select Case cboCompressionFormat.SelectedItem.ToString()

            Case "Standard"

              Dim standard As XceedCompressedStream = New XceedCompressedStream(destinationStream, method, CompressionLevel.Highest)
              standard.Write(bytesToCompress, 0, bytesToCompress.Length)
              standard.Close()

            Case "GZip"

              Dim gzip As GZipCompressedStream = New GZipCompressedStream(destinationStream)
              gzip.Write(bytesToCompress, 0, bytesToCompress.Length)
              gzip.Close()

            Case "ZLib"

              Dim zlib As ZLibCompressedStream = New ZLibCompressedStream(destinationStream, method, CompressionLevel.Highest)
              zlib.Write(bytesToCompress, 0, bytesToCompress.Length)
              zlib.Close()

          End Select

          m_compressedData = destinationStream.ToArray()
          lblCompressedSizeValue.Text = m_compressedData.Length.ToString()

          btnCompress.Enabled = False
          cboCompressionFormat.Enabled = False

          destinationStream.Close()

        Catch exception As exception

          MessageBox.Show("Failed to compress text " + Environment.NewLine + exception.Message)

        Finally
          Me.Cursor = Cursors.Default
        End Try
      End If

    End Sub

    ' ------------------------------------------------------------------------------------
    ' Do the decompression of the compressed byte array
    ' ------------------------------------------------------------------------------------
    Private Sub btnDecompress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecompress.Click

      If (m_compressedData.Length > 0) Then

        Try

          Me.Cursor = Cursors.WaitCursor

          Dim sourceStream As MemoryStream = New MemoryStream(m_compressedData)

          Dim decompressedText As String = String.Empty

          Dim destinationStream As MemoryStream = New MemoryStream()

          Dim buffer(32768) As Byte
          Dim read As Integer = 0

          Select Case cboCompressionFormat.SelectedItem.ToString()

            Case "Standard"

              Dim standard As XceedCompressedStream = New XceedCompressedStream(sourceStream)

              read = standard.Read(buffer, 0, buffer.Length)
              While (read > 0)
                destinationStream.Write(buffer, 0, read)
                read = standard.Read(buffer, 0, buffer.Length)
              End While

              decompressedText = System.Text.Encoding.Default.GetString(destinationStream.ToArray())
              standard.Close()

            Case "GZip"

              Dim gzip As GZipCompressedStream = New GZipCompressedStream(sourceStream)

              read = gzip.Read(buffer, 0, buffer.Length)
              While (read > 0)
                destinationStream.Write(buffer, 0, read)
                read = gzip.Read(buffer, 0, buffer.Length)
              End While

              decompressedText = System.Text.Encoding.Default.GetString(destinationStream.ToArray())
              gzip.Close()

            Case "ZLib"
              Dim zlib As ZLibCompressedStream = New ZLibCompressedStream(sourceStream)

              read = zlib.Read(buffer, 0, buffer.Length)
              While (read > 0)
                destinationStream.Write(buffer, 0, read)
                read = zlib.Read(buffer, 0, buffer.Length)
              End While

              decompressedText = System.Text.Encoding.Default.GetString(destinationStream.ToArray())
              zlib.Close()



          End Select

          destinationStream.Close()

          m_compressedData = New Byte(0) {}
          lblCompressedSizeValue.Text = "0"
          txtDecompressed.Text = decompressedText

          btnCompress.Enabled = True
          cboCompressionFormat.Enabled = True

          sourceStream.Close()

        Catch exception As Exception

          MessageBox.Show("Failed to compress text " + Environment.NewLine + exception.Message)

        Finally
          Me.Cursor = Cursors.Default
        End Try
      End If

    End Sub

    ' ------------------------------------------------------------------------------------
    ' We clear the decompressed text.
    ' ------------------------------------------------------------------------------------
    Private Sub cboCompressionFormat_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompressionFormat.SelectedIndexChanged

      If txtDecompressed.Text <> String.Empty Then
        txtDecompressed.Text = String.Empty
      End If

      cboCompressionMethod.Items.Clear()

      Select Case (cboCompressionFormat.SelectedItem.ToString())

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


    Private m_compressedData() As Byte = New Byte(0) {}

  End Class
End Namespace
