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
    <Serializable, XmlRoot("NewsItem")> _
    Public Class News
        Private symbol As String
        Private _iconUriPath As String
        Private _title As String
        Private _body As String
        Private _publishedDate As Date

        Public Sub New()
        End Sub

        Public Sub New(ByVal symbol As String)
            Me.New(symbol, Nothing, String.Empty, String.Empty)
        End Sub

        Public Sub New(ByVal symbol As String, ByVal title As String)
            Me.New(symbol, Nothing, title, String.Empty)
        End Sub

        Public Sub New(ByVal symbol As String, ByVal title As String, ByVal body As String)
            Me.New(symbol, Nothing, title, body)
        End Sub

        Public Sub New(ByVal symbol As String, ByVal iconUriPath As String, ByVal title As String, ByVal body As String)
            Me.New(symbol, iconUriPath, Date.MinValue, title, body)
        End Sub

        Public Sub New(ByVal symbol As String, ByVal iconUriPath As String, ByVal publishedDate As Date, ByVal title As String, ByVal body As String)
            Me.symbol = symbol
            Me._iconUriPath = iconUriPath
            Me._publishedDate = publishedDate
            Me._title = title
            Me._body = body
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

        <XmlAttribute("IconUri")> _
        Public Property IconUriPath() As String
            Get
                Return Me._iconUriPath
            End Get
            Set(ByVal value As String)
                Me._iconUriPath = value
            End Set
        End Property

        <XmlElement("Title")> _
        Public Property Title() As String
            Get
                Return Me._title
            End Get
            Set(ByVal value As String)
                Me._title = value
            End Set
        End Property

        <XmlElement("Body")> _
        Public Property Body() As String
            Get
                Return Me._body
            End Get
            Set(ByVal value As String)
                Me._body = value
            End Set
        End Property

        Public Property PublishedDate() As Date
            Get
                Return Me._publishedDate
            End Get
            Set(ByVal value As Date)
                Me._publishedDate = value
            End Set
        End Property
    End Class
End Namespace
