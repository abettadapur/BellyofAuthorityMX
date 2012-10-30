using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace TheBellyofAuthority
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class HomePage : TheBellyofAuthority.Common.LayoutAwarePage
    {
        string displayname;
        public HomePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override async void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
         //   bool connectionfailed=false;
            try
            {
                SiteDataSource siteDataSource = (SiteDataSource)App.Current.Resources["siteDataSource"];
                if (siteDataSource != null)
                {
                    if (siteDataSource.Posts.Count == 0)
                    {

                        await siteDataSource.retrieveData();
                    }


                    this.DefaultViewModel["Items"] = siteDataSource.Posts;
                    FeedProgressBar.Visibility = Visibility.Collapsed;
                    var settings = ApplicationData.Current.LocalSettings;
                    if (settings.Values.ContainsKey("AuthUser"))
                    {

                        Authenticator.postStatus((string)settings.Values["AuthUser"]);
                        displayname = await Authenticator.userInfo((string)settings.Values["AuthUser"]);
                        LabelBox.Text = "Signed in";
                        UserBox.Text = displayname;
                        Authenticator.displayname = displayname;

                    }

                    else
                    {
                        LabelBox.Text = "Sign in ";
                        UserBox.Text = "to Wordpress";
                    }
                    // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
                }
            }
            catch (WebException wex)
            {
                MessageDialog md = new MessageDialog("We could not load any posts");
                md.ShowAsync();
                FeedProgressBar.Visibility = Visibility.Collapsed;
            }

        }
   

        private void itemGridView_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(DetailPage), e.ClickedItem);
        }

        private async void UserPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;
            
                if (settings.Values.ContainsKey("AuthUser"))
                {
                    MessageDialog md = new MessageDialog("You are currently logged in as " + displayname + ". Would you like to log out?");
                    md.Commands.Add(new UICommand("Log out", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                    md.Commands.Add(new UICommand("Cancel",new UICommandInvokedHandler(this.CommandInvokedHandler)));
                   await md.ShowAsync();

                }
                else
                {
                    bool success = await  Authenticator.authenticateUser();
                    //Authenticator.postStatus((string)(settings.Values["AuthUser"]));
                    if (success)
                    {
                        displayname = await Authenticator.userInfo((string)(settings.Values["AuthUser"]));
                        LabelBox.Text = "Signed in as:";
                        UserBox.Text = displayname;
                    }
                }
            
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Log out")
            {
                ApplicationData.Current.LocalSettings.Values.Remove("AuthUser");
                ApplicationData.Current.LocalSettings.Values.Remove("AuthUserID");
                Authenticator.clearPosts();
                displayname = "";
                LabelBox.Text = "Sign in";
                UserBox.Text = "to Wordpress";

            }

        }

        private async void loadPosts(object sender, RoutedEventArgs e)
        {
            try
            {
                FeedProgressBar.Visibility = Visibility.Visible;
                SiteDataSource siteDataSource = (SiteDataSource)App.Current.Resources["siteDataSource"];
                if (siteDataSource != null)
                {
                    if (siteDataSource.Posts.Count == 0)
                    {

                        await siteDataSource.retrieveData();
                    }


                    this.DefaultViewModel["Items"] = siteDataSource.Posts;
                    FeedProgressBar.Visibility = Visibility.Collapsed;
                    var settings = ApplicationData.Current.LocalSettings;
                    if (settings.Values.ContainsKey("AuthUser"))
                    {

                        Authenticator.postStatus((string)settings.Values["AuthUser"]);
                        displayname = await Authenticator.userInfo((string)settings.Values["AuthUser"]);
                        LabelBox.Text = "Signed in";
                        UserBox.Text = displayname;
                        Authenticator.displayname = displayname;

                    }

                    else
                    {
                        LabelBox.Text = "Sign in ";
                        UserBox.Text = "to Wordpress";
                    }
                    // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
                }
            }
            catch (WebException wex)
            {
                MessageDialog md = new MessageDialog("We could not load any posts");
                md.ShowAsync();
                FeedProgressBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
