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
Imports StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.MockModels
Imports System.Globalization
Imports AcceptanceTestLibrary.ApplicationHelper

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
    Public Class MarketDataProvider
        Inherits DataProviderBase(Of Market)
        Public Sub New()
            MyBase.New()
        End Sub

        Public Overrides Function GetDataFilePath() As String
            Return ConfigHandler.GetValue("MarketDataFile")
        End Function

        Public Overrides Function GetData() As List(Of Market)
            Dim ds As New DataSet()
            ds.Locale = CultureInfo.CurrentCulture
            ds.ReadXml(GetDataFilePath())
            Dim dr As DataRow = Nothing

            Dim market As New List(Of Market)()
            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                dr = ds.Tables(1).Rows(i)
                market.Add(New Market(dr(TestDataInfrastructure.GetTestInputData("TickerSymbol")).ToString(), Decimal.Parse(dr(TestDataInfrastructure.GetTestInputData("LastPrice")).ToString(), CultureInfo.InvariantCulture)))
            Next i

            Return market
        End Function
    End Class
End Namespace
