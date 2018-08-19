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
        void Delete(int id);
        IEnumerable<FriendRequestDTO> GetFriendsRequests(string id);
        IEnumerable<FriendRequestDTO> GetMyFriendRequests(string id);
        void MakeFriendRequest(string RequestedTo_Id, string RequestedBy_Id);
        void AcceptFriend(FriendRequestDTO friend);
        void BlockUser(string RequestedTo_Id, string RequestedBy_Id);
        void UnBlockUser(string RequestedTo_Id, string RequestedBy_Id);
        void DeclineFriendRequest(FriendRequestDTO friendRequest);

    }
}
