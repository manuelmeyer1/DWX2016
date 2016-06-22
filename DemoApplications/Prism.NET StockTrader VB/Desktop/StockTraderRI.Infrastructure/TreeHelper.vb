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
Imports System.Windows.Media

Namespace StockTraderRI.Infrastructure
    ''' <summary>
    ''' Helper class used to traverse the Visual Tree.
    ''' </summary>
    Public NotInheritable Class TreeHelper
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Traverses the Visual Tree upwards looking for the ancestor that satisfies the <paramref name="predicate"/>.
        ''' </summary>
        ''' <param name="dependencyObject">The element for which the ancestor is being looked for.</param>
        ''' <param name="predicate">The predicate that evaluates if an element is the ancestor that is being looked for.</param>
        ''' <returns>
        ''' The ancestor element that matches the <paramref name="predicate"/> or <see langword="null"/>
        ''' if the ancestor was not found.
        ''' </returns>
        Public Shared Function FindAncestor(ByVal dependencyObject As DependencyObject, _
                                             ByVal predicate As Func(Of DependencyObject, Boolean)) As DependencyObject
            If predicate(dependencyObject) Then
                Return dependencyObject
            End If

            Dim parent As DependencyObject = Nothing
#If SILVERLIGHT Then
            Dim frameworkElement As FrameworkElement = TryCast(dependencyObject, FrameworkElement)
            If frameworkElement IsNot Nothing Then
                parent = If(frameworkElement.Parent, VisualTreeHelper.GetParent(frameworkElement))
            End If
#Else
            parent = LogicalTreeHelper.GetParent(dependencyObject)
#End If
            If parent IsNot Nothing Then
                Return FindAncestor(parent, predicate)
            End If

            Return Nothing
        End Function
    End Class
End Namespace