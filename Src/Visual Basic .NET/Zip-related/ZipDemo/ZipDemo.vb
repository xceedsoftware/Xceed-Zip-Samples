'Xceed Zip for .NET - StreamDemo for VB.NET Sample Application
'Copyright (c) 2000-2002 - Xceed Software Inc
'
'[ZipDemo.vb]
'
'This console application demonstrates how to perform basic zip file manipulations.
'
'This file is part of Xceed Zip for .NET. The source code in this file
'is only intended as a supplement to the documentation and is provided "as is"
'without warranty of any kind, either expresed or implied.

Imports System
Imports System.IO
Imports Xceed.FileSystem
Imports Xceed.Compression
Imports Xceed.Zip

Namespace Xceed.Zip.Samples
  Module Module1

    'Create a ZipEvents object for handling the ItemProgression event
    Dim WithEvents m_events As New ZipEvents()

#Region " Zipfile listing methods "

    'Lists the contents of a zip file based on a file mask (wildcard) and displays
    'the results in a non-formatted list on the console.
    '
    'zipFilename is the name of the zip file. The zip file MUST exist.
    'fileMask is the wildcard that is used to filter the list output.
    Public Sub ListZip(ByVal zipFilename As String, ByVal fileMask As String)

      'Create a DiskFile object for the specified zip filename
      Dim zipFile As New DiskFile(zipFilename)

      If Not zipFile.Exists Then
        Console.WriteLine("The specified zip file does not exist.")
        Return
      End If

      Console.WriteLine("Listing all files matching the mask {0} contained in {1}...", fileMask, zipFilename)
      Console.WriteLine()

      'Create a ZipArchive object to access the zip file
      Dim zip As New ZipArchive(zipFile)

      'Obtain a flat array of all files contained in the zip file and it's subfolders.
      Dim files() As AbstractFile = zip.GetFiles(True, fileMask)

      Dim file As AbstractFile
      For Each file In files
        Console.WriteLine(file.FullName)
      Next

      Console.WriteLine()

      If (files.Length = 0) Then
        Console.WriteLine("The zip file is empty or it does not contain any file matching the specified mask.")
      Else
        Console.WriteLine("{0} files.", files.Length)
      End If

    End Sub

    'Lists the contents of a zip file based on a file mask (wildcard) and displays
    'the results folder by folder.
    '
    'zipFilename is the name of the zip file. The zip file MUST exist.
    'fileMask is the wildcard that is used to filter the list output.
    Public Sub ListZipByFolder(ByVal zipFilename As String, ByVal fileMask As String)

      'Create a DiskFile object for the specified zip filename.
      Dim zipFile As New DiskFile(zipFilename)

      If Not zipFile.Exists Then
        Console.WriteLine("The specified zip file does not exist.")
        Return
      End If

      Console.WriteLine("Listing all the files matching the mask {0} contained in {1}, folder by folder...", fileMask, zipFilename)
      Console.WriteLine()

      'Create  a ZipArchive object to access the zip file
      Dim zip As New ZipArchive(zipFile)

      'Since the ZipArchive class derives from the ZippedFolder class which in turn
      'derives from the AbstractFolder class, we can directly call a utility method 
      'that lists the contents of an AbstractFolder recursively.
      ListFolder(zip, fileMask)
    End Sub

    'Utility method that lists the contents of a folder in a zip file and calls 
    'itself recursively for subfolders.
    Public Sub ListFolder(ByVal folder As AbstractFolder, ByVal fileMask As String)

      Console.WriteLine()
      Console.WriteLine("Listing of " + folder.FullName)
      Console.WriteLine()

      'Obtain an array of files contained in the current folder
      Dim files() As AbstractFile = folder.GetFiles(False, fileMask)

      'Iterate on the returned array of AbstractFile objects and print
      'the details of each file.
      Dim file As AbstractFile

      For Each file In files
        Console.WriteLine("{0} " + vbTab + "{1} " + vbTab + "{2} " + vbTab + "{3}", file.LastWriteDateTime.ToShortDateString(), file.LastWriteDateTime.ToShortTimeString(), file.Size, file.Name)
      Next

      Console.WriteLine()
      Console.WriteLine("{0} files.", files.Length)

      'Call ListFolder recursively for the subfolders of the current folder
      Dim subFolder As AbstractFolder

      For Each subFolder In folder.GetFolders(False)
        ListFolder(subFolder, fileMask)
      Next
    End Sub

#End Region

#Region " Zipfile extraction methods "

    'Extracts the contents of a zip file to a specified folder based on a filemask (wilcard)
    Public Sub ExtractZip(ByVal zipFilename As String, ByVal destFolder As String, ByVal fileMask As String, ByVal password As String)

      'Create a DiskFile object for the specified zip filename
      Dim zipFile As New DiskFile(zipFilename)

      If Not zipFile.Exists Then
        Console.WriteLine("The specified zip file does not exist.")
        Return
      End If

      Console.WriteLine("Extracting all files matching the mask {0} to {1}...", fileMask, destFolder)
      Console.WriteLine()

      'Create a ZipArchive object to access the zip file
      Dim zip As New ZipArchive(zipFile)
      zip.DefaultDecryptionPassword = password

      'Create a DiskFolder object for the destination folder
      Dim destinationFolder As New DiskFolder(destFolder)

      'Copy the contents of the zip file to the destination folder
      zip.CopyFilesTo(m_events, "Extracting", destinationFolder, True, True, fileMask)
    End Sub

#End Region

#Region " Zip update methods "

    'Adds files to a zip file
    '
    'zipFilename is the anme of the zip file. If it does not exist, it will be created. 
    'If it exists, it will be updated.
    '
    'sourceFolder is the name of the folder from which the files will be added.
    'fileMask is the name of the file to add to the zip file. It can include wildcards.
    'recursive specifies if the files in the subfolders of sourceFolder should also be added.
    Public Sub AddFilesToZip(ByVal zipFilename As String, ByVal sourceFolder As String, ByVal fileMask As String, ByVal recursive As Boolean, ByVal password As String, ByVal encMethod As EncryptionMethod)

      If (sourceFolder.Length = 0) Then
        Throw New ArgumentException("You must specify a source folder from which files will be added to the zip file.", "sourceFolder")
      End If

      'Create a DiskFile object for the specified zip filename
      Dim zipFile As New DiskFile(zipFilename)

      'Check if the file exists
      If Not zipFile.Exists Then
        Console.WriteLine("Creating a new zip file {0}...", zipFilename)
        zipFile.Create()
      Else
        Console.WriteLine("Updating existing zip file {0}...", zipFilename)
      End If

      Console.WriteLine()

      'Create a ZipArchive object to access the zip file
      Dim zip As New ZipArchive(zipFile)
      zip.DefaultCompressionMethod = m_method
      zip.DefaultEncryptionPassword = password
      zip.DefaultEncryptionMethod = encMethod
      zip.AllowSpanning = True

      'Create a DiskFolder object for the source folder
      Dim source As New DiskFolder(sourceFolder)

      'Copy the contents of the source folder to the zip file
      source.CopyFilesTo(m_events, "Zipping", zip, recursive, True, fileMask)
    End Sub

    'Remove files from the zip file.
    '
    'zipFilename is the name of the zip file. The file MUST exist.
    'fileMask is the name of the file to remove from the zip file. It can contain wildcards.
    'recursive specifies if the files in the subfolders of zipFilename should also be removed.
    Public Sub RemoveFilesFromZip(ByVal zipFilename As String, ByVal fileMask As String, ByVal recursive As Boolean)

      If (fileMask.Length = 0) Then
        Throw New ArgumentException("You must specify file to remove from the zip file.", "fileMask")
      End If

      'Create a DiskFile object for the specified filename
      Dim zipFile As New DiskFile(zipFilename)

      If Not zipFile.Exists Then
        Console.WriteLine("The specified zip file does not exist.")
        Return
      End If

      Console.WriteLine("Removing files matching the mask {0} from {1}...", fileMask, zipFilename)
      Console.WriteLine()

      'Create a ZipArchive object to acces the zip file
      Dim zip As New ZipArchive(zipFile)

      'Obtain a flat array of files to remove
      Dim filesToRemove() As AbstractFile = zip.GetFiles(recursive, fileMask)

      'To avoid updating the physical zip file each time a file is removed, 
      'we call BeginUpdate/EndUpdate on the zip archive
      zip.BeginUpdate()

      Try
        'Iterate on the returned array of AbstractFile objects and delete each file
        Dim file As AbstractFile
        For Each file In filesToRemove
          Console.WriteLine("Removing {0}...", file.FullName)
          file.Delete()
        Next
      Finally
        zip.EndUpdate()
      End Try
    End Sub

#End Region

#Region " Zip event handlers "

    'Handles the ItemProgression event.
    '
    'sender is the object the raised this event.
    'e is the data related to this event.
    Public Sub m_events_ItemProgression(ByVal sender As Object, ByVal e As Xceed.FileSystem.ItemProgressionEventArgs) Handles m_events.ItemProgression
      If Not e.CurrentItem Is Nothing And e.AllItems.Percent < 100 Then
        m_RetryCounter = 0
        Console.WriteLine("{0} {1}...", e.UserData, e.CurrentItem.FullName)
      End If
    End Sub

    Public Sub m_events_ItemException(ByVal sender As Object, ByVal e As Xceed.FileSystem.ItemExceptionEventArgs) Handles m_events.ItemException
      If (TypeOf (e.CurrentItem) Is ZippedFile) Then
        If (TypeOf (e.Exception) Is InvalidDecryptionPasswordException) Then
          If (m_RetryCounter < 3) Then

            Console.Write("Enter the password for the file {0}: ", e.CurrentItem.Name)

            Dim archive As ZipArchive = e.CurrentItem.RootFolder
            archive.DefaultDecryptionPassword = Console.ReadLine()

            e.Action = ItemExceptionAction.Retry
            m_RetryCounter = m_RetryCounter + 1
          Else
            Console.WriteLine("{0} has been skipped due to an invalid password", e.CurrentItem.Name)
            e.Action = ItemExceptionAction.Ignore
          End If
        End If
      End If

    End Sub

    Public Sub m_events_DiskRequired(ByVal sender As Object, ByVal e As Xceed.Zip.DiskRequiredEventArgs) Handles m_events.DiskRequired
      If e.Action = DiskRequiredAction.Fail Then
        Console.WriteLine("Please insert a disk and press <Enter>.")
        Console.WriteLine("Press <Ctrl-C> to cancel the operation.")
        Console.ReadLine()

        e.Action = DiskRequiredAction.[Continue]
      End If
    End Sub

#End Region

#Region " Entry-point, Licensing and non-zip related methods "
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

        ' Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 


        Console.WriteLine()
        Console.WriteLine("Xceed Zip for .NET - VB.NET ZipDemo Application")
        Console.WriteLine("===============================================")
        Console.WriteLine()

        Dim args() As String = GetCommandLineArgs()

        If (args.Length < 2) Then
          PrintUsage()
          Return
        End If

        Select Case args(0).ToLower()
          Case "-l"
            If (args.Length >= 3) Then
              ListZip(args(1), args(2))
            Else
              ListZip(args(1), "")
            End If
          Case "-lf"
            If (args.Length >= 3) Then
              ListZipByFolder(args(1), args(2))
            Else
              ListZipByFolder(args(1), "")
            End If
          Case "-x"
            Select Case args.Length()
              Case 0 To 2
                PrintUsage()

              Case 3
                ExtractZip(args(1), args(2), "", "")

              Case 4
                If (args(3).StartsWith("-p:")) Then
                  ExtractZip(args(1), args(2), "", args(3).Substring(3))
                Else
                  ExtractZip(args(1), args(2), args(3), "")
                End If

              Case Else
                If Not (args(4).StartsWith("-p:")) Then
                  PrintUsage()
                Else
                  ExtractZip(args(1), args(2), args(3), args(4).Substring(3))
                End If
            End Select

          Case "-a", "-a64"
            If args(0).ToLower() = "-a64" Then
              m_method = CompressionMethod.Deflated64
            End If

            Select Case args.Length()
              Case 0 To 3
                PrintUsage()

              Case 4
                AddFilesToZip(args(1), args(2), args(3), False, "", EncryptionMethod.Compatible)

              Case 5
                If (args(4) = "-r") Then
                  AddFilesToZip(args(1), args(2), args(3), True, "", EncryptionMethod.Compatible)
                ElseIf (args(4).StartsWith("-p:")) Then
                  AddFilesToZip(args(1), args(2), args(3), False, args(4).Substring(3), EncryptionMethod.Compatible)
                ElseIf (args(4).StartsWith("-pa:")) Then
                  AddFilesToZip(args(1), args(2), args(3), False, args(4).Substring(4), EncryptionMethod.WinZipAes)
                Else
                  PrintUsage()
                End If

              Case Else
                If (args(4) <> "-r") Then
                  PrintUsage()
                ElseIf (args(5).StartsWith("-p:")) Then
                  AddFilesToZip(args(1), args(2), args(3), True, args(5).Substring(3), EncryptionMethod.Compatible)
                ElseIf (args(5).StartsWith("-pa:")) Then
                  AddFilesToZip(args(1), args(2), args(3), True, args(5).Substring(4), EncryptionMethod.WinZipAes)
                Else
                  PrintUsage()
                End If
            End Select

          Case "-d"
              If (args.Length < 3) Then
                PrintUsage()
              Else
                Dim recurse As Boolean = False
                If (args.Length >= 4) Then
                  recurse = (args(3) = "-r")
                End If
                RemoveFilesFromZip(args(1), args(2), recurse)
              End If

          Case Else
              PrintUsage()
        End Select

      Catch except As Exception
        Console.WriteLine()
        Console.WriteLine("ERROR: The following exception occured:")
        Console.WriteLine(except.ToString())
        Console.WriteLine()
      End Try
      Console.WriteLine("Done!")
    End Sub

    'Displays usage guidelines for the command-line application.
    Private Sub PrintUsage()
      Console.WriteLine("Usage:")
      Console.WriteLine()
      Console.WriteLine(" zipdemo.exe -l filename.zip [filemask]")
      Console.WriteLine(" -----------------------------")
      Console.WriteLine("  Lists the contents of filename.zip filtered by the optional filemask")
      Console.WriteLine()
      Console.WriteLine(" zipdemo.exe -lf filename.zip [filemask]")
      Console.WriteLine(" -----------------------------")
      Console.WriteLine("  Lists the contents of filename.zip by folders, filtered by the optional filemask")
      Console.WriteLine()
      Console.WriteLine(" zipdemo.exe -x filename.zip destFolder [filemask] [-p:password]")
      Console.WriteLine(" -----------------------------")
      Console.WriteLine("  Extracts the contents of filename.zip to destFolder filtered by the optional filemask")
      Console.WriteLine("  * If -p: is specified, decrypts encrypted files using the provided password.")
      Console.WriteLine()
      Console.WriteLine(" zipdemo.exe -a[64] filename.zip sourceFolder sourceFileMask [-r] [-p[a]:password]")
      Console.WriteLine(" ---------------------------")
      Console.WriteLine("  Adds the contents of sourceFolder and sourceFileMask to zipfile.zip.")
      Console.WriteLine("  * If -a64 is specified instead of -a, uses the deflate64 compression method.")
      Console.WriteLine("  * If filename.zip does not exist, creates a new file. If it exists, it will be updated.")
      Console.WriteLine("  * If -r is specified, adds the files from sourceFolder's subfolders.")
      Console.WriteLine("  * If -p: is specified, encrypts the files using the standard compatible zip encryption (weak).")
      Console.WriteLine("  * If -pa: is specified instead of -p, uses the AES strong encryption.")
      Console.WriteLine()
      Console.WriteLine("  zipdemo.exe -d filename.zip fileMask [-r]")
      Console.WriteLine("  -----------------------------")
      Console.WriteLine("     Removes fileMask from filename.zip. If -r is specified, files will also be removed from the subfolders")
      Console.WriteLine()
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

#Region " Private Fields "
    Private m_RetryCounter As Integer = 0
    Private m_method As CompressionMethod = CompressionMethod.Deflated
#End Region


  End Module
End Namespace
