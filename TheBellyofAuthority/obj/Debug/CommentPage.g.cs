﻿

#pragma checksum "C:\Users\Alex\Documents\Visual Studio 2012\Projects\TheBellyofAuthority\TheBellyofAuthority\CommentPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8A4E9DC879B0CC4CF48947F5DD90D34A"
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
    partial class CommentPage : TheBellyofAuthority.Common.LayoutAwarePage, IComponentConnector
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 22 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.AppBar)(target)).Opened += this.commentBar_Opened_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 24 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.deleteComment;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 25 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.editComment;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 59 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.commentThread_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 105 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.postComment;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 48 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 50 "..\..\CommentPage.xaml"
                ((Windows.UI.Xaml.UIElement)(target)).Tapped += this.UserPanel_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

