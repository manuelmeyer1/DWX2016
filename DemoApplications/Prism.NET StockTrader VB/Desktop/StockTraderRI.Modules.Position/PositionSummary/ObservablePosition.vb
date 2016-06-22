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
Imports System.ComponentModel.Composition
Imports System.Collections.ObjectModel
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Position.PositionSummary
    <Export(GetType(IObservablePosition)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class ObservablePosition
        Implements IObservablePosition

        Private accountPositionService As IAccountPositionService
        Private marketFeedService As IMarketFeedService

        Private _Items As ObservableCollection(Of PositionSummaryItem)

        Public Property Items() As ObservableCollection(Of PositionSummaryItem) Implements IObservablePosition.Items
            Get
                Return _Items
            End Get
            Private Set(ByVal value As ObservableCollection(Of PositionSummaryItem))
                _Items = value
            End Set
        End Property

        <ImportingConstructor()> _
        Public Sub New(ByVal accountPositionService As IAccountPositionService, _
                        ByVal marketFeedService As IMarketFeedService, ByVal eventAggregator As IEventAggregator)
            Me.Items = New ObservableCollection(Of PositionSummaryItem)()

            Me.accountPositionService = accountPositionService
            Me.marketFeedService = marketFeedService

            eventAggregator.GetEvent(Of MarketPricesUpdatedEvent)().Subscribe(AddressOf MarketPricesUpdated, _
                                                                                ThreadOption.UIThread)

            PopulateItems()

            AddHandler Me.accountPositionService.Updated, AddressOf PositionSummaryItems_Updated
        End Sub

        Public Sub MarketPricesUpdated(ByVal tickerSymbolsPrice As IDictionary(Of String, Decimal))
            For Each position As PositionSummaryItem In Me.Items
                If tickerSymbolsPrice.ContainsKey(position.TickerSymbol) Then
                    position.CurrentPrice = tickerSymbolsPrice(position.TickerSymbol)
                End If
            Next position
        End Sub

        Private Sub PositionSummaryItems_Updated(ByVal sender As Object, ByVal e As AccountPositionModelEventArgs)
            If e.AcctPosition IsNot Nothing Then
                Dim _
                    positionSummaryItem As PositionSummaryItem = _
                        Me.Items.First(Function(p) p.TickerSymbol = e.AcctPosition.TickerSymbol)

                If positionSummaryItem IsNot Nothing Then
                    positionSummaryItem.Shares = e.AcctPosition.Shares
                    positionSummaryItem.CostBasis = e.AcctPosition.CostBasis
                End If
            End If
        End Sub

        Private Sub PopulateItems()
            Dim positionSummaryItem As PositionSummaryItem
            For Each accountPosition As AccountPosition In Me.accountPositionService.GetAccountPositions()
                positionSummaryItem = _
                    New PositionSummaryItem(accountPosition.TickerSymbol, accountPosition.CostBasis, _
                                             accountPosition.Shares, _
                                             Me.marketFeedService.GetPrice(accountPosition.TickerSymbol))
                Me.Items.Add(positionSummaryItem)
            Next accountPosition
        End Sub
    End Class
End Namespace
