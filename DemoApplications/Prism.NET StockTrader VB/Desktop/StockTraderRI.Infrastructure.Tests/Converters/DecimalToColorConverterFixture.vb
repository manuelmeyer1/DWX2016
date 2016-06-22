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
Imports StockTraderRI.Infrastructure.Converters
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests.Converters
    <TestClass()> _
    Public Class DecimalToColorConverterFixture
        <TestMethod()> _
        Public Sub ShouldConvertFromDecimalToColorString()
            Dim converter As New DecimalToColorConverter()

            Dim convertedValue = TryCast(converter.Convert(20D, Nothing, Nothing, Nothing), String)
            Assert.IsNotNull(convertedValue)
            Assert.AreEqual("#ff00cc00", convertedValue)

            convertedValue = TryCast(converter.Convert(-20D, Nothing, Nothing, Nothing), String)
            Assert.IsNotNull(convertedValue)
            Assert.AreEqual("#ffff0000", convertedValue)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnNullIfValueToConvertIsNullOrNotDecimal()
            Dim converter As New DecimalToColorConverter()

            Dim convertedValue = TryCast(converter.Convert(Nothing, Nothing, Nothing, Nothing), String)
            Assert.IsNull(convertedValue)

            convertedValue = TryCast(converter.Convert("NotADecimal", Nothing, Nothing, Nothing), String)
            Assert.IsNull(convertedValue)
        End Sub
    End Class
End Namespace
