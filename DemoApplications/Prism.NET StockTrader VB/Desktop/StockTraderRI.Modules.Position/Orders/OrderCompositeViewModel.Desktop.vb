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
Imports System.Globalization
Imports StockTraderRI.Modules.Position.Models

Namespace StockTraderRI.Modules.Position.Orders
    Partial Public Class OrderCompositeViewModel
        Private Sub SetTransactionInfo(ByVal transactionInfo As TransactionInfo)
            'This instance of TransactionInfo acts as a "shared model" between this view and the order details view.
            'The scenario says that these 2 views are decoupled, so they don't share the presentation model, they are only tied
            'with this TransactionInfo
            Me.orderDetailsViewModel.TransactionInfo = transactionInfo

            'Bind the CompositeOrderView header to a string representation of the TransactionInfo shared instance (we expect the details presenter to modify it from user interaction).
            Dim binding As New MultiBinding()
            binding.Bindings.Add(New Binding("TransactionType") With {.Source = transactionInfo})
            binding.Bindings.Add(New Binding("TickerSymbol") With {.Source = transactionInfo})
            binding.Converter = New OrderHeaderConverter()
            BindingOperations.SetBinding(Me, HeaderInfoProperty, binding)
        End Sub

        Public Property HeaderInfo() As String
            Get
                Return CStr(GetValue(HeaderInfoProperty))
            End Get
            Set(ByVal value As String)
                SetValue(HeaderInfoProperty, value)
            End Set
        End Property

        Private Class OrderHeaderConverter
            Implements IMultiValueConverter

            ''' <summary>
            ''' Converts a <see cref="Infrastructure.TransactionType"/> and a ticker symbol to a header that can be placed on the TabItem header
            ''' </summary>
            ''' <param name="values">values[0] should be of type <see cref="Infrastructure.TransactionType"/>. values[1] should be a string with the ticker symbol</param>
            ''' <param name="targetType"></param>
            ''' <param name="parameter"></param>
            ''' <param name="culture"></param>
            ''' <returns>Returns a human readable string with the transaction type and ticker symbol</returns>
            Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                     ByVal culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
                Return values(0).ToString() & " " & values(1).ToString()
            End Function

            Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, _
                                         ByVal culture As CultureInfo) As Object() _
                Implements IMultiValueConverter.ConvertBack
                Throw New NotImplementedException()
            End Function
        End Class
    End Class
End Namespace
