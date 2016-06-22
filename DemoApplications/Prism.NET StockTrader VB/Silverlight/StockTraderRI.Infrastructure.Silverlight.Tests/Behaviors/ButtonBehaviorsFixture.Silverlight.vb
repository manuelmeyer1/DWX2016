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
Imports System.Windows.Controls
Imports StockTraderRI.Infrastructure.Behaviors
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows.Controls.Primitives

Namespace StockTraderRI.Infrastructure.Tests.Behaviors
    <TestClass()> _
    Public Class ButtonBehaviorsFixture
        <TestMethod()> _
        Public Sub ShouldCloseParentPopupOnButtonClick()
            Dim parentPopup As New Popup()
            Dim button = New ClickableButton()
            button.SetValue(ButtonBehaviors.CloseAncestorPopupProperty, True)
            parentPopup.Child = button

            parentPopup.IsOpen = True

            button.RaiseClick()

            Assert.IsFalse(parentPopup.IsOpen)
        End Sub

        <TestMethod()> _
        Public Sub ShouldCloseAncestorPopupOnButtonClick()
            Dim ancestorPopup As New Popup()
            Dim button = New ClickableButton()
            button.SetValue(ButtonBehaviors.CloseAncestorPopupProperty, True)
            Dim immediateParent = New ContentControl()
            immediateParent.Content = button
            ancestorPopup.Child = immediateParent

            ancestorPopup.IsOpen = True

            button.RaiseClick()

            Assert.IsFalse(ancestorPopup.IsOpen)
        End Sub

        Friend Class ClickableButton
            Inherits ButtonBase

            Public Sub RaiseClick()
                MyBase.OnClick()
            End Sub
        End Class
    End Class
End Namespace
