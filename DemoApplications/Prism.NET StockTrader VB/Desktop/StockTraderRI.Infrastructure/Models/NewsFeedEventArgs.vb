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


Namespace StockTraderRI.Infrastructure.Models
    Public Class NewsFeedEventArgs
        Inherits EventArgs

        Public Sub New(ByVal tickerSymbol As String, ByVal newsHeadline As String)
            Me.TickerSymbol = tickerSymbol
            Me.NewsHeadline = newsHeadline
        End Sub

        Private _TickerSymbol As String

        Public Property TickerSymbol() As String
            Get
                Return _TickerSymbol
            End Get
            Set(ByVal value As String)
                _TickerSymbol = value
            End Set
        End Property

        Private _NewsHeadline As String

        Public Property NewsHeadline() As String
            Get
                Return _NewsHeadline
            End Get
            Set(ByVal value As String)
                _NewsHeadline = value
            End Set
        End Property
    End Class
End Namespace
