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
    Public Class EnumToBooleanConverterFixture
        <TestMethod()> _
        Public Sub EnumToBooleanConverterConverts()
            Dim converter As New EnumToBooleanConverter()
            Dim value As Object = converter.Convert(TransactionType.Buy, GetType(TransactionType), "Buy", Nothing)

            Assert.IsTrue(CBool(value))
        End Sub

        <TestMethod()> _
        Public Sub EnumToBooleanConverterConvertsBack()
            Dim converter As New EnumToBooleanConverter()
            Dim value As Object = converter.ConvertBack(True, GetType(TransactionType), "Sell", Nothing)

            Assert.AreEqual(TransactionType.Sell, value)
        End Sub
    End Class
End Namespace