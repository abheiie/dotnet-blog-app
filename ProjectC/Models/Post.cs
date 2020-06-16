using System;
using System.Collections.Generic;

namespace ProjectC.Models
{
    public partial class Post
    {
        public Post()
        {
            Tag = new HashSet<Tag>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual ICollection<Tag> Tag { get; set; }
    }
}
