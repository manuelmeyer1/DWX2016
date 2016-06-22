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
Imports System.ComponentModel.Composition
Imports Microsoft.Practices.Prism.Commands

Namespace StockTraderRI.Infrastructure
    Public NotInheritable Class StockTraderRICommands
        Private Shared _submitOrderCommand As New CompositeCommand(True)
        Private Shared _cancelOrderCommand As New CompositeCommand(True)
        Private Shared _submitAllOrdersCommand As New CompositeCommand()
        Private Shared _cancelAllOrdersCommand As New CompositeCommand()

        Private Sub New()
        End Sub

        Public Shared Property SubmitOrderCommand() As CompositeCommand
            Get
                Return _submitOrderCommand
            End Get
            Set(ByVal value As CompositeCommand)
                _submitOrderCommand = value
            End Set
        End Property

        Public Shared Property CancelOrderCommand() As CompositeCommand
            Get
                Return _cancelOrderCommand
            End Get
            Set(ByVal value As CompositeCommand)
                _cancelOrderCommand = value
            End Set
        End Property

        Public Shared Property SubmitAllOrdersCommand() As CompositeCommand
            Get
                Return _submitAllOrdersCommand
            End Get
            Set(ByVal value As CompositeCommand)
                _submitAllOrdersCommand = value
            End Set
        End Property

        Public Shared Property CancelAllOrdersCommand() As CompositeCommand
            Get
                Return _cancelAllOrdersCommand
            End Get
            Set(ByVal value As CompositeCommand)
                _cancelAllOrdersCommand = value
            End Set
        End Property
    End Class

    <Export(), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class StockTraderRICommandProxy
        Public Overridable ReadOnly Property SubmitOrderCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.SubmitOrderCommand
            End Get
        End Property

        Public Overridable ReadOnly Property CancelOrderCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.CancelOrderCommand
            End Get
        End Property

        Public Overridable ReadOnly Property SubmitAllOrdersCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.SubmitAllOrdersCommand
            End Get
        End Property

        Public Overridable ReadOnly Property CancelAllOrdersCommand() As CompositeCommand
            Get
                Return StockTraderRICommands.CancelAllOrdersCommand
            End Get
        End Property
    End Class
End Namespace
