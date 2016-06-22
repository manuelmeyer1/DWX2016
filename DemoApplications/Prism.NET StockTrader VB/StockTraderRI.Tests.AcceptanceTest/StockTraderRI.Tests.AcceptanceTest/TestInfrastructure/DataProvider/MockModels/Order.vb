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
Imports System.Xml.Serialization

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
    <Serializable, XmlRoot("Order")> _
    Public Class Order
        Private _symbol As String
        Private limitprice As Decimal
        Private _orderType As String
        Private shares As Integer
        Private _timeInForce As String
        Private _transactionType As String

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Instantiate Order for symbol
        ''' </summary>
        ''' <param name="symbol">Symbol for which the model is created</param>
        Public Sub New(ByVal symbol As String)
            Me._symbol = symbol
        End Sub

        ''' <summary>
        ''' Instantiate Order with given parameter values
        ''' </summary>
        ''' <param name="symbol">Symbol for which the model is created</param>
        ''' <param name="limitprice">Limit/Stop price for buying / selling the stock</param>
        ''' <param name="orderType">type of the order</param>
        ''' <param name="shares">number of shares to be bought / sold</param>
        ''' <param name="timeInForce">time in force</param>
        Public Sub New(ByVal symbol As String, ByVal limitPrice As Decimal, ByVal orderType As String, ByVal shares As Integer, ByVal timeInForce As String, ByVal transactionType As String)
            Me._symbol = symbol
            Me.limitprice = limitPrice
            Me._orderType = orderType
            Me.shares = shares
            Me._timeInForce = timeInForce
            Me._transactionType = transactionType
        End Sub

        <XmlAttribute("TickerSymbol")> _
        Public Property Symbol() As String
            Get
                Return Me._symbol
            End Get
            Set(ByVal value As String)
                Me._symbol = value
            End Set
        End Property

        <XmlAttribute("StopLimitPrice")> _
        Public Property LimitStopPrice() As Decimal
            Get
                Return Me.limitprice
            End Get
            Set(ByVal value As Decimal)
                Me.limitprice = value
            End Set
        End Property

        <XmlAttribute("OrderType")> _
        Public Property OrderType() As String
            Get
                Return Me._orderType
            End Get
            Set(ByVal value As String)
                Me._orderType = value
            End Set
        End Property

        <XmlAttribute("Shares")> _
        Public Property NumberOfShares() As Integer
            Get
                Return Me.shares
            End Get
            Set(ByVal value As Integer)
                Me.shares = value
            End Set
        End Property

        <XmlAttribute("TimeInForce")> _
        Public Property TimeInForce() As String
            Get
                Return Me._timeInForce
            End Get
            Set(ByVal value As String)
                Me._timeInForce = value
            End Set
        End Property

        Public Property FormattedTimeInForce() As String
            Get
                If Me._timeInForce = TestDataInfrastructure.GetTestInputData("TimeInForceEndOfDay") Then
                    Return TestDataInfrastructure.GetTestInputData("FormattedTimeInForceEndOfDay")
                Else
                    Return String.Empty
                End If
            End Get
            Set(ByVal value As String)
                Me._timeInForce = value
            End Set
        End Property

        <XmlAttribute("TransactionType")> _
        Public Property TransactionType() As String
            Get
                Return Me._transactionType
            End Get
            Set(ByVal value As String)
                Me._transactionType = value
            End Set
        End Property

        Public Overrides Overloads Function Equals(ByVal obj As Object) As Boolean
            Dim o As Order = TryCast(obj, Order)

            Return (Me._symbol.ToUpperInvariant().Equals(o.Symbol.ToUpperInvariant()) AndAlso Me.limitprice.Equals(o.LimitStopPrice) AndAlso Me._orderType.ToUpperInvariant().Equals(o.OrderType.ToUpperInvariant()) AndAlso Me.shares.Equals(o.NumberOfShares) AndAlso Me._timeInForce.ToUpperInvariant().Equals(o.TimeInForce.ToUpperInvariant()) AndAlso Me._transactionType.ToUpperInvariant().Equals(o.TransactionType.ToUpperInvariant()))
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function
    End Class
End Namespace
