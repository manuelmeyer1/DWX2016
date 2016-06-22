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

Namespace StockTraderRI.Infrastructure.Interfaces
    Public Interface INewsFeedService
        Function GetNews(ByVal tickerSymbol As String) As IList(Of NewsArticle)
        Function HasNews(ByVal tickerSymbol As String) As Boolean
        Event Updated As EventHandler(Of NewsFeedEventArgs)
    End Interface
End Namespace
