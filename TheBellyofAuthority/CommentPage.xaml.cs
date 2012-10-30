using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Callisto.Controls;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TheBellyofAuthority
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class CommentPage : TheBellyofAuthority.Common.LayoutAwarePage
    {
        ObservableCollection<Comment> comments = new ObservableCollection<Comment>();
        ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
        string displayname = Authenticator.displayname;
        TextBox editBox;
        Flyout flyout;
        string postID = "";


        public CommentPage()
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
            string postid = navigationParameter as string;
            postID = postid;
            await getComments();

            commentThread.ItemsSource = comments;
            if (settings.Values.ContainsKey("AuthUser"))
            {

                //Authenticator.postStatus((string)settings.Values["AuthUser"]);
                displayname = Authenticator.displayname;
                LabelBox.Text = "Signed in as:";
                UserBox.Text = displayname;
            }

            else
            {
                LabelBox.Text = "Sign in to Wordpress";
                UserBox.Text = "";
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
        
        private async Task getComments()
        {
            WebRequest wr = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + postID + "/replies");
            WebResponse response = await wr.GetResponseAsync();
            Stream objResponse = response.GetResponseStream();
            StreamReader reader = new StreamReader(objResponse);
            string data = reader.ReadToEnd();
            CommentWrapper comments = JsonConvert.DeserializeObject<CommentWrapper>(data);
            ObservableCollection<Comment> processedComments = new ObservableCollection<Comment>();
            foreach (Comment c in comments.comments)
            {
                processedComments.Add(processComment(c));

            }

            this.comments = new ObservableCollection<Comment>(processedComments.Reverse());
        }
        
        private Comment processComment(Comment c)
        {
            c.date = parseDate(c.date);
            c.author.URL = fixHTML(c.author.URL);
            c.author.avatar_URL = fixHTML(c.author.avatar_URL);
            c.content = parseHTML(c.content);
            c.content = removeCharacters(c.content);
            return c;

        }

        private string parseHTML(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            return doc.DocumentNode.InnerText;
        }
        
        private string parseDate(string date)
        {
            string year = date.Substring(0, 4);
            string month = date.Substring(5, 2);
            string day = date.Substring(8, 2);
            return month + "/" + day + "/" + year;
        }
        
        private string removeCharacters(string item)
        {
            return System.Net.WebUtility.HtmlDecode(item);
        }
        
        private string fixHTML(string item)
        {
            return item.Replace("\\/", "/");
        }
        
        private async void postComment(object sender, RoutedEventArgs e)
        {

            bool authenticated = true;
            if (!settings.Values.ContainsKey("AuthUser"))
            {

                authenticated = await authenticateUser();

            }
            if (authenticated == true)
            {
                progBar.Visibility = Visibility.Visible;
                string comment = "content=" + commentBox.Text;
                comment = WebUtility.HtmlEncode(comment);

                WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + postID + "/replies/new");
                string token = settings.Values["AuthUser"] as string;
                request.Headers["Authorization"] = "Bearer " + token;
                request.Method = "POST";
                Stream requestStream = await request.GetRequestStreamAsync();
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] data = Encoding.UTF8.GetBytes(comment);
                requestStream.Write(data, 0, data.Length);




                WebResponse response = await request.GetResponseAsync();
                Stream objStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(objStream);
                string responsestr = reader.ReadToEnd();
                progBar.Visibility = Visibility.Collapsed;
                Comment newComment = JsonConvert.DeserializeObject<Comment>(responsestr);
                newComment = processComment(newComment);

                comments.Add(newComment);
                commentBox.Text = "";
            }

        }

        private async Task<bool> authenticateUser()
        {
            bool success = await Authenticator.authenticateUser();
            //await getLike();
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("AuthUser"))
            {
                displayname = Authenticator.displayname;
                LabelBox.Text = "Signed in as:";
                UserBox.Text = displayname;


            }
            return success;
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
                displayname = "";
                LabelBox.Text = "Sign in to Wordpress";
                UserBox.Text = "";

            }
        }

        private void commentThread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            commentBar.IsOpen = true;
        }

        private void commentBar_Opened_1(object sender, object e)
        {
            if (commentThread.SelectedIndex == -1)
                commentThread.SelectedIndex = 0;
        }

        private async void deleteComment(object sender, RoutedEventArgs e)
        {
            bool authenticated = true;
            if (!settings.Values.ContainsKey("AuthUser"))
            {

                authenticated = await authenticateUser();

            }
            if (authenticated == true)
            {
                Comment selectedComment = (Comment)commentThread.SelectedItem;
                string userID = settings.Values["AuthUserID"] as string;
                if (selectedComment.author.ID == userID)
                {
                    progBar.Visibility = Visibility.Visible;
                    string comment = "content=" + commentBox.Text;
                    comment = WebUtility.HtmlEncode(comment);

                    WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/comments/" + selectedComment.ID + "/delete");
                    string token = settings.Values["AuthUser"] as string;
                    request.Headers["Authorization"] = "Bearer " + token;
                    request.Method = "POST";
                    Stream requestStream = await request.GetRequestStreamAsync();
                    request.ContentType = "application/x-www-form-urlencoded";
                    byte[] data = Encoding.UTF8.GetBytes(comment);
                    requestStream.Write(data, 0, data.Length);




                    WebResponse response = await request.GetResponseAsync();
                    comments.Remove(selectedComment);
                    progBar.Visibility = Visibility.Collapsed;



                }
                else
                {
                    MessageDialog md = new MessageDialog("You are not the author of this comment and can't delete it.", "Cannot Delete");
                   await md.ShowAsync();
                }
            }
        }

        private void editComment(object sender, RoutedEventArgs e)
        {
            var element = commentThread.ItemContainerGenerator.ContainerFromItem(commentThread.SelectedItem) as UIElement;
            flyout = new Flyout();
            flyout.Background = new SolidColorBrush(Colors.Black);
            StackPanel sp = new StackPanel();

            editBox = new TextBox() { Width = 300, Height = 100, Padding = new Thickness(5, 0, 0, 0), Text = (commentThread.SelectedItem as Comment).content, TextWrapping = TextWrapping.Wrap };
            sp.Children.Add(editBox);
            Button b = new Button();
            b.Content = "Edit Comment";
            b.Click += postEdit;
            sp.Children.Add(b);
            flyout.Content = sp;
            flyout.PlacementTarget = element;
            flyout.IsOpen = true;




        }
        
        private async void postEdit(object sender, RoutedEventArgs e)
        {
            Comment selectedComment = commentThread.SelectedItem as Comment;
            bool authenticated = true;
            if (!settings.Values.ContainsKey("AuthUser"))
            {

                authenticated = await authenticateUser();

            }
            if (authenticated == true)
            {
                if (selectedComment.author.ID == (string)settings.Values["AuthUserID"])
                {
                    progBar.Visibility = Visibility.Visible;
                    string comment = "content=" + editBox.Text;
                    comment = WebUtility.HtmlEncode(comment);

                    WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/comments/" + selectedComment.ID);
                    string token = settings.Values["AuthUser"] as string;
                    request.Headers["Authorization"] = "Bearer " + token;
                    request.Method = "POST";
                    Stream requestStream = await request.GetRequestStreamAsync();
                    request.ContentType = "application/x-www-form-urlencoded";
                    byte[] data = Encoding.UTF8.GetBytes(comment);
                    requestStream.Write(data, 0, data.Length);




                    WebResponse response = await request.GetResponseAsync();
                    Stream objStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(objStream);
                    string responsestr = reader.ReadToEnd();
                    progBar.Visibility = Visibility.Collapsed;
                    Comment editedComment = JsonConvert.DeserializeObject<Comment>(responsestr);
                    editedComment = processComment(editedComment);

                    int index = comments.IndexOf(selectedComment);
                    comments.Remove(selectedComment);
                    comments.Insert(index, editedComment);
                    flyout.IsOpen = false;


                }
                else
                {
                    MessageDialog md = new MessageDialog("You are not the author of this comment and can't edit it.", "Cannot Edit");
                    await md.ShowAsync();
                    flyout.IsOpen = false;

                }

            }
        }



    }
}
