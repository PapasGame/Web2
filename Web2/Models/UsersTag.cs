using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class UsersTag
    {
        public int UsersTagId { get; set; }
        public int TagId { get; set; }
        public int UserId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
    }
}
