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

Namespace StockTraderRI.Modules.Watch
    Public Class WatchItem
        Implements INotifyPropertyChanged
        Private _currentPrice? As Decimal

        Public Sub New(ByVal tickerSymbol As String, ByVal currentPrice? As Decimal)
            Me.TickerSymbol = tickerSymbol
            Me.CurrentPrice = currentPrice
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Private _TickerSymbol As String

        Public Property TickerSymbol() As String
            Get
                Return _TickerSymbol
            End Get
            Set(ByVal value As String)
                _TickerSymbol = value
            End Set
        End Property

        Public Property CurrentPrice() As Decimal?
            Get
                Return _currentPrice
            End Get
            Set(ByVal value? As Decimal)
                If _currentPrice = value Then
                Else
                    _currentPrice = value
                    OnPropertyChanged("CurrentPrice")
                End If
            End Set
        End Property

        Private Sub OnPropertyChanged(ByVal propertyName As String)
            Dim Handler As PropertyChangedEventHandler = PropertyChangedEvent
            If Handler IsNot Nothing Then
                Handler(Me, New PropertyChangedEventArgs(propertyName))
            End If
        End Sub
    End Class
End Namespace
