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
Namespace StockTraderRI.Modules.Position.Orders
    Public Class ValueDescription(Of T As Structure)

        Public Sub New()
        End Sub

        Public Sub New(ByVal value As T, ByVal description As String)
            Me.Value = value
            Me.Description = description
        End Sub

        Private _Value As T

        Public Property Value() As T
            Get
                Return _Value
            End Get
            Set(ByVal value As T)
                _Value = value
            End Set
        End Property

        Private _Description As String

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return Description
        End Function
    End Class
End Namespace