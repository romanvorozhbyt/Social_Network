using BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class ChatController : ApiController
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [Authorize(Roles = ("User, Moderator"))]
        [Route("api/chat/getUserChat/{userId}")]
        public IHttpActionResult Get(string userId)
        {
            return Ok(_chatService.GetAllUserChats(userId));
        }

       
        [HttpPost]
        [Route("api/Chat/Create/{creatorId}/{userToInviteId}")]
        public IHttpActionResult CreateChat(string creatorId, string userToInviteId)
        {
            var chat =_chatService.CreateChat(creatorId, userToInviteId);
            return Ok(chat);
        }

        [HttpDelete]
        [Route("api/Chat/RemoveUser/{chatId}/{userId}")]
        public IHttpActionResult RemoveUserFromChat(int chatId, string userId)
        {
            _chatService.RemoveUserFromChat(chatId,userId);
            return Ok();
        }

        [HttpPost]
        [Route("api/Chat/AddUser/{chatId}/{userId}")]
        public IHttpActionResult AddUserToChat(int chatId, string userId)
        {
            _chatService.AddUserToChat(chatId, userId);
            return Ok();
        }
        // DELETE: api/Chat/5
        public IHttpActionResult DeleteChat(int id)
        {
            if (_chatService.GetById(id) == null)
                return NotFound();
            try
            {
                _chatService.DeleteChat(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}
