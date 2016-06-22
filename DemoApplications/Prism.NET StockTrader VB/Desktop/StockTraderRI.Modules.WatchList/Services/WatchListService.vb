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
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports System.Globalization
Imports Microsoft.Practices.Prism.Commands
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Watch.Services
    <Export(GetType(IWatchListService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class WatchListService
        Implements IWatchListService
        Private ReadOnly marketFeedService As IMarketFeedService

        Private _WatchItems As ObservableCollection(Of String)

        Private Property WatchItems() As ObservableCollection(Of String)
            Get
                Return _WatchItems
            End Get
            Set(ByVal value As ObservableCollection(Of String))
                _WatchItems = value
            End Set
        End Property

        <ImportingConstructor()> _
        Public Sub New(ByVal marketFeedService As IMarketFeedService)
            Me.marketFeedService = marketFeedService
            WatchItems = New ObservableCollection(Of String)()

            AddWatchCommand = New DelegateCommand(Of String)(AddressOf AddWatch)
        End Sub

        Public Function RetrieveWatchList() As ObservableCollection(Of String) _
            Implements IWatchListService.RetrieveWatchList
            Return WatchItems
        End Function

        Private Sub AddWatch(ByVal tickerSymbol As String)
            If Not String.IsNullOrEmpty(tickerSymbol) Then
                Dim upperCasedTrimmedSymbol As String = tickerSymbol.ToUpper(CultureInfo.InvariantCulture).Trim()
                If Not WatchItems.Contains(upperCasedTrimmedSymbol) Then
                    If marketFeedService.SymbolExists(upperCasedTrimmedSymbol) Then
                        WatchItems.Add(upperCasedTrimmedSymbol)
                    End If
                End If
            End If
        End Sub

        Private _AddWatchCommand As ICommand

        Public Property AddWatchCommand() As ICommand Implements IWatchListService.AddWatchCommand
            Get
                Return _AddWatchCommand
            End Get
            Set(ByVal value As ICommand)
                _AddWatchCommand = value
            End Set
        End Property
    End Class
End Namespace
