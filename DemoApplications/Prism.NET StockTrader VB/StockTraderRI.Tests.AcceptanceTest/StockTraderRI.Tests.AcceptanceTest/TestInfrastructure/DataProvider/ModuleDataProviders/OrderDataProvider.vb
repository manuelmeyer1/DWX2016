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
Imports System.IO
Imports System.Globalization
Imports AcceptanceTestLibrary.ApplicationHelper


Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
    Public Class OrderDataProvider
        Inherits DataProviderBase(Of Order)
        Public Sub New()
            MyBase.New()
        End Sub

        Public Overrides Function GetDataFilePath() As String
            Return ConfigHandler.GetValue("OrderProcessingFile")
        End Function

        Public Overrides Function GetData() As List(Of Order)
            Dim order As New List(Of Order)()
            Dim filepath As String = GetDataFilePath()

            If File.Exists(filepath) Then
                Dim ds As New DataSet()
                ds.Locale = CultureInfo.CurrentCulture
                ds.ReadXml(filepath)
                Dim dr As DataRow = Nothing


                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    dr = ds.Tables(0).Rows(i)
                    order.Add(New Order(dr("TickerSymbol").ToString(), Decimal.Parse(dr("StopLimitPrice").ToString(), CultureInfo.InvariantCulture), dr("OrderType").ToString(), Integer.Parse(dr("Shares").ToString(), CultureInfo.InvariantCulture), dr("TimeInForce").ToString(), dr("TransactionType").ToString()))
                Next i
            End If
            Return order
        End Function
    End Class
End Namespace
