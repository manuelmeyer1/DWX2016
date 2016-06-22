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
    Public Class TransactionTypeToStringConverterFixture
        <TestMethod()> _
        Public Sub ShouldConvertTransactionTypeValuesToString()
            Dim converter As New TransactionTypeToStringConverter()

            Dim convertedValue = TryCast(converter.Convert(TransactionType.Buy, Nothing, Nothing, Nothing), String)

            Assert.IsNotNull(convertedValue)
            Assert.AreEqual("BUY ", convertedValue)

            convertedValue = TryCast(converter.Convert(TransactionType.Sell, Nothing, Nothing, Nothing), String)

            Assert.IsNotNull(convertedValue)
            Assert.AreEqual("SELL ", convertedValue)
        End Sub

        <TestMethod()> _
        Public Sub ShouldReturnNullIfValueToConvertIsNullOrNotTransactionType()
            Dim converter As New TransactionTypeToStringConverter()

            Dim convertedValue = TryCast(converter.Convert(Nothing, Nothing, Nothing, Nothing), String)
            Assert.IsNull(convertedValue)

            convertedValue = TryCast(converter.Convert("NotATransactionType", Nothing, Nothing, Nothing), String)
            Assert.IsNull(convertedValue)
        End Sub
    End Class
End Namespace
