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
Imports StockTraderRI.Modules.Position.Controllers

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Public Class MockOrdersController
        Implements IOrdersController

#Region "IOrdersController Members"

        Private _buyCommand As New DelegateCommand(Of String)(Sub()

                                                              End Sub)

        Public ReadOnly Property BuyCommand() As DelegateCommand(Of String) Implements IOrdersController.BuyCommand
            Get
                Return _buyCommand
            End Get
        End Property

        Private _sellCommand As New DelegateCommand(Of String)(Sub()

                                                               End Sub)

        Public ReadOnly Property SellCommand() As DelegateCommand(Of String) Implements IOrdersController.SellCommand
            Get
                Return _sellCommand
            End Get
        End Property

#End Region
    End Class
End Namespace
