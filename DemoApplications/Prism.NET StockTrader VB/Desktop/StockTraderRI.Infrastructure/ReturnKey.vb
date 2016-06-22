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
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Controls
Imports StockTraderRI.Infrastructure.Behaviors

Namespace StockTraderRI.Infrastructure
    Public NotInheritable Class ReturnKey
        ''' <summary>
        ''' Command to execute on return key event.
        ''' </summary>
        Public Shared ReadOnly _
            CommandProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("Command", GetType(ICommand), GetType(ReturnKey), _
                                                     New PropertyMetadata(AddressOf OnSetCommandCallback))

        ''' <summary>
        ''' Default text to set to the TextBox once the Command has been executed
        ''' </summary>
        Public Shared ReadOnly _
            DefaultTextAfterCommandExecutionProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("DefaultTextAfterCommandExecution", GetType(String), _
                                                     GetType(ReturnKey), _
                                                     New PropertyMetadata( _
                                                                           AddressOf _
                                                                              OnSetDefaultTextAfterCommandExecutionCallback))

        Private Shared ReadOnly _
            ReturnCommandBehaviorProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ReturnCommandBehavior", GetType(ReturnCommandBehavior), _
                                                     GetType(ReturnKey), Nothing)

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Sets the <see cref="string"/> to set to the TextBox once the Command has been executed.
        ''' </summary>
        ''' <param name="textBox">TextBox dependency object on which the default text will be set after the Command has been executed.</param>
        ''' <param name="defaultText">Default text to set.</param>
        Public Shared Sub SetDefaultTextAfterCommandExecution(ByVal textBox As TextBox, ByVal defaultText As String)
            textBox.SetValue(DefaultTextAfterCommandExecutionProperty, defaultText)
        End Sub

        ''' <summary>
        ''' Retrieves the default text set to the <see cref="TextBox"/> after the Command has been executed.
        ''' </summary>
        ''' <param name="textBox">TextBox dependency object on which the default text will be set after the Command has been executed.</param>
        ''' <returns>Default text to set.</returns>
        Public Shared Function GetDefaultTextAfterCommandExecution(ByVal textBox As TextBox) As String
            Return TryCast(textBox.GetValue(DefaultTextAfterCommandExecutionProperty), String)
        End Function

        ''' <summary>
        ''' Sets the <see cref="ICommand"/> to execute on the return key event.
        ''' </summary>
        ''' <param name="textBox">TextBox dependency object to attach command</param>
        ''' <param name="command">Command to attach</param>
        Public Shared Sub SetCommand(ByVal textBox As TextBox, ByVal command As ICommand)
            textBox.SetValue(CommandProperty, command)
        End Sub

        ''' <summary>
        ''' Retrieves the <see cref="ICommand"/> attached to the <see cref="TextBox"/>.
        ''' </summary>
        ''' <param name="textBox">TextBox containing the Command dependency property</param>
        ''' <returns>The value of the command attached</returns>
        Public Shared Function GetCommand(ByVal textBox As TextBox) As ICommand
            Return TryCast(textBox.GetValue(CommandProperty), ICommand)
        End Function

        Private Shared Sub OnSetDefaultTextAfterCommandExecutionCallback(ByVal dependencyObject As DependencyObject, _
                                                                          ByVal e As DependencyPropertyChangedEventArgs)
            Dim textBox As TextBox = TryCast(dependencyObject, TextBox)
            If textBox IsNot Nothing Then
                Dim behavior As ReturnCommandBehavior = GetOrCreateBehavior(textBox)
                behavior.DefaultTextAfterCommandExecution = TryCast(e.NewValue, String)
            End If
        End Sub

        Private Shared Sub OnSetCommandCallback(ByVal dependencyObject As DependencyObject, _
                                                 ByVal e As DependencyPropertyChangedEventArgs)
            Dim textBox As TextBox = TryCast(dependencyObject, TextBox)
            If textBox IsNot Nothing Then
                Dim behavior As ReturnCommandBehavior = GetOrCreateBehavior(textBox)
                behavior.Command = TryCast(e.NewValue, ICommand)
            End If
        End Sub

        Private Shared Function GetOrCreateBehavior(ByVal textBox As TextBox) As ReturnCommandBehavior
            Dim _
                behavior As ReturnCommandBehavior = _
                    TryCast(textBox.GetValue(ReturnCommandBehaviorProperty), ReturnCommandBehavior)
            If behavior Is Nothing Then
                behavior = New ReturnCommandBehavior(textBox)
                textBox.SetValue(ReturnCommandBehaviorProperty, behavior)
            End If

            Return behavior
        End Function
    End Class
End Namespace