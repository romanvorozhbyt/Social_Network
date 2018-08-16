using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        //IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(object id);
        void Insert(T item);
        void Delete(object id);
        void Update(T item);
    }
}
