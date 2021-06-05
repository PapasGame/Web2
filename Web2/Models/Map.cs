using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class Map
    {
        public Map()
        {
            UsersMaps = new HashSet<UsersMap>();
        }

        public int MapId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Access { get; set; }

        public virtual ICollection<UsersMap> UsersMaps { get; set; }
    }
}
