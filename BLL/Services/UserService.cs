using System;
using System.Collections.Generic;
using AutoMapper;
using BLL.Abstraction;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public UserDetailsDTO GetById(string id)
        {
            using (_db)
            {
                var user = _db.Users.GetById(id);
                return _mapper.Map<UserDetailsDTO>(user);
            }
        }

        public void Insert(UserDetailsDTO userDto)
        {
            var user = _mapper.Map<UserDetails>(userDto);
            user.DateOfBirth = new DateTime(1970, 1, 1);
            using (_db)
            {
                _db.Users.Insert(user);
                _db.Save();
            }
        }

        public void Delete(string id)
        {
            using (_db)
            {
                _db.Users.Delete(id);
                _db.Save();
            }
        }

        public void Update(UserDetailsDTO user)
        {
            var p = _mapper.Map<UserDetails>(user);
            using (_db)
            {
                _db.Users.Update(p);
                _db.Save();
            }
        }

        public IEnumerable<UserDetailsDTO> Search(QueryParams parameters)
        {
            var dbQueryParams = _mapper.Map<DatabaseQueryParams>(parameters);
            var users = _db.Users.Search(dbQueryParams);
            return _mapper.Map<IEnumerable<UserDetailsDTO>>(users);

        }


    }
}
