using System;
using System.Collections.Generic;

#nullable disable

namespace Web2
{
    public partial class User
    {
        public User()
        {
            UsersMaps = new HashSet<UsersMap>();
            UsersTags = new HashSet<UsersTag>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UsersMap> UsersMaps { get; set; }
        public virtual ICollection<UsersTag> UsersTags { get; set; }
    }
}
