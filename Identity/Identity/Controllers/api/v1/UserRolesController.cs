using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Filters;
using IdentityModels;
using IdentityRepository;
using Identity.Middleware;
using IdentityModels.Roles;
using IdentityModels.Users;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class UserRolesController : Controller
    {
        Jwt jwt = new Jwt();
        UserRoleRepo userRoleRepo;
        UserRepo userRepo;

        public UserRolesController(IConfiguration configuratiom)
        {
            userRoleRepo = new UserRoleRepo(configuratiom);
            userRepo = new UserRepo(configuratiom);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(long Id)
        {
            jwt = ViewBag.Jwt;
            User user = userRepo.GetById(jwt.User_Id, Id);
            List<Role> userRole = new List<Role>();//user.Role;
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, userRole, null);
        }


        // POST api/values
        [HttpPost("{UserId}/{RoleId}")]
        public CommonApiResponse Post(long UserId, long RoleId)
        {
            jwt = ViewBag.Jwt;
            Result result = userRoleRepo.UserAddRole(jwt.User_Id, UserId, RoleId);

            User user = userRepo.GetById(jwt.User_Id, UserId);
            List<Role> userRole = new List<Role>(); //user.Role;
            
            return CommonApiResponse.Create(Response, userRole.LastOrDefault(), result);

        }


        // DELETE api/values/5
        [HttpDelete("{UserId}/{RoleId}")]
        public CommonApiResponse Delete(long UserId, long RoleId)
        {
            jwt = ViewBag.Jwt;
            bool result = userRoleRepo.UserRemoveRole(jwt.User_Id, UserId, RoleId);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, result, null, null);
        }
    }
}
