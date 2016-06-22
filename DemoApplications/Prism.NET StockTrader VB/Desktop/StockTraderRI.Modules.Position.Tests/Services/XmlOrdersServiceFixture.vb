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
Imports System.Xml.Linq
Imports System.Globalization
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Position.Orders
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Services
Imports StockTraderRI.Modules.Position.Tests.Mocks

Namespace StockTraderRI.Modules.Position.Tests.Services
    <TestClass()> _
    Public Class XmlOrdersServiceFixture
        <TestMethod()> _
        Public Sub SubmitSavesOrderToXml()
            Dim logger = New MockLogger()
            Dim ordersService = New XmlOrdersService(logger)
            Dim document = New XDocument()

            Dim _
                order = _
                    New Order _
                    With {.OrderType = OrderType.Stop, .Shares = 3, .StopLimitPrice = 14, .TickerSymbol = "TESTSTOCK", _
                    .TimeInForce = TimeInForce.ThirtyDays, .TransactionType = TransactionType.Buy}

            ordersService.Submit(order, document)

            Dim root = document.Element("Orders")
            Assert.IsNotNull(root)
            Assert.AreEqual(1, root.Elements("Order").Count())
            Dim orderElement = root.Element("Order")
            Assert.IsNotNull(orderElement)
            Assert.IsTrue(orderElement.Attributes().Count() >= 6)
            Assert.AreEqual(Of String)(order.OrderType.ToString(), orderElement.Attribute("OrderType").Value)
            Assert.AreEqual(Of String)(order.Shares.ToString(), orderElement.Attribute("Shares").Value)
            Assert.AreEqual(Of String)(order.StopLimitPrice.ToString(CultureInfo.InvariantCulture), _
                                         orderElement.Attribute("StopLimitPrice").Value)
            Assert.AreEqual(Of String)(order.TickerSymbol, orderElement.Attribute("TickerSymbol").Value)
            Assert.AreEqual(Of String)(order.TimeInForce.ToString(), orderElement.Attribute("TimeInForce").Value)
            Assert.AreEqual(Of String)(order.TransactionType.ToString(), _
                                         orderElement.Attribute("TransactionType").Value)

            Dim dateElement = orderElement.Attribute("Date")
            Assert.IsNotNull(dateElement)
            Dim parsedDate = Date.Parse(dateElement.Value, CultureInfo.InvariantCulture)
            Assert.IsTrue(parsedDate < Date.Now.AddSeconds(1))
            Assert.IsTrue(parsedDate > Date.Now.AddSeconds(-10))
        End Sub

        <TestMethod()> _
        Public Sub SubmitLogsAnEntry()
            Dim logger = New MockLogger()
            Dim ordersService = New XmlOrdersService(logger)
            Dim document = New XDocument()

            Dim _
                order = _
                    New Order _
                    With {.OrderType = OrderType.Stop, .Shares = 3, .StopLimitPrice = 14, .TickerSymbol = "TESTSTOCK", _
                    .TimeInForce = TimeInForce.ThirtyDays, .TransactionType = TransactionType.Buy}

            ordersService.Submit(order, document)

            StringAssert.Contains(logger.LastMessage, "An order has been submitted.")
            StringAssert.Contains(logger.LastMessage, "TESTSTOCK")
            StringAssert.Contains(logger.LastMessage, "3")
            StringAssert.Contains(logger.LastMessage, TransactionType.Buy.ToString())
        End Sub
    End Class
End Namespace
