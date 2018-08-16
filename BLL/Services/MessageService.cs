using System;
using System.Collections.Generic;
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
            var content = _db.Content.GetById(message.Content.Id);
            return _mapper.MapToMessageDTO(message, content);
        }

        public void Insert(MessageDTO friend)
        {

            var (message, content) = _mapper.MapToMessage(friend);
            _db.Content.Insert(content);
            _db.Save();
            message.Content = content;
            message.Content.Id = content.Id;
            _db.Messages.Insert(message);
            _db.Save();
        }

        public void Delete(int id)
        {
            _db.Messages.Delete(id);
        }

        public void Update(MessageDTO mess)
        {
            var (message, newcontent) = _mapper.MapToMessage(mess);
            var oldcontent = _db.Content.GetById(message.Content.Id);
            oldcontent.MessageContent = newcontent.MessageContent;
            _db.Content.Update(oldcontent);
            message.ModifiedDate = DateTime.Now;
            _db.Messages.Update(message);
            _db.Save();
        }

        public IEnumerable<MessageDTO> GetChatMessages(int chatId, int pageIndex = 1, int pageSize = 100)
        {
            var messages = _db.Messages.GetAll()
                .Where(m => m.Chat.Id == chatId)
                .OrderByDescending(m => m.CreateDate)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //List<Content> content = new List<Content>();

            //foreach (var VARIABLE in messages)
            //{
            //    content.Add(_db.Content.GetById(VARIABLE.ContentId));
            //}
            //var messagesDTO = _mapper.MapToMessageDTOList(messages).ToList();
            //for (int i = 0; i < messagesDTO.Count(); i++)
            //{
            //    messagesDTO[i].Content.MessageContent = content[i].MessageContent;
            //}

            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }
    }
}
