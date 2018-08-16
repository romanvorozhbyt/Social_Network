using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDetails> GetAll();
        //IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        UserDetails GetById(string id);
        void Insert(UserDetails item);
        void Delete(String id);
        void Update(UserDetails item);
    }
}
