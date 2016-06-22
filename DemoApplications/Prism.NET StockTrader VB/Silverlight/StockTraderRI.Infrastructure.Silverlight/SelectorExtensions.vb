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
Imports System.Reflection
Imports System.Windows.Controls.Primitives
Imports System.Windows.Controls
Imports System.Globalization
Imports My.Resources

Namespace StockTraderRI.Infrastructure
    ''' <summary>
    ''' Defines the Attached Behavior needed to keep synchronized the selected value 
    ''' of a <see cref="Windows.Controls.Primitives.Selector"/> with a bound property.
    ''' </summary>
    ''' <remarks>This is to workaround the missing SelectedItem property that is present in WPF but not in Silverlight.</remarks>
    Public NotInheritable Class SelectorExtensions
        ''' <summary>
        ''' The property bound to the <see cref="Windows.Controls.Primitives.Selector"/>'s selected value.
        ''' </summary>
        Public Shared ReadOnly _
            SelectedValueProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("SelectedValue", GetType(Object), GetType(SelectorExtensions), _
                                                     New PropertyMetadata(AddressOf SelectedValueChanged))

        ''' <summary>
        ''' The path to the bound property getter.
        ''' </summary>
        Public Shared ReadOnly _
            SelectedValuePathProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("SelectedValuePath", GetType(String), GetType(SelectorExtensions), _
                                                     New PropertyMetadata(AddressOf SelectedValuePathChanged))

        Private Shared ReadOnly cachedPropertyGetters As New Dictionary(Of String, MethodInfo)()

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the <see cref="SelectedValueProperty"/> value.
        ''' </summary>
        ''' <param name="dependencyObject">The <see cref="DependencyObject"/> on which the attached property is set.</param>
        ''' <returns>The value of the <see cref="SelectedValueProperty"/> property.</returns>
        Public Shared Function GetSelectedValue(ByVal dependencyObject As DependencyObject) As Object
            Return dependencyObject.GetValue(SelectedValueProperty)
        End Function

        ''' <summary>
        ''' Sets the <see cref="SelectedValueProperty"/> value.
        ''' </summary>
        ''' <param name="dependencyObject">The <see cref="DependencyObject"/> on which the attached property will be set.</param>
        ''' <param name="value">Value to set to <see cref="SelectedValueProperty"/> attached property.</param>
        Public Shared Sub SetSelectedValue(ByVal dependencyObject As DependencyObject, ByVal value As Object)
            dependencyObject.SetValue(SelectedValueProperty, value)
        End Sub

        ''' <summary>
        ''' Gets the <see cref="SelectedValuePathProperty"/> value.
        ''' </summary>
        ''' <param name="dependencyObject">The <see cref="DependencyObject"/> on which the attached property is set.</param>
        ''' <returns>The value of the <see cref="SelectedValuePathProperty"/> property.</returns>
        Public Shared Function GetSelectedValuePath(ByVal dependencyObject As DependencyObject) As String
            Return TryCast(dependencyObject.GetValue(SelectedValuePathProperty), String)
        End Function

        ''' <summary>
        ''' Sets the <see cref="SelectedValuePathProperty"/> value.
        ''' </summary>
        ''' <param name="dependencyObject">The <see cref="DependencyObject"/> on which the attached property will be set.</param>
        ''' <param name="value">Value to set to <see cref="SelectedValuePathProperty"/> attached property.</param>
        Public Shared Sub SetSelectedValuePath(ByVal dependencyObject As DependencyObject, ByVal value As String)
            dependencyObject.SetValue(SelectedValuePathProperty, value)
        End Sub

        Private Shared Sub SelectedValueChanged(ByVal d As DependencyObject, _
                                                 ByVal e As DependencyPropertyChangedEventArgs)
            Dim newValue As Object = e.NewValue
            If Not Equals(newValue, e.OldValue) Then
                Dim selector As Selector = TryCast(d, Selector)
                If selector IsNot Nothing Then
                    SyncronizeSelectedItem(selector, newValue)
                End If
            End If
        End Sub

        Private Shared Sub SyncronizeSelectedItem(ByVal selector As Selector, ByVal value As Object)
            Dim selectedValuePath As String = GetSelectedValuePath(selector)
            If Not String.IsNullOrEmpty(selectedValuePath) Then
                If _
                    selector.SelectedItem Is Nothing OrElse _
                    (Not Equals(GetValueForPath(selector.SelectedItem, selectedValuePath), value)) Then
                    Dim _
                        selectedItem As Object = _
                            selector.Items.FirstOrDefault( _
                                                           Function(item) _
                                                              Equals(GetValueForPath(item, selectedValuePath), value))
                    selector.SelectedItem = selectedItem
                End If
            End If
        End Sub

        Private Shared Sub SelectedValuePathChanged(ByVal d As DependencyObject, _
                                                     ByVal e As DependencyPropertyChangedEventArgs)
            Dim selector As Selector = TryCast(d, Selector)
            If selector IsNot Nothing Then
                If e.OldValue Is Nothing AndAlso e.NewValue IsNot Nothing Then
                    ' Subscribes to selection changes in the Selector.
                    AddHandler selector.SelectionChanged, AddressOf SelectorSelectionChanged
                End If

                If e.OldValue IsNot Nothing AndAlso e.NewValue Is Nothing Then
                    ' Unsubscribes to clean up.
                    RemoveHandler selector.SelectionChanged, AddressOf SelectorSelectionChanged
                End If

                SyncronizeSelectedItem(selector, GetSelectedValue(selector))
            End If
        End Sub

        Private Shared Sub SelectorSelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs)
            Dim selector As Selector = CType(sender, Selector)
            Dim selectedItem As Object = selector.SelectedItem
            Dim selectedValuePath As String = GetSelectedValuePath(selector)
            Dim selectedValue As Object = GetValueForPath(selectedItem, selectedValuePath)
            If Not Equals(GetSelectedValue(selector), selectedValue) Then
                SetSelectedValue(selector, selectedValue)
            End If
        End Sub

        Private Shared Function GetValueForPath(ByVal instance As Object, ByVal valuePath As String) As Object
            Dim propertyGetter As MethodInfo = GetPropertyGetterForType(instance.GetType(), valuePath)
            Dim returnValue As Object = propertyGetter.Invoke(instance, Nothing)
            Return returnValue
        End Function

        Private Shared Function GetPropertyGetterForType(ByVal _type As Type, ByVal memberName As String) As MethodInfo
            Dim _
                hashKey As String = _
                    String.Format(CultureInfo.InvariantCulture, "{0}&{1}", _type.AssemblyQualifiedName, memberName)
            Dim methodInfo As MethodInfo = Nothing
            If Not cachedPropertyGetters.TryGetValue(hashKey, methodInfo) Then
                Dim [property] As PropertyInfo = _type.GetProperty(memberName)
                If [property] IsNot Nothing Then
                    methodInfo = [property].GetGetMethod()
                End If

                cachedPropertyGetters.Add(hashKey, methodInfo)
            End If

            If methodInfo Is Nothing Then
                Throw _
                    New InvalidOperationException( _
                                                   String.Format(CultureInfo.CurrentCulture, _
                                                                  SelectorExtensionCannotResolveMember, _type.FullName, _
                                                                  memberName, GetType(SelectorExtensions).Name))
            End If

            Return methodInfo
        End Function
    End Class
End Namespace