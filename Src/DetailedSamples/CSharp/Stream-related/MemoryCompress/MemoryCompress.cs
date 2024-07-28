/* Xceed Streaming Compression Library - Memory Compress sample
 *  Copyright (c) 2002 Xceed Software Inc.
 *
 * [MemoryCompress.cs]
 *
 * This sample demonstrates how to compress a chunk of memory data 
 * using different kinds of compression formats, and decompress a 
 * compressed memory data. 
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

namespace MemoryCompress
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MemoryCompress : System.Windows.Forms.Form
	{
    private System.Windows.Forms.Label lblText;
    private System.Windows.Forms.TextBox txtTextToCompress;
    private System.Windows.Forms.Label lblCompressionFormat;
    private System.Windows.Forms.ComboBox cboCompressionFormat;
    private System.Windows.Forms.Button btnCompress;
    private System.Windows.Forms.Button btnDecompress;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblCompressedSizeValue;
    private System.Windows.Forms.Label lblOriginalSizeValue;
    private System.Windows.Forms.Label lblOriginalSize;
    private System.Windows.Forms.Label lblDecompressedText;
    private System.Windows.Forms.Button btnQuit;
    private System.Windows.Forms.TextBox txtDecompressed;
    private System.Windows.Forms.Label lblCompressionMethod;
    private System.Windows.Forms.ComboBox cboCompressionMethod;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MemoryCompress()
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
      this.lblText = new System.Windows.Forms.Label();
      this.txtTextToCompress = new System.Windows.Forms.TextBox();
      this.lblCompressionFormat = new System.Windows.Forms.Label();
      this.cboCompressionFormat = new System.Windows.Forms.ComboBox();
      this.btnCompress = new System.Windows.Forms.Button();
      this.btnDecompress = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.lblCompressedSizeValue = new System.Windows.Forms.Label();
      this.lblOriginalSizeValue = new System.Windows.Forms.Label();
      this.lblOriginalSize = new System.Windows.Forms.Label();
      this.txtDecompressed = new System.Windows.Forms.TextBox();
      this.lblDecompressedText = new System.Windows.Forms.Label();
      this.btnQuit = new System.Windows.Forms.Button();
      this.lblCompressionMethod = new System.Windows.Forms.Label();
      this.cboCompressionMethod = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // lblText
      // 
      this.lblText.AutoSize = true;
      this.lblText.Location = new System.Drawing.Point(8, 8);
      this.lblText.Name = "lblText";
      this.lblText.Size = new System.Drawing.Size(91, 13);
      this.lblText.TabIndex = 0;
      this.lblText.Text = "Text to compress";
      // 
      // txtTextToCompress
      // 
      this.txtTextToCompress.Location = new System.Drawing.Point(8, 24);
      this.txtTextToCompress.Multiline = true;
      this.txtTextToCompress.Name = "txtTextToCompress";
      this.txtTextToCompress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtTextToCompress.Size = new System.Drawing.Size(320, 120);
      this.txtTextToCompress.TabIndex = 1;
      this.txtTextToCompress.Text = "This is a little test to show you how the memory compression works.\r\nAnd it is ve" +
        "ry easy to use.\r\nAnd there is some repeating \r\nrepeating repeating repeating rep" +
        "eating repeating repeating \r\nrepeating text.";
      this.txtTextToCompress.TextChanged += new System.EventHandler(this.txtTextToCompress_TextChanged);
      // 
      // lblCompressionFormat
      // 
      this.lblCompressionFormat.AutoSize = true;
      this.lblCompressionFormat.Location = new System.Drawing.Point(8, 184);
      this.lblCompressionFormat.Name = "lblCompressionFormat";
      this.lblCompressionFormat.Size = new System.Drawing.Size(106, 13);
      this.lblCompressionFormat.TabIndex = 2;
      this.lblCompressionFormat.Text = "Compression format";
      // 
      // cboCompressionFormat
      // 
      this.cboCompressionFormat.Items.AddRange(new object[] {
                                                              "Standard",
                                                              "GZip",
                                                              "ZLib"});
      this.cboCompressionFormat.Location = new System.Drawing.Point(128, 184);
      this.cboCompressionFormat.Name = "cboCompressionFormat";
      this.cboCompressionFormat.Size = new System.Drawing.Size(200, 21);
      this.cboCompressionFormat.TabIndex = 3;
      this.cboCompressionFormat.SelectedIndexChanged += new System.EventHandler(this.cboCompressionFormat_SelectedIndexChanged);
      // 
      // btnCompress
      // 
      this.btnCompress.Location = new System.Drawing.Point(8, 248);
      this.btnCompress.Name = "btnCompress";
      this.btnCompress.Size = new System.Drawing.Size(80, 23);
      this.btnCompress.TabIndex = 4;
      this.btnCompress.Text = "Compress";
      this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
      // 
      // btnDecompress
      // 
      this.btnDecompress.Location = new System.Drawing.Point(248, 248);
      this.btnDecompress.Name = "btnDecompress";
      this.btnDecompress.Size = new System.Drawing.Size(80, 23);
      this.btnDecompress.TabIndex = 5;
      this.btnDecompress.Text = "Decompress";
      this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(96, 256);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(92, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "Compressed size";
      // 
      // lblCompressedSizeValue
      // 
      this.lblCompressedSizeValue.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.lblCompressedSizeValue.Location = new System.Drawing.Point(200, 256);
      this.lblCompressedSizeValue.Name = "lblCompressedSizeValue";
      this.lblCompressedSizeValue.Size = new System.Drawing.Size(40, 13);
      this.lblCompressedSizeValue.TabIndex = 7;
      this.lblCompressedSizeValue.Text = "0";
      // 
      // lblOriginalSizeValue
      // 
      this.lblOriginalSizeValue.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.lblOriginalSizeValue.Location = new System.Drawing.Point(272, 152);
      this.lblOriginalSizeValue.Name = "lblOriginalSizeValue";
      this.lblOriginalSizeValue.Size = new System.Drawing.Size(56, 13);
      this.lblOriginalSizeValue.TabIndex = 9;
      this.lblOriginalSizeValue.Text = "0";
      // 
      // lblOriginalSize
      // 
      this.lblOriginalSize.AutoSize = true;
      this.lblOriginalSize.Location = new System.Drawing.Point(184, 152);
      this.lblOriginalSize.Name = "lblOriginalSize";
      this.lblOriginalSize.Size = new System.Drawing.Size(67, 13);
      this.lblOriginalSize.TabIndex = 8;
      this.lblOriginalSize.Text = "Original size";
      // 
      // txtDecompressed
      // 
      this.txtDecompressed.Location = new System.Drawing.Point(8, 304);
      this.txtDecompressed.Multiline = true;
      this.txtDecompressed.Name = "txtDecompressed";
      this.txtDecompressed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtDecompressed.Size = new System.Drawing.Size(320, 120);
      this.txtDecompressed.TabIndex = 11;
      this.txtDecompressed.Text = "";
      // 
      // lblDecompressedText
      // 
      this.lblDecompressedText.AutoSize = true;
      this.lblDecompressedText.Location = new System.Drawing.Point(8, 288);
      this.lblDecompressedText.Name = "lblDecompressedText";
      this.lblDecompressedText.Size = new System.Drawing.Size(102, 13);
      this.lblDecompressedText.TabIndex = 10;
      this.lblDecompressedText.Text = "Decompressed text";
      // 
      // btnQuit
      // 
      this.btnQuit.Location = new System.Drawing.Point(248, 440);
      this.btnQuit.Name = "btnQuit";
      this.btnQuit.Size = new System.Drawing.Size(80, 23);
      this.btnQuit.TabIndex = 12;
      this.btnQuit.Text = "Quit";
      this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
      // 
      // lblCompressionMethod
      // 
      this.lblCompressionMethod.AutoSize = true;
      this.lblCompressionMethod.Location = new System.Drawing.Point(8, 208);
      this.lblCompressionMethod.Name = "lblCompressionMethod";
      this.lblCompressionMethod.Size = new System.Drawing.Size(112, 13);
      this.lblCompressionMethod.TabIndex = 13;
      this.lblCompressionMethod.Text = "Compression method";
      // 
      // cboCompressionMethod
      // 
      this.cboCompressionMethod.Items.AddRange(new object[] {
                                                              "Standard",
                                                              "GZip",
                                                              "ZLib"});
      this.cboCompressionMethod.Location = new System.Drawing.Point(128, 208);
      this.cboCompressionMethod.Name = "cboCompressionMethod";
      this.cboCompressionMethod.Size = new System.Drawing.Size(200, 21);
      this.cboCompressionMethod.TabIndex = 14;
      // 
      // MemoryCompress
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(336, 471);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.lblCompressionMethod,
                                                                  this.cboCompressionMethod,
                                                                  this.btnQuit,
                                                                  this.txtDecompressed,
                                                                  this.lblDecompressedText,
                                                                  this.lblOriginalSize,
                                                                  this.label1,
                                                                  this.lblCompressionFormat,
                                                                  this.lblText,
                                                                  this.lblOriginalSizeValue,
                                                                  this.lblCompressedSizeValue,
                                                                  this.btnDecompress,
                                                                  this.btnCompress,
                                                                  this.cboCompressionFormat,
                                                                  this.txtTextToCompress});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MemoryCompress";
      this.Text = "Memory Compression";
      this.Load += new System.EventHandler(this.MemoryCompress_Load);
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

      Application.Run(new MemoryCompress());
    }

    // ------------------------------------------------------------------------------------
    // Initialize the original size label and fill the combo box
    // ------------------------------------------------------------------------------------
    private void MemoryCompress_Load(object sender, System.EventArgs e)
    {
      cboCompressionFormat.SelectedIndex = 0;
      lblOriginalSizeValue.Text = txtTextToCompress.Text.Length.ToString();
    }

    // ------------------------------------------------------------------------------------
    // Quit the sample application
    // ------------------------------------------------------------------------------------
    private void btnQuit_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    // ------------------------------------------------------------------------------------
    // Update the original size label when the user modify the text to compress text box
    // ------------------------------------------------------------------------------------
    private void txtTextToCompress_TextChanged(object sender, System.EventArgs e)
    {
     lblOriginalSizeValue.Text = txtTextToCompress.Text.Length.ToString();
    }

    // ------------------------------------------------------------------------------------
    // Do the compression of the text
    // ------------------------------------------------------------------------------------
    private void btnCompress_Click(object sender, System.EventArgs e)
    {
      if( txtTextToCompress.Text != string.Empty )
      {
       
        try
        {
          this.Cursor = Cursors.WaitCursor;
      
          byte[] bytesToCompress = System.Text.Encoding.Default.GetBytes( txtTextToCompress.Text );

          CompressionMethod method;

          using( MemoryStream destinationStream = new MemoryStream() )
          {
            switch( cboCompressionFormat.SelectedItem.ToString() )
            {
              case "Standard":                
                method = ( CompressionMethod )Enum.Parse( typeof( CompressionMethod ), cboCompressionMethod.SelectedItem.ToString() );

                using( XceedCompressedStream standard = new XceedCompressedStream( destinationStream, method, CompressionLevel.Highest ) )
                {  
                  standard.Write( bytesToCompress, 0, bytesToCompress.Length );
                }
                break;

              case "GZip":
                using( GZipCompressedStream gzip = new GZipCompressedStream( destinationStream ) )
                {
                  gzip.Write( bytesToCompress, 0, bytesToCompress.Length );
                }
                break;
                    
              case "ZLib":
                method = ( CompressionMethod )Enum.Parse( typeof( CompressionMethod ), cboCompressionMethod.SelectedItem.ToString() );

                using( ZLibCompressedStream zlib = new ZLibCompressedStream( destinationStream, method, CompressionLevel.Highest ) )
                {
                  zlib.Write( bytesToCompress, 0, bytesToCompress.Length );
                }
                break;
            }

            m_compressedData = destinationStream.ToArray();
            lblCompressedSizeValue.Text = m_compressedData.Length.ToString();
          
            btnCompress.Enabled = false;
            cboCompressionFormat.Enabled = false;

            if( cboCompressionMethod.Enabled )
             cboCompressionMethod.Enabled = false;
          }
        
        }
        catch( Exception exception )
        {
          MessageBox.Show( "Failed to compress text " + Environment.NewLine + exception.Message );
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }
      }
    }

    // ------------------------------------------------------------------------------------
    // Do the decompression of the compressed byte array
    // ------------------------------------------------------------------------------------
    private void btnDecompress_Click(object sender, System.EventArgs e)
    {
      if( m_compressedData.Length > 0 )
      {
        try
        {
          this.Cursor = Cursors.WaitCursor;

          using( MemoryStream sourceStream = new MemoryStream( m_compressedData ) )
          {
            string decompressedText = string.Empty;

            using( MemoryStream destinationStream = new MemoryStream () )
            {
              byte[] buffer = new byte[ 32768 ];
              int read = 0;

              switch( cboCompressionFormat.SelectedItem.ToString() )
              {
                case "Standard":
           
                  using( XceedCompressedStream standard = new XceedCompressedStream( sourceStream ))
                  {  
                    while( ( read = standard.Read( buffer, 0, buffer.Length ) ) > 0 )
                    {
                      destinationStream.Write( buffer, 0, read );
                    }

                    decompressedText = System.Text.Encoding.Default.GetString( destinationStream.ToArray() );
                  }
                  break;

                case "GZip":
                  using( GZipCompressedStream gzip = new GZipCompressedStream( sourceStream ) )
                  {
                    while( ( read = gzip.Read( buffer, 0, buffer.Length ) ) > 0 )
                    {
                      destinationStream.Write( buffer, 0, read );
                    }

                    decompressedText = System.Text.Encoding.Default.GetString( destinationStream.ToArray() );
                  }
                  break;
                    
                case "ZLib":
                  using( ZLibCompressedStream zlib = new ZLibCompressedStream( sourceStream ) )
                  {
                    while( ( read = zlib.Read( buffer, 0, buffer.Length ) ) > 0 )
                    {
                      destinationStream.Write( buffer, 0, read );
                    }

                    decompressedText = System.Text.Encoding.Default.GetString( destinationStream.ToArray() );
                  }
                  break;
              }

            }

            m_compressedData = new byte[0];
            lblCompressedSizeValue.Text = "0";
            txtDecompressed.Text = decompressedText;

            btnCompress.Enabled = true;
            cboCompressionFormat.Enabled = true;
           
            if( cboCompressionFormat.SelectedItem.ToString() != "GZip" )
              cboCompressionMethod.Enabled = true;
          }
        
        }
        catch( Exception exception )
        {
          MessageBox.Show( "Failed to compress text " + Environment.NewLine + exception.Message );
        }
        finally
        {
          this.Cursor = Cursors.Default;
        }

      }
    }

    // ------------------------------------------------------------------------------------
    // We clear the decompressed text.
    // ------------------------------------------------------------------------------------
    private void cboCompressionFormat_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if( txtDecompressed.Text != String.Empty )
        txtDecompressed.Text = string.Empty;

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

    private byte[] m_compressedData =  new byte[0];


	}
}
