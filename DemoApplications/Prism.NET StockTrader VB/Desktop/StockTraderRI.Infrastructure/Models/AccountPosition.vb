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
    Public Class AccountPosition
        Public Event Updated As EventHandler(Of AccountPositionEventArgs)

        Public Sub New()
        End Sub

        Public Sub New(ByVal tickerSymbol As String, ByVal costBasis As Decimal, ByVal shares As Long)
            Me.TickerSymbol = tickerSymbol
            Me.CostBasis = costBasis
            Me.Shares = shares
        End Sub

        Private _tickerSymbol As String

        Public Property TickerSymbol() As String
            Get
                Return _tickerSymbol
            End Get
            Set(ByVal value As String)
                If Not value.Equals(_tickerSymbol) Then
                    _tickerSymbol = value
                    RaiseEvent Updated(Me, New AccountPositionEventArgs())
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
                    RaiseEvent Updated(Me, New AccountPositionEventArgs())
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
                    RaiseEvent Updated(Me, New AccountPositionEventArgs())
                End If
            End Set
        End Property
    End Class
End Namespace
