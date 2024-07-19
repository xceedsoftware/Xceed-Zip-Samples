![NuGet Downloads](https://img.shields.io/nuget/dt/Xceed.Products.Zip.Full) ![Static Badge](https://img.shields.io/badge/.Net_Framework-4.0%2B-blue) ![Static Badge](https://img.shields.io/badge/.Net-5.0%2B-blue) [![Learn More](https://img.shields.io/badge/Learn-More-blue?style=flat&labelColor=gray)](https://xceed.com/en/our-products/product/zip-for-net)

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
- 
## Getting Started with the Xceed Zip for .NET

To get started, clone this repository and explore the various sample projects provided. Each sample demonstrates different features and capabilities of Xceed Zip for .NET.

### Requirements
- Visual Studio 2015 or later
- .NET Framework 4.0 or later
- .NET 5.0 or later

### 1. Installing the DataGrid from nuget
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

### 2. Adding a DataGrid to the XAML

To add a DataGrid to your XAML, follow these steps:

1. **Open your XAML file (e.g., MainWindow.xaml).**
2. **Add the following namespace at the top of your XAML file:**
   ```xaml
   xmlns:xcdg="http://schemas.xceed.com/wpf/xaml/datagrid"
   ```
3. **Add the DataGrid control to your layout:**
   ```xaml
   <xcdg:DataGridControl x:Name="myDataGrid"
                         AutoCreateColumns="True"
                         ItemsSource="{Binding YourDataSource}" />
   ```
4. Ensure your DataContext is set to an appropriate data source in your code-behind or ViewModel.

### 3. How to License the Product Using the LicenseKey Property
To license the Xceed Zip for .NET using the LicenseKey property, follow these steps:

1. **Obtain your license key** from Xceed. (Download the product from xceed.com or send us a request at support@xceed.com
2. **Set the LicenseKey property in your application startup code:**
   ```csharp
   using System.Windows;

   public partial class MainWindow : Window
   {
       public MainWindow()
       {
           InitializeComponent();
           Xceed.Wpf.DataGrid.Licenser.LicenseKey = "Your-Key-Here";
       }
   }
   ```
3. Ensure the license key is set before any DataGrid control is instantiated.

## Examples Overview

Below is a list of the examples available in this repository:

- **AsyncBinding**: Demonstrates how to bind the DataGrid asynchronously.
- **BatchUpdating**: Shows how to perform batch updates in the DataGrid.
- **CardView**: Provides an example of displaying data in a card view layout.
- **ColumnChooser**: Demonstrates how to implement a column chooser for the DataGrid.
- **ColumnManagerRow**: Shows how to use a column manager row.
- **CustomFiltering**: Demonstrates custom filtering techniques.
- **CustomViews**: Provides examples of custom views in the DataGrid.
- **DataVirtualization**: Shows how to use data virtualization to enhance performance.
- **EditModes**: Demonstrates various edit modes available in the DataGrid.
- **Exporting**: Provides examples of exporting data to different formats.
- **FlexibleBinding**: Shows how to bind data flexibly.
- **FlexibleRowsColumn**: Demonstrates flexible row and column configurations.
- **Formatting**: Provides examples of data formatting.
- **Grouping**: Demonstrates grouping data in the DataGrid.
- **IncludedEditors**: Shows how to use included editors.
- **LargeDataSets**: Demonstrates handling large datasets.
- **LiveUpdating**: Shows how to update data live.
- **MasterDetail**: Demonstrates master-detail views.
- **MergedHeaders**: Shows how to create merged headers.
- **MultiView**: Demonstrates multiple view configurations.
- **MVVM**: Provides examples of using MVVM pattern with the DataGrid.
- **PersistSettings**: Shows how to persist settings.
- **Printing**: Demonstrates printing capabilities.
- **Selection**: Shows how to handle selection in the DataGrid.
- **SpannedCells**: Demonstrates cell spanning techniques.
- **SummariesAndTotals**: Shows how to implement summaries and totals.
- **Tableflow**: Demonstrates table flow layout.
- **TableView**: Shows how to use the table view.
- **Theming**: Demonstrates theming capabilities.
- **TreeGridflowView**: Shows how to implement a tree grid flow view.
- **Validation**: Demonstrates data validation techniques.
- **Views3D**: Provides examples of 3D views.

## Getting Started with the Samples

To get started with these examples, clone the repository and open the solution file in Visual Studio.

```bash
git clone https://github.com/your-repo/xceed-datagrid-wpf-examples.git
cd xceed-datagrid-wpf-examples
```
Open the solution file in Visual Studio and build the project to restore the necessary NuGet packages.

## Requirements
- Visual Studio 2015 or later
- .NET Framework 4.0 or later
- .NET 5.0 or later
  
## Documentation

For more information on how to use the Xceed Zip for .NET, please refer to the [official documentation](https://doc.xceed.com/xceed-datagrid-for-wpf/webframe.html#rootWelcome.html).

## Licensing

To receive a license key, visit [xceed.com](https://xceed.com) and download the product, or contact us directly at [support@xceed.com](mailto:support@xceed.com) and we will provide you with a trial key.

## Contact

If you have any questions, feel free to open an issue or contact us at [support@xceed.com](mailto:support@xceed.com).

---

© 2024 Xceed Software Inc. All rights reserved.
