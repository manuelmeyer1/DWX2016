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
Imports System.Reflection
Imports StockTraderRI.Infrastructure.Converters
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests.Converters
    <TestClass()> _
    Public Class ErrorConverterFixture
        <TestMethod()> _
        Public Sub ShouldReturnEmptyStringIfValueIsNull()
            Dim converter As New ErrorConverter()
            Dim errors As Object = Nothing

            Dim result As Object = converter.Convert(errors, Nothing, Nothing, Nothing)

            Assert.AreEqual(String.Empty, result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnEmptyStringIfCollectionIsEmpty()
            Dim converter As New ErrorConverter()

            Dim errors As New List(Of ValidationError)()

            Dim result As Object = converter.Convert(errors.AsReadOnly(), Nothing, Nothing, Nothing)

            Assert.AreEqual(String.Empty, result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnTheExceptionMessageOfTheFirstItemInTheCollection()
            Dim converter As New ErrorConverter()

            Dim errors As New List(Of ValidationError)()
            Dim [error] As New ValidationError(New ExceptionValidationRule(), New Object())
            [error].Exception = New Exception("TestError")
            errors.Add([error])

            Dim result As Object = converter.Convert(errors.AsReadOnly(), Nothing, Nothing, Nothing)

            Assert.AreEqual("TestError", result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnTheInnerExceptionMessageOfATargetInvocationException()
            Dim converter As New ErrorConverter()

            Dim errors As New List(Of ValidationError)()
            Dim [error] As New ValidationError(New ExceptionValidationRule(), New Object())
            [error].Exception = New TargetInvocationException(Nothing, New Exception("TestError"))
            errors.Add([error])

            Dim result As Object = converter.Convert(errors.AsReadOnly(), Nothing, Nothing, Nothing)

            Assert.AreEqual("TestError", result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnTheErrorContentOfTheFirstItemInTheCollection()
            Dim converter As New ErrorConverter()

            Dim errors As New List(Of ValidationError)()
            Dim [error] As New ValidationError(New ExceptionValidationRule(), New Object())
            [error].ErrorContent = "TestError"
            errors.Add([error])

            Dim result As Object = converter.Convert(errors.AsReadOnly(), Nothing, Nothing, Nothing)

            Assert.AreEqual("TestError", result)
        End Sub
    End Class
End Namespace
