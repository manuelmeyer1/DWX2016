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
Imports System.Windows.Controls
Imports System.Collections.Specialized
Imports System.Collections.ObjectModel

Namespace StockTraderRI.Infrastructure
    ''' <summary>
    ''' Provides a way to apply a tooltip and set the background of a control to a different color when an exception occurs in a binding.
    ''' </summary>
    ''' <remarks>
    ''' This is to workaround the lack of Style triggers and ErrorTemplate that is present in WPF and not in Silverlight.
    ''' This should not be taken as guidance on how to do validation.
    ''' </remarks>
    Public NotInheritable Class OnValidationError
        ''' <summary>
        ''' <see cref="Windows.Media.Brush"/> value to set on <see cref="Windows.Controls.Control.Background"/> when a validation error ocurr.
        ''' </summary>
        Public Shared ReadOnly _
            ToggleBackgroundProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ToggleBackground", GetType(Brush), GetType(OnValidationError), _
                                                     New PropertyMetadata(AddressOf ToggleBackgroundPropertyChanged))

        ''' <summary>
        ''' When <see langword="true"/> sets the validation error message as tooltip over the control.
        ''' </summary>
        Public Shared ReadOnly _
            ShowToolTipProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ShowToolTip", GetType(Boolean), GetType(OnValidationError), _
                                                     New PropertyMetadata(AddressOf ShowToolTipPropertyChanged))

#Region "Private attached properties to control lifetime of attached behaviors"

        Private Shared ReadOnly _
            MonitorBindingValidationErrorsBehaviorProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("MonitorBindingValidationErrorsBehavior", _
                                                     GetType(MonitorBindingValidationErrorsBehavior), _
                                                     GetType(OnValidationError), Nothing)

        Private Shared ReadOnly _
            ToggleBackgroundOnValidationBehaviorProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ToggleBackgroundOnValidationBehavior", _
                                                     GetType(ToggleBackgroundOnValidationBehavior), _
                                                     GetType(OnValidationError), Nothing)

        Private Shared ReadOnly _
            ShowToolTipOnValidationBehaviorProperty As DependencyProperty = _
                DependencyProperty.RegisterAttached("ShowToolTipOnValidationBehavior", _
                                                     GetType(ShowToolTipOnValidationBehavior), _
                                                     GetType(OnValidationError), Nothing)

#End Region

#Region "Public wrappers around the Attached Properties"

        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the <see cref="Brush"/> value that is set on <see cref="Controls.Control.Background"/> when a validation error ocurr.
        ''' </summary>
        ''' <param name="dependencyObject"><see cref="Controls.Control"/> on which the <see cref="ToggleBackgroundProperty"/> property is set.</param>
        ''' <returns>Value of the <see cref="ToggleBackgroundProperty"/> property.</returns>
        Public Shared Function GetToggleBackground(ByVal dependencyObject As DependencyObject) As Brush
            Return TryCast(dependencyObject.GetValue(ToggleBackgroundProperty), Brush)
        End Function

        ''' <summary>
        ''' Sets the <see cref="Brush"/> value that is set on <see cref="Controls.Control.Background"/> when a validation error ocurr.
        ''' </summary>
        ''' <param name="dependencyObject"><see cref="Controls.Control"/> on which the <see cref="ToggleBackgroundProperty"/> property will be set.</param>
        ''' <param name="value">Value to set to the <see cref="ToggleBackgroundProperty"/> property.</param>
        Public Shared Sub SetToggleBackground(ByVal dependencyObject As DependencyObject, ByVal value As Brush)
            dependencyObject.SetValue(ToggleBackgroundProperty, value)
        End Sub

        ''' <summary>
        ''' Gets the value for the <see cref="ShowToolTipProperty"/> on the <paramref name="dependencyObject"/>.
        ''' </summary>
        ''' <param name="dependencyObject"><see cref="Controls.Control"/> on which the <see cref="ShowToolTipProperty"/> property is set.</param>
        ''' <returns>Value of the <see cref="ShowToolTipProperty"/> property.</returns>
        Public Shared Function GetShowToolTip(ByVal dependencyObject As DependencyObject) As Boolean
            Return CBool(If(dependencyObject.GetValue(ShowToolTipProperty), False))
        End Function

        ''' <summary>
        ''' Sets the value for the <see cref="ShowToolTipProperty"/> on the <paramref name="dependencyObject"/>.
        ''' </summary>
        ''' <param name="dependencyObject"><see cref="Controls.Control"/> on which the <see cref="ShowToolTipProperty"/> property will be set.</param>
        ''' <param name="value">Value to set to the <see cref="ShowToolTipProperty"/> property.</param>
        Public Shared Sub SetShowToolTip(ByVal dependencyObject As DependencyObject, ByVal value As Boolean)
            dependencyObject.SetValue(ShowToolTipProperty, value)
        End Sub

#End Region

        ''' <summary>
        ''' Sets the value of <see cref="MonitorBindingValidationErrorsBehavior.Target"/> property to <paramref name="element"/>.
        ''' </summary>
        ''' <param name="element">Element whose <see cref="FrameworkElement.BindingValidationError"/> event will be handled.</param>
        Public Shared Sub MonitorBindingValidationErrors(ByVal element As FrameworkElement)
            Dim _
                behavior = _
                    DependencyPropertyHelper.GetOrAddValue(Of MonitorBindingValidationErrorsBehavior)(element, _
                                                                                                        MonitorBindingValidationErrorsBehaviorProperty)
            behavior.Target = element
        End Sub

        Private Shared Sub ToggleBackgroundPropertyChanged(ByVal dependencyObject As DependencyObject, _
                                                            ByVal e As DependencyPropertyChangedEventArgs)
            Dim element = TryCast(dependencyObject, Control)
            If element IsNot Nothing Then
                MonitorBindingValidationErrors(element)

                Dim _
                    behavior = _
                        DependencyPropertyHelper.GetOrAddValue(Of ToggleBackgroundOnValidationBehavior)(element, _
                                                                                                          ToggleBackgroundOnValidationBehaviorProperty)
                behavior.Target = element
                behavior.ErrorBrush = TryCast(e.NewValue, Brush)
            End If
        End Sub

        Private Shared Sub ShowToolTipPropertyChanged(ByVal dependencyObject As DependencyObject, _
                                                       ByVal e As DependencyPropertyChangedEventArgs)
            Dim element = TryCast(dependencyObject, FrameworkElement)
            If element IsNot Nothing Then
                If CBool(e.NewValue) = True Then
                    MonitorBindingValidationErrors(element)
                    Dim _
                        behavior = _
                            DependencyPropertyHelper.GetOrAddValue(Of ShowToolTipOnValidationBehavior)(element, _
                                                                                                         ShowToolTipOnValidationBehaviorProperty)
                    behavior.Target = element
                End If
            End If
        End Sub

        Friend Class MonitorBindingValidationErrorsBehavior
            Private _target As FrameworkElement

            Public Property Target() As FrameworkElement
                Get
                    Return Me._target
                End Get

                Set(ByVal value As FrameworkElement)
                    If Me._target IsNot value Then
                        Debug.Assert(Me._target Is Nothing)
                        Me._target = value
                        Me.Attach()
                    End If
                End Set
            End Property

            Private Sub Attach()
                AddHandler Me.Target.BindingValidationError, AddressOf OnElementValidationError
            End Sub

            Private Sub OnElementValidationError(ByVal sender As Object, ByVal args As ValidationErrorEventArgs)
                Dim errors As ObservableCollection(Of ValidationError) = Validation.GetErrors(Me.Target)

                If args.Action = ValidationErrorEventAction.Added Then
                    If Not errors.Contains(args.Error) Then
                        errors.Add(args.Error)
                    End If
                Else
                    errors.Remove(args.Error)
                End If
            End Sub
        End Class

        Friend Class ToggleBackgroundOnValidationBehavior
            Private _target As Control
            Private originalBrush As Brush
            Private hasErrors As Boolean

            Public Property Target() As Control
                Get
                    Return Me._target
                End Get

                Set(ByVal value As Control)
                    If Me._target IsNot value Then
                        Debug.Assert(Me._target Is Nothing)
                        Me._target = value
                        Me.Attach()
                    End If
                End Set
            End Property

            Private _ErrorBrush As Brush

            Public Property ErrorBrush() As Brush
                Get
                    Return _ErrorBrush
                End Get
                Set(ByVal value As Brush)
                    _ErrorBrush = value
                End Set
            End Property

            Private Sub Attach()
                Dim errors As ObservableCollection(Of ValidationError) = Validation.GetErrors(Me.Target)
                AddHandler errors.CollectionChanged, AddressOf ErrorsCollectionChanged
                Me.RefreshBackground()
            End Sub

            Private Sub ErrorsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
                Me.RefreshBackground()
            End Sub

            Private Sub RefreshBackground()
                Dim targetControl As Control = Me.Target
                Dim errors As ObservableCollection(Of ValidationError) = Validation.GetErrors(targetControl)
                If (Not Me.hasErrors) AndAlso errors.Count > 0 Then
                    Me.hasErrors = True
                    Me.originalBrush = targetControl.Background
                    targetControl.Background = Me.ErrorBrush
                ElseIf Me.hasErrors AndAlso errors.Count = 0 Then
                    Me.hasErrors = False
                    targetControl.Background = Me.originalBrush
                End If
            End Sub
        End Class

        Friend Class ShowToolTipOnValidationBehavior
            Private _target As DependencyObject
            Private hasErrors As Boolean
            Private originalToolTip As Object

            Public Property Target() As DependencyObject
                Get
                    Return Me._target
                End Get

                Set(ByVal value As DependencyObject)
                    If Me._target IsNot value Then
                        Debug.Assert(Me._target Is Nothing)
                        Me._target = value
                        Me.Attach()
                    End If
                End Set
            End Property

            Private Sub Attach()
                Dim targetControl As DependencyObject = Me.Target
                Dim errors As ObservableCollection(Of ValidationError) = Validation.GetErrors(targetControl)
                AddHandler errors.CollectionChanged, AddressOf ErrorsCollectionChanged

                '                #Region "Workaround for a known issue in ToolTip"
                ' There is a known issue in Silverlight if setting the ToolTip for the first time on a control that
                ' has the mouse over it. As soon as you move the mouse out of the control, an exception is thrown.
                ' To workaround the issue, we set the ToolTip to an invisible ToolTip if none was already specified.
                Dim toolTip As Object = ToolTipService.GetToolTip(targetControl)
                If toolTip Is Nothing Then
                    ToolTipService.SetToolTip(targetControl, _
                                               New ToolTip With {.Content = "InvisibleToolTip", .Opacity = 0})
                End If
                '                #End Region

                Me.RefreshToolTip()
            End Sub

            Private Sub ErrorsCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
                Me.RefreshToolTip()
            End Sub

            Private Sub RefreshToolTip()
                Dim errors As ObservableCollection(Of ValidationError) = Validation.GetErrors(Me.Target)
                If errors.Count > 0 Then
                    If Not Me.hasErrors Then
                        Me.hasErrors = True
                        Me.originalToolTip = ToolTipService.GetToolTip(Me.Target)
                    End If

                    ToolTipService.SetToolTip(Me.Target, errors(0).Exception.Message)
                ElseIf Me.hasErrors Then
                    ToolTipService.SetToolTip(Me.Target, Me.originalToolTip)
                End If
            End Sub
        End Class
    End Class
End Namespace