using System.Net;
using System.Web.Http;
using AutoMapper;
using  BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;
        public UserController(IUserService userService , IMapper mapper, IChatService chatService)
        {
            _service = userService;
            _mapper = mapper;
            _chatService = chatService;
        }
        
        
        // GET: api/User/5
        public IHttpActionResult Get(string id)
        {
            var user = _service.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
            
        }
        [Authorize(Roles = "Moderator")]
        [Route("chat/{userId}")]
        public IHttpActionResult GetUserChats(string userId)
        {
            return Ok(_chatService.GetAllUserChats(userId));
        }
        [Authorize(Roles = "User")]
        [Route("chats")]
        public IHttpActionResult GetChats()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            return Ok(_chatService.GetAllUserChats(userId));
        }


        public IHttpActionResult GetMyProfile()
        {
            var id = RequestContext.Principal.Identity.GetUserId();
            var user = _service.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);

        }

        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search([FromBody]SearchParams param)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (param != null)
            {
                var queryParam = _mapper.Map<QueryParams>(param);
                return Ok(_service.Search(queryParam));
            }

            return NotFound();
        }
        // PUT: api/User/5
        public IHttpActionResult Put(string id, [FromBody]UserDetailsDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != id)
                return StatusCode(HttpStatusCode.Forbidden);
            var c = _service.GetById(id);
            if (c != null)
            {
                _service.Update(user);
            }
           
            return Ok();
        }


        [Authorize(Roles = "Administrator")]
        // DELETE: api/User/5
        public IHttpActionResult Delete(string id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != id)
                return StatusCode(HttpStatusCode.Forbidden);

            if (_service.GetById(id) == null)
                return NotFound();
            
               _service.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
