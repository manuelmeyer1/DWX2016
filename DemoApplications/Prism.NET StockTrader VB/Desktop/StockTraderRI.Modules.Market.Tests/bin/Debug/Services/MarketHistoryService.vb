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
Imports StockTraderRI.Modules.Market.Properties
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Market.Services
    <Export(GetType(IMarketHistoryService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class MarketHistoryService
        Implements IMarketHistoryService
        Private _marketHistory As Dictionary(Of String, MarketHistoryCollection)

        Public Sub New()
            InitializeMarketHistory()
        End Sub

        Private Sub InitializeMarketHistory()
            Dim document As XDocument = XDocument.Parse(MarketHistory)

            _marketHistory = _
                document.Descendants("MarketHistoryItem").GroupBy(Function(x) x.Attribute("TickerSymbol").Value, _
                                                                    Function(x) _
                                                                       New MarketHistoryItem _
                                                                       With { _
                                                                       .DateTimeMarker = _
                                                                       Date.Parse(x.Attribute("Date").Value, _
                                                                                   CultureInfo.InvariantCulture), _
                                                                       .Value = _
                                                                       Decimal.Parse(x.Value, NumberStyles.Float, _
                                                                                      CultureInfo.InvariantCulture)}). _
                    ToDictionary(Function(group) group.Key, Function(group) New MarketHistoryCollection(group))
        End Sub

        Public Function GetPriceHistory(ByVal tickerSymbol As String) As MarketHistoryCollection _
            Implements IMarketHistoryService.GetPriceHistory
            Dim items As MarketHistoryCollection = _marketHistory(tickerSymbol)
            Return items
        End Function
    End Class
End Namespace
