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
Imports StockTraderRI.Infrastructure.Interfaces
Imports StockTraderRI.Modules.Position.Controllers
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.Practices.Prism.ViewModel

Namespace StockTraderRI.Modules.Position.PositionSummary
    <Export(GetType(IPositionSummaryViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class PositionSummaryViewModel
        Inherits NotificationObject
        Implements IPositionSummaryViewModel

        Private _currentPositionSummaryItem As PositionSummaryItem

        Private ReadOnly eventAggregator As IEventAggregator

        Private _Position As IObservablePosition

        Public Property Position() As IObservablePosition Implements IPositionSummaryViewModel.Position
            Get
                Return _Position
            End Get
            Private Set(ByVal value As IObservablePosition)
                _Position = value
            End Set
        End Property

        <ImportingConstructor()> _
        Public Sub New(ByVal ordersController As IOrdersController, ByVal eventAggregator As IEventAggregator, _
                        ByVal observablePosition As IObservablePosition)
            Me.eventAggregator = eventAggregator
            Me.Position = observablePosition

            BuyCommand = ordersController.BuyCommand
            SellCommand = ordersController.SellCommand

            Me.CurrentPositionSummaryItem = New PositionSummaryItem("FAKEINDEX", 0, 0, 0)
        End Sub

        Private _BuyCommand As ICommand

        Public Property BuyCommand() As ICommand Implements IPositionSummaryViewModel.BuyCommand
            Get
                Return _BuyCommand
            End Get
            Private Set(ByVal value As ICommand)
                _BuyCommand = value
            End Set
        End Property

        Private _SellCommand As ICommand

        Public Property SellCommand() As ICommand Implements IPositionSummaryViewModel.SellCommand
            Get
                Return _SellCommand
            End Get
            Private Set(ByVal value As ICommand)
                _SellCommand = value
            End Set
        End Property

        Public ReadOnly Property HeaderInfo() As String Implements IHeaderInfoProvider(Of String).HeaderInfo
            Get
                Return "POSITION"
            End Get
        End Property

        Public Property CurrentPositionSummaryItem() As PositionSummaryItem
            Get
                Return _currentPositionSummaryItem
            End Get
            Set(ByVal value As PositionSummaryItem)
                If _currentPositionSummaryItem IsNot value Then
                    _currentPositionSummaryItem = value
                    Me.RaisePropertyChanged(Function() Me.CurrentPositionSummaryItem)
                    If _currentPositionSummaryItem IsNot Nothing Then
                        eventAggregator.GetEvent(Of TickerSymbolSelectedEvent)().Publish( _
                                                                                           CurrentPositionSummaryItem. _
                                                                                              TickerSymbol)
                    End If
                End If
            End Set
        End Property
    End Class
End Namespace
