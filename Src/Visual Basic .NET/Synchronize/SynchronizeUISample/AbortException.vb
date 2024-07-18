'
'* Xceed FileSystem for .NET - Synchronize Sample Application
'* Copyright (c) 2006 - Xceed Software Inc.
'*
'* [AbortException.vb]
'*
'* This application demonstrate how to use the Xceed Synchronize
'* functionnality.
'*
'* This file is part of Xceed FileSystem for .NET. The source code in this file
'* is only intended as a supplement to the documentation, and is provided
'* "as is", without warranty of any kind, either expressed or implied.
'


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace SynchronizeUISample
    ''' <summary>
    ''' Class used to trigger an abort operation
    ''' </summary>
    Public Class AbortException
        Inherits Exception
#Region "PUBLIC CONSTRUCTORS"

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        ''' <param name="message"></param>
        Public Sub New(ByVal message As String)
            MyBase.New(message)
        End Sub

#End Region
    End Class
End Namespace
