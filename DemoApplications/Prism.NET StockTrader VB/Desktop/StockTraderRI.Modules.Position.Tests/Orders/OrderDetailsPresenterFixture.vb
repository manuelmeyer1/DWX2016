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
Imports System.Reflection
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Modules.Position.Tests.Mocks
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Interfaces
Imports StockTraderRI.Modules.Position.Orders
Imports StockTraderRI.Infrastructure.Interfaces
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Modules.Position.Tests.Orders
    <TestClass()> _
    Public Class OrderDetailsPresenterFixture
        <TestMethod()> _
        Public Sub PresenterCreatesPublicSubmitCommand()
            Dim presenter = CreatePresentationModel(Nothing)

            Assert.IsNotNull(presenter.SubmitCommand)
        End Sub

        <TestMethod()> _
        Public Sub CanExecuteChangedIsRaisedForSubmitCommandWhenModelBecomesValid()
            Dim canExecuteChanged As Boolean = False
            Dim presenter = CreatePresentationModel(Nothing)
            AddHandler presenter.SubmitCommand.CanExecuteChanged, Sub()
                                                                      canExecuteChanged = True
                                                                  End Sub
            presenter.Shares = 2
            canExecuteChanged = False

            presenter.StopLimitPrice = 2

            Assert.IsTrue(canExecuteChanged)
        End Sub

        <TestMethod(), ExpectedException(GetType(InputValidationException))> _
        Public Sub NonPositiveSharesThrows()
            Dim presenter = CreatePresentationModel(Nothing)

            presenter.Shares = 0
        End Sub

        <TestMethod()> _
        Public Sub CannotSubmitWhenSharesIsNotPositive()
            Dim presenter = CreatePresentationModel(Nothing)

            presenter.Shares = 2
            presenter.StopLimitPrice = 2
            Assert.IsTrue(presenter.SubmitCommand.CanExecute(Nothing))

            Try
                presenter.Shares = 0
            Catch e1 As InputValidationException
            End Try

            Assert.IsFalse(presenter.SubmitCommand.CanExecute(Nothing))
        End Sub

        <TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
        Public Sub SubmitThrowsIfCanExecuteIsFalse()
            Dim presenter = CreatePresentationModel(New MockAccountPositionService())
            Try
                presenter.Shares = 0
            Catch e1 As InputValidationException
            End Try

            Assert.IsFalse(presenter.SubmitCommand.CanExecute(Nothing))

            presenter.SubmitCommand.Execute(Nothing)
        End Sub

        <TestMethod()> _
        Public Sub CancelRaisesCloseViewEvent()
            Dim closeViewRaised As Boolean = False

            Dim presenter = CreatePresentationModel(Nothing)
            AddHandler presenter.CloseViewRequested, Sub()
                                                         closeViewRaised = True
                                                     End Sub

            presenter.CancelCommand.Execute(Nothing)

            Assert.IsTrue(closeViewRaised)
        End Sub

        <TestMethod()> _
        Public Sub SubmitRaisesCloseViewEvent()
            Dim closeViewRaised As Boolean = False

            Dim presenter = CreatePresentationModel(New MockAccountPositionService())
            AddHandler presenter.CloseViewRequested, Sub()
                                                         closeViewRaised = True
                                                     End Sub

            presenter.TransactionType = TransactionType.Buy
            presenter.Shares = 1
            presenter.StopLimitPrice = 1
            Assert.IsTrue(presenter.SubmitCommand.CanExecute(Nothing))
            presenter.SubmitCommand.Execute(Nothing)

            Assert.IsTrue(closeViewRaised)
        End Sub

        <TestMethod()> _
        Public Sub CannotSubmitOnSellWhenSharesIsHigherThanCurrentPosition()
            Dim accountPositionService = New MockAccountPositionService()
            accountPositionService.AddPosition(New AccountPosition("TESTFUND", 10D, 15))
            Dim presenter = CreatePresentationModel(accountPositionService)

            presenter.TickerSymbol = "TESTFUND"
            presenter.TransactionType = TransactionType.Sell
            presenter.Shares = 5
            presenter.StopLimitPrice = 1
            Assert.IsTrue(presenter.SubmitCommand.CanExecute(Nothing))

            Try
                presenter.Shares = 16
            Catch e1 As InputValidationException
            End Try
            Assert.IsFalse(presenter.SubmitCommand.CanExecute(Nothing))
        End Sub

        <TestMethod()> _
        Public Sub SharesIsHigherThanCurrentPositionOnSellThrows()
            Dim accountPositionService = New MockAccountPositionService()
            accountPositionService.AddPosition(New AccountPosition("TESTFUND", 10D, 15))
            Dim presenter = CreatePresentationModel(accountPositionService)

            presenter.TickerSymbol = "TESTFUND"
            presenter.TransactionType = TransactionType.Sell

            Try
                presenter.Shares = 16
            Catch e1 As InputValidationException
                Return
            End Try
            Assert.Fail("Exception not thrown.")
        End Sub

        <TestMethod()> _
        Public Sub PresenterCreatesCallSetOrderTypes()
            Dim presenter = New OrderDetailsViewModel(Nothing, Nothing)

            Assert.IsNotNull(presenter.AvailableOrderTypes)
            Assert.IsTrue(presenter.AvailableOrderTypes.Count > 0)
            Assert.AreEqual(GetEnumCount(GetType(OrderType)), presenter.AvailableOrderTypes.Count)
        End Sub

        <TestMethod()> _
        Public Sub PresenterCreatesCallSetTimeInForce()
            Dim presenter = New OrderDetailsViewModel(Nothing, Nothing)
            Assert.IsNotNull(presenter.AvailableTimesInForce)
            Assert.IsTrue(presenter.AvailableTimesInForce.Count > 0)
            Assert.AreEqual(GetEnumCount(GetType(TimeInForce)), presenter.AvailableTimesInForce.Count)
        End Sub

        <TestMethod()> _
        Public Sub SetTransactionInfoShouldUpdateTheModel()
            Dim presenter = New OrderDetailsViewModel(New MockAccountPositionService(), Nothing)
            presenter.TransactionInfo = _
                New TransactionInfo With {.TickerSymbol = "T000", .TransactionType = TransactionType.Sell}

            Assert.AreEqual("T000", presenter.TickerSymbol)
            Assert.AreEqual(TransactionType.Sell, presenter.TransactionType)
        End Sub

        <TestMethod()> _
        Public Sub SubmitCallsServiceWithCorrectOrder()
            Dim ordersService = New MockOrdersService()
            Dim presenter = New OrderDetailsViewModel(New MockAccountPositionService(), ordersService)
            presenter.Shares = 2
            presenter.TickerSymbol = "AAAA"
            presenter.TransactionType = TransactionType.Buy
            presenter.TimeInForce = TimeInForce.EndOfDay
            presenter.OrderType = OrderType.Limit
            presenter.StopLimitPrice = 15

            Assert.IsNull(ordersService.SubmitArgumentOrder)
            presenter.SubmitCommand.Execute(Nothing)

            Dim submittedOrder = ordersService.SubmitArgumentOrder
            Assert.IsNotNull(submittedOrder)
            Assert.AreEqual("AAAA", submittedOrder.TickerSymbol)
            Assert.AreEqual(TransactionType.Buy, submittedOrder.TransactionType)
            Assert.AreEqual(TimeInForce.EndOfDay, submittedOrder.TimeInForce)
            Assert.AreEqual(OrderType.Limit, submittedOrder.OrderType)
            Assert.AreEqual(15D, submittedOrder.StopLimitPrice)
        End Sub

        <TestMethod()> _
        Public Sub VerifyTransactionInfoModificationsInOrderDetails()
            Dim orderDetailsPresenter = New OrderDetailsViewModel(New MockAccountPositionService(), Nothing)
            Dim _
                transactionInfo = _
                    New TransactionInfo With {.TickerSymbol = "Fund0", .TransactionType = TransactionType.Buy}
            orderDetailsPresenter.TransactionInfo = transactionInfo
            orderDetailsPresenter.TransactionType = TransactionType.Sell
            Assert.AreEqual(TransactionType.Sell, transactionInfo.TransactionType)

            orderDetailsPresenter.TickerSymbol = "Fund1"
            Assert.AreEqual("Fund1", transactionInfo.TickerSymbol)
        End Sub

        <TestMethod()> _
        Public Sub CannotSubmitIfStopLimitZero()
            Dim accountPositionService = New MockAccountPositionService()
            accountPositionService.AddPosition(New AccountPosition("TESTFUND", 10D, 15))
            Dim presenter = CreatePresentationModel(accountPositionService)

            presenter.TickerSymbol = "TESTFUND"
            presenter.TransactionType = TransactionType.Sell
            presenter.Shares = 5
            presenter.StopLimitPrice = 1
            Assert.IsTrue(presenter.SubmitCommand.CanExecute(Nothing))

            Try
                presenter.StopLimitPrice = 0
            Catch e1 As InputValidationException
            End Try

            Assert.IsFalse(presenter.SubmitCommand.CanExecute(Nothing))
        End Sub

        '[TestMethod]
        'public void ShouldSetStopLimitPriceInModel()
        '{
        '    var accountPositionService = new MockAccountPositionService();
        '    accountPositionService.AddPosition(new AccountPosition("TESTFUND", 10m, 15));
        '    var presenter = CreatePresentationModel(new MockOrderDetailsView(), accountPositionService);

        '    presenter.TickerSymbol = "TESTFUND";
        '    presenter.TransactionType = TransactionType.Sell;
        '    presenter.Shares = 5;
        '    presenter.StopLimitPrice = 0;

        '    Assert.AreEqual<string>("The stop limit price must be greater than 0", presenter["StopLimitPrice"]);
        '}

        '[TestMethod]
        'public void DisposeUnregistersLocalCommandsFromGlobalCommands()
        '{
        '    var presenter = new TestableOrderDetailsPresentationModel(new MockOrderDetailsView(), null);
        '    Assert.IsTrue(StockTraderRICommands.SubmitOrderCommand.
        '}
        <TestMethod()> _
        Public Sub PropertyChangedIsRaisedWhenSharesIsChanged()
            Dim presenter = New OrderDetailsViewModel(Nothing, Nothing)
            presenter.Shares = 5

            Dim sharesPropertyChangedRaised As Boolean = False
            AddHandler presenter.PropertyChanged, Sub(sender, e)
                                                      If e.PropertyName = "Shares" Then
                                                          sharesPropertyChangedRaised = True
                                                      End If
                                                  End Sub
            presenter.Shares = 1
            Assert.IsTrue(sharesPropertyChangedRaised)
        End Sub

        Private Shared Function GetEnumCount(ByVal enumType As Type) As Integer
            Dim availableOrderTypes As Array
#If SILVERLIGHT Then
            availableOrderTypes = enumType.GetFields(BindingFlags.Public Or BindingFlags.Static)
#Else
            availableOrderTypes = System.Enum.GetValues(enumType)
#End If
            Return availableOrderTypes.Length
        End Function

        Private Shared Function CreatePresentationModel(ByVal accountPositionService As IAccountPositionService) _
            As OrderDetailsViewModel
            Return New OrderDetailsViewModel(accountPositionService, New MockOrdersService())
        End Function
    End Class

    Friend Class MockOrdersService
        Implements IOrdersService
        Public SubmitArgumentOrder As Order

        Public Sub Submit(ByVal order As Order) Implements IOrdersService.Submit
            SubmitArgumentOrder = order
        End Sub
    End Class
End Namespace
