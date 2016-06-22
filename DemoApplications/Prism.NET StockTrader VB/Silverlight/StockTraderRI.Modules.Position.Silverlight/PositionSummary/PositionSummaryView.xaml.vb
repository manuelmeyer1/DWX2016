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
Imports System.Windows.Controls
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.Position.PositionSummary
    <ViewExport(privateRegionName:=RegionNames.MainRegion), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class PositionSummaryView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        <Import()> _
        Public Property Model() As IPositionSummaryViewModel
            Get
                Return TryCast(Me.DataContext, IPositionSummaryViewModel)
            End Get

            Set(ByVal value As IPositionSummaryViewModel)
                ' Because in Silverlight you cannot bind to a RelativeSource, we are using Resources with an observable value,
                ' in order to be able to bind to the Buy and Sell commands. 
                ' The resources are declared in the XAML, because Silverlight has StaticResource markup only, so these
                ' resources should be available when the control is initializing, even though the Value is yet not set.
                CType(Me.Resources("BuyCommand"), ObservableCommand).Value = _
                    If(value IsNot Nothing, value.BuyCommand, Nothing)
                CType(Me.Resources("SellCommand"), ObservableCommand).Value = _
                    If(value IsNot Nothing, value.SellCommand, Nothing)
                DataContext = value
            End Set
        End Property
    End Class
End Namespace