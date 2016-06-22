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
Imports StockTraderRI.Modules.Position.Interfaces

Namespace StockTraderRI.Modules.Position.Orders
    ''' <summary>
    ''' Interaction logic for TransactionView.xaml
    ''' </summary>
    <Export(GetType(IOrdersView)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class OrdersView
        Inherits UserControl
        Implements IOrdersView

        Public Sub New()
            InitializeComponent()
        End Sub

        <Import()> _
        Public WriteOnly Property ViewModel() As IOrdersViewModel
            Set(ByVal value As IOrdersViewModel)
                Me.DataContext = value
            End Set
        End Property
    End Class
End Namespace
