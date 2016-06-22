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
Imports Microsoft.Practices.Prism.Regions
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.Events
Imports System.Collections.ObjectModel
Imports Moq
Imports StockTraderRI.Modules.Watch.Services
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Watch.WatchList
Imports StockTraderRI.Modules.Watch

Namespace StockTraderRI.Modules.WatchList.Tests.WatchList
    <TestClass()> _
    Public Class WatchListViewModelFixture
        <TestMethod()> _
        Public Sub WhenConstructed_IntializesValues()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockRegionManager As New Mock(Of IRegionManager)()

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            ' Act
            Dim actual As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            ' Verify
            Assert.IsNotNull(actual)
            Assert.AreEqual("WATCH LIST", actual.HeaderInfo)
            Assert.AreEqual(3, actual.WatchListItems.Count)
            Assert.IsNull(actual.CurrentWatchItem)
            Assert.IsNotNull(actual.RemoveWatchCommand)
            mockWatchListService.VerifyAll()
        End Sub

        <TestMethod()> _
        Public Sub WhenCurrentWatchItemSet_PropertyIsUpdated()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()
            Dim mockTickerSymbolSelectedEvent As New Mock(Of TickerSymbolSelectedEvent)()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockRegionManager As New Mock(Of IRegionManager)()

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         mockTickerSymbolSelectedEvent _
                                                                                                            .Object)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "CurrentWatchItem" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.CurrentWatchItem = target.WatchListItems(1)

            ' Verify
            Assert.AreSame(target.WatchListItems(1), target.CurrentWatchItem)
            Assert.IsTrue(propertyChangedRaised)
        End Sub

        <TestMethod()> _
        Public Sub WhenCurrentWatchItemSet_TickerSymbolSelectedEventRaised()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()
            Dim mockTickerSymbolSelectedEvent As New Mock(Of TickerSymbolSelectedEvent)()
            mockTickerSymbolSelectedEvent.Setup(Sub(x) x.Publish("B")).Verifiable()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockRegionManager As New Mock(Of IRegionManager)()

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         mockTickerSymbolSelectedEvent _
                                                                                                            .Object)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            ' Act
            target.CurrentWatchItem = target.WatchListItems(1)

            ' Verify
            mockTickerSymbolSelectedEvent.VerifyAll()
        End Sub

        <TestMethod()> _
        Public Sub WhenRemoveCommandExecuted_RemovesWatchEntry()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockMainRegion As New Mock(Of IRegion)()

            Dim mockRegionManager As New Mock(Of IRegionManager)()
            mockRegionManager.Setup(Function(x) x.Regions(RegionNames.MainRegion)).Returns(mockMainRegion.Object)

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            ' Act
            target.RemoveWatchCommand.Execute("A")

            ' Verify
            Assert.AreEqual(2, target.WatchListItems.Count)
        End Sub

        <TestMethod()> _
        Public Sub WhenWatchListItemAdded_NavigatesToWatchListView()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockMainRegion As New Mock(Of IRegion)()
            mockMainRegion.Setup( _
                                  Sub(x) _
                                     x.RequestNavigate(New Uri("/WatchListView", UriKind.RelativeOrAbsolute), _
                                                        It.IsAny(Of Action(Of NavigationResult))())).Verifiable()

            Dim mockRegionManager As New Mock(Of IRegionManager)()
            mockRegionManager.Setup(Function(x) x.Regions(RegionNames.MainRegion)).Returns(mockMainRegion.Object)

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            ' Act
            target.WatchListItems.Add(New WatchItem("D", 20))

            ' Verify
            mockMainRegion.Verify( _
                                   Sub(x) _
                                      x.RequestNavigate(New Uri("/WatchListView", UriKind.RelativeOrAbsolute), _
                                                         It.IsAny(Of Action(Of NavigationResult))()), Times.Once())
        End Sub

        <TestMethod()> _
        Public Sub WhenMarketPriceNotAvailable_PriceSetToNull()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Throws(New ArgumentException("tickerSymbol"))

            Dim mockRegionManager As New Mock(Of IRegionManager)()

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            ' Act
            Dim actual As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            ' Verify
            Assert.IsNotNull(actual)
            Assert.AreEqual(1, actual.WatchListItems.Count)
            Assert.AreEqual("A", actual.WatchListItems(0).TickerSymbol)
            Assert.AreEqual(Nothing, actual.WatchListItems(0).CurrentPrice)
        End Sub

        <TestMethod()> _
        Public Sub WhenMarketPriceChagnes_PriceUpdated()
            ' Prepare
            Dim marketPricesUpdatedEvent As New MockMarketPricesUpdatedEvent()

            Dim watchList As New ObservableCollection(Of String)()
            watchList.Add("A")
            watchList.Add("B")
            watchList.Add("C")

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.Setup(Function(x) x.RetrieveWatchList()).Returns(watchList).Verifiable()

            Dim mockMarketFeedService As New Mock(Of IMarketFeedService)()
            mockMarketFeedService.Setup(Function(x) x.GetPrice("A")).Returns(1)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("B")).Returns(2)
            mockMarketFeedService.Setup(Function(x) x.GetPrice("C")).Returns(3)

            Dim mockRegionManager As New Mock(Of IRegionManager)()

            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of MarketPricesUpdatedEvent)()).Returns( _
                                                                                                        marketPricesUpdatedEvent)

            Dim watchListService As IWatchListService = mockWatchListService.Object
            Dim marketFeedService As IMarketFeedService = mockMarketFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New WatchListViewModel(watchListService, marketFeedService, regionManager, eventAggregator)

            Dim newPrices As New Dictionary(Of String, Decimal)()
            newPrices.Add("A", 10)
            newPrices.Add("B", 20)
            newPrices.Add("C", 30)

            ' Act
            marketPricesUpdatedEvent.Publish(newPrices)

            ' Verify
            Assert.AreEqual(3, target.WatchListItems.Count)
            Assert.AreEqual(10D, target.WatchListItems(0).CurrentPrice)
            Assert.AreEqual(20D, target.WatchListItems(1).CurrentPrice)
            Assert.AreEqual(30D, target.WatchListItems(2).CurrentPrice)
        End Sub

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

            Public Overrides Sub Publish(ByVal payload As IDictionary(Of String, Decimal))
                Me.SubscribeArgumentAction(payload)
            End Sub
        End Class

        Private Class MockTickerSymbolSelectedEvent
            Inherits TickerSymbolSelectedEvent
            Public PublishCalled As Boolean
            Public PublishArg As String

            Public Overrides Sub Publish(ByVal payload As String)
                PublishCalled = True
                PublishArg = payload
            End Sub
        End Class
    End Class
End Namespace
