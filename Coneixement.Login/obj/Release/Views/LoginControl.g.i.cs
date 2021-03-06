﻿#pragma checksum "..\..\..\Views\LoginControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DBEBBB73BA975CB42B45079BF7AA9216"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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


namespace Coneixement.Login.Views {
    
    
    /// <summary>
    /// LoginControl
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class LoginControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TopMessage;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock UserLabel;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserName;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PasswordLabel;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox RememberMe;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\LoginControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancleButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Coneixement.Login;component/views/logincontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\LoginControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TopMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.UserLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.UserName = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\Views\LoginControl.xaml"
            this.UserName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.UserName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PasswordLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 55 "..\..\..\Views\LoginControl.xaml"
            this.Password.KeyUp += new System.Windows.Input.KeyEventHandler(this.Password_KeyUp);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RememberMe = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\Views\LoginControl.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CancleButton = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\..\Views\LoginControl.xaml"
            this.CancleButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

