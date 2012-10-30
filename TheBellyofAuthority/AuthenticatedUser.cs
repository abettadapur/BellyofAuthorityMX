using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheBellyofAuthority
{
    class AuthenticatedUser
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string blog_id { get; set; }
        public string blog_url { get; set; }
    }
}
