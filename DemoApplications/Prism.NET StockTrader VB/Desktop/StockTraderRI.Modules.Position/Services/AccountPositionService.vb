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
Imports System.ComponentModel.Composition
Imports System.Xml.Linq
Imports System.IO
Imports System.Globalization
Imports My.Resources
Imports StockTraderRI.Infrastructure.Models
Imports StockTraderRI.Infrastructure.Interfaces

Namespace StockTraderRI.Modules.Position.Services
    <Export(GetType(IAccountPositionService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class AccountPositionService
        Implements IAccountPositionService
        Private _positions As New List(Of AccountPosition)()

        Public Sub New()
            InitializePositions()
        End Sub

#Region "IAccountPositionService Members"

        Public Event Updated As EventHandler(Of AccountPositionModelEventArgs) _
            Implements IAccountPositionService.Updated

        Public Function GetAccountPositions() As IList(Of AccountPosition) _
            Implements IAccountPositionService.GetAccountPositions
            Return _positions
        End Function

#End Region

        Private Sub InitializePositions()
            Dim document As XDocument = XDocument.Load(New StringReader(AccountPositions))
            _positions = _
                document.Descendants("AccountPosition").Select( _
                                                                 Function(x) _
                                                                    New AccountPosition( _
                                                                                         x.Element("TickerSymbol"). _
                                                                                            Value, _
                                                                                         Decimal.Parse( _
                                                                                                        x.Element( _
                                                                                                                   "CostBasis") _
                                                                                                           .Value, _
                                                                                                        CultureInfo. _
                                                                                                           InvariantCulture), _
                                                                                         Long.Parse( _
                                                                                                     x.Element("Shares") _
                                                                                                        .Value, _
                                                                                                     CultureInfo. _
                                                                                                        InvariantCulture))) _
                    .ToList()
        End Sub
    End Class
End Namespace
