﻿#pragma checksum "..\..\..\Controls\OldVersionMessage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89D45C1C3D238557A23B89C8B8C2C458"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Microsoft.FamilyShow {
    
    
    /// <summary>
    /// OldVersionMessage
    /// </summary>
    public partial class OldVersionMessage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.FamilyShow.OldVersionMessage OldVersionMessageControl;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Header;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HeaderTextBlock;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ContentGrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox DontShowCheckBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Footer;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Controls\OldVersionMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ContinueButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FamilyShow;component/controls/oldversionmessage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\OldVersionMessage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.OldVersionMessageControl = ((Microsoft.FamilyShow.OldVersionMessage)(target));
            return;
            case 2:
            this.Header = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.HeaderTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ContentGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.DontShowCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.Footer = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.ContinueButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Controls\OldVersionMessage.xaml"
            this.ContinueButton.Click += new System.Windows.RoutedEventHandler(this.ContinueButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
