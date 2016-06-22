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
Imports System.Windows.Data
Imports System.Globalization

Namespace StockTraderRI.Infrastructure.Converters
    Public Class TransactionTypeToStringConverter
        Implements IValueConverter

#Region "IValueConverter Members"

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                 ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing OrElse Not (TypeOf value Is TransactionType) Then
                Return Nothing
            End If

            Dim transactionType As TransactionType = CType(value, TransactionType)
            Return (If(transactionType = transactionType.Buy, "BUY", "SELL")) & " "
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                     ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

#End Region
    End Class
End Namespace
