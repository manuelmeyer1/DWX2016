﻿#ExternalChecksum("D:\__TEMPNODELETE\DDC2015\Prism.NET StockTrader VB - Starter\Silverlight\StockTraderRI.Modules.Position.Silverlight\Orders\OrdersView.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","EB1B113011057511A946FFB2EFB736AB")
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

Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Shapes
Imports System.Windows.Threading


Namespace StockTraderRI.Modules.Position.Orders
    
    <Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
    Partial Public Class OrdersView
        Inherits System.Windows.Controls.UserControl
        
        Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
        
        Friend WithEvents OrdersItemsControl As System.Windows.Controls.ItemsControl
        
        Private _contentLoaded As Boolean
        
        '''<summary>
        '''InitializeComponent
        '''</summary>
        <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Sub InitializeComponent()
            If _contentLoaded Then
                Return
            End If
            _contentLoaded = true
            System.Windows.Application.LoadComponent(Me, New System.Uri("/StockTraderRI.Modules.Position;component/Orders/OrdersView.xaml", System.UriKind.Relative))
            Me.LayoutRoot = CType(Me.FindName("LayoutRoot"),System.Windows.Controls.Grid)
            Me.OrdersItemsControl = CType(Me.FindName("OrdersItemsControl"),System.Windows.Controls.ItemsControl)
        End Sub
    End Class
End Namespace

