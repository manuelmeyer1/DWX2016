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
Imports System.Threading.Tasks
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports Microsoft.Practices.Prism.Logging

Namespace StockTraderRI
    ' todo: Are we still using this?
    Public Class EnterpriseLibraryLoggerAdapter
        Implements ILoggerFacade

#Region "ILoggerFacade Members"

        Public Sub Log(ByVal message As String, ByVal category As Category, ByVal priority As Priority) _
            Implements ILoggerFacade.Log

            Task.Factory.StartNew(Sub()


                                      Logger.Write(message, category.ToString(), CInt(priority))
                                  End Sub)

        End Sub

#End Region
    End Class
End Namespace
