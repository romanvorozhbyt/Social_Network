using System;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialNetworkContext _context;

        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IFriendRequestRepository> _friendsRepository;
        private readonly Lazy<IMessageRepository> _messagesRepository;
        private readonly Lazy<IContentRepository> _contentRepository;
        private readonly Lazy<IChatRepository> _chatRepository;

        public IUserRepository Users => _userRepository.Value;
        public IFriendRequestRepository FriendsRequest => _friendsRepository.Value;
        public IMessageRepository Messages => _messagesRepository.Value;
        public IContentRepository Content => _contentRepository.Value;
        public IChatRepository Chat => _chatRepository.Value;

        public UnitOfWork(string connectionString)
        {
            _context = new SocialNetworkContext(connectionString);

            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _friendsRepository = new Lazy<IFriendRequestRepository>(() => new FriendRequestRepository(_context));
            _messagesRepository = new Lazy<IMessageRepository>(() => new MessageRepository(_context));
            _contentRepository = new Lazy<IContentRepository>(() => new ContentRepository(_context));
            _chatRepository = new Lazy<IChatRepository>(()=>new ChatRepository(_context));
        }

        public void Save() => _context.SaveChanges();

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.SaveChanges();
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
