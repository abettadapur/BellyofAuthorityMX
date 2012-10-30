using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Foundation;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Popups;

namespace TheBellyofAuthority
{
    class Authenticator
    {
      public static AuthenticatedUser user { get; set; }
      //public static string access_token { get; set; }
      public static string displayname { get; set; }

        public static async Task<bool> authenticateUser()
        {

            String WordpressURL = "https://public-api.wordpress.com/oauth2/authorize?client_id=428&redirect_uri=http://127.0.0.1&response_type=code";


            System.Uri StartUri = new Uri(WordpressURL, UriKind.RelativeOrAbsolute);
            System.Uri EndUri = new Uri("http://127.0.0.1", UriKind.RelativeOrAbsolute);



            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(WebAuthenticationResult.ResponseData.ToString());
                string authtoken = decoder.First().Value;
                string post_string = FormatPostRequest(authtoken);
                var content = new StringContent(post_string);
                var client = new System.Net.Http.HttpClient();
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                var responseTask = client.PostAsync("https://public-api.wordpress.com/oauth2/token", content);
                var response = await responseTask;
                string jsonresponse = await response.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<AuthenticatedUser>(jsonresponse);
       
               ApplicationData.Current.LocalSettings.Values["AuthUser"] = Authenticator.user.access_token;

               postStatus(user.access_token);
               displayname = await userInfo(user.access_token);
               Authenticator.displayname = displayname;
               return true;
               


            }
            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private static string FormatPostRequest(string code)
        {

            StringBuilder paramterBuilder = new StringBuilder();

            paramterBuilder.AppendFormat("{0}={1}&", "client_id", WebUtility.UrlEncode("428"));
            paramterBuilder.AppendFormat("{0}={1}&", "redirect_uri", "http://127.0.0.1");
            paramterBuilder.AppendFormat("{0}={1}&", "client_secret", WebUtility.UrlEncode("hhxpKFt0crWFLXMifeN6gcfsbG2O2EzRZ5G2r1z3mtcOkZrl8MA4KGhOJIaETWlV"));
            paramterBuilder.AppendFormat("{0}={1}&", "code", WebUtility.UrlEncode(code));
            paramterBuilder.AppendFormat("{0}={1}", "grant_type", WebUtility.UrlEncode("authorization_code"));


            return paramterBuilder.ToString();
        }
        public async static void postStatus(string token)
        {
            
            SiteDataSource source = (SiteDataSource)App.Current.Resources["siteDataSource"];
            foreach (jsonpost p in source.Posts)
            {
                
                WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/" + p.ID + "/likes/mine/");
                request.Headers["Authorization"] = "Bearer " + token;
                WebResponse response = await request.GetResponseAsync();
                Stream objStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(objStream);
                string responsestr = reader.ReadToEnd();
             MyLike like = JsonConvert.DeserializeObject<MyLike>(responsestr);
             if (like.i_like)
             {
                
                 p.liked = true;
             }
            }
        }
        public async static Task<string> userInfo(string token)
        {
            WebRequest request = WebRequest.Create("https://public-api.wordpress.com/rest/v1/me");
            request.Headers["Authorization"] = "Bearer " + token;
            WebResponse response = await request.GetResponseAsync();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responsestr = reader.ReadToEnd();
            UserResponse tempuser = JsonConvert.DeserializeObject<UserResponse>(responsestr);
            ApplicationData.Current.LocalSettings.Values["AuthUserID"] = tempuser.ID;
            return tempuser.display_name;
        }
        public static void clearPosts()
        {
            SiteDataSource source = (SiteDataSource)App.Current.Resources["siteDataSource"];
            foreach (jsonpost p in source.Posts)
            {
                p.liked = false;
            }
        }
    }
  
}
