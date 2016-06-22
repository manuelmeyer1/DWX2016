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
Imports System.Globalization
Imports StockTraderRI.Modules.Position.Interfaces
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Modules.Position.Models
Imports My.Resources
Imports Microsoft.Practices.ServiceLocation
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Modules.Position.Orders
Imports StockTraderRI.Infrastructure.Interfaces
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Regions

Namespace StockTraderRI.Modules.Position.Controllers
    <Export(GetType(IOrdersController)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class OrdersController
        Implements IOrdersController

        Private _regionManager As IRegionManager
        Private ReadOnly commandProxy As StockTraderRICommandProxy
        Private _accountPositionService As IAccountPositionService

        <ImportingConstructor()> _
        Public Sub New(ByVal regionManager As IRegionManager, ByVal commandProxy As StockTraderRICommandProxy, _
                        ByVal accountPositionService As IAccountPositionService)
            _regionManager = regionManager
            _accountPositionService = accountPositionService
            Me.commandProxy = commandProxy
            _BuyCommand = New DelegateCommand(Of String)(AddressOf OnBuyExecuted)
            _SellCommand = New DelegateCommand(Of String)(AddressOf OnSellExecuted)
            SubmitAllVoteOnlyCommand = New DelegateCommand(Sub()

                                                           End Sub, AddressOf SubmitAllCanExecute)
            OrderModels = New List(Of IOrderCompositeViewModel)()
            commandProxy.SubmitAllOrdersCommand.RegisterCommand(SubmitAllVoteOnlyCommand)
        End Sub

        Private Sub OnSellExecuted(ByVal parameter As String)
            StartOrder(parameter, TransactionType.Sell)
        End Sub

        Private Sub OnBuyExecuted(ByVal parameter As String)
            StartOrder(parameter, TransactionType.Buy)
        End Sub

        Protected Overridable Function SubmitAllCanExecute() As Boolean
            Dim sellOrderShares As New Dictionary(Of String, Long)()

            If OrderModels.Count = 0 Then
                Return False
            End If

            For Each order In OrderModels
                If order.TransactionInfo.TransactionType = TransactionType.Sell Then
                    Dim tickerSymbol As String = order.TransactionInfo.TickerSymbol.ToUpper(CultureInfo.CurrentCulture)
                    If Not sellOrderShares.ContainsKey(tickerSymbol) Then
                        sellOrderShares.Add(tickerSymbol, 0)
                    End If

                    'populate dictionary with total shares bought or sold by tickersymbol
                    sellOrderShares(tickerSymbol) += order.Shares
                End If
            Next order

            Dim positions As IList(Of AccountPosition) = _accountPositionService.GetAccountPositions()

            For Each key As String In sellOrderShares.Keys
                Dim aKey As String = key
                Dim _
                    position As AccountPosition = _
                        positions.FirstOrDefault( _
                                                  Function(x) _
                                                     String.Compare(x.TickerSymbol, aKey, _
                                                                     StringComparison.CurrentCultureIgnoreCase) = 0)
                If position Is Nothing OrElse position.Shares < sellOrderShares(key) Then
                    'trying to sell more shares than we own
                    Return False
                End If
            Next key

            Return True
        End Function

        Protected Overridable Sub StartOrder(ByVal tickerSymbol As String, ByVal transactionType As TransactionType)
            If String.IsNullOrEmpty(tickerSymbol) Then
                Throw _
                    New ArgumentException( _
                                           String.Format(CultureInfo.CurrentCulture, StringCannotBeNullOrEmpty, _
                                                          "tickerSymbol"))
            End If
            Me.ShowOrdersView()

            Dim ordersRegion As IRegion = _regionManager.Regions(RegionNames.OrdersRegion)

            Dim orderCompositeViewModel = ServiceLocator.Current.GetInstance(Of IOrderCompositeViewModel)()

            orderCompositeViewModel.TransactionInfo = New TransactionInfo(tickerSymbol, transactionType)
            AddHandler orderCompositeViewModel.CloseViewRequested, Sub()
                                                                       OrderModels.Remove(orderCompositeViewModel)
                                                                       commandProxy.SubmitAllOrdersCommand.UnregisterCommand(orderCompositeViewModel.SubmitCommand)
                                                                       commandProxy.CancelAllOrdersCommand.UnregisterCommand(orderCompositeViewModel.CancelCommand)
                                                                       commandProxy.SubmitOrderCommand.UnregisterCommand(orderCompositeViewModel.SubmitCommand)
                                                                       commandProxy.CancelOrderCommand.UnregisterCommand(orderCompositeViewModel.CancelCommand)
                                                                       ordersRegion.Remove(orderCompositeViewModel)
                                                                       If ordersRegion.Views.Count() = 0 Then
                                                                           Me.RemoveOrdersView()
                                                                       End If
                                                                   End Sub

            ordersRegion.Add(orderCompositeViewModel)
            OrderModels.Add(orderCompositeViewModel)

            commandProxy.SubmitAllOrdersCommand.RegisterCommand(orderCompositeViewModel.SubmitCommand)
            commandProxy.CancelAllOrdersCommand.RegisterCommand(orderCompositeViewModel.CancelCommand)
            commandProxy.SubmitOrderCommand.RegisterCommand(orderCompositeViewModel.SubmitCommand)
            commandProxy.CancelOrderCommand.RegisterCommand(orderCompositeViewModel.CancelCommand)

            ordersRegion.Activate(orderCompositeViewModel)
        End Sub

        Private Sub RemoveOrdersView()
            Dim region As IRegion = Me._regionManager.Regions(RegionNames.ActionRegion)

            Dim ordersView As Object = region.GetView("OrdersView")
            If ordersView IsNot Nothing Then
                region.Remove(ordersView)
            End If
        End Sub

        Private Sub ShowOrdersView()
            Dim region As IRegion = Me._regionManager.Regions(RegionNames.ActionRegion)

            Dim ordersView As Object = region.GetView("OrdersView")
            If ordersView Is Nothing Then
                ordersView = ServiceLocator.Current.GetInstance(Of IOrdersView)()
                region.Add(ordersView, "OrdersView")
            End If

            region.Activate(ordersView)
        End Sub

#Region "IOrdersController Members"

        Private _BuyCommand As DelegateCommand(Of String)

        Public ReadOnly Property BuyCommand() As DelegateCommand(Of String) Implements IOrdersController.BuyCommand
            Get
                Return _BuyCommand
            End Get
            'Private Set(ByVal value As DelegateCommand(Of String))
            '    privateBuyCommand = value
            'End Set
        End Property

        Private _SellCommand As DelegateCommand(Of String)

        Public ReadOnly Property SellCommand() As DelegateCommand(Of String) Implements IOrdersController.SellCommand
            Get
                Return _SellCommand
            End Get
            'Private Set(ByVal value As DelegateCommand(Of String))
            '    privateSellCommand = value
            'End Set
        End Property

        Private _SubmitAllVoteOnlyCommand As DelegateCommand

        Public Property SubmitAllVoteOnlyCommand() As DelegateCommand
            Get
                Return _SubmitAllVoteOnlyCommand
            End Get
            Private Set(ByVal value As DelegateCommand)
                _SubmitAllVoteOnlyCommand = value
            End Set
        End Property

        Private _OrderModels As List(Of IOrderCompositeViewModel)

        Private Property OrderModels() As List(Of IOrderCompositeViewModel)
            Get
                Return _OrderModels
            End Get
            Set(ByVal value As List(Of IOrderCompositeViewModel))
                _OrderModels = value
            End Set
        End Property

#End Region
    End Class
End Namespace
