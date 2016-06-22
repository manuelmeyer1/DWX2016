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
Imports StockTraderRI.Infrastructure
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Orders
Imports StockTraderRI.Modules.Position.Tests.Mocks

Namespace StockTraderRI.Modules.Position.Tests.Orders
    ''' <summary>
    ''' Summary description for OrderCompositePresenterFixture
    ''' </summary>
    <TestClass()> _
    Public Class OrderCompositePresentationModelFixture
        <TestMethod()> _
        Public Sub ShouldCreateOrderdetailsViewModel()
            Dim detailsViewModel = New MockOrderDetailsViewModel()

            Dim composite = New OrderCompositeViewModel(detailsViewModel)

            composite.TransactionInfo = New TransactionInfo("FXX01", TransactionType.Sell)

            Assert.IsNotNull(detailsViewModel.TransactionInfo)
        End Sub

        <TestMethod()> _
        Public Sub ShouldAddDetailsViewAndControlsViewToContentArea()
            Dim detailsViewModel = New MockOrderDetailsViewModel()

            Dim composite = New OrderCompositeViewModel(detailsViewModel)

            Assert.AreSame(detailsViewModel, composite.OrderDetails)
        End Sub

        <TestMethod()> _
        Public Sub PresenterExposesChildOrderPresentersCloseRequested()
            Dim detailsViewModel = New MockOrderDetailsViewModel()

            Dim composite = New OrderCompositeViewModel(detailsViewModel)

            Dim closeViewRequestedFired = False
            AddHandler composite.CloseViewRequested, Sub()
                                                         closeViewRequestedFired = True
                                                     End Sub

            detailsViewModel.RaiseCloseViewRequested()

            Assert.IsTrue(closeViewRequestedFired)

        End Sub

        <TestMethod()> _
        Public Sub TransactionInfoAndSharesAndCommandsAreTakenFromOrderDetails()
            Dim orderDetailsPM = New MockOrderDetailsViewModel()
            Dim composite = New OrderCompositeViewModel(orderDetailsPM)
            orderDetailsPM.privateShares = 100

            Assert.AreEqual(orderDetailsPM.Shares, composite.Shares)
            Assert.AreSame(orderDetailsPM.SubmitCommand, composite.SubmitCommand)
            Assert.AreSame(orderDetailsPM.CancelCommand, composite.CancelCommand)
            Assert.AreSame(orderDetailsPM.TransactionInfo, composite.TransactionInfo)
        End Sub

#If Not SILVERLIGHT Then
        ' In the Silverlight version of the RI, header binding is done purely in the XAML separate textblocks (no MultiBinding available).
        <TestMethod()> _
        Public Sub ShouldSetHeaderInfo()
            Dim composite = New OrderCompositeViewModel(New MockOrderDetailsViewModel())

            composite.TransactionInfo = New TransactionInfo("FXX01", TransactionType.Sell)

            Assert.IsNotNull(composite.HeaderInfo)
            Assert.IsTrue(composite.HeaderInfo.Contains("FXX01"))
            Assert.IsTrue(composite.HeaderInfo.Contains("Sell"))
            Assert.AreEqual("Sell FXX01", composite.HeaderInfo)
        End Sub

        <TestMethod()> _
        Public Sub ShouldUpdateHeaderInfoWhenUpdatingTransactionInfo()
            Dim orderDetailsPM = New MockOrderDetailsViewModel()
            Dim composite = New OrderCompositeViewModel(orderDetailsPM)

            composite.TransactionInfo = New TransactionInfo("FXX01", TransactionType.Sell)

            orderDetailsPM.TransactionInfo.TickerSymbol = "NEW_SYMBOL"
            Assert.AreEqual("Sell NEW_SYMBOL", composite.HeaderInfo)

            orderDetailsPM.TransactionInfo.TransactionType = TransactionType.Buy
            Assert.AreEqual("Buy NEW_SYMBOL", composite.HeaderInfo)
        End Sub
#End If
    End Class
End Namespace
