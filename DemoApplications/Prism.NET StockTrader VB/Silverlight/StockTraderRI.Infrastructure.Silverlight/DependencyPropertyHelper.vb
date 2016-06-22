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

Namespace StockTraderRI.Infrastructure
    ''' <summary>
    ''' Defines helper methods for dependency properties.
    ''' </summary>
    Public NotInheritable Class DependencyPropertyHelper
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets or, if it is not defined sets, the <paramref name="property"/> value on the <paramref name="dependencyObject"/> object.
        ''' </summary>
        ''' <typeparam name="T"><see cref="System.Type"/> of the <see cref="Windows.DependencyProperty"/>.</typeparam>
        ''' <param name="dependencyObject">Object on which the <paramref name="property"/> is set.</param>
        ''' <param name="property">Property whose value will be retrieved.</param>
        ''' <returns>Value of <paramref name="property"/> on <paramref name="dependencyObject"/>.</returns>
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
