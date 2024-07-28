'
'* Xceed Zip for .NET - Xceed Windows Explorer sample application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [PathLabel.vb]
'*
'* Custom Label use to display a path and trim it if needed.
'*
'* This file is part of Xceed Zip for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'

Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Namespace Xceed.FileSystem.Samples
  Public Class PathLabel : Inherits Label
    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
      Dim format As System.Drawing.StringFormat = New System.Drawing.StringFormat
      format.Trimming = System.Drawing.StringTrimming.EllipsisPath
      format.Alignment = System.Drawing.StringAlignment.Near
      format.LineAlignment = System.Drawing.StringAlignment.Near
      format.FormatFlags = System.Drawing.StringFormatFlags.LineLimit

      Dim rect As System.Drawing.RectangleF = New System.Drawing.RectangleF(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height)
      e.Graphics.DrawString(Me.Text, Me.Font, System.Drawing.SystemBrushes.WindowText, rect, format)
    End Sub
  End Class
End Namespace
