using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IFriendRequestRepository FriendsRequest { get; }
        IRepository<Message> Messages { get; }
         IRepository<Content> Content { get; }
        IChatRepository Chat { get; }
        void Save();

    }
}
