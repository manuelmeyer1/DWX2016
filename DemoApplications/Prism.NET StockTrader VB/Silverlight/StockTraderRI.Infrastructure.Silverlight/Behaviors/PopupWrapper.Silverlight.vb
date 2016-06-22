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
Imports System.Windows.Controls.Primitives
Imports System.Windows.Controls
Imports System.Windows

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines a wrapper for the <see cref="Windows.Controls.Primitives.Popup"/> class that implements the <see cref="IWindow"/> interface.
    ''' </summary>
    Public Class PopupWrapper
        Implements IWindow
        Private ReadOnly popUp As Popup
        Private ReadOnly container As ContentControl
        Private _owner As FrameworkElement

        ''' <summary>
        ''' Initializes a new instance of <see cref="PopupWrapper"/>.
        ''' </summary>
        Public Sub New()
            Me.container = New ContentControl()

            Me.popUp = New Popup()
            Me.popUp.Child = Me.container
        End Sub

        ''' <summary>
        ''' Ocurrs when the <see cref="Popup"/> is closed.
        ''' </summary>
        Public Custom Event Closed As EventHandler Implements IWindow.Closed
            AddHandler(ByVal value As EventHandler)
                AddHandler Me.popUp.Closed, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler Me.popUp.Closed, value
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                ' RaiseEvent Me.popUp.Closed
            End RaiseEvent
        End Event

        ''' <summary>
        ''' Gets or Sets the content for the <see cref="Popup"/>.
        ''' </summary>
        Public Property Content() As Object Implements IWindow.Content
            Get
                Return Me.container.Content
            End Get

            Set(ByVal value As Object)
                If value IsNot Nothing Then
                    Me.container.Content = value
                Else
                    Me.container.Content = Nothing
                    Me.popUp.Child = Nothing
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the owner of the <see cref="Popup"/> which is used for resizing.
        ''' </summary>
        Public Property Owner() As Object Implements IWindow.Owner
            Get
                Return Me._owner
            End Get

            Set(ByVal value As Object)
                If Me._owner IsNot Nothing Then
                    RemoveHandler Me._owner.SizeChanged, AddressOf OwnerSizeChanged
                End If

                Me._owner = TryCast(value, FrameworkElement)
                If Me._owner IsNot Nothing Then
                    Me.container.Width = Me._owner.ActualWidth
                    Me.container.Height = Me._owner.ActualHeight
                    AddHandler Me._owner.SizeChanged, AddressOf OwnerSizeChanged
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the <see cref="FrameworkElement.Style"/> to apply to the <see cref="Popup"/>.
        ''' </summary>
        Public Property Style() As Style Implements IWindow.Style
            Get
                Return Me.container.Style
            End Get
            Set(ByVal value As Style)
                Me.container.Style = value
            End Set
        End Property

        ''' <summary>
        ''' Opens the <see cref="Popup"/>.
        ''' </summary>
        Public Sub Show() Implements IWindow.Show
            Me.popUp.IsOpen = True
        End Sub

        ''' <summary>
        ''' Closes the <see cref="Popup"/>.
        ''' </summary>
        Public Sub Close() Implements IWindow.Close
            Me.popUp.IsOpen = False
        End Sub

        Private Sub OwnerSizeChanged(ByVal sender As Object, ByVal e As SizeChangedEventArgs)
            If Me.container IsNot Nothing Then
                Me.container.Width = Me._owner.ActualWidth
                Me.container.Height = Me._owner.ActualHeight
            End If
        End Sub
    End Class
End Namespace
