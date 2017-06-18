using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Filters;
using Identity.Middleware;
using IdentityModels;
using IdentityRepository;
using IdentityModels.RolePermissions;
using IdentityHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers.api.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class RolePermissionController : Controller
    {
        RolePermissionRepo rolePermissionRepo = new RolePermissionRepo();
        private StatusRepo statusRepo = new StatusRepo();
        Jwt jwt = new Jwt();

        // GET: api/values
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            List<RolePermission> list = rolePermissionRepo.GetByUserIdAndRoleId(jwt.UserId, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, list, null);
        }

        // POST api/values
        [HttpPost("{RoleId}/{PermissionId}")]
        public CommonApiResponse Post(string RoleId, string PermissionId)
        {
            jwt = ViewBag.Jwt;
            RolePermission rolePermission = new RolePermission();
            //rolePermission.OwnerId = jwt.UserId; //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            //rolePermission.UserId = rolePermissionCrudView.UserId;
            rolePermission.UserId = jwt.UserId;
            rolePermission.PermissionId = PermissionId;
            rolePermission.RoleId = RoleId;

            bool result = rolePermissionRepo.Insert(rolePermission);

            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, rolePermission, null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }


        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public CommonApiResponse Delete(string Id)
        {
            jwt = ViewBag.Jwt;
            bool result = rolePermissionRepo.Delete(jwt.UserId, Id);

            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }
    }
}
