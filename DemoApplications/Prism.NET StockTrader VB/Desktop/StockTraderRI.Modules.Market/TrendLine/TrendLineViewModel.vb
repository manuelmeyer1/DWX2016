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
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Market.TrendLine
    <Export(GetType(TrendLineViewModel))> _
    Public Class TrendLineViewModel
        Inherits NotificationObject
        Private ReadOnly marketHistoryService As IMarketHistoryService

        Private _tickerSymbol As String

        Private _historyCollection As MarketHistoryCollection

        <ImportingConstructor()> _
        Public Sub New(ByVal marketHistoryService As IMarketHistoryService, ByVal eventAggregator As IEventAggregator)
            Me.marketHistoryService = marketHistoryService
            eventAggregator.GetEvent(Of TickerSymbolSelectedEvent)().Subscribe(AddressOf Me.TickerSymbolChanged)
        End Sub

        Public Sub TickerSymbolChanged(ByVal newTickerSymbol As String)
            Dim _
                newHistoryCollection As MarketHistoryCollection = _
                    Me.marketHistoryService.GetPriceHistory(newTickerSymbol)

            Me.TickerSymbol = newTickerSymbol
            Me.HistoryCollection = newHistoryCollection
        End Sub

        Public Property TickerSymbol() As String
            Get
                Return Me._tickerSymbol
            End Get
            Set(ByVal value As String)
                If Me._tickerSymbol <> value Then
                    Me._tickerSymbol = value
                    Me.RaisePropertyChanged(Function() Me.TickerSymbol)
                End If
            End Set
        End Property

        Public Property HistoryCollection() As MarketHistoryCollection
            Get
                Return _historyCollection
            End Get
            Private Set(ByVal value As MarketHistoryCollection)
                If Me._historyCollection IsNot value Then
                    Me._historyCollection = value
                    Me.RaisePropertyChanged(Function() Me.HistoryCollection)
                End If
            End Set
        End Property
    End Class
End Namespace
