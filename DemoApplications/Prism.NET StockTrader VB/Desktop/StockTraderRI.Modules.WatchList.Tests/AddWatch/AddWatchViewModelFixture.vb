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
Imports System.Windows.Input
Imports StockTraderRI.Modules.Watch.AddWatch
Imports Microsoft.Practices.Prism.Commands
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports StockTraderRI.Modules.Watch.Services

Namespace StockTraderRI.Modules.WatchList.Tests.AddWatch
    <TestClass()> _
    Public Class AddWatchViewModelFixture
        <TestMethod()> _
        Public Sub WhenConstructed_IntializesValues()
            ' Prepare
            Dim addWatchCommand As ICommand = New DelegateCommand(Sub()

                                                                  End Sub)

            Dim mockWatchListService As New Mock(Of IWatchListService)()
            mockWatchListService.SetupGet(Function(x) x.AddWatchCommand).Returns(addWatchCommand)

            Dim watchListService As IWatchListService = mockWatchListService.Object

            ' Act
            Dim actual As New AddWatchViewModel(watchListService)

            ' Verify
            Assert.IsNotNull(actual)
            Assert.AreEqual(addWatchCommand, actual.AddWatchCommand)
        End Sub

        <TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
        Public Sub WhenConstructedWithNullWatchListService_Throws()
            ' Prepare
            Dim watchListService As IWatchListService = Nothing

            ' Act
            Dim actual As New AddWatchViewModel(watchListService)

            ' Verify
        End Sub

        <TestMethod()> _
        Public Sub WhenStockSymbolSet_PropertyIsUpdated()
            ' Prepare
            Dim stockSymbol As String = "StockSymbol"

            Dim mockWatchListService As New Mock(Of IWatchListService)()

            Dim watchListService As IWatchListService = mockWatchListService.Object

            Dim target As New AddWatchViewModel(watchListService)

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "StockSymbol" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            ' Act
            target.StockSymbol = stockSymbol

            ' Verify
            Assert.AreEqual(stockSymbol, target.StockSymbol)
            Assert.IsTrue(propertyChangedRaised)
        End Sub
    End Class
End Namespace
