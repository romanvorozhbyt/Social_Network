using System.Collections.Generic;
using BLL.Abstraction;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SocialNetwork.Models;
using SocialNetwork.Tools;

namespace SocialNetwork.Controllers
{
    
    [RoutePrefix("api/Chat")]
    public class ChatController : ApiController
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public ChatController(IChatService chatService, IUserService userService, IMessageService messageService)
        {
            _chatService = chatService;
            _userService = userService;
            _messageService = messageService;
        }
       
       
        [Route("{chatId}")]
        public HttpResponseMessage GetChat(int chatId)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var chat = _chatService.GetById(chatId);
            var user = _userService.GetById(userId);
            if (chat == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (chat.Users.FirstOrDefault(u => u.Id == user.Id) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return Request.CreateResponse(HttpStatusCode.Forbidden);
            return Request.CreateResponse(HttpStatusCode.Found, _messageService.GetChatMessages(chatId, userId), new JsonMediaTypeFormatter
            {
                SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DynamicContractResolver<MessageDTO>(m => m.UserFrom, m => m.Chat)
                    }
            });
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IHttpActionResult CreateChat( [FromBody] string userToInviteId)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var user = _userService.GetById(userId);
            var userToInvite = _userService.GetById(userToInviteId);
            if (userToInvite == null)
                return NotFound();
            _chatService.CreateChat(user.Id, userToInvite.Id);
            return Ok();
        }

        [HttpPut]
        [Route("{chatId}")]
        public IHttpActionResult RemoveUserFromChat(int chatId, [FromBody] string userId)
        {
            var user = _userService.GetById(RequestContext.Principal.Identity.GetUserId());
            var chat = _chatService.GetById(chatId);
            if (chat.Users.FirstOrDefault(u => u.Id == user.Id) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);
            _chatService.RemoveUserFromChat(chatId, userId);
            return Ok();
        }

        [HttpPut]
        [Route("{chatId}/users")]
        public IHttpActionResult AddUserToChat(int chatId, [FromBody] string userToAddId)
        {
            var user = _userService.GetById(RequestContext.Principal.Identity.GetUserId());
            var chat = _chatService.GetById(chatId);
            var userToAdd = _userService.GetById(userToAddId);
            if (chat == null || userToAdd == null)
                return NotFound();
            if (chat.Users.FirstOrDefault(u => u.Id == user.Id) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);

            _chatService.AddUserToChat(chat.Id, userToAdd.Id);
            return Ok();
        }
        // DELETE: api/Chat/5
        public IHttpActionResult DeleteChat(int id)
        {
            var chat = _chatService.GetById(id);
            if (chat == null)
                return NotFound();
            var user = _userService.GetById(RequestContext.Principal.Identity.GetUserId());
            if (chat.Users.FirstOrDefault(u => u.Id == user.Id) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);

            _chatService.DeleteChat(id);
            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}
    