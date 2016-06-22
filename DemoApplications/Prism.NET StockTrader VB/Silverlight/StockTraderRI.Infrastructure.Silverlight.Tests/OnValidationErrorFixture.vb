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
Imports System.Windows.Media
Imports System.Windows.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests
    <TestClass()> _
    Public Class OnValidationErrorFixture
        <TestMethod()> _
        Public Sub ShouldChangeBackgroundOnValidationError()
            Dim element = New TextBox()
            Dim background = New SolidColorBrush()
            Dim model = New MockModel()
            OnValidationError.SetToggleBackground(element, background)
            CreateBindingThatValidatesOnExceptions(element, model)

            element.Text = "InvalidValue"

            Assert.AreEqual(background, element.Background)
        End Sub

        <TestMethod()> _
        Public Sub ShouldChangeToOriginalBackgroundErrorRemoved()
            Dim element = New TextBox()
            Dim originalBackground = New SolidColorBrush()
            element.Background = originalBackground
            Dim model = New MockModel()
            OnValidationError.SetToggleBackground(element, New SolidColorBrush())
            CreateBindingThatValidatesOnExceptions(element, model)
            element.Text = "InvalidValue"
            Assert.AreNotEqual(originalBackground, element.Background)

            element.Text = "ValidValue"

            Assert.AreEqual(originalBackground, element.Background)
        End Sub

        <TestMethod()> _
        Public Sub ShouldSetToolTipOnError()
            Dim element = New TextBox()
            Dim model = New MockModel()
            model.ExceptionMessage = "My custom Exception message"
            OnValidationError.SetShowToolTip(element, True)
            CreateBindingThatValidatesOnExceptions(element, model)

            Dim originalTooltip = ToolTipService.GetToolTip(element)

            element.Text = "InvalidValue"

            Assert.IsNotNull(ToolTipService.GetToolTip(element))
            Assert.AreEqual(model.ExceptionMessage, ToolTipService.GetToolTip(element))
        End Sub

        <TestMethod()> _
        Public Sub ShouldRemoveToolTipOnErrorRemoved()
            Dim element = New TextBox()
            Dim model = New MockModel()
            OnValidationError.SetShowToolTip(element, True)
            CreateBindingThatValidatesOnExceptions(element, model)

            Dim originalTooltip = ToolTipService.GetToolTip(element)

            element.Text = "InvalidValue"
            Assert.IsNotNull(ToolTipService.GetToolTip(element))

            element.Text = "ValidValue"

            Assert.AreEqual(originalTooltip, ToolTipService.GetToolTip(element))
        End Sub

        <TestMethod()> _
        Public Sub ShouldSetToolTipToOriginalOnErrorRemoved()
            Dim element = New TextBox()
            Dim originalToolTip As String = "Please enter a valid value"
            ToolTipService.SetToolTip(element, originalToolTip)
            Dim model = New MockModel()
            OnValidationError.SetShowToolTip(element, True)
            CreateBindingThatValidatesOnExceptions(element, model)
            element.Text = "InvalidValue"
            Assert.IsNotNull(ToolTipService.GetToolTip(element))

            element.Text = "ValidValue"

            Assert.AreEqual(originalToolTip, ToolTipService.GetToolTip(element))
        End Sub

        Private Shared Sub CreateBindingThatValidatesOnExceptions(ByVal element As TextBox, ByVal source As Object)
            Dim binding = New Binding("Property")
            binding.Source = source
            binding.Mode = BindingMode.TwoWay
            binding.NotifyOnValidationError = True
            binding.ValidatesOnExceptions = True
            element.SetBinding(TextBox.TextProperty, binding)
        End Sub

        Public Class MockModel
            Public ExceptionMessage As String = "Error Text"
            Private _property As String

            Public Property [Property]() As String
                Get
                    Return Me._property
                End Get
                Set(ByVal value As String)
                    If value = "InvalidValue" Then
                        Throw New Exception(Me.ExceptionMessage)
                    End If

                    Me._property = value
                End Set
            End Property
        End Class
    End Class
End Namespace
