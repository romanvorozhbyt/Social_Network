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
   public class ChatRepository : IChatRepository
    {
        private readonly SocialNetworkContext _context;
        private readonly DbSet<Chat> _dbSet;
        public ChatRepository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<Chat>();
        }
        public void Insert(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException(nameof(chat));

            var check = _dbSet.Find(chat.Id);
            if (check == null)
                _dbSet.Add(chat);
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public Chat GetById(int id)
        {
            return  _dbSet.Find(id);
        }

        public IEnumerable<Chat> GetAll(Expression<Func<Chat, bool>> predicate)
        {
            return _dbSet.Where(predicate.Compile());
        }
    }
}
