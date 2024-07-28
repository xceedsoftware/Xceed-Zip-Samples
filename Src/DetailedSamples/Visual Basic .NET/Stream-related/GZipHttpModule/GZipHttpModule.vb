'
' Xceed Streaming Compresion for .NET - GZip HTTP module 
' Copyright (c) 2000-2002 - Xceed Software Inc
'
' [DeflateHttpModule.vb]
'
' This HTTP module can be used to compress any web application's response.
' All you need to do is add this Class Library to your ASP.NET web application's 
' references and check the web.config file of this project for stuff to add
' to your ASP.NET web application's web.config file.
'
' This file is part of Xceed Zip for .NET. The source code in this file
' is only intended as a supplement to the documentation and is provided "as is"
' without warranty of any kind, either expresed or implied.
'

Imports System
Imports System.Web
Imports Xceed.Compression
Imports Xceed.Compression.Formats

' This HTTP module can be used to compress any web application's response.
' All you need to do is add this Class Library to your ASP.NET web application's 
' references and check the web.config file of this project for stuff to add
' to your ASP.NET web application's web.config file.
Namespace Xceed.Compression.Formats.Samples.GZipHttpModule
  Public Class GZipHttpModule
    Implements System.Web.IHttpModule

    Sub Init(ByVal app As HttpApplication) Implements IHttpModule.Init
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

      ' We want to receive the BeginRequest event
      AddHandler app.BeginRequest, AddressOf Me.OnBeginRequest
    End Sub

    Sub Dispose() Implements IHttpModule.Dispose
    End Sub

    Public Sub OnBeginRequest(ByVal sender As Object, ByVal e As EventArgs)
      ' The sender is the HttpApplication
      Dim app As HttpApplication = sender

      ' Before processing this request, we want to check the client's request headers
      ' to see if it supports the gzip encoding. Both Internet Explorer and Netscape
      ' have been supporting this encoding for a long time.
      Dim encodings As String = app.Request.Headers.Get("Accept-Encoding")

      If (encodings Is Nothing) _
      Or (encodings.Length = 0) _
      Or (encodings.ToLower().IndexOf("gzip") < 0) Then
        ' We can't gzip, too bad
        Exit Sub
      End If

      ' Great, the client supports the "gzip" encoding. We create ourself a new
      ' CompressedStream, make it the new active filter, and give it the previous
      ' filter as its inner stream. A filter is nothing else than a stream. Using
      ' pass-thru streams, you can plug this way as many processing streams as you 
      ' wish. We can presume that the initial stream is the TCP stream on port 80!
      app.Response.Filter = New GZipCompressedStream(app.Response.Filter)

      ' Don't forget to tell that client our response is encoded using "deflate"!
      app.Response.AppendHeader("Content-Encoding", "gzip")
    End Sub

  End Class
End Namespace
