using System.Collections.Generic;
using DAL.Models;

namespace DAL.Interfaces
{
   public interface IMessageRepository
    {
        void Delete(int id);
        void DeleteRange(IEnumerable<Message> messageList);
        void Update(Message item);
        void Insert(Message item);
        Message GetById(int id);
        IEnumerable<Message> GetAll();

    }
}
