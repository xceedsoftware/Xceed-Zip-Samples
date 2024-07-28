'Xceed Zip for .NET - StreamDemo for VB.NET Sample Application
'Copyright (c) 2000-2002 - Xceed Software Inc
'
'[StreamDemo.vb]
'
'This console application demonstrates how touse the CompressedStream class
'to compress/decompress data as it is written to/read from a Stream.
'
'This file is part of Xceed Zip for .NET. The source code in this file
'is only intended as a supplement to the documentation and is provided "as is"
'without warranty of any kind, either expresed or implied.

Imports System
Imports System.IO
Imports Xceed.Compression

Namespace Xceed.Grid.Samples
  Module Module1

#Region " Compression Methods "

    'Compress sourceFile into destFile using the default compression algorithm (deflate).
    'sourceFile is the name of the file to compress. 
    'destFile is the name of the output file.
    '
    'If destFile exists, it will be overwritten.
    Private Sub DoCompress(ByVal sourceFile As String, ByVal destFile As String)
      Console.WriteLine("Compressing file {0} to {1} ...", sourceFile, destFile)
      Console.WriteLine()

      'Open the source file into a FileStream
      Dim sourceStream As New FileStream(sourceFile, FileMode.Open, FileAccess.Read)

      'Open the destination file into a FileStream
      Dim destStream As New FileStream(destFile, FileMode.Create, FileAccess.Write)

      'Creates a CompressedStream around the destination FileStream so that 
      'all data written to that stream will be compressed.
      Dim compStream As New CompressedStream(destStream)

      'Copy the stream
      StreamCopy(sourceStream, compStream)
    End Sub
#End Region

#Region " Decompression Methods "

    'Decompress sourceFile into destFile using the default compression algorithm (deflate).
    'sourceFile is the name of the file to decompress.
    'destFile is the name of the output file.
    '
    'If destFile exists, it will be overwritten.
    Private Sub DoDecompress(ByVal sourceFile As String, ByVal destFile As String)
      Console.WriteLine("Decompression file {0} to {1} ...", sourceFile, destFile)
      Console.WriteLine()

      'Open the source file into a FileStream
      Dim sourceStream As New FileStream(sourceFile, FileMode.Open, FileAccess.Read)

      'Open the destination file into a FileStream
      Dim destStream As New FileStream(destFile, FileMode.Create, FileAccess.Write)

      'Creates a CompressedStream around the source FileStream so that all
      'data read from that stream will be decompressed.
      Dim compStream As New CompressedStream(sourceStream)

      'Copy the stream
      StreamCopy(compStream, destStream)
    End Sub
#End Region

#Region " Private Utility Methods "

    'Copies the contents of sourceStream into destStream.
    'sourceStream is the input stream.
    'destStream is the output stream.
    '
    'When done, this function closes both streams.
    Private Sub StreamCopy(ByVal sourceStream As Stream, ByVal destStream As Stream)
      Try
        Dim bytesRead As Integer
        Dim buffer(32768) As Byte

        bytesRead = sourceStream.Read(buffer, 0, buffer.Length)

        While (bytesRead > 0)
          destStream.Write(buffer, 0, bytesRead)
          bytesRead = sourceStream.Read(buffer, 0, buffer.Length)
        End While

      Finally
        sourceStream.Close()
        destStream.Close()
      End Try
    End Sub
#End Region

#Region " Entry-point, Licensing and non-compress related methods "

    'Entry point for the console application. 
    'The arguments are supplied on the command line
    Sub Main()
      Try
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

        ' Xceed.Compression.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 

        Console.WriteLine()
        Console.WriteLine("Xceed Zip for .NET - VB.NET StreamDemo")
        Console.WriteLine("======================================")
        Console.WriteLine()

        Dim args() As String = GetCommandLineArgs()

        If args.Length <> 3 Then
          PrintUsage()
          Return
        End If

        Select Case args(0)
          Case "-c"
            DoCompress(args(1), args(2))
          Case "-d"
            DoDecompress(args(1), args(2))
          Case Else
            PrintUsage()
            Return
        End Select
      Catch except As Exception
        Console.WriteLine()
        Console.WriteLine("ERROR: The following exception occured:")
        Console.WriteLine(except.ToString())
        Console.WriteLine()
      End Try

      Console.WriteLine("Done!")
    End Sub

    'Displays usage guidelines for the command-line application
    Private Sub PrintUsage()
      Console.WriteLine("Usage: StreamDemoVB.exe [-c | -d] sourceFile destFile")
      Console.WriteLine()
      Console.WriteLine("   -c : Compress sourceFile into destFile")
      Console.WriteLine("   -d : Decompress sourceFile into destFile")
    End Sub

    'Get the command-line arguments in a function that returns 
    'them in an object containing an array.
    Function GetCommandLineArgs() As String()
      Dim commandLine As String = Microsoft.VisualBasic.Command()
      Dim argument As String
      Dim stringOpened As Boolean = False
      Dim I As Long
      Dim args(0) As String

      argument = ""
      For I = 0 To commandLine.Length - 1
        If commandLine.Chars(I) = " " And Not stringOpened Then
          If argument.Length > 0 Then
            ReDim Preserve args(UBound(args) + 1)
            args(UBound(args) - 1) = argument.ToString()
            argument = ""
          End If
        Else
          If commandLine.Chars(I) = """" Then
            stringOpened = Not stringOpened
          Else
            argument = argument & commandLine.Chars(I)
          End If
        End If
      Next

      If (argument.Length > 0) Then
        ReDim Preserve args(UBound(args) + 1)
        args(UBound(args) - 1) = argument.ToString()
      End If

      If UBound(args) > 1 Then
        ReDim Preserve args(UBound(args) - 1)
      End If

      Return args
    End Function
#End Region

  End Module
End Namespace
