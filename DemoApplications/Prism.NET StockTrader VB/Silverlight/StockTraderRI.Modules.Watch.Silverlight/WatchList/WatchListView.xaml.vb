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
Imports System.Diagnostics.CodeAnalysis
Imports System.Windows.Data
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.Watch.WatchList
    <ViewExport(privateRegionName:=RegionNames.MainRegion), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class WatchListView
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
        Public WriteOnly Property ViewModel() As WatchListViewModel
            Set(ByVal value As WatchListViewModel)
                Me.DataContext = value
                Me.OnDataContextChanged()
            End Set
        End Property

        Private Sub OnDataContextChanged()
            ' Because in Silverlight you cannot bind to a RelativeSource, we are using Resources with an observable value,
            ' in order to be able to bind to the Buy and Sell commands. 
            ' The resources are declared in the XAML, because Silverlight has StaticResource markup only, so these
            ' resources should be available when the control is initializing, even though the Value is yet not set.
            Dim binding As New Binding("RemoveWatchCommand")
            binding.Source = Me.DataContext
            CType(Me.Resources("RemoveWatchCommand"), ObservableCommand).SetBinding(ObservableCommand.ValueProperty, _
                                                                                       binding)
        End Sub
    End Class
End Namespace
