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
Imports Moq
Imports StockTraderRI.Modules.Position.PositionSummary
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Modules.Position.Tests.Mocks
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Modules.Position.Tests.PositionSummary
    <TestClass()> _
    Public Class ObservablePositionFixture
        <TestMethod()> _
        Public Sub GeneratesModelFromPositionAndMarketFeeds()
            Dim accountPositionService = New MockAccountPositionService()
            Dim marketFeedService = New MockMarketFeedService()
            accountPositionService.AddPosition(New AccountPosition("FUND0", 300D, 1000))
            accountPositionService.AddPosition(New AccountPosition("FUND1", 200D, 100))
            marketFeedService.SetPrice("FUND0", 30D)
            marketFeedService.SetPrice("FUND1", 20D)

            Dim position As New ObservablePosition(accountPositionService, marketFeedService, CreateEventAggregator())

            Assert.AreEqual(Of Decimal)(30D, _
                                          position.Items.First(Function(x) x.TickerSymbol = "FUND0").CurrentPrice)
            Assert.AreEqual(Of Long)(1000, position.Items.First(Function(x) x.TickerSymbol = "FUND0").Shares)
            Assert.AreEqual(Of Decimal)(20D, _
                                          position.Items.First(Function(x) x.TickerSymbol = "FUND1").CurrentPrice)
            Assert.AreEqual(Of Long)(100, position.Items.First(Function(x) x.TickerSymbol = "FUND1").Shares)
        End Sub

        <TestMethod()> _
        Public Sub ShouldUpdateDataWithMarketUpdates()
            Dim accountPositionService = New MockAccountPositionService()
            Dim marketFeedService = New MockMarketFeedService()
            Dim eventAggregator = CreateEventAggregator()
            Dim _
                marketPricesUpdatedEvent = _
                    TryCast(eventAggregator.GetEvent(Of MarketPricesUpdatedEvent)(), MockMarketPricesUpdatedEvent)
            marketFeedService.SetPrice("FUND0", 30D)
            accountPositionService.AddPosition("FUND0", 25D, 1000)
            marketFeedService.SetPrice("FUND1", 20D)
            accountPositionService.AddPosition("FUND1", 15D, 100)
            Dim position As New ObservablePosition(accountPositionService, marketFeedService, eventAggregator)
            Dim updatedPriceList = New Dictionary(Of String, Decimal)
            updatedPriceList.Add("FUND0", 50D)
            Assert.IsNotNull(marketPricesUpdatedEvent.SubscribeArgumentAction)
            Assert.AreEqual(ThreadOption.UIThread, marketPricesUpdatedEvent.SubscribeArgumentThreadOption)

            marketPricesUpdatedEvent.SubscribeArgumentAction(updatedPriceList)

            Assert.AreEqual(Of Decimal)(50D, _
                                          position.Items.First(Function(x) x.TickerSymbol = "FUND0").CurrentPrice)
        End Sub

        <TestMethod()> _
        Public Sub MarketUpdatesPresenterPositionUpdatesButCollectionDoesNot()
            Dim accountPositionService = New MockAccountPositionService()
            Dim marketFeedService = New MockMarketFeedService()
            Dim eventAggregator = CreateEventAggregator()
            Dim _
                marketPricesUpdatedEvent = _
                    TryCast(eventAggregator.GetEvent(Of MarketPricesUpdatedEvent)(), MockMarketPricesUpdatedEvent)
            marketFeedService.SetPrice("FUND1", 20D)
            accountPositionService.AddPosition("FUND1", 15D, 100)

            Dim position As New ObservablePosition(accountPositionService, marketFeedService, eventAggregator)

            Dim itemsCollectionUpdated As Boolean = False
            AddHandler position.Items.CollectionChanged, Sub()
                                                             itemsCollectionUpdated = True
                                                         End Sub

            Dim itemUpdated As Boolean = False
            AddHandler position.Items.First(Function(p) p.TickerSymbol = "FUND1").PropertyChanged, Sub()
                                                                                                       itemUpdated = True
                                                                                                   End Sub
            marketPricesUpdatedEvent.SubscribeArgumentAction(New Dictionary(Of String, Decimal) From {{"FUND1", 50D}})

            Assert.IsFalse(itemsCollectionUpdated)
            Assert.IsTrue(itemUpdated)
        End Sub

        <TestMethod()> _
        Public Sub AccountPositionModificationUpdatesPM()
            Dim accountPositionService = New MockAccountPositionService()
            Dim marketFeedService = New MockMarketFeedService()
            marketFeedService.SetPrice("FUND0", 20D)
            accountPositionService.AddPosition("FUND0", 150D, 100)
            Dim position As New ObservablePosition(accountPositionService, marketFeedService, CreateEventAggregator())

            Dim itemUpdated As Boolean = False
            AddHandler position.Items.First(Function(p) p.TickerSymbol = "FUND0").PropertyChanged, Sub()
                                                                                                       itemUpdated = True
                                                                                                   End Sub

            Dim _
                accountPosition As AccountPosition = _
                    accountPositionService.GetAccountPositions().First(Function(p) p.TickerSymbol = "FUND0")
            accountPosition.Shares += 11
            accountPosition.CostBasis = 25D

            Assert.IsTrue(itemUpdated)
            Dim sxj As Object = position.Items.First(Function(p) p.TickerSymbol = "FUND0").Shares
            Assert.AreEqual(111L, position.Items.First(Function(p) p.TickerSymbol = "FUND0").Shares)
            Assert.AreEqual(25D, position.Items.First(Function(p) p.TickerSymbol = "FUND0").CostBasis)
        End Sub

        Private Shared Function CreateEventAggregator() As IEventAggregator
            Dim eventAggregator = New Mock(Of IEventAggregator)()
            eventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                    New  _
                                                                                                       MockMarketPricesUpdatedEvent())

            Return eventAggregator.Object
        End Function

        Private Class MockMarketPricesUpdatedEvent
            Inherits MarketPricesUpdatedEvent
            Public SubscribeArgumentAction As Action(Of IDictionary(Of String, Decimal))
            Public SubscribeArgumentFilter As Predicate(Of IDictionary(Of String, Decimal))
            Public SubscribeArgumentThreadOption As ThreadOption

            Public Overloads Overrides Function Subscribe(ByVal action As Action(Of IDictionary(Of String, Decimal)), _
                                                           ByVal threadOption As ThreadOption, _
                                                           ByVal keepSubscriberReferenceAlive As Boolean, _
                                                           ByVal filter As Predicate(Of IDictionary(Of String, Decimal))) _
                As SubscriptionToken
                SubscribeArgumentAction = action
                SubscribeArgumentFilter = filter
                SubscribeArgumentThreadOption = threadOption
                Return Nothing
            End Function
        End Class
    End Class

    '    
    '     * updates view when position added/removed
    '     * 
    '     * presentationModel does NOT update view when market feed does not relate to positions (filtering)
    '     
End Namespace
