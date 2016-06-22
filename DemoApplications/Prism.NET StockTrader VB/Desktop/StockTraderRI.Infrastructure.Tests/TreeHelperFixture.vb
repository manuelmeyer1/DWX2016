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
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests
    <TestClass()> _
    Public Class TreeHelperFixture
        <TestMethod()> _
        Public Sub ShouldFindDirectAncestor()
            Dim parent As New ContentControl()
            Dim child As New TextBlock()
            parent.Content = child

            Dim foundParent = TreeHelper.FindAncestor(child, Function(d) d Is parent)

            Assert.IsNotNull(foundParent)
            Assert.AreSame(parent, foundParent)
        End Sub

        <TestMethod()> _
        Public Sub ShouldFindIndirectAncestor()
            Dim grandParent As New ContentControl()
            Dim parent As New ContentControl()
            Dim child As New TextBlock()
            grandParent.Content = parent
            parent.Content = child

            Dim foundGrandParent = TreeHelper.FindAncestor(child, Function(d) d Is grandParent)

            Assert.IsNotNull(foundGrandParent)
            Assert.AreSame(grandParent, foundGrandParent)
        End Sub
    End Class
End Namespace
