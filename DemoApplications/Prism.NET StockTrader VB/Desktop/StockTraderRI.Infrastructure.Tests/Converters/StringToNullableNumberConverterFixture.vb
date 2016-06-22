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
Imports System.Globalization
Imports StockTraderRI.Infrastructure.Converters
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests.Converters
    ''' <summary>
    ''' Summary description for StringToNullableIntConverterFixture
    ''' </summary>
    <TestClass()> _
    Public Class StringToNullableNumberConverterFixture
        <TestMethod()> _
        Public Sub ShouldConvertValidIntFromString()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = "123"

            Dim result As Object = converter.ConvertBack(source, GetType(Integer?), Nothing, Nothing)

            Assert.IsInstanceOfType(result, GetType(Integer))
            Assert.AreEqual(Of Integer)(123, CInt(Fix(result)))
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnOriginalValueForNonNullableIntFromString()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = "123"

            Dim result As Object = converter.ConvertBack(source, GetType(Integer), Nothing, Nothing)

            Assert.AreSame(source, result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnNullForInvalidInteger()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = "xxx"

            Dim result As Object = converter.ConvertBack(source, GetType(Integer?), Nothing, Nothing)

            Assert.IsNull(result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldConvertValidDecimalFromString()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = 10.05.ToString(CultureInfo.CurrentCulture)

            Dim result As Object = converter.ConvertBack(source, GetType(Decimal?), Nothing, Nothing)

            Assert.IsInstanceOfType(result, GetType(Decimal))
            Assert.AreEqual(Of Decimal)(10.05D, CDec(result))
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnOriginalValueForNonNullableDecimalTarget()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = 10.05.ToString(CultureInfo.CurrentCulture)

            Dim result As Object = converter.ConvertBack(source, GetType(Decimal), Nothing, Nothing)

            Assert.AreSame(source, result)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnNullForInvalidDecimal()
            Dim converter As New StringToNullableNumberConverter()

            Dim source As String = "xxx"

            Dim result As Object = converter.ConvertBack(source, GetType(Decimal?), Nothing, Nothing)

            Assert.IsNull(result)
        End Sub
    End Class
End Namespace
