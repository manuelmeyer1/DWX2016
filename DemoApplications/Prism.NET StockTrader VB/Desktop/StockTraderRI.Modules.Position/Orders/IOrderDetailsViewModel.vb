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
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Infrastructure
Imports StockTraderRI.Modules.Position.Models

Namespace StockTraderRI.Modules.Position.Orders
    Public Interface IOrderDetailsViewModel
        Event CloseViewRequested As EventHandler
        ' TODO consider interaction request

        Property TransactionInfo() As TransactionInfo

        ' ReadOnly Property TransactionType() As TransactionType
        Property TransactionType() As TransactionType

        'ReadOnly Property TickerSymbol() As String
        Property TickerSymbol() As String

        'ReadOnly Property Shares() As Integer?
        Property Shares() As Integer?

        'ReadOnly Property StopLimitPrice() As Decimal?
        Property StopLimitPrice() As Decimal?

        ' ReadOnly Property SubmitCommand() As DelegateCommand(Of Object)
        Property SubmitCommand() As DelegateCommand(Of Object)

        ' ReadOnly Property CancelCommand() As DelegateCommand(Of Object)
        Property CancelCommand() As DelegateCommand(Of Object)
    End Interface
End Namespace
