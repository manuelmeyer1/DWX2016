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

Namespace StockTraderRI.Infrastructure.Tests
    Public Class DependencyPropertyHelper
        Public Shared Function GetOrAddValue(Of T As {Class, New})(ByVal dependencyObject As DependencyObject, _
                                                                     ByVal [property] As DependencyProperty) As T
            Dim value As T = TryCast(dependencyObject.GetValue([property]), T)
            If value Is Nothing Then
                value = New T()
                dependencyObject.SetValue([property], value)
            End If

            Return value
        End Function
    End Class
End Namespace
