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
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.Position.PositionSummary
    <ViewExport(privateRegionName:=RegionNames.MainRegion), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class PositionSummaryView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

#Region "IPositionSummaryView Members"

        <Import()> _
        Public Property Model() As IPositionSummaryViewModel
            Get
                Return TryCast(DataContext, IPositionSummaryViewModel)
            End Get
            Set(ByVal value As IPositionSummaryViewModel)
                DataContext = value
            End Set
        End Property

#End Region
    End Class
End Namespace
