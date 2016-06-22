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
Imports System.Collections
Imports System.Windows.Controls
Imports Microsoft.Practices.Prism.Regions
Imports System.Collections.Specialized
Imports StockTraderRI.Infrastructure.Behaviors
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows

Namespace StockTraderRI.Infrastructure.Tests.Behaviors
    <TestClass()> _
    Public Class DialogActivationBehaviorFixture
        <TestMethod()> _
        Public Sub ShouldCreateWindowOnViewActivation()
            Dim parentWindow = New MockDependencyObject()
            Dim region = New MockRegion()
            Dim view = New UserControl()
            Dim behavior = New TestableDialogActivationBehavior()
            behavior.HostControl = parentWindow
            behavior.Region = region
            behavior.Attach()

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view)

            Assert.IsNotNull(behavior.CreatedWindow)
            Assert.IsTrue(behavior.CreatedWindow.ShowCalled)
            Assert.AreSame(view, behavior.CreatedWindow.Content)
            Assert.AreSame(parentWindow, behavior.CreatedWindow.Owner)
        End Sub

        <TestMethod()> _
        Public Sub ShouldCloseWindowOnViewDeactivation()
            Dim region = New MockRegion()
            Dim view = New UserControl()
            Dim behavior = New TestableDialogActivationBehavior()
            behavior.HostControl = New MockDependencyObject()
            behavior.Region = region
            behavior.Attach()

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view)
            Assert.IsNotNull(behavior.CreatedWindow)

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Remove, view)

            Assert.IsTrue(behavior.CreatedWindow.CloseCalled)
        End Sub

        <TestMethod()> _
        Public Sub ShouldDeactivateViewWhenClosed()
            Dim view = New UserControl()
            Dim region = New MockRegion()
            Dim behavior = New TestableDialogActivationBehavior()
            behavior.HostControl = New MockDependencyObject()
            behavior.Region = region
            behavior.Attach()

            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view)

            behavior.CreatedWindow.InvokeClosed()

            Assert.IsTrue(region.DeactivateCalled)
        End Sub

        <TestMethod()> _
        Public Sub ShouldAlwaysShowOnlyLastActiveView()
            Dim region = New MockRegion()
            Dim behavior = New TestableDialogActivationBehavior()
            behavior.HostControl = New MockDependencyObject()
            behavior.Region = region
            behavior.Attach()

            Dim view1 = New UserControl()
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view1)
            Assert.AreSame(view1, behavior.CreatedWindow.Content)

            Dim view2 = New UserControl()
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view2)
            Assert.AreSame(view2, behavior.CreatedWindow.Content)
        End Sub

        <TestMethod()> _
        Public Sub ShouldSetStyleToRegionWindow()
            Dim parentWindow = New MockDependencyObject()
            Dim region = New MockRegion()
            Dim behavior = New TestableDialogActivationBehavior()
            behavior.HostControl = parentWindow
            behavior.Region = region

            Dim regionStyle = New Style()
            parentWindow.SetValue(RegionPopupBehaviors.ContainerWindowStyleProperty, regionStyle)

            behavior.Attach()
            Dim view = New UserControl()
            region.MockActiveViews.TriggerNotifyCollectionChangedEvent(NotifyCollectionChangedAction.Add, view)

            Assert.AreEqual(regionStyle, behavior.CreatedWindow.Style)
        End Sub

        Private Class TestableDialogActivationBehavior
            Inherits DialogActivationBehavior
            Public CreatedWindow As MockWindow

            Protected Overrides Function CreateWindow() As IWindow
                If CreatedWindow Is Nothing Then
                    CreatedWindow = New MockWindow()
                End If

                Return CreatedWindow
            End Function
        End Class

        Private Class MockWindow
            Implements IWindow
            Public ShowCalled As Boolean

            Public CloseCalled As Boolean

            Public Event Closed As EventHandler Implements IWindow.Closed

            Private _Content As Object

            Public Property Content() As Object Implements IWindow.Content
                Get
                    Return _Content
                End Get
                Set(ByVal value As Object)
                    _Content = value
                End Set
            End Property

            Private _Owner As Object

            Public Property Owner() As Object Implements IWindow.Owner
                Get
                    Return _Owner
                End Get
                Set(ByVal value As Object)
                    _Owner = value
                End Set
            End Property

            Private _Style As Style

            Public Property Style() As Style Implements IWindow.Style
                Get
                    Return _Style
                End Get
                Set(ByVal value As Style)
                    _Style = value
                End Set
            End Property

            Public Sub Show() Implements IWindow.Show
                ShowCalled = True
            End Sub

            Public Sub Close() Implements IWindow.Close
                CloseCalled = True
            End Sub

            Public Sub InvokeClosed()
                Dim closedHandler As EventHandler = ClosedEvent
                If closedHandler IsNot Nothing Then
                    closedHandler(Me, Nothing)
                End If
            End Sub
        End Class

        Public Class MockDependencyObject
            Inherits DependencyObject
        End Class

        Friend Class MockRegion
            Implements IRegion
            Public MockActiveViews As MockViewsCollection
            Public DeactivateCalled As Boolean

            Public ReadOnly Property ActiveViews() As IViewsCollection Implements IRegion.ActiveViews
                Get
                    Return MockActiveViews
                End Get
            End Property

            Public Sub New()
                Me.MockActiveViews = New MockViewsCollection()
            End Sub

            Public Sub Deactivate(ByVal view As Object) Implements IRegion.Deactivate
                DeactivateCalled = True
            End Sub

#Region "Not Implemented members"

            Public Event PropertyChanged As PropertyChangedEventHandler _
                Implements INotifyPropertyChanged.PropertyChanged

            Public ReadOnly Property Views() As IViewsCollection Implements IRegion.Views
                Get
                    Throw New NotImplementedException()
                End Get
            End Property

            Public Property Context() As Object Implements IRegion.Context
                Get
                    Throw New NotImplementedException()
                End Get
                Set(ByVal value As Object)
                    Throw New NotImplementedException()
                End Set
            End Property

            Public Property Name() As String Implements IRegion.Name
                Get
                    Throw New NotImplementedException()
                End Get
                Set(ByVal value As String)
                    Throw New NotImplementedException()
                End Set
            End Property

            Public Function Add(ByVal view As Object) As IRegionManager Implements IRegion.Add
                Throw New NotImplementedException()
            End Function

            Public Function Add(ByVal view As Object, ByVal viewName As String) As IRegionManager _
                Implements IRegion.Add
                Throw New NotImplementedException()
            End Function

            Public Function Add(ByVal view As Object, ByVal viewName As String, _
                                 ByVal createRegionManagerScope As Boolean) As IRegionManager Implements IRegion.Add
                Throw New NotImplementedException()
            End Function

            Public Sub Remove(ByVal view As Object) Implements IRegion.Remove
                Throw New NotImplementedException()
            End Sub

            Public Sub Activate(ByVal view As Object) Implements IRegion.Activate
                Throw New NotImplementedException()
            End Sub

            Public Function GetView(ByVal viewName As String) As Object Implements IRegion.GetView
                Throw New NotImplementedException()
            End Function

            Public Property RegionManager() As IRegionManager Implements IRegion.RegionManager
                Get
                    Throw New NotImplementedException()
                End Get
                Set(ByVal value As IRegionManager)
                    Throw New NotImplementedException()
                End Set
            End Property

            Public ReadOnly Property Behaviors() As IRegionBehaviorCollection Implements IRegion.Behaviors
                Get
                    Throw New NotImplementedException()
                End Get
            End Property

#End Region

            Public Sub RequestNavigate(ByVal source As Uri, ByVal navigationCallback As Action(Of NavigationResult)) _
                Implements INavigateAsync.RequestNavigate
                Throw New NotImplementedException()
            End Sub

            Public Property NavigationService() As IRegionNavigationService Implements IRegion.NavigationService
                Get
                    Throw New NotImplementedException()
                End Get
                Set(ByVal value As IRegionNavigationService)
                    Throw New NotImplementedException()
                End Set
            End Property

            Public Property SortComparison() As Comparison(Of Object) Implements IRegion.SortComparison
                Get
                    Throw New NotImplementedException()
                End Get
                Set(ByVal value As Comparison(Of Object))
                    Throw New NotImplementedException()
                End Set
            End Property
        End Class

        Friend Class MockViewsCollection
            Implements IViewsCollection

            Public Event CollectionChanged As NotifyCollectionChangedEventHandler _
                Implements INotifyCollectionChanged.CollectionChanged

            Public Sub TriggerNotifyCollectionChangedEvent(ByVal action As NotifyCollectionChangedAction, _
                                                            ByVal view As Object)
                Dim handler As NotifyCollectionChangedEventHandler = CollectionChangedEvent
                If handler IsNot Nothing Then
                    Dim args As New NotifyCollectionChangedEventArgs(action, view, 0)
                    handler(Me, args)
                End If
            End Sub

#Region "Not Implemented members"

            Public Function GetEnumerator() As IEnumerator(Of Object) Implements IEnumerable(Of Object).GetEnumerator
                Throw New NotImplementedException()
            End Function

            Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                Return GetEnumerator()
            End Function

            Public Function Contains(ByVal value As Object) As Boolean Implements IViewsCollection.Contains
                Throw New NotImplementedException()
            End Function

#End Region
        End Class
    End Class
End Namespace
