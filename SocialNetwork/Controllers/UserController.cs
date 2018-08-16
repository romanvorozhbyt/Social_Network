using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;
using  BLL.Abstraction;
using BLL.ModelsDTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        
        // GET: api/User
        public IHttpActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        // GET: api/User/5
        public IHttpActionResult Get(string id)
        {
            return Ok(_service.GetById(id));
        }

        // POST: api/User
        public IHttpActionResult Post(UserDetailsDTO user)
        {
            if (ModelState.IsValid)
                _service.Insert(user);
            else return BadRequest();
            return Ok(HttpStatusCode.NoContent);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
