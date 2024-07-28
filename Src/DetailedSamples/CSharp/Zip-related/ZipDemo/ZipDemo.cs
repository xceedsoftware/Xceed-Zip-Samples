/*
 * Xceed Zip for .NET - ZipDemo Sample Application
 * Copyright (c) 2000-2002 - Xceed Software Inc.
 * 
 * [ZipDemo.cs]
 * 
 * This console application demonstrates how to perform basic zip file manipulation.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.IO;
using Xceed.FileSystem;
using Xceed.Compression;
using Xceed.Zip;

namespace Xceed.Zip.Samples.ZipDemo
{
  /// <summary>
  /// Demonstrates how to perform basic zip file manipulation.
  /// </summary>
  public class ZipDemo
  {
    #region Zipfile listing methods

    /// <summary>
    /// Lists the contents of a zip file based on a file mask (wildcard), and displays
    /// the results as a non-formatted list on the console.
    /// </summary>
    /// <param name="zipFilename">Name of the zip file. The file must exist.</param>
    /// <param name="fileMask">Wildcard for filtering the list output.</param>
    public static void ListZip( string zipFilename, string fileMask )
    {
      // Create a DiskFile object for the specified zip filename

      DiskFile zipFile = new DiskFile( zipFilename );

      if( !zipFile.Exists )
      {
        Console.WriteLine( "The specified zip file does not exist." );
        return;
      }

      Console.WriteLine( "Listing all files matching the mask \"{0}\" contained in {1}...", fileMask, zipFilename );
      Console.WriteLine();

      // Create a ZipArchive object to access the zipfile.

      ZipArchive zip = new ZipArchive( zipFile );

      // Obtain a flat array of all the files contained in the zip file and its subfolders.

      AbstractFile[] files = zip.GetFiles( true, fileMask );
      
      // Iterate on the returned array of AbstractFile objects, and print the full name
      // of each file.

      foreach( AbstractFile file in files )
        Console.WriteLine( file.FullName );

      Console.WriteLine();

      if( files.Length == 0 )
        Console.WriteLine( "The zipfile is empty, or it does not contain any file matching the specified mask." );
      else
        Console.WriteLine( "{0} files.", files.Length );
    }

    /// <summary>
    /// Lists the contents of a zip file based on a file mask (wildcard), and displays
    /// the results folder by folder.
    /// </summary>
    /// <param name="zipFilename">Name of the zip file. The file must exist.</param>
    /// <param name="fileMask">Wildcard for filtering the list output.</param>
    public static void ListZipByFolder( string zipFilename, string fileMask )
    {
      // Create a DiskFile object for the specified zip filename

      DiskFile zipFile = new DiskFile( zipFilename );

      if( !zipFile.Exists )
      {
        Console.WriteLine( "The specified zip file does not exist." );
        return;
      }

      Console.WriteLine( "Listing all files matching the mask \"{0}\" contained in {1}, folder by folder...", fileMask, zipFilename );
      Console.WriteLine();

      // Create a ZipArchive object to access the zipfile.

      ZipArchive zip = new ZipArchive( zipFile );

      // Since the ZipArchive class derives from ZippedFolded, which derives from
      // AbstractFolder, we can directly call a utility method that lists the contents
      // of an AbstractFolder object recursively.

      ListFolder( zip, fileMask );
    }

    /// <summary>
    /// Utility method that lists the contents of one folder in a zip file, and calls
    /// itself recursively for subfolders.
    /// </summary>
    /// <param name="folder">Folder that must be listed.</param>
    /// <param name="fileMask">File Mask used for filtering.</param>
    private static void ListFolder( AbstractFolder folder, string fileMask )
    {
      Console.WriteLine();
      Console.WriteLine( "Listing of " + folder.FullName );
      Console.WriteLine();

      // Obtain an array of files contained in the current folder.

      AbstractFile[] files = folder.GetFiles( false, fileMask );

      // Iterate on the returned array of AbstractFile objects, and print the details
      // of each file.

      foreach( AbstractFile file in files )
      {
        Console.WriteLine( 
          "{0}\t{1}\t{2}\t{3}", 
          file.LastWriteDateTime.ToShortDateString(), 
          file.LastWriteDateTime.ToShortTimeString(),  
          file.Size,
          file.Name );
      }

      Console.WriteLine();
      Console.WriteLine( "{0} files.", files.Length );

      // Call ListFolder recursively for the subfolders of the current folder.
      
      foreach( AbstractFolder subFolder in folder.GetFolders( false ) )
        ListFolder( subFolder, fileMask );
    }

    #endregion

    #region Zipfile extraction methods

    /// <summary>
    /// Extracts the contents of a zip file to a specified folder, based on a file mask (wildcard).
    /// </summary>
    /// <param name="zipFilename">Name of the zip file. The file must exist.</param>
    /// <param name="destFolder">Folder into which the files should be extracted.</param>
    /// <param name="fileMask">Wildcard for filtering the files to be extracted.</param>
    public static void ExtractZip( string zipFilename, string destFolder, string fileMask, string password )
    {
      // Create a DiskFile object for the specified zip filename

      DiskFile zipFile = new DiskFile( zipFilename );

      if( !zipFile.Exists )
      {
        Console.WriteLine( "The specified zip file does not exist." );
        return;
      }

      Console.WriteLine( "Extracting all files matching the mask \"{0}\" to \"{1}\"...", fileMask, destFolder );
      Console.WriteLine();

      // Create a ZipArchive object to access the zipfile.

      ZipArchive zip = new ZipArchive( zipFile );
      zip.DefaultDecryptionPassword = password;

      // Create a DiskFolder object for the destination folder

      DiskFolder destinationFolder = new DiskFolder( destFolder );

      // Create a FileSystemEvents object for handling the ItemProgression event

      FileSystemEvents events = new FileSystemEvents();

      // Subscribe to the ItemProgression event

      events.ItemProgression += new ItemProgressionEventHandler( OnItemProgression );
      events.ItemException += new ItemExceptionEventHandler( OnItemException );

      // Copy the contents of the zip to the destination folder.

      zip.CopyFilesTo( events, "Extracting", destinationFolder, true, true, fileMask );
    }

    #endregion

    #region Zip update methods

    /// <summary>
    /// Adds files to a zip file.
    /// </summary>
    /// <param name="zipFilename">Name of the zip file. If it does not exist, it will be created. If it exists, it will be updated.</param>
    /// <param name="sourceFolder">Name of the folder from which to add files.</param>
    /// <param name="fileMask">Name of the file to add to the zip file. Can include wildcards.</param>
    /// <param name="recursive">Specifies if the files in the sub-folders of <paramref name="sourceFolder"/> should also be added.</param>
    /// <param name="password"></param>
    /// <param name="method"></param>
    public static void AddFilesToZip( 
      string zipFilename, 
      string sourceFolder, 
      string fileMask, 
      bool recursive, 
      string password, 
      EncryptionMethod encryptionMethod )
    {
      if( sourceFolder.Length == 0 )
        throw new ArgumentException( "You must specify a source folder from which files will be added to the zip file.", "sourceFolder" );
      // Create a DiskFile object for the specified zip filename

      DiskFile zipFile = new DiskFile( zipFilename );

      // Check if the file exists

      if( !zipFile.Exists )
      {
        Console.WriteLine( "Creating a new zip file \"{0}\"...", zipFilename );
        zipFile.Create();
      }
      else
      {
        Console.WriteLine( "Updating existing zip file \"{0}\"...", zipFilename );
      }

      Console.WriteLine();

      // Create a ZipArchive object to access the zipfile.

      ZipArchive zip = new ZipArchive( zipFile );

      zip.DefaultCompressionMethod = m_method;
      zip.DefaultEncryptionPassword = password;
      zip.DefaultEncryptionMethod = encryptionMethod;
      zip.AllowSpanning = true;

      // Create a DiskFolder object for the source folder

      DiskFolder source = new DiskFolder( sourceFolder );

      // Create a ZipEvents object for handling the ItemProgression event

      ZipEvents events = new ZipEvents();

      // Subscribe to the ItemProgression event and DiskRequired event

      events.ItemProgression += new ItemProgressionEventHandler( OnItemProgression );
      events.DiskRequired += new DiskRequiredEventHandler( OnDiskRequired );


      // Copy the contents of the zip to the destination folder.
      source.CopyFilesTo( events, "Zipping", zip, recursive, true, fileMask );      
    }

    /// <summary>
    /// Removes files from a zip file.
    /// </summary>
    /// <param name="zipFilename">Name of the zip file. The file must exist.</param>
    /// <param name="fileMask">Name of the file to remove from the zip file. Can contain wildcards.</param>
    /// <param name="recursive">Specifies if the files in the sub-folders of <paramref name="zipFilename"/> should also be removed.</param>
    public static void RemoveFilesFromZip( string zipFilename, string fileMask, bool recursive )
    {
      if( fileMask.Length == 0 )
        throw new ArgumentException( "You must specify a file to remove from the zip file.", "fileMask" );

      // Create a DiskFile object for the specified zip filename

      DiskFile zipFile = new DiskFile( zipFilename );

      if( !zipFile.Exists )
      {
        Console.WriteLine( "The specified zip file does not exist." );
        return;
      }

      Console.WriteLine( "Removing files matching the mask \"{0}\" from \"{1}\"...", fileMask, zipFilename );
      Console.WriteLine();

      // Create a ZipArchive object to access the zipfile.

      ZipArchive zip = new ZipArchive( zipFile );

      // Obtain a flat array of files to remove

      AbstractFile[] filesToRemove = zip.GetFiles( recursive, fileMask );

      // To avoid updating the physical zip file each time a single file is
      // removed, we call BeginUpdate/EndUpdate on the zip archive.

      zip.BeginUpdate();

      try
      {
        // Iterate on the returned array of AbstractFile objects, and delete each file.

        foreach( AbstractFile file in filesToRemove )
        {
          Console.WriteLine( "Removing {0}...", file.FullName );
          file.Delete();
        }
      }
      finally
      {
        zip.EndUpdate();
      }
    }

    #endregion

    #region Zip events handlers

    /// <summary>
    /// Handles the ItemProgression event.
    /// </summary>
    /// <param name="sender">The object that raised this event.</param>
    /// <param name="e">Data related to this event.</param>
    private static void OnItemProgression( object sender, ItemProgressionEventArgs e )
    {
      if( ( e.CurrentItem != null ) && ( e.AllItems.Percent < 100 ) )
      {
        m_RetryCounter = 0;
        Console.WriteLine( "{0} {1}...", ( string )e.UserData, e.CurrentItem.FullName );
      }
    }


    private static void OnItemException( object sender, ItemExceptionEventArgs e )
    {
      if( e.CurrentItem is ZippedFile )
      {
        if( e.Exception is InvalidDecryptionPasswordException )
        {
          
            if( m_RetryCounter < 3 )
            {
              Console.Write( "Enter the password for the file {0}: ", e.CurrentItem.Name );

              ( ( ZipArchive )e.CurrentItem.RootFolder ).DefaultDecryptionPassword = Console.ReadLine();          
              e.Action = ItemExceptionAction.Retry;
              m_RetryCounter++;
            }
            else
            {
              Console.WriteLine( "{0} has been skipped due to an invalid password", e.CurrentItem.Name );
              e.Action = ItemExceptionAction.Ignore;
            }
          }
      }
    }

    private static void OnDiskRequired( object sender, DiskRequiredEventArgs e )
    {
      if( e.Action == DiskRequiredAction.Fail )
      {
        Console.WriteLine( "Please insert a disk and press <Enter>." );
        Console.WriteLine( "Press <Ctrl-C> to cancel the operation." );
        Console.ReadLine();
        
        e.Action = DiskRequiredAction.Continue;
      }
    }

    #endregion

    #region Entry-point, Licensing and non-zip related methods

    /// <summary>
    /// Entry-point for the console application.
    /// </summary>
    /// <param name="args">Arguments supplied on the command line.</param>
    public static void Main( string[] args )
    {
      try
      {
        /* ================================
         * How to license Xceed components 
         * ================================       
         * To license your product, set the LicenseKey property to a valid trial or registered license key 
         * in the main entry point of the application to ensure components are licensed before any of the 
         * component methods are called.      
         * 
         * If the component is used in a DLL project (no entry point available), it is 
         * recommended that the LicenseKey property be set in a static constructor of a 
         * class that will be accessed systematically before any component is instantiated or, 
         * you can simply set the LicenseKey property immediately BEFORE you instantiate 
         * an instance of the component.
         * 
         * For instance, if you wanted to deploy this sample, the license key needs to be set here.
         * If your trial period has expired, you must purchase a registered license key,
         * uncomment the next line of code, and insert your registerd license key.
         * For more information, consult the "How the 45-day trial works" and the 
         * "How to license the component once you purchase" topics in the documentation of this product.
         */

        // Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 
        
        Console.WriteLine();
        Console.WriteLine( "Xceed Zip for .NET - ZipDemo" );
        Console.WriteLine( "=================================" );
        Console.WriteLine();

        if( args.Length < 2 )
        {
          PrintUsage();
          return;
        }

        switch( args[ 0 ].ToLower() )
        {
          case "-l":
            ListZip( args[ 1 ], args.Length >= 3 ? args[ 2 ] : "" );
            break;

          case "-lf":
            ListZipByFolder( args[ 1 ], args.Length >= 3 ? args[ 2 ] : "" );
            break;

          case "-x":
            switch( args.Length )
            {
              case 0:
              case 1:
              case 2:
                PrintUsage();
                break;

              case 3:
                ExtractZip( args[ 1 ], args[ 2 ], "", "" );
                break;

              case 4: 
                if( args[ 3 ].StartsWith( "-p:" ) )
                  ExtractZip( args[ 1 ], args[ 2 ], "", args[ 3 ].Substring( 3 ) );
                else
                  ExtractZip( args[ 1 ], args[ 2 ], args[ 3 ], "" );
                break;

              default:
                if ( !args[ 4 ].StartsWith( "-p:" ) )
                {
                  PrintUsage();
                }
                else
                  ExtractZip( args[ 1 ], args[ 2 ], args[ 3 ], args[ 4 ].Substring( 3 ) );
                break;
            }
              
            break;

          case "-a":
          case "-a64":
            m_method = ( args[ 0 ].ToLower() == "-a64" ) ? CompressionMethod.Deflated64 : CompressionMethod.Deflated;

            switch( args.Length )
            {
              case 0:
              case 1:
              case 2:
              case 3:
                PrintUsage();
                break;

              case 4:
                AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], false, "", EncryptionMethod.Compatible );
                break;

              case 5:
                if( args[ 4 ] == "-r" )
                  AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], true, "", EncryptionMethod.Compatible );
                else if( args[ 4 ].StartsWith( "-p:" ) )
                  AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], false, args[ 4 ].Substring( 3 ), EncryptionMethod.Compatible );
                else if( args[ 4 ].StartsWith( "-pa:" ) )
                  AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], false, args[ 4 ].Substring( 4 ), EncryptionMethod.WinZipAes );
                else
                  PrintUsage();

                break;

              default:
                if( args[ 4 ] != "-r" )
                  PrintUsage();
                else if( args[ 5 ].StartsWith( "-p:" ) )
                  AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], true, args[ 5 ].Substring( 3 ), EncryptionMethod.Compatible );
                else if( args[ 5 ].StartsWith( "-pa:" ) )
                  AddFilesToZip( args[ 1 ], args[ 2 ], args[ 3 ], true, args[ 5 ].Substring( 4 ), EncryptionMethod.WinZipAes );
                else
                  PrintUsage();

                break;
            }
            break;

          case "-d":
            if( args.Length < 3 )
              PrintUsage();
            else
              RemoveFilesFromZip( args[ 1 ], args[ 2 ], args.Length >= 4 && args[ 3 ] == "-r" );

            break;

          default:
            PrintUsage();
            break;
        }
      }
      catch( Exception except )
      {
        Console.WriteLine();
        Console.WriteLine( "ERROR: The following exception occured:" );
        Console.WriteLine( except.Message );
        Console.WriteLine();
      }

      Console.WriteLine( "Done!" );
    }

    /// <summary>
    /// Displays usage guidelines for the command-line application.
    /// </summary>
    private static void PrintUsage()
    {
      Console.WriteLine( "Usage:" );
      Console.WriteLine();
      Console.WriteLine( " zipdemo.exe -l filename.zip [filemask]" );
      Console.WriteLine( " ---------------------------" );
      Console.WriteLine( "  Lists the contents of filename.zip, filtered by the optional filemask." );
      Console.WriteLine();
      Console.WriteLine( " zipdemo.exe -lf filename.zip [filemask]" );
      Console.WriteLine( " ---------------------------" );
      Console.WriteLine( "  Lists the contents of filename.zip by folders, filtered by the optional filemask." );
      Console.WriteLine();
      Console.WriteLine( " zipdemo.exe -x filename.zip destFolder [filemask] [-p:password]" );
      Console.WriteLine( " ---------------------------" );
      Console.WriteLine( "  Extracts the contents of filename.zip to destFolder, filtered by the optional filemask." );
      Console.WriteLine( "  * If -p: is specified, decrypts encrypted files using the provided password." );
      Console.WriteLine();
      Console.WriteLine( " zipdemo.exe -a[64] filename.zip sourceFolder sourceFileMask [-r] [-p[a]:password]" );
      Console.WriteLine( " ---------------------------" );
      Console.WriteLine( "  Adds the contents of sourceFolder and sourceFileMask to zipfile.zip." );
      Console.WriteLine( "  * If -a64 is specified instead of -a, uses the deflate64 compression method." );
      Console.WriteLine( "  * If filename.zip does not exist, creates a new file. If it exists, it will be updated." );
      Console.WriteLine( "  * If -r is specified, adds the files from sourceFolder's subfolders." );
      Console.WriteLine( "  * If -p: is specified, encrypts the files using the standard compatible zip encryption (weak)." );
      Console.WriteLine( "  * If -pa: is specified instead of -p, uses the AES strong encryption." );
      Console.WriteLine();
      Console.WriteLine( "  zipdemo.exe -d filename.zip fileMask [-r]" );
      Console.WriteLine( "  ---------------------------" );
      Console.WriteLine( "    Removes fileMask from filename.zip. If -r is specified, files will also be removed from subfolders." );
      Console.WriteLine();
    }

    #endregion

    #region Private Fields
      private static int m_RetryCounter; 
      private static CompressionMethod  m_method = CompressionMethod.Deflated;
    #endregion

  }
}

