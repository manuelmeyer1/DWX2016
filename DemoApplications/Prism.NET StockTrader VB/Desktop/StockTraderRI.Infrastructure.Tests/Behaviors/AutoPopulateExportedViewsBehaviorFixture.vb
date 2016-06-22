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
Imports System.ComponentModel.Composition.Hosting
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Practices.Prism.Regions

Namespace StockTraderRI.Infrastructure.Tests.Behaviors
    <TestClass()> _
    Public Class AutoPopulateExportedViewsBehaviorFixture
        <TestMethod()> _
        Public Sub WhenAttached_ThenAddsViewsRegisteredToTheRegion()
            Dim catalog = New AggregateCatalog()
            catalog.Catalogs.Add(New AssemblyCatalog(GetType(AutoPopulateExportedViewsBehavior).Assembly))
            catalog.Catalogs.Add(New TypeCatalog(GetType(View1), GetType(View2)))

            Dim container = New CompositionContainer(catalog)

            Dim behavior = container.GetExportedValue(Of AutoPopulateExportedViewsBehavior)()

            Dim region = New Region() With {.Name = "region1"}

            region.Behaviors.Add("", behavior)

            Assert.AreEqual(1, region.Views.Cast(Of Object)().Count())
            Assert.IsTrue(region.Views.Cast(Of Object)().Any(Function(e) e.GetType() Is GetType(View1)))
        End Sub

        <TestMethod()> _
        Public Sub WhenRecomposed_ThenAddsNewViewsRegisteredToTheRegion()
            Dim catalog = New AggregateCatalog()
            catalog.Catalogs.Add(New AssemblyCatalog(GetType(AutoPopulateExportedViewsBehavior).Assembly))
            catalog.Catalogs.Add(New TypeCatalog(GetType(View1), GetType(View2)))

            Dim container = New CompositionContainer(catalog)

            Dim behavior = container.GetExportedValue(Of AutoPopulateExportedViewsBehavior)()

            Dim region = New Region() With {.Name = "region1"}

            region.Behaviors.Add("", behavior)

            catalog.Catalogs.Add(New TypeCatalog(GetType(View3), GetType(View4)))

            Assert.AreEqual(2, region.Views.Cast(Of Object)().Count())
            Assert.IsTrue(region.Views.Cast(Of Object)().Any(Function(e) e.GetType() Is GetType(View1)))
            Assert.IsTrue(region.Views.Cast(Of Object)().Any(Function(e) e.GetType() Is GetType(View3)))
        End Sub
    End Class

    <ViewExport(privateRegionName:="region1")> _
    Public Class View1
    End Class

    <ViewExport(privateRegionName:="region2")> _
    Public Class View2
    End Class

    <ViewExport(privateRegionName:="region1")> _
    Public Class View3
    End Class

    <ViewExport(privateRegionName:="region2")> _
    Public Class View4
    End Class
End Namespace
