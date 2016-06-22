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
Imports System.Windows.Controls
Imports System.Windows
Imports System.Windows.Media.Animation

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
        Private previousSelectedTabItem As TabItem
        Private previousSelectedTabItemContent As FrameworkElement

        Public Sub New()
            DefaultStyleKey = GetType(AnimatedTabControl)
        End Sub

        Private _CurrentView As FrameworkElement

        Private Property CurrentView() As FrameworkElement
            Get
                Return _CurrentView
            End Get
            Set(ByVal value As FrameworkElement)
                _CurrentView = value
            End Set
        End Property

        Private _BufferView As ContentControl

        Private Property BufferView() As ContentControl
            Get
                Return _BufferView
            End Get
            Set(ByVal value As ContentControl)
                _BufferView = value
            End Set
        End Property

        Private _StartingTransition As Storyboard

        Private Property StartingTransition() As Storyboard
            Get
                Return _StartingTransition
            End Get
            Set(ByVal value As Storyboard)
                _StartingTransition = value
            End Set
        End Property

        Private _EndingTransition As Storyboard

        Private Property EndingTransition() As Storyboard
            Get
                Return _EndingTransition
            End Get
            Set(ByVal value As Storyboard)
                _EndingTransition = value
            End Set
        End Property

        Public Overrides Sub OnApplyTemplate()
            MyBase.OnApplyTemplate()

            Me.CurrentView = CType(Me.GetTemplateChild("ContentTop"), FrameworkElement)
            Me.BufferView = CType(Me.GetTemplateChild("BufferView"), ContentControl)

            Dim containerGrid As FrameworkElement = CType(Me.GetTemplateChild("LayoutRoot"), FrameworkElement)
            Me.StartingTransition = CType(containerGrid.FindName("StartingTransition"), Storyboard)
            Me.EndingTransition = CType(containerGrid.FindName("EndingTransition"), Storyboard)
            AddHandler Me.StartingTransition.Completed, AddressOf StartingTransition_Completed
        End Sub

        Protected Overrides Sub OnSelectionChanged(ByVal args As SelectionChangedEventArgs)
            If args.RemovedItems.Count > 0 Then
                Me.RestoreBufferedTabItemContent()

                ' Put the "old" view in a buffer so we can still show it to perform the starting animation with it
                Me.previousSelectedTabItem = CType(args.RemovedItems(0), TabItem)
                Me.previousSelectedTabItemContent = CType(Me.previousSelectedTabItem.Content, FrameworkElement)
                Me.previousSelectedTabItem.Content = Nothing
                Me.CurrentView.Visibility = Visibility.Collapsed
                Me.BufferView.Content = Me.previousSelectedTabItemContent

                Me.StartingTransition.Begin()
            End If
        End Sub

        Private Sub RestoreBufferedTabItemContent()
            If Me.previousSelectedTabItemContent Is Nothing OrElse Me.previousSelectedTabItem Is Nothing Then
                Return
            End If

            Me.BufferView.Content = Nothing
            Me.previousSelectedTabItem.Content = Me.previousSelectedTabItemContent
            Me.previousSelectedTabItem = Nothing
            Me.previousSelectedTabItemContent = Nothing
        End Sub

        Private Sub StartingTransition_Completed(ByVal sender As Object, ByVal e As EventArgs)
            Me.RestoreBufferedTabItemContent()

            Me.CurrentView.Visibility = Visibility.Visible

            ' fire transition
            Me.EndingTransition.Begin()
        End Sub
    End Class
End Namespace