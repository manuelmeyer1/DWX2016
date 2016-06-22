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
Imports Microsoft.Practices.Prism.Regions.Behaviors
Imports Microsoft.Practices.Prism.Regions
Imports System.Windows
Imports System.Collections.Specialized

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines a behavior that creates a Dialog to display the active view of the target <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.
    ''' </summary>
    Public MustInherit Class DialogActivationBehavior
        Inherits RegionBehavior
        Implements IHostAwareRegionBehavior

        ''' <summary>
        ''' The key of this behavior
        ''' </summary>
        Public Const BehaviorKey As String = "DialogActivation"

        Private contentDialog As IWindow

        ''' <summary>
        ''' Gets or sets the that the is attached to.
        ''' </summary>
        ''' <remarks>A  that the is attached to.
        ''' This is usually a that is part of the tree.</remarks>
        Private _HostControl As DependencyObject

        Public Property HostControl() As DependencyObject Implements IHostAwareRegionBehavior.HostControl
            Get
                Return _HostControl
            End Get
            Set(ByVal value As DependencyObject)
                _HostControl = value
            End Set
        End Property

        ''' <summary>
        ''' Performs the logic after the behavior has been attached.
        ''' </summary>
        Protected Overrides Sub OnAttach()
            AddHandler Me.Region.ActiveViews.CollectionChanged, AddressOf ActiveViews_CollectionChanged
        End Sub

        ''' <summary>
        ''' Override this method to create an instance of the <see cref="IWindow"/> that 
        ''' will be shown when a view is activated.
        ''' </summary>
        ''' <returns>
        ''' An instance of <see cref="IWindow"/> that will be shown when a 
        ''' view is activated on the target <see cref="IRegion"/>.
        ''' </returns>
        Protected MustOverride Function CreateWindow() As IWindow

        Private Sub ActiveViews_CollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
            If e.Action = NotifyCollectionChangedAction.Add Then
                Me.CloseContentDialog()
                Me.PrepareContentDialog(e.NewItems(0))
            ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
                Me.CloseContentDialog()
            End If
        End Sub

        Private Function GetStyleForView() As Style
            Return TryCast(Me.HostControl.GetValue(RegionPopupBehaviors.ContainerWindowStyleProperty), Style)
        End Function

        Private Sub PrepareContentDialog(ByVal view As Object)
            Me.contentDialog = Me.CreateWindow()
            Me.contentDialog.Content = view
            Me.contentDialog.Owner = Me.HostControl
            AddHandler Me.contentDialog.Closed, AddressOf ContentDialogClosed
            Me.contentDialog.Style = Me.GetStyleForView()
            Me.contentDialog.Show()
        End Sub

        Private Sub CloseContentDialog()
            If Me.contentDialog IsNot Nothing Then
                RemoveHandler Me.contentDialog.Closed, AddressOf ContentDialogClosed
                Me.contentDialog.Close()
                Me.contentDialog.Content = Nothing
                Me.contentDialog.Owner = Nothing
            End If
        End Sub

        Private Sub ContentDialogClosed(ByVal sender As Object, ByVal e As EventArgs)
            Me.Region.Deactivate(Me.contentDialog.Content)
            Me.CloseContentDialog()
        End Sub
    End Class
End Namespace
