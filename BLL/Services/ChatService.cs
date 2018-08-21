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
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }


        public void CreateChat(string creatorId, string invitedUserId)
        {
            using (_db)
            {
                var creator = _db.Users.GetById(creatorId);
                var invitedUser = _db.Users.GetById(invitedUserId);
                var blockRequest =
                    _db.FriendsRequest.FindFriendRequest(creatorId, invitedUserId, FriendRequestFlag.Blocked);

                if (blockRequest == null)
                {
                    if (creator.Chats != null)
                    {
                        var chat = creator.Chats.FirstOrDefault(c =>
                            c.Users.FirstOrDefault(u => u.Id == invitedUser.Id) != null && c.Users.Count == 2);
                        if (chat == null)
                        {
                            chat = new Chat()
                            {
                                CreateDate = DateTime.Now,
                                Users = new List<UserDetails>() { creator, invitedUser }
                            };
                            _db.Chat.Insert(chat);
                            _db.Save();
                            creator.Chats.Add(chat);
                            invitedUser.Chats.Add(chat);
                        }
                    }
                    else
                    {

                        var chat = new Chat()
                        {
                            CreateDate = DateTime.Now,
                            Users = new List<UserDetails>() { creator, invitedUser }
                        };
                        _db.Chat.Insert(chat);
                        _db.Save();
                        creator.Chats = new List<Chat>() { chat };
                        invitedUser.Chats.Add(chat);
                    }
                }

                throw new InvalidOperationException();
            }
        }

        public ChatDTO GetById(int id)
        {
            using (_db)
            {
                var chat = _db.Chat.GetById(id);
                return _mapper.Map<ChatDTO>(chat);
            }
        }

        public void DeleteChat(int id)
        {

            using (_db)
            {
                var chat = _db.Chat.GetById(id);
                var messages = chat.Messages;
                List<Content> contentList = new List<Content>();
                foreach (var message in messages)
                {
                    contentList.Add(message.Content);
                    message.Content = null;
                }
                _db.Content.DeleteRange(contentList);
                _db.Save();
                _db.Messages.DeleteRange(messages);
                _db.Save();
                _db.Chat.Delete(id);
            }
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
            using (_db)
            {
                if (chat.Users.FirstOrDefault(u => u.Id == userToAddId) == null)
                {
                    userToAdd.Chats.Add(chat);
                    chat.Users.Add(userToAdd);
                }
            }

        }

        public void RemoveUserFromChat(int chatId, string userToRemoveId)
        {
            var chat = _db.Chat.GetById(chatId);
            var userToRemove = _db.Users.GetById(userToRemoveId);
            using (_db)
            {
                if (chat.Users.FirstOrDefault(u => u.Id == userToRemoveId) != null)
                {
                    userToRemove.Chats.Remove(chat);
                    chat.Users.Remove(userToRemove);
                }
            }

        }
    }
}
