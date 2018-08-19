using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        public bool IsRequestExist(string RequestedTo_Id, string RequestedBy_Id)
        {

            var request = _dbSet.FirstOrDefault(f => f.RequestedBy_Id == RequestedBy_Id
                             && f.RequestedTo.Id == RequestedTo_Id || f.RequestedBy_Id == RequestedTo_Id
                             && f.RequestedTo.Id == RequestedBy_Id);
            if (request == null)
                return false;
            else
                return true;
        }

    }
}
