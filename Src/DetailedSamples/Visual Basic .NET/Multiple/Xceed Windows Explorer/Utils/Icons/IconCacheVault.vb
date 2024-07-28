'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [IconCacheVault.vb]
 '*
 '* This class keep, in an hashtable, each icons and their indexes
 '* in the image lists. This is to avoid keeping duplicate icons
 '* in the image lists.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Drawing
Imports System.IO

Namespace Xceed.FileSystem.Samples.Utils.Icons
  Public Class IconCacheVault
    #Region "CONSTRUCTORS"

    Shared Sub New()
      m_iconVault = New Hashtable()
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC STATIC METHODS"

    Public Shared Sub Add(ByVal wrapper As IconWrapper, ByVal index As Integer)
      m_iconVault.Add(wrapper, index)
    End Sub

    Public Shared Sub Clear()
      m_iconVault.Clear()
    End Sub

    Public Shared Function Contains(ByVal wrapper As IconWrapper) As Boolean
      Return m_iconVault.Contains(wrapper)
    End Function

    Public Shared Function IndexOf(ByVal wrapper As IconWrapper) As Integer
      Return CInt(m_iconVault(wrapper))
    End Function

    #End Region ' PUBLIC STATIC METHODS

    #Region "PRIVATE FIELDS"

    Private Shared m_iconVault As Hashtable

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
