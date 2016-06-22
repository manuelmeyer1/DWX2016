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
    ''' <summary>
    ''' Summary description for PositionSummaryItemFixture
    ''' </summary>
    <TestClass()> _
    Public Class PositionSummaryItemFixture
        <TestMethod()> _
        Public Sub ChangingCurrentPriceFiresPropertyChangeNotificationEvent()
            Dim positionSummary As New PositionSummaryItem("FUND0", 49.99D, 50, 52.99D)

            Dim currentPriceChanged As Boolean = False
            AddHandler positionSummary.PropertyChanged, Sub(sender, e)
                                                            If e.PropertyName = "CurrentPrice" Then
                                                                currentPriceChanged = True
                                                            End If
                                                        End Sub

            positionSummary.CurrentPrice -= 5

            Assert.IsTrue(currentPriceChanged)
        End Sub

        <TestMethod()> _
        Public Sub ChangingCostBasisFiresPropertyChangeNotificationEvent()
            Dim positionSummary As New PositionSummaryItem("FUND0", 49.99D, 50, 52.99D)

            Dim costBasisPropertyChanged As Boolean = False
            AddHandler positionSummary.PropertyChanged, Sub(sender, e)
                                                            If e.PropertyName = "CostBasis" Then
                                                                costBasisPropertyChanged = True
                                                            End If
                                                        End Sub

            positionSummary.CostBasis -= 5

            Assert.IsTrue(costBasisPropertyChanged)
        End Sub

        <TestMethod()> _
        Public Sub ChangingSharesFiresPropertyChangeNotificationEvent()
            Dim positionSummary As New PositionSummaryItem("FUND0", 49.99D, 50, 52.99D)

            Dim sharesPropertyChanged As Boolean = False
            AddHandler positionSummary.PropertyChanged, Sub(sender, e)
                                                            If e.PropertyName = "Shares" Then
                                                                sharesPropertyChanged = True
                                                            End If
                                                        End Sub

            positionSummary.Shares -= 5

            Assert.IsTrue(sharesPropertyChanged)
        End Sub

        <TestMethod()> _
        Public Sub ChangingSymbolPropertyChangeNotificationEvent()
            Dim positionSummary As New PositionSummaryItem("AAAA", 49.99D, 50, 52.99D)

            Dim propertyChanged As Boolean = False
            Dim lastPropertyChanged As String = String.Empty

            AddHandler positionSummary.PropertyChanged, Sub(sender, e)
                                                            propertyChanged = True
                                                            lastPropertyChanged = e.PropertyName
                                                        End Sub

            positionSummary.TickerSymbol = "XXXX"

            Assert.IsTrue(propertyChanged)
            Assert.AreEqual(Of String)("TickerSymbol", lastPropertyChanged)
        End Sub

        <TestMethod()> _
        Public Sub PositionSummaryCalculatesCurrentMarketValue()
            Dim lastPrice As Decimal = 52.99D
            Dim numShares As Long = 50

            Dim positionSummary As New PositionSummaryItem("AAAA", 49.99D, numShares, lastPrice)

            Assert.AreEqual(Of Decimal)(lastPrice * numShares, positionSummary.MarketValue)
        End Sub

        <TestMethod()> _
        Public Sub PositionSummaryCalculatesGainLossPercent()
            Dim costBasis As Decimal = 49.99D
            Dim lastPrice As Decimal = 52.99D
            Dim numShares As Long = 1000

            Dim positionSummary As New PositionSummaryItem("AAAA", costBasis, numShares, lastPrice)

            Assert.AreEqual(Of Decimal)(105901.2002D, Math.Round(positionSummary.GainLossPercent, 4))
        End Sub
    End Class
End Namespace