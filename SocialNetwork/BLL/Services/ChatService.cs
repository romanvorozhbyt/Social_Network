using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstraction;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class ChatService:IChatService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }
        

        public ChatDTO CreateChat(string creatorId, string invitedUserId)
        {
            var creator = _db.Users.GetById(creatorId);
            var invitedUser = _db.Users.GetById(invitedUserId);
            if (creator != null && invitedUser != null)
            {

                var chat = creator.Chats.FirstOrDefault(c => c.Users.Contains(invitedUser) && c.Users.Count == 2);
                if (chat == null)
                {
                    chat = new Chat()
                    {
                        CreateDate = DateTime.Now,
                        Users = new List<UserDetails>() {creator, invitedUser}
                    };
                    _db.Chat.Insert(chat);
                    _db.Save();
                    creator.Chats.Add(chat);
                    invitedUser.Chats.Add(chat);
                    _db.Save();
                }

                return _mapper.Map<ChatDTO>(chat);
            }
            else throw new ObjectNotFoundException();
        }

        public ChatDTO GetById(int id)
        {
            var chat = _db.Chat.GetById(id);
            return _mapper.Map<ChatDTO>(chat);
        }

        public void DeleteChat(int id)
        {
            _db.Chat.Delete(id);
            _db.Save();
        }

        public IEnumerable<ChatDTO> GetAllUserChats(string userId)
        {
            var user = _db.Users.GetById(userId);
            if (user != null)
            {
                var userChats = user.Chats;
                return _mapper.Map<IEnumerable<ChatDTO>>(userChats);
            }
            return null;
        }

        public void AddUserToChat(int chatId, string userToAddId)
        {
            var chat = _db.Chat.GetById(chatId);
            var userToAdd = _db.Users.GetById(userToAddId);
            if (chat != null && userToAdd != null)
            {
                userToAdd.Chats.Add(chat);
                chat.Users.Add(userToAdd);
                _db.Save();
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public void RemoveUserFromChat(int chatId, string userToRemoveId)
        {
            var chat = _db.Chat.GetById(chatId);
            var userToAdd = _db.Users.GetById(userToRemoveId);
            if (chat != null && userToAdd != null)
            {
                userToAdd.Chats.Remove(chat);
                chat.Users.Remove(userToAdd);
                _db.Save();
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }
    }
}
