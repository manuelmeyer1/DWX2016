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
Imports System.Windows.Controls
Imports System.Collections.ObjectModel

Namespace StockTraderRI.Infrastructure
    ''' <summary>
    ''' Defines a <see cref="Windows.DependencyProperty"/> where all <see cref="Windows.Controls.ValidationError"/> are stored to be used from XAML on <see cref="Windows.Controls.ToolTip"/>.
    ''' </summary>
    Public NotInheritable Class Validation
        ''' <summary>
        ''' Collection of <see cref="Windows.Controls.ValidationError"/> ocurred on the attached element.
        ''' </summary>
        Public Shared ReadOnly _
            ErrorsProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("Errors", GetType(ObservableCollection(Of ValidationError)), _
                                                     GetType(Validation), Nothing)

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the value of <see cref="ErrorsProperty"/> on <paramref name="dependencyObject"/>.
        ''' </summary>
        ''' <param name="dependencyObject">Element on which the <see cref="ErrorsProperty"/> property is attached.</param>
        ''' <returns>Value of <see cref="ErrorsProperty"/>.</returns>
        Public Shared Function GetErrors(ByVal dependencyObject As DependencyObject) _
            As ObservableCollection(Of ValidationError)
            Return _
                DependencyPropertyHelper.GetOrAddValue(Of ObservableCollection(Of ValidationError))(dependencyObject, _
                                                                                                      ErrorsProperty)
        End Function
    End Class
End Namespace