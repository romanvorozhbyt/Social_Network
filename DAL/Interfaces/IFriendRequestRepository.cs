using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
   public interface IFriendRequestRepository
    {
        IEnumerable<FriendRequest> GetAll(Expression<Func<FriendRequest,bool>> predicate);
        FriendRequest GetById(int id);
        void Insert(FriendRequest item);
        void Delete(int id);
        void Update(FriendRequest friendRequest);
        bool IsRequestExist(string RequestedTo_Id, string RequestedBy_Id);

    }
}
