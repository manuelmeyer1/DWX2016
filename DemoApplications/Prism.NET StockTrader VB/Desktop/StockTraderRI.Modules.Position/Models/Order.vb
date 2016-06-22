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
Imports StockTraderRI.Modules.Position.Orders

Namespace StockTraderRI.Modules.Position.Models
    Public Class Order
        Private _Shares As Integer

        Public Property Shares() As Integer
            Get
                Return _Shares
            End Get
            Set(ByVal value As Integer)
                _Shares = value
            End Set
        End Property

        Private _TimeInForce As TimeInForce

        Public Property TimeInForce() As TimeInForce
            Get
                Return _TimeInForce
            End Get
            Set(ByVal value As TimeInForce)
                _TimeInForce = value
            End Set
        End Property

        Private _TickerSymbol As String

        Public Property TickerSymbol() As String
            Get
                Return _TickerSymbol
            End Get
            Set(ByVal value As String)
                _TickerSymbol = value
            End Set
        End Property

        Private _TransactionType As TransactionType

        Public Property TransactionType() As TransactionType
            Get
                Return _TransactionType
            End Get
            Set(ByVal value As TransactionType)
                _TransactionType = value
            End Set
        End Property

        Private _StopLimitPrice As Decimal

        Public Property StopLimitPrice() As Decimal
            Get
                Return _StopLimitPrice
            End Get
            Set(ByVal value As Decimal)
                _StopLimitPrice = value
            End Set
        End Property

        Private _OrderType As OrderType

        Public Property OrderType() As OrderType
            Get
                Return _OrderType
            End Get
            Set(ByVal value As OrderType)
                _OrderType = value
            End Set
        End Property
    End Class
End Namespace
