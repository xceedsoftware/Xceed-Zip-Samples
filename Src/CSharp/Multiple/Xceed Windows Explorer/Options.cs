/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [Options.cs]
 * 
 * Static class use to store the global options for the application.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using Xceed.Compression;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples
{
	public class Options
	{
    #region GZIP PROPERTIES

    /// <summary>
    /// Gets or sets a value controlling how GZip archive handles multiple files.
    /// </summary>
    public static bool GZipAllowMultipleFiles
    {
      get { return mg_gzipAllowMultipleFiles; }
      set { mg_gzipAllowMultipleFiles = value; }
    }
    
    #endregion GZIP PROPERTIES

    #region ZIP PROPERTIES

    /// <summary>
    /// Gets or sets the default compression level used for new Zip archives.
    /// </summary>
    public static CompressionLevel ZipDefaultCompressionLevel
    {
      get { return mg_zipDefaultCompressionLevel; }
      set { mg_zipDefaultCompressionLevel = value; }
    }

    /// <summary>
    /// Gets or sets the default compression method used for new Zip archives.
    /// </summary>
    public static CompressionMethod ZipDefaultCompressionMethod
    {
      get { return mg_zipDefaultCompressionMethod; }
      set { mg_zipDefaultCompressionMethod = value; }
    }

    /// <summary>
    /// Gets or sets the default extra headers used for new Zip archives.
    /// </summary>
    public static ExtraHeaders ZipDefaultExtraHeaders
    {
      get { return mg_zipDefaultExtraHeaders; }
      set { mg_zipDefaultExtraHeaders = value; }
    }

    /// <summary>
    /// Gets or sets the encryption password used when items are added to a Zip archive.
    /// </summary>
    public static string ZipDefaultEncryptionPassword
    {
      get { return mg_zipDefaultEncryptionPassword; }
      set { mg_zipDefaultEncryptionPassword = value; }
    }

    /// <summary>
    /// Gets or sets the encryption method used when items are added to a Zip archive with a password.
    /// </summary>
    public static EncryptionMethod ZipDefaultEncryptionMethod
    {
      get { return mg_zipDefaultEncryptionMethod; }
      set { mg_zipDefaultEncryptionMethod = value; }
    }

    /// <summary>
    /// Gets the encryption strength, in bits, used when items are added to a Zip archive with AES encryption.
    /// </summary>
    public static int ZipDefaultEncryptionStrength
    {
      get { return 256; }
    }

    /// <summary>
    /// Gets or sets the last decryption password used for a Zip archive.
    /// </summary>
    public static string ZipLastDecryptionPasswordUsed
    {
      get { return mg_zipLastDecryptionPasswordUsed; }
      set { mg_zipLastDecryptionPasswordUsed = value; }
    }
    
    #endregion ZIP PROPERTIES

    #region FTP PROPERTIES

    /// <summary>
    /// Gets or sets a value indicating if the Ftp connections should connect through an HTTP proxy server or not.
    /// </summary>
    public static bool FtpConnectThroughProxy
    {
      get { return mg_useProxyServer; }
      set { mg_useProxyServer = value; }
    }

    public static string FtpProxyServerAddress
    {
      get { return mg_proxyAddress; }
      set { mg_proxyAddress = value; }
    }

    public static int FtpProxyServerPort
    {
      get { return mg_proxyPort; }
      set { mg_proxyPort = value; }
    }

    public static string FtpProxyUsername
    {
      get { return mg_proxyUsername; }
      set { mg_proxyUsername = value; }
    }

    public static string FtpProxyPassword
    {
      get { return mg_proxyPassword; }
      set { mg_proxyPassword = value; }
    }

    #endregion FTP PROPERTIES

    #region PRIVATE FIELDS

    // GZIP
    private static bool mg_gzipAllowMultipleFiles = false;

    // ZIP
    private static CompressionLevel mg_zipDefaultCompressionLevel = CompressionLevel.Normal;
    private static CompressionMethod mg_zipDefaultCompressionMethod = CompressionMethod.Deflated;
    private static ExtraHeaders mg_zipDefaultExtraHeaders = ExtraHeaders.FileTimes | ExtraHeaders.Unicode;
    private static string mg_zipDefaultEncryptionPassword = string.Empty;
    private static EncryptionMethod mg_zipDefaultEncryptionMethod = EncryptionMethod.Compatible;
    private static string mg_zipLastDecryptionPasswordUsed = string.Empty;

    // FTP
    private static bool mg_useProxyServer = false;
    private static string mg_proxyAddress = string.Empty;
    private static int mg_proxyPort = 8080;
    private static string mg_proxyUsername = string.Empty;
    private static string mg_proxyPassword = string.Empty;

    #endregion PRIVATE FIELDS
  }
}
