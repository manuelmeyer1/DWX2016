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
Imports StockTraderRI.Modules.News.Article
Imports Microsoft.Practices.Prism.Events
Imports StockTraderRI.Infrastructure
Imports Moq
Imports Microsoft.Practices.Prism.Regions
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Modules.News.Tests.Article
    <TestClass()> _
    Public Class ArticleViewModelFixture
        <TestMethod()> _
        Public Sub WhenConstructed_InitializesValues()
            ' Prepare
            Dim newsFeedService As INewsFeedService = New Mock(Of INewsFeedService)().Object
            Dim regionManager As IRegionManager = New Mock(Of IRegionManager)().Object

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockEventAggregator = New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            ' Act
            Dim actual As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            ' Verify
            Assert.IsNull(actual.Articles)
            Assert.IsNull(actual.SelectedArticle)
            Assert.IsNotNull(actual.ShowArticleListCommand)
            Assert.IsNotNull(actual.ShowNewsReaderCommand)
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
        Public Sub WhenConstructedWithNullNewsFeedService_Throws()
            ' Prepare
            Dim newsFeedService As INewsFeedService = Nothing
            Dim regionManager As IRegionManager = New Mock(Of IRegionManager)().Object
            Dim eventAggregator As IEventAggregator = New Mock(Of IEventAggregator)().Object

            ' Act
            Dim actual As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            ' Verify
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
        Public Sub WhenConstructedWithNullRegionManager_Throws()
            ' Prepare
            Dim newsFeedService As INewsFeedService = New Mock(Of INewsFeedService)().Object
            Dim regionManager As IRegionManager = Nothing
            Dim eventAggregator As IEventAggregator = New Mock(Of IEventAggregator)().Object

            ' Act
            Dim actual As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            ' Verify
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
        Public Sub WhenConstructedWithNullEventAggregator_Throws()
            ' Prepare
            Dim newsFeedService As INewsFeedService = New Mock(Of INewsFeedService)().Object
            Dim regionManager As IRegionManager = New Mock(Of IRegionManager)().Object
            Dim eventAggregator As IEventAggregator = Nothing

            ' Act
            Dim actual As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            ' Verify
        End Sub

        <TestMethod()> _
        Public Sub WhenCompanySymbolSet_PropertyIsUpdated()
            ' Prepare
            Dim companySymbol As String = "CompanySymbol"

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockNewsFeedService As New Mock(Of INewsFeedService)()
            Dim mockRegionManager As New Mock(Of IRegionManager)()
            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)

            Dim newsFeedService As INewsFeedService = mockNewsFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "CompanySymbol" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.CompanySymbol = companySymbol

            ' Verify
            Assert.AreEqual(companySymbol, target.CompanySymbol)
            Assert.IsTrue(propertyChangedRaised)
        End Sub

        <TestMethod()> _
        Public Sub WhenCompanySymbolSet_NewsArticlesAreRetrieved()
            ' Prepare
            Dim companySymbol As String = "CompanySymbol"

            Dim articles As New List(Of NewsArticle)()

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockNewsFeedService As New Mock(Of INewsFeedService)()
            mockNewsFeedService.Setup(Function(x) x.GetNews(companySymbol)).Returns(articles).Verifiable()
            Dim mockRegionManager As New Mock(Of IRegionManager)()
            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)

            Dim newsFeedService As INewsFeedService = mockNewsFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "Articles" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.CompanySymbol = companySymbol

            ' Verify
            mockNewsFeedService.VerifyAll()
            Assert.AreSame(target.Articles, articles)
            Assert.IsTrue(propertyChangedRaised)
        End Sub

        <TestMethod()> _
        Public Sub WhenSelectedArticleSet_PropertyIsUpdated()
            ' Prepare
            Dim newsArticle As New NewsArticle()

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockNewsFeedService As New Mock(Of INewsFeedService)()
            Dim mockRegionManager As New Mock(Of IRegionManager)()
            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)

            Dim newsFeedService As INewsFeedService = mockNewsFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "SelectedArticle" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.SelectedArticle = newsArticle

            ' Verify
            Assert.AreEqual(newsArticle, target.SelectedArticle)
            Assert.IsTrue(propertyChangedRaised)
        End Sub

        <TestMethod()> _
        Public Sub WhenShowNewsReaderCommandInvokes_RegionIsNavigated()
            ' Prepare
            Dim newsArticle As New NewsArticle()

            Dim tickerSymbolSelectedEvent = New Mock(Of TickerSymbolSelectedEvent)().Object

            Dim mockNewsFeedService As New Mock(Of INewsFeedService)()
            Dim mockRegionManager As New Mock(Of IRegionManager)()
            Dim mockEventAggregator As New Mock(Of IEventAggregator)()
            mockEventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                         tickerSymbolSelectedEvent)

            Dim newsFeedService As INewsFeedService = mockNewsFeedService.Object
            Dim regionManager As IRegionManager = mockRegionManager.Object
            Dim eventAggregator As IEventAggregator = mockEventAggregator.Object

            Dim target As New ArticleViewModel(newsFeedService, regionManager, eventAggregator)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "SelectedArticle" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.SelectedArticle = newsArticle

            ' Verify
            Assert.AreEqual(newsArticle, target.SelectedArticle)
            Assert.IsTrue(propertyChangedRaised)
        End Sub
    End Class
End Namespace
