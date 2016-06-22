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
Imports System.ComponentModel
Imports StockTraderRI.Modules.News.Article

Namespace StockTraderRI.Modules.News.Controllers
    <Export(GetType(INewsController)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class NewsController
        Implements INewsController
        Private ReadOnly articleViewModel As ArticleViewModel
        Private ReadOnly newsReaderViewModel As NewsReaderViewModel

        <SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId:="newsReader"), _
            ImportingConstructor()> _
        Public Sub New(ByVal articleViewModel As ArticleViewModel, ByVal newsReaderViewModel As NewsReaderViewModel)
            Me.articleViewModel = articleViewModel
            Me.newsReaderViewModel = newsReaderViewModel
            AddHandler Me.articleViewModel.PropertyChanged, AddressOf ArticleViewModel_PropertyChanged
        End Sub

        Private Sub ArticleViewModel_PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
            Select Case e.PropertyName
                Case "SelectedArticle"
                    Me.newsReaderViewModel.NewsArticle = Me.articleViewModel.SelectedArticle
            End Select
        End Sub
    End Class
End Namespace
