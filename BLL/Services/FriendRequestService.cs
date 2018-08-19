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
            var friend = _db.FriendsRequest.GetById(id);
            return _mapper.Map<FriendRequestDTO>(friend);
        }

       
        public void Delete(int id)
        {
            _db.FriendsRequest.Delete(id);
            _db.Save();
        }

    

        public IEnumerable<FriendRequestDTO> GetFriendsRequests(string id)
        {
            var friendRequests = _db.FriendsRequest.GetAll(f => f.RequestedTo.Id == id && f.FriendRequestFlag == FriendRequestFlag.Pending)
                .OrderBy(f => f.RequestedTo.LastName);
            return _mapper.Map<IEnumerable<FriendRequestDTO>>(friendRequests);
        }
        public IEnumerable<FriendRequestDTO> GetMyFriendRequests(string id)
        {
            var myRequests = _db.FriendsRequest.GetAll(f => f.RequestedBy.Id == id && f.FriendRequestFlag == FriendRequestFlag.Pending)
                .OrderBy(f => f.RequestedTo.LastName);
            return _mapper.Map<IEnumerable<FriendRequestDTO>>(myRequests);
        }

        
        public void MakeFriendRequest(string RequestedTo_Id, string RequestedBy_Id)
        {
            var requestedBy = _db.Users.GetById(RequestedBy_Id);
            var requestedTo = _db.Users.GetById(RequestedTo_Id);
            if (requestedTo != null && requestedBy != null)
            {

                if (!_db.FriendsRequest.IsRequestExist(RequestedTo_Id, RequestedBy_Id))
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
                    _db.Save();
                }
            }
            else
                throw new ObjectNotFoundException();
        }

        public void AcceptFriend(FriendRequestDTO friendRequest)
        {
            var fr = _mapper.Map<FriendRequest>(friendRequest);

            var user = _db.Users.GetById(fr.RequestedBy_Id);
            var friend = _db.Users.GetById(fr.RequestedTo.Id);
            if (user != null && friend != null)
            {
                fr.FriendRequestFlag = FriendRequestFlag.Approved;
                _db.FriendsRequest.Update(fr);
                user.Friends.Add(friend);
                friend.Friends.Add(user);

                _db.Save();
            }
            else throw new ObjectNotFoundException();

        }

        public void BlockUser(string RequestedTo_Id, string RequestedBy_Id)
        {
            var requestedBy = _db.Users.GetById(RequestedBy_Id);
            var requestedTo = _db.Users.GetById(RequestedTo_Id);
            var request = _db.FriendsRequest
                .GetAll(f => f.RequestedBy_Id == RequestedBy_Id
                             && f.RequestedTo.Id == RequestedTo_Id || f.RequestedBy_Id == RequestedTo_Id
                             && f.RequestedTo.Id == RequestedBy_Id).FirstOrDefault();
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
            _db.Save();

        }

        public void UnBlockUser(string RequestedTo_Id, string RequestedBy_Id)
        {
            var request = _db.FriendsRequest.GetAll(fr =>
                fr.RequestedBy_Id == RequestedBy_Id && fr.RequestedTo.Id == RequestedTo_Id &&
                fr.FriendRequestFlag == FriendRequestFlag.Blocked).FirstOrDefault();
            if (request != null)
            {
                _db.FriendsRequest.Delete(request.Id);
                _db.Save();
            }
            else 
                throw new ObjectNotFoundException();
        }

        public void DeclineFriendRequest(FriendRequestDTO friendRequest)
        {
            var fr = _mapper.Map<FriendRequest>(friendRequest);

                fr.FriendRequestFlag = FriendRequestFlag.Declined;
                _db.FriendsRequest.Update(fr);
            
                _db.Save();
        }
        
    }
}
