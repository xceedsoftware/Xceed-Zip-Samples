/* Xceed Streaming Compression Library - Compression Manager sample
 * Copyright (c) 2002 Xceed Software Inc.
 * 
 * [Manager.cs]
 * 
 * This sample demonstrates how to compress and decompress a file using 
 * different kinds of Compression formats. 
 *
 * This file is part of the Xceed Streaming Compression Library sample applications.
 * The source code in this file is only intended as a supplement to Xceed
 * Streaming Compression Library's documentation, and is provided "as is", without
 * warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Xceed.Compression;
using Xceed.Compression.Formats;

namespace Manager
{
  /// <summary>
  /// Summary description for Manager.
  /// </summary>
  public class Manager : System.Windows.Forms.Form
  {
    private System.Windows.Forms.MainMenu mainMenu1;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuFileQuit;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.Button btnDecompress;
    private System.Windows.Forms.Button btnCompress;
    private System.Windows.Forms.Button btnSelectDestinationFile;
    private System.Windows.Forms.Button btnSelectSourceFile;
    private System.Windows.Forms.TextBox txtDestinationFile;
    private System.Windows.Forms.TextBox txtSourceFile;
    private System.Windows.Forms.Label lblDestinationFile;
    private System.Windows.Forms.Label lblSourceFile;
    private System.Windows.Forms.Label lblCompressionFormat;
    private System.Windows.Forms.ComboBox cboCompressionFormat;
    private System.Windows.Forms.Label lblErrorWarnings;
    private System.Windows.Forms.TextBox txtMessage;
    private System.Windows.Forms.Label lblCompressionMethod;
    private System.Windows.Forms.ComboBox cboCompressionMethod;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    public Manager()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      //
      // TODO: Add any constructor code after InitializeComponent call
      //
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if (components != null) 
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

		#region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.mainMenu1 = new System.Windows.Forms.MainMenu();
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuFileQuit = new System.Windows.Forms.MenuItem();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.btnDecompress = new System.Windows.Forms.Button();
      this.btnCompress = new System.Windows.Forms.Button();
      this.btnSelectDestinationFile = new System.Windows.Forms.Button();
      this.btnSelectSourceFile = new System.Windows.Forms.Button();
      this.txtDestinationFile = new System.Windows.Forms.TextBox();
      this.txtSourceFile = new System.Windows.Forms.TextBox();
      this.lblDestinationFile = new System.Windows.Forms.Label();
      this.lblSourceFile = new System.Windows.Forms.Label();
      this.lblCompressionFormat = new System.Windows.Forms.Label();
      this.cboCompressionFormat = new System.Windows.Forms.ComboBox();
      this.lblErrorWarnings = new System.Windows.Forms.Label();
      this.txtMessage = new System.Windows.Forms.TextBox();
      this.lblCompressionMethod = new System.Windows.Forms.Label();
      this.cboCompressionMethod = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                              this.menuFile});
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                             this.menuFileQuit});
      this.menuFile.Text = "File";
      // 
      // menuFileQuit
      // 
      this.menuFileQuit.Index = 0;
      this.menuFileQuit.Text = "Quit";
      this.menuFileQuit.Click += new System.EventHandler(this.menuFileQuit_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.FileName = "doc1";
      // 
      // btnDecompress
      // 
      this.btnDecompress.Location = new System.Drawing.Point(400, 112);
      this.btnDecompress.Name = "btnDecompress";
      this.btnDecompress.Size = new System.Drawing.Size(82, 23);
      this.btnDecompress.TabIndex = 27;
      this.btnDecompress.Text = "Decompress";
      this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
      // 
      // btnCompress
      // 
      this.btnCompress.Location = new System.Drawing.Point(304, 112);
      this.btnCompress.Name = "btnCompress";
      this.btnCompress.Size = new System.Drawing.Size(82, 23);
      this.btnCompress.TabIndex = 26;
      this.btnCompress.Text = "Compress";
      this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
      // 
      // btnSelectDestinationFile
      // 
      this.btnSelectDestinationFile.Location = new System.Drawing.Point(456, 80);
      this.btnSelectDestinationFile.Name = "btnSelectDestinationFile";
      this.btnSelectDestinationFile.Size = new System.Drawing.Size(24, 23);
      this.btnSelectDestinationFile.TabIndex = 25;
      this.btnSelectDestinationFile.Text = "...";
      this.btnSelectDestinationFile.Click += new System.EventHandler(this.btnSelectDestinationFile_Click);
      // 
      // btnSelectSourceFile
      // 
      this.btnSelectSourceFile.Location = new System.Drawing.Point(456, 56);
      this.btnSelectSourceFile.Name = "btnSelectSourceFile";
      this.btnSelectSourceFile.Size = new System.Drawing.Size(24, 23);
      this.btnSelectSourceFile.TabIndex = 24;
      this.btnSelectSourceFile.Text = "...";
      this.btnSelectSourceFile.Click += new System.EventHandler(this.btnSelectSourceFile_Click);
      // 
      // txtDestinationFile
      // 
      this.txtDestinationFile.Location = new System.Drawing.Point(128, 80);
      this.txtDestinationFile.Name = "txtDestinationFile";
      this.txtDestinationFile.Size = new System.Drawing.Size(320, 20);
      this.txtDestinationFile.TabIndex = 23;
      this.txtDestinationFile.Text = "";
      // 
      // txtSourceFile
      // 
      this.txtSourceFile.AllowDrop = true;
      this.txtSourceFile.Location = new System.Drawing.Point(128, 56);
      this.txtSourceFile.Name = "txtSourceFile";
      this.txtSourceFile.Size = new System.Drawing.Size(320, 20);
      this.txtSourceFile.TabIndex = 22;
      this.txtSourceFile.Text = "";
      this.txtSourceFile.DragOver += new System.Windows.Forms.DragEventHandler(this.txtSourceFile_DragOver);
      this.txtSourceFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtSourceFile_DragDrop);
      // 
      // lblDestinationFile
      // 
      this.lblDestinationFile.AutoSize = true;
      this.lblDestinationFile.Location = new System.Drawing.Point(8, 80);
      this.lblDestinationFile.Name = "lblDestinationFile";
      this.lblDestinationFile.Size = new System.Drawing.Size(79, 13);
      this.lblDestinationFile.TabIndex = 21;
      this.lblDestinationFile.Text = "Destination file";
      // 
      // lblSourceFile
      // 
      this.lblSourceFile.AutoSize = true;
      this.lblSourceFile.Location = new System.Drawing.Point(8, 56);
      this.lblSourceFile.Name = "lblSourceFile";
      this.lblSourceFile.Size = new System.Drawing.Size(58, 13);
      this.lblSourceFile.TabIndex = 20;
      this.lblSourceFile.Text = "Source file";
      // 
      // lblCompressionFormat
      // 
      this.lblCompressionFormat.AutoSize = true;
      this.lblCompressionFormat.Location = new System.Drawing.Point(8, 8);
      this.lblCompressionFormat.Name = "lblCompressionFormat";
      this.lblCompressionFormat.Size = new System.Drawing.Size(106, 13);
      this.lblCompressionFormat.TabIndex = 19;
      this.lblCompressionFormat.Text = "Compression format";
      // 
      // cboCompressionFormat
      // 
      this.cboCompressionFormat.Items.AddRange(new object[] {
                                                              "Standard",
                                                              "GZip",
                                                              "ZLib"});
      this.cboCompressionFormat.Location = new System.Drawing.Point(128, 8);
      this.cboCompressionFormat.Name = "cboCompressionFormat";
      this.cboCompressionFormat.Size = new System.Drawing.Size(200, 21);
      this.cboCompressionFormat.TabIndex = 18;
      this.cboCompressionFormat.SelectedIndexChanged += new System.EventHandler(this.cboCompressionFormat_SelectedIndexChanged);
      // 
      // lblErrorWarnings
      // 
      this.lblErrorWarnings.AutoSize = true;
      this.lblErrorWarnings.Location = new System.Drawing.Point(8, 128);
      this.lblErrorWarnings.Name = "lblErrorWarnings";
      this.lblErrorWarnings.Size = new System.Drawing.Size(130, 13);
      this.lblErrorWarnings.TabIndex = 29;
      this.lblErrorWarnings.Text = "Error / Warning message";
      // 
      // txtMessage
      // 
      this.txtMessage.Location = new System.Drawing.Point(8, 152);
      this.txtMessage.Multiline = true;
      this.txtMessage.Name = "txtMessage";
      this.txtMessage.Size = new System.Drawing.Size(480, 104);
      this.txtMessage.TabIndex = 28;
      this.txtMessage.Text = "";
      // 
      // lblCompressionMethod
      // 
      this.lblCompressionMethod.AutoSize = true;
      this.lblCompressionMethod.Location = new System.Drawing.Point(8, 32);
      this.lblCompressionMethod.Name = "lblCompressionMethod";
      this.lblCompressionMethod.Size = new System.Drawing.Size(112, 13);
      this.lblCompressionMethod.TabIndex = 31;
      this.lblCompressionMethod.Text = "Compression method";
      // 
      // cboCompressionMethod
      // 
      this.cboCompressionMethod.Location = new System.Drawing.Point(128, 32);
      this.cboCompressionMethod.Name = "cboCompressionMethod";
      this.cboCompressionMethod.Size = new System.Drawing.Size(200, 21);
      this.cboCompressionMethod.TabIndex = 30;
      // 
      // Manager
      // 
      this.AllowDrop = true;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(504, 275);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.lblCompressionMethod,
                                                                  this.cboCompressionMethod,
                                                                  this.lblErrorWarnings,
                                                                  this.lblDestinationFile,
                                                                  this.lblSourceFile,
                                                                  this.lblCompressionFormat,
                                                                  this.txtMessage,
                                                                  this.btnDecompress,
                                                                  this.btnCompress,
                                                                  this.btnSelectDestinationFile,
                                                                  this.btnSelectSourceFile,
                                                                  this.txtDestinationFile,
                                                                  this.txtSourceFile,
                                                                  this.cboCompressionFormat});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Menu = this.mainMenu1;
      this.Name = "Manager";
      this.Text = "Compress / Decompress manager";
      this.Load += new System.EventHandler(this.Manager_Load);
      this.DragOver += new System.Windows.Forms.DragEventHandler(this.Manager_DragOver);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Manager_DragDrop);
      this.ResumeLayout(false);

    }
		#endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() 
    {
      /* ================================
       * How to license Xceed components 
       * ================================       
       * To license your product, set the LicenseKey property to a valid trial or registered license key 
       * in the main entry point of the application to ensure components are licensed before any of the 
       * component methods are called.      
       * 
       * If the component is used in a DLL project (no entry point available), it is 
       * recommended that the LicenseKey property be set in a static constructor of a 
       * class that will be accessed systematically before any component is instantiated or, 
       * you can simply set the LicenseKey property immediately BEFORE you instantiate 
       * an instance of the component.
       * 
       * For instance, if you wanted to deploy this sample, the license key needs to be set here.
       * If your trial period has expired, you must purchase a registered license key,
       * uncomment the next line of code, and insert your registerd license key.
       * For more information, consult the "How the 45-day trial works" and the 
       * "How to license the component once you purchase" topics in the documentation of this product.
       */
        
      // Xceed.Compression.Formats.Licenser.LicenseKey = "SCNXX-XXXXX-XXXXX-XXXX"; // Uncomment and set license key here to deploy 

      Application.Run(new Manager());
    }

    // ------------------------------------------------------------------------------------
    // Initialize the default or last saved options
    // ------------------------------------------------------------------------------------
    private void Manager_Load(object sender, System.EventArgs e)
    {
      cboCompressionFormat.SelectedIndex = 0;
    }

    // -----------------------------------------------------------------------------------
    // Let the user select a source file name and path using a file open
    // dialog box
    // ------------------------------------------------------------------------------------
    private void btnSelectSourceFile_Click(object sender, System.EventArgs e)
    {
      openFileDialog1.FileName = "";
      openFileDialog1.Title = "Source file";
      openFileDialog1.Filter = "All type (*.*)|*.*";
      openFileDialog1.FilterIndex = 0;
      DialogResult result = this.openFileDialog1.ShowDialog( this );

      if( result == DialogResult.OK )
      {
        txtSourceFile.Text = openFileDialog1.FileName;
        SetDestinationFileName();
      }
    }

    // ------------------------------------------------------------------------------------
    // Let the user select a destination file name and path using a file save
    // dialog box
    // ------------------------------------------------------------------------------------
    private void btnSelectDestinationFile_Click(object sender, System.EventArgs e)
    {
      openFileDialog1.FileName = "";
      openFileDialog1.Title = "Destination file";
      openFileDialog1.Filter = "Compressed (*.gz;*.std;*.zp3;*.zl;*.dfl;*.sto)|*.gz;*.std;*.zip;*.zl;*.dfl;*.sto|" +
        "All type (*.*)|*.*";
      openFileDialog1.FilterIndex = 0;
      DialogResult result = this.saveFileDialog1.ShowDialog( this );

      if( result == DialogResult.OK )
      {
        txtDestinationFile.Text = saveFileDialog1.FileName;
      }

    }

    // ------------------------------------------------------------------------------------
    // Compress the selected source file to the specified destination file
    // ------------------------------------------------------------------------------------
    private void btnCompress_Click(object sender, System.EventArgs e)
    {
     
      if( CompressFile( txtSourceFile.Text, txtDestinationFile.Text) )
      {
        // If the Compression is successful, empty the source and destination
        // text box to simplify subsequent Compression/Decompression.
        txtSourceFile.Text = "";
        txtDestinationFile.Text = "";
      }
    }

    // -----------------------------------------------------------------------------------
    // Decompress the selected source file to the specified destination file
    // ------------------------------------------------------------------------------------
    private void btnDecompress_Click(object sender, System.EventArgs e)
    {
      if( DecompressFile( txtSourceFile.Text, txtDestinationFile.Text ) )
      {
        //If the Decompression is successful, empty the source and destination
        //text box to simplify subsequent Compression/Decompression.
        txtSourceFile.Text = "";
        txtDestinationFile.Text = "";
      }
    }

    // ------------------------------------------------------------------------------------
    // Allow a drag and drop of a file in the form
    // ------------------------------------------------------------------------------------
    private void Manager_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      CompressDragDrop( e );
    }
    
    private void Manager_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
      CompressDragOver( e );
    }

    // ------------------------------------------------------------------------------------
    // Quit the sample application
    // ------------------------------------------------------------------------------------
    private void menuFileQuit_Click(object sender, System.EventArgs e)
    {
      Close();
    }

    // ------------------------------------------------------------------------------------
    // Initialize the destination file to a default value if the destination
    // text box is empty.
    // ------------------------------------------------------------------------------------
    private void txtDestinationFile_Leave(object sender, System.EventArgs e)
    {
      SetDestinationFileName();    
    }

    // ------------------------------------------------------------------------------------
    // Allow a drag and drop of a file in the source text box
    // ------------------------------------------------------------------------------------
    private void txtSourceFile_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
    {
      CompressDragDrop( e );
    }

    private void txtSourceFile_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
    {
      CompressDragOver( e );
    }

    // ------------------------------------------------------------------------------------
    // Method that perform the actual Compression of a source file to a destination file
    // ------------------------------------------------------------------------------------
    private bool CompressFile( string sourceFileName, string compressedFileName )
    {
      bool compressedFile = false;
      
      try
      {
        
        this.Cursor = Cursors.WaitCursor;

        using( Stream sourceFile = new FileStream( sourceFileName, FileMode.Open ) )
        {
          using( Stream destinationFile = new FileStream( compressedFileName, FileMode.Create ) )
          {
            byte[] buffer = new byte[ 32768 ];
            int read = 0;
            CompressionMethod method;

            switch( cboCompressionFormat.SelectedItem.ToString() )
            {
              case "Standard":
              
               method = ( CompressionMethod )Enum.Parse( typeof( CompressionMethod ), cboCompressionMethod.SelectedItem.ToString() );

                using( XceedCompressedStream standard = new XceedCompressedStream( destinationFile, method, CompressionLevel.Highest ) )
                {
                  while( ( read = sourceFile.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    standard.Write( buffer, 0, read );
                  }
                }
                break;

              case "GZip":
                using( GZipCompressedStream gzip = new GZipCompressedStream( destinationFile ) )
                {
                  while( ( read = sourceFile.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    gzip.Write( buffer, 0, read );
                  }
                }
                break;
                
              case "ZLib":

               method = ( CompressionMethod )Enum.Parse( typeof( CompressionMethod ), cboCompressionMethod.SelectedItem.ToString() );

                using( ZLibCompressedStream zlib = new ZLibCompressedStream( destinationFile, method, CompressionLevel.Highest ) )
                {
                  while( ( read = sourceFile.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    zlib.Write( buffer, 0, read );
                  }
                }
                break;
            }
          }
        }
      
        compressedFile = true;
        txtMessage.Text = sourceFileName + " successfully compressed in " + compressedFileName;
      }
      catch( Exception exception )
      {
        txtMessage.Text = "Failed to compress file " + sourceFileName + Environment.NewLine + exception.Message;
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }

      return compressedFile;
    }

    // ------------------------------------------------------------------------------------
    // Function that perform the actual Decompression of a source file to a destination file
    // ------------------------------------------------------------------------------------
    private bool DecompressFile( string sourceFileName, string decompressedFileName )
    {
      bool decompressedFile = false;
      
      try
      {
        
        this.Cursor = Cursors.WaitCursor;

        using( Stream sourceFile = new FileStream( sourceFileName, FileMode.Open ) )
        {
          using( Stream destinationFile = new FileStream( decompressedFileName, FileMode.Create ) )
          {
            byte[] buffer = new byte[ 32768 ];
            int read = 0;

            switch( cboCompressionFormat.SelectedItem.ToString() )
            {
              case "Standard":
              
                using( XceedCompressedStream standard = new XceedCompressedStream( sourceFile ) )
                {
                  while( ( read = standard.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    destinationFile.Write( buffer, 0, read );
                  }
                }
                break;

              case "GZip":
                using( GZipCompressedStream gzip = new GZipCompressedStream( sourceFile ) )
                {
                  while( ( read = gzip.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    destinationFile.Write( buffer, 0, read );
                  }
                }
                break;

              case "ZLib":
                using( ZLibCompressedStream zlib = new ZLibCompressedStream( sourceFile ) )
                {
                  while( ( read = zlib.Read( buffer, 0, buffer.Length ) ) > 0 )
                  {
                    destinationFile.Write( buffer, 0, read );
                  }
                }
                break;
            }
          }
        }
      
        decompressedFile = true;
        txtMessage.Text = sourceFileName + " successfully decompressed in " + decompressedFileName;
      }
      catch( Exception exception )
      {
        txtMessage.Text = "Failed to decompress file " + sourceFileName + Environment.NewLine + exception.Message;
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }

      return decompressedFile;
    }

    // ------------------------------------------------------------------------------------
    // Method that perform the actual drop of a file on the form or the source text box
    // ------------------------------------------------------------------------------------
    private void CompressDragDrop( DragEventArgs e )
    {
      if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
      {
        System.Array file = ( System.Array )e.Data.GetData( DataFormats.FileDrop );
        txtSourceFile.Text = ( string )file.GetValue(0);
        SetDestinationFileName();
      }
    }

    private void CompressDragOver( DragEventArgs e )
    {
      if( e.Data.GetDataPresent( DataFormats.FileDrop ) )
      {
        e.Effect = DragDropEffects.Copy;
      }
      else
      {
        e.Effect = DragDropEffects.None;      
      }
    }

    // ------------------------------------------------------------------------------------
    // Assign a default value to the destination file name if the destination text box
    // is empty.
    // ------------------------------------------------------------------------------------
    private void SetDestinationFileName()
    {
      string compressedFilename = txtDestinationFile.Text;

      if( compressedFilename == string.Empty )
      {
        compressedFilename = RemoveFileExtension( txtSourceFile.Text );

        switch( cboCompressionFormat.SelectedItem.ToString() )
        {
          case "Standard":
            txtDestinationFile.Text = compressedFilename + ".std";
            break;

          case "GZip":
            txtDestinationFile.Text = compressedFilename + ".gz";
            break;

          case "ZLib":
            txtDestinationFile.Text = compressedFilename + ".zl";
            break;
        }
      }
    }

    // ------------------------------------------------------------------------------------
    // Returns the path and file name without its extension.
    // ------------------------------------------------------------------------------------
    private string RemoveFileExtension( string fileName )
    {
      string newFileName = fileName;

      int end = fileName.LastIndexOf( "." );

      if( end != 0 )
      {
        newFileName = fileName.Substring( 0, end );
      }
      
      return newFileName;
    }

    // ------------------------------------------------------------------------------------
    // Selects the proper compression methods.
    // ------------------------------------------------------------------------------------
    private void cboCompressionFormat_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      cboCompressionMethod.Items.Clear();

      switch( cboCompressionFormat.SelectedItem.ToString() )
      {
        case "Standard":
          cboCompressionMethod.Enabled = true;
          cboCompressionMethod.Items.Add( "Deflated" );
          cboCompressionMethod.Items.Add( "Deflated64" );
          cboCompressionMethod.Items.Add( "Stored" );
          cboCompressionMethod.SelectedIndex = 0;
          break;

        case "GZip":
          cboCompressionMethod.Text = string.Empty;
          cboCompressionMethod.Enabled = false;
          break;

        case "ZLib":
          cboCompressionMethod.Enabled = true;
          cboCompressionMethod.Items.Add( "Deflated" );
          cboCompressionMethod.Items.Add( "Deflated64" );
          cboCompressionMethod.SelectedIndex = 0;
          break;
      }
    }

  }
}
