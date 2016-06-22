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

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Public Class MockStockTraderRICommandProxy
        Inherits StockTraderRICommandProxy
        Private _submitAllOrdersCommand As New CompositeCommand()
        Private _cancelAllOrdersCommand As New CompositeCommand()
        Private _submitOrderCommand As New CompositeCommand(True)
        Private _cancelOrderCommand As New CompositeCommand(True)

        Public Overrides ReadOnly Property SubmitOrderCommand() As CompositeCommand
            Get
                Return Me._submitOrderCommand
            End Get
        End Property

        Public Overrides ReadOnly Property SubmitAllOrdersCommand() As CompositeCommand
            Get
                Return Me._submitAllOrdersCommand
            End Get
        End Property

        Public Overrides ReadOnly Property CancelOrderCommand() As CompositeCommand
            Get
                Return Me._cancelOrderCommand
            End Get
        End Property

        Public Overrides ReadOnly Property CancelAllOrdersCommand() As CompositeCommand
            Get
                Return Me._cancelAllOrdersCommand
            End Get
        End Property
    End Class
End Namespace
