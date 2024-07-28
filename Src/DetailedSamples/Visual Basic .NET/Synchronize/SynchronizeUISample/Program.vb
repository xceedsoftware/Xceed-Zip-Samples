'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [Program.vb]
'*
'* This application demonstrate how to use the Xceed Synchronize
'* functionnality.
'*
'* This file is part of Xceed FileSystem for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace SynchronizeUISample
  Friend Class Program
    ''' <summary>
    ''' The main entry point for the application.
    ''' </summary>
    Private Sub New()
    End Sub
    <STAThread()> _
    Shared Sub Main()
      ' ================================
      '* How to license Xceed components
      '* ================================
      '* To license your product, set the LicenseKey property to a valid trial or registered license key
      '* in the main entry point of the application to ensure components are licensed before any of the
      '* component methods are called.
      '*
      '* If the component is used in a DLL project (no entry point available), it is
      '* recommended that the LicenseKey property be set in a static constructor of a
      '* class that will be accessed systematically before any component is instantiated or,
      '* you can simply set the LicenseKey property immediately BEFORE you instantiate
      '* an instance of the component.
      '*
      '* For instance, if you wanted to deploy this sample, the license key needs to be set in the Main() method.
      '* If your trial period has expired, you must purchase a registered license key,
      '* uncomment the next line of code, and insert your registered license key.
      '* For more information, consult the "How the 45-day trial works" and the
      '* "How to license the component once you purchase" topics in the documentation of this product.
      '

      ' Xceed.Synchronization.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" ' Uncomment and set license key here to deploy

      Application.EnableVisualStyles()
      Application.SetCompatibleTextRenderingDefault(False)
      Application.Run(New SampleUI())
    End Sub
  End Class
End Namespace