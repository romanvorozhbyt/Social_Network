
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
        ChatDTO GetById(int id);
        void CreateChat(string creatorId, string invitedUser);
        void DeleteChat(int id);
        IEnumerable<ChatDTO> GetAllUserChats(string userId);
        void AddUserToChat(int chatId, string userToAddId);
        void RemoveUserFromChat(int chatId, string userToRemoveId);

    }
}
