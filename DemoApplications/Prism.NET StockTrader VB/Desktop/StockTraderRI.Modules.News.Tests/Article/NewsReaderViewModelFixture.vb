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
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Modules.News.Article
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Modules.News.Tests.Article
    <TestClass()> _
    Public Class NewsReaderViewModelFixture
        <TestMethod()> _
        Public Sub SetNewsArticleUpdatesProperty()
            Dim target = New NewsReaderViewModel()

            Dim propertyChangedRaised As Boolean = False
            AddHandler target.PropertyChanged, Sub(sender, e)
                                                   If e.PropertyName = "NewsArticle" Then
                                                       propertyChangedRaised = True
                                                   End If
                                               End Sub

            Dim article As New NewsArticle() With {.Title = "My Title", .Body = "My Body"}
            target.NewsArticle = article

            Assert.AreSame(article, target.NewsArticle)
            Assert.IsTrue(propertyChangedRaised)
        End Sub
    End Class
End Namespace
