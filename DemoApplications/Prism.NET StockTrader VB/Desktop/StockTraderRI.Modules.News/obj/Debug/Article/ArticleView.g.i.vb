﻿#ExternalChecksum("..\..\..\Article\ArticleView.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","2EEEB6D000E8B3F62AABFA6DE536E2AD")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports StockTraderRI.Infrastructure.Models
Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell

Namespace StockTraderRI.Modules.News.Article
    
    '''<summary>
    '''ArticleView
    '''</summary>
    <Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
    Partial Public Class ArticleView
        Inherits System.Windows.Controls.UserControl
        Implements System.Windows.Markup.IComponentConnector
        
        
        #ExternalSource("..\..\..\Article\ArticleView.xaml",164)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents ListGrid As System.Windows.Controls.Grid
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Article\ArticleView.xaml",178)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents NewsList As System.Windows.Controls.ListBox
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Article\ArticleView.xaml",180)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents DetailsGrid As System.Windows.Controls.Grid
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Article\ArticleView.xaml",196)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents BackButton As System.Windows.Controls.Button
        
        #End ExternalSource
        
        
        #ExternalSource("..\..\..\Article\ArticleView.xaml",197)
        <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
        Friend WithEvents OpenButton As System.Windows.Controls.Button
        
        #End ExternalSource
        
        Private _contentLoaded As Boolean
        
        '''<summary>
        '''InitializeComponent
        '''</summary>
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
        Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
            If _contentLoaded Then
                Return
            End If
            _contentLoaded = true
            Dim resourceLocater As System.Uri = New System.Uri("/StockTraderRI.Modules.News;component/article/articleview.xaml", System.UriKind.Relative)
            
            #ExternalSource("..\..\..\Article\ArticleView.xaml",1)
            System.Windows.Application.LoadComponent(Me, resourceLocater)
            
            #End ExternalSource
        End Sub
        
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0"),  _
         System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
         System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
        Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
            If (connectionId = 1) Then
                Me.ListGrid = CType(target,System.Windows.Controls.Grid)
                Return
            End If
            If (connectionId = 2) Then
                Me.NewsList = CType(target,System.Windows.Controls.ListBox)
                
                #ExternalSource("..\..\..\Article\ArticleView.xaml",178)
                AddHandler Me.NewsList.SelectionChanged, New System.Windows.Controls.SelectionChangedEventHandler(AddressOf Me.ListBox_SelectionChanged)
                
                #End ExternalSource
                Return
            End If
            If (connectionId = 3) Then
                Me.DetailsGrid = CType(target,System.Windows.Controls.Grid)
                Return
            End If
            If (connectionId = 4) Then
                Me.BackButton = CType(target,System.Windows.Controls.Button)
                Return
            End If
            If (connectionId = 5) Then
                Me.OpenButton = CType(target,System.Windows.Controls.Button)
                Return
            End If
            Me._contentLoaded = true
        End Sub
    End Class
End Namespace

