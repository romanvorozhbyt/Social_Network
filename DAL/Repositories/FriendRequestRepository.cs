using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly SocialNetworkContext _context;
        private readonly DbSet<FriendRequest> _dbSet;
        public FriendRequestRepository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<FriendRequest>();
        }

        public IEnumerable<FriendRequest> GetAll(Expression<Func<FriendRequest, bool>> predicate)
        {
            return _dbSet.Where(predicate.Compile());
        }

        public FriendRequest GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(FriendRequest item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var check = _dbSet.Find(item.Id);
            if (check == null)
                _dbSet.Add(item);
        }

        public void Update(FriendRequest friendRequest)
        {

            var itemToModify = _dbSet.Find(friendRequest.Id);
            if (itemToModify == null)
                throw new ArgumentException(nameof(friendRequest));
            _context.Entry(itemToModify).CurrentValues.SetValues(friendRequest);
            _context.Entry(itemToModify).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }


        public bool IsRequestExist(string requestedToId, string requestedById)
        {

            var request = _dbSet.FirstOrDefault(f => f.RequestedBy_Id == requestedById
                             && f.RequestedTo.Id == requestedToId || f.RequestedBy_Id == requestedToId
                             && f.RequestedTo.Id == requestedById);
            if (request == null)
                return false;
            else
                return true;
        }

        public FriendRequest FindFriendRequest(string firstUserId, string secondUserId, FriendRequestFlag flag)
        {
            var request = _dbSet.FirstOrDefault(fr => fr.RequestedBy_Id == firstUserId&& fr.RequestedTo.Id == secondUserId||
                fr.RequestedBy_Id == secondUserId && fr.RequestedTo.Id == firstUserId  & fr.FriendRequestFlag == flag);
            return request;
          
        }

    }
}
