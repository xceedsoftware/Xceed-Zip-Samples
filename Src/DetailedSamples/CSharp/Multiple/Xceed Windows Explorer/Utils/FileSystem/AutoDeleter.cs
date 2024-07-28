/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AutoDeleter.cs]
 * 
 * Class that handles tempoyrary folder so that it is deleted when the application
 * closes.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using Xceed.FileSystem;

namespace Xceed.FileSystem.Samples.Utils.FileSystem
{
  public class AutoDeleter
  {
    internal AutoDeleter( DiskFolder folder )
    {
      m_folder = folder;
    }

    ~AutoDeleter()
    {
      if( m_folder != null )
      {
        try
        {
          m_folder.Delete();
        }
        catch {}
      }
    }

    private DiskFolder m_folder = null;
  }
}
