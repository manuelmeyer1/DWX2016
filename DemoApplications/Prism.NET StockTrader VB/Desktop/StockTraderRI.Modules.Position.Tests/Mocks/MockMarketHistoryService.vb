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

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Public Class MockMarketHistoryService
        Implements IMarketHistoryService

#Region "IMarketHistoryService Members"

        Public Function GetPriceHistory(ByVal tickerSymbol As String) As MarketHistoryCollection _
            Implements IMarketHistoryService.GetPriceHistory
            'return new MarketHistoryCollection { 
            '    new MarketHistoryItem { DateTimeMarker = new DateTime(1), Value = 1.00m }
            '    , new MarketHistoryItem { DateTimeMarker = new DateTime(2), Value =  2.00m}
            '};

            Throw New NotImplementedException()
        End Function

#End Region
    End Class
End Namespace
