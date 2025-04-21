using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Zip;

namespace Xceed.Winform.Zip.Sample
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			Xceed.Zip.Licenser.LicenseKey = "LICENSE_PLACEHOLDER";
		}

		private async void CompressFileButton_Click( object sender, EventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "All files (*.*)|*.*",
				Title = "Select a file or folder",
				CheckFileExists = false,
				CheckPathExists = true,
				Multiselect = true
			};
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Filter = "Zip files (*.zip)|*.zip",
					Title = "Save compressed file"
				};
				if( saveFileDialog.ShowDialog() == DialogResult.OK )
				{
					string zipFilePath = saveFileDialog.FileName;
					try
					{
						this.Enabled = false;
						progressBar1.Visible = true;
						await Task.Run( () =>
						{
							var z = new ZipArchive( new DiskFile( zipFilePath ) );
							foreach( var item in openFileDialog.FileNames )
							{
								var diskFile = new DiskFile( item.ToString() );
								diskFile.CopyTo( z, true );
							}
						} );
						this.Enabled = true;
						progressBar1.Visible = false; 
						if( openFileDialog.FileNames.Count() > 1 )
						{
							MessageBox.Show( "Files compressed and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
						}
						else
						{
							MessageBox.Show( "File compressed and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
						}
					}
					catch( Exception ex )
					{
						MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
					}
				}
			}
		}

		private async void CompressFolderButton_Click( object sender, EventArgs e )
		{
			var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
			{
				Description = "Select a folder to compress"
			};

			if( folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				string selectedFolderPath = folderBrowserDialog.SelectedPath;
				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					Filter = "Zip files (*.zip)|*.zip",
					Title = "Save compressed file"
				};

				if( saveFileDialog.ShowDialog() == DialogResult.OK )
				{
					string zipFilePath = saveFileDialog.FileName;
					try
					{
						this.Enabled = false;
						progressBar1.Visible = true;
						await Task.Run( () => {
							var z = new ZipArchive( new DiskFile( zipFilePath ) );

							var diskfolder = new DiskFolder( selectedFolderPath );
							diskfolder.CopyTo( z, true );
						} );
						this.Enabled = true;
						progressBar1.Visible = false;
						MessageBox.Show( "Folder compressed and saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}
					catch( Exception ex )
					{
						MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
					}
				}
			}
		}

		private async void ListZipContentButton_Click( object sender, EventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Zip files (*.zip)|*.zip",
				Title = "Select a .zip file",
				CheckFileExists = true,
				CheckPathExists = true
			};
			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				string fileName = openFileDialog.FileName;
				listBox1.Items.Clear();
				this.Enabled = false;
				progressBar1.Visible = true;
				try
				{
					await Task.Run( () =>
					{
						ZipArchive zip = new ZipArchive( new DiskFile( fileName ) );
						foreach( AbstractFile f in zip.GetFiles( true ) )
						{
							Invoke( new Action( () =>
							{
								listBox1.Items.Add( f );
							} ) );
						}
					} );
					this.Enabled = true;
					progressBar1.Visible = false;
				}
				catch( Exception ex )
				{
					MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}

		private async void UnzipFilesButton_Click( object sender, EventArgs e )
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Zip files (*.zip)|*.zip",
				Title = "Select a zip file",
				CheckFileExists = false,
				CheckPathExists = true,
				Multiselect = false
			};

			if( openFileDialog.ShowDialog() == DialogResult.OK )
			{
				var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
				{
					Description = "Select a folder to decompress"
				};
				if( folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK )
				{
					try
					{
						this.Enabled = false;
						progressBar1.Visible = true; await Task.Run( () =>
						{
							var zip = new ZipArchive( new DiskFile( openFileDialog.FileName ) );
							DiskFolder folder = new DiskFolder( folderBrowserDialog.SelectedPath );
							zip.CopyFilesTo( folder, true, true );
						} );
						this.Enabled = true;
						progressBar1.Visible = false;
						MessageBox.Show( "Files decompressed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}
					catch( Exception ex )
					{
						MessageBox.Show( $"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
					}
				}
			}
		}
	}
}
