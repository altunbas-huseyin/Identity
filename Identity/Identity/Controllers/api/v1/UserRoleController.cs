using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Filters;
using IdentityModels;
using IdentityRepository;
using Identity.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class UserRoleController : Controller
    {
        Jwt jwt = new Jwt();
        UserRoleRepo userRoleRepo = new UserRoleRepo();
        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]string UserId, String RoleId)
        {
            jwt = ViewBag.Jwt;
            bool result = userRoleRepo.UserAddRole(jwt.UserId, UserId, RoleId);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, result, null, null);
        }


        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse Delete(string UserId, string RoleId)
        {
            jwt = ViewBag.Jwt;
            bool result = userRoleRepo.UserRemoveRole(jwt.UserId, UserId, RoleId);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, result, null, null);
        }
    }
}
