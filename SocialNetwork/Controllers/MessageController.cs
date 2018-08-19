using BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using AutoMapper;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SocialNetwork.Models;
using SocialNetwork.Tools;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class MessageController : ApiController
    {

        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;
        public MessageController(IMessageService messageService, IChatService chatService)
        {
            _messageService = messageService;
            _chatService = chatService;
        }
        
        [Route("api/Message/getChat/{chatId}")]
        public HttpResponseMessage GetChat(int chatId)
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            var chat = _chatService.GetById(chatId);
            if (chat == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
             //if(chat.Users.Contains())
            return Request.CreateResponse(HttpStatusCode.Found, _messageService.GetChatMessages(chatId,userId), new JsonMediaTypeFormatter
            {
                SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DynamicContractResolver<MessageDTO>(m => m.UserFrom, m=>m.Chat)
                    }
            });
        }

        // GET: api/Message/5
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse(HttpStatusCode.Found, _messageService.GetById(id), new JsonMediaTypeFormatter
            {
                SerializerSettings =
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DynamicContractResolver<MessageDTO>(m=>m.Chat, m=>m.Chat, m=>m.UserFrom)
                    }
            });

        }

        [HttpPost]
        [Route("api/Message/send")]
        // POST: api/Message
        public IHttpActionResult SendMessage([FromBody]MessageModel message)
        {
            MessageDTO messageDto;
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid");
            try
            {
                messageDto = new MessageDTO()
                {
                    UserFromId = message.UserFromId,
                    ChatId = message.ChatId,
                    Content = new ContentDTO() {MessageContent = message.Content}
                };
                _messageService.SendMessage(messageDto);

            }
            catch
            {
                return InternalServerError();
            }
            return CreatedAtRoute("DefaultApi", new { id = messageDto.Id }, messageDto);
           
        }
        [HttpPut]
        [Route("api/Message/EditMessage")]
        // PUT: api/Message/5
        public IHttpActionResult EditMessage(int id, [FromBody]MessageDTO message)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var m = _messageService.GetById(id);
            if (m != null)
            {
                _messageService.EditMessage(message);
            }
            
            return Ok();
        }

        [HttpPost]
        [Route("api/Message/Forward")]
        public IHttpActionResult ForwardMessage(int chatId, [FromBody] MessageModel messageModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            var message = new MessageDTO()
            {
                ChatId = messageModel.ChatId,
                UserFromId = messageModel.UserFromId,
                Content = new ContentDTO() {MessageContent = messageModel.Content}
            };
           
                _messageService.ForwardMessage(message, chatId);
           

            return Ok();
        }
        // DELETE: api/Message/5
        public IHttpActionResult Delete(int id)
        {
            if (_messageService.GetById(id) == null)
                return NotFound();
            try
            {
                _messageService.DeleteMessage(id);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
