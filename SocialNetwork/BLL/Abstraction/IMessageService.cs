using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using  BLL.ModelsDTO;

namespace BLL.Abstraction
{
    public interface IMessageService
    {
        MessageDTO GetById(int id);
        void DeleteMessage(int id);
        void EditMessage(MessageDTO friend);
        IEnumerable<MessageDTO> GetChatMessages(int chatId, string userId, int pageIndex = 1 , int pageSize = 100);
        void SendMessage(MessageDTO message);
        void ForwardMessage(MessageDTO message, int chatId);
    }
}
