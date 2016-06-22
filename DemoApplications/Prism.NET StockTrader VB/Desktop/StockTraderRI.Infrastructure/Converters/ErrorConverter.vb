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
Imports System.Reflection

Namespace StockTraderRI.Infrastructure.Converters
    Public Class ErrorConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                 ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim errors As IList(Of ValidationError) = TryCast(value, IList(Of ValidationError))

            If errors Is Nothing OrElse errors.Count = 0 Then
                Return String.Empty
            End If

            Dim exception As Exception = errors(0).Exception
            If exception IsNot Nothing Then
                If TypeOf exception Is TargetInvocationException Then
                    ' It's an exception in the the model's Property setter. Get the inner exception
                    exception = exception.InnerException
                End If

                Return exception.Message
            End If

            Return errors(0).ErrorContent
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, _
                                     ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace
