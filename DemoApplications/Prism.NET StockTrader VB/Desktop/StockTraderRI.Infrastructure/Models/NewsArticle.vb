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


Namespace StockTraderRI.Infrastructure.Models
    Public Class NewsArticle
        Private _PublishedDate As Date

        Public Property PublishedDate() As Date
            Get
                Return _PublishedDate
            End Get
            Set(ByVal value As Date)
                _PublishedDate = value
            End Set
        End Property

        Private _Title As String

        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Private _Body As String

        Public Property Body() As String
            Get
                Return _Body
            End Get
            Set(ByVal value As String)
                _Body = value
            End Set
        End Property

        Private _IconUri As String

        Public Property IconUri() As String
            Get
                Return _IconUri
            End Get
            Set(ByVal value As String)
                _IconUri = value
            End Set
        End Property
    End Class
End Namespace
