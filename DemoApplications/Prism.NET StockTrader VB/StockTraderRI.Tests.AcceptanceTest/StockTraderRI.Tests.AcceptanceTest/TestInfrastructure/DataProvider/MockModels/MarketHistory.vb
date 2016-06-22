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
Imports System.Text
Imports System.Xml.Serialization

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels
    <Serializable, XmlRoot("MarketHistoryItem")> _
    Public Class MarketHistoryItem
        Private symbol As String
        Private _date As Date
        Private item As Decimal

        Public Sub New()
        End Sub

        Public Sub New(ByVal symbol As String, ByVal [date] As Date, ByVal item As Decimal)
            Me.symbol = symbol
            Me._date = [date]
            Me.item = item
        End Sub

        <XmlAttribute("TickerSymbol")> _
        Public Property TickerSymbol() As String
            Get
                Return Me.symbol
            End Get
            Set(ByVal value As String)
                Me.symbol = value
            End Set
        End Property

        <XmlAttribute("Date")> _
        Public Property [Date]() As Date
            Get
                Return Me._date
            End Get
            Set(ByVal value As Date)
                Me._date = value
            End Set
        End Property

        <XmlElement("MarketHistoryItem")> _
        Public Property MarketItem() As Decimal
            Get
                Return Me.item
            End Get
            Set(ByVal value As Decimal)
                Me.item = value
            End Set
        End Property
    End Class
End Namespace
