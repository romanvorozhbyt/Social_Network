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

namespace DAL
{
    public class Repository<T> :IRepository<T> where T: Entity
    {
        private readonly SocialNetworkContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Delete(object id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate) =>  _dbSet.Where(predicate.Compile());

        public T GetById(object id) => _dbSet.Find(id);

        public void Insert(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var check = _dbSet.Find(item.Id);
            if (check == null)
                _dbSet.Add(item);
        }

        public void Update(T item)
        {
            var itemToModify = _dbSet.Find(item.Id);
            if (itemToModify == null)
                throw new ArgumentException(nameof(item));
            _context.Entry(itemToModify).CurrentValues.SetValues(item);
            _context.Entry(itemToModify).State = EntityState.Modified;
        }
    }
}
