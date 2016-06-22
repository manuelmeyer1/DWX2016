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

Namespace StockTraderRI.Infrastructure.Behaviors
    ''' <summary>
    ''' Defines the interface for the Dialogs that are shown by <see cref="DialogActivationBehavior"/>.
    ''' </summary>
    Public Interface IWindow
        ''' <summary>
        ''' Ocurrs when the <see cref="IWindow"/> is closed.
        ''' </summary>
        Event Closed As EventHandler

        ''' <summary>
        ''' Gets or sets the content for the <see cref="IWindow"/>.
        ''' </summary>
        Property Content() As Object

        ''' <summary>
        ''' Gets or sets the owner control of the <see cref="IWindow"/>.
        ''' </summary>
        Property Owner() As Object

        ''' <summary>
        ''' Gets or sets the <see cref="System.Windows.Style"/> to apply to the <see cref="IWindow"/>.
        ''' </summary>
        Property Style() As Style

        ''' <summary>
        ''' Opens the <see cref="IWindow"/>.
        ''' </summary>
        Sub Show()

        ''' <summary>
        ''' Closes the <see cref="IWindow"/>.
        ''' </summary>
        Sub Close()
    End Interface
End Namespace