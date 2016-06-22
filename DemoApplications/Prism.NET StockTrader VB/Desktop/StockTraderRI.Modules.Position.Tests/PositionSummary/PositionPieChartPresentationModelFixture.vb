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
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports StockTraderRI.Modules.Position.PositionSummary

Namespace StockTraderRI.Modules.Position.Tests.PositionSummary
    <TestClass()> _
    Public Class PositionPieChartPresentationModelFixture
        <TestMethod()> _
        Public Sub ShouldBuildCorrectly()
            Dim observablePosition = New MockObservablePosition()

            Dim model As New PositionPieChartViewModel(observablePosition)

            Assert.AreSame(observablePosition, model.Position)
        End Sub
    End Class
End Namespace
