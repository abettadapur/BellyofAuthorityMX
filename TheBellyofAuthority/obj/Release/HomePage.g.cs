﻿

#pragma checksum "C:\Users\Alex\SkyDrive\Documents\Visual Studio 2012\Projects\TheBellyofAuthority\TheBellyofAuthority\HomePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5FCFEDD6A0E33E2C6BC8578463B250C8"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheBellyofAuthority
{
    partial class HomePage : global::TheBellyofAuthority.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 47 "..\..\HomePage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.loadPosts;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 101 "..\..\HomePage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 121 "..\..\HomePage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.itemGridView_ItemClick_1;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 76 "..\..\HomePage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 79 "..\..\HomePage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.UserPanel_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


