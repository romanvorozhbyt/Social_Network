using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class MessageRepository :IMessageRepository
    {

        private readonly SocialNetworkContext _context;
        private readonly DbSet<Message> _dbSet;
        public MessageRepository(SocialNetworkContext context)
        {
            _context = context;
            _dbSet = context.Set<Message>();
        }

        public void Delete(int id)
        {
            var item = _dbSet.Find(id);
            if (item != null)
                _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<Message> messageList)
        {
            foreach (var message in messageList.Reverse())
            {
                var item = _dbSet.Find(message.Id);
                if (item != null)
                    _dbSet.Remove(item);
            }
        }

        public void Update(Message item)
        {
            var itemToModify = _dbSet.Find(item.Id);
            if (itemToModify == null)
                throw new ArgumentException(nameof(item));
            _context.Entry(itemToModify).CurrentValues.SetValues(item);
            _context.Entry(itemToModify).State = EntityState.Modified;

        }

        public void Insert(Message item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var check = _dbSet.Find(item.Id);
            if (check == null)
                _dbSet.Add(item);
        }

        public Message GetById(int id) => _dbSet.Find(id);
        

        public IEnumerable<Message> GetAll() => _dbSet;
    }
}
