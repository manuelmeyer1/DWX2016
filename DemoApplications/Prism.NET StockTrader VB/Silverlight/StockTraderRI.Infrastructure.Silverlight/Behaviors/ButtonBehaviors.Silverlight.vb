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
Imports System.Windows.Controls.Primitives

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines a behavior for <see cref="Windows.Controls.Primitives.ButtonBase"/> that on <see cref="Windows.Controls.Primitives.ButtonBase.Click"/> closes the ancestor <see cref="Windows.Controls.Primitives.Popup"/> in the Visual Tree.
    ''' </summary>
    Public NotInheritable Class ButtonBehaviors
        ''' <summary>
        ''' When <see langword="true"/> closes the ancestor <see cref="Windows.Controls.Primitives.Popup"/> on <see cref="Windows.Controls.Primitives.ButtonBase.Click"/>.
        ''' </summary>
        Public Shared ReadOnly _
            CloseAncestorPopupProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("CloseAncestorPopup", GetType(Boolean), GetType(ButtonBehaviors), _
                                                     New PropertyMetadata(AddressOf OnCloseAncestorPopupChanged))

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the value of <see cref="CloseAncestorPopupProperty"/>.
        ''' </summary>
        ''' <param name="dependencyObject">The button on which the behavior is attached.</param>
        ''' <returns>The value of <see cref="CloseAncestorPopupProperty"/>.</returns>
        Public Shared Function GetCloseAncestorPopup(ByVal dependencyObject As DependencyObject) As Boolean
            Return CBool(If(dependencyObject.GetValue(CloseAncestorPopupProperty), False))
        End Function

        ''' <summary>
        ''' Sets the value of <see cref="CloseAncestorPopupProperty"/>.
        ''' </summary>
        ''' <param name="dependencyObject">The button on which the behavior will be attached.</param>
        ''' <param name="value">The value to set on <see cref="CloseAncestorPopupProperty"/>.</param>
        Public Shared Sub SetCloseAncestorPopup(ByVal dependencyObject As DependencyObject, ByVal value As Boolean)
            dependencyObject.SetValue(CloseAncestorPopupProperty, value)
        End Sub

        Private Shared Sub OnCloseAncestorPopupChanged(ByVal d As DependencyObject, _
                                                        ByVal e As DependencyPropertyChangedEventArgs)
            Dim button As ButtonBase = TryCast(d, ButtonBase)
            If button IsNot Nothing Then
                If CBool(e.NewValue) Then
                    AddHandler button.Click, AddressOf CloseButtonClicked
                Else
                    RemoveHandler button.Click, AddressOf CloseButtonClicked
                End If
            End If
        End Sub

        Private Shared Sub CloseButtonClicked(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button As ButtonBase = TryCast(sender, ButtonBase)
            If button IsNot Nothing AndAlso GetCloseAncestorPopup(button) Then
                Dim _popup = TryCast(TreeHelper.FindAncestor(button, Function(d) TypeOf d Is Popup), Popup)
                If _popup IsNot Nothing Then
                    _popup.IsOpen = False
                End If
            End If
        End Sub
    End Class
End Namespace
