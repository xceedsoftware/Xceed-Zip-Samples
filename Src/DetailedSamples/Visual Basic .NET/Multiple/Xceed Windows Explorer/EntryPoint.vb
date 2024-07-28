Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms

Namespace Xceed.FileSystem.Samples
  Public Class EntryPoint
    <STAThread> _
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
       '* uncomment the next lines of code, and insert your registered license keys.
       '* For more information, consult the "How the 45-day trial works" and the
       '* "How to license the component once you purchase" topics in the documentation of this product.
       '

      ' Xceed.Tar.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" // Uncomment and set license key here to deploy 
      ' Xceed.Zip.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" // Uncomment and set license key here to deploy 
      ' Xceed.Ftp.Licenser.LicenseKey = "ZINXX-XXXXX-XXXXX-XXXX" // Uncomment and set license key here to deploy 
      ' Xceed.SmartUI.Licenser.LicenseKey = "SUNXX-XXXXX-XXXXX-XXXX" // Uncomment and set license key here to deploy 

      ' Enables the application to use Windows XP visual style for drawing the controls.
      Application.EnableVisualStyles()

      ' Workaround for the bug where no icon were displayed.
      Application.DoEvents()

      Application.Run(New MainForm())
    End Sub
  End Class
End Namespace
