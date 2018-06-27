using System;
using System.Collections.Generic;
using System.Text;

namespace Radicitus.Entities
{
    public class NewsFeed
    {
        public int NewsFeedId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Content { get; set; }
    }
}
