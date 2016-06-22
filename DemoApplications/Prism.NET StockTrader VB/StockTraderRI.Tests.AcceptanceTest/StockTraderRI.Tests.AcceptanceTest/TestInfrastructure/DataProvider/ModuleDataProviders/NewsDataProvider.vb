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

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure.DataProvider.ModuleDataProviders
    Public Class NewsDataProvider
        Inherits DataProviderBase(Of News)
        Public Sub New()
            MyBase.New()
        End Sub

        Public Overrides Function GetDataFilePath() As String
            Return ConfigHandler.GetValue("NewsDataFile")
        End Function

        Public Overrides Function GetDataForId(ByVal id As String) As List(Of News)
            Dim news As New List(Of News)()

            Dim xDoc As XDocument = XDocument.Load(GetDataFilePath())
            For Each newsItem As XElement In xDoc.Descendants("NewsItems").Descendants("NewsItem").Where(Function(_newsItem) _newsItem.Attribute(TestDataInfrastructure.GetTestInputData("TickerSymbol")).Value.Equals(id))

                news.Add(New News(id, newsItem.Attribute(TestDataInfrastructure.GetTestInputData("IconUri")).Value, Date.Parse(newsItem.Attribute(TestDataInfrastructure.GetTestInputData("PublishedDate")).Value, CultureInfo.InvariantCulture), newsItem.Elements("Title").ToList()(0).Value, newsItem.Elements("Body").ToList()(0).Value))
            Next newsItem

            Return news
        End Function
    End Class
End Namespace
