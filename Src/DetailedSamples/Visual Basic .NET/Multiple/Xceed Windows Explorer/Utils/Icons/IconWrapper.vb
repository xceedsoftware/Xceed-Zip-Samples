'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [IconWrapper.vb]
 '*
 '* This class wraps an icon to expose a custom hashcode for it.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO

Namespace Xceed.FileSystem.Samples.Utils.Icons
  Public Class IconWrapper
    #Region "CONSTRUCTORS"

    Public Sub New(ByVal icon As Icon)
      If icon Is Nothing Then
        Throw New ArgumentNullException("icon")
      End If

      Try
        ' Create a byte array from the icon data.
        Dim stream As MemoryStream = New MemoryStream
        Try
          icon.Save(stream)
          m_iconData = stream.ToArray()
        Finally
          CType(stream, IDisposable).Dispose()
        End Try
      Catch
        m_iconData = New Byte() {}
      End Try
    End Sub

    #End Region ' CONSTRUCTORS

    #Region "PUBLIC PROPERTIES"

    Public ReadOnly Property IconData() As Byte()
      Get
        Return m_iconData
      End Get
    End Property

    #End Region ' PUBLIC PROPERTIES

    #Region "OVERRIDES"

    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
      If CType(IIf(TypeOf obj Is IconWrapper, obj, Nothing), IconWrapper) Is Nothing Then
        Return False
      End If

      Dim targetArray As Byte() = (CType(obj, IconWrapper)).IconData

      If m_iconData.Length <> targetArray.Length Then
        Return False
      End If

      Dim i As Integer = 0
      Do While i < m_iconData.Length
        If m_iconData(i) <> targetArray(i) Then
          Return False
        End If
        i += 1
      Loop

      Return True
    End Function

    Public Overrides Function GetHashCode() As Integer
      Dim hash As Integer = 0
      Dim index As Integer = 0

      Do While (index + 4) < m_iconData.Length
        hash = hash Xor BitConverter.ToInt32(m_iconData, index)
        index += 4
      Loop

      Return hash
    End Function

#End Region     ' OVERRIDES

    #Region "PRIVATE FIELDS"

    Private m_iconData As Byte() '= null

    #End Region ' PRIVATE FIELDS
  End Class
End Namespace
