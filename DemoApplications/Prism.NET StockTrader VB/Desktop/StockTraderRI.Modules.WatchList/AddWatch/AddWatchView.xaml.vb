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
Imports System.Diagnostics.CodeAnalysis
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.Watch.AddWatch
    ''' <summary>
    ''' Interaction logic for AddWatchControl.xaml
    ''' </summary>
    <ViewExport(privateRegionName:=RegionNames.MainToolBarRegion), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class AddWatchView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Sets the ViewModel.
        ''' </summary>
        ''' <remarks>
        ''' This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        ''' the appropriate view model.
        ''' </remarks>
        <Import(), _
            SuppressMessage _
                ("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", _
                 Justification:="Needs to be a property to be composed by MEF")> _
        Private WriteOnly Property ViewModel() As AddWatchViewModel
            Set(ByVal value As AddWatchViewModel)
                Me.DataContext = value
            End Set
        End Property
    End Class
End Namespace
