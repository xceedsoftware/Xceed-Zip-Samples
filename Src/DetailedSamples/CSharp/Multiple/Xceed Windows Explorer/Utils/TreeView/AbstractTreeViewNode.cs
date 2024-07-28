/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AbstractTreeViewNode.cs]
 * 
 * Custom TreeNode exposing method to controls the action the user
 * can make on an item. (copy, paste, delete...)
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;
using Xceed.FileSystem;
using Xceed.FileSystem.Samples.Utils.Icons;
using Xceed.Zip;

namespace Xceed.FileSystem.Samples.Utils.TreeView
{
  public abstract class AbstractTreeViewNode : TreeNode
  {
    #region CONSTRUCTORS

    ~AbstractTreeViewNode()
    {
      if( m_iconUpdater != null )
        m_iconUpdater.StopUpdate();
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    /// <summary>
    /// Gets the AbstractFolder this TreeNode represent.
    /// </summary>
    public abstract AbstractFolder Folder { get; }

    /// <summary>
    /// Gets the FileSystemItem this TreeNode represent.
    /// </summary>
    public virtual FileSystemItem Item
    {
      get { return m_item; }
    }

    /// <summary>
    /// Determine if the "New" function should be enabled.
    /// </summary>
    public virtual bool NewToolEnabled
    {
      get { return true; }
    }

    /// <summary>
    /// Determine if the "New Ftp connection" function should be enabled.
    /// </summary>
    public virtual bool NewFtpConnectionToolEnabled
    {
      get { return false; }
    }

    /// <summary>
    /// Determine if the "Paste" function should be enabled.
    /// </summary>
    public virtual bool PasteToolEnabled
    {
      get { return true; }
    }
    
    /// <summary>
    /// Determine if the "Up" function should be enabled.
    /// </summary>
    public virtual bool UpToolEnabled
    {
      get
      {
        return ( this.Parent != null );
      }
    }

    /// <summary>
    /// Gets the root node of this node.
    /// </summary>
    public AbstractTreeViewNode RootNode
    {
      get
      {
        if( this.Parent != null )
        {
          AbstractTreeViewNode parentNode = ( AbstractTreeViewNode )this.Parent;
          return parentNode.RootNode;
        }

        return this;
      }
    }

    #endregion PUBLIC PROPERTIES
  
    #region PUBLIC METHODS

    public virtual void InitializeFolder()
    {
      // Let implementation do the job.
    }

    /// <summary>
    /// Refresh the current item with this new FileSystemItem. 
    /// </summary>
    /// <param name="item">The new FileSystemItem that this item represent.</param>
    public virtual void Refresh( FileSystemItem item )
    {
      if( item == null )
        throw new ArgumentNullException( "item" );

      m_item = item;

      this.RefreshText();
      this.RefreshIcon( true );
    }

    /// <summary>
    /// Refresh the icon.
    /// </summary>
    /// <param name="forceRefresh">Determine if we force a cache refresh of the icon.</param>
    public void RefreshIcon( bool forceRefresh )
    {
      // Get the indexes from the cache.
      int index = IconCache.GetIconIndex( m_item, false, forceRefresh );
      int selectedIndex = IconCache.GetIconIndex( m_item, true, forceRefresh );

      // Update the icon indexes of the node.
      if( this.TreeView != null && this.TreeView.InvokeRequired )
      {
        this.TreeView.Invoke( new RefreshIconIndexDelegate( this.RefreshIconIndex ), new object[]{ index, selectedIndex } );
      }
      else
      {
        this.RefreshIconIndex( index, selectedIndex );
      }
    }

    /// <summary>
    /// Refresh the icons of the node's children.
    /// </summary>
    public void RefreshIcons()
    {
      // To avoid multiple refresh process running at the same time, we stop
      // any current refresh process and start a new one.
      m_iconUpdater.StopUpdate();
      m_iconUpdater.StartUpdate();
    }

    #endregion PUBLIC METHODS

    #region PROTECTED DELEGATES

    /// <summary>
    /// Delegate for the RefreshIconIndex method when it is called from a dirrefent thread.
    /// </summary>
    protected delegate void RefreshIconIndexDelegate( int index, int selectedIndex );

    #endregion PROTECTED DELEGATES

    #region PROTECTED METHODS

    /// <summary>
    /// Change the icon index of the item.
    /// </summary>
    /// <param name="index">The new icon index representing the normal state.</param>
    /// <param name="selectedIndex">The new icon index representing the selected state.</param>
    protected void RefreshIconIndex( int index, int selectedIndex )
    {
      if( index > -1 )
      {
        this.ImageIndex = index;
        this.SelectedImageIndex = index; // In case there is no special icon for the selected state.
      }

      if( selectedIndex > -1 )
        this.SelectedImageIndex = selectedIndex;
    }

    /// <summary>
    /// Refresh the display text of the node.
    /// </summary>
    protected virtual void RefreshText()
    {
      AbstractFolder folder = m_item as AbstractFolder;

      if( m_displayText.Length > 0 )
      {
        this.Text = m_displayText;
      }
      else if( folder != null )
      {
        this.Text = ( folder.IsRoot ? folder.FullName : folder.Name );
      }
      else
      {
        this.Text = m_item.Name;
      }

      // Encryption (ZIP)
      ZippedFile zippedFile = m_item as ZippedFile;

      if(  ( zippedFile != null )
        && ( zippedFile.Encrypted ) )
      {
        this.ForeColor = System.Drawing.Color.Green;
      }
      else
      {
        this.ForeColor = System.Drawing.SystemColors.WindowText;
      }
    }

    #endregion PROTECTED METHODS

    #region PROTECTED FIELDS

    protected FileSystemItem m_item; //= null
    protected string m_displayText = string.Empty;
    protected TreeViewIconUpdater m_iconUpdater; //= null
    protected System.Windows.Forms.ListView m_contentListView; //= null

    #endregion PROTECTED FIELDS
  }
}