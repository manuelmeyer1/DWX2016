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
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Position.Models
    Public Class TransactionInfo
        Inherits NotificationObject
        Private _tickerSymbol As String
        Private _transactionType As TransactionType

        Public Sub New()
        End Sub

        Public Sub New(ByVal tickerSymbol As String, ByVal transactionType As TransactionType)
            Me._tickerSymbol = tickerSymbol
            Me._transactionType = transactionType
        End Sub

        Public Property TickerSymbol() As String
            Get
                Return Me._tickerSymbol
            End Get

            Set(ByVal value As String)
                If Me._tickerSymbol <> value Then
                    Me._tickerSymbol = value
                    Me.RaisePropertyChanged(Function() Me.TickerSymbol)
                End If
            End Set
        End Property

        Public Property TransactionType() As TransactionType
            Get
                Return Me._transactionType
            End Get

            Set(ByVal value As TransactionType)
                If Me._transactionType <> value Then
                    Me._transactionType = value
                    Me.RaisePropertyChanged(Function() Me.TransactionType)
                End If
            End Set
        End Property
    End Class
End Namespace