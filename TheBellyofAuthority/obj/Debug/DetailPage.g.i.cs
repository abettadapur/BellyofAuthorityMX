﻿

#pragma checksum "C:\Users\alexb_000\Documents\Visual Studio 2012\Projects\TheBellyofAuthority\TheBellyofAuthority\DetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "28D13D3FFF5A86EA7490A75553CE54C3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace TheBellyofAuthority
{
    partial class DetailPage : TheBellyofAuthority.Common.LayoutAwarePage
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private TheBellyofAuthority.Common.LayoutAwarePage pageRoot; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.Button LikeButton; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.FlipView flipView; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.TextBlock contentView; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.Button backButton; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.TextBlock pageTitle; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.StackPanel UserPanel; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.TextBlock LabelBox; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.Controls.TextBlock UserBox; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.VisualStateGroup ApplicationViewStates; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.VisualState FullScreenLandscape; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.VisualState Filled; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.VisualState FullScreenPortrait; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private Windows.UI.Xaml.VisualState Snapped; 
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            Application.LoadComponent(this, new System.Uri("ms-appx:///DetailPage.xaml"), Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            pageRoot = (TheBellyofAuthority.Common.LayoutAwarePage)this.FindName("pageRoot");
            LikeButton = (Windows.UI.Xaml.Controls.Button)this.FindName("LikeButton");
            flipView = (Windows.UI.Xaml.Controls.FlipView)this.FindName("flipView");
            contentView = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("contentView");
            backButton = (Windows.UI.Xaml.Controls.Button)this.FindName("backButton");
            pageTitle = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("pageTitle");
            UserPanel = (Windows.UI.Xaml.Controls.StackPanel)this.FindName("UserPanel");
            LabelBox = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("LabelBox");
            UserBox = (Windows.UI.Xaml.Controls.TextBlock)this.FindName("UserBox");
            ApplicationViewStates = (Windows.UI.Xaml.VisualStateGroup)this.FindName("ApplicationViewStates");
            FullScreenLandscape = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenLandscape");
            Filled = (Windows.UI.Xaml.VisualState)this.FindName("Filled");
            FullScreenPortrait = (Windows.UI.Xaml.VisualState)this.FindName("FullScreenPortrait");
            Snapped = (Windows.UI.Xaml.VisualState)this.FindName("Snapped");
        }
    }
}


