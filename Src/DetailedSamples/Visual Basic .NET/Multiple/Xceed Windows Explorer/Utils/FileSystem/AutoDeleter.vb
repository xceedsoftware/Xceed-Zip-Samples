'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [AutoDeleter.vb]
 '*
 '* Class that handles tempoyrary folder so that it is deleted when the application
 '* closes.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports Xceed.FileSystem

Namespace Xceed.FileSystem.Samples.Utils.FileSystem
  Public Class AutoDeleter
    Friend Sub New(ByVal folder As DiskFolder)
      m_folder = folder
    End Sub

    Protected Overrides Sub Finalize()
      If Not m_folder Is Nothing Then
        Try
          m_folder.Delete()
        Catch
        End Try
      End If
    End Sub

    Private m_folder As DiskFolder = Nothing
  End Class
End Namespace
