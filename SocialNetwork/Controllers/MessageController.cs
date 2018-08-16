using BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialNetwork.Controllers
{
    public class MessageController : ApiController
    {

        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }
        // GET: api/Message
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Route("api/Message/getChat/{chatId}")]
        public IHttpActionResult GetChat(int chatId)
        {
            return Ok(_service.GetChatMessages(chatId, 1,100));
        }

        // GET: api/Message/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_service.GetById(id));
        }

        // POST: api/Message
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Message/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Message/5
        public void Delete(int id)
        {
        }
    }
}
