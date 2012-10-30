using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBellyofAuthority
{
    class LikeWrapper
    {
        public bool success { get; set; }
        public bool i_like { get; set; }
        public int like_count { get; set; }
    }
    class MyLike
    {
        
        public bool i_like { get; set; }
    }
    class UserResponse
    {
       
        public string display_name { get; set; }
        public string ID { get; set; }
    }
    class CommentWrapper
    {
        public Comment[] comments { get; set; }
    }
    class Comment
    {
        public string content { get; set; }
        public string date { get; set; }
        public jsonauthor author { get; set; }
        public string ID { get; set; }
    }
}
