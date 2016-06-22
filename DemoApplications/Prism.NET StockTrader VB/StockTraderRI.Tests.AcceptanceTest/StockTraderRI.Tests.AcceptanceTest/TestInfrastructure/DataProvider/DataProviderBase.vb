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
Imports System.Xml.Serialization
Imports System.Xml

Namespace StockTraderRI.Tests.AcceptanceTest.TestInfrastructure
    Public MustInherit Class DataProviderBase(Of TEntity)
        Implements IDataProvider(Of TEntity), IDisposable
        Private xmlSerializer As XmlSerializer = Nothing
        Private xmlReader As XmlTextReader = Nothing

        Protected Sub New()
            xmlSerializer = New XmlSerializer(GetType(List(Of TEntity)))
            xmlReader = New XmlTextReader(GetDataFilePath())
        End Sub

        Public Overridable Function GetData() As List(Of TEntity) Implements IDataProvider(Of TEntity).GetData
            Return CType(xmlSerializer.Deserialize(xmlReader), List(Of TEntity))
        End Function

        Public Overridable Function GetDataForId(ByVal id As String) As List(Of TEntity) Implements IDataProvider(Of TEntity).GetDataForId
            Throw New NotImplementedException()
        End Function

        Public Overridable Function GetCount() As Integer Implements IDataProvider(Of TEntity).GetCount
            Return (CType(xmlSerializer.Deserialize(xmlReader), List(Of TEntity))).Count
        End Function

        Public MustOverride Function GetDataFilePath() As String Implements IDataProvider(Of TEntity).GetDataFilePath

        #Region "IDisposable Members"

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        #End Region

        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Nothing IsNot xmlSerializer Then
                    xmlSerializer = Nothing
                End If

                If Nothing IsNot xmlReader Then
                    xmlReader = Nothing
                End If
            End If
        End Sub
    End Class
End Namespace
