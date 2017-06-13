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
            List<RolePermission> list = rolePermissionRepo.GetByOwnerIdAndUserId(jwt.UserId, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, list, null);
        }

        // GET api/values/5
        [HttpGet]
        public CommonApiResponse Get(string UserId, String Id)
        {
            jwt = ViewBag.Jwt;
            RolePermission rolePermission = rolePermissionRepo.GetByUserIdAndPermissionId(jwt.UserId, UserId, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, rolePermission, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]RolePermissionCrudView rolePermissionCrudView)
        {
            jwt = ViewBag.Jwt;
            RolePermission rolePermission = new RolePermission();
            rolePermission.OwnerId = jwt.UserId; //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            rolePermission.UserId = rolePermissionCrudView.UserId;
            rolePermission.PermissionId = rolePermissionCrudView.PermissionId;
            rolePermission.RoleId = rolePermissionCrudView.RoleId;

            bool result = rolePermissionRepo.Insert(rolePermission);

            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, rolePermission, null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }


        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse Delete(string UserId, String Id)
        {
            jwt = ViewBag.Jwt;
            bool result = rolePermissionRepo.Delete(jwt.UserId, UserId, Id);

            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }
    }
}
