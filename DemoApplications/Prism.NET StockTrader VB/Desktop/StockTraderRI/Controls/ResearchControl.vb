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
Imports System.Windows.Controls
Imports System.Windows
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized

Namespace StockTraderRI.Controls
    ''' <summary>
    ''' Custom ItemsControl with Header.
    ''' </summary>
    Public Class ResearchControl
        Inherits ItemsControl

        Public Shared ReadOnly _
            HeadersProperty As DependencyProperty = _
                DependencyProperty.Register("Headers", GetType(ObservableCollection(Of Object)), _
                                             GetType(ResearchControl), Nothing)

        Public Sub New()
            Me.Headers = New ObservableCollection(Of Object)()
        End Sub

        Public Property Headers() As ObservableCollection(Of Object)
            Get
                Return CType(GetValue(HeadersProperty), ObservableCollection(Of Object))
            End Get
            Private Set(ByVal value As ObservableCollection(Of Object))
                SetValue(HeadersProperty, value)
            End Set
        End Property

        Protected Overrides Sub OnItemsChanged(ByVal e As NotifyCollectionChangedEventArgs)
            MyBase.OnItemsChanged(e)
            If e.Action = NotifyCollectionChangedAction.Add Then
                Dim newItem As Object = e.NewItems(0)
                Dim header As DependencyObject = GetHeader(TryCast(newItem, FrameworkElement))
                Me.Headers.Insert(e.NewStartingIndex, header)
            ElseIf e.Action = NotifyCollectionChangedAction.Remove Then
                Me.Headers.RemoveAt(e.OldStartingIndex)
            End If
        End Sub

        Private Shared Function GetHeader(ByVal view As FrameworkElement) As DependencyObject
            If view IsNot Nothing Then
                Dim template As DataTemplate = TryCast(view.Resources("HeaderIcon"), DataTemplate)
                If template IsNot Nothing Then
                    Return template.LoadContent()
                End If
            End If

            Return Nothing
        End Function
    End Class
End Namespace
