using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialNetworkContext _context;
        private readonly DbSet<UserDetails> _dbSet;
        public UserRepository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<UserDetails>();
        }

        //public IEnumerable<UserDetails> GetAll()
        //{
        //    return _dbSet;
        //}

        public IEnumerable<UserDetails> GetAll(Expression<Func<UserDetails, bool>> predicate)
        {
            return _dbSet.Where(predicate.Compile());
        }

        public UserDetails GetById(string id)
        {
            return _dbSet.Find(id);

        }

        public void Insert(UserDetails item)
        {

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var check = _dbSet.Find(item.Id);
            if (check == null)
                _dbSet.Add(item);
        }

        public void Delete(string id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public void Update(UserDetails item)
        {
            var itemToModify = _dbSet.Find(item.Id);
            if (itemToModify == null)
                throw new ArgumentException(nameof(item));
            if(itemToModify.DateOfBirth<DateTime.MinValue)
                itemToModify.DateOfBirth = new DateTime(1960, 01,01);
            _context.Entry(itemToModify).CurrentValues.SetValues(item);
            _context.Entry(itemToModify).State = EntityState.Modified;
        }

        public IEnumerable<UserDetails> Search(DatabaseQueryParams parameters)
        {
            var query =
                from user in _context.Users
                where SqlFunctions.PatIndex("%"+parameters.FirstName+"%", user.FirstName) > 0 ||
                      SqlFunctions.PatIndex("%" + parameters.LastName+ "%", user.LastName) > 0 
                select user;
            return query.ToList();
        }
    }
}
