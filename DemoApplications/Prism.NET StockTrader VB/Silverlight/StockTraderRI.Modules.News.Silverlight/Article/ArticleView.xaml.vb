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
Imports StockTraderRI.Modules.News.Controllers
Imports StockTraderRI.Infrastructure
Imports System.Windows

Namespace StockTraderRI.Modules.News.Article
    <ViewExport(privateRegionName:=RegionNames.ResearchRegion), PartCreationPolicy(CreationPolicy.NonShared)> _
    Partial Public Class ArticleView
        Inherits UserControl
        ' Note - this import is here so that the controller is created and gets wired to the article and news reader
        ' view models, which are shared instances
        <Import()> Public newsController As INewsController

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
        Public WriteOnly Property ViewModel() As ArticleViewModel
            Set(ByVal value As ArticleViewModel)
                Me.DataContext = value
            End Set
        End Property

        Private Sub ListBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            If Me.NewsList.SelectedItem IsNot Nothing Then
                VisualStateManager.GoToState(Me, "Details", True)
            Else
                VisualStateManager.GoToState(Me, "List", True)
            End If
        End Sub
    End Class
End Namespace
