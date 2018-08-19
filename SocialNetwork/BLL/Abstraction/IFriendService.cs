using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using DAL.Models;

namespace BLL.Abstraction
{
    public interface IFriendService
    {
        IEnumerable<UserDetailsDTO> GetAllMyFriends(string id, int pageIndex = 1, int pageSize = 50);
        void RemoveFriend(string userId, string friendId);

    }
}
