using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstraction;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    class FriendService : IFriendService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public FriendService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<FriendsDTO> GetAll()
        {
            var friends = _db.Friends.GetAll();
            return _mapper.Map<IEnumerable<FriendsDTO>>(friends);
        }

        
        public FriendsDTO GetById(int id)
        {
            var friend = _db.Friends.GetById(id);
            return _mapper.Map<FriendsDTO>(friend);
        }

        public void Insert(FriendsDTO friend)
        {
            var fr = _mapper.Map<Friends>(friend);
            _db.Friends.Insert(fr);
            _db.Save();
        }

        public void Delete(int id)
        {
            _db.Friends.Delete(id);
            _db.Save();
        }

        public void Update(FriendsDTO friend)
        {
            var fr = _mapper.Map<Friends>(friend);
            _db.Friends.Update(fr);
            _db.Save();
        }

        public IEnumerable<FriendsDTO> GetAllMyFriends(Guid id, int pageIndex = 1,int pageSize = 50 )
        {
            var  myFriends = _db.Friends.GetAll()
                .Where(f=>f.MeId == id && f.Status == "Confirmed")
                .Skip((pageIndex-1)*pageSize)
                .Take(pageSize)
                .OrderBy(f=>f.Friend.LastName);
            return _mapper.Map<IEnumerable<FriendsDTO>>(myFriends);
        }
        public IEnumerable<FriendsDTO> GetFriendsRequests(Guid id)
        {
            var requestedUsers = _db.Friends.GetAll()
                .Where(f => f.MeId == id && f.Status == "Pending")
                .OrderBy(f => f.Friend.LastName);
            return _mapper.Map<IEnumerable<FriendsDTO>>(requestedUsers);
        }
        public IEnumerable<FriendsDTO> GetMyFriendRequests(Guid id)
        {
            var myRequests = _db.Friends.GetAll()
                .Where(f => f.FriendId == id && f.Status == "Pending")
                .OrderBy(f => f.Friend.LastName);
            return _mapper.Map<IEnumerable<FriendsDTO>>(myRequests);
        }
    }
}
