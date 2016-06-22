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
Imports System.Collections.ObjectModel
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.Events
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Moq
Imports StockTraderRI.Modules.Position.PositionSummary
Imports StockTraderRI.Modules.Position.Tests.Mocks

Namespace StockTraderRI.Modules.Position.Tests.PositionSummary
    <TestClass()> _
    Public Class PositionSummaryPresentationModelFixture
        Private ordersController As MockOrdersController
        Private eventAggregator As Mock(Of IEventAggregator)
        Private observablePosition As MockObservablePosition

        <TestInitialize()> _
        Public Sub SetUp()
            ordersController = New MockOrdersController()
            observablePosition = New MockObservablePosition()
            eventAggregator = New Mock(Of IEventAggregator)()
            eventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                     New  _
                                                                                                        MockTickerSymbolSelectedEvent())
        End Sub

        <TestMethod()> _
        Public Sub ShouldTriggerPropertyChangedEventOnCurrentPositionSummaryItemChange()
            Dim presentationModel As PositionSummaryViewModel = CreatePresentationModel()
            Dim changedPropertyName As String = String.Empty

            AddHandler presentationModel.PropertyChanged, Sub(o, e) changedPropertyName = e.PropertyName
            presentationModel.CurrentPositionSummaryItem = New PositionSummaryItem("NewTickerSymbol", 0, 0, 0)

            Assert.AreEqual("CurrentPositionSummaryItem", changedPropertyName)
        End Sub

        <TestMethod()> _
        Public Sub TickerSymbolSelectedPublishesEvent()
            Dim tickerSymbolSelectedEvent = New MockTickerSymbolSelectedEvent()
            eventAggregator.Setup(Function(x) x.GetEvent(Of TickerSymbolSelectedEvent)()).Returns( _
                                                                                                     tickerSymbolSelectedEvent)
            Dim presentationModel = CreatePresentationModel()

            presentationModel.CurrentPositionSummaryItem = New PositionSummaryItem("FUND0", 0, 0, 0)

            Assert.IsTrue(tickerSymbolSelectedEvent.PublishCalled)
            Assert.AreEqual("FUND0", tickerSymbolSelectedEvent.PublishArgumentPayload)
        End Sub

        <TestMethod()> _
        Public Sub ControllerCommandsSetIntoPresentationModel()
            Dim presentationModel = CreatePresentationModel()

            Assert.AreSame(presentationModel.BuyCommand, ordersController.BuyCommand)
            Assert.AreSame(presentationModel.SellCommand, ordersController.SellCommand)
        End Sub

        Private Function CreatePresentationModel() As PositionSummaryViewModel
            Return New PositionSummaryViewModel(ordersController, eventAggregator.Object, observablePosition)

        End Function

        <TestMethod()> _
        Public Sub CurrentItemNullDoesNotThrow()
            Dim model = CreatePresentationModel()

            model.CurrentPositionSummaryItem = Nothing
        End Sub
    End Class

    Friend Class MockTickerSymbolSelectedEvent
        Inherits TickerSymbolSelectedEvent
        Public PublishCalled As Boolean
        Public PublishArgumentPayload As String

        Public Overrides Sub Publish(ByVal payload As String)
            PublishCalled = True
            PublishArgumentPayload = payload
        End Sub
    End Class

    Friend Class MockObservablePosition
        Implements IObservablePosition

        Private _Items As ObservableCollection(Of PositionSummaryItem)

        Public Property Items() As ObservableCollection(Of PositionSummaryItem) Implements IObservablePosition.Items
            Get
                Return _Items
            End Get
            Set(ByVal value As ObservableCollection(Of PositionSummaryItem))

            End Set
        End Property
    End Class
End Namespace

'
' * 
' * market update with volume change should be reflected in presentation model
' 
