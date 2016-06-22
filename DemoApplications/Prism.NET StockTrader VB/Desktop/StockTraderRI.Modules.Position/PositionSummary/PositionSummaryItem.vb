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
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Position.PositionSummary
    Public Class PositionSummaryItem
        Inherits NotificationObject

        Public Sub New(ByVal tickerSymbol As String, ByVal costBasis As Decimal, ByVal shares As Long, _
                        ByVal currentPrice As Decimal)
            Me.TickerSymbol = tickerSymbol
            Me.CostBasis = costBasis
            Me.Shares = shares
            Me.CurrentPrice = currentPrice
        End Sub

        Private _tickerSymbol As String

        Public Property TickerSymbol() As String
            Get
                Return _tickerSymbol
            End Get
            Set(ByVal value As String)
                If Not value.Equals(_tickerSymbol) Then
                    _tickerSymbol = value
                    Me.RaisePropertyChanged(Function() Me.TickerSymbol)
                End If
            End Set
        End Property

        Private _costBasis As Decimal

        Public Property CostBasis() As Decimal
            Get
                Return _costBasis
            End Get
            Set(ByVal value As Decimal)
                If Not value.Equals(_costBasis) Then
                    _costBasis = value
                    Me.RaisePropertyChanged(Function() Me.CostBasis)
                    Me.RaisePropertyChanged(Function() Me.GainLossPercent)
                End If
            End Set
        End Property

        Private _shares As Long

        Public Property Shares() As Long
            Get
                Return _shares
            End Get
            Set(ByVal value As Long)
                If Not value.Equals(_shares) Then
                    _shares = value
                    Me.RaisePropertyChanged(Function() Me.Shares)
                    Me.RaisePropertyChanged(Function() Me.MarketValue)
                    Me.RaisePropertyChanged(Function() Me.GainLossPercent)
                End If
            End Set
        End Property

        Private _currentPrice As Decimal

        Public Property CurrentPrice() As Decimal
            Get
                Return _currentPrice
            End Get
            Set(ByVal value As Decimal)
                If Not value.Equals(_currentPrice) Then
                    _currentPrice = value
                    Me.RaisePropertyChanged(Function() Me.CurrentPrice)
                    Me.RaisePropertyChanged(Function() Me.MarketValue)
                    Me.RaisePropertyChanged(Function() Me.GainLossPercent)
                End If
            End Set
        End Property

        Public ReadOnly Property MarketValue() As Decimal
            Get
                Return (_shares * _currentPrice)
            End Get
        End Property

        Public ReadOnly Property GainLossPercent() As Decimal
            Get
                Return ((CurrentPrice * Shares - CostBasis) * 100 / CostBasis)
            End Get
        End Property
    End Class
End Namespace