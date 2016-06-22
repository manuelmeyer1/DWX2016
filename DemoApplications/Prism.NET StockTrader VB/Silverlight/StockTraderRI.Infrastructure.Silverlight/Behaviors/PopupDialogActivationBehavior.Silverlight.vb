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
    ''' Specifies the <see cref="DialogActivationBehavior"/> class for using the behavior on Silverlight.
    ''' </summary>
    Public Class PopupDialogActivationBehavior
        Inherits DialogActivationBehavior

        ''' <summary>
        ''' Creates a wrapper for the Silverlight <see cref="System.Windows.Controls.Primitives.Popup"/>.
        ''' </summary>
        ''' <returns>Instance of the <see cref="System.Windows.Controls.Primitives.Popup"/> wrapper.</returns>
        Protected Overrides Function CreateWindow() As IWindow
            Return New PopupWrapper()
        End Function
    End Class
End Namespace
