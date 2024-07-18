/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [TarArchiveTreeViewNode.cs]
 * 
 * Implementation of the AbstractTreeViewNode class and represents a TarArchive.
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
using Xceed.Tar;
using Xceed.FileSystem.Samples.Utils.ListView;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public class TarArchiveTreeViewNode : AbstractTreeViewNode
  {
    #region CONSTRUCTORS

    public TarArchiveTreeViewNode( AbstractFile file )
      : this( file, null )
    {
    }
  
    public TarArchiveTreeViewNode( AbstractFile file, System.Windows.Forms.ListView contentListView )
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
        return m_tarArchive;
      }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    public override void InitializeFolder()
    {
      if( m_tarArchive != null )
        return;

      try
      {
        m_tarArchive = new TarArchive( m_item as AbstractFile );
      }
      catch( InvalidTarStructureException )
      {
        MessageBox.Show( "The Tar archive is either corrupted or not a valid archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
      catch( FileSystemNotSupportedException )
      {
        MessageBox.Show( "Unknown Tar archive format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
      catch
      {
        MessageBox.Show( "An error occured while reading the archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    /// <summary>
    /// Refresh the current item with this new AbstractFolder. 
    /// </summary>
    /// <param name="item">The new AbstractFolder that this item represent.</param>
    public override void Refresh( FileSystemItem item )
    {
      base.Refresh( item );

      // Refresh the archive if it was previously loaded.
      if( m_tarArchive != null )
      {
        m_tarArchive = null;

        this.InitializeFolder();
      }
    }

    #endregion PUBLIC METHODS

    #region PRIVATE FIELDS

    private TarArchive m_tarArchive; // = null

    #endregion PRIVATE FIELDS
  }
}