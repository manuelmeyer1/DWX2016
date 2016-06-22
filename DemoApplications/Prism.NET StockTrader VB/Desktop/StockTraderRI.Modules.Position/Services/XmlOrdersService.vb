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
Imports System.Globalization
Imports Microsoft.Practices.Prism.Logging
Imports My.Resources
Imports StockTraderRI.Modules.Position.Models
Imports StockTraderRI.Modules.Position.Interfaces
Imports System.IO

Namespace StockTraderRI.Modules.Position.Services
    <Export(GetType(IOrdersService)), PartCreationPolicy(CreationPolicy.Shared)> _
    Public Class XmlOrdersService
        Implements IOrdersService
        Private logger As ILoggerFacade

        <ImportingConstructor()> _
        Public Sub New(ByVal logger As ILoggerFacade)
            Me.logger = logger
        End Sub

        Private _fileName As String = "SubmittedOrders.xml"

        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        Public Sub Submit(ByVal order As Order) Implements IOrdersService.Submit
#If Not SILVERLIGHT Then
            Dim document As XDocument = If(File.Exists(FileName), XDocument.Load(FileName), New XDocument())
            Submit(order, document)
            document.Save(FileName)
#Else
            ' In silverlight, you would normally not save the order to a file, but rather send it to an XML webservice
            ' This would be the place were you would call that xml webservice. 
#End If
        End Sub

        Public Sub Submit(ByVal order As Order, ByVal document As XDocument)
            Dim ordersElement = document.Element("Orders")
            If ordersElement Is Nothing Then
                ordersElement = New XElement("Orders")
                document.Add(ordersElement)
            End If

            Dim _
                orderElement = _
                    New XElement("Order", New XAttribute("OrderType", order.OrderType), _
                                  New XAttribute("Shares", order.Shares), _
                                  New XAttribute("StopLimitPrice", order.StopLimitPrice), _
                                  New XAttribute("TickerSymbol", order.TickerSymbol), _
                                  New XAttribute("TimeInForce", order.TimeInForce), _
                                  New XAttribute("TransactionType", order.TransactionType), _
                                  New XAttribute("Date", Date.Now.ToString(CultureInfo.InvariantCulture)))
            ordersElement.Add(orderElement)

            Dim _
                message As String = _
                    String.Format(CultureInfo.CurrentCulture, LogOrderSubmitted, orderElement.ToString())
            logger.Log(message, Category.Debug, Priority.Low)
        End Sub
    End Class
End Namespace
