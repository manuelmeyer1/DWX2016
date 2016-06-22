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
Imports Microsoft.Practices.Prism.ViewModel
Imports StockTraderRI.Modules.Watch.Services

Namespace StockTraderRI.Modules.Watch.AddWatch
    <Export(GetType(AddWatchViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class AddWatchViewModel
        Inherits NotificationObject
        Private _stockSymbol As String
        Private watchListService As IWatchListService

        <ImportingConstructor()> _
        Public Sub New(ByVal watchListService As IWatchListService)
            If watchListService Is Nothing Then
                Throw New ArgumentNullException("service")
            End If
            Me.watchListService = watchListService
        End Sub

        Public Property StockSymbol() As String
            Get
                Return _stockSymbol
            End Get
            Set(ByVal value As String)
                _stockSymbol = value
                Me.RaisePropertyChanged(Function() StockSymbol)
            End Set
        End Property

        Public ReadOnly Property AddWatchCommand() As ICommand
            Get
                Return Me.watchListService.AddWatchCommand
            End Get
        End Property
    End Class
End Namespace
