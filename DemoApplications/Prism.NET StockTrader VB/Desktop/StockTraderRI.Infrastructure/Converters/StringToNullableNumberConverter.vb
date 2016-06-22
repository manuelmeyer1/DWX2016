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
    Public Class StringToNullableNumberConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                 ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                     ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim stringValue As String = TryCast(value, String)
            If stringValue IsNot Nothing Then
                If targetType Is GetType(Integer?) Then
                    Dim result As Integer
                    If Integer.TryParse(stringValue, result) Then
                        Return result
                    End If

                    Return Nothing
                End If

                If targetType Is GetType(Decimal?) Then
                    Dim result As Decimal
                    If Decimal.TryParse(stringValue, result) Then
                        Return result
                    End If

                    Return Nothing
                End If
            End If

            Return value
        End Function
    End Class
End Namespace