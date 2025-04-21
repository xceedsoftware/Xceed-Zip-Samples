using System.Windows;
using Win32 = Microsoft.Win32;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Xceed.Wpf.Zip.Sample
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      Xceed.Zip.Licenser.LicenseKey = "LICENSE_PLACEHOLDER";
    }

    private async void CompressFileButton_Click( object sender, RoutedEventArgs e )
    {
      Win32.OpenFileDialog openFileDialog = new()
      {
        Filter = "All files (*.*)|*.*",
        Title = "Select a file or folder",
        CheckFileExists = false,
        CheckPathExists = true,
        Multiselect = true
      };
      if( openFileDialog.ShowDialog() == true )
      {
        Win32.SaveFileDialog saveFileDialog = new()
        {
          Filter = "Zip files (*.zip)|*.zip",
          Title = "Save compressed file"
        };
        if( saveFileDialog.ShowDialog() == true )
        {
          string zipFilePath = saveFileDialog.FileName;
          try
          {
            CompressFileButton.IsEnabled = false;
            CompressFileButtonProgress.Visibility = Visibility.Visible;
            await Task.Run( () =>
            {
              var z = new ZipArchive( new DiskFile( zipFilePath ) );
              foreach( var item in openFileDialog.FileNames )
              {
                var diskFile = new DiskFile( item.ToString() );
                diskFile.CopyTo( z, true );
              }
            } );
            CompressFileButton.IsEnabled = true;
            CompressFileButtonProgress.Visibility = Visibility.Hidden;
            if( openFileDialog.FileNames.Length > 1 )
            {
              System.Windows.MessageBox.Show( "Files compressed and saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
            }
            else
            {
              System.Windows.MessageBox.Show( "File compressed and saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
            }
          }
          catch( Exception ex )
          {
            CompressFileButton.IsEnabled = true;
            CompressFileButtonProgress.Visibility = Visibility.Hidden;
            System.Windows.MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
          }
        }
      }
    }

    private async void CompressFolderButton_Click( object sender, RoutedEventArgs e )
    {
      var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
      {
        Description = "Select a folder to compress"
      };

      if( folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
      {
        string selectedFolderPath = folderBrowserDialog.SelectedPath;
        Win32.SaveFileDialog saveFileDialog = new()
        {
          Filter = "Zip files (*.zip)|*.zip",
          Title = "Save compressed file"
        };

        if( saveFileDialog.ShowDialog() == true )
        {
          string zipFilePath = saveFileDialog.FileName;
          try
          {
            CompressFolderButtonProgress.Visibility = Visibility.Visible;
            CompressFolderButton.IsEnabled = false;
            await Task.Run( () => {
              var z = new ZipArchive( new DiskFile( zipFilePath ) );

              var diskfolder = new DiskFolder( selectedFolderPath );
              diskfolder.CopyTo( z, true );
            } );

            CompressFolderButtonProgress.Visibility = Visibility.Hidden;
            CompressFolderButton.IsEnabled = true;
            System.Windows.MessageBox.Show( "Folder compressed and saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
          }
          catch( Exception ex )
          {
            CompressFolderButtonProgress.Visibility = Visibility.Hidden;
            CompressFolderButton.IsEnabled = true;
            System.Windows.MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
          }
        }
      }
    }


    private async void ListZipContentButton_Click( object sender, RoutedEventArgs e )
    {
      Win32.OpenFileDialog openFileDialog = new()
      {
        Filter = "Zip files (*.zip)|*.zip",
        Title = "Select a .zip file",
        CheckFileExists = true,
        CheckPathExists = true
      };
      if( openFileDialog.ShowDialog() == true )
      {
        string fileName = openFileDialog.FileName;
        ListBoxZipContent.Items.Clear();
        ListZipContentButton.IsEnabled = false;
        ListZipContentProgress.Visibility = Visibility.Visible;
        try
        {
          await Task.Run( () =>
          {
            ZipArchive zip = new( new DiskFile( fileName ) );
            foreach( AbstractFile f in zip.GetFiles( true ) )
            {
              Dispatcher.Invoke( () =>
              {
                ListBoxZipContent.Items.Add( f );
              } );
            }
          } );
          ListZipContentButton.IsEnabled = true;
          ListZipContentProgress.Visibility = Visibility.Hidden;
        }
        catch( Exception ex )
        {
          ListZipContentButton.IsEnabled = true;
          ListZipContentProgress.Visibility = Visibility.Hidden;
          System.Windows.MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error );
        }
      }
    }

    private async void Unzip_Click( object sender, RoutedEventArgs e )
    {
      Win32.OpenFileDialog openFileDialog = new()
      {
        Filter = "Zip files (*.zip)|*.zip",
        Title = "Select a zip file",
        CheckFileExists = false,
        CheckPathExists = true,
        Multiselect = false
      };

      if( openFileDialog.ShowDialog() == true )
      {
        var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
        {
          Description = "Select a folder to decompress"
        };
        if( folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
        {
          try
          {
            UnzipFilesButton.IsEnabled = false;
            UnzipFilesProgress.Visibility = Visibility.Visible;
            await Task.Run( () =>
            {
              var zip = new ZipArchive( new DiskFile( openFileDialog.FileName ) );
              DiskFolder folder = new( folderBrowserDialog.SelectedPath );
              zip.CopyFilesTo( folder, true, true );
            } );
            UnzipFilesButton.IsEnabled = true;
            UnzipFilesProgress.Visibility = Visibility.Hidden;
            System.Windows.MessageBox.Show( "Files decompressed successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information );
          }
          catch( Exception ex )
          {
            UnzipFilesButton.IsEnabled = true;
            UnzipFilesProgress.Visibility = Visibility.Hidden;
            System.Windows.MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error );            
          }
        }
      }
    }
  }
}
