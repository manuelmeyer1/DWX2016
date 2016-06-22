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
Imports StockTraderRI.Modules.News.Controllers
Imports StockTraderRI.Modules.News.Article
Imports Moq
Imports Microsoft.Practices.Prism.Regions
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.Events
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Modules.News.Tests.Controllers
    <TestClass()> _
    Public Class NewsControllerFixture
        <TestMethod()> _
        Public Sub WhenArticleViewModelSelectedArticleChanged_NewsReaderViewModelNewsArticleUpdated()
            ' Prepare
            Dim newsFeedService As INewsFeedService = New Mock(Of INewsFeedService)().Object
            Dim regionManager As IRegionManager = New Mock(Of IRegionManager)().Object

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockEventAggregator = New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim articleViewModel As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)
            Dim newsReaderViewModel As New NewsReaderViewModel()

            Dim controller = New NewsController(articleViewModel, newsReaderViewModel)

            Dim newsArticle As New NewsArticle() With {.Title = "SomeTitle", .Body = "Newsbody"}

            ' Act
            articleViewModel.SelectedArticle = newsArticle

            ' Verify
            Assert.AreSame(newsArticle, newsReaderViewModel.NewsArticle)
        End Sub

        Friend Class MockTickerSymbolSelectedEvent
            Inherits TickerSymbolSelectedEvent
            Public SubscribeArgumentAction As Action(Of String)
            Public SubscribeArgumentFilter As Predicate(Of String)

            Public Overloads Overrides Function Subscribe(ByVal action As Action(Of String), _
                                                           ByVal threadOption As ThreadOption, _
                                                           ByVal keepSubscriberReferenceAlive As Boolean, _
                                                           ByVal filter As Predicate(Of String)) As SubscriptionToken
                SubscribeArgumentAction = action
                SubscribeArgumentFilter = filter
                Return Nothing
            End Function
        End Class
    End Class
End Namespace
