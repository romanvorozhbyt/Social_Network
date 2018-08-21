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
            var content = new Content() { MessageContent = message.Content.MessageContent };
            var chat = _db.Chat.GetById(message.Chat.Id);
            message.Content = null;
            message.CreateDate = DateTime.Now;
            message.ModifiedDate = DateTime.Now;
            message.Chat = chat;
            _db.Messages.Insert(message);
            _db.Save();
            content.Id = message.Id;
            message.Content = content;
            chat.Messages.Add(message);
            _db.Content.Insert(message.Content);
            _db.Save();


        }

        public void DeleteMessage(int id)
        {
            using (_db)
            {
                var message = _db.Messages.GetById(id);
                message.Content = null;
                _db.Content.Delete(id);

                _db.Save();
                _db.Messages.Delete(id);

            }
            
        }

        public void EditMessage(MessageDTO messageDto, string content)
        {
            var message = _mapper.Map<Message>(messageDto);
            message.ModifiedDate = DateTime.Now;
            message.Content.MessageContent = content;
            using (_db)
            {
                _db.Messages.Update(message);
                _db.Content.Update(message.Content);

            }
        }


        public IEnumerable<MessageDTO> GetChatMessages(int chatId, string userId, int pageIndex = 1, int pageSize = 100)
        {
            var user = _db.Users.GetById(userId);
                var messages = _db.Messages.GetAll()
                    .Where(m => m.Chat.Id == chatId && m.Chat.Users.Contains(user))
                    .OrderByDescending(m => m.CreateDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize);
                return _mapper.Map<IEnumerable<MessageDTO>>(messages);
            
        }


        public void ForwardMessage(MessageDTO messageDto, int chatId)
        {
            var message = _mapper.Map<Message>(messageDto);

            using (_db)
            {

                var forwardChat = _db.Chat.GetById(chatId);
                var content = message.Content;
                var forwardedMessage = new Message()
                {
                    Chat = forwardChat,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserFromId = message.UserFrom.Id,

                };
                _db.Messages.Insert(forwardedMessage);
                _db.Save();
                content.Id = forwardedMessage.Id;
                forwardedMessage.Content = content;
                forwardChat.Messages.Add(forwardedMessage);
                _db.Content.Insert(forwardedMessage.Content);
            }
        }
    }
}
