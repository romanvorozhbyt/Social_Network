using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstraction;
using BLL.ModelsDTO;
using DAL.Interfaces;

namespace BLL.Services
{
    public class FriendService :IFriendService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public FriendService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<UserDetailsDTO> GetAllMyFriends(string id, int pageIndex = 1, int pageSize = 50)
        {
            var user = _db.Users.GetById(id);
            if (user != null)
            {
                var friends = _db.Users.GetAll(u => u.Friends.Contains(user))
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(f => f.LastName);
                return _mapper.Map<IEnumerable<UserDetailsDTO>>(friends);
            }
            throw new ObjectNotFoundException();
            
        }

        public void RemoveFriend(string userId, string friendId)
        {
            var user = _db.Users.GetById(userId);
            if (user != null)
            {
                var friendToRemove = _db.Users.GetById(friendId);
                user.Friends.Remove(friendToRemove);
                friendToRemove.Friends.Remove(user);
                var friendRequestToRemove  = _db.FriendsRequest.GetAll(fr =>
                    fr.RequestedBy == user && fr.RequestedTo == friendToRemove ||
                    fr.RequestedTo == user && fr.RequestedBy == friendToRemove).FirstOrDefault();
                if (friendRequestToRemove!=null)
                    _db.FriendsRequest.Delete(friendRequestToRemove.Id);

                _db.Save();
            }
        }
    }
}
