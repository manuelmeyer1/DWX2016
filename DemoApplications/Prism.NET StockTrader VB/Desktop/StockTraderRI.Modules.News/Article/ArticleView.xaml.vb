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
Imports System.Windows.Media.Animation
Imports StockTraderRI.Modules.News.Controllers
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.News.Article
    ''' <summary>
    ''' Interaction logic for ArticleView.xaml
    ''' </summary>
    <ViewExport(privateRegionName:=RegionNames.ResearchRegion), PartCreationPolicy(CreationPolicy.Shared)> _
    Partial Public Class ArticleView
        Inherits UserControl
        ' Note - this import is here so that the controller is created and gets wired to the article and news reader
        ' view models, which are shared instances
        '#pragma warning disable 169
        <Import()> Private newsController As INewsController
        '#pragma warning restore 169

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
        Private WriteOnly Property ViewModel() As ArticleViewModel
            Set(ByVal value As ArticleViewModel)
                Me.DataContext = value
            End Set
        End Property

        Private Sub ListBox_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            If Me.NewsList.SelectedItem IsNot Nothing Then
                Dim storyboard = CType(Me.Resources("Details"), Storyboard)
                storyboard.Begin()
            Else
                Dim storyboard = CType(Me.Resources("List"), Storyboard)
                storyboard.Begin()
            End If
        End Sub
    End Class
End Namespace
