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
            CreateMap<FriendRequestDTO, FriendRequest>().ReverseMap();
            CreateMap<MessageDTO, Message>() .ReverseMap();
            CreateMap<ChatDTO, Chat>().ReverseMap();
            CreateMap<QueryParams, DatabaseQueryParams>();

        }
    }

    
    }

