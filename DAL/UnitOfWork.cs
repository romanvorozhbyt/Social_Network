using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SocialNetworkContext _context;

        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IRepository<Friends>> _friendsRepository;
        private readonly Lazy<IRepository<Message>> _messagesRepository;
        private readonly Lazy<IRepository<News>> _newsRepository;
        private readonly Lazy<IRepository<Content>> _contentRepository;
        private readonly Lazy<IRepository<Chat>> _chatRepository;

        public IUserRepository Users => _userRepository.Value;
        public IRepository<Friends> Friends => _friendsRepository.Value;
        public IRepository<Message> Messages => _messagesRepository.Value;
        public IRepository<News> News => _newsRepository.Value;
        public IRepository<Content> Content => _contentRepository.Value;
        public IRepository<Chat> Chat => _chatRepository.Value;

        public UnitOfWork(string connectionString)
        {
            _context = new SocialNetworkContext(connectionString);

            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _friendsRepository = new Lazy<IRepository<Friends>>(() => new Repository<Friends>(_context));
            _messagesRepository = new Lazy<IRepository<Message>>(() => new Repository<Message>(_context));
            _newsRepository = new Lazy<IRepository<News>>(() => new Repository<News>(_context));
            _contentRepository = new Lazy<IRepository<Content>>(() => new Repository<Content>(_context));
            _chatRepository = new Lazy<IRepository<Chat>>(()=>new Repository<Chat>(_context));
        }

        public void Save() => _context.SaveChanges();

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
