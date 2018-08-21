using System.Net;
using System.Web.Http;
using BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [RoutePrefix("api/FriendRequest")]
    public class FriendRequestController : ApiController
    {
        private readonly IFriendRequestService _requestService;
        private readonly IUserService _userService;
        public FriendRequestController(IFriendRequestService requestService, IUserService userService)
        {
            _requestService = requestService;
            _userService = userService;
        }


        public IHttpActionResult Get()
        {
            var id = RequestContext.Principal.Identity.GetUserId();
            return Ok(_requestService.GetFriendsRequests(id));
        }


        [HttpPost]
         public IHttpActionResult FriendRequest([FromBody] string requestedToId)
        {

            var userTo = _userService.GetById(requestedToId);
            if (userTo == null)
                return NotFound();
            var userId = RequestContext.Principal.Identity.GetUserId();
            _requestService.MakeFriendRequest(requestedToId, userId);
            return Ok();
        }

        [HttpPut]
        [Route("{id}/Accept")]
        public IHttpActionResult AcceptFriend(int id, [FromBody] FriendRequestDTO friendRequest)
        {

            var request = _requestService.GetById(id);
            if (request == null)
                return NotFound();
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != request.RequestedTo.Id)
                return StatusCode(HttpStatusCode.Forbidden);

            _requestService.AcceptFriend(request);
            return Ok();

        }
        [HttpPut]
        [Route("{id}/Decline")]
        public IHttpActionResult Decline(int id, [FromBody] FriendRequestDTO friendRequest)
        {
            if (_requestService.GetById(friendRequest.Id) == null)
                return NotFound();
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != friendRequest.RequestedTo.Id)
                return StatusCode(HttpStatusCode.Forbidden);

            _requestService.DeclineFriendRequest(friendRequest);
            return Ok();

        }

        [HttpPost]
        [Route("BlockUser/{userToBlockId}")]
        public IHttpActionResult BlockUser(string userToBlockId)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            var userToBlock = _userService.GetById(userToBlockId);
            if (userToBlock == null)
                return NotFound();
            _requestService.BlockUser(userToBlockId, userId);
            return Ok();
        }

        [HttpPost]
        [Route("UnBlockUser/{userToBlockId}")]
        public IHttpActionResult UnBlockUser(string userToUnBlockId)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();
            var userToBlock = _userService.GetById(userToUnBlockId);
            if (userToBlock == null)
                return NotFound();
            _requestService.UnBlockUser(userToUnBlockId, userId);
            return Ok();
        }

       
    }
}
