/*
 * Xceed Zip for .NET - MiniExplorer Sample Application
 * Copyright (c) 2000-2003 - Xceed Software Inc.
 * 
 * [FolderTreeNode.cs]
 * 
 * This application demonstrates how to use the Xceed FileSystem object model
 * in a generic way.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;
using Xceed.FileSystem;

namespace Xceed.FileSystem.Samples.MiniExplorer
{
	/// <summary>
	/// Summary description for FolderTreeNode.
	/// </summary>
	internal class FolderTreeNode : TreeNode 
	{
		public FolderTreeNode( AbstractFolder folder, string displayName )
      : base( displayName, 1, 1 )
		{
      m_folder = folder;

      // Display a [+] by default
      this.Nodes.Add( string.Empty );
		}

    public FolderTreeNode( string displayName )
      : base( displayName, 1, 1 )
    {
    }

    public AbstractFolder Folder
    {
      get { return m_folder; }
    }

    private AbstractFolder m_folder = null;
	}
}
