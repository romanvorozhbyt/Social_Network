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
        void Insert(MessageDTO friend);
        void Delete(int id);
        void Update(MessageDTO friend);
        IEnumerable<MessageDTO> GetChatMessages(int chatId, int pageIndex, int pageSize);
    }
}
