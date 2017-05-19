using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModels.Users;
using Identity.Middleware;
using IdentityRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class Login : Controller
    {
        private UserRepo userRepo = new UserRepo();
        private StatusRepo statusRepo = new StatusRepo();
        private RoleRepo roleRepo = new RoleRepo();
        private string error = "";
        private bool status = false;
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]UserLoginView userLoginView)
        {
            UserView _user = userRepo.LoginByEmail(userLoginView.Email, userLoginView.Password);
            if (_user == null)
            {
                error = "Kullanıcı bilgileri geçersiz.";
                status = false;
            }
            else
            {
                status = true;
            }

            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, status, _user, error);
        }

        // PUT api/values/5 
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
