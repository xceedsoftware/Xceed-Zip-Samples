/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AbstractListViewItem.cs]
 * 
 * Abstract ListViewItem that expose a FileSystemItem and properties
 * to control the main toolbar's actions. It also allow to manipulate the
 * item. (copy, paste, delete, rename, etc.)
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.FileSystem.Samples.Utils.Icons;
using Xceed.FileSystem.Samples.Utils.TreeView;

namespace Xceed.FileSystem.Samples.Utils.ListView
{
	public abstract class AbstractListViewItem : ListViewItem
	{
    #region PUBLIC PROPERTIES

    public virtual AbstractTreeViewNode ParentNode
    { 
      get { return null; }
    }

    /// <summary>
    /// The FileSystemItem this ListViewItem represent.
    /// </summary>
    public FileSystemItem FileSystemItem
    {
      get{ return m_item; }
    }

    /// <summary>
    /// Determine if the "Copy" function should be enabled.
    /// </summary>
    public virtual bool CopyToolEnabled
    {
      get
      {
        AbstractFolder folder = m_item as AbstractFolder;

        // Copy is available for a file. 
        if( folder == null )
          return true;

        // Copy is available for folder if they are 
        // not the root folder.
        return ( !folder.IsRoot );
      }
    }

    /// <summary>
    /// Determine if the "Cut" function should be enabled.
    /// </summary>
    public virtual bool CutToolEnabled
    {
      get
      {
        AbstractFolder folder = m_item as AbstractFolder;

        // Cut is available for a file. 
        if( folder == null )
          return true;

        // Cut is available for folder if they are 
        // not the root folder.
        return ( !folder.IsRoot );
      }
    }

    /// <summary>
    /// Determine if the "Delete" function should be enabled.
    /// </summary>
    public virtual bool DeleteToolEnabled
    {
      get
      {
        AbstractFolder folder = m_item as AbstractFolder;

        // Delete is available for a file. 
        if( folder == null )
          return true;

        // Cut is available for folder if they are 
        // not the root folder.
        return ( !folder.IsRoot );
      }
    }

    /// <summary>
    /// Determine if the "Rename" function should be enabled.
    /// </summary>
    public virtual bool RenameToolEnabled
    {
      get
      {
        AbstractFolder folder = m_item as AbstractFolder;

        // Rename is available for a file. 
        if( folder == null )
          return true;

        // Rename is available for folder if they are 
        // not the root folder.
        return ( !folder.IsRoot );
      }
    }

    #endregion PUBLIC PROPERTIES

    #region PUBLIC METHODS

    /// <summary>
    /// Create the item physically and add it to the FolderTree if applicable.
    /// </summary>
    public virtual void Create()
    {
      m_item.Create();
    }

    /// <summary>
    /// Delete the item physically and remove it from the FolderTree if applicable.
    /// </summary>
    /// <param name="confirmDelete">Control if we show a confirmation message before deleting.</param>
    public abstract void Delete( bool confirmDelete );
    
    /// <summary>
    /// Refresh the current item with this new FileSystemItem. A refresh to the matching
    /// FolderTree node will be made if applicable.
    /// </summary>
    /// <param name="item">The new FileSystemItem that this item represent.</param>
    public abstract void Refresh( FileSystemItem item );
    
    /// <summary>
    /// Rename the item physically and the matching FolderTree node if applicable.
    /// </summary>
    /// <param name="name">The new name for the item.</param>
    public abstract void Rename( string name );

    /// <summary>
    /// Refresh the icon.
    /// </summary>
    /// <param name="forceRefresh">Determine if we force a cache refresh of the icon.</param>
    public void RefreshIcon( bool forceRefresh )
    {
      // Set the item icon index. 
      int index = IconCache.GetIconIndex( m_item, false, forceRefresh );

      if( this.ListView != null && this.ListView.InvokeRequired )
      {
        this.ListView.Invoke( new RefreshIconIndexDelegate( this.RefreshIconIndex ), new object[]{ index } );
      }
      else
      {
        this.RefreshIconIndex( index );
      }
    }

    #endregion PUBLIC METHODS

    #region PROTECTED DELEGATES

    /// <summary>
    /// Delegate for the RefreshIconIndex method when it is called from a dirrefent thread.
    /// </summary>
    protected delegate void RefreshIconIndexDelegate( int index );

    #endregion PROTECTED DELEGATES

    #region PROTECTED METHODS

    /// <summary>
    /// Set the item and subitems text.
    /// </summary>
    protected abstract void FillData();
    
    /// <summary>
    /// Return a string representing the formatted compressed size.
    /// </summary>
    protected abstract string GetFormattedCompressedSize();
    
    /// <summary>
    /// Return a string representing the formatted size.
    /// </summary>
    protected abstract string GetFormattedSize();

    /// <summary>
    /// Change the icon index of the item.
    /// </summary>
    /// <param name="index">The new icon index.</param>
    protected void RefreshIconIndex( int index )
    {
      if( index > -1 )
        this.ImageIndex = index;
    }

    #endregion PROTECTED METHODS

    #region PROTECTED FIELDS

    protected FileSystemItem m_item; //= null
    protected string m_displayText = string.Empty;

    #endregion PROTECTED FIELDS
	}
}
