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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class UserRolesController : Controller
    {
        Jwt jwt = new Jwt();
        UserRoleRepo userRoleRepo = new UserRoleRepo();
        UserRepo userRepo = new UserRepo();
        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            User user = userRepo.GetById(jwt.UserId, Id);
            List<Role> userRole = user.Role;
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, userRole, null);
        }


        // POST api/values
        [HttpPost("{UserId}/{RoleId}")]
        public CommonApiResponse Post(string UserId, String RoleId)
        {
            jwt = ViewBag.Jwt;
            Result result = userRoleRepo.UserAddRole(jwt.UserId, UserId, RoleId);

            User user = userRepo.GetById(jwt.UserId, UserId);
            List<Role> userRole = user.Role;
            
            return CommonApiResponse.Create(Response, userRole.LastOrDefault(), result);

        }


        // DELETE api/values/5
        [HttpDelete("{UserId}/{RoleId}")]
        public CommonApiResponse Delete(String UserId, String RoleId)
        {
            jwt = ViewBag.Jwt;
            bool result = userRoleRepo.UserRemoveRole(jwt.UserId, UserId.ToString(), RoleId.ToString());
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, result, null, null);
        }
    }
}
