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
    [Authorize]
    [RoutePrefix("api/Message")]
    public class MessageController : ApiController
    {

        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        public MessageController(IMessageService messageService, IChatService chatService, IUserService userService)
        {
            _messageService = messageService;
            _chatService = chatService;
            _userService = userService;
        }

       

        // GET: api/Message/5
        public HttpResponseMessage Get(int id)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var message = _messageService.GetById(id);
            var chat = _chatService.GetById(message.ChatId);
            var user = _userService.GetById(userId);
            if (chat.Users.FirstOrDefault(u => u.Id == user.Id) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return Request.CreateResponse(HttpStatusCode.Forbidden);

            return Request.CreateResponse(HttpStatusCode.Found, message, new JsonMediaTypeFormatter
            {
                SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DynamicContractResolver<MessageDTO>(m => m.Chat, m => m.Chat, m => m.UserFrom)
                    }
            });

        }

        [HttpPost]
        // POST: api/Message
        public IHttpActionResult SendMessage([FromBody]MessageModel message)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");
            string userId = RequestContext.Principal.Identity.GetUserId();
            var chat = _chatService.GetById(message.ChatId);
            if (chat == null)
                return NotFound();

            if (chat.Users.FirstOrDefault(u=>u.Id==userId) == null && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);


            var messageDto = new MessageDTO()
            {
                UserFromId = userId,
                Chat = chat,
                ChatId = message.ChatId,
                Content = new ContentDTO() { MessageContent = message.Content }
            };
            _messageService.SendMessage(messageDto);

            return Ok();

        }
        [HttpPut]
        [Route("{messageId}")]
        // PUT: api/Message/5
        public IHttpActionResult EditMessage(int messageId, [FromBody] string content)
        {
          
            string userId = RequestContext.Principal.Identity.GetUserId();
            var message = _messageService.GetById(messageId);
            if (message == null)
                return NotFound();
            var chat = _chatService.GetById(message.ChatId);
            if (chat == null)
                return NotFound();

            if (!message.UserFromId.Equals(userId) && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);

            _messageService.EditMessage(message, content);

            return Ok();
        }

        [HttpPost]
        [Route("{messageId}/chat/{chatId}")]
        public IHttpActionResult ForwardMessage(int chatId, int messageId)
        {
          
            string userId = RequestContext.Principal.Identity.GetUserId();
            var chat = _chatService.GetById(chatId);
            var message = _messageService.GetById(messageId);
            if (chat == null || message == null)
                return NotFound();

            if (chat.Users.FirstOrDefault(u=>u.Id==userId)==null && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);

            _messageService.ForwardMessage(message, chatId);
              return Ok();
        }
        // DELETE: api/Message/5
        public IHttpActionResult Delete(int id)
        {
            var message = _messageService.GetById(id);
            if (message == null)
                return NotFound();
            string userId = RequestContext.Principal.Identity.GetUserId();
            var chat = _chatService.GetById(message.ChatId);
            if (chat == null)
                return NotFound();

            if (message.UserFromId!=userId && !RequestContext.Principal.IsInRole("Moderator"))
                return StatusCode(HttpStatusCode.Forbidden);

            _messageService.DeleteMessage(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
