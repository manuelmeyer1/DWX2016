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

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines a wrapper for the <see cref="Window"/> class that implements the <see cref="IWindow"/> interface.
    ''' </summary>
    Public Class WindowWrapper
        Implements IWindow
        Private ReadOnly window As Window

        ''' <summary>
        ''' Initializes a new instance of <see cref="WindowWrapper"/>.
        ''' </summary>
        Public Sub New()
            Me.window = New Window()
        End Sub

        ''' <summary>
        ''' Ocurrs when the <see cref="Window"/> is closed.
        ''' </summary>
        Public Custom Event Closed As EventHandler Implements IWindow.Closed
            AddHandler(ByVal value As EventHandler)
                AddHandler Me.window.Closed, value
            End AddHandler
            RemoveHandler(ByVal value As EventHandler)
                RemoveHandler Me.window.Closed, value
            End RemoveHandler
            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                ' RaiseEvent Me.window.Closed
            End RaiseEvent
        End Event

        ''' <summary>
        ''' Gets or Sets the content for the <see cref="Window"/>.
        ''' </summary>
        Public Property Content() As Object Implements IWindow.Content
            Get
                Return Me.window.Content
            End Get
            Set(ByVal value As Object)
                Me.window.Content = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the <see cref="Window.Owner"/> control of the <see cref="Window"/>.
        ''' </summary>
        Public Property Owner() As Object Implements IWindow.Owner
            Get
                Return Me.window.Owner
            End Get
            Set(ByVal value As Object)
                Me.window.Owner = TryCast(value, Window)
            End Set
        End Property

        ''' <summary>
        ''' Gets or Sets the <see cref="FrameworkElement.Style"/> to apply to the <see cref="Window"/>.
        ''' </summary>
        Public Property Style() As Style Implements IWindow.Style
            Get
                Return Me.window.Style
            End Get
            Set(ByVal value As Style)
                Me.window.Style = value
            End Set
        End Property

        ''' <summary>
        ''' Opens the <see cref="Window"/>.
        ''' </summary>
        Public Sub Show() Implements IWindow.Show
            Me.window.Show()
        End Sub

        ''' <summary>
        ''' Closes the <see cref="Window"/>.
        ''' </summary>
        Public Sub Close() Implements IWindow.Close
            Me.window.Close()
        End Sub
    End Class
End Namespace