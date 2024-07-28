'
 '* Xceed Zip for .NET - Xceed Windows Explorer sample application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
'* [ShellIconEnum.vb]
 '*
 '* The values here represent the index of specific icons in the
 '* Shell32.dll system file.
 '*
 '* This file is part of Xceed Zip for .NET. The source code in this file
 '* is only intended as a supplement to the documentation, and is provided
 '* "as is", without warranty of any kind, either expressed or implied.
 '


Imports Microsoft.VisualBasic
Imports System

Namespace Xceed.FileSystem.Samples.Utils.Icons
  Public Enum ShellIcon
    File = 0
    Application = 2
    ClosedFolder = 3
    OpenedFolder = 4
    MyComputer = 15
  End Enum
End Namespace
