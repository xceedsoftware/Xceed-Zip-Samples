/*
 * Xceed Zip for .NET - Xceed Windows Explorer sample application
 * Copyright (c) 2006 - Xceed Software Inc.
 * 
 * [PathLabel.cs]
 * 
 * Custom Label use to display a path and trim it if needed.
 * 
 * This file is part of Xceed Zip for .NET. The source code in this file 
 * is only intended as a supplement to the documentation, and is provided 
 * "as is", without warranty of any kind, either expressed or implied.
 */

using System;
using System.Windows.Forms;

namespace Xceed.FileSystem.Samples
{
	public class PathLabel : Label
	{
    protected override void OnPaint( PaintEventArgs e )
    {
      System.Drawing.StringFormat format = new System.Drawing.StringFormat();
      format.Trimming = System.Drawing.StringTrimming.EllipsisPath;
      format.Alignment = System.Drawing.StringAlignment.Near;
      format.LineAlignment = System.Drawing.StringAlignment.Near;
      format.FormatFlags = System.Drawing.StringFormatFlags.LineLimit;

      e.Graphics.DrawString( this.Text, this.Font, System.Drawing.SystemBrushes.WindowText, e.ClipRectangle, format );
    }
	}
}
