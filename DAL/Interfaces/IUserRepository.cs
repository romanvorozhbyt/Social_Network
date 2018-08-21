using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDetails> GetAll(Expression<Func<UserDetails, bool>> predicate);
        UserDetails GetById(string id);
        void Insert(UserDetails item);
        void Delete(string id);
        void Update(UserDetails item);
        IEnumerable<UserDetails> Search(DatabaseQueryParams parameters);
    }
}
