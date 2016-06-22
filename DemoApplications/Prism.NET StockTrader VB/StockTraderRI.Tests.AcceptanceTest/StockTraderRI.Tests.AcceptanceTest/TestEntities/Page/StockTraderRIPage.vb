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
Imports AcceptanceTestLibrary.TestEntityBase
Imports AcceptanceTestLibrary.ApplicationHelper
Imports System.Threading
Imports AcceptanceTestLibrary.UIAWrapper

Namespace StockTraderRI.Tests.AcceptanceTest.TestEntities.Page
    Public NotInheritable Class StockTraderRIPage(Of TApp As {AppLauncherBase, New})

        Private Sub New()
        End Sub
        Public Shared Property Window() As AutomationElement
            Get
                Return PageBase(Of TApp).Window
            End Get
            Set(ByVal value As AutomationElement)
                PageBase(Of TApp).Window = value
            End Set
        End Property

        Public Shared ReadOnly Property PositionSummaryTab() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("PositionSummaryTab")
            End Get
        End Property
        Public Shared ReadOnly Property PositionSummaryGrid() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("PositionSummaryGrid")
            End Get
        End Property
        Public Shared ReadOnly Property PositionBuySellTab() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("PositionBuySellTab")
            End Get
        End Property
        Public Shared ReadOnly Property ActionsBuyButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("ActionsBuyButton")
            End Get
        End Property
        Public Shared ReadOnly Property ActionsSellButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("ActionsSellButton")
            End Get
        End Property
        Public Shared ReadOnly Property HistoricalDataText() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("HistoricalDataTextBlock")
            End Get
        End Property
        Public Shared ReadOnly Property WatchListGrid() As AutomationElement
            Get
                Dim ae As AutomationElement = StockTraderRIPage(Of TApp).PositionSummaryTab
                Dim clickablePoint As New System.Windows.Point(CInt(Fix(Math.Floor(ae.Current.BoundingRectangle.X + 150))), CInt(Fix(Math.Floor(ae.Current.BoundingRectangle.Y + 15))))
                System.Windows.Forms.Cursor.Position = New System.Drawing.Point(CInt(Fix(clickablePoint.X)), CInt(Fix(clickablePoint.Y)))
                Thread.Sleep(1000)
                MouseEvents.Click()
                System.Threading.Thread.Sleep(3000)
                Return PageBase(Of TApp).FindControlByAutomationId("WatchListGrid")
            End Get
        End Property
        Public Shared ReadOnly Property ActionsRemoveButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("ActionsRemoveButton")
            End Get
        End Property
        Public Shared ReadOnly Property NewsArticleText() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("NewsArticleText")
            End Get
        End Property
        Public Shared ReadOnly Property PieChartTextBlock() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("PieChartTextBlock")
            End Get
        End Property
        Public Shared ReadOnly Property SubmitButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("SubmitButton")
            End Get
        End Property
        Public Shared ReadOnly Property SubmitAllButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("SubmitAllButton")
            End Get
        End Property
        Public Shared ReadOnly Property CancelButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("CancelButton")
            End Get
        End Property
        Public Shared ReadOnly Property CancelAllButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("CancelAllButton")
            End Get
        End Property
        Public Shared ReadOnly Property AddToWatchListButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("AddToWatchListButton")
            End Get
        End Property
        Public Shared ReadOnly Property TextBoxBlock() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("TextBoxBlock")
            End Get
        End Property
        Public Shared ReadOnly Property TermComboBox() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("TermComboBox")
            End Get
        End Property
        Public Shared ReadOnly Property PriceLimitTextBox() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("PriceLimitTextBox")
            End Get
        End Property
        Public Shared ReadOnly Property SellRadioButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("SellRadioButton")
            End Get
        End Property
        Public Shared ReadOnly Property BuyRadioButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("BuyRadioButton")
            End Get
        End Property
        Public Shared ReadOnly Property SharesTextBox() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("SharesTextBox")
            End Get
        End Property
        Public Shared ReadOnly Property SymbolTextBox() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("SymbolTextBox")
            End Get
        End Property
        Public Shared ReadOnly Property OrderTypeComboBox() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByAutomationId("OrderTypeComboBox")
            End Get
        End Property
        Public Shared ReadOnly Property OrderCommandSubmit() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByContent("Submit")
            End Get
        End Property
        Public Shared ReadOnly Property OrderCommandCancel() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByContent("Cancel")
            End Get
        End Property
        Public Shared ReadOnly Property OrderCommandSubmitAllButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByContent("SubmitAll")
            End Get
        End Property
        Public Shared ReadOnly Property OrderCommandCancelAllButton() As AutomationElement
            Get
                Return PageBase(Of TApp).FindControlByContent("CancelAll")
            End Get
        End Property


    End Class
End Namespace
