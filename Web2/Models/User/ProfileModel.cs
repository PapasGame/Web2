using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web2.Models.User
{
    public class ProfileModel
    {
        public List<Map> AllMap { get; set; }

        public List<UsersMap> SubsMap { get; set; }
    }
}
