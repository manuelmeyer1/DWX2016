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
Imports StockTraderRI.Modules.Position.Interfaces
Imports Moq
Imports Microsoft.Practices.ServiceLocation
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Orders
Imports StockTraderRI.Infrastructure
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.Regions
Imports StockTraderRI.Modules.Position.Controllers
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Position.Tests.Mocks

Namespace StockTraderRI.Modules.Position.Tests.Controllers
    <TestClass()> _
    Public Class OrdersControllerFixture
        Private regionManager As MockRegionManager
        Private ordersRegion As MockRegion

        <TestInitialize()> _
        Public Sub SetUp()
            regionManager = New MockRegionManager()
            regionManager.Regions.Add("ActionRegion", New MockRegion())
            ordersRegion = New MockRegion()
            regionManager.Regions.Add("OrdersRegion", ordersRegion)
        End Sub

        <TestMethod()> _
        Public Sub BuyAndSellCommandsInvokeController()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim buyOrderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim sellOrderCompositePresenter = New MockOrderCompositePresentationModel()

                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              buyOrderCompositePresenter)
                controller.BuyCommand.Execute("STOCK01")

                Assert.AreEqual("STOCK01", controller.StartOrderArgumentTickerSymbol)
                Assert.AreEqual(TransactionType.Buy, controller.StartOrderArgumentTransactionType)

                ' Set new CompositePresentationModel to simulate resolution of new instance.
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              sellOrderCompositePresenter)
                controller.SellCommand.Execute("STOCK02")

                Assert.AreEqual("STOCK02", controller.StartOrderArgumentTickerSymbol)
                Assert.AreEqual(TransactionType.Sell, controller.StartOrderArgumentTransactionType)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub ControllerAddsViewIfNotPresent()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)

                Dim collapsibleRegion = CType(regionManager.Regions("ActionRegion"), MockRegion)

                Assert.AreEqual(Of Integer)(0, collapsibleRegion.AddedViews.Count)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")
                Assert.AreEqual(Of Integer)(1, collapsibleRegion.AddedViews.Count)

            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub ControllerAddsANewOrderOnStartOrder()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)

                Assert.AreEqual(Of Integer)(0, ordersRegion.AddedViews.Count)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")
                Assert.AreEqual(Of Integer)(1, ordersRegion.AddedViews.Count)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub NewOrderIsShownOrder()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)

                Assert.AreEqual(Of Integer)(0, ordersRegion.AddedViews.Count)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")
                Assert.AreSame(ordersRegion.SelectedItem, ordersRegion.AddedViews(0))
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub StartOrderHooksInstanceCommandsToGlobalSaveAllAndCancelAllCommands()
            Try

                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")

                Assert.IsFalse(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)
                commandProxy.SubmitAllOrdersCommand.Execute(Nothing)
                Assert.IsTrue(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)

                Assert.IsFalse(orderCompositePresenter.MockCancelCommand.ExecuteCalled)
                commandProxy.CancelAllOrdersCommand.Execute(Nothing)
                Assert.IsTrue(orderCompositePresenter.MockCancelCommand.ExecuteCalled)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub StartOrderHooksInstanceCommandsToGlobalSaveAndCancelCommands()
            Try

                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")

                Assert.IsFalse(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)
                commandProxy.SubmitOrderCommand.Execute(Nothing)
                Assert.IsTrue(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)

                Assert.IsFalse(orderCompositePresenter.MockCancelCommand.ExecuteCalled)
                commandProxy.CancelOrderCommand.Execute(Nothing)
                Assert.IsTrue(orderCompositePresenter.MockCancelCommand.ExecuteCalled)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub OnCloseViewRequestedTheControllerUnhooksGlobalCommands()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")

                Assert.AreEqual(1, ordersRegion.AddedViews.Count)

                ' Act
                orderCompositePresenter.RaiseCloseViewRequested()

                ' Verify
                Assert.AreEqual(0, ordersRegion.AddedViews.Count)

                commandProxy.SubmitAllOrdersCommand.Execute(Nothing)
                Assert.IsFalse(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)

                commandProxy.CancelAllOrdersCommand.Execute(Nothing)
                Assert.IsFalse(orderCompositePresenter.MockCancelCommand.ExecuteCalled)

                commandProxy.SubmitOrderCommand.Execute(Nothing)
                Assert.IsFalse(orderCompositePresenter.MockSubmitCommand.ExecuteCalled)

                commandProxy.CancelOrderCommand.Execute(Nothing)
                Assert.IsFalse(orderCompositePresenter.MockCancelCommand.ExecuteCalled)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub StartOrderCreatesCompositePMAndPassesCorrectInitInfo()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, Nothing)

                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")

                Assert.AreEqual("STOCK01", orderCompositePresenter.TransactionInfo.TickerSymbol)
                Assert.AreEqual(TransactionType.Buy, orderCompositePresenter.TransactionInfo.TransactionType)

            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub SubmitAllInstanceCommandHookedToGlobalSubmitAllCommands()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim accountPositionService = New MockAccountPositionService()
                accountPositionService.AddPosition("STOCK01", 10D, 100)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, accountPositionService)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")

                Assert.IsFalse(controller.SubmitAllCommandCalled)
                commandProxy.SubmitAllOrdersCommand.CanExecute(Nothing)
                Assert.IsTrue(controller.SubmitAllCommandCalled)

            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try

        End Sub

        <TestMethod()> _
        Public Sub CannotSellMoreSharesThanAreOwned()
            Try

                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim accountPositionService = New MockAccountPositionService()
                accountPositionService.AddPosition("STOCK01", 10D, 100)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, accountPositionService)

                ' Act
                Dim buyOrder = New MockOrderCompositePresentationModel() With {.privateShares = 100}
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns(buyOrder)
                controller.InvokeStartOrder(TransactionType.Buy, "STOCK01")
                Assert.IsTrue(controller.SubmitAllVoteOnlyCommand.CanExecute())

                Dim sellOrder = New MockOrderCompositePresentationModel() With {.privateShares = 200}
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns(sellOrder)
                controller.InvokeStartOrder(TransactionType.Sell, "STOCK01")

                'Should not be able to sell even though owned shares==100, buy==100 and sell==200
                Assert.IsFalse(controller.SubmitAllVoteOnlyCommand.CanExecute())
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub CannotSellMoreSharesThanAreOwnedInDifferentOrders()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim accountPositionService = New MockAccountPositionService()
                accountPositionService.AddPosition("STOCK01", 10D, 100)

                Dim controller = New TestableOrdersController(regionManager, commandProxy, accountPositionService)
                Dim sellOrder1 = New MockOrderCompositePresentationModel() With {.privateShares = 100}
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns(sellOrder1)

                controller.InvokeStartOrder(TransactionType.Sell, "STOCK01")

                Assert.IsTrue(controller.SubmitAllVoteOnlyCommand.CanExecute())

                Dim sellOrder2 = New MockOrderCompositePresentationModel() With {.privateShares = 100}
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns(sellOrder2)

                controller.InvokeStartOrder(TransactionType.Sell, "stock01")

                Assert.IsFalse(controller.SubmitAllVoteOnlyCommand.CanExecute())
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub CannotSellMoreSharesThatAreNotOwned()
            Try

                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel() With {.privateShares = 1}
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim _
                    controller = _
                        New TestableOrdersController(regionManager, New MockStockTraderRICommandProxy(), _
                                                      New MockAccountPositionService())
                controller.InvokeStartOrder(TransactionType.Sell, "NOTOWNED")

                Assert.IsFalse(controller.SubmitAllVoteOnlyCommand.CanExecute())
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub CannotSubmitAllWhenNothingToSubmit()
            Dim _
                controller = _
                    New TestableOrdersController(New MockRegionManager(), New MockStockTraderRICommandProxy(), _
                                                  New MockAccountPositionService())

            Assert.IsFalse(controller.SubmitAllVoteOnlyCommand.CanExecute())
        End Sub

        <TestMethod()> _
        Public Sub AfterAllOrdersSubmittedSubmitAllCommandShouldBeDisabled()
            Try
                Dim mockOrdersView As New Mock(Of IOrdersView)()
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()
                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel()
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositeView As IOrdersView = mockOrdersView.Object
                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns(orderCompositeView)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim _
                    controller = _
                        New TestableOrdersController(regionManager, commandProxy, New MockAccountPositionService())

                Dim buyOrder = New MockOrderCompositePresentationModel() With {.privateShares = 100}
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns(buyOrder)

                controller.InvokeStartOrder(TransactionType.Buy, "STOCK1")

                Dim canExecuteChangedCalled As Boolean = False
                Dim canExecuteResult As Boolean = False

                AddHandler commandProxy.SubmitAllOrdersCommand.CanExecuteChanged, Sub()
                                                                                      canExecuteChangedCalled = True
                                                                                      canExecuteResult = controller.SubmitAllVoteOnlyCommand.CanExecute()
                                                                                  End Sub
                buyOrder.RaiseCloseViewRequested()

                Assert.IsTrue(canExecuteChangedCalled)
                Assert.IsFalse(canExecuteResult)
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        <TestMethod()> _
        Public Sub ShouldRemoveOrdersViewWhenClosingLastOrder()
            Try
                Dim mockOrdersViewModel As New Mock(Of IOrdersViewModel)()

                Dim mockServiceLocator As New Mock(Of ServiceLocatorImplBase)()

                Dim orderCompositePresenter = New MockOrderCompositePresentationModel() With {.privateShares = 100}
                Dim commandProxy = New MockStockTraderRICommandProxy()

                Dim orderCompositePresentationModel As IOrdersViewModel = mockOrdersViewModel.Object

                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersView)()).Returns( _
                                                                                                 New  _
                                                                                                    Mock(Of IOrdersView)() _
                                                                                                    .Object)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrdersViewModel)()).Returns( _
                                                                                                      orderCompositePresentationModel)
                mockServiceLocator.Setup(Function(x) x.GetInstance(Of IOrderCompositeViewModel)()).Returns( _
                                                                                                              orderCompositePresenter)
                ServiceLocator.SetLocatorProvider(Function() mockServiceLocator.Object)

                Dim _
                    controller = _
                        New TestableOrdersController(regionManager, commandProxy, New MockAccountPositionService())

                Dim region = CType(regionManager.Regions("ActionRegion"), MockRegion)

                controller.InvokeStartOrder(TransactionType.Buy, "STOCK1")

                Assert.AreEqual(Of Integer)(1, region.AddedViews.Count)

                orderCompositePresenter.RaiseCloseViewRequested()

                Assert.AreEqual(Of Integer)(0, region.AddedViews.Count)

            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub
    End Class

    Friend Class TestableOrdersController
        Inherits OrdersController

        Public Sub New(ByVal regionManager As IRegionManager, ByVal commandProxy As MockStockTraderRICommandProxy, _
                        ByVal accountPositionService As IAccountPositionService)
            MyBase.New(regionManager, commandProxy, accountPositionService)
        End Sub

        Private _StartOrderArgumentTickerSymbol As String

        Public Property StartOrderArgumentTickerSymbol() As String
            Get
                Return _StartOrderArgumentTickerSymbol
            End Get
            Set(ByVal value As String)
                _StartOrderArgumentTickerSymbol = value
            End Set
        End Property

        Private _StartOrderArgumentTransactionType As TransactionType

        Public Property StartOrderArgumentTransactionType() As TransactionType
            Get
                Return _StartOrderArgumentTransactionType
            End Get
            Set(ByVal value As TransactionType)
                _StartOrderArgumentTransactionType = value
            End Set
        End Property

        Protected Overrides Sub StartOrder(ByVal tickerSymbol As String, ByVal transactionType As TransactionType)
            MyBase.StartOrder(tickerSymbol, transactionType)

            StartOrderArgumentTickerSymbol = tickerSymbol
            StartOrderArgumentTransactionType = transactionType
        End Sub

        Public Sub InvokeStartOrder(ByVal transactionType As TransactionType, ByVal symbol As String)
            StartOrder(symbol, transactionType)
        End Sub

        Public SubmitAllCommandCalled As Boolean = False

        Protected Overrides Function SubmitAllCanExecute() As Boolean
            SubmitAllCommandCalled = True
            Return MyBase.SubmitAllCanExecute()
        End Function
    End Class

    Public Class MockOrdersViewModel
        Implements IOrdersViewModel

        Public ReadOnly Property HeaderInfo() As String Implements IHeaderInfoProvider(Of String).HeaderInfo
            Get
                Throw New NotImplementedException()
            End Get
        End Property
    End Class

    Friend Class MockOrderCompositePresentationModel
        Implements IOrderCompositeViewModel
        Public MockSubmitCommand As New MockCommand()
        Public MockCancelCommand As New MockCommand()

        Public Event CloseViewRequested As EventHandler Implements IOrderCompositeViewModel.CloseViewRequested

        Public ReadOnly Property SubmitCommand() As ICommand Implements IOrderCompositeViewModel.SubmitCommand
            Get
                Return MockSubmitCommand
            End Get
        End Property

        Public ReadOnly Property CancelCommand() As ICommand Implements IOrderCompositeViewModel.CancelCommand
            Get
                Return MockCancelCommand
            End Get
        End Property

        Private _TransactionInfo As TransactionInfo

        Public Property TransactionInfo() As TransactionInfo Implements IOrderCompositeViewModel.TransactionInfo
            Get
                Return _TransactionInfo
            End Get
            Set(ByVal value As TransactionInfo)
                _TransactionInfo = value
            End Set
        End Property

        Friend privateShares As Integer

        Public ReadOnly Property Shares() As Integer Implements IOrderCompositeViewModel.Shares
            Get
                Return privateShares
            End Get
        End Property

        Friend Sub RaiseCloseViewRequested()
            RaiseEvent CloseViewRequested(Me, EventArgs.Empty)
        End Sub
    End Class

    Friend Class MockCommand
        Implements ICommand
        Public ExecuteCalled As Boolean

        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
            ExecuteCalled = True
        End Sub
    End Class
End Namespace

