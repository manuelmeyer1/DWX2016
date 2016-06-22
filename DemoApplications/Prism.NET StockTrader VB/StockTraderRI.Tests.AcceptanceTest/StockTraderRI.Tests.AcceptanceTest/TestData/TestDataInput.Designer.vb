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
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System

Namespace StockTraderRI.Tests.AcceptanceTest.TestData


    ''' <summary>
    '''   A strongly-typed resource class, for looking up localized strings, etc.
    ''' </summary>
    ' This class was auto-generated by the StronglyTypedResourceBuilder
    ' class via a tool like ResGen or Visual Studio.
    ' To add or remove a member, edit your .ResX file then rerun ResGen
    ' with the /str option, or rebuild your VS project.
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Friend Class TestDataInput

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Friend Sub New()
        End Sub

        ''' <summary>
        '''   Returns the cached ResourceManager instance used by this class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As New Global.System.Resources.ResourceManager("TestDataInput", GetType(TestDataInput).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        ''' <summary>
        '''   Overrides the current thread's CurrentUICulture property for all
        '''   resource lookups using this strongly typed resource class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to -20.15.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultInvalidPriceLimit() As String
            Get
                Return ResourceManager.GetString("DefaultInvalidPriceLimit", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to -15.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultInvalidShares() As String
            Get
                Return ResourceManager.GetString("DefaultInvalidShares", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 20.15.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultPriceLimit() As String
            Get
                Return ResourceManager.GetString("DefaultPriceLimit", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 10.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultShares() As String
            Get
                Return ResourceManager.GetString("DefaultShares", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to End of day.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultTerm() As String
            Get
                Return ResourceManager.GetString("DefaultTerm", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 0.
        ''' </summary>
        Friend Shared ReadOnly Property DefaultValue() As String
            Get
                Return ResourceManager.GetString("DefaultValue", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 7.
        ''' </summary>
        Friend Shared ReadOnly Property PositionSummaryColumnCount() As String
            Get
                Return ResourceManager.GetString("PositionSummaryColumnCount", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 5.
        ''' </summary>
        Friend Shared ReadOnly Property PositionSummaryRowCount() As String
            Get
                Return ResourceManager.GetString("PositionSummaryRowCount", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Stock0.
        ''' </summary>
        Friend Shared ReadOnly Property StockName() As String
            Get
                Return ResourceManager.GetString("StockName", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to 1.
        ''' </summary>
        Friend Shared ReadOnly Property WatchListRowCount() As String
            Get
                Return ResourceManager.GetString("WatchListRowCount", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
