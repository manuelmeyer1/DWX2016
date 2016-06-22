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
Imports System.Text
Imports AcceptanceTestLibrary.ApplicationHelper

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
    Public Class TestDataInfrastructure
        Public Function GetCount(Of T As {IDataProvider(Of TEntity), New}, TEntity)() As Integer
            Return New T().GetCount()
        End Function

        Public Function GetData(Of T As {IDataProvider(Of TEntity), New}, TEntity)() As List(Of TEntity)
            Return New T().GetData()
        End Function

        Public Function GetDataForId(Of T As {IDataProvider(Of TEntity), New}, TEntity)(ByVal id As String) As List(Of TEntity)
            Return New T().GetDataForId(id)
        End Function
        Public Shared Function GetTestInputData(ByVal key As String) As String
            Dim testInputHandler As New ResXConfigHandler(ConfigHandler.GetValue("TestDataInputFile"))
            Return testInputHandler.GetValue(key)
        End Function

        Public Shared Function GetControlId(ByVal key As String) As String
            Dim testInputHandler As New ResXConfigHandler(ConfigHandler.GetValue("ControlIdentifiersFile"))
            Return testInputHandler.GetValue(key)
        End Function
    End Class
End Namespace
