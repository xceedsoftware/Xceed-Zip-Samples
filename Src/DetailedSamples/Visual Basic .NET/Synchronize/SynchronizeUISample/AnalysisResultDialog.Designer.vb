'
 '* Xceed FileSystem for .NET - Synchronize Sample Application
 '* Copyright (c) 2006 - Xceed Software Inc.
 '*
 '* [AnalysisResultDialog.Designer.vb]
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
Namespace SynchronizeUISample
  Public Partial Class AnalysisResultDialog
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing AndAlso (Not components Is Nothing) Then
        components.Dispose()
      End If
      MyBase.Dispose(disposing)
    End Sub

    #Region "Windows Form Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
      Me.InformationTextBox = New System.Windows.Forms.TextBox()
      Me.OkButton = New System.Windows.Forms.Button()
      Me.CancelButton = New System.Windows.Forms.Button()
      Me.AnalysisListView = New System.Windows.Forms.ListView()
      Me.Source = New System.Windows.Forms.ColumnHeader()
      Me.Action = New System.Windows.Forms.ColumnHeader()
      Me.Destination = New System.Windows.Forms.ColumnHeader()
      Me.AllSameActionCheckBox = New System.Windows.Forms.CheckBox()
      Me.SuspendLayout()
      ' 
      ' InformationTextBox
      ' 
      Me.InformationTextBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
      Me.InformationTextBox.Enabled = False
      Me.InformationTextBox.Location = New System.Drawing.Point(12, 12)
      Me.InformationTextBox.Multiline = True
      Me.InformationTextBox.Name = "InformationTextBox"
      Me.InformationTextBox.Size = New System.Drawing.Size(560, 63)
      Me.InformationTextBox.TabIndex = 3
      Me.InformationTextBox.Text = "Chose action(s) you want to perform in the list below :" & Constants.vbCrLf & Constants.vbCrLf & "Checked" & Constants.vbTab + Constants.vbTab & "Action will b" & "e performed" & Constants.vbCrLf & "Unchecked" & Constants.vbTab & "Action won't be performed"
      ' 
      ' OkButton
      ' 
      Me.OkButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
      Me.OkButton.Location = New System.Drawing.Point(412, 329)
      Me.OkButton.Name = "OkButton"
      Me.OkButton.Size = New System.Drawing.Size(76, 23)
      Me.OkButton.TabIndex = 0
      Me.OkButton.Text = "Ok"
      Me.OkButton.UseVisualStyleBackColor = True
'      Me.OkButton.Click += New System.EventHandler(Me.OkButton_Click);
      ' 
      ' CancelButton
      ' 
      Me.CancelButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
      Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
      Me.CancelButton.Location = New System.Drawing.Point(494, 329)
      Me.CancelButton.Name = "CancelButton"
      Me.CancelButton.Size = New System.Drawing.Size(78, 23)
      Me.CancelButton.TabIndex = 1
      Me.CancelButton.Text = "Cancel"
      Me.CancelButton.UseVisualStyleBackColor = True
      ' 
      ' AnalysisListView
      ' 
      Me.AnalysisListView.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
      Me.AnalysisListView.CheckBoxes = True
      Me.AnalysisListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() { Me.Source, Me.Action, Me.Destination})
      Me.AnalysisListView.GridLines = True
      Me.AnalysisListView.Location = New System.Drawing.Point(12, 81)
      Me.AnalysisListView.Name = "AnalysisListView"
      Me.AnalysisListView.Size = New System.Drawing.Size(560, 242)
      Me.AnalysisListView.TabIndex = 2
      Me.AnalysisListView.UseCompatibleStateImageBehavior = False
      Me.AnalysisListView.View = System.Windows.Forms.View.Details
      ' 
      ' Source
      ' 
      Me.Source.Text = "Source Item"
      Me.Source.Width = 256
      ' 
      ' Action
      ' 
      Me.Action.Text = "Action"
      Me.Action.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
      Me.Action.Width = 80
      ' 
      ' Destination
      ' 
      Me.Destination.Text = "Destination Item"
      Me.Destination.Width = 256
      ' 
      ' AllSameActionCheckBox
      ' 
      Me.AllSameActionCheckBox.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles))
      Me.AllSameActionCheckBox.AutoSize = True
      Me.AllSameActionCheckBox.Location = New System.Drawing.Point(12, 333)
      Me.AllSameActionCheckBox.Name = "AllSameActionCheckBox"
      Me.AllSameActionCheckBox.Size = New System.Drawing.Size(306, 17)
      Me.AllSameActionCheckBox.TabIndex = 4
      Me.AllSameActionCheckBox.Text = "Do this for all files or folders (Only Conflicts will be displayed)"
      Me.AllSameActionCheckBox.UseVisualStyleBackColor = True
'      Me.AllSameActionCheckBox.CheckedChanged += New System.EventHandler(Me.ApproveAllCheckBox_CheckedChanged);
      ' 
      ' AnalysisResultDialog
      ' 
      Me.AcceptButton = Me.OkButton
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(584, 364)
      Me.Controls.Add(Me.AllSameActionCheckBox)
      Me.Controls.Add(Me.AnalysisListView)
      Me.Controls.Add(Me.CancelButton)
      Me.Controls.Add(Me.OkButton)
      Me.Controls.Add(Me.InformationTextBox)
      Me.MinimumSize = New System.Drawing.Size(600, 400)
      Me.Name = "AnalysisResultDialog"
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "Analysis Result"
      Me.ResumeLayout(False)
      Me.PerformLayout()

    End Sub

    #End Region

    Private InformationTextBox As System.Windows.Forms.TextBox
    Private WithEvents OkButton As System.Windows.Forms.Button
    Private Shadows CancelButton As System.Windows.Forms.Button
    Private AnalysisListView As System.Windows.Forms.ListView
    Private Source As System.Windows.Forms.ColumnHeader
    Private Action As System.Windows.Forms.ColumnHeader
    Private Destination As System.Windows.Forms.ColumnHeader
    Private WithEvents AllSameActionCheckBox As System.Windows.Forms.CheckBox
  End Class
End Namespace