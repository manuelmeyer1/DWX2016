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
Imports System.Windows.Browser

Namespace StockTraderRI
    Partial Public Class App
        Inherits Application

        Public Sub New()
            AddHandler Me.Startup, AddressOf Application_Startup
            AddHandler Me.Exit, AddressOf Application_Exit
            AddHandler Me.UnhandledException, AddressOf Application_UnhandledException

            InitializeComponent()
        End Sub

        Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
            'this.RootVisual = new Shell();
            Dim bootstrapper = New StockTraderRIBootstrapper()
            bootstrapper.Run()
        End Sub

        Private Sub Application_Exit(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private Sub Application_UnhandledException(ByVal sender As Object, _
                                                    ByVal e As ApplicationUnhandledExceptionEventArgs)
            ' If the app is running outside of the debugger then report the exception using
            ' the browser's exception mechanism. On IE this will display it a yellow alert 
            ' icon in the status bar and Firefox will display a script error.
            If Not Debugger.IsAttached Then

                ' NOTE: This will allow the application to continue running after an exception has been thrown
                ' but not handled. 
                ' For production applications this error handling should be replaced with something that will 
                ' report the error to the website and stop the application.
                e.Handled = True
                Deployment.Current.Dispatcher.BeginInvoke(Sub()
                                                              ReportErrorToDOM(e)
                                                          End Sub)
            Else
                Dim exception As Exception = e.ExceptionObject
                Debugger.Break()
            End If
        End Sub

        Private Sub ReportErrorToDOM(ByVal e As ApplicationUnhandledExceptionEventArgs)
            Try
                Dim errorMsg As String = e.ExceptionObject.Message + e.ExceptionObject.StackTrace
                errorMsg = errorMsg.Replace(""""c, "'"c).Replace(vbCrLf, vbLf)

                HtmlPage.Window.Eval( _
                                      "throw new Error(""Unhandled Error in Silverlight 2 Application " & errorMsg & _
                                      """);")
            Catch e1 As Exception
            End Try
        End Sub
    End Class
End Namespace
