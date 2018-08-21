using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ModelsDTO
{
    public class FriendRequestDTO :EntityDTO
    {

        public string RequestedBy_Id { get; set; }
        public UserDetailsDTO RequestedBy { get; set; }

        public UserDetailsDTO RequestedTo { get; set; }

        public DateTime? RequestTime { get; set; }

        public FriendRequestFlagDTO FriendRequestFlag { get; set; }
    }


    public enum FriendRequestFlagDTO
    {
        None,
        Approved,
        Declined,
        Blocked,
        Spam
    };
}
