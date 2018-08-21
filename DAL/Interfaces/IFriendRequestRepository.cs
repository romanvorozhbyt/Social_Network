using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        bool IsRequestExist(string requestedToId, string requestedById);
        FriendRequest FindFriendRequest(string firstUserId, string secondUserId, FriendRequestFlag flag);

    }
}
