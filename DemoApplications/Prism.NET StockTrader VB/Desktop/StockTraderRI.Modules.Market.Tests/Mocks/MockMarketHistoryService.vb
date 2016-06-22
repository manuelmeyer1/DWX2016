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
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Market.Tests.Mocks
    Friend Class MockMarketHistoryService
        Implements IMarketHistoryService
        Public GetPriceHistoryCalled As Boolean
        Public GetPriceHistoryArgument As String
        Public Data As New MarketHistoryCollection()

        Public Sub New()
            Data.Add(New MarketHistoryItem With {.DateTimeMarker = New Date(2008, 1, 1), .Value = 10D})
            Data.Add(New MarketHistoryItem With {.DateTimeMarker = New Date(2008, 6, 1), .Value = 15D})
        End Sub

        Public Function GetPriceHistory(ByVal tickerSymbol As String) As MarketHistoryCollection _
            Implements IMarketHistoryService.GetPriceHistory
            GetPriceHistoryCalled = True
            GetPriceHistoryArgument = tickerSymbol

            Return Data
        End Function
    End Class
End Namespace
