/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [IconCacheVault.cs]
 * 
 * This class keep, in an hashtable, each icons and their indexes
 * in the image lists. This is to avoid keeping duplicate icons
 * in the image lists.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections;
using System.Drawing;
using System.IO;

namespace Xceed.FileSystem.Samples.Utils.Icons
{
	public class IconCacheVault
	{
    #region CONSTRUCTORS

		static IconCacheVault()
		{
      m_iconVault = new Hashtable();
		}

    #endregion CONSTRUCTORS

    #region PUBLIC STATIC METHODS

    public static void Add( IconWrapper wrapper, int index )
    {
      m_iconVault.Add( wrapper, index );
    }

    public static void Clear()
    {
      m_iconVault.Clear();
    }

    public static bool Contains( IconWrapper wrapper )
    {
      return m_iconVault.Contains( wrapper );
    }

    public static int IndexOf( IconWrapper wrapper )
    {
      return ( int )m_iconVault[ wrapper ];
    }

    #endregion PUBLIC STATIC METHODS

    #region PRIVATE FIELDS

    private static Hashtable m_iconVault;

    #endregion PRIVATE FIELDS
	}
}
