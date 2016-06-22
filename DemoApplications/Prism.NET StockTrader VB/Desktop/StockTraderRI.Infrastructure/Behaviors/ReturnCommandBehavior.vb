'===================================================================================
' Microsoft patterns & practices
' Composite Application Guidance for Windows Presentation Foundation and Silverlight
'===================================================================================
' Copyright (c) Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===================================================================================
' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
'===================================================================================
Imports System.Windows.Controls
Imports System.Windows.Input
Imports Microsoft.Practices.Prism.Commands

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines a behavior that executes a <see cref="Windows.Input.ICommand"/> when the Return key is pressed inside a <see cref="Windows.Controls.TextBox"/>.
    ''' </summary>
    ''' <remarks>This behavior also supports setting a basic watermark on the <see cref="Windows.Controls.TextBox"/>.</remarks>
    Public Class ReturnCommandBehavior
        Inherits CommandBehaviorBase(Of TextBox)

        ''' <summary>
        ''' Initializes a new instance of <see cref="ReturnCommandBehavior"/>.
        ''' </summary>
        ''' <param name="textBox">The <see cref="TextBox"/> over which the <see cref="Windows.Input.ICommand"/> will work.</param>
        Public Sub New(ByVal textBox As TextBox)
            MyBase.New(textBox)
            textBox.AcceptsReturn = False
            AddHandler textBox.KeyDown, Sub(s, e) Me.KeyPressed(e.Key)
            AddHandler textBox.GotFocus, Sub(s, e) Me.GotFocus()
            AddHandler textBox.LostFocus, Sub(s, e) Me.LostFocus()
        End Sub

        ''' <summary>
        ''' Gets or Sets the text which is set as water mark on the <see cref="TextBox"/>.
        ''' </summary>
        Private _DefaultTextAfterCommandExecution As String

        Public Property DefaultTextAfterCommandExecution() As String
            Get
                Return _DefaultTextAfterCommandExecution
            End Get
            Set(ByVal value As String)
                _DefaultTextAfterCommandExecution = value
            End Set
        End Property

        ''' <summary>
        ''' Executes the <see cref="Windows.Input.ICommand"/> when <paramref name="key"/> is <see cref="Key.Enter"/>.
        ''' </summary>
        ''' <param name="key">The key pressed on the <see cref="TextBox"/>.</param>
        Protected Sub KeyPressed(ByVal key As Key)
            If key = key.Enter AndAlso TargetObject IsNot Nothing Then
                Me.CommandParameter = TargetObject.Text
                ExecuteCommand()

                Me.ResetText()
            End If
        End Sub

        Private Sub GotFocus()
            If TargetObject IsNot Nothing AndAlso TargetObject.Text = Me.DefaultTextAfterCommandExecution Then
                Me.ResetText()
            End If
        End Sub

        Private Sub ResetText()
            TargetObject.Text = String.Empty
        End Sub

        Private Sub LostFocus()
            If _
                TargetObject IsNot Nothing AndAlso String.IsNullOrEmpty(TargetObject.Text) AndAlso _
                Me.DefaultTextAfterCommandExecution IsNot Nothing Then
                TargetObject.Text = Me.DefaultTextAfterCommandExecution
            End If
        End Sub
    End Class
End Namespace
