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
Imports System.Windows.Input
Imports System.Windows.Controls
Imports StockTraderRI.Infrastructure.Behaviors
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests.Behaviors
    <TestClass()> _
    Public Class ReturnCommandBehaviorFixture
        <TestMethod()> _
        Public Sub ShouldExecuteCommandOnEnter()
            Dim textBox = New TextBox()
            textBox.Text = "MyParameter"
            textBox.AcceptsReturn = True
            Dim command = New MockCommand()
            Assert.IsTrue(textBox.AcceptsReturn)

            Dim behavior As New TestableReturnCommandBehavior(textBox)
            Assert.IsFalse(textBox.AcceptsReturn)
            behavior.Command = command

            behavior.InvokeKeyPressed(Key.Enter)

            Assert.IsTrue(command.ExecuteCalled)
            Assert.AreEqual("MyParameter", command.ExecuteParameter)
        End Sub

        <TestMethod()> _
        Public Sub ShouldNotExecuteCommandOnKeyDifferentThanEnter()
            Dim textBox = New TextBox()
            textBox.Text = "MyParameter"
            Dim behavior As New TestableReturnCommandBehavior(textBox)
            Dim command = New MockCommand()
            behavior.Command = command

            behavior.InvokeKeyPressed(Key.Space)

            Assert.IsFalse(command.ExecuteCalled)
        End Sub

        <TestMethod()> _
        Public Sub ShouldSetEmptyTextAfterCommandExecution()
            Dim textBox = New TextBox()
            Dim command = New MockCommand()

            Dim behavior As New TestableReturnCommandBehavior(textBox)
            behavior.Command = command
            behavior.DefaultTextAfterCommandExecution = "MyDefaultText"

            behavior.InvokeKeyPressed(Key.Enter)

            Assert.AreEqual(String.Empty, textBox.Text)
        End Sub
    End Class

    Friend Class MockCommand
        Implements ICommand
        Public ExecuteCalled As Boolean
        Public ExecuteParameter As Object
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            ExecuteCalled = True
            ExecuteParameter = parameter
        End Sub
    End Class

    Friend Class TestableReturnCommandBehavior
        Inherits ReturnCommandBehavior

        Public Sub New(ByVal textBox As TextBox)
            MyBase.New(textBox)
        End Sub

        Public Sub InvokeKeyPressed(ByVal keyPressed As Key)
            MyBase.KeyPressed(keyPressed)
        End Sub
    End Class
End Namespace