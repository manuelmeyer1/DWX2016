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
Imports System.Windows.Input
Imports StockTraderRI.Modules.Position.Models

Namespace StockTraderRI.Modules.Position.Orders
    Public Interface IOrderCompositeViewModel
        Event CloseViewRequested As EventHandler

        ReadOnly Property SubmitCommand() As ICommand
        ReadOnly Property CancelCommand() As ICommand
        Property TransactionInfo() As TransactionInfo
        ReadOnly Property Shares() As Integer
    End Interface
End Namespace