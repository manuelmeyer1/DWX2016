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
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Infrastructure
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Orders

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Public Class MockOrderDetailsViewModel
        Implements IOrderDetailsViewModel
        Public DisposeCalled As Boolean

        Public Event CloseViewRequested As EventHandler Implements IOrderDetailsViewModel.CloseViewRequested

#Region "IDisposable Members"

        Public Sub Dispose()
            DisposeCalled = True
        End Sub

#End Region

        Friend Sub RaiseCloseViewRequested()
            RaiseEvent CloseViewRequested(Me, EventArgs.Empty)
        End Sub

        Private _TransactionInfo As TransactionInfo

        Public Property TransactionInfo() As TransactionInfo Implements IOrderDetailsViewModel.TransactionInfo
            Get
                Return _TransactionInfo
            End Get
            Set(ByVal value As TransactionInfo)
                _TransactionInfo = value
            End Set
        End Property

        Friend privateTransactionType As TransactionType

        Public Property TransactionType() As TransactionType Implements IOrderDetailsViewModel.TransactionType
            Get
                Return privateTransactionType
            End Get
            Set(ByVal value As TransactionType)

            End Set
        End Property

        Friend privateTickerSymbol As String

        Public Property TickerSymbol() As String Implements IOrderDetailsViewModel.TickerSymbol
            Get
                Return privateTickerSymbol
            End Get
            Set(ByVal value As String)

            End Set
        End Property

        Friend privateShares? As Integer

        Public Property Shares() As Integer? Implements IOrderDetailsViewModel.Shares
            Get
                Return privateShares
            End Get
            Set(ByVal value As Integer?)

            End Set
        End Property

        Friend privateStopLimitPrice? As Decimal

        Public Property StopLimitPrice() As Decimal? Implements IOrderDetailsViewModel.StopLimitPrice
            Get
                Return privateStopLimitPrice
            End Get
            Set(ByVal value As Decimal?)

            End Set
        End Property

        Friend privateSubmitCommand As DelegateCommand(Of Object)

        Public Property SubmitCommand() As DelegateCommand(Of Object) Implements IOrderDetailsViewModel.SubmitCommand
            Get
                Return privateSubmitCommand
            End Get
            Set(ByVal value As DelegateCommand(Of Object))

            End Set
        End Property

        Friend privateCancelCommand As DelegateCommand(Of Object)

        Public Property CancelCommand() As DelegateCommand(Of Object) Implements IOrderDetailsViewModel.CancelCommand
            Get
                Return privateCancelCommand
            End Get
            Set(ByVal value As DelegateCommand(Of Object))

            End Set
        End Property

#Region "IOrderDetailsViewModel Members"

#End Region
    End Class
End Namespace
