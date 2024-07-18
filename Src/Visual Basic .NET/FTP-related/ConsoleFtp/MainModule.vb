' 
' Xceed FTP for .NET - ClientFtp Sample Application
' Copyright (c) 2000-2003 - Xceed Software Inc.
'  
' [MainModule.vb]
'  
' This application demonstrates how to use the Xceed FTP Object model
' in a generic way.
'  
' This file is part of Xceed Ftp for .NET. The source code in Me file 
' is only intended as a supplement to the documentation, and is provided 
' "as is", without warranty of any kind, either expressed or implied.
' 

Module MainModule

  Function Main(ByVal args() As String) As Integer
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

        ' Xceed.Ftp.Licenser.LicenseKey = "FTNXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy 

    If args.Length > 1 Then
      ' We only accept the server host name as a parameter.
      Console.WriteLine("Invalid command-line arguments.")
      Console.WriteLine("Format: CONSOLEFTP {ftp_server_address}")

      Return 1
    End If

    ShowWelcomeMessage()

    ' Create an application object and run it!
    Dim app As New ConsoleFtp()

    If args.Length = 0 Then
      app.Run(String.Empty)
    Else
      app.Run()
    End If

    ShowGoodbyeMessage()
    Return 0
  End Function

  Private Sub ShowWelcomeMessage()
    Console.WriteLine("Welcome to Xceed Console FTP sample application.")
  End Sub

  Private Sub ShowGoodbyeMessage()
    Console.WriteLine("Thank you for trying Xceed FTP for .NET")
  End Sub

End Module
