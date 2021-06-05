using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class UsersMap
    {
        public int UsersMapId { get; set; }
        public int MapId { get; set; }
        public int UserId { get; set; }
        public string Progress { get; set; }

        public virtual Map Map { get; set; }
        public virtual User User { get; set; }
    }
}
