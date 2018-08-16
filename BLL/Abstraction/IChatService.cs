
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ModelsDTO;

namespace BLL.Abstraction
{
    public  interface IChatService
    {
        void CreateChat();
        void DeleteChat(int id);
        IEnumerable<ChatDTO> GetAllUserChats(string userId);

    }
}
