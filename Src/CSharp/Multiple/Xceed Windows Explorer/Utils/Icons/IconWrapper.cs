/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [IconWrapper.cs]
 * 
 * This class wraps an icon to expose a custom hashcode for it.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Drawing;
using System.IO;

namespace Xceed.FileSystem.Samples.Utils.Icons
{
	public class IconWrapper
	{
    #region CONSTRUCTORS

		public IconWrapper( Icon icon )
		{
      if( icon == null )
        throw new ArgumentNullException( "icon" );

      try
      {
        // Create a byte array from the icon data.
        using( MemoryStream stream = new MemoryStream() )
        {
          icon.Save( stream );
          m_iconData = stream.ToArray();
        }
      }
      catch
      {
        m_iconData = new byte[ 0 ];
      }
    }

    #endregion CONSTRUCTORS

    #region PUBLIC PROPERTIES

    public byte[] IconData
    {
      get{ return m_iconData; }
    }

    #endregion PUBLIC PROPERTIES

    #region OVERRIDES

    public override bool Equals(object obj)
    {
      if( obj as IconWrapper == null )
        return false;

      byte[] targetArray = ( ( IconWrapper )obj ).IconData;

      if( m_iconData.Length != targetArray.Length )
        return false;

      for( int i = 0; i < m_iconData.Length; i++ )
      {
        if( m_iconData[ i ] != targetArray[ i ] )
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      int hash = 0;
      int index = 0;

      while( ( index + 4 ) < m_iconData.Length )
      {
        hash ^= BitConverter.ToInt32( m_iconData, index );
        index += 4;
      }
      
      return hash;
    }

    #endregion OVERRIDES

    #region PRIVATE FIELDS

    private byte[] m_iconData; //= null

    #endregion PRIVATE FIELDS
  }
}
