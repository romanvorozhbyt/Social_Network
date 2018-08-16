using System;
using System.Collections.Generic;
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
    class ChatService:IChatService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }
        public void CreateChat()
        {
            _db.Chat.Insert(new Chat(){CreateDate = DateTime.Now}); 
        
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
                var userChats = _db.Chat.GetAll().Where(c => c.Users.Contains(user));
                return _mapper.Map<IEnumerable<ChatDTO>>(userChats);
            }
            return null;

        }
    }
}
