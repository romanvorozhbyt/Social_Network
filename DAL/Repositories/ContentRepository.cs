using System;
using System.Collections.Generic;
using System.Data.Entity;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class ContentRepository :IContentRepository
    {
        private readonly SocialNetworkContext _context;
        private readonly DbSet<Content> _dbSet;
        public ContentRepository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<Content>();
        }
        public Content GetById(int id) => _dbSet.Find(id);
        
        public void Insert(Content item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var check = _dbSet.Find(item.Id);
            if (check == null)
                _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<Content> contentList)
        {
            foreach (var content in contentList)
            {
                var item = _dbSet.Find(content.Id);
                if (item != null)
                    _dbSet.Remove(item);
            }
        }

        public void Update(Content item)
        {
            var itemToModify = _dbSet.Find(item.Id);
            if (itemToModify == null)
                throw new ArgumentException(nameof(item));
            _context.Entry(itemToModify).CurrentValues.SetValues(item);
            _context.Entry(itemToModify).State = EntityState.Modified;
        }
    }
}
