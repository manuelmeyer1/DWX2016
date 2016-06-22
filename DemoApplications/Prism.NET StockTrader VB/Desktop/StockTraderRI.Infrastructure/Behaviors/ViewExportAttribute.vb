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

Namespace StockTraderRI.Infrastructure
    <AttributeUsage(AttributeTargets.Class, AllowMultiple:=False), MetadataAttribute()> _
    Public Class ViewExportAttribute
        Inherits ExportAttribute
        Implements IViewRegionRegistration

        Public Sub New()
            MyBase.New(GetType(Object))
        End Sub

        Public Sub New(ByVal viewName As String)
            MyBase.New(viewName, GetType(Object))
        End Sub

        Public privateRegionName As String

        Public ReadOnly Property RegionName() As String Implements IViewRegionRegistration.RegionName
            Get
                Return privateRegionName
            End Get
            'Set(ByVal value As String)
            '    privateRegionName = value
            'End Set
        End Property
    End Class
End Namespace
