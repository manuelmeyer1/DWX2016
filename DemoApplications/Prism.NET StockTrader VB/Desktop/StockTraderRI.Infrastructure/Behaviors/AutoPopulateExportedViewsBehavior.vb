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
Imports System.ComponentModel.Composition
Imports Microsoft.Practices.Prism.Regions

Namespace StockTraderRI.Infrastructure
    <Export(GetType(AutoPopulateExportedViewsBehavior)), PartCreationPolicy(CreationPolicy.NonShared)> _
    Public Class AutoPopulateExportedViewsBehavior
        Inherits RegionBehavior
        Implements IPartImportsSatisfiedNotification

        Protected Overrides Sub OnAttach()
            AddRegisteredViews()
        End Sub

        Public Sub OnImportsSatisfied() Implements IPartImportsSatisfiedNotification.OnImportsSatisfied
            AddRegisteredViews()
        End Sub

        Private Sub AddRegisteredViews()
            If Me.Region IsNot Nothing Then
                For Each viewEntry In Me.RegisteredViews
                    If viewEntry.Metadata.RegionName = Me.Region.Name Then
                        Dim view = viewEntry.Value

                        If Not Me.Region.Views.Contains(view) Then
                            Me.Region.Add(view)
                        End If
                    End If
                Next viewEntry
            End If
        End Sub

        Private _RegisteredViews As Lazy(Of Object, IViewRegionRegistration)()

        <ImportMany(AllowRecomposition:=True)> _
        Public Property RegisteredViews() As Lazy(Of Object, IViewRegionRegistration)()
            Get
                Return _RegisteredViews
            End Get
            Set(ByVal value As Lazy(Of Object, IViewRegionRegistration)())
                _RegisteredViews = value
            End Set
        End Property
    End Class
End Namespace
