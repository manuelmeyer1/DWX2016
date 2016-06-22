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
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Position.PositionSummary
    Public Interface IPositionSummaryViewModel
        Inherits IHeaderInfoProvider(Of String)
        'ReadOnly Property Position() As IObservablePosition
        Property Position() As IObservablePosition

        'ReadOnly Property BuyCommand() As ICommand
        Property BuyCommand() As ICommand

        'ReadOnly Property SellCommand() As ICommand
        Property SellCommand() As ICommand
    End Interface
End Namespace