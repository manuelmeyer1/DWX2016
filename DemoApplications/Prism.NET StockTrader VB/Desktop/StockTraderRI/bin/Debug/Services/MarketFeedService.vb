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
Imports System.Xml.Linq
Imports System.Globalization
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports StockTraderRI.Modules.Market.Properties
Imports StockTraderRI.Infrastructure.Interfaces
Imports System.Threading

Namespace StockTraderRI.Modules.Market.Services
    <Export(GetType(IMarketFeedService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class MarketFeedService
        Implements IMarketFeedService, IDisposable
        Private _EventAggregator As IEventAggregator

        Private Property EventAggregator() As IEventAggregator
            Get
                Return _EventAggregator
            End Get
            Set(ByVal value As IEventAggregator)
                _EventAggregator = value
            End Set
        End Property

        Private ReadOnly _priceList As New Dictionary(Of String, Decimal)()
        Private ReadOnly _volumeList As New Dictionary(Of String, Long)()
        Private Shared ReadOnly randomGenerator As New Random((CInt(Date.Now.Ticks Mod Integer.MaxValue)))
        Private _timer As Timer
        Private _refreshInterval As Integer = 10000
        Private ReadOnly _lockObject As New Object()

        <ImportingConstructor()> _
        Public Sub New(ByVal eventAggregator As IEventAggregator)
            Me.New(XDocument.Parse(Resources.Market), eventAggregator)
        End Sub

        Protected Sub New(ByVal document As XDocument, ByVal eventAggregator As IEventAggregator)
            Me.EventAggregator = eventAggregator
            _timer = New Timer(AddressOf TimerTick)

            Dim marketItemsElement = document.Element("MarketItems")
            Dim refreshRateAttribute = marketItemsElement.Attribute("RefreshRate")
            If refreshRateAttribute IsNot Nothing Then
                RefreshInterval = _
                    CalculateRefreshIntervalMillisecondsFromSeconds( _
                                                                     Integer.Parse(refreshRateAttribute.Value, _
                                                                                    CultureInfo.InvariantCulture))
            End If

            Dim itemElements = marketItemsElement.Elements("MarketItem")
            For Each item As XElement In itemElements
                Dim tickerSymbol As String = item.Attribute("TickerSymbol").Value
                Dim _
                    lastPrice As Decimal = _
                        Decimal.Parse(item.Attribute("LastPrice").Value, NumberStyles.Float, _
                                       CultureInfo.InvariantCulture)
                Dim volume As Long = Convert.ToInt64(item.Attribute("Volume").Value, CultureInfo.InvariantCulture)
                _priceList.Add(tickerSymbol, lastPrice)
                _volumeList.Add(tickerSymbol, volume)
            Next item
        End Sub

        Public Property RefreshInterval() As Integer
            Get
                Return _refreshInterval
            End Get
            Set(ByVal value As Integer)
                _refreshInterval = value
                _timer.Change(_refreshInterval, _refreshInterval)
            End Set
        End Property

        ''' <summary>
        ''' Callback for Timer
        ''' </summary>
        ''' <param name="state"></param>
        Private Sub TimerTick(ByVal state As Object)
            UpdatePrices()
        End Sub

        Public Function GetPrice(ByVal tickerSymbol As String) As Decimal Implements IMarketFeedService.GetPrice
            If Not SymbolExists(tickerSymbol) Then
                Throw New ArgumentException(MarketFeedTickerSymbolNotFoundException, "tickerSymbol")
            End If

            Return _priceList(tickerSymbol)
        End Function

        Public Function GetVolume(ByVal tickerSymbol As String) As Long Implements IMarketFeedService.GetVolume
            Return _volumeList(tickerSymbol)
        End Function

        Public Function SymbolExists(ByVal tickerSymbol As String) As Boolean _
            Implements IMarketFeedService.SymbolExists
            Return _priceList.ContainsKey(tickerSymbol)
        End Function

        Protected Sub UpdatePrice(ByVal tickerSymbol As String, ByVal newPrice As Decimal, ByVal newVolume As Long)
            SyncLock _lockObject
                _priceList(tickerSymbol) = newPrice
                _volumeList(tickerSymbol) = newVolume
            End SyncLock
            OnMarketPricesUpdated()
        End Sub

        Protected Sub UpdatePrices()
            SyncLock _lockObject
                For Each symbol As String In _priceList.Keys.ToArray()
                    Dim newValue As Decimal = _priceList(symbol)
                    newValue += Convert.ToDecimal(randomGenerator.NextDouble() * 10.0F) - 5D
                    _priceList(symbol) = If(newValue > 0, newValue, 0.1D)
                Next symbol
            End SyncLock
            OnMarketPricesUpdated()
        End Sub

        Private Sub OnMarketPricesUpdated()
            Dim clonedPriceList As Dictionary(Of String, Decimal) = Nothing
            SyncLock _lockObject
                clonedPriceList = New Dictionary(Of String, Decimal)(_priceList)
            End SyncLock
            EventAggregator.GetEvent(Of MarketPricesUpdatedEvent)().Publish(clonedPriceList)
        End Sub

        Private Shared Function CalculateRefreshIntervalMillisecondsFromSeconds(ByVal seconds As Integer) As Integer
            Return seconds * 1000
        End Function

#Region "IDisposable"

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not disposing Then
                Return
            End If
            If _timer IsNot Nothing Then
                _timer.Dispose()
            End If
            _timer = Nothing
        End Sub

        ' Use VB destructor syntax for finalization code.
        Protected Overrides Sub Finalize()
            Dispose(False)
        End Sub

#End Region
    End Class
End Namespace
