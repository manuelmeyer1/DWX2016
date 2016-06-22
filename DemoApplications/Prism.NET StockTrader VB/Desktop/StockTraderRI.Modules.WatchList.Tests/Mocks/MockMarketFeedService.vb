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
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.WatchList.Tests.Mocks
    Public Class MockMarketFeedService
        Implements IMarketFeedService
        Public MockSymbolExists As Boolean = True
        Public SymbolExistsArgumentTickerSymbol As String

        Public feedData As New Dictionary(Of String, Decimal)()

        Friend Sub SetPrice(ByVal tickerSymbol As String, ByVal price As Decimal)
            feedData.Add(tickerSymbol, price)
        End Sub

#Region "IMarketFeedService Members"

        Public Function GetPrice(ByVal tickerSymbol As String) As Decimal Implements IMarketFeedService.GetPrice
            If Not MockSymbolExists Then
                Throw New ArgumentException()
            End If

            If feedData.ContainsKey(tickerSymbol) Then
                Return feedData(tickerSymbol)
            Else
                Return 0D
            End If
        End Function

        Public Function GetVolume(ByVal tickerSymbol As String) As Long Implements IMarketFeedService.GetVolume
            Throw New NotImplementedException()
        End Function

        Public Event Updated As EventHandler

#End Region

        Public Function SymbolExists(ByVal tickerSymbol As String) As Boolean _
            Implements IMarketFeedService.SymbolExists
            SymbolExistsArgumentTickerSymbol = tickerSymbol
            Return MockSymbolExists
        End Function
    End Class
End Namespace
