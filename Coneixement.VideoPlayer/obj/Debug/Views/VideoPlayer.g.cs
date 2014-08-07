﻿#pragma checksum "..\..\..\Views\VideoPlayer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "921CBCCEF18C7AB703FFC7F67BDF4ACB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Coneixement.Infrastructure.Convertors;
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


namespace Coneixement.VideoPlayer.Views {
    
    
    /// <summary>
    /// VideoPlayer
    /// </summary>
    public partial class VideoPlayer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 13 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TopicListView;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Overlay;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement videoElement;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider timelineSlider;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock videoElementTime;
        
        #line default
        #line hidden
        
        
        #line 305 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image test;
        
        #line default
        #line hidden
        
        
        #line 316 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider volumeSlider;
        
        #line default
        #line hidden
        
        
        #line 333 "..\..\..\Views\VideoPlayer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider speedRatioSlider;
        
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
            System.Uri resourceLocater = new System.Uri("/Coneixement.VideoPlayer;component/views/videoplayer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\VideoPlayer.xaml"
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
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.TopicListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            this.Overlay = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.videoElement = ((System.Windows.Controls.MediaElement)(target));
            
            #line 244 "..\..\..\Views\VideoPlayer.xaml"
            this.videoElement.Loaded += new System.Windows.RoutedEventHandler(this.videoElement_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.timelineSlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 7:
            this.videoElementTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.test = ((System.Windows.Controls.Image)(target));
            
            #line 303 "..\..\..\Views\VideoPlayer.xaml"
            this.test.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownPlayMedia);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 308 "..\..\..\Views\VideoPlayer.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownPauseMedia);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 311 "..\..\..\Views\VideoPlayer.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDownStopMedia);
            
            #line default
            #line hidden
            return;
            case 11:
            this.volumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 318 "..\..\..\Views\VideoPlayer.xaml"
            this.volumeSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ChangeMediaVolume);
            
            #line default
            #line hidden
            return;
            case 12:
            this.speedRatioSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 335 "..\..\..\Views\VideoPlayer.xaml"
            this.speedRatioSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ChangeMediaSpeedRatio);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseLeftButtonUpEvent;
            
            #line 168 "..\..\..\Views\VideoPlayer.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListViewItem_MouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

