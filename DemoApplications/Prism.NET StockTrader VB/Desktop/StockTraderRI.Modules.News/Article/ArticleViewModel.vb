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
Imports System.Windows.Input
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Commands
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.Regions
Imports StockTraderRI.Infrastructure.Interfaces
Imports Microsoft.Practices.Prism.ViewModel
Imports StockTraderRI.Infrastructure.Models

Namespace StockTraderRI.Modules.News.Article
    <Export(GetType(ArticleViewModel)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class ArticleViewModel
        Inherits NotificationObject
        Private _companySymbol As String
        Private _articles As IList(Of NewsArticle)
        Private _selectedArticle As NewsArticle
        Private ReadOnly newsFeedService As INewsFeedService
        Private ReadOnly regionManager As IRegionManager
        Private ReadOnly _showArticleListCommand As ICommand
        Private ReadOnly showNewsReaderViewCommand As ICommand

        <ImportingConstructor()> _
        Public Sub New(ByVal newsFeedService As INewsFeedService, ByVal regionManager As IRegionManager, _
                        ByVal eventAggregator As IEventAggregator)
            If newsFeedService Is Nothing Then
                Throw New ArgumentNullException("newsFeedService")
            End If

            If regionManager Is Nothing Then
                Throw New ArgumentNullException("regionManager")
            End If

            If eventAggregator Is Nothing Then
                Throw New ArgumentNullException("eventAggregator")
            End If

            Me.newsFeedService = newsFeedService
            Me.regionManager = regionManager

            Me._showArticleListCommand = New DelegateCommand(AddressOf Me.ShowArticleList)
            Me.showNewsReaderViewCommand = New DelegateCommand(AddressOf Me.ShowNewsReaderView)

            eventAggregator.GetEvent(Of TickerSymbolSelectedEvent)().Subscribe(AddressOf OnTickerSymbolSelected, _
                                                                                 ThreadOption.UIThread)
        End Sub

        Public Property CompanySymbol() As String
            Get
                Return Me._companySymbol
            End Get
            Set(ByVal value As String)
                If Me._companySymbol <> value Then
                    Me._companySymbol = value
                    Me.RaisePropertyChanged(Function() Me.CompanySymbol)
                    Me.OnCompanySymbolChanged()
                End If
            End Set
        End Property

        Public Property SelectedArticle() As NewsArticle
            Get
                Return Me._selectedArticle
            End Get
            Set(ByVal value As NewsArticle)
                If Me._selectedArticle IsNot value Then
                    Me._selectedArticle = value
                    Me.RaisePropertyChanged(Function() Me.SelectedArticle)
                End If
            End Set
        End Property

        Public Property Articles() As IList(Of NewsArticle)
            Get
                Return Me._articles
            End Get
            Private Set(ByVal value As IList(Of NewsArticle))
                If Me._articles IsNot value Then
                    Me._articles = value
                    Me.RaisePropertyChanged(Function() Me.Articles)
                End If
            End Set
        End Property

        Public ReadOnly Property ShowNewsReaderCommand() As ICommand
            Get
                Return Me.showNewsReaderViewCommand
            End Get
        End Property

        Public ReadOnly Property ShowArticleListCommand() As ICommand
            Get
                Return Me._showArticleListCommand
            End Get
        End Property

#If SILVERLIGHT Then

        Public Sub OnTickerSymbolSelected(ByVal companySymbol As String)
            Me.CompanySymbol = companySymbol
        End Sub

#Else
        Private Sub OnTickerSymbolSelected(ByVal companySymbol As String)
            Me.CompanySymbol = companySymbol
        End Sub
#End If

        Private Sub OnCompanySymbolChanged()
            Me.Articles = newsFeedService.GetNews(_companySymbol)
        End Sub

        Private Sub ShowArticleList()
            Me.SelectedArticle = Nothing
        End Sub

        Private Sub ShowNewsReaderView()
            Me.regionManager.RequestNavigate(RegionNames.SecondaryRegion, New Uri("/NewsReaderView", UriKind.Relative))
        End Sub
    End Class
End Namespace
