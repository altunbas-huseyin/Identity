using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityModels.Users;
using Identity.Middleware;
using IdentityRepository;
using IdentityHelper;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private UserRepo userRepo;
        private StatusRepo statusRepo;
        private RoleRepo roleRepo;
        private string error = "";
        private bool status = false;


        public LoginController(IConfiguration configuration)
        {
            userRepo = new UserRepo(configuration);
            statusRepo = new StatusRepo(configuration);
            roleRepo = new RoleRepo(configuration);
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]IdentityModels.Users.UserLoginView userLoginView)
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

            return CommonApiResponse.Create( Response, System.Net.HttpStatusCode.OK, status, _user, FluentValidationHelper.GenerateErrorList(error));
        }
    }
}
