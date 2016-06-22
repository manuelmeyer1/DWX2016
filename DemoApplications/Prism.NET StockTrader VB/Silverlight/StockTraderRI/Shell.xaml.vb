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
Imports System.Windows.Controls
Imports System.Diagnostics.CodeAnalysis
Imports System.Windows
Imports System.Windows.Media.Animation
Imports StockTraderRI.Resources

Namespace StockTraderRI
    <Export()> _
    Partial Public Class Shell
        Inherits UserControl
        Private Const VisibleGridDefaultHeight As Integer = 5

        Public Sub New()
            InitializeComponent()

            AddHandler Loaded, AddressOf Shell_Loaded
        End Sub

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
        Public WriteOnly Property ViewModel() As ShellViewModel
            Set(ByVal value As ShellViewModel)
                Me.DataContext = value
            End Set
        End Property

        Private Sub Shell_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim story = CType(Me.Resources(ResourceNames.EntryStoryboardName), Storyboard)
            story.Begin()
        End Sub

        Private Sub ActionControl_LayoutUpdated(ByVal sender As Object, ByVal e As EventArgs)
            Dim _
                newVisibility As Visibility = _
                    If((Me.ActionControl.Content IsNot Nothing), Visibility.Visible, Visibility.Collapsed)
            If Me.ActionControl.Visibility <> newVisibility Then
                Me.ActionControl.Visibility = newVisibility
                Me.ActionRow.Height = _
                    New GridLength(If(newVisibility = Visibility.Visible, VisibleGridDefaultHeight, 0), _
                                    GridUnitType.Star)
            End If
        End Sub
    End Class
End Namespace
