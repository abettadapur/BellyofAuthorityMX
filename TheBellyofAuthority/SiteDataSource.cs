using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TheBellyofAuthority
{
    class SiteDataSource
    {
        private ObservableCollection<jsonpost> _Posts = new ObservableCollection<jsonpost>();
        public ObservableCollection<jsonpost> Posts
        {
            get { return this._Posts; }
        }
        public async Task retrieveData()
        {
            Task<jsonpost[]> site = GetFeedAsync("https://public-api.wordpress.com/rest/v1/sites/bellyofauthority.wordpress.com/posts/?number=100");
            processData(await site);
      

        }
        private async Task<jsonpost[]> GetFeedAsync(string url)
        {
            WebRequest wr = WebRequest.Create(url);
            WebResponse response = await wr.GetResponseAsync();
            Stream objResponse = response.GetResponseStream();
            StreamReader reader = new StreamReader(objResponse);
            string data = reader.ReadToEnd();
            jsonwrapper wrapper = JsonConvert.DeserializeObject<jsonwrapper>(data);
            var posts = wrapper.posts;
            return posts;
        }
        private void processData(jsonpost[] posts)
        {
            for (int i = 0; i < posts.Length; i++)
            {
                jsonpost selPost = posts[i];
                selPost.date = parseDate(selPost.date);
                selPost.title = removeCharacters(selPost.title);
                selPost.url = fixHTML(selPost.url);
                selPost.content = fixHTML(selPost.content);
                selPost.content=parseHTML(selPost,selPost.content);
                selPost.content = removeCharacters(selPost.content);
                this.Posts.Add(selPost);
            }
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
        private string parseHTML(jsonpost post, string item)
        {
            string output = "" ;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(item);
            List<HtmlNode> toRemove = new List<HtmlNode>();
            foreach (HtmlNode node in doc.DocumentNode.ChildNodes)
            {
                if (node.Name == "div")
                    toRemove.Add(node);

                else if (node.HasChildNodes)
                {
                    foreach (HtmlNode childnode in node.ChildNodes)
                    {
                       
                        if (childnode.Name == "a")
                        {
                            if (childnode.FirstChild.Name == "img")
                            {
                                jsonimage newimage = new jsonimage();
                                Image image = new Image();
                                BitmapImage bmp = new BitmapImage();
                                bmp.UriSource = new Uri(childnode.Attributes.FirstOrDefault().Value.ToString());
                                newimage.image = bmp;
                                post.images.Add(newimage);
                            }
                            
                            
                        }
                        if (childnode.Name == "div")
                        {
                            toRemove.Add(childnode);
                            //if (node.HasChildNodes)
                            //{
                            //    foreach (HtmlNode childnode2 in childnode.ChildNodes)
                            //    {

                            //        if (childnode2.Name == "a")
                            //        {
                            //            if (childnode2.FirstChild.Name == "img")
                            //            {
                            //                post.images.Add(childnode2.Attributes.FirstOrDefault().Value.ToString());
                            //            }


                            //        }
                            //    }
                            //}
                            
                        }
                    }
                }
            }
            foreach (HtmlNode node in toRemove)
            {
                jsonimage newimage = new jsonimage();
                foreach (HtmlNode childNode in node.ChildNodes)
                {
                    
                    if (childNode.Name == "a")
                    {
                        if (childNode.FirstChild.Name == "img")
                        {
                            Image image = new Image();
                            BitmapImage bmp = new BitmapImage();
                            bmp.UriSource = new Uri(childNode.Attributes.FirstOrDefault().Value.ToString());
                            newimage.image = bmp;
                        }
                    }
                    if (childNode.Name == "p")
                    {
                        newimage.caption=WebUtility.HtmlDecode(childNode.InnerText);
                    }

                }
                if (newimage.image != null)
                    post.images.Add(newimage);
                node.Remove();
            }
            output = doc.DocumentNode.InnerText;
            
            return output;
            
        }


    }
    public class jsonwrapper
    {
        public jsonpost[] posts { get; set; }
    }
   public class jsonpost
    {
        public jsonauthor author { get; set; }
        public string date { get; set; }
        public bool liked { get; set; }
        public int like_count { get; set; }
        public int comment_count { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string ID { get; set; }
        private ObservableCollection<jsonimage> _images = new ObservableCollection<jsonimage>();
        public ObservableCollection<jsonimage> images { get { return this._images; } }

    }
    public class jsonauthor
    {
        public string name { get; set; }
        public string avatar_URL { get; set; }
        public string ID { get; set; }
        public string URL { get; set; }
    }
    public class jsonimage
    {
        public BitmapImage image { get; set; }
        public string caption { get; set; }
    }


    class GetRequest
    {
       
    }

}
