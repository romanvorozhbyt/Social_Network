using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace BLL.Abstraction
{
    public interface IFriendRequestService
    {


        FriendRequestDTO GetById(int id);
        IEnumerable<FriendRequestDTO> GetFriendsRequests(string id);
        void MakeFriendRequest(string requestedToId, string requestedById);
        void AcceptFriend(FriendRequestDTO friend);
        void BlockUser(string requestedToId, string requestedById);
        void UnBlockUser(string requestedToId, string requestedById);
        void DeclineFriendRequest(FriendRequestDTO friendRequest);

    }
}
