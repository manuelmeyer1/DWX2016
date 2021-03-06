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

Namespace StockTraderRI.Modules.Market.Properties


    ''' <summary>
    '''   A strongly-typed resource class, for looking up localized strings, etc.
    ''' </summary>
    ' This class was auto-generated by the StronglyTypedResourceBuilder
    ' class via a tool like ResGen or Visual Studio.
    ' To add or remove a member, edit your .ResX file then rerun ResGen
    ' with the /str option, or rebuild your VS project.
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(), _
    Global.Microsoft.VisualBasic.HideModuleNameAttribute()> _
    Friend Module Resources

        Private resourceMan As Global.System.Resources.ResourceManager

        Private resourceCulture As Global.System.Globalization.CultureInfo

        '        internal Resources()
        '        {
        '        }

        ''' <summary>
        '''   Returns the cached ResourceManager instance used by this class.
        ''' </summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As New Global.System.Resources.ResourceManager("Resources", GetType(Resources).Assembly)
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
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(ByVal value As System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        '''&lt;MarketItems xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns:xsd=&quot;http://www.w3.org/2001/XMLSchema&quot; RefreshRate=&quot;5&quot;&gt;
        '''  &lt;MarketItem TickerSymbol=&quot;STOCK0&quot; LastPrice=&quot;37.00&quot; Volume=&quot;1000&quot;&gt;&lt;/MarketItem&gt;
        '''  &lt;MarketItem TickerSymbol=&quot;STOCK1&quot; LastPrice=&quot;78.13&quot; Volume=&quot;100&quot;&gt;&lt;/MarketItem&gt;
        '''  &lt;MarketItem TickerSymbol=&quot;STOCK2&quot; LastPrice=&quot;23.80&quot; Volume=&quot;200&quot;&gt;&lt;/MarketItem&gt;
        '''  &lt;MarketItem TickerSymbol=&quot;STOCK3&quot; LastPrice=&quot;34.22&quot; Volume=&quot;300&quot;&gt;&lt;/MarketItem&gt;
        '''  &lt;MarketItem [rest of string was truncated]&quot;;.
        ''' </summary>
        Friend ReadOnly Property Market() As String
            Get
                Return ResourceManager.GetString("Market", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to Symbol does not exist in market feed..
        ''' </summary>
        Friend ReadOnly Property MarketFeedTickerSymbolNotFoundException() As String
            Get
                Return ResourceManager.GetString("MarketFeedTickerSymbolNotFoundException", resourceCulture)
            End Get
        End Property

        ''' <summary>
        '''   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
        '''&lt;MarketHistoryItems xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns:xsd=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        '''  &lt;MarketHistoryItem TickerSymbol=&quot;STOCK0&quot; Date=&quot;2008-02-23&quot;&gt;30.00&lt;/MarketHistoryItem&gt;
        '''  &lt;MarketHistoryItem TickerSymbol=&quot;STOCK0&quot; Date=&quot;2008-02-24&quot;&gt;35.00&lt;/MarketHistoryItem&gt;
        '''  &lt;MarketHistoryItem TickerSymbol=&quot;STOCK0&quot; Date=&quot;2008-02-25&quot;&gt;50.00&lt;/MarketHistoryItem&gt;
        '''  &lt;MarketHistoryItem TickerSymbol=&quot;STOCK0&quot; Date=&quot;2008-02-26&quot;&gt;75.00&lt;/MarketHistoryItem&gt;
        ''' [rest of string was truncated]&quot;;.
        ''' </summary>
        Friend ReadOnly Property MarketHistory() As String
            Get
                Return ResourceManager.GetString("MarketHistory", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
