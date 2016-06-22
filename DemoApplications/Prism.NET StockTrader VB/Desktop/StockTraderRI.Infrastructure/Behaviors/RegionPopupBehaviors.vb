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
Imports System.Windows
Imports System.ComponentModel
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.ServiceLocation

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Declares the Attached Properties and Behaviors for implementing Popup regions.
    ''' </summary>
    ''' <remarks>
    ''' Although the fastest way is to create a RegionAdapter for a Window and register it with the RegionAdapterMappings,
    ''' this would be conceptually incorrect because we want to create a new popup window everytime a view is added 
    ''' (instead of having a Window as a host control and replacing its contents everytime Views are added, as other adapters do).
    ''' This is why we have a different class for this behavior, instead of reusing the <see cref="Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/> attached property.
    ''' </remarks>
    Public NotInheritable Class RegionPopupBehaviors
        ''' <summary>
        ''' The name of the Popup <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.
        ''' </summary>
        Public Shared ReadOnly _
            CreatePopupRegionWithNameProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("CreatePopupRegionWithName", GetType(String), _
                                                     GetType(RegionPopupBehaviors), _
                                                     New PropertyMetadata( _
                                                                           AddressOf _
                                                                              CreatePopupRegionWithNamePropertyChanged))

        ''' <summary>
        ''' The <see cref="Style"/> to set to the Popup.
        ''' </summary>
        Public Shared ReadOnly _
            ContainerWindowStyleProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ContainerWindowStyle", GetType(Style), _
                                                     GetType(RegionPopupBehaviors), Nothing)

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the name of the Popup <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.
        ''' </summary>
        ''' <param name="owner">Owner of the Popup.</param>
        ''' <returns>The name of the Popup <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.</returns>
        Public Shared Function GetCreatePopupRegionWithName(ByVal owner As DependencyObject) As String
            Return TryCast(owner.GetValue(CreatePopupRegionWithNameProperty), String)
        End Function

        ''' <summary>
        ''' Sets the name of the Popup <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.
        ''' </summary>
        ''' <param name="owner">Owner of the Popup.</param>
        ''' <param name="value">Name of the Popup <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.</param>
        Public Shared Sub SetCreatePopupRegionWithName(ByVal owner As DependencyObject, ByVal value As String)
            owner.SetValue(CreatePopupRegionWithNameProperty, value)
        End Sub

        ''' <summary>
        ''' Gets the <see cref="Style"/> for the Popup.
        ''' </summary>
        ''' <param name="owner">Owner of the Popup.</param>
        ''' <returns>The <see cref="Style"/> for the Popup.</returns>
        Public Shared Function GetContainerWindowStyle(ByVal owner As DependencyObject) As Style
            Return TryCast(owner.GetValue(ContainerWindowStyleProperty), Style)
        End Function

        ''' <summary>
        ''' Sets the <see cref="Style"/> for the Popup.
        ''' </summary>
        ''' <param name="owner">Owner of the Popup.</param>
        ''' <param name="style"><see cref="Style"/> for the Popup.</param>
        Public Shared Sub SetContainerWindowStyle(ByVal owner As DependencyObject, ByVal style As Style)
            owner.SetValue(ContainerWindowStyleProperty, style)
        End Sub

        ''' <summary>
        ''' Creates a new <see cref="Microsoft.Practices.Prism.Regions.IRegion"/> and registers it in the default <see cref="Microsoft.Practices.Prism.Regions.IRegionManager"/>
        ''' attaching to it a <see cref="DialogActivationBehavior"/> behavior.
        ''' </summary>
        ''' <param name="owner">The owner of the Popup.</param>
        ''' <param name="regionName">The name of the <see cref="Microsoft.Practices.Prism.Regions.IRegion"/>.</param>
        ''' <remarks>
        ''' This method would typically not be called directly, instead the behavior 
        ''' should be set through the Attached Property <see cref="CreatePopupRegionWithNameProperty"/>.
        ''' </remarks>
        Public Shared Sub RegisterNewPopupRegion(ByVal owner As DependencyObject, ByVal regionName As String)
            ' Creates a new region and registers it in the default region manager.
            ' Another option if you need the complete infrastructure with the default region behaviors
            ' is to extend DelayedRegionCreationBehavior overriding the CreateRegion method and create an 
            ' instance of it that will be in charge of registering the Region once a RegionManager is
            ' set as an attached property in the Visual Tree.
            Dim regionManager As IRegionManager = ServiceLocator.Current.GetInstance(Of IRegionManager)()
            If regionManager IsNot Nothing Then
                Dim region As IRegion = New SingleActiveRegion()
                Dim behavior As DialogActivationBehavior
#If SILVERLIGHT Then
                behavior = New PopupDialogActivationBehavior()
#Else
                behavior = New WindowDialogActivationBehavior()
#End If
                behavior.HostControl = owner

                region.Behaviors.Add(DialogActivationBehavior.BehaviorKey, behavior)
                regionManager.Regions.Add(regionName, region)
            End If
        End Sub

        Private Shared Sub CreatePopupRegionWithNamePropertyChanged(ByVal hostControl As DependencyObject, _
                                                                     ByVal e As DependencyPropertyChangedEventArgs)
            If IsInDesignMode(hostControl) Then
                Return
            End If

            RegisterNewPopupRegion(hostControl, TryCast(e.NewValue, String))
        End Sub

        Private Shared Function IsInDesignMode(ByVal element As DependencyObject) As Boolean
            ' Due to a known issue in Cider, GetIsInDesignMode attached property value is not enough to know if it's in design mode.
            Return _
                DesignerProperties.GetIsInDesignMode(element) OrElse Application.Current Is Nothing OrElse _
                Application.Current.GetType() Is GetType(Application)
        End Function
    End Class
End Namespace
