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
Imports System.Collections.ObjectModel

Namespace StockTraderRI.Infrastructure.Models
    Public Class MarketHistoryCollection
        Inherits ObservableCollection(Of MarketHistoryItem)

        Public Sub New()
        End Sub

        Public Sub New(ByVal list As IEnumerable(Of MarketHistoryItem))
            For Each marketHistoryItem As MarketHistoryItem In list
                Me.Add(marketHistoryItem)
            Next marketHistoryItem
        End Sub
    End Class

    Public Class MarketHistoryItem
        Private _DateTimeMarker As Date

        Public Property DateTimeMarker() As Date
            Get
                Return _DateTimeMarker
            End Get
            Set(ByVal value As Date)
                _DateTimeMarker = value
            End Set
        End Property

        Private _Value As Decimal

        Public Property Value() As Decimal
            Get
                Return _Value
            End Get
            Set(ByVal value As Decimal)
                _Value = value
            End Set
        End Property
    End Class
End Namespace