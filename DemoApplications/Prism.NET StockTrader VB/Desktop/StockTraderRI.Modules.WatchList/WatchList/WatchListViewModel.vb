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
Imports System.Windows.Input
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Regions
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports StockTraderRI.Modules.Watch.Properties
Imports StockTraderRI.Modules.Watch.Services
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Watch.WatchList
    <Export(GetType(WatchListViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class WatchListViewModel
        Inherits NotificationObject
        Private ReadOnly marketFeedService As IMarketFeedService
        Private ReadOnly eventAggregator As IEventAggregator
        Private ReadOnly regionManager As IRegionManager
        Private ReadOnly watchList As ObservableCollection(Of String)
        Private _removeWatchCommand As ICommand
        Private _watchListItems As ObservableCollection(Of WatchItem)
        Private _currentWatchItem As WatchItem

        <ImportingConstructor()> _
        Public Sub New(ByVal watchListService As IWatchListService, ByVal marketFeedService As IMarketFeedService, _
                        ByVal regionManager As IRegionManager, ByVal eventAggregator As IEventAggregator)
            Me.HeaderInfo = WatchListTitle
            Me.WatchListItems = New ObservableCollection(Of WatchItem)()

            Me.marketFeedService = marketFeedService
            Me.regionManager = regionManager

            Me.watchList = watchListService.RetrieveWatchList()
            AddHandler Me.watchList.CollectionChanged, Sub()
                                                           Me.PopulateWatchItemsList(Me.watchList)
                                                       End Sub
            Me.PopulateWatchItemsList(Me.watchList)

            Me.eventAggregator = eventAggregator
            Me.eventAggregator.GetEvent(Of MarketPricesUpdatedEvent)().Subscribe(AddressOf Me.MarketPricesUpdated, _
                                                                                   ThreadOption.UIThread)

            Me._removeWatchCommand = New DelegateCommand(Of String)(AddressOf Me.RemoveWatch)

            AddHandler Me._watchListItems.CollectionChanged, AddressOf WatchListItems_CollectionChanged
        End Sub

        Public Property WatchListItems() As ObservableCollection(Of WatchItem)
            Get
                Return Me._watchListItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of WatchItem))
                If Me._watchListItems IsNot value Then
                    Me._watchListItems = value
                    Me.RaisePropertyChanged(Function() Me.WatchListItems)
                End If
            End Set
        End Property

        Public Property CurrentWatchItem() As WatchItem
            Get
                Return Me._currentWatchItem
            End Get

            Set(ByVal value As WatchItem)
                If value IsNot Nothing AndAlso Me._currentWatchItem IsNot value Then
                    Me._currentWatchItem = value
                    Me.RaisePropertyChanged(Function() CurrentWatchItem)
                    Me.eventAggregator.GetEvent(Of TickerSymbolSelectedEvent)().Publish( _
                                                                                          Me._currentWatchItem. _
                                                                                             TickerSymbol)
                End If
            End Set
        End Property

        Private _HeaderInfo As String

        Public Property HeaderInfo() As String
            Get
                Return _HeaderInfo
            End Get
            Set(ByVal value As String)
                _HeaderInfo = value
            End Set
        End Property

        Public ReadOnly Property RemoveWatchCommand() As ICommand
            Get
                Return Me._removeWatchCommand
            End Get
        End Property

#If SILVERLIGHT Then

        Public Sub MarketPricesUpdated(ByVal updatedPrices As IDictionary(Of String, Decimal))
            For Each watchItem As WatchItem In Me.WatchListItems
                If updatedPrices.ContainsKey(watchItem.TickerSymbol) Then
                    watchItem.CurrentPrice = updatedPrices(watchItem.TickerSymbol)
                End If
            Next watchItem
        End Sub

#Else
        Private Sub MarketPricesUpdated(ByVal updatedPrices As IDictionary(Of String, Decimal))
            For Each watchItem As WatchItem In Me.WatchListItems
                If updatedPrices.ContainsKey(watchItem.TickerSymbol) Then
                    watchItem.CurrentPrice = updatedPrices(watchItem.TickerSymbol)
                End If
            Next watchItem
        End Sub
#End If

        Private Sub RemoveWatch(ByVal tickerSymbol As String)
            Me.watchList.Remove(tickerSymbol)
        End Sub

        Private Sub PopulateWatchItemsList(ByVal watchItemsList As IEnumerable(Of String))
            Me.WatchListItems.Clear()
            For Each tickerSymbol As String In watchItemsList
                Dim currentPrice? As Decimal
                Try
                    currentPrice = Me.marketFeedService.GetPrice(tickerSymbol)
                Catch e1 As ArgumentException
                    currentPrice = Nothing
                End Try

                Me.WatchListItems.Add(New WatchItem(tickerSymbol, currentPrice))
            Next tickerSymbol
        End Sub

        Private Sub WatchListItems_CollectionChanged(ByVal sender As Object, _
                                                      ByVal e As NotifyCollectionChangedEventArgs)
            If e.Action = NotifyCollectionChangedAction.Add Then
                regionManager.Regions(RegionNames.MainRegion).RequestNavigate("/WatchListView", Sub(nr)

                                                                                                End Sub)
            End If
        End Sub
    End Class
End Namespace
