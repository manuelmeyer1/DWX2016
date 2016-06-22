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
Imports StockTraderRI.Infrastructure.Behaviors
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Windows
Imports Microsoft.Practices.Prism.Regions
Imports System.Collections.Specialized
Imports Microsoft.Practices.ServiceLocation

Namespace StockTraderRI.Infrastructure.Tests.Behaviors
    <TestClass()> _
    Public Class RegionPopupBehaviorsFixture
        <TestMethod()> _
        Public Sub ShouldCreateRegion()
            Try
                Dim regionManager = New MockRegionManager()
                ServiceLocator.SetLocatorProvider(Function() New MockServiceLocator(Function() regionManager))

                Dim hostControl As FrameworkElement = New MockFrameworkElement()
                RegionPopupBehaviors.RegisterNewPopupRegion(hostControl, "MyPopupRegion")

                Assert.IsTrue(regionManager.MockRegions.Regions.ContainsKey("MyPopupRegion"))
                Assert.IsNotNull(regionManager.MockRegions.Regions("MyPopupRegion"))
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions("MyPopupRegion"), _
                                         GetType(SingleActiveRegion))
                Assert.IsTrue( _
                               regionManager.MockRegions.Regions("MyPopupRegion").Behaviors.ContainsKey( _
                                                                                                          DialogActivationBehavior _
                                                                                                             . _
                                                                                                             BehaviorKey))
#If SILVERLIGHT Then
                Assert.IsInstanceOfType( _
                                         regionManager.MockRegions.Regions("MyPopupRegion").Behaviors( _
                                                                                                        DialogActivationBehavior _
                                                                                                           .BehaviorKey), _
                                         GetType(PopupDialogActivationBehavior))
#Else
                Assert.IsInstanceOfType(regionManager.MockRegions.Regions("MyPopupRegion").Behaviors( _
DialogActivationBehavior.BehaviorKey), GetType(WindowDialogActivationBehavior))
#End If
            Finally
                ServiceLocator.SetLocatorProvider(Function() Nothing)
            End Try
        End Sub

        Friend Class MockFrameworkElement
            Inherits FrameworkElement
        End Class

        Friend Class MockRegionManager
            Implements IRegionManager
            Public MockRegions As New MockRegions()

            Public ReadOnly Property Regions() As IRegionCollection Implements IRegionManager.Regions
                Get
                    Return MockRegions
                End Get
            End Property

#Region "Not implemented members"

            Public Function CreateRegionManager() As IRegionManager Implements IRegionManager.CreateRegionManager
                Throw New NotImplementedException()
            End Function

#End Region

            Public Function Navigate(ByVal source As Uri) As Boolean
                Throw New NotImplementedException()
            End Function
        End Class

        Friend Class MockRegions
            Implements IRegionCollection

            Public Regions As New Dictionary(Of String, IRegion)()

            Default Public ReadOnly Property Item(ByVal regionName As String) As IRegion _
                Implements IRegionCollection.Item
                Get
                    Return Me.Regions(regionName)
                End Get
            End Property

            Public Sub Add(ByVal region As IRegion) Implements IRegionCollection.Add
                Me.Regions.Add(region.Name, region)
            End Sub

#Region "Not implemented members"

            Public Function GetEnumerator() As IEnumerator(Of IRegion) Implements IEnumerable(Of IRegion).GetEnumerator
                Throw New NotImplementedException()
            End Function

            Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
                Throw New NotImplementedException()
            End Function

            Public Function Remove(ByVal regionName As String) As Boolean Implements IRegionCollection.Remove
                Throw New NotImplementedException()
            End Function

            Public Function ContainsRegionWithName(ByVal regionName As String) As Boolean _
                Implements IRegionCollection.ContainsRegionWithName
                Throw New NotImplementedException()
            End Function

#End Region

            Public Event CollectionChanged As NotifyCollectionChangedEventHandler _
                Implements INotifyCollectionChanged.CollectionChanged
        End Class

        Friend Class MockServiceLocator
            Inherits ServiceLocatorImplBase
            Public ResolveMethod As Func(Of Object)

            Public Sub New(ByVal resolveMethod As Func(Of Object))
                Me.ResolveMethod = resolveMethod
            End Sub

            Protected Overrides Function DoGetInstance(ByVal serviceType As Type, ByVal key As String) As Object
                Return Me.ResolveMethod()
            End Function

#Region "Not implemented members"

            Protected Overrides Function DoGetAllInstances(ByVal serviceType As Type) As IEnumerable(Of Object)
                Throw New NotImplementedException()
            End Function

#End Region
        End Class
    End Class
End Namespace
