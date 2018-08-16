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
        IRepository<Friends> Friends { get; }
        IRepository<Message> Messages { get; }
        IRepository<News> News { get; }
        IRepository<Content> Content { get; }
        IRepository<Chat> Chat { get; }
        void Save();

    }
}
