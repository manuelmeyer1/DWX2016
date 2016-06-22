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
    <Serializable, XmlRoot("MarketItem")> _
    Public Class Market
        Private symbol As String
        Private marketValue As Decimal
        Private _volume As Long

        Public Sub New()
        End Sub

        Public Sub New(ByVal symbol As String, ByVal value As Decimal)
            Me.symbol = symbol
            Me.marketValue = value
        End Sub

        Public Sub New(ByVal symbol As String, ByVal value As Decimal, ByVal volume As Long)
            Me.symbol = symbol
            Me.marketValue = value
            Me._volume = volume
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

        <XmlAttribute("LastPrice")> _
        Public Property LastPrice() As Decimal
            Get
                Return Me.marketValue
            End Get
            Set(ByVal value As Decimal)
                Me.marketValue = value
            End Set
        End Property

        <XmlAttribute("Volume")> _
        Public Property Volume() As Long
            Get
                Return Me._volume
            End Get
            Set(ByVal value As Long)
                Me._volume = value
            End Set
        End Property
    End Class
End Namespace
