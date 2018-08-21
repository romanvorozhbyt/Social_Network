using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public FriendRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public FriendRequestDTO GetById(int id)
        {
            using (_db)
            {
                var friend = _db.FriendsRequest.GetById(id);
                return _mapper.Map<FriendRequestDTO>(friend);
            }
        }




        public IEnumerable<FriendRequestDTO> GetFriendsRequests(string id)
        {
            using (_db)
            {
                var friendRequests = _db.FriendsRequest.GetAll(f =>
                        f.RequestedTo.Id == id && f.FriendRequestFlag == FriendRequestFlag.Pending)
                    .OrderBy(f => f.RequestedTo.LastName);
                return _mapper.Map<IEnumerable<FriendRequestDTO>>(friendRequests);
            }
        }


        public void MakeFriendRequest(string requestedToId, string requestedById)
        {
            using (_db)
            {
                var requestedBy = _db.Users.GetById(requestedById);
                var requestedTo = _db.Users.GetById(requestedToId);
                if (!_db.FriendsRequest.IsRequestExist(requestedToId, requestedById))
                {

                    FriendRequest fr = new FriendRequest()
                    {
                        RequestedBy_Id = requestedBy.Id,
                        RequestedBy = requestedBy,
                        RequestedTo = requestedTo,
                        RequestTime = DateTime.Now,
                        FriendRequestFlag = FriendRequestFlag.Pending
                    };
                    _db.FriendsRequest.Insert(fr);
                    requestedBy.FriendRequests.Add(fr);
                    
                }
            }


        }

        public void AcceptFriend(FriendRequestDTO friendRequest)
        {
            var fr = _mapper.Map<FriendRequest>(friendRequest);
            using (_db)
            {
                var user = _db.Users.GetById(fr.RequestedBy_Id);
                var friend = _db.Users.GetById(fr.RequestedTo.Id);

                fr.FriendRequestFlag = FriendRequestFlag.Approved;
                _db.FriendsRequest.Update(fr);
                user.Friends.Add(friend);
                friend.Friends.Add(user);
            }

        }
        public void DeclineFriendRequest(FriendRequestDTO friendRequest)
        {
            var fr = _mapper.Map<FriendRequest>(friendRequest);
            using (_db)
            {
                fr.FriendRequestFlag = FriendRequestFlag.Declined;
                _db.FriendsRequest.Update(fr);
            }
        }

        public void BlockUser(string requestedToId, string requestedById)
        {
            using (_db)
            {
                var requestedBy = _db.Users.GetById(requestedById);
                var requestedTo = _db.Users.GetById(requestedToId);
                var request = _db.FriendsRequest
                    .GetAll(f => f.RequestedBy_Id == requestedById
                                 && f.RequestedTo.Id == requestedToId || f.RequestedBy_Id == requestedToId
                                 && f.RequestedTo.Id == requestedById).FirstOrDefault();
                if (request == null)
                {
                    request = new FriendRequest()
                    {
                        RequestedBy_Id = requestedBy.Id,
                        RequestedBy = requestedBy,
                        RequestedTo = requestedTo,
                        FriendRequestFlag = FriendRequestFlag.Blocked,
                        RequestTime = DateTime.Now
                    };
                    _db.FriendsRequest.Insert(request);
                }
                else
                {
                    request.FriendRequestFlag = FriendRequestFlag.Blocked;
                    request.RequestedBy = requestedBy;
                    request.RequestedBy_Id = requestedBy.Id;
                    request.RequestedTo = requestedTo;
                    _db.FriendsRequest.Update(request);
                }

                if (requestedBy.Friends.Contains(requestedTo))
                    requestedBy.Friends.Remove(requestedTo);
                if (requestedTo.Friends.Contains(requestedBy))
                    requestedTo.Friends.Remove(requestedBy);
             
            }

        }

        public void UnBlockUser(string requestedToId, string requestedById)
        {
            using (_db)
            {
                var request =
                    _db.FriendsRequest.FindFriendRequest(requestedToId, requestedById, FriendRequestFlag.Blocked);
                if (request != null)
                    _db.FriendsRequest.Delete(request.Id);
            }
        }

   

    }
}
