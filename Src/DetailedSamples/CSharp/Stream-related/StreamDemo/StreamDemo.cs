/*
 * Xceed Zip for .NET - StreamDemo Sample Application
 * Copyright (c) 2000-2002 - Xceed Software Inc.
 * 
 * [StreamDemo.cs]
 * 
 * This console application demonstrates how to use the CompressedStream class
 * to compress/decompress data as it is written to/read from a Stream.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.IO;
using Xceed.Compression;

namespace Xceed.Compression.Samples.StreamDemo
{
  /// <summary>
  /// Demonstrates how to use the CompressedStream class.
  /// </summary>
  public class StreamDemo
  {
    #region Compression methods

    /// <summary>
    /// Compresses <paramref name="sourceFile"/> into <paramref name="destFile"/>, using
    /// the default compression algorithm (deflate).
    /// </summary>
    /// <param name="sourceFile">Name of the file to compress.</param>
    /// <param name="destFile">Name of the output file.</param>
    /// <remarks>
    /// If <paramref name="destFile"/> exists, it will be overwitten.
    /// </remarks>
    private static void DoCompress( String sourceFile, String destFile )
    {
      Console.WriteLine( "Compressing file {0} to {1} ...", sourceFile, destFile );
      Console.WriteLine();

      // Open the source file into a FileStream
      FileStream sourceStream = new FileStream( sourceFile, FileMode.Open, FileAccess.Read );

      // Open the destination file into a FileStream
      FileStream destStream = new FileStream( destFile, FileMode.Create, FileAccess.Write );

      // Creates a CompressedStream around the destination FileStream, so that all
      // data written to that stream will be compressed.
      CompressedStream compStream = new CompressedStream( destStream );

      // Copy the stream
      StreamCopy( sourceStream, compStream );
    }

    #endregion

    #region Decompression methods

    /// <summary>
    /// Decompresses <paramref name="sourceFile"/> into <paramref name="destFile"/>, using
    /// the default decompression algorithm (deflate).
    /// </summary>
    /// <param name="sourceFile">Name of the file to decompress.</param>
    /// <param name="destFile">Name of the output file.</param>
    /// <remarks>
    /// If <paramref name="destFile"/> exists, it will be overwitten.
    /// </remarks>
    private static void DoDecompress( String sourceFile, String destFile )
    {
      Console.WriteLine( "Decompressing file {0} to {1} ...", sourceFile, destFile );
      Console.WriteLine();

      // Open the source file into a FileStream
      FileStream sourceStream = new FileStream( sourceFile, FileMode.Open, FileAccess.Read );

      // Open the destination file into a FileStream
      FileStream destStream = new FileStream( destFile, FileMode.Create, FileAccess.Write );

      // Creates a CompressedStream around the source FileStream so that all
      // data read from that stream will be decompressed.
      CompressedStream compStream = new CompressedStream( sourceStream );

      // Copy the stream
      StreamCopy( compStream, destStream );
    }

    #endregion

    #region Private utility methods

    /// <summary>
    /// Copies the contents of <paramref name="sourceStream"/> into <paramref name="destStream"/>.
    /// </summary>
    /// <param name="sourceStream">Input stream.</param>
    /// <param name="destStream">Output stream.</param>
    /// <remarks>
    /// When done, this function closes both streams.
    /// </remarks>
    private static void StreamCopy( Stream sourceStream, Stream destStream )
    {
      try
      {
        int bytesRead;
        byte[] buffer = new byte[ 32768 ];

        while( ( bytesRead = sourceStream.Read( buffer, 0, buffer.Length ) ) > 0 )
          destStream.Write( buffer, 0, bytesRead );
      }
      finally
      {
        sourceStream.Close();
        destStream.Close();
      }
    }

    #endregion

    #region Entry-point, Licensing and non-compress related methods

    /// <summary>
    /// Entry-point for the console application.
    /// </summary>
    /// <param name="args">
    /// Arguments supplied on the command line.
    /// </param>
    public static void Main(string[] args)
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
        
        // Xceed.Compression.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 


        Console.WriteLine();
        Console.WriteLine( "Xceed Zip for .NET - StreamDemo" );
        Console.WriteLine( "====================================" );
        Console.WriteLine();

        if( args.Length != 3 )
        {
          PrintUsage();
          return;
        }

        switch( args[ 0 ] )
        {
          case "-c":
          case "-C":
            DoCompress( args[ 1 ], args[ 2 ] );
            break;

          case "-d":
          case "-D":
            DoDecompress( args[ 1 ], args[ 2 ] );
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
        Console.WriteLine( except.ToString() );
        Console.WriteLine();
      }

      Console.WriteLine( "Done!" );
    }

    /// <summary>
    /// Displays usage guidelines for the command-line application.
    /// </summary>
    private static void PrintUsage()
    {
      Console.WriteLine( "Usage: StreamDemo.exe [-c | -d] sourceFile destFile\n" );
      Console.WriteLine();
      Console.WriteLine( "  -c : Compress sourceFile into destFile" );
      Console.WriteLine( "  -d : Decompress sourceFile into destFile" );
    }

    #endregion
  }
}
