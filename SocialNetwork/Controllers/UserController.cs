using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.UI;
using  BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        
        
        // GET: api/User/5
        public IHttpActionResult Get(string id)
        {
            return Ok(_service.GetById(id));
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

        // DELETE: api/User/5
        public IHttpActionResult Delete(string id)
        {
            var userId = RequestContext.Principal.Identity.GetUserId();
            if (userId != id)
                return StatusCode(HttpStatusCode.Forbidden);

            if (_service.GetById(id) == null)
                return NotFound();
            
            try
            {
                _service.Delete(id);
            }
            catch
            {
                return InternalServerError();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
