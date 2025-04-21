using Pastel;
using Xceed.FileSystem;
using Xceed.Zip;

Console.Title = "Xceed Zip Sample for .NET";
Xceed.Zip.Licenser.LicenseKey = "LICENSE_PLACEHOLDER";

bool opened = false;
while( true )
{
	if( !opened )
	{
		PrintMenu();
	}
	var key = Console.ReadKey().KeyChar;
	Console.Clear();
	opened = true;
	PrintMenu();
	string filePath = "LoremIpsum.txt";
	string zipFilePathForFile = "LoremIpsum.zip";

	string projectDirectory = Directory.GetCurrentDirectory();
	string zipFolderPath = Path.Combine( projectDirectory, "ProjectFolder.zip" );

	string zipFilePath = Path.Combine( projectDirectory, zipFolderPath );
	string decompressedFolderPath = Path.Combine( projectDirectory, "Decompressed" );


	switch( key )
	{
		case '1':
			CreateCompressAndDeleteTextFile( filePath, zipFilePathForFile );
			break;
		case '2':
			CompressFolder( projectDirectory, zipFolderPath );
			break;
		case '3':
			ListZipContent( zipFolderPath );
			break;
		case '4':
			DecompressZipFile( zipFilePath, decompressedFolderPath );
			break;
		case '0':
			return;
		default:
			Console.WriteLine( "Invalid option. Try again.".Pastel( "#FF0000" ) );
			break;
	}

	static void PrintMenu()
	{
		Console.WriteLine( "Choose an option:".Pastel( "#FE671A" ) );
		Console.WriteLine( "1 - To Select a file and save as zip".Pastel( "#FE671A" ) );
		Console.WriteLine( "2 - To Select a folder and save as zip".Pastel( "#FE671A" ) );
		Console.WriteLine( "3 - To Select a zip to list the content (execute option 2 first to list the files)".Pastel( "#FE671A" ) );
		Console.WriteLine( "4 - Select a zip file to decompress (execute option 2 first to generate the zip to decompress).".Pastel( "#FE671A" ) );
		Console.WriteLine( "0 - Exit".Pastel( "#FE671A" ) );
	}


	static void CreateCompressAndDeleteTextFile( string filePath, string outputZipPath )
	{
		string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
			"Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
			"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
			"Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
			"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. " +
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
			"Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
			"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
			"Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
			"Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

		File.WriteAllText( filePath, loremIpsum );
		Console.WriteLine( $"Text file created at: {filePath}".Pastel( "#33B0A4" ) );

		AbstractFile zipFile = new DiskFile( outputZipPath );

		if( zipFile.Exists )
			zipFile.Delete();

		var zip = new ZipArchive( zipFile );
		var diskFile = new DiskFile( filePath );

		diskFile.CopyTo( zip, true );
		Console.WriteLine( $"File compressed at: {outputZipPath}".Pastel( "#33B0A4" ) );

		if( File.Exists( filePath ) )
		{
			File.Delete( filePath );
			Console.WriteLine( $"File {filePath} deleted after compression.".Pastel( "#33B0A4" ) );
		}
		else
		{
			Console.WriteLine( $"File {filePath} does not exist.".Pastel( "#33B0A4" ) );
		}
	}

	static void CompressFolder( string folderPath, string outputZipPath )
	{
		AbstractFile zipFile = new DiskFile( outputZipPath );

		if( zipFile.Exists )
			zipFile.Delete();

		var zip = new ZipArchive( zipFile );

		var folder = new DiskFolder( folderPath );
		folder.CopyTo( zip, true );
		Console.WriteLine( $"Folder compressed at: {outputZipPath}".Pastel( "#33B0A4" ) );
	}
}


static void DecompressZipFile( string zipFilePath, string outputFolderPath )
{
	AbstractFile zipFile = new DiskFile( zipFilePath );
	var destinationFolder = new DiskFolder( outputFolderPath );

	if( !Directory.Exists( outputFolderPath ) )
	{
		Directory.CreateDirectory( outputFolderPath );
	}
	else
	{
		Directory.Delete( outputFolderPath, true );
		Directory.CreateDirectory( outputFolderPath );
	}

	var zip = new ZipArchive( zipFile );

	zip.CopyFilesTo( destinationFolder, true, true );
	Console.WriteLine( $"Zip file decompressed to: {outputFolderPath}".Pastel( "#33B0A4" ) );
}

void ListZipContent( string zipFolderPath )
{
	var zip = new ZipArchive( new DiskFile( zipFolderPath ) );
	Console.WriteLine( "Elements inside the zip: ".Pastel( "#33B0A4" ) );
	foreach( AbstractFile f in zip.GetFiles( true ) )
	{
		Console.WriteLine( f.Name.Pastel( "#33B0A4" ) );
	}
}