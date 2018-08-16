using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsDTO
{
    public class FriendsDTO :EntityDTO
    {

        public string MeId { get; set; }
        public UserDetailsDTO Me { get; set; }

        public string FriendId { get; set; }
        public UserDetailsDTO Friend { get; set; }

        public string UserGroup { get; set; }
        public string Status { get; set; }
    }
}
