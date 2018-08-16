using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace BLL.Abstraction
{
    public interface IFriendService
    {

        IEnumerable<FriendsDTO> GetAll();
        FriendsDTO GetById(int id);
        void Insert(FriendsDTO friend);
        void Delete(int id);
        void Update(FriendsDTO friend);
        IEnumerable<FriendsDTO> GetAllMyFriends(Guid id, int pageIndex, int pageSize );
        IEnumerable<FriendsDTO> GetFriendsRequests(Guid id);
        IEnumerable<FriendsDTO> GetMyFriendRequests(Guid id);
    }
}
