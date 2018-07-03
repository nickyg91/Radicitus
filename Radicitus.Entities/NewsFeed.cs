using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;

namespace Radicitus.Entities
{
    public class NewsFeed
    {
        private string _content;
        public int NewsFeedId { get; set; }
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content
        {
            get => WebUtility.HtmlEncode(_content);
            set => _content = WebUtility.HtmlDecode(value);
        }
    }
}
