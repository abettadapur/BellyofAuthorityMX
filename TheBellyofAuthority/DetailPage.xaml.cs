using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Automation;
using Windows.UI;
using Callisto.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TheBellyofAuthority
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class DetailPage : TheBellyofAuthority.Common.LayoutAwarePage
    {
        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
       public static jsonpost currentpost{get;set;}
        
      
     
        string displayname;
        CommentWrapper commentWrapper;
        public DetailPage()
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
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            currentpost = navigationParameter as jsonpost;
            //this.webView.Navigate(new Uri(post.url));
       
          
            contentView.Text = currentpost.content;
            pageTitle.Text = currentpost.title;
            flipView.Items.Clear();
            //foreach (string s in currentpost.images)
            //{
            //    Image image = new Image();
            //    BitmapImage bmp = new BitmapImage();
            //    bmp.UriSource = new Uri(s);
            //    image.Source = bmp;
            //    flipView.Items.Add(image);
            //}
            flipView.ItemsSource = currentpost.images;

            if (settings.Values.ContainsKey("AuthUser"))
            {
                await getLike();
                //Authenticator.postStatus((string)settings.Values["AuthUser"]);
                displayname = Authenticator.displayname;
                LabelBox.Text = "Signed in";
                UserBox.Text = displayname;
            }
            
            else
            {
                LabelBox.Text = "Sign in";
                UserBox.Text = "to Wordpress";
            }
            await getComments();
            if (currentpost.liked)
            {
                LikeButton.SetValue(AutomationProperties.NameProperty, "Liked");
            }
         
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void likePost(object sender, RoutedEventArgs e)
        {
            bool success = true;
            if (!settings.Values.ContainsKey("AuthUser"))
            {
               success= await authenticateUser();
            }
            if (success)
            {
                if (currentpost.liked)
                {
                    MessageDialog md = new MessageDialog("You already like this post.");
                   await md.ShowAsync();
                }
                else if (settings.Values.ContainsKey("AuthUser"))
                {
                    LikeButton.SetValue(AutomationProperties.NameProperty, "Liking...");
                    WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + currentpost.ID + "/likes/new");
                    string token = settings.Values["AuthUser"] as string;
                    request.Headers["Authorization"] = "Bearer " + token;

                    request.Method = "POST";
                    WebResponse response = await request.GetResponseAsync();
                    Stream objStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(objStream);
                    string responsestr = reader.ReadToEnd();
                    LikeWrapper like = JsonConvert.DeserializeObject<LikeWrapper>(responsestr);
                    if (like.success)
                    {
                        LikeButton.SetValue(AutomationProperties.NameProperty, "Liked");
                        // AutomationProperties.NameProperty; 
                    }
                }
            }
            
        }

        private void commentPost(object sender, RoutedEventArgs e)
        {
          
            this.Frame.Navigate(typeof(CommentPage), currentpost.ID);







        }
      

        private async void UserPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var settings = ApplicationData.Current.LocalSettings;

            if (settings.Values.ContainsKey("AuthUser"))
            {
                MessageDialog md = new MessageDialog("You are currently logged in as " + displayname + ". Would you like to log out?");
                md.Commands.Add(new UICommand("Log out", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                md.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CommandInvokedHandler)));
                await md.ShowAsync();

            }
            else
            {
                await authenticateUser();
            }
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            if (command.Label == "Log out")
            {
                ApplicationData.Current.LocalSettings.Values.Remove("AuthUser");
                ApplicationData.Current.LocalSettings.Values.Remove("AuthUserID");
                Authenticator.clearPosts();
                LikeButton.SetValue(AutomationProperties.NameProperty, "Like This Post");
                displayname = "";
                LabelBox.Text = "Sign in";
                UserBox.Text = "to Wordpress";

            }

        }
        private async Task getComments()
        {
            WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + currentpost.ID+"/replies/");
            request.Method = "GET";
            WebResponse response = await request.GetResponseAsync();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responsestr = reader.ReadToEnd();
            commentWrapper = JsonConvert.DeserializeObject<CommentWrapper>(responsestr);
        }
        private async Task<bool> authenticateUser()
        {
           bool success =  await Authenticator.authenticateUser();
            
            if (success)
            {
                await getLike();
                displayname = Authenticator.displayname;
                LabelBox.Text = "Signed in as:";
                UserBox.Text = displayname;
               
                      
            }
            return success;
        }
        private async Task getLike()
        {
            WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + currentpost.ID + "/likes/mine/");
            request.Headers["Authorization"] = "Bearer " + settings.Values["AuthUser"];
            WebResponse response = await request.GetResponseAsync();
            Stream objStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(objStream);
            string responsestr = reader.ReadToEnd();
            MyLike like = JsonConvert.DeserializeObject<MyLike>(responsestr);
            if (like.i_like)
            {

                currentpost.liked = true;
                LikeButton.SetValue(AutomationProperties.NameProperty, "Liked");
                
            }
        }

      
    


    }
}
