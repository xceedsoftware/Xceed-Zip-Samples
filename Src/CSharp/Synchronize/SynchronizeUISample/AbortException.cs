/*
 * Xceed FileSystem for .NET - Synchronize Sample Application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [AbortException.cs]
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
  /// Class used to trigger an abort operation
  /// </summary>
  public class AbortException : Exception
  {
    #region PUBLIC CONSTRUCTORS

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="message"></param>
    public AbortException( string message )
      : base( message )
    {
    } 

    #endregion
  }
}
