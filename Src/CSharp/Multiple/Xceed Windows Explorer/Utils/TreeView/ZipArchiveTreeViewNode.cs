/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [ZipArchiveTreeViewNode.cs]
 * 
 * Implementation of the AbstractTreeViewNode and represent a ZipArchive.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.IO;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.Zip;
using Xceed.FileSystem.Samples.Utils.ListView;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public class ZipArchiveTreeViewNode : AbstractTreeViewNode
  {
    #region CONSTRUCTORS

    public ZipArchiveTreeViewNode( AbstractFile file )
      : this( file, null )
    {
    }
  
    public ZipArchiveTreeViewNode( AbstractFile file, System.Windows.Forms.ListView contentListView )
    {
      if( file == null )
        throw new ArgumentNullException( "file" );

      m_item = file;
      m_contentListView = contentListView;

      this.RefreshIcon( false );
      this.RefreshText();

      // Display a [+] by default
      this.Nodes.Add( string.Empty );

      // Initialize the icon updater.
      m_iconUpdater = new TreeViewIconUpdater( this );
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    /// <summary>
    /// Gets the AbstractFolder this node represent.
    /// </summary>
    public override AbstractFolder Folder
    {
      get
      {
        return m_zipArchive;
      }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    public override void InitializeFolder()
    {
      if( m_zipArchive != null )
        return;

      try
      {
        m_zipArchive = new ZipArchive( m_item as AbstractFile );

        m_zipArchive.DefaultEncryptionPassword = Options.ZipDefaultEncryptionPassword;
        m_zipArchive.DefaultEncryptionMethod = Options.ZipDefaultEncryptionMethod;
        m_zipArchive.DefaultEncryptionStrength = Options.ZipDefaultEncryptionStrength;
        m_zipArchive.DefaultCompressionLevel = Options.ZipDefaultCompressionLevel;
        m_zipArchive.DefaultCompressionMethod = Options.ZipDefaultCompressionMethod;
  
        if( Options.ZipLastDecryptionPasswordUsed.Length > 0 )
          m_zipArchive.DefaultDecryptionPassword = Options.ZipLastDecryptionPasswordUsed;
      }
      catch( InvalidZipStructureException )
      {
        MessageBox.Show( "The Zip archive is either corrupted or not a valid archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
      catch
      {
        MessageBox.Show( "An error occured while reading the archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    /// <summary>
    /// Refresh the current item with this new AbstractFolder. 
    /// </summary>
    /// <param name="folder">The new AbstractFolder that this item represent.</param>
    public override void Refresh( FileSystemItem item )
    {
      base.Refresh( item );

      // Refresh the archive if it was previously loaded.
      if( m_zipArchive != null )
      {
        m_zipArchive = null;

        this.InitializeFolder();
      }
    }

    #endregion PUBLIC METHODS

    #region PRIVATE FIELDS

    private ZipArchive m_zipArchive; // = null

    #endregion PRIVATE FIELDS
  }
}