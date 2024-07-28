/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [MasterItemTypeEnum.cs]
 * 
 * This application demonstrate how to use the Xceed Synchronize
 * functionnality.
 * 
 * This file is part of Xceed FileSystem for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizeUISample
{
  /// <summary>
  /// Enum used to keep tracking of the master item type (File or Folder). 
  /// None if undefined.
  /// </summary>
  public enum MasterItemType
  {
    None,
    File,
    Folder
  }
}
