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
Imports Microsoft.Practices.Prism.ViewModel
Imports StockTraderRI.Infrastructure.Models

Namespace StockTraderRI.Modules.News.Article
    <Export(GetType(NewsReaderViewModel)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class NewsReaderViewModel
        Inherits NotificationObject
        Private _newsArticle As NewsArticle

        Public Property NewsArticle() As NewsArticle
            Get
                Return Me._newsArticle
            End Get
            Set(ByVal value As NewsArticle)
                If Me._newsArticle IsNot value Then
                    Me._newsArticle = value
                    Me.RaisePropertyChanged(Function() Me.NewsArticle)
                End If
            End Set
        End Property
    End Class
End Namespace
