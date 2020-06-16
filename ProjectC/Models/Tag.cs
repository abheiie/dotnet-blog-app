using System;
using System.Collections.Generic;

namespace ProjectC.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
