using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstraction;
using BLL.Infrastructure;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public MessageDTO GetById(int id)
        {

            var message = _db.Messages.GetById(id);
            return _mapper.Map<MessageDTO>(message);
        }

        public void SendMessage(MessageDTO messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            var content = new Content() {MessageContent = message.Content.MessageContent};
            var chat = _db.Chat.GetById(message.ChatId);
            var userFrom = _db.Users.GetById(message.UserFromId);
            if (chat != null && userFrom != null)
            {
                message.Content = null;
                message.UserFrom = userFrom;
                message.Chat = chat;
                message.CreateDate = DateTime.Now;
                message.ModifiedDate = DateTime.Now;

                _db.Messages.Insert(message);
                _db.Save();
                content.Id = message.Id;
                message.Content = content;
                chat.Messages.Add(message);
                _db.Content.Insert(message.Content);
                _db.Save();
                
            }
        }

        public void DeleteMessage(int id)
        {
            _db.Messages.Delete(id);
            _db.Save();
        }

        public void EditMessage(MessageDTO messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);
            var user = _db.Users.GetById(message.UserFromId);
            var chat = _db.Chat.GetById(message.ChatId);
            var content = message.Content;
            message.UserFrom = user;
            message.Chat = chat;
            message.ModifiedDate = DateTime.Now;
            _db.Messages.Update(message);
            _db.Content.Update(content);
            _db.Save();
        }

       
        public IEnumerable<MessageDTO> GetChatMessages(int chatId, string userId, int pageIndex = 1, int pageSize = 100)
        {
            var user = _db.Users.GetById(userId);
            if (user != null)
            {
                var messages = _db.Messages.GetAll()
                    .Where(m => m.Chat.Id == chatId && m.Chat.Users.Contains(user))
                    .OrderByDescending(m => m.CreateDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return _mapper.Map<IEnumerable<MessageDTO>>(messages);
            }
            else throw  new ArgumentException();
            

            
        }

        
        public void ForwardMessage(MessageDTO messageDto, int chatId)
        {
            var message = _mapper.Map<Message>(messageDto);
            var forwardChat = _db.Chat.GetById(chatId);
            var content = message.Content;
            var userFrom = _db.Users.GetById(message.UserFromId);
            if (forwardChat != null)
            {
                var forwardedMessage = new Message()
                {
                    Chat = forwardChat,
                    ChatId = forwardChat.Id,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserFrom = userFrom,
                    UserFromId = message.UserFromId

                };
                _db.Messages.Insert(forwardedMessage);
                _db.Save();
                content.Id = forwardedMessage.Id;
                forwardedMessage.Content = content;
                forwardChat.Messages.Add(forwardedMessage);
                _db.Content.Insert(forwardedMessage.Content);
                _db.Save();
            }
            else throw  new ObjectNotFoundException();
        }
    }
}
