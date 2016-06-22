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
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports StockTraderRI.Modules.WatchList.Tests.Mocks
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Watch.Services

Namespace StockTraderRI.Modules.WatchList.Tests.Services
    ''' <summary>
    ''' Summary description for WatchListServiceFixture
    ''' </summary>
    <TestClass()> _
    Public Class WatchListServiceFixture
        <TestMethod()> _
        Public Sub ServiceListensToAddWatchCommand()
            Dim service As New WatchListService(New MockMarketFeedService())
            Assert.AreEqual(0, service.RetrieveWatchList().Count)

            service.AddWatchCommand.Execute("Stock999")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
        End Sub

        <TestMethod()> _
        Public Sub ServiceListensToAddWatchCommandAndReturnsCommandParamsInList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("STOCK00")
            service.AddWatchCommand.Execute("STOCK99")

            Assert.AreEqual(2, service.RetrieveWatchList().Count)
            Assert.AreEqual(Of String)("STOCK00", service.RetrieveWatchList()(0))
            Assert.AreEqual(Of String)("STOCK99", service.RetrieveWatchList()(1))
        End Sub

        <TestMethod()> _
        Public Sub GetWatchListShouldReturnObservableCollection()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("Stock000")
            Dim watchList As ObservableCollection(Of String) = service.RetrieveWatchList()

            Dim collectionChanged As Boolean = False
            AddHandler watchList.CollectionChanged, Sub()
                                                        collectionChanged = True
                                                    End Sub

            service.AddWatchCommand.Execute("Stock111")

            Assert.AreEqual(True, collectionChanged)
        End Sub

        <TestMethod()> _
        Public Sub TickerSymbolGetsConvertedToUppercase()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("StockInMixedCase")

            Assert.AreEqual(Of String)("StockInMixedCase".ToUpper(CultureInfo.InvariantCulture), _
                                         service.RetrieveWatchList()(0))
        End Sub

        <TestMethod()> _
        Public Sub NullOrEmptyStringIsNotAddedToList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute(Nothing)
            service.AddWatchCommand.Execute(String.Empty)

            Assert.AreEqual(0, service.RetrieveWatchList().Count)
        End Sub

        <TestMethod()> _
        Public Sub AddingSameSymbolTwiceOnlyAddsItOnceToTheList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("DUPE")
            service.AddWatchCommand.Execute("DUPE")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
        End Sub

        <TestMethod()> _
        Public Sub DoesNotAddWatchIfSymbolDoesNotExistInMarketFeed()
            Dim marketFeedService As New MockMarketFeedService()
            marketFeedService.MockSymbolExists = False

            Dim service As New WatchListService(marketFeedService)

            service.AddWatchCommand.Execute("INEXISTENT")

            Assert.AreEqual(0, service.RetrieveWatchList().Count)
            Assert.AreEqual(Of String)("INEXISTENT", marketFeedService.SymbolExistsArgumentTickerSymbol)
        End Sub

        <TestMethod()> _
        Public Sub SymbolWithLeadingBlankSpacesIsAddedToList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("    FUND0")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
            Assert.AreEqual("FUND0", service.RetrieveWatchList()(0))
        End Sub

        <TestMethod()> _
        Public Sub SymbolWithTrailingBlankSpacesIsAddedToList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("FUND0    ")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
            Assert.AreEqual("FUND0", service.RetrieveWatchList()(0))
        End Sub

        <TestMethod()> _
        Public Sub AddingSameSymbolOneWithLeadingBlankSpacesTwiceOnlyAddsItOnceToTheList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("    FUND0")
            service.AddWatchCommand.Execute("FUND0")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
            Assert.AreEqual("FUND0", service.RetrieveWatchList()(0))
        End Sub

        <TestMethod()> _
        Public Sub AddingSameSymbolOneWithTrailingBlankSpacesTwiceOnlyAddsItOnceToTheList()
            Dim service As New WatchListService(New MockMarketFeedService())

            service.AddWatchCommand.Execute("FUND0    ")
            service.AddWatchCommand.Execute("FUND0")

            Assert.AreEqual(1, service.RetrieveWatchList().Count)
            Assert.AreEqual("FUND0", service.RetrieveWatchList()(0))
        End Sub

        <TestMethod()> _
        Public Sub ServiceExposesCommandInstance()
            Dim service As New WatchListService(New MockMarketFeedService())

            Assert.AreEqual(0, service.RetrieveWatchList().Count)
            service.AddWatchCommand.Execute("testSymbol")
            Assert.AreEqual(1, service.RetrieveWatchList().Count)
            Assert.AreEqual("TESTSYMBOL", service.RetrieveWatchList()(0))

        End Sub
    End Class

    Friend Class TestableWatchListService
        Inherits WatchListService

        Public Sub New(ByVal marketFeedService As IMarketFeedService)
            MyBase.New(marketFeedService)
        End Sub
    End Class
End Namespace
