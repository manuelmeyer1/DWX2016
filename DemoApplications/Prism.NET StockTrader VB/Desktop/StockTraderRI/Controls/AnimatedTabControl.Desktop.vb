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
Imports System.Windows.Threading

Namespace StockTraderRI.Controls
    ''' <summary>
    ''' Custom Tab control with animations.
    ''' </summary>
    ''' <remarks>
    ''' This customization of the TabControl was required to create the animations for the transition 
    ''' between the tab items.
    ''' </remarks>
    Public Class AnimatedTabControl
        Inherits TabControl

        Public Shared ReadOnly _
            SelectionChangingEvent As RoutedEvent = _
                EventManager.RegisterRoutedEvent("SelectionChanging", RoutingStrategy.Direct, _
                                                  GetType(RoutedEventHandler), GetType(AnimatedTabControl))

        Private timer As DispatcherTimer

        Public Sub New()
            DefaultStyleKey = GetType(AnimatedTabControl)
        End Sub

        Public Custom Event SelectionChanging As RoutedEventHandler
            AddHandler(ByVal value As RoutedEventHandler)
                MyBase.AddHandler(SelectionChangingEvent, value)
            End AddHandler
            RemoveHandler(ByVal value As RoutedEventHandler)
                MyBase.RemoveHandler(SelectionChangingEvent, value)
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As RoutedEventArgs)
            End RaiseEvent
        End Event

        Protected Overrides Sub OnSelectionChanged(ByVal e As SelectionChangedEventArgs)
            Me.Dispatcher.BeginInvoke(CType(Sub()
                                                Me.RaiseSelectionChangingEvent()
                                                Me.StopTimer()
                                                Me.timer = New DispatcherTimer With {.Interval = New TimeSpan(0, 0, 0, 0, 500)}
                                                Dim handler As EventHandler = Nothing
                                                handler = Sub(sender, args)
                                                              Me.StopTimer()
                                                              MyBase.OnSelectionChanged(e)
                                                          End Sub
                                                AddHandler Me.timer.Tick, handler
                                                Me.timer.Start()
                                            End Sub, Action))
        End Sub

        ' This method raises the Tap event
        Private Sub RaiseSelectionChangingEvent()
            Dim args = New RoutedEventArgs(SelectionChangingEvent)
            MyBase.RaiseEvent(args)
        End Sub

        Private Sub StopTimer()
            If Me.timer IsNot Nothing Then
                Me.timer.Stop()
                Me.timer = Nothing
            End If
        End Sub
    End Class
End Namespace