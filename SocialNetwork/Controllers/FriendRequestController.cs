using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SocialNetwork.Tools;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendRequestController : ApiController
    {
        private readonly IFriendRequestService _requestService;
        private readonly IUserService _userService;
        public FriendRequestController(IFriendRequestService requestService, IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;
        }



        // GET: api/FriendRequest/5
        //public HttpResponseMessage Get(int id)
        //{
        //    return Request.CreateResponse(HttpStatusCode.Found, _requestService.GetById(id), new JsonMediaTypeFormatter
        //    {
        //        SerializerSettings =
        //            new JsonSerializerSettings
        //            {
        //                ContractResolver = new DynamicContractResolver<FriendRequestDTO>(
        //                    fr=>fr.RequestedBy.Friends, 
        //                    fr => fr.RequestedBy.FriendRequests, 
        //                    fr => fr.RequestedTo.FriendRequests,
        //                    fr => fr.RequestedTo.Friends)
        //            }
        //    });
            
        //}

        
        [HttpPost]
        [Route("api/FriendRequest/{RequestedTo_Id}/{RequestedFrom_Id}")]
        public IHttpActionResult FriendRequest(string RequestedTo_Id, string RequestedFrom_Id)
        {
            var userTo = _userService.GetById(RequestedTo_Id);
            if (userTo == null)
                return NotFound();
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != RequestedFrom_Id)
                return StatusCode(HttpStatusCode.Forbidden);
            _requestService.MakeFriendRequest(RequestedTo_Id, RequestedFrom_Id);
            return Ok();
        }

        [HttpPut]
        [Route("api/FriendRequest/AcceptFriend/{id}")]
        public IHttpActionResult AcceptFriend(int id, [FromBody] FriendRequestDTO friend)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != friend.RequestedTo.Id)
                return StatusCode(HttpStatusCode.Forbidden);

            var c = _requestService.GetById(id);
            if (c != null)
            {
                _requestService.AcceptFriend(friend);
                return Ok();
            }
            else
            {
             return NotFound();
            }
        }
        [HttpPut]
        [Route("api/FriendRequest/Decline/{id}")]
        public IHttpActionResult Decline(int id, [FromBody] FriendRequestDTO friendRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != friendRequest.RequestedTo.Id.ToString().ToLower())
                return StatusCode(HttpStatusCode.Forbidden);

            var c = _requestService.GetById(id);
            if (c != null)
            {
                _requestService.DeclineFriendRequest(friendRequest);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/BlockUser/{userId}/userToBlockId")]
        public IHttpActionResult BlockUser(string userId, string userToBlockId)
        {
            var identityUserId = RequestContext.Principal.Identity.GetUserId();
            if (identityUserId != userId)
                return StatusCode(HttpStatusCode.Forbidden);
            var userToBlock = _userService.GetById(userToBlockId);
            if (userToBlock == null)
                return NotFound();
            _requestService.BlockUser(userToBlockId,userId);
            return Ok();
        }

        [HttpPost]
        [Route("api/UnBlockUser/{userId}/userToBlockId")]
        public IHttpActionResult UnBlockUser(string userId, string userToUnBlockId)
        {

            var identityUserId = RequestContext.Principal.Identity.GetUserId();
            if (identityUserId != userId)
                return StatusCode(HttpStatusCode.Forbidden);
            var userToBlock = _userService.GetById(userToUnBlockId);
            if (userToBlock == null)
                return NotFound();
            _requestService.UnBlockUser(userToUnBlockId, userId);
            return Ok();
        }

        [HttpDelete]
        // DELETE: api/FriendRequest/5
        public IHttpActionResult DeleteFriend(int id)
        {
            var request = _requestService.GetById(id);
            if (request == null)
                return NotFound();
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != request.RequestedBy_Id.ToString().ToLower() || userId != request.RequestedTo.Id.ToString().ToLower())
                return StatusCode(HttpStatusCode.Forbidden);

            try
            {
                _requestService.Delete(id);
            }
            catch
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
