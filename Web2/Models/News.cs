using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class News
    {
        public int NewId { get; set; }
        public int TagId { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
