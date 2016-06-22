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
Imports StockTraderRI.Modules.News.Properties
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.News.Services
    <Export(GetType(INewsFeedService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class NewsFeedService
        Implements INewsFeedService
        Private ReadOnly newsData As New Dictionary(Of String, List(Of NewsArticle))()

        Public Sub New()
            Dim document = XDocument.Parse(Resources.News)
            newsData = _
                document.Descendants("NewsItem").GroupBy(Function(x) x.Attribute("TickerSymbol").Value, _
                                                           Function(x) _
                                                              New NewsArticle _
                                                              With { _
                                                              .PublishedDate = _
                                                              Date.Parse(x.Attribute("PublishedDate").Value, _
                                                                          CultureInfo.InvariantCulture), _
                                                              .Title = x.Element("Title").Value, _
                                                              .Body = x.Element("Body").Value, _
                                                              .IconUri = _
                                                              If _
                                                              (x.Attribute("IconUri") IsNot Nothing, _
                                                               x.Attribute("IconUri").Value, Nothing)}).ToDictionary( _
                                                                                                                       Function _
                                                                                                                          ( _
                                                                                                                          group _
                                                                                                                          ) _
                                                                                                                          group _
                                                                                                                          . _
                                                                                                                          Key, _
                                                                                                                       Function _
                                                                                                                          ( _
                                                                                                                          group _
                                                                                                                          ) _
                                                                                                                          group _
                                                                                                                          . _
                                                                                                                          ToList())
        End Sub

#Region "INewsFeed Members"

        Public Function GetNews(ByVal tickerSymbol As String) As IList(Of NewsArticle) _
            Implements INewsFeedService.GetNews
            Dim articles As New List(Of NewsArticle)()
            newsData.TryGetValue(tickerSymbol, articles)
            Return articles
        End Function

        Public Event Updated As EventHandler(Of NewsFeedEventArgs) Implements INewsFeedService.Updated

        Public Function HasNews(ByVal tickerSymbol As String) As Boolean Implements INewsFeedService.HasNews
            Return newsData.ContainsKey(tickerSymbol)
        End Function

#End Region
    End Class
End Namespace
