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

Namespace StockTraderRI.Modules.Position.PositionSummary
    <Export(GetType(IPositionPieChartViewModel)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class PositionPieChartViewModel
        Implements IPositionPieChartViewModel
        Private _Position As IObservablePosition

        Public Property Position() As IObservablePosition Implements IPositionPieChartViewModel.Position
            Get
                Return _Position
            End Get
            Private Set(ByVal value As IObservablePosition)
                _Position = value
            End Set
        End Property

        <ImportingConstructor()> _
        Public Sub New(ByVal observablePosition As IObservablePosition)
            Me.Position = observablePosition
        End Sub
    End Class
End Namespace
