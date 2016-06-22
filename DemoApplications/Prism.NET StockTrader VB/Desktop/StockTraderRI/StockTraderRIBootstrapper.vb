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
Imports StockTraderRI.Modules.News
Imports StockTraderRI.Modules.Position
Imports StockTraderRI.Modules.Market
Imports StockTraderRI.Infrastructure
Imports Microsoft.Practices.Prism.MefExtensions
Imports Microsoft.Practices.Prism.Regions
Imports System.Windows
Imports StockTraderRI.Modules.Watch

Namespace StockTraderRI
    <CLSCompliant(False)> _
    Partial Public Class StockTraderRIBootstrapper
        Inherits MefBootstrapper

        Protected Overrides Sub ConfigureAggregateCatalog()
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(StockTraderRIBootstrapper).Assembly))
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(StockTraderRICommands).Assembly))
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(MarketModule).Assembly))
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(PositionModule).Assembly))
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(WatchModule).Assembly))
            Me.AggregateCatalog.Catalogs.Add(New AssemblyCatalog(GetType(NewsModule).Assembly))
        End Sub

        Protected Overrides Sub ConfigureContainer()
            MyBase.ConfigureContainer()
        End Sub

        Protected Overrides Sub InitializeShell()
            MyBase.InitializeShell()

#If SILVERLIGHT Then
            Application.Current.RootVisual = CType(Me.Shell, Shell)
#Else
            Application.Current.MainWindow = CType(Me.Shell, Shell)
            Application.Current.MainWindow.Show()
#End If
        End Sub

        Protected Overrides Function ConfigureDefaultRegionBehaviors() As IRegionBehaviorFactory
            Dim factory = MyBase.ConfigureDefaultRegionBehaviors()

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", GetType(AutoPopulateExportedViewsBehavior))

            Return factory
        End Function

        Protected Overrides Function CreateShell() As DependencyObject
            Return Me.Container.GetExportedValue(Of Shell)()
        End Function
    End Class
End Namespace
