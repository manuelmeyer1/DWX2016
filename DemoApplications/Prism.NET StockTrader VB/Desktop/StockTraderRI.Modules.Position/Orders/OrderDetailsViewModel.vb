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
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Infrastructure.Models
Imports My.Resources
Imports StockTraderRI.Infrastructure
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Interfaces
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Position.Orders
    <Export(GetType(IOrderDetailsViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class OrderDetailsViewModel
        Inherits NotificationObject
        Implements IOrderDetailsViewModel
        Private ReadOnly accountPositionService As IAccountPositionService
        Private ReadOnly ordersService As IOrdersService
        Private _transactionInfo As TransactionInfo
        Private _shares? As Integer
        Private _orderType As OrderType = OrderType.Market
        Private _stopLimitPrice? As Decimal
        Private _timeInForce As TimeInForce

        Private ReadOnly errors As New List(Of String)()

        <ImportingConstructor()> _
        Public Sub New(ByVal accountPositionService As IAccountPositionService, ByVal ordersService As IOrdersService)
            Me.accountPositionService = accountPositionService
            Me.ordersService = ordersService

            Me._transactionInfo = New TransactionInfo()

            'use localizable enum descriptions
            Me.AvailableOrderTypes = _
                New List(Of ValueDescription(Of OrderType))( _
                                                             New ValueDescription(Of OrderType)() _
                                                                {New ValueDescription(Of OrderType)(OrderType.Limit, _
                                                                                                     OrderType_Limit), _
                                                                 New ValueDescription(Of OrderType)(OrderType.Market, _
                                                                                                     OrderType_Market), _
                                                                 New ValueDescription(Of OrderType)(OrderType.Stop, _
                                                                                                     OrderType_Stop)})

            Me.AvailableTimesInForce = _
                New List(Of ValueDescription(Of TimeInForce))( _
                                                               New ValueDescription(Of TimeInForce)() _
                                                                  {New ValueDescription(Of TimeInForce)( _
                                                                                                         TimeInForce. _
                                                                                                            EndOfDay, _
                                                                                                         TimeInForce_EndOfDay), _
                                                                   New ValueDescription(Of TimeInForce)( _
                                                                                                         TimeInForce. _
                                                                                                            ThirtyDays, _
                                                                                                         TimeInForce_ThirtyDays)})

            Me.SubmitCommand = New DelegateCommand(Of Object)(AddressOf Me.Submit, AddressOf Me.CanSubmit)
            Me.CancelCommand = New DelegateCommand(Of Object)(AddressOf Me.Cancel)

            Me.SetInitialValidState()
        End Sub

        Public Event CloseViewRequested As EventHandler Implements IOrderDetailsViewModel.CloseViewRequested

        Private _AvailableOrderTypes As IList(Of ValueDescription(Of OrderType))

        Public Property AvailableOrderTypes() As IList(Of ValueDescription(Of OrderType))
            Get
                Return _AvailableOrderTypes
            End Get
            Private Set(ByVal value As IList(Of ValueDescription(Of OrderType)))
                _AvailableOrderTypes = value
            End Set
        End Property

        Private _AvailableTimesInForce As IList(Of ValueDescription(Of TimeInForce))

        Public Property AvailableTimesInForce() As IList(Of ValueDescription(Of TimeInForce))
            Get
                Return _AvailableTimesInForce
            End Get
            Private Set(ByVal value As IList(Of ValueDescription(Of TimeInForce)))
                _AvailableTimesInForce = value
            End Set
        End Property

        Public Property TransactionInfo() As TransactionInfo Implements IOrderDetailsViewModel.TransactionInfo
            Get
                Return Me._transactionInfo
            End Get
            Set(ByVal value As TransactionInfo)
                Me._transactionInfo = value
                Me.RaisePropertyChanged(Function() Me.TransactionType)
                Me.RaisePropertyChanged(Function() Me.TickerSymbol)
            End Set
        End Property

        Public Property TransactionType() As TransactionType Implements IOrderDetailsViewModel.TransactionType
            Get
                Return Me._transactionInfo.TransactionType
            End Get
            Set(ByVal value As TransactionType)
                Me.ValidateHasEnoughSharesToSell(Me.Shares, value, False)

                If Me._transactionInfo.TransactionType <> value Then
                    Me._transactionInfo.TransactionType = value
                    Me.RaisePropertyChanged(Function() Me.TransactionType)
                End If
            End Set
        End Property

        Public Property TickerSymbol() As String Implements IOrderDetailsViewModel.TickerSymbol
            Get
                Return Me._transactionInfo.TickerSymbol
            End Get
            Set(ByVal value As String)
                If Me._transactionInfo.TickerSymbol <> value Then
                    Me._transactionInfo.TickerSymbol = value
                    Me.RaisePropertyChanged(Function() Me.TickerSymbol)
                End If
            End Set
        End Property

        Public Property Shares() As Integer? Implements IOrderDetailsViewModel.Shares
            Get
                Return Me._shares
            End Get
            Set(ByVal value? As Integer)
                Me.ValidateShares(value, True)
                Me.ValidateHasEnoughSharesToSell(value, Me.TransactionType, True)

                If Me._shares = value Then
                Else
                    Me._shares = value
                    Me.RaisePropertyChanged(Function() Me.Shares)
                End If
            End Set
        End Property

        Public Property OrderType() As OrderType
            Get
                Return Me._orderType
            End Get
            Set(ByVal value As OrderType)
                If Not value.Equals(Me._orderType) Then
                    Me._orderType = value
                    Me.RaisePropertyChanged(Function() Me.OrderType)
                End If
            End Set
        End Property

        Public Property StopLimitPrice() As Decimal? Implements IOrderDetailsViewModel.StopLimitPrice
            Get
                Return Me._stopLimitPrice
            End Get
            Set(ByVal value? As Decimal)
                Me.ValidateStopLimitPrice(value, True)

                If value = Me._stopLimitPrice Then
                Else
                    Me._stopLimitPrice = value
                    Me.RaisePropertyChanged(Function() Me.StopLimitPrice)
                End If
            End Set
        End Property

        Public Property TimeInForce() As TimeInForce
            Get
                Return Me._timeInForce
            End Get
            Set(ByVal value As TimeInForce)
                If value <> Me._timeInForce Then
                    Me._timeInForce = value
                    Me.RaisePropertyChanged(Function() Me.TimeInForce)
                End If

                Me._timeInForce = value
            End Set
        End Property

        Private _SubmitCommand As DelegateCommand(Of Object)

        Public Property SubmitCommand() As DelegateCommand(Of Object) Implements IOrderDetailsViewModel.SubmitCommand
            Get
                Return _SubmitCommand
            End Get
            Private Set(ByVal value As DelegateCommand(Of Object))
                _SubmitCommand = value
            End Set
        End Property

        Private _CancelCommand As DelegateCommand(Of Object)

        Public Property CancelCommand() As DelegateCommand(Of Object) Implements IOrderDetailsViewModel.CancelCommand
            Get
                Return _CancelCommand
            End Get
            Private Set(ByVal value As DelegateCommand(Of Object))
                _CancelCommand = value
            End Set
        End Property

        Private Sub SetInitialValidState()
            Me.ValidateShares(Me.Shares, False)
            Me.ValidateStopLimitPrice(Me.StopLimitPrice, False)
        End Sub

        Private Sub ValidateShares(ByVal newSharesValue? As Integer, ByVal throwException As Boolean)
            If (Not newSharesValue.HasValue) OrElse newSharesValue.Value <= 0 Then
                Me.AddError("InvalidSharesRange")
                If throwException Then
                    Throw New InputValidationException(InvalidSharesRange)
                End If
            Else
                Me.RemoveError("InvalidSharesRange")
            End If
        End Sub

        Private Sub ValidateStopLimitPrice(ByVal price? As Decimal, ByVal throwException As Boolean)
            If (Not price.HasValue) OrElse price.Value <= 0 Then
                Me.AddError("InvalidStopLimitPrice")
                If throwException Then
                    Throw New InputValidationException(InvalidStopLimitPrice)
                End If
            Else
                Me.RemoveError("InvalidStopLimitPrice")
            End If
        End Sub

        Private Sub ValidateHasEnoughSharesToSell(ByVal sharesToSell? As Integer, _
                                                   ByVal _transactionType As TransactionType, _
                                                   ByVal throwException As Boolean)
            If _
                _transactionType = TransactionType.Sell AndAlso _
                (Not Me.HoldsEnoughShares(Me.TickerSymbol, sharesToSell)) Then
                Me.AddError("NotEnoughSharesToSell")
                If throwException Then
                    Throw _
                        New InputValidationException( _
                                                      String.Format(CultureInfo.InvariantCulture, NotEnoughSharesToSell, _
                                                                     sharesToSell))
                End If
            Else
                Me.RemoveError("NotEnoughSharesToSell")
            End If
        End Sub

        Private Sub AddError(ByVal ruleName As String)
            If Not Me.errors.Contains(ruleName) Then
                Me.errors.Add(ruleName)
                Me.SubmitCommand.RaiseCanExecuteChanged()
            End If
        End Sub

        Private Sub RemoveError(ByVal ruleName As String)
            If Me.errors.Contains(ruleName) Then
                Me.errors.Remove(ruleName)
                If Me.errors.Count = 0 Then
                    Me.SubmitCommand.RaiseCanExecuteChanged()
                End If
            End If
        End Sub

        Private Function CanSubmit(ByVal parameter As Object) As Boolean
            Return Me.errors.Count = 0
        End Function

        Private Function HoldsEnoughShares(ByVal symbol As String, ByVal sharesToSell? As Integer) As Boolean
            If Not sharesToSell.HasValue Then
                Return False
            End If

            For Each accountPosition As AccountPosition In Me.accountPositionService.GetAccountPositions()
                If accountPosition.TickerSymbol.Equals(symbol, StringComparison.OrdinalIgnoreCase) Then
                    If accountPosition.Shares >= sharesToSell Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Next accountPosition
            Return False
        End Function

        Private Sub Submit(ByVal parameter As Object)
            If Not Me.CanSubmit(parameter) Then
                Throw New InvalidOperationException()
            End If

            Dim order = New Order()
            order.TransactionType = Me.TransactionType
            order.OrderType = Me.OrderType
            order.Shares = Me.Shares.Value
            order.StopLimitPrice = Me.StopLimitPrice.Value
            order.TickerSymbol = Me.TickerSymbol
            order.TimeInForce = Me.TimeInForce

            ordersService.Submit(order)

            RaiseEvent CloseViewRequested(Me, EventArgs.Empty)
        End Sub

        Private Sub Cancel(ByVal parameter As Object)
            RaiseEvent CloseViewRequested(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace
