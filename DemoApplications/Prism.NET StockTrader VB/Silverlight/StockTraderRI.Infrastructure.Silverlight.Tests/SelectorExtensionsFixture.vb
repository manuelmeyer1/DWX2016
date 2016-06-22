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
Imports System.ComponentModel
Imports System.Windows.Controls
Imports System.Windows.Data
Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace StockTraderRI.Infrastructure.Tests
    <TestClass()> _
    Public Class SelectorExtensionsFixture
        <TestMethod()> _
        Public Sub ShouldUpdateModelOnSelectionChange()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim item2 = New MockItem With {.[Property] = 2}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1, item2})

            Dim selector = New ComboBox()
            selector.ItemsSource = itemsSource
            SelectorExtensions.SetSelectedValuePath(selector, "Property")

            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)

            selector.SelectedItem = item1
            Assert.AreEqual(item1.[Property], model.SelectedValueInModel)

            selector.SelectedItem = item2
            Assert.AreEqual(item2.[Property], model.SelectedValueInModel)
        End Sub

        <TestMethod()> _
        Public Sub ShouldInitiallySetSelectedItemInSelectorFromModel()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim item2 = New MockItem With {.[Property] = 2}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1, item2})

            Dim selector = New ComboBox()
            selector.ItemsSource = itemsSource
            SelectorExtensions.SetSelectedValuePath(selector, "Property")

            model.SelectedValueInModel = 2

            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)

            Assert.AreSame(item2, selector.SelectedItem)
        End Sub

        <TestMethod()> _
        Public Sub ShouldUpdateSelectedItemInSelectorWhenPropertyChanges()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim item2 = New MockItem With {.[Property] = 2}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1, item2})

            Dim selector = New ComboBox()
            selector.ItemsSource = itemsSource
            SelectorExtensions.SetSelectedValuePath(selector, "Property")

            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)

            Assert.AreNotSame(item2, selector.SelectedItem)

            model.SelectedValueInModel = 2

            Assert.AreSame(item2, selector.SelectedItem)
        End Sub

        <TestMethod()> _
        Public Sub ShouldNotFailIfItemsSourceIsNotSetBeforeSettingTheBindingToModel()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim item2 = New MockItem With {.[Property] = 2}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1, item2})

            Dim selector = New ComboBox()
            SelectorExtensions.SetSelectedValuePath(selector, "Property")
            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)

            selector.ItemsSource = itemsSource
        End Sub

        <TestMethod()> _
        Public Sub ShouldSetSelectedItemWhenSettingValuePathAfterBindingToModel()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim item2 = New MockItem With {.[Property] = 2}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1, item2})
            Dim selector = New ComboBox()
            selector.ItemsSource = itemsSource
            model.SelectedValueInModel = 2

            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)
            Assert.AreNotSame(item2, selector.SelectedItem)

            SelectorExtensions.SetSelectedValuePath(selector, "Property")

            Assert.AreSame(item2, selector.SelectedItem)
        End Sub

        <TestMethod(), ExpectedException(GetType(InvalidOperationException))> _
        Public Sub InvalidValuePathThrows()
            Dim model = New MockModel()
            Dim item1 = New MockItem With {.[Property] = 1}
            Dim itemsSource = New List(Of MockItem)(New MockItem() {item1})
            Dim selector = New ComboBox()
            selector.ItemsSource = itemsSource

            Dim valueBinding As New Binding("SelectedValueInModel")
            valueBinding.Mode = BindingMode.TwoWay
            valueBinding.Source = model
            selector.SetBinding(SelectorExtensions.SelectedValueProperty, valueBinding)

            SelectorExtensions.SetSelectedValuePath(selector, "InvalidProperty")
        End Sub

        Public Class MockItem
            Private _Property As Integer

            Public Property [Property]() As Integer
                Get
                    Return _Property
                End Get
                Set(ByVal value As Integer)
                    _Property = value
                End Set
            End Property
        End Class

        Public Class MockModel
            Implements INotifyPropertyChanged
            Private _selectedValueInModel As Integer

            Public Property SelectedValueInModel() As Integer
                Get
                    Return Me._selectedValueInModel
                End Get
                Set(ByVal value As Integer)
                    If Me._selectedValueInModel <> value Then
                        Me._selectedValueInModel = value
                        If Me.PropertyChangedEvent IsNot Nothing Then
                            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("SelectedValueInModel"))
                        End If
                    End If
                End Set
            End Property

            Public Event PropertyChanged As PropertyChangedEventHandler _
                Implements INotifyPropertyChanged.PropertyChanged
        End Class
    End Class
End Namespace