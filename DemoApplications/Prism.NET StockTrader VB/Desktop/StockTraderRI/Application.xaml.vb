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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace StockTraderRI
    ''' <summary>
    ''' Interaction logic for App.xaml
    ''' </summary>
    Partial Public Class App
        Inherits Application

        Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
            MyBase.OnStartup(e)

#If (Debug) Then
            RunInDebugMode()
#Else
            RunInReleaseMode()
#End If
            Me.ShutdownMode = ShutdownMode.OnMainWindowClose
        End Sub

        Private Shared Sub RunInDebugMode()
            Dim bootstrapper As New StockTraderRIBootstrapper()
            bootstrapper.Run()
        End Sub

        Private Shared Sub RunInReleaseMode()
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf AppDomainUnhandledException
            Try
                Dim bootstrapper As New StockTraderRIBootstrapper()
                bootstrapper.Run()
            Catch ex As Exception
                HandleException(ex)
            End Try
        End Sub

        Private Shared Sub AppDomainUnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            HandleException(TryCast(e.ExceptionObject, Exception))
        End Sub

        Private Shared Sub HandleException(ByVal ex As Exception)
            If ex Is Nothing Then
                Return
            End If

            ExceptionPolicy.HandleException(ex, "Default Policy")
            MessageBox.Show(My.Resources.UnhandledException)
            Environment.Exit(1)
        End Sub
    End Class
End Namespace
