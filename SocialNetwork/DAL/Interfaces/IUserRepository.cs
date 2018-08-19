using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        //IEnumerable<UserDetails> GetAll();
        IEnumerable<UserDetails> GetAll(Expression<Func<UserDetails, bool>> predicate);
        UserDetails GetById(string id);
        void Insert(UserDetails item);
        void Delete(string id);
        void Update(UserDetails item);
    }
}
