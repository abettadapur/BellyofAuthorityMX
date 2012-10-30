using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace TheBellyofAuthority
{
    class SiteDataSourceRSS
    {
        private ObservableCollection<Post> _Posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> Posts
        { get { return this._Posts; } }

        public async Task retrieveData()
        {
            Task<Post> siteData = GetFeedAsync("http://bellyofauthority.wordpress.com/feed/");
        }
        private async Task<Post> GetFeedAsync(string url)
        {

            SyndicationClient client = new SyndicationClient();
            Uri uri = new Uri(url);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(uri);

                foreach (SyndicationItem item in feed.Items)
                {
                    Post post = new Post();
                    post.title = item.Title.Text;
                    post.content = item.Content.Text;
                    post.url = item.Links.FirstOrDefault().ToString();
                    post.date = item.PublishedDate.ToString();
                }
            }
        }


    }
    class Post
    {
        public string date { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string image { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string ID { get; set; }
    }
   
}
