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

Imports System.Threading
Imports System.IO


Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Text
Imports System.Windows.Automation.Provider

Imports AcceptanceTestLibrary.Common
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Page
Imports StockTraderRI.Tests.AcceptanceTest.TestEntities.Assertion
Imports AcceptanceTestLibrary.ApplicationObserver
Imports AcceptanceTestLibrary.Common.Silverlight
Imports Microsoft.VisualStudio.TestTools.UnitTesting.Web
Imports AcceptanceTestLibrary.ApplicationHelper
Imports System.Collections.Specialized
Imports System.Text.RegularExpressions
Imports AcceptanceTestLibrary.TestEntityBase
Imports System.Reflection


Namespace StockTraderRI.Tests.AcceptanceTest.Silverlight
    ''' <summary>
    ''' Summary description for SilverlightAcceptanceTest
    ''' </summary>
#If DEBUG Then
    <DeploymentItem("..\Silverlight\StockTraderRI\bin\Debug", "Silverlight")>
    <DeploymentItem(".\StockTraderRI.Tests.AcceptanceTest\bin\Debug")>
    <TestClass()>
    Public Class StockTraderRISilverlightFixture
        Inherits FixtureBase(Of SilverlightAppLauncher)
        Private Const BACKTRACKLENGTH As Integer = 5

#Region "Additional test attributes"

        ' Use TestInitialize to run code before running each test 
        <TestInitialize()> _
        Public Sub MyTestInitialize()
            Dim currentOutputPath As String = (New System.IO.DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.FullName
            StockTraderRIPage(Of SilverlightAppLauncher).Window = MyBase.LaunchApplication(currentOutputPath & GetSilverlightApplication(), GetBrowserTitle())(0)
        End Sub

        ' Use TestCleanup to run code after each test has run
        <TestCleanup()> _
        Public Sub MyTestCleanup()
            PageBase(Of SilverlightAppLauncher).DisposeWindow()
            SilverlightAppLauncher.UnloadBrowser(GetBrowserTitle())
        End Sub

#End Region

#Region "Application Launch Test"

        ''' <summary>
        ''' Tests if RI is launched in silverlight 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationLoadTest()
            Assert.IsNotNull(StockTraderRIPage(Of SilverlightAppLauncher).Window, "StockTraderRI is not launched.")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightNewsArticleTextLoadTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertNewsArticleText()
        End Sub

#End Region

#Region "Position summary Module Load Test"
        ''' <summary>
        ''' Tests if position summary details are loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryTest()
            InvokePositionSummaryAssert()
        End Sub

        ''' <summary>
        ''' Tests the number of columns from position summary view table. 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryColumnCountTest()
            'For now the test data is hardcoded in resource file. But if the datasource is available it will be read from the datasource
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryColumnCount()
        End Sub

        ''' <summary>
        ''' Tests the number of rows from position summary view table. 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryRowCountTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryRowCount()
        End Sub

        ''' <summary>
        ''' Tests the computed value (Market value & Gain Loss %) with the value loaded in the screen
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryDataTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryValuesForSymbol()
        End Sub
#End Region

#Region "Market Trend Data Test"
        ''' <summary>
        ''' Tests if historical data textblock is loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationMarketTrendTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertHistoricalDataText()
        End Sub

        ''' <summary>
        ''' Tests if historical data textblock is loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPieChartTextBlockTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPieChartTextBlock()
        End Sub


#End Region

#Region "WatchList Module Test"

        ''' <summary>
        ''' Tests the Watch List Grid is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub SilverLightWatchListGridLoadTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListGrid()
        End Sub
#End Region

#Region "Watch List Module Test"

        ''' <summary>
        ''' Tests the AddtoWatchList Button and the text Box is loaded
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub SilverLightApplicationAddtoWatchListTextBoxLoadTest()
            InvokeAddtoWatchListAssert()
        End Sub

        <TestMethod()> _
        Public Sub SilverLightStockAddedinWatchListTextBoxTest()
            InvokeStockAddedinWatchListTextBoxAssert()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightStockRemovedfromWatchListTextBoxTest()
            InvokeStockRemovedfromWatchListTextBoxAssert()
        End Sub
#End Region

#Region "PositionBuySellTab Test"
        <TestMethod()> _
        Public Sub SilverLightPositionBuySellTabControlsLoadTest()
            InvokeSilverLightPositionBuySellTabControlsLoad("Buy")
        End Sub


        <TestMethod()> _
        Public Sub SilverLightAttemptBuyStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Buy")
        End Sub

        <TestMethod()> _
        Public Sub SilverLightAttemptBuyStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Buy")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptSellStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptSellStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptStockBuySellCancelByCancelButton()
            InvokeAttemptOrderCancelByCancelButton()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellByCancelAllButton()
            InvokeProcessMultipleStockBuySellByCancelAllButton()
        End Sub

#End Region

#Region "private Methods"

        Private Shared Function GetSilverlightApplication() As String
            Return ConfigHandler.GetValue("SilverlightAppLocation")
        End Function

        Private Shared Function GetSilverlightApplicationPath(ByVal backTrackLength As Integer) As String
            Dim currentDirectory As String = Directory.GetCurrentDirectory()
            If (Not String.IsNullOrEmpty(currentDirectory)) AndAlso Directory.Exists(currentDirectory) Then
                For iIndex As Integer = 0 To backTrackLength - 1
                    currentDirectory = Directory.GetParent(currentDirectory).ToString()
                Next iIndex
            End If
            Return currentDirectory & GetSilverlightApplication()
        End Function

        Private Shared Function GetBrowserTitle() As String
            Return New ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("SilverlightAppTitle")
        End Function

        Private Sub InvokePositionSummaryAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryGrid()
        End Sub

        Private Sub InvokeOrderToolBarAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSubmitButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSubmitAllButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertCancelButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertCancelAllButton()
        End Sub

        Private Sub InvokeAddtoWatchListAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertTextBoxBlock()

        End Sub

        Private Sub InvokeStockAddedinWatchListTextBoxAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListRowCount()
        End Sub
        Private Sub InvokeStockRemovedfromWatchListTextBoxAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListRowCount()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockRemovedfromWatchListTextBox()
        End Sub

        Private Sub InvokeSilverLightPositionBuySellTabControlsLoad(ByVal [option] As String)
            'StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionBuyButtonClickTest([option])

            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertTermComboBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPriceLimitTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSellRadioButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertBuyRadioButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSharesTextBox()

            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderTypeComboBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandSubmit()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandCancel()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandSubmitAllButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandCancelAllButton()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithValidData(ByVal [option] As String)
            InvokeSilverLightPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptBuySellOrderWithValidData()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithInValidData(ByVal [option] As String)
            InvokeSilverLightPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptBuySellOrderWithInValidData()
        End Sub

        Private Sub InvokeAttemptOrderCancelByCancelButton()
            InvokeSilverLightPositionBuySellTabControlsLoad("Buy")
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptOrderCancelByCancelButton()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        Private Sub InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellByCancelAllButton()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellByCancelAllButton()
        End Sub
#End Region
    End Class

#Else
<DeploymentItem("..\Silverlight\StockTraderRI\bin\Release", "Silverlight")>
<DeploymentItem(".\StockTraderRI.Tests.AcceptanceTest\bin\Release")>
<TestClass()> 
    Public Class StockTraderRISilverlightFixture
        Inherits FixtureBase(Of SilverlightAppLauncher)
        Private Const BACKTRACKLENGTH As Integer = 5

#Region "Additional test attributes"

        ' Use TestInitialize to run code before running each test 
        <TestInitialize()> _
        Public Sub MyTestInitialize()
            Dim currentOutputPath As String = (New System.IO.DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).Parent.FullName
            StockTraderRIPage(Of SilverlightAppLauncher).Window = MyBase.LaunchApplication(currentOutputPath & GetSilverlightApplication(), GetBrowserTitle())(0)
        End Sub

        ' Use TestCleanup to run code after each test has run
        <TestCleanup()> _
        Public Sub MyTestCleanup()
            PageBase(Of SilverlightAppLauncher).DisposeWindow()
            SilverlightAppLauncher.UnloadBrowser(GetBrowserTitle())
        End Sub

#End Region

#Region "Application Launch Test"

        ''' <summary>
        ''' Tests if RI is launched in silverlight 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationLoadTest()
            Assert.IsNotNull(StockTraderRIPage(Of SilverlightAppLauncher).Window, "StockTraderRI is not launched.")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightNewsArticleTextLoadTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertNewsArticleText()
        End Sub

#End Region

#Region "Position summary Module Load Test"
        ''' <summary>
        ''' Tests if position summary details are loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryTest()
            InvokePositionSummaryAssert()
        End Sub

        ''' <summary>
        ''' Tests the number of columns from position summary view table. 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryColumnCountTest()
            'For now the test data is hardcoded in resource file. But if the datasource is available it will be read from the datasource
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryColumnCount()
        End Sub

        ''' <summary>
        ''' Tests the number of rows from position summary view table. 
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryRowCountTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryRowCount()
        End Sub

        ''' <summary>
        ''' Tests the computed value (Market value & Gain Loss %) with the value loaded in the screen
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPositionSummaryDataTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryValuesForSymbol()
        End Sub
#End Region

#Region "Market Trend Data Test"
        ''' <summary>
        ''' Tests if historical data textblock is loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationMarketTrendTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertHistoricalDataText()
        End Sub

        ''' <summary>
        ''' Tests if historical data textblock is loaded.
        ''' </summary>
        <TestMethod()> _
        Public Sub SilverlightApplicationPieChartTextBlockTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPieChartTextBlock()
        End Sub


#End Region

#Region "WatchList Module Test"

        ''' <summary>
        ''' Tests the Watch List Grid is loaded 
        ''' </summary>
        ''' 

        <TestMethod()> _
        Public Sub SilverLightWatchListGridLoadTest()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListGrid()
        End Sub
#End Region

#Region "Watch List Module Test"

        ''' <summary>
        ''' Tests the AddtoWatchList Button and the text Box is loaded
        ''' </summary>
        ''' 
        <TestMethod()> _
        Public Sub SilverLightApplicationAddtoWatchListTextBoxLoadTest()
            InvokeAddtoWatchListAssert()
        End Sub

        <TestMethod()> _
        Public Sub SilverLightStockAddedinWatchListTextBoxTest()
            InvokeStockAddedinWatchListTextBoxAssert()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightStockRemovedfromWatchListTextBoxTest()
            InvokeStockRemovedfromWatchListTextBoxAssert()
        End Sub
#End Region

#Region "PositionBuySellTab Test"
        <TestMethod()> _
        Public Sub SilverLightPositionBuySellTabControlsLoadTest()
            InvokeSilverLightPositionBuySellTabControlsLoad("Buy")
        End Sub


        <TestMethod()> _
        Public Sub SilverLightAttemptBuyStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Buy")
        End Sub

        <TestMethod()> _
        Public Sub SilverLightAttemptBuyStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Buy")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptSellStockWithValidData()
            InvokeAttemptBuySellOrderWithValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptSellStockWithInValidData()
            InvokeAttemptBuySellOrderWithInValidData("Sell")
        End Sub
        <TestMethod()> _
        Public Sub SilverLightAttemptStockBuySellCancelByCancelButton()
            InvokeAttemptOrderCancelByCancelButton()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        <TestMethod()> _
        Public Sub SilverLightProcessMultipleStockBuySellByCancelAllButton()
            InvokeProcessMultipleStockBuySellByCancelAllButton()
        End Sub

#End Region

#Region "private Methods"

        Private Shared Function GetSilverlightApplication() As String
            Return ConfigHandler.GetValue("SilverlightAppLocation")
        End Function

        Private Shared Function GetSilverlightApplicationPath(ByVal backTrackLength As Integer) As String
            Dim currentDirectory As String = Directory.GetCurrentDirectory()
            If (Not String.IsNullOrEmpty(currentDirectory)) AndAlso Directory.Exists(currentDirectory) Then
                For iIndex As Integer = 0 To backTrackLength - 1
                    currentDirectory = Directory.GetParent(currentDirectory).ToString()
                Next iIndex
            End If
            Return currentDirectory & GetSilverlightApplication()
        End Function

        Private Shared Function GetBrowserTitle() As String
            Return New ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile")).GetValue("SilverlightAppTitle")
        End Function

        Private Sub InvokePositionSummaryAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryGrid()
        End Sub

        Private Sub InvokeOrderToolBarAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSubmitButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSubmitAllButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertCancelButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertCancelAllButton()
        End Sub

        Private Sub InvokeAddtoWatchListAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertTextBoxBlock()

        End Sub

        Private Sub InvokeStockAddedinWatchListTextBoxAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListRowCount()
        End Sub
        Private Sub InvokeStockRemovedfromWatchListTextBoxAssert()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockAddedinWatchListTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertWatchListRowCount()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertStockRemovedfromWatchListTextBox()
        End Sub

        Private Sub InvokeSilverLightPositionBuySellTabControlsLoad(ByVal [option] As String)
            'StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionBuyButtonClickTest([option])

            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertTermComboBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPriceLimitTextBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSellRadioButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertBuyRadioButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertSharesTextBox()

            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderTypeComboBox()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandSubmit()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandCancel()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandSubmitAllButton()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertOrderCommandCancelAllButton()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithValidData(ByVal [option] As String)
            InvokeSilverLightPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptBuySellOrderWithValidData()
        End Sub

        Private Sub InvokeAttemptBuySellOrderWithInValidData(ByVal [option] As String)
            InvokeSilverLightPositionBuySellTabControlsLoad([option])
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptBuySellOrderWithInValidData()
        End Sub

        Private Sub InvokeAttemptOrderCancelByCancelButton()
            InvokeSilverLightPositionBuySellTabControlsLoad("Buy")
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertAttemptOrderCancelByCancelButton()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforValidData()
        End Sub

        Private Sub InvokeProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellBySubmitAllButtonforInValidData()
        End Sub
        Private Sub InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionSummaryTab()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertPositionBuyButtonClickTest("Buy")
        End Sub


        Private Sub InvokeProcessMultipleStockBuySellByCancelAllButton()
            InvokeSilverLightPositionBuySellTabLoad()
            StockTraderRIAssertion(Of SilverlightAppLauncher).AssertProcessMultipleStockBuySellByCancelAllButton()
        End Sub
#End Region
    End Class

#End If
End Namespace
