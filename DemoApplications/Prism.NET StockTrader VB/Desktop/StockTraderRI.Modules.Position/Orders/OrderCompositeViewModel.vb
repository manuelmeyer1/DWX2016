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
Imports System.Windows
Imports System.Windows.Input
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Position.Orders
    <Export(GetType(IOrderCompositeViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class OrderCompositeViewModel
        Inherits DependencyObject
        Implements IOrderCompositeViewModel, IHeaderInfoProvider(Of String)

        Private ReadOnly orderDetailsViewModel As IOrderDetailsViewModel

        Public Shared ReadOnly _
            HeaderInfoProperty As DependencyProperty = _
                DependencyProperty.Register("HeaderInfo", GetType(String), GetType(OrderCompositeViewModel), Nothing)

        <ImportingConstructor()> _
        Public Sub New(ByVal orderDetailsViewModel As IOrderDetailsViewModel)
            Me.orderDetailsViewModel = orderDetailsViewModel
            AddHandler Me.orderDetailsViewModel.CloseViewRequested, AddressOf _orderPresenter_CloseViewRequested
        End Sub

        Private Sub _orderPresenter_CloseViewRequested(ByVal sender As Object, ByVal e As EventArgs)
            OnCloseViewRequested(sender, e)
        End Sub

        Partial Private Sub SetTransactionInfo(ByVal transactionInfo As TransactionInfo)
        End Sub

        Private Sub OnCloseViewRequested(ByVal sender As Object, ByVal e As EventArgs)
            RaiseEvent CloseViewRequested(sender, e)
        End Sub

        Public Event CloseViewRequested As EventHandler Implements IOrderCompositeViewModel.CloseViewRequested

        Public Property TransactionInfo() As TransactionInfo Implements IOrderCompositeViewModel.TransactionInfo
            Get
                Return Me.orderDetailsViewModel.TransactionInfo
            End Get
            Set(ByVal value As TransactionInfo)
                SetTransactionInfo(value)
            End Set
        End Property

        Public ReadOnly Property SubmitCommand() As ICommand Implements IOrderCompositeViewModel.SubmitCommand
            Get
                Return Me.orderDetailsViewModel.SubmitCommand
            End Get
        End Property

        Public ReadOnly Property CancelCommand() As ICommand Implements IOrderCompositeViewModel.CancelCommand
            Get
                Return Me.orderDetailsViewModel.CancelCommand
            End Get
        End Property

        Public ReadOnly Property Shares() As Integer Implements IOrderCompositeViewModel.Shares
            Get
                Return If(Me.orderDetailsViewModel.Shares, 0)
            End Get
        End Property

        Public ReadOnly Property OrderDetails() As Object
            Get
                Return Me.orderDetailsViewModel
            End Get
        End Property

        Public ReadOnly Property HeaderInfo1 As String Implements IHeaderInfoProvider(Of String).HeaderInfo
            Get
                Return Nothing
            End Get
        End Property
    End Class
End Namespace