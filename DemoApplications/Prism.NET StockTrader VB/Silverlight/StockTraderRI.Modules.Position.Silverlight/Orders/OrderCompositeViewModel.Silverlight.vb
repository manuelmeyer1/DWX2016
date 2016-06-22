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
Imports System.ComponentModel
Imports StockTraderRI.Modules.Position.Models

Namespace StockTraderRI.Modules.Position.Orders
    Partial Public Class OrderCompositeViewModel
        Implements INotifyPropertyChanged

        Private Sub SetTransactionInfo(ByVal transactionInfo As TransactionInfo)
            'This instance of TransactionInfo acts as a "shared model" between this view and the order details view.
            'The scenario says that these 2 views are decoupled, so they don't share the presentation model, they are only tied
            'with this TransactionInfo
            Me.orderDetailsViewModel.TransactionInfo = transactionInfo
            Me.HeaderInfo = transactionInfo.TickerSymbol
        End Sub

        Public Property HeaderInfo() As String
            Get
                Return CStr(GetValue(HeaderInfoProperty))
            End Get
            Set(ByVal value As String)
                SetValue(HeaderInfoProperty, value)
                InvokePropertyChanged(New PropertyChangedEventArgs("HeaderInfo"))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private Sub InvokePropertyChanged(ByVal e As PropertyChangedEventArgs)
            Dim Handler As PropertyChangedEventHandler = PropertyChangedEvent
            If Handler IsNot Nothing Then
                Handler(Me, e)
            End If
        End Sub
    End Class
End Namespace
