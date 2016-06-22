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
Imports System.Text
Imports AcceptanceTestLibrary.Common
Imports System.Windows.Automation
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Page
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports AcceptanceTestLibrary.ApplicationHelper
Imports System.Globalization
Imports AcceptanceTestLibrary.UIAWrapper
Imports StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
Imports System.Threading

Namespace StockTraderRI.Tests.AcceptanceTest.TestEntities.Assertion
    Public NotInheritable Class StockTraderRIAssertion(Of TApp As {AppLauncherBase, New})
        #Region "Position Summary Module Assert"
        Private Shared testDataInfrastructure As New TestDataInfrastructure()
        Private Sub New()
        End Sub
        Public Shared Sub AssertPositionSummaryTab()
            InternalAssertControl(StockTraderRIPage(Of TApp).PositionSummaryTab, "Position Summary tab is not loaded.")
        End Sub

        Public Shared Sub AssertPositionSummaryGrid()
            InternalAssertControl(StockTraderRIPage(Of TApp).PositionSummaryGrid, "Position Summary grid is not loaded.")
        End Sub

        Public Shared Sub AssertPositionSummaryColumnCount()
            Dim gridPattern As GridPattern = GetGridPattern()
            Assert.AreEqual(New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("PositionSummaryColumnCount"), gridPattern.Current.ColumnCount.ToString())
        End Sub

        Public Shared Sub DesktopAssertPositionSummaryColumnCount()
            Dim gridPattern As GridPattern = GetGridPattern()
            Assert.AreEqual("7", gridPattern.Current.ColumnCount.ToString())
        End Sub

        Public Shared Sub AssertPositionSummaryRowCount()
            Dim gridPattern As GridPattern = GetGridPattern()
            'read number of account positions from the AccountPosition.xml data file
            Dim positionRowCount As String = New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("PositionSummaryRowCount")
            'testDataInfrastructure.GetCount(Of AccountPositionDataProvider, AccountPosition)()
            Assert.AreEqual(positionRowCount, gridPattern.Current.RowCount.ToString())
        End Sub

        Public Shared Sub AssertPositionSummaryValuesForSymbol()
            'For each row, get the column values and test it the new value
            Dim gridPattern As GridPattern = GetGridPattern()

            Dim share As String
            Dim lastPrice As String
            Dim marketPrice As String
            Dim gainLoss As String
            Dim costBasis As String
            Dim symbol As String

            'Test for the number of rows displayed in the Position summary table in the UI
            For rowCountIndex As Integer = 0 To gridPattern.Current.RowCount - 1
                ' Get the Stock name 
                symbol = gridPattern.GetItem(rowCountIndex, 0).Current.Name ' Column 0 is for Stock


                'input columns
                share = gridPattern.GetItem(rowCountIndex, 1).Current.Name.Replace("$"c, "0"c) ' Column 1 is for number of share
                lastPrice = gridPattern.GetItem(rowCountIndex, 2).Current.Name.Replace("$"c, "0"c) ' Column 2 is for last price
                costBasis = gridPattern.GetItem(rowCountIndex, 3).Current.Name.Replace("$"c, "0"c) ' Column 3 is for cost

                'computed columns
                marketPrice = gridPattern.GetItem(rowCountIndex, 4).Current.Name.Replace("$"c, "0"c) ' Column 4 is for market price for symbol
                gainLoss = gridPattern.GetItem(rowCountIndex, 5).Current.Name ' Column 5 is for Gain Loss % for symbol
                gainLoss = gainLoss.Remove(gainLoss.Length - 1)
                Dim tempValue As Double
                Assert.IsTrue(Double.TryParse(share, tempValue), String.Format(CultureInfo.CurrentCulture, "Number of shares {0} is not numeric", symbol))
                Assert.IsTrue(Double.TryParse(lastPrice, tempValue), String.Format(CultureInfo.CurrentCulture, "Lastprice for {0} is not numeric", symbol))
                Assert.IsTrue(Double.TryParse(costBasis, tempValue), String.Format(CultureInfo.CurrentCulture, "Cost basis Value for {0} is not numeric", symbol))
                Assert.IsTrue(Double.TryParse(marketPrice, tempValue), String.Format(CultureInfo.CurrentCulture, "Market price for {0} is not numeric", symbol))
                Assert.IsTrue(Double.TryParse(gainLoss, tempValue), String.Format(CultureInfo.CurrentCulture, "Gainloss % Value for {0} is not numeric", symbol))
            Next rowCountIndex
        End Sub

        #End Region

        #Region "Common Methods"
        Private Shared Sub InternalAssertControl(ByVal control As AutomationElement, ByVal message As String)
            Assert.IsNotNull(control, message)
        End Sub
        #End Region

        #Region "Market Trend Module Assert"

        Public Shared Sub AssertHistoricalDataText()
            InternalAssertControl(StockTraderRIPage(Of TApp).HistoricalDataText, "Historical Data Text block is not loaded.")
        End Sub

        Public Shared Sub AssertPieChartTextBlock()
            InternalAssertControl(StockTraderRIPage(Of TApp).PieChartTextBlock, "Pie Chart Text block is not loaded.")
        End Sub
        #End Region

        #Region "WatchList Module Assert"

        Public Shared Sub AssertWatchListGrid()
            InternalAssertControl(StockTraderRIPage(Of TApp).WatchListGrid, "WatchList Grid is not loaded.")
        End Sub
        #End Region

        #Region "NewsArticle Module Assert"

        Public Shared Sub AssertNewsArticleText()
            InternalAssertControl(StockTraderRIPage(Of TApp).NewsArticleText, "News Article Text block is not loaded.")
        End Sub
        #End Region

        #Region "Orders ToolBar Module Assert"

        Public Shared Sub AssertSubmitButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).SubmitButton, "Submit Button in Orders Tool bar is not loaded.")
        End Sub

        Public Shared Sub AssertSubmitAllButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).SubmitAllButton, "Submit All Button in Orders Tool bar is not loaded.")
        End Sub

        Public Shared Sub AssertCancelButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).CancelButton, "Cancel Button in Orders Tool bar is not loaded.")
        End Sub

        Public Shared Sub AssertCancelAllButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).CancelAllButton, "Cancel All Button in Orders Tool bar is not loaded.")
        End Sub

        Public Shared Sub AssertAddtoWatchListButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).AddToWatchListButton, "Add to Watch List Button in Orders Tool bar is not loaded..")
        End Sub

        Public Shared Sub AssertTextBoxBlock()
            InternalAssertControl(StockTraderRIPage(Of TApp).TextBoxBlock, "TextBox in Orders Tool bar is not loaded.")
        End Sub

        Public Shared Sub AssertStockAddedinWatchListTextBox()
            Dim aeTextBoxBlock As AutomationElement = StockTraderRIPage(Of TApp).TextBoxBlock
            InternalAssertControl(aeTextBoxBlock, "TextBox in Orders Tool bar is not loaded.")
            aeTextBoxBlock.SetValue(New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("StockName"))
            SendKeys.SendWait("{ENTER}")
            System.Threading.Thread.Sleep(1000)
        End Sub
        Public Shared Sub AssertStockRemovedfromWatchListTextBox()
            Dim aeRemoveButton As AutomationElement = StockTraderRIPage(Of TApp).ActionsRemoveButton
            aeRemoveButton.Click()
            Dim gridPattern As GridPattern = GetGridPatternWatchList()
            Assert.AreEqual(New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("DefaultValue"), gridPattern.Current.RowCount.ToString())

        End Sub

        Public Shared Sub AssertWatchListRowCount()
            Dim gridPattern As GridPattern = GetGridPatternWatchList()
            Assert.AreEqual(New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue("WatchListRowCount"), gridPattern.Current.RowCount.ToString())
        End Sub

        #End Region

        #Region "PositionBuySellTab Assert"

        Public Shared Sub AssertPositionBuySellTab()
            InternalAssertControl(StockTraderRIPage(Of TApp).PositionBuySellTab, "Position Buy & Sell tab is not loaded.")
        End Sub

        Public Shared Sub AssertPositionBuyButtonClickTest(ByVal [option] As String)
            If [option].Equals("Buy") Then
                System.Threading.Thread.Sleep(3000)
                Dim ActionBuyButton As AutomationElement = StockTraderRIPage(Of TApp).ActionsBuyButton
                ActionBuyButton.Click()
            ElseIf [option].Equals("Sell") Then
                System.Threading.Thread.Sleep(3000)
                Dim ActionsellButton As AutomationElement = StockTraderRIPage(Of TApp).ActionsSellButton
                ActionsellButton.Click()
            End If
        End Sub

        Public Shared Sub AssertTermComboBox()
            InternalAssertControl(StockTraderRIPage(Of TApp).TermComboBox, "Term ComboBox is not loaded.")
        End Sub

        Public Shared Sub AssertPriceLimitTextBox()
            InternalAssertControl(StockTraderRIPage(Of TApp).PriceLimitTextBox, "PriceLimit TextBox is not loaded.")
        End Sub

        Public Shared Sub AssertSellRadioButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).SellRadioButton, "Sell RadioButton is not loaded.")
        End Sub

        Public Shared Sub AssertBuyRadioButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).BuyRadioButton, "Buy RadioButton is not loaded.")
        End Sub

        Public Shared Sub AssertSharesTextBox()
            InternalAssertControl(StockTraderRIPage(Of TApp).SharesTextBox, "Shares TextBox is not loaded.")
        End Sub

        Public Shared Sub AssertSymbolTextBox()
            InternalAssertControl(StockTraderRIPage(Of TApp).SymbolTextBox, "Symbol TextBox is not loaded.")
        End Sub

        Public Shared Sub AssertOrderTypeComboBox()
            InternalAssertControl(StockTraderRIPage(Of TApp).OrderTypeComboBox, "OrderType ComboBox is not loaded.")
        End Sub

        Public Shared Sub AssertOrderCommandSubmit()
            InternalAssertControl(StockTraderRIPage(Of TApp).OrderCommandSubmit, "OrderCommand Submit Button is not loaded.")
        End Sub

        Public Shared Sub AssertOrderCommandCancel()
            InternalAssertControl(StockTraderRIPage(Of TApp).OrderCommandCancel, "Order Command Cancel Button is not loaded.")
        End Sub

        Public Shared Sub AssertOrderCommandSubmitAllButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).OrderCommandSubmitAllButton, "Order Command SubmitAll Button is not loaded.")
        End Sub

        Public Shared Sub AssertOrderCommandCancelAllButton()
            InternalAssertControl(StockTraderRIPage(Of TApp).OrderCommandCancelAllButton, "Order Command CancelAll Button is not loaded.")
        End Sub

        Public Shared Sub AssertAttemptBuySellOrderWithValidData()
            SLPopulateValidStockDetails()
            System.Threading.Thread.Sleep(3000)
            Dim SubmitButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandSubmit
            Assert.IsTrue(SubmitButton.Current.IsEnabled, "Submit Button disabled for valid Shares details")
            SubmitButton.Click()
            System.Threading.Thread.Sleep(3000)
        End Sub

        Public Shared Sub AssertAttemptBuySellOrderWithInValidData()
            SLPopulateInvalidStockDetails()
            System.Threading.Thread.Sleep(3000)
            Dim SubmitButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandSubmit
            Assert.IsFalse(SubmitButton.Current.IsEnabled, "Submit Button enabled for Invalid Shares details")
        End Sub

        Public Shared Sub AssertAttemptOrderCancelByCancelButton()
            Dim SharesTextBoxValue As AutomationElement = StockTraderRIPage(Of TApp).SharesTextBox
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultShares"))
            Dim PriceLimitValue As AutomationElement = StockTraderRIPage(Of TApp).PriceLimitTextBox
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultPriceLimit"))
            Dim TermComboBoxValue As AutomationElement = StockTraderRIPage(Of TApp).TermComboBox
            Dim CancelButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandCancel
            Assert.IsTrue(CancelButton.Current.IsEnabled, "Cancel Button disabled")
            CancelButton.Click()
        End Sub

        Public Shared Sub AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            SLPopulateValidStockDetails()
            System.Threading.Thread.Sleep(3000)
            Dim submitAllButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandSubmitAllButton
            submitAllButton.Click()
            System.Threading.Thread.Sleep(3000)
            Assert.IsFalse(submitAllButton.Current.IsEnabled, "Submit All Button Enabled  even after Submitting All the Details")

        End Sub
        Public Shared Sub AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            SLPopulateValidStockDetails()

            MultipleStockSelectionforSubmitAllCancelAll()
            System.Threading.Thread.Sleep(4000)
            SLPopulateInvalidStockDetails()
            System.Threading.Thread.Sleep(4000)
            Dim submitAllButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandSubmitAllButton
            Assert.IsFalse(submitAllButton.Current.IsEnabled, "Submit All Button Enabled  forInvalid Stock Details")

        End Sub
        Public Shared Sub AssertProcessMultipleStockBuySellByCancelAllButton()
            SLPopulateValidStockDetails()

            MultipleStockSelectionforSubmitAllCancelAll()
            SLPopulateValidStockDetails()
            System.Threading.Thread.Sleep(4000)
            Dim CancelAllButton As AutomationElement = StockTraderRIPage(Of TApp).OrderCommandCancelAllButton
            CancelAllButton.Click()
            System.Threading.Thread.Sleep(4000)
            Assert.IsFalse(CancelAllButton.Current.IsEnabled, "Submit All Button Enabled  even after Submitting All the Details")
        End Sub
        #End Region

        #Region "Private method GridPatterns"

        Private Shared Function GetGridPatternWatchList() As GridPattern
            Return TryCast(StockTraderRIPage(Of TApp).WatchListGrid.GetCurrentPattern(GridPattern.Pattern), GridPattern)
        End Function
        Private Shared Function GetGridPattern() As GridPattern
            Return TryCast(StockTraderRIPage(Of TApp).PositionSummaryGrid.GetCurrentPattern(GridPattern.Pattern), GridPattern)
        End Function
        Private Shared Function GetDataFromTestDataFile(ByVal keyName As String) As String
            Return New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile")).GetValue(keyName)
        End Function

        Private Shared Sub SLPopulateValidStockDetails()
            Dim SharesTextBoxValue As AutomationElement = StockTraderRIPage(Of TApp).SharesTextBox
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultShares"))
            Dim PriceLimitValue As AutomationElement = StockTraderRIPage(Of TApp).PriceLimitTextBox
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultPriceLimit"))
            Dim TermComboBoxValue As AutomationElement = StockTraderRIPage(Of TApp).TermComboBox
            TermComboBoxValue.Expand()
            System.Threading.Thread.Sleep(3000)
            Dim TermValue As AutomationElement = TermComboBoxValue.FindFirst(TreeScope.Descendants, New PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultTerm")))
            'Dim orderviews As AutomationElementCollection = aeorderView.FindAll(TreeScope.Children, New PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.ListItem))
            Assert.IsNotNull(TermValue, "Could not find items in Term combobox")

            Dim clickablePoint As New System.Windows.Point(CInt(Fix(Math.Floor(TermValue.Current.BoundingRectangle.X))), CInt(Fix(Math.Floor(TermValue.Current.BoundingRectangle.Y))))
            System.Windows.Forms.Cursor.Position = New System.Drawing.Point(CInt(Fix(clickablePoint.X)), CInt(Fix(clickablePoint.Y)))
            Thread.Sleep(1000)
            MouseEvents.Click()
        End Sub


        Private Shared Sub SLPopulateInvalidStockDetails()
            Dim SharesTextBoxValue As AutomationElement = StockTraderRIPage(Of TApp).SharesTextBox
            SharesTextBoxValue.SetValue(GetDataFromTestDataFile("DefaultInvalidShares"))
            Dim PriceLimitValue As AutomationElement = StockTraderRIPage(Of TApp).PriceLimitTextBox
            PriceLimitValue.SetValue(GetDataFromTestDataFile("DefaultInvalidPriceLimit"))
            Dim TermComboBoxValue As AutomationElement = StockTraderRIPage(Of TApp).TermComboBox
            TermComboBoxValue.Expand()
            System.Threading.Thread.Sleep(3000)
            Dim TermValue As AutomationElement = TermComboBoxValue.FindFirst(TreeScope.Descendants, New PropertyCondition(AutomationElement.NameProperty, GetDataFromTestDataFile("DefaultTerm")))
            Assert.IsNotNull(TermValue, "Could not find Order item in OrderType combobox")

            Dim clickablePoint As New System.Windows.Point(CInt(Fix(Math.Floor(TermValue.Current.BoundingRectangle.X))), CInt(Fix(Math.Floor(TermValue.Current.BoundingRectangle.Y))))
            System.Windows.Forms.Cursor.Position = New System.Drawing.Point(CInt(Fix(clickablePoint.X)), CInt(Fix(clickablePoint.Y)))
            Thread.Sleep(1000)
            MouseEvents.Click()

        End Sub

        Private Shared Sub MultipleStockSelectionforSubmitAllCancelAll()
            System.Threading.Thread.Sleep(2000)
            Dim PositionSummaryTabvalue As AutomationElement = StockTraderRIPage(Of TApp).PositionSummaryTab
            System.Threading.Thread.Sleep(2000)

            Dim clickablePoint As New System.Windows.Point(CInt(Fix(Math.Floor(PositionSummaryTabvalue.Current.BoundingRectangle.X))), CInt(Fix(Math.Floor(PositionSummaryTabvalue.Current.BoundingRectangle.Y))))
            System.Windows.Forms.Cursor.Position = New System.Drawing.Point(CInt(Fix(clickablePoint.X)), CInt(Fix(clickablePoint.Y)))
            Thread.Sleep(1000)
            MouseEvents.Click()

            System.Threading.Thread.Sleep(3000)
            Dim PositionSummaryGridValue As AutomationElement = StockTraderRIPage(Of TApp).PositionSummaryGrid

            Dim tmpX As Double = 0
            If PositionSummaryGridValue.Current.BoundingRectangle.X > Double.MaxValue Then
                tmpX = -2147483648
            Else
                tmpX = PositionSummaryGridValue.Current.BoundingRectangle.X + 500
            End If

            Dim tmpY As Double = 0
            If PositionSummaryGridValue.Current.BoundingRectangle.Y > Double.MaxValue Then
                tmpY = -2147483648
            Else
                tmpY = PositionSummaryGridValue.Current.BoundingRectangle.Y + 100
            End If

            Dim clickablePoint1 As New System.Windows.Point(
                CInt(Fix(Math.Floor(
                         tmpX))),
                CInt(Fix(Math.Floor(tmpY))))
            System.Windows.Forms.Cursor.Position = New System.Drawing.Point(CInt(Fix(clickablePoint1.X)), CInt(Fix(clickablePoint1.Y)))
            Thread.Sleep(1000)
            MouseEvents.Click()
            System.Threading.Thread.Sleep(3000)
        End Sub
        #End Region
    End Class
End Namespace
