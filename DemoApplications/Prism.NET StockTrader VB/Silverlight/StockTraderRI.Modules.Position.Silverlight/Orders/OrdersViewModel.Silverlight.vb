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
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Commands

Namespace StockTraderRI.Modules.Position.Orders
    Partial Public Class OrdersViewModel
        Public ReadOnly Property SubmitAllOrdersCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.SubmitAllOrdersCommand
            End Get
        End Property

        Public ReadOnly Property CancelAllOrdersCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.CancelAllOrdersCommand
            End Get
        End Property
    End Class
End Namespace
