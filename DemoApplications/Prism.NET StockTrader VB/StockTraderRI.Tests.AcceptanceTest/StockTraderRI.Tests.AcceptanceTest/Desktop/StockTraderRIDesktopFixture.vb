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
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports System.IO
Imports System.Threading

Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Text
Imports System.Windows.Automation.Provider

Imports AcceptanceTestLibrary.ApplicationHelper
Imports AcceptanceTestLibrary.Common
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Page
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Assertion
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Action
Imports AcceptanceTestLibrary.ApplicationObserver
Imports AcceptanceTestLibrary.Common.Desktop
Imports System.Reflection
Imports AcceptanceTestLibrary.TestEntityBase

Namespace StockTraderRI.Tests.AcceptanceTest.Desktop

    ''' <summary>
    ''' Acceptance test fixture for WPF application
    ''' </summary>
#If DEBUG Then
    <DeploymentItem("..\Desktop\StockTraderRI\bin\Debug", "WPF")>
    <DeploymentItem(".\StockTraderRI.Tests.AcceptanceTest\bin\Debug")>
    <TestClass()>
    Public Class StockTraderRIDesktopFixture
        Inherits FixtureBase(Of WpfAppLauncher)
#Region "Additional tes attributes"

        ' Use TestInitialize to run code before running each test 
        <TestInitialize()> _
        Public Sub MyTestInitialize()
            Dim currentOutputPath As String = (New System.IO.DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.FullName
            StockTraderRIPage(Of WpfAppLauncher).Window = MyBase.LaunchApplication(currentOutputPath & GetDesktopApplication(), GetDesktopApplicationProcess())(0)
        End Sub

        ' Use TestCleanup to run code after each test has run
        <TestCleanup()> _
        Public Sub MyTestCleanup()
            PageBase(Of WpfAppLauncher).DisposeWindow()
            Dim p As Process = WpfAppLauncher.GetCurrentAppProcess()
            MyBase.UnloadApplication(p)
        End Sub

#End Region

#Region "Test Methods"

#Region "Application Launch Test"
        <TestMethod()> _
        Public Sub DesktopApplicationLoadTest()
            Assert.IsNotNull(StockTraderRIPage(Of WpfAppLauncher).Window, "StockTraderRI is not launched.")
        End Sub
#End Region

#End Region

#Region "Position summary Module Load Test"

        ''' <summary>
        ''' Tests if position summary details are loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryTest()
            InvokePositionSummaryAssert()
        End Sub

        ''' <summary>
        ''' Tests the number of columns from position summary view table. 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryColumnCountTest()

            'For now the test data is hardcoded in resource file. But if the datasource is available it will be read from the datasource
            StockTraderRIAssertion(Of WpfAppLauncher).DesktopAssertPositionSummaryColumnCount()
        End Sub

        ''' <summary>
        ''' Tests the number of rows from position summary view table. 
        ''' </summary>

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryRowCountTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryRowCount()
        End Sub

        ''' <summary>
        ''' Tests the computed value (Market value & Gain Loss %) with the value loaded in the screen
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryDataTest()
            'For each Stock or Symbol take the old value and get the value from Web service and monitor that

            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryValuesForSymbol()
        End Sub

#End Region

#Region "Market Trend Test"
        ''' <summary>
        ''' Tests the Historical Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationMarketTrendTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertHistoricalDataText()
        End Sub

        ''' <summary>
        ''' Tests the Pie Chart Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPieChartTextLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPieChartTextBlock()
        End Sub
#End Region

#Region "PositionBuySellTab Test"
        <TestMethod()> _
        Public Sub DesktopPositionBuySellTabControlsLoadTest()
            InvokeDesktopPositionBuySellTabControlsLoad("Buy")
        End Sub


        <TestMethod()> _
        Public Sub DesktopAttemptBuyStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Buy")
        End Sub

        <TestMethod()> _
        Public Sub DesktopAttemptBuyStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Buy")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptSellStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptSellStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptStockBuySellCancelByCancelButton()
            InvokeAttemptOrderCancelByCancelButton()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellByCancelAllButton()
            InvokeProcessMultipleStockBuySellByCancelAllButton()
        End Sub

#End Region

#Region "WatchList Module Test"

        ''' <summary>
        ''' Tests the Watch List Grid is loaded 
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub DesktopApplicationWatchListGridLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListGrid()
        End Sub


#End Region

#Region "NewsArticle Module Load Test"
        ''' <summary>
        ''' Tests the News Articles Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationNewsArticleTextLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertNewsArticleText()
        End Sub
#End Region

#Region "Watch List Module Test"

        ''' <summary>
        ''' Tests the AddtoWatchList Button and the text Box is loaded
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationAddtoWatchListTextBoxLoadTest()
            InvokeAddtoWatchListAssert()
        End Sub


        <TestMethod()> _
        Public Sub DesktopStockRemovedfromWatchListTextBoxTest()
            InvokeStockRemovedfromWatchListTextBoxAssert()
        End Sub

        ''' <summary>
        ''' Tests the stock added in the TextBox gets added to the Watch List Grid on Clicking the AddtoWatchList Button
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub DesktopApplicationStockAddedinWatchListTextBoxTest()
            InvokeStockAddedinWatchListTextBoxAssert()
        End Sub
#End Region

#Region "private methods"
        Private Shared Function GetDesktopApplicationProcess() As String
            Return ConfigHandler.GetValue("WpfAppProcessName")
        End Function

        Private Shared Function GetDesktopApplication() As String
            Return ConfigHandler.GetValue("WpfAppLocation")
        End Function

        Private Sub InvokePositionSummaryAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryGrid()
        End Sub
        Private Sub InvokeOrderToolBarAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSubmitButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSubmitAllButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertCancelButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertCancelAllButton()
        End Sub
        Private Sub InvokeAddtoWatchListAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertTextBoxBlock()
        End Sub
        Private Sub InvokeStockAddedinWatchListTextBoxAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListRowCount()
        End Sub

        Private Sub InvokeStockRemovedfromWatchListTextBoxAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListRowCount()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockRemovedfromWatchListTextBox()
        End Sub

        Private Sub InvokeDesktopPositionBuySellTabControlsLoad(ByVal [option] As String)
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest([option])

            StockTraderRIAssertion(Of WpfAppLauncher).AssertTermComboBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPriceLimitTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSellRadioButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertBuyRadioButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSharesTextBox()

            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderTypeComboBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandSubmit()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandCancel()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandSubmitAllButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandCancelAllButton()
        End Sub
        Private Sub InvokeAttemptBuySellOrderWithValidData(ByVal [option] As String)
            InvokeDesktopPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptBuySellOrderWithValidData()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithInValidData(ByVal [option] As String)
            InvokeDesktopPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptBuySellOrderWithInValidData()
        End Sub

        Private Sub InvokeAttemptOrderCancelByCancelButton()
            InvokeDesktopPositionBuySellTabControlsLoad("Buy")
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptOrderCancelByCancelButton()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeDesktopPositionBuySellTabLoad()

            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub

        Private Sub InvokeDesktopPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        Private Sub InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellByCancelAllButton()
            InvokeDesktopPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellByCancelAllButton()
        End Sub
#End Region
    End Class

#Else
<DeploymentItem("..\Desktop\StockTraderRI\bin\Release", "WPF")> 
<DeploymentItem(".\StockTraderRI.Tests.AcceptanceTest\bin\Release")>
<TestClass()>
 Public Class StockTraderRIDesktopFixture
        Inherits FixtureBase(Of WpfAppLauncher)
#Region "Additional test attributes"

        ' Use TestInitialize to run code before running each test 
        <TestInitialize()> _
        Public Sub MyTestInitialize()
            Dim currentOutputPath As String = (New System.IO.DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.FullName
            StockTraderRIPage(Of WpfAppLauncher).Window = MyBase.LaunchApplication(currentOutputPath & GetDesktopApplication(), GetDesktopApplicationProcess())(0)
        End Sub

        ' Use TestCleanup to run code after each test has run
        <TestCleanup()> _
        Public Sub MyTestCleanup()
            PageBase(Of WpfAppLauncher).DisposeWindow()
            Dim p As Process = WpfAppLauncher.GetCurrentAppProcess()
            MyBase.UnloadApplication(p)
        End Sub

#End Region

#Region "Test Methods"

#Region "Application Launch Test"
        <TestMethod()> _
        Public Sub DesktopApplicationLoadTest()
            Assert.IsNotNull(StockTraderRIPage(Of WpfAppLauncher).Window, "StockTraderRI is not launched.")
        End Sub
#End Region

#End Region

#Region "Position summary Module Load Test"

        ''' <summary>
        ''' Tests if position summary details are loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryTest()
            InvokePositionSummaryAssert()
        End Sub

        ''' <summary>
        ''' Tests the number of columns from position summary view table. 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryColumnCountTest()

            'For now the test data is hardcoded in resource file. But if the datasource is available it will be read from the datasource
            StockTraderRIAssertion(Of WpfAppLauncher).DesktopAssertPositionSummaryColumnCount()
        End Sub

        ''' <summary>
        ''' Tests the number of rows from position summary view table. 
        ''' </summary>

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryRowCountTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryRowCount()
        End Sub

        ''' <summary>
        ''' Tests the computed value (Market value & Gain Loss %) with the value loaded in the screen
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPositionSummaryDataTest()
            'For each Stock or Symbol take the old value and get the value from Web service and monitor that

            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryValuesForSymbol()
        End Sub

#End Region

#Region "Market Trend Test"
        ''' <summary>
        ''' Tests the Historical Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationMarketTrendTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertHistoricalDataText()
        End Sub

        ''' <summary>
        ''' Tests the Pie Chart Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationPieChartTextLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPieChartTextBlock()
        End Sub
#End Region

#Region "PositionBuySellTab Test"
        <TestMethod()> _
        Public Sub DesktopPositionBuySellTabControlsLoadTest()
            InvokeDesktopPositionBuySellTabControlsLoad("Buy")
        End Sub


        <TestMethod()> _
        Public Sub DesktopAttemptBuyStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Buy")
        End Sub

        <TestMethod()> _
        Public Sub DesktopAttemptBuyStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Buy")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptSellStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptSellStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub DesktopAttemptStockBuySellCancelByCancelButton()
            InvokeAttemptOrderCancelByCancelButton()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        <TestMethod()> _
        Public Sub DesktopProcessMultipleStockBuySellByCancelAllButton()
            InvokeProcessMultipleStockBuySellByCancelAllButton()
        End Sub

#End Region

#Region "WatchList Module Test"

        ''' <summary>
        ''' Tests the Watch List Grid is loaded 
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub DesktopApplicationWatchListGridLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListGrid()
        End Sub


#End Region

#Region "NewsArticle Module Load Test"
        ''' <summary>
        ''' Tests the News Articles Data Block is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationNewsArticleTextLoadTest()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertNewsArticleText()
        End Sub
#End Region

#Region "Watch List Module Test"

        ''' <summary>
        ''' Tests the AddtoWatchList Button and the text Box is loaded
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub DesktopApplicationAddtoWatchListTextBoxLoadTest()
            InvokeAddtoWatchListAssert()
        End Sub


        <TestMethod()> _
        Public Sub DesktopStockRemovedfromWatchListTextBoxTest()
            InvokeStockRemovedfromWatchListTextBoxAssert()
        End Sub

        ''' <summary>
        ''' Tests the stock added in the TextBox gets added to the Watch List Grid on Clicking the AddtoWatchList Button
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub DesktopApplicationStockAddedinWatchListTextBoxTest()
            InvokeStockAddedinWatchListTextBoxAssert()
        End Sub
#End Region

#Region "private methods"
        Private Shared Function GetDesktopApplicationProcess() As String
            Return ConfigHandler.GetValue("WpfAppProcessName")
        End Function

        Private Shared Function GetDesktopApplication() As String
            Return ConfigHandler.GetValue("WpfAppLocation")
        End Function

        Private Sub InvokePositionSummaryAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryGrid()
        End Sub
        Private Sub InvokeOrderToolBarAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSubmitButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSubmitAllButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertCancelButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertCancelAllButton()
        End Sub
        Private Sub InvokeAddtoWatchListAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertTextBoxBlock()
        End Sub
        Private Sub InvokeStockAddedinWatchListTextBoxAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListRowCount()
        End Sub

        Private Sub InvokeStockRemovedfromWatchListTextBoxAssert()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertWatchListRowCount()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertStockRemovedfromWatchListTextBox()
        End Sub

        Private Sub InvokeDesktopPositionBuySellTabControlsLoad(ByVal [option] As String)
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest([option])

            StockTraderRIAssertion(Of WpfAppLauncher).AssertTermComboBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPriceLimitTextBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSellRadioButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertBuyRadioButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertSharesTextBox()

            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderTypeComboBox()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandSubmit()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandCancel()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandSubmitAllButton()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertOrderCommandCancelAllButton()
        End Sub
        Private Sub InvokeAttemptBuySellOrderWithValidData(ByVal [option] As String)
            InvokeDesktopPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptBuySellOrderWithValidData()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithInValidData(ByVal [option] As String)
            InvokeDesktopPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptBuySellOrderWithInValidData()
        End Sub

        Private Sub InvokeAttemptOrderCancelByCancelButton()
            InvokeDesktopPositionBuySellTabControlsLoad("Buy")
            StockTraderRIAssertion(Of WpfAppLauncher).AssertAttemptOrderCancelByCancelButton()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeDesktopPositionBuySellTabLoad()

            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub

        Private Sub InvokeDesktopPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        Private Sub InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellByCancelAllButton()
            InvokeDesktopPositionBuySellTabLoad()
            StockTraderRIAssertion(Of WpfAppLauncher).AssertProcessMultipleStockBuySellByCancelAllButton()
        End Sub
#End Region
    End Class
#End If

End Namespace
