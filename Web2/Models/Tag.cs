using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class Tag
    {
        public Tag()
        {
            News = new HashSet<News>();
            UsersTags = new HashSet<UsersTag>();
        }

        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<UsersTag> UsersTags { get; set; }
    }
}
