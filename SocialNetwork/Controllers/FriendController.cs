using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.ApplicationInsights.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SocialNetwork.Tools;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class FriendController : ApiController
    {

        private readonly IFriendService _friendService;
        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }


        // GET: api/Friend
        public HttpResponseMessage GetAllFriends(string userId, int pageIndex = 1, int pageSize = 50)
        {

            return Request.CreateResponse(HttpStatusCode.Found, _friendService.GetAllMyFriends(userId, pageIndex, pageSize), new JsonMediaTypeFormatter
            {
                SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DynamicContractResolver<UserDetailsDTO>(u=>u.FriendRequests, u=>u.Friends)
                    }
            });

        }

        

        // DELETE: api/Friend/{FriendId}
        public void Delete( string friendId)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            _friendService.RemoveFriend(userId, friendId);
        }
    }
}
