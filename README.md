![NuGet Downloads](https://img.shields.io/nuget/dt/Xceed.Products.Zip.Full) ![Static Badge](https://img.shields.io/badge/.Net_Framework-4.0%2B-blue) ![Static Badge](https://img.shields.io/badge/.Net-5.0%2B-blue) [![Learn More](https://img.shields.io/badge/Learn-More-blue?style=flat&labelColor=gray)](https://xceed.com/en/our-products/product/zip-for-net) [![See Demo](https://img.shields.io/badge/Simple_Live_Demo-▶-brightgreen)](https://xceedsoftware.github.io/Xceed-Zip-Samples/)

[![Xceed Zip for .NET](./Resources/zip_banner.png)](https://xceed.com/en/our-products/product/zip-for-net)

# Xceed Zip for .NET - Examples

Welcome to the Xceed Zip for .NET Samples repository. This repository provides a collection of sample applications and code snippets to help you get started with the Xceed Zip for .NET library. The examples are provided in both C# and Visual Basic .NET.

## About The Product

Xceed Zip for .NET is a powerful and versatile library that provides easy-to-use APIs for compressing and decompressing files, creating and extracting zip archives, and more. With its high performance and comprehensive feature set, it is the ideal choice for developers who need robust file compression capabilities. Key features include:

- **Comprehensive Compression**: Supports standard zip and zipx formats, and can create self-extracting zip files.
- **Strong Encryption**: Includes AES encryption to secure your compressed files.
- **Multi-threaded Compression**: Take advantage of multi-core processors to speed up the compression process.
- **Stream Support**: Read and write zip files directly to/from streams.
- **Flexible APIs**: Provides both low-level and high-level APIs for maximum flexibility.
- **Compatibility**: Fully compatible with .NET Standard, .NET Core, and .NET Framework.

For more information, please visit the [official product page](https://xceed.com/en/our-products/product/zip-for-net).

### Why Choose Xceed Zip for .NET?

- The richest feature set with over 200 capabilities
- Supports the latest zip file format standards
- Used and trusted by Microsoft in their server products
- Supports .NET 4.5, 5, 6 and 7 (including .NET core and .NET Standard)
  
## Getting Started with the Xceed Zip for .NET

To get started, clone this repository and explore the various sample projects provided. Each sample demonstrates different features and capabilities of Xceed Zip for .NET.

### Requirements
- Visual Studio 2015 or later
- .NET Framework 4.0 or later
- .NET 5.0 or later

### 1. Installing the Zip for .NET from nuget
To install the Xceed Zip for .NET from NuGet, follow these steps:

1. **Open your project in Visual Studio.**
2. **Open the NuGet Package Manager Console** by navigating to `Tools > NuGet Package Manager > Package Manager Console`.
3. **Run the following command:**
```sh
   dotnet add package Xceed.Products.Zip.Full
```

4. Alternatively, you can use the NuGet Package Manager GUI:

1. Right-click on your project in the Solution Explorer.
2. Select Manage NuGet Packages.
3. Search for Xceed.Products.Zip.Full and click Install.

![Nuget library](./Resources/nuget_sample.png)

### 2. Refering Xceed Zip for .NET library

1. **Add the reference with using statement at the top of the class**
   ```
   using Xceed.Zip;
   ```
   
2. **Use the classes and elements from the namespace**
   ```c#
   using Xceed.Zip;

   namespace BlazeDocX.Services
   {
       public class ZipSample
       {
            public void QuickZipSample()
            {
               QuickZip.Zip( @"d:\test.zip", @"d:\file.txt" );
            }
   
            public void QuickUnZipSample()
            {
               QuickZip.Unzip( @"d:\test.zip", @"d:\", true, true, false, "*" );
            }
   
            public QuickZipItem[] QuickGetContent()
            {
               QuickZipItem[] items = QuickZip.GetZipContents( @"c:\test.zip", "*" );
               return items;
            }
   
            public void QuickRemoveZip()
            {
               QuickZip.Remove( @"d:\test\files.zip", "old*" );
            }
       }
   }
   ```

   ### 3. A quick sample
   
   How to Zip and UnZip a file in .NET C# in seconds!
   
   Xceed Zip for .NET makes manipulating Zip files in C# very easy and simple.

   First launched in 2002 for .NET 1.0, Xceed Zip for .NET has been updated frequently ever since by our developers. It allows to quickly Zip and Unzip files using C# or Visual Basic for .NET code. 
   
   Note that the following example is done in C#, but that the Visual Basic for .NET code will be very similar.
   
   Also note that this code is compatible with .NET Standard, .NET Core, .NET 5 and .NET 6. Should you have an older project that also needs help from Xceed Zip for .NET, it is also compatible with all legacy versions of the .NET Framework. 
   
   So let's get to it!
   
   There are 2 ways we can go about compressing. The easiest way is to use our QuickZip class.
   
   If your scenarios are simple, this may just be the solution you need:
   
   ```csharp
   Xceed.Zip.QuickZip.Zip(@"c:ResultZipZippedFile.zip", true, true, false, @"c:FilesToZip*.*");
   ```
   
   To find all the different options and parameters related to this class, go [here](https://doc.xceed.com/xceed-filesystem-for-net/topic9402.html).
   
   As you just saw, you can create a zip archive from a file or folder using just 1 line of code. Isn't that fantastic!
   
   Xceed Zip for .NET is pretty much limitless and is very flexible. Here's a more complex example:
   
   ```csharp
   //Zip Archive Approach
   ZipArchive zipFile = new ZipArchive(new DiskFile(@"c:ResultZipZippedFileArchive.zip"));
   DiskFolder folder = new DiskFolder(@"c:FilesToZip");
   folder.CopyFilesTo(zipFile, true, true);
   ```    
   
   To UnZip, simply do the operations in reverse. It really is that simple and easy!
   ```csharp
   Xceed.Zip.QuickZip.Unzip(...)
   ```
   Same as the Zip Archive approach, but this time you do the reverse operations:
   ```csharp
   ZipArchive zipFile = new ZipArchive(new DiskFile(@"c:ResultZipZippedFileArchive.zip"));
   DiskFolder folder = new DiskFolder(@"c:FilesToZip");
   zipFile2.CopyFilesTo(folder , true, true);
   ```
   
   Xceed Zip for .NET offers a ton more functionalities! If you need more information, all functionalities are documented [here](https://doc.xceed.com/xceed-filesystem-for-net/webframe.html#topic87.html) (including encryption and different compression algorithms):

   ### 4. How to License the Product Using the LicenseKey Property
To license the Xceed Zip for .NET using the LicenseKey property, follow these steps:

1. **Obtain your license key** from Xceed. (Download the product from xceed.com or send us a request at support@xceed.com
2. **Set the LicenseKey property in your application startup code:**

   2.1 In case of WPF or Desktop app could be in the MainWindow
   ```csharp
   using System.Windows;

   public partial class MainWindow : Window
   {
       public MainWindow()
       {
           InitializeComponent();
           Xceed.Zip.Licenser.LicenseKey = "XXXX-XXXX-XXXX-XXXX";
       }
   }
   ```
   2.2 In case of ASP.NET application must be in Program.cs class
   ```csharp
   using System.Net;
   using System.Text.Json;
   using System.Text.Json.Serialization;
   ...
   using Xceed.Document.NET;
   ...
   Xceed.Zip.Licenser.LicenseKey = "XXXX-XXXX-XXXX-XXXX";
   ...
   var builder = WebAssemblyHostBuilder.CreateDefault(args);
   ```
4. Ensure the license key is set before any Zip class, instance or similar control is instantiated.

## Getting Started with the Samples

To get started with these examples, clone the repository and open the solution file in Visual Studio.

```bash
git clone https://github.com/your-repo/Xceed-Zip-Samples.git
cd xceed-zip-samples
```
Open the solution file in Visual Studio and build the project to restore the necessary NuGet packages.

## Requirements
- Visual Studio 2015 or later
- .NET Framework 4.0 or later
- .NET 5.0 or later
  
## Documentation

For more information on how to use the Xceed Zip for .NET, please refer to the [official documentation](https://doc.xceed.com/xceed-filesystem-for-net/webframe.html#topic46.html).

## Licensing

To receive a license key, visit [xceed.com](https://xceed.com) and download the product, or contact us directly at [support@xceed.com](mailto:support@xceed.com) and we will provide you with a trial key.

## Contact

If you have any questions, feel free to open an issue or contact us at [support@xceed.com](mailto:support@xceed.com).

---

© 2024 Xceed Software Inc. All rights reserved.
