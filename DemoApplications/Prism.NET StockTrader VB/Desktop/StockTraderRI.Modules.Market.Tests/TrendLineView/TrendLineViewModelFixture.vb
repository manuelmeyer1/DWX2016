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
Imports Microsoft.Practices.Prism.Events
Imports Moq
Imports StockTraderRI.Modules.Market.Tests.Mocks
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Market.TrendLine

Namespace StockTraderRI.Modules.Market.Tests.TrendLine
    ''' <summary>
    ''' Unit tests for TrendLineViewModel
    ''' </summary>
    <TestClass()> _
    Public Class TrendLineViewModelFixture
        <TestMethod()> _
        Public Sub CanInitPresenter()
            Dim historyService = New MockMarketHistoryService()
            Dim eventAggregator = New Mock(Of IEventAggregator)()
            eventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                     New  _
                                                                                                        MockTickerSymbolSelectedEvent())
            Dim presentationModel As New TrendLineViewModel(historyService, eventAggregator.Object)

            Assert.IsNotNull(presentationModel)
        End Sub

        <TestMethod()> _
        Public Sub ShouldUpdateModelWithDataFromServiceOnTickerSymbolSelected()
            Dim historyService = New MockMarketHistoryService()
            Dim tickerSymbolSelected = New MockTickerSymbolSelectedEvent()
            Dim eventAggregator = New Mock(Of IEventAggregator)()
            eventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                     tickerSymbolSelected)

            Dim presentationModel As New TrendLineViewModel(historyService, eventAggregator.Object)

            tickerSymbolSelected.SubscribeArgumentAction("MyTickerSymbol")

            Assert.IsTrue(historyService.GetPriceHistoryCalled)
            Assert.AreEqual("MyTickerSymbol", historyService.GetPriceHistoryArgument)
            Assert.IsNotNull(presentationModel.HistoryCollection)
            Assert.AreEqual(historyService.Data.Count, presentationModel.HistoryCollection.Count)
            Assert.AreEqual(historyService.Data(0), presentationModel.HistoryCollection(0))
            Assert.AreEqual("MyTickerSymbol", presentationModel.TickerSymbol)
        End Sub
    End Class

    Friend Class MockTickerSymbolSelectedEvent
        Inherits TickerSymbolSelectedEvent
        Public SubscribeArgumentAction As Action(Of String)
        Public SubscribeArgumentFilter As Predicate(Of String)

        Public Overloads Overrides Function Subscribe(ByVal action As Action(Of String), _
                                                       ByVal threadOption As ThreadOption, _
                                                       ByVal keepSubscriberReferenceAlive As Boolean, _
                                                       ByVal filter As Predicate(Of String)) As SubscriptionToken
            SubscribeArgumentAction = action
            SubscribeArgumentFilter = filter
            Return Nothing
        End Function
    End Class
End Namespace
