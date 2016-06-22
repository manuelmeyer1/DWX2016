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
Imports System.Diagnostics.CodeAnalysis
Imports StockTraderRI.Infrastructure

Namespace StockTraderRI.Modules.News.Article
    ''' <summary>
    ''' Interaction logic for NewsReader.xaml
    ''' </summary>
    <ViewExport("NewsReaderView"), PartCreationPolicy(CreationPolicy.Shared)> _
    Partial Public Class NewsReaderView
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Public Shared ReadOnly Property Title() As String
            Get
                Return Properties.NewsReaderViewTitle
            End Get
        End Property

        ''' <summary>
        ''' Sets the ViewModel.
        ''' </summary>
        ''' <remarks>
        ''' This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        ''' the appropriate view model.
        ''' </remarks>
        <Import(), _
            SuppressMessage _
                ("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", _
                 Justification:="Needs to be a property to be composed by MEF")> _
        Private WriteOnly Property ViewModel() As NewsReaderViewModel
            Set(ByVal value As NewsReaderViewModel)
                Me.DataContext = value
            End Set
        End Property
    End Class
End Namespace
