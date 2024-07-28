'
'* Xceed Zip for .NET - Xceed Windows Explorer sample application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [Options.vb]
'*
'* Static class use to store the global options for the application.
'*
'* This file is part of Xceed Zip for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'


Imports Microsoft.VisualBasic
Imports System
Imports Xceed.Compression
Imports Xceed.Zip

Namespace Xceed.FileSystem.Samples
  Public Class Options
    #Region "GZIP PROPERTIES"

    ''' <summary>
    ''' Gets or sets a value controlling how GZip archive handles multiple files.
    ''' </summary>
    Public Shared Property GZipAllowMultipleFiles() As Boolean
      Get
        Return mg_gzipAllowMultipleFiles
      End Get
      Set
        mg_gzipAllowMultipleFiles = Value
      End Set
    End Property

    #End Region ' GZIP PROPERTIES

    #Region "ZIP PROPERTIES"

    ''' <summary>
    ''' Gets or sets the default compression level used for new Zip archives.
    ''' </summary>
    Public Shared Property ZipDefaultCompressionLevel() As CompressionLevel
      Get
        Return mg_zipDefaultCompressionLevel
      End Get
      Set
        mg_zipDefaultCompressionLevel = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the default compression method used for new Zip archives.
    ''' </summary>
    Public Shared Property ZipDefaultCompressionMethod() As CompressionMethod
      Get
        Return mg_zipDefaultCompressionMethod
      End Get
      Set
        mg_zipDefaultCompressionMethod = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the default extra headers used for new Zip archives.
    ''' </summary>
    Public Shared Property ZipDefaultExtraHeaders() As ExtraHeaders
      Get
        Return mg_zipDefaultExtraHeaders
      End Get
      Set
        mg_zipDefaultExtraHeaders = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the encryption password used when items are added to a Zip archive.
    ''' </summary>
    Public Shared Property ZipDefaultEncryptionPassword() As String
      Get
        Return mg_zipDefaultEncryptionPassword
      End Get
      Set
        mg_zipDefaultEncryptionPassword = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets or sets the encryption method used when items are added to a Zip archive with a password.
    ''' </summary>
    Public Shared Property ZipDefaultEncryptionMethod() As EncryptionMethod
      Get
        Return mg_zipDefaultEncryptionMethod
      End Get
      Set(ByVal Value As EncryptionMethod)
        mg_zipDefaultEncryptionMethod = Value
      End Set
    End Property

    ''' <summary>
    ''' Gets the encryption strength, in bits, used when items are added to a Zip archive with AES encryption.
    ''' </summary>
    Public Shared ReadOnly Property ZipDefaultEncryptionStrength() As Integer
      Get
        Return 256
      End Get
    End Property

    ''' <summary>
    ''' Gets or sets the last decryption password used for a Zip archive.
    ''' </summary>
    Public Shared Property ZipLastDecryptionPasswordUsed() As String
      Get
        Return mg_zipLastDecryptionPasswordUsed
      End Get
      Set(ByVal Value As String)
        mg_zipLastDecryptionPasswordUsed = Value
      End Set
    End Property

#End Region     ' ZIP PROPERTIES

#Region "FTP PROPERTIES"

    ''' <summary>
    ''' Gets or sets a value indicating if the Ftp connections should connect through an HTTP proxy server or not.
    ''' </summary>
    Public Shared Property FtpConnectThroughProxy() As Boolean
      Get
        Return mg_useProxyServer
      End Get
      Set(ByVal Value As Boolean)
        mg_useProxyServer = Value
      End Set
    End Property

    Public Shared Property FtpProxyServerAddress() As String
      Get
        Return mg_proxyAddress
      End Get
      Set(ByVal Value As String)
        mg_proxyAddress = Value
      End Set
    End Property

    Public Shared Property FtpProxyServerPort() As Integer
      Get
        Return mg_proxyPort
      End Get
      Set(ByVal Value As Integer)
        mg_proxyPort = Value
      End Set
    End Property

    Public Shared Property FtpProxyUsername() As String
      Get
        Return mg_proxyUsername
      End Get
      Set(ByVal Value As String)
        mg_proxyUsername = Value
      End Set
    End Property

    Public Shared Property FtpProxyPassword() As String
      Get
        Return mg_proxyPassword
      End Get
      Set(ByVal Value As String)
        mg_proxyPassword = Value
      End Set
    End Property

#End Region  ' FTP PROPERTIES

#Region "PRIVATE FIELDS"

    ' GZIP
    Private Shared mg_gzipAllowMultipleFiles As Boolean = False

    ' ZIP
    Private Shared mg_zipDefaultCompressionLevel As CompressionLevel = CompressionLevel.Normal
    Private Shared mg_zipDefaultCompressionMethod As CompressionMethod = CompressionMethod.Deflated
    Private Shared mg_zipDefaultExtraHeaders As ExtraHeaders = ExtraHeaders.FileTimes Or ExtraHeaders.Unicode
    Private Shared mg_zipDefaultEncryptionPassword As String = String.Empty
    Private Shared mg_zipDefaultEncryptionMethod As EncryptionMethod = EncryptionMethod.Compatible
    Private Shared mg_zipLastDecryptionPasswordUsed As String = String.Empty

    ' FTP
    Private Shared mg_useProxyServer As Boolean = False
    Private Shared mg_proxyAddress As String = String.Empty
    Private Shared mg_proxyPort As Integer = 8080
    Private Shared mg_proxyUsername As String = String.Empty
    Private Shared mg_proxyPassword As String = String.Empty

#End Region     ' PRIVATE FIELDS
  End Class
End Namespace
