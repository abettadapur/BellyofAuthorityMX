﻿

#pragma checksum "C:\Users\Alex\SkyDrive\Documents\Visual Studio 2012\Projects\TheBellyofAuthority\TheBellyofAuthority\DetailPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DF2360B247842F67BF1CF994D49073AC"
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
    partial class DetailPage : global::TheBellyofAuthority.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 25 "..\..\DetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.likePost;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 26 "..\..\DetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.commentPost;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 53 "..\..\DetailPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GoBack;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 56 "..\..\DetailPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.UserPanel_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


