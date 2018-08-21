using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork :IDisposable
    {
        IUserRepository Users { get; }
        IFriendRequestRepository FriendsRequest { get; }
        IMessageRepository Messages { get; }
         IContentRepository Content { get; }
        IChatRepository Chat { get; }
        void Save();

    }
}
