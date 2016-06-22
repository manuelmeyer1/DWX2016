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
Imports System.Collections
Imports System.ComponentModel
Imports Microsoft.Practices.Prism.Regions
Imports System.Collections.Specialized

Namespace StockTraderRI.Modules.Position.Tests.Mocks
    Public Class MockRegionManager
        Implements IRegionManager
        Private _regions As New MockRegionCollection()

        Public ReadOnly Property Regions() As IRegionCollection Implements IRegionManager.Regions
            Get
                Return _regions
            End Get
        End Property

        Public Function AttachNewRegion(ByVal regionTarget As Object, ByVal regionName As String) As IRegion
            Throw New NotImplementedException()
        End Function

        Public Function CreateRegionManager() As IRegionManager Implements IRegionManager.CreateRegionManager
            Throw New NotImplementedException()
        End Function

        Public Function Navigate(ByVal source As Uri) As Boolean
            Throw New NotImplementedException()
        End Function
    End Class

    Friend Class MockRegionCollection
        Implements IRegionCollection

        Private regions As New Dictionary(Of String, IRegion)()

        Public Function GetEnumerator() As IEnumerator(Of IRegion) Implements IEnumerable(Of IRegion).GetEnumerator
            Throw New NotImplementedException()
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        Default Public ReadOnly Property Item(ByVal regionName As String) As IRegion Implements IRegionCollection.Item
            Get
                Return regions(regionName)
            End Get
        End Property

        Public Sub Add(ByVal region As IRegion) Implements IRegionCollection.Add
            regions(region.Name) = region
        End Sub

        Public Function Remove(ByVal regionName As String) As Boolean Implements IRegionCollection.Remove
            Throw New NotImplementedException()
        End Function

        Public Function ContainsRegionWithName(ByVal regionName As String) As Boolean _
            Implements IRegionCollection.ContainsRegionWithName
            Throw New NotImplementedException()
        End Function

        Public Event CollectionChanged As NotifyCollectionChangedEventHandler _
            Implements INotifyCollectionChanged.CollectionChanged
    End Class

    Public Class MockRegion
        Implements IRegion
        Public AddedViews As New List(Of Object)()

        Private _Name As String

        Public Property Name() As String Implements IRegion.Name
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Function Add(ByVal view As Object) As IRegionManager Implements IRegion.Add
            AddedViews.Add(view)
            Return Nothing
        End Function

        Public Sub Remove(ByVal view As Object) Implements IRegion.Remove
            AddedViews.Remove(view)
        End Sub

        Public ReadOnly Property Views() As IViewsCollection Implements IRegion.Views
            Get
                Return New MockViewsCollection(AddedViews)
            End Get
        End Property

        Public Sub Activate(ByVal view As Object) Implements IRegion.Activate
            SelectedItem = view
        End Sub

        Public Sub Deactivate(ByVal view As Object) Implements IRegion.Deactivate
            Throw New NotImplementedException()
        End Sub

        Public Function Add(ByVal view As Object, ByVal viewName As String) As IRegionManager Implements IRegion.Add
            Add(view)
            Return Nothing
        End Function

        Public Function GetView(ByVal viewName As String) As Object Implements IRegion.GetView
            Return If(AddedViews.Count > 0, AddedViews(0), Nothing)
        End Function

        Public Function Add(ByVal view As Object, ByVal viewName As String, ByVal createRegionManagerScope As Boolean) _
            As IRegionManager Implements IRegion.Add
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

        Private _SelectedItem As Object

        Public Property SelectedItem() As Object
            Get
                Return _SelectedItem
            End Get
            Set(ByVal value As Object)
                _SelectedItem = value
            End Set
        End Property

        Public ReadOnly Property ActiveViews() As IViewsCollection Implements IRegion.ActiveViews
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

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

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

        Private ReadOnly views As IList(Of Object)

        Public Sub New(ByVal views As IList(Of Object))
            Me.views = views
        End Sub

        Public Function Contains(ByVal value As Object) As Boolean Implements IViewsCollection.Contains
            Throw New NotImplementedException()
        End Function

        Public Function GetEnumerator() As IEnumerator(Of Object) Implements IEnumerable(Of Object).GetEnumerator
            Return views.GetEnumerator()
        End Function

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Throw New NotImplementedException()
        End Function

        Public Event CollectionChanged As NotifyCollectionChangedEventHandler _
            Implements INotifyCollectionChanged.CollectionChanged
    End Class
End Namespace
