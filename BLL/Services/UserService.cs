using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

        public IEnumerable<UserDetailsDTO> GetAll()
        {
           var users = _db.Users.GetAll().ToList();
            return _mapper.Map<IEnumerable<UserDetailsDTO>>(users);
        }

        

        public UserDetailsDTO GetById(string id)
        {
            var user = _db.Users.GetById(id);
            return _mapper.Map<UserDetailsDTO>(user);
        }

        public void Insert(UserDetailsDTO userDTO)
        {
            var user = _mapper.Map<UserDetails>(userDTO);
            _db.Users.Insert(user);
        }

        public void Delete(string id)
        {
            _db.Users.Delete(id);
            _db.Save();
        }

        public void Update(UserDetailsDTO user)
        {
            var p = _mapper.Map<UserDetails>(user);
            _db.Users.Update(p);
            _db.Save();
        }

        public IEnumerable<UserDetailsDTO> GetUsersFromCity(string city)
        {
            var users = _db.Users.GetAll().Where(u => u.City == city);
            return _mapper.Map<IEnumerable<UserDetailsDTO>>(users);
        }
        public IEnumerable<UserDetailsDTO> GetUsersByAddress(string address)
        {
            var users = _db.Users.GetAll().Where(u => u.Address == address);
            return _mapper.Map<IEnumerable<UserDetailsDTO>>(users);
        }
        public IEnumerable<UserDetailsDTO> GetUsersFromCountry(string country)
        {
            var users = _db.Users.GetAll().Where(u => u.Country== country);
            return _mapper.Map<IEnumerable<UserDetailsDTO>>(users);
        }
    }
}
