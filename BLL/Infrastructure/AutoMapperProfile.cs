using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Models;
using BLL.ModelsDTO;

namespace BLL.Infrastructure
{
   public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDetailsDTO, UserDetails>().ReverseMap();
            CreateMap<ContentDTO, Content>().ReverseMap();
            CreateMap<NewsDTO, News>().ReverseMap();
            CreateMap<FriendsDTO, Friends>().ReverseMap();
            CreateMap<MessageDTO, Message>() .ReverseMap();

        }
    }

    public static  class AutoMapperExtension
    {
        public static MessageDTO MapToMessageDTO(this IMapper mapper, Message message, Content content)
        {
            return new MessageDTO()
            {
                Id = message.Id,
                UserToId = message.UserToId,
                UserFromId = message.UserFromId,
                CreateDate = message.CreateDate,
                ModifiedDate = message.ModifiedDate,
                Content = content.MessageContent
            };
        }

        public static IEnumerable<MessageDTO> MapToMessageDTOList(this IMapper mapper, IEnumerable<Message> messages)
        {
            List<MessageDTO> messageDtos = new List<MessageDTO>();
            foreach (var message in messages)
            {
                messageDtos.Add(new MessageDTO()
                {
                    Id = message.Id,
                    CreateDate = message.CreateDate,
                    ModifiedDate = message.ModifiedDate,
                    //ChatId = message.ChatId,
                    //IdFrom = message.IdFrom,
                    //IdTo = message.IdTo
                });

            }

            return messageDtos;
        }
        public static (Message, Content) MapToMessage(this IMapper mapper, MessageDTO message)
        {
            return (new Message()
            {

                //Id = message.Id,
                //Chat = message.Chat.Id,
                //IdTo = message.IdTo,
                //IdFrom = message.IdFrom,
                CreateDate = message.CreateDate,
                ModifiedDate = message.ModifiedDate,


            },
                            new Content()
                            {
                                MessageContent = message.Content
                            });
        }
    }
}
