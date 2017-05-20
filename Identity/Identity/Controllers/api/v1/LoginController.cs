using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModels.Users;
using Identity.Middleware;
using IdentityRepository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private UserRepo userRepo = new UserRepo();
        private StatusRepo statusRepo = new StatusRepo();
        private RoleRepo roleRepo = new RoleRepo();
        private string error = "";
        private bool status = false;
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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
    }
}
