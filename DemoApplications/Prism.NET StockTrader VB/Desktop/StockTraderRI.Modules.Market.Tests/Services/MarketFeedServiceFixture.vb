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
Imports StockTraderRI.Modules.Market.Tests.Properties
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Market.Services
Imports System.Threading

Namespace StockTraderRI.Modules.Market.Tests.Services
    <TestClass()> _
    Public Class MarketFeedServiceFixture
        <TestMethod()> _
        Public Sub CanGetPriceAndVolumeFromMarketFeed()
            Using marketFeed = New TestableMarketFeedService(New MockPriceUpdatedEventAggregator())
                marketFeed.TestUpdatePrice("STOCK0", 40D, 1234)

                Assert.AreEqual(Of Decimal)(40D, marketFeed.GetPrice("STOCK0"))
                Assert.AreEqual(Of Long)(1234, marketFeed.GetVolume("STOCK0"))
            End Using
        End Sub

        <TestMethod()> _
        Public Sub ShouldPublishUpdatedOnSinglePriceChange()
            Dim eventAggregator = New MockPriceUpdatedEventAggregator()

            Using marketFeed As New TestableMarketFeedService(eventAggregator)
                marketFeed.TestUpdatePrice("STOCK0", 30D, 1000)
            End Using

            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled)
        End Sub

        <TestMethod()> _
        Public Sub GetPriceOfNonExistingSymbolThrows()
            Using marketFeed = New MarketFeedService(New MockPriceUpdatedEventAggregator())
                Try
                    marketFeed.GetPrice("NONEXISTANT")
                    Assert.Fail("No exception thrown")
                Catch ex As Exception
                    Assert.IsInstanceOfType(ex, GetType(ArgumentException))
                    Assert.IsTrue(ex.Message.Contains("Symbol does not exist in market feed."))
                End Try
            End Using
        End Sub

        <TestMethod()> _
        Public Sub SymbolExistsWorksAsExpected()
            Using marketFeed = New MarketFeedService(New MockPriceUpdatedEventAggregator())
                Assert.IsTrue(marketFeed.SymbolExists("STOCK0"))
                Assert.IsFalse(marketFeed.SymbolExists("NONEXISTANT"))
            End Using
        End Sub

        <TestMethod()> _
        Public Sub ShouldUpdatePricesWithin5Points()
            Using marketFeed = New TestableMarketFeedService(New MockPriceUpdatedEventAggregator())
                Dim originalPrice As Decimal = marketFeed.GetPrice("STOCK0")
                marketFeed.InvokeUpdatePrices()
                Assert.IsTrue(Math.Abs(marketFeed.GetPrice("STOCK0") - originalPrice) <= 5)
            End Using
        End Sub

        <TestMethod()> _
        Public Sub ShouldPublishUpdatedAfterUpdatingPrices()
            Dim eventAggregator = New MockPriceUpdatedEventAggregator()

            Using marketFeed = New TestableMarketFeedService(eventAggregator)
                marketFeed.InvokeUpdatePrices()
            End Using
            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled)
        End Sub

        <TestMethod()> _
        Public Sub MarketServiceReadsIntervalFromXml()
            Dim xmlMarketData = XDocument.Parse(TestXmlMarketData)
            Using marketFeed = New TestableMarketFeedService(xmlMarketData, New MockPriceUpdatedEventAggregator())
                Assert.AreEqual(Of Integer)(5000, marketFeed.RefreshInterval)
            End Using
        End Sub

        <TestMethod()> _
        Public Sub UpdateShouldPublishWithinRefreshInterval()
            Dim eventAggregator = New MockPriceUpdatedEventAggregator()

            Using marketFeed = New TestableMarketFeedService(eventAggregator)
                marketFeed.RefreshInterval = 500
                ' ms

                Dim callCompletedEvent = New ManualResetEvent(False)

                AddHandler eventAggregator.MockMarketPriceUpdatedEvent.PublishCalledEvent, Sub()
                                                                                               callCompletedEvent.Set()
                                                                                           End Sub
#If SILVERLIGHT Then
                callCompletedEvent.WaitOne(5000)
                ' Wait up to 5 seconds
#Else
                callCompletedEvent.WaitOne(5000, True) ' Wait up to 5 seconds
#End If
            End Using
            Assert.IsTrue(eventAggregator.MockMarketPriceUpdatedEvent.PublishCalled)
        End Sub

        <TestMethod()> _
        Public Sub RefreshIntervalDefaultsTo10SecondsWhenNotSpecified()
            Dim xmlMarketData = XDocument.Parse(TestXmlMarketData)
            xmlMarketData.Element("MarketItems").Attribute("RefreshRate").Remove()

            Using marketFeed = New TestableMarketFeedService(xmlMarketData, New MockPriceUpdatedEventAggregator())
                Assert.AreEqual(Of Integer)(10000, marketFeed.RefreshInterval)
            End Using
        End Sub

        <TestMethod()> _
        Public Sub PublishedEventContainsTheUpdatedPriceList()
            Dim eventAgregator = New MockPriceUpdatedEventAggregator()
            Dim marketFeed = New TestableMarketFeedService(eventAgregator)
            Assert.IsTrue(marketFeed.SymbolExists("STOCK0"))

            marketFeed.InvokeUpdatePrices()

            Assert.IsTrue(eventAgregator.MockMarketPriceUpdatedEvent.PublishCalled)
            Dim payload = eventAgregator.MockMarketPriceUpdatedEvent.PublishArgumentPayload
            Assert.IsNotNull(payload)
            Assert.IsTrue(payload.ContainsKey("STOCK0"))
            Assert.AreEqual(marketFeed.GetPrice("STOCK0"), payload("STOCK0"))
        End Sub
    End Class

    Friend Class TestableMarketFeedService
        Inherits MarketFeedService

        Public Sub New(ByVal eventAggregator As MockPriceUpdatedEventAggregator)
            MyBase.New(eventAggregator)

        End Sub

        Public Sub New(ByVal xmlDocument As XDocument, ByVal eventAggregator As MockPriceUpdatedEventAggregator)
            MyBase.New(xmlDocument, eventAggregator)
        End Sub

        Public Sub TestUpdatePrice(ByVal tickerSymbol As String, ByVal price As Decimal, ByVal volume As Long)
            Me.UpdatePrice(tickerSymbol, price, volume)
        End Sub

        Public Sub InvokeUpdatePrices()
            MyBase.UpdatePrices()
        End Sub
    End Class

    Friend Class MockEventAggregator
        Implements IEventAggregator
        Private events As New Dictionary(Of Type, Object)()

        Public Function GetEvent(Of TEventType As {EventBase, New})() As TEventType _
            Implements IEventAggregator.GetEvent
            Return CType(events(GetType(TEventType)), TEventType)
        End Function

        Public Sub AddMapping(Of TEventType)(ByVal mockEvent As TEventType)
            events.Add(GetType(TEventType), mockEvent)
        End Sub
    End Class

    Friend Class MockPriceUpdatedEventAggregator
        Inherits MockEventAggregator
        Public MockMarketPriceUpdatedEvent As New MockMarketPricesUpdatedEvent()

        Public Sub New()
            AddMapping(Of MarketPricesUpdatedEvent)(MockMarketPriceUpdatedEvent)
        End Sub

        Public Class MockMarketPricesUpdatedEvent
            Inherits MarketPricesUpdatedEvent
            Public PublishCalled As Boolean
            Public PublishArgumentPayload As IDictionary(Of String, Decimal)
            Public Event PublishCalledEvent As EventHandler

            Private Sub OnPublishCalledEvent(ByVal sender As Object, ByVal args As EventArgs)
                RaiseEvent PublishCalledEvent(sender, args)
            End Sub

            Public Overrides Sub Publish(ByVal payload As IDictionary(Of String, Decimal))
                PublishCalled = True
                PublishArgumentPayload = payload
                OnPublishCalledEvent(Me, EventArgs.Empty)
            End Sub
        End Class
    End Class
End Namespace
