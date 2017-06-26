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
using IdentityModels.Permissions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers.api.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class RolePermissionController : Controller
    {
        RolePermissionRepo rolePermissionRepo = new RolePermissionRepo();
        PermissionRepo permissionRepo = new PermissionRepo();
        private StatusRepo statusRepo = new StatusRepo();
        Jwt jwt = new Jwt();

        // GET: api/values
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            //List<Permission> permissionList = permissionRepo.GetByUserId(jwt.User_Id);
            //List<RolePermission> rolePermissionList = rolePermissionRepo.GetByUserIdAndRoleId(jwt.User_Id, Id);
            //
            //var result = from rolePermission in rolePermissionList
            //            join permission in permissionList
            //                 on rolePermission.PermissionId equals permission.Id
            //            select new
            //            {
            //                rolePermission.Id,
            //                rolePermission.PermissionId,
            //                rolePermission.RoleId,
            //                permission.Name,
            //                permission.Description
            //            };

            var result = rolePermissionRepo.GetByUserIdWithJoinPermission(jwt.User_Id, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, result, null);
        }

        // POST api/values
        [HttpPost("{RoleId}/{PermissionId}")]
        public CommonApiResponse Post(string RoleId, string PermissionId)
        {
            jwt = ViewBag.Jwt;
            RolePermission rolePermission = new RolePermission();
            //rolePermission.OwnerId = jwt.User_Id; //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            //rolePermission.User_Id = rolePermissionCrudView.User_Id;
            rolePermission.User_Id = jwt.User_Id;
            rolePermission.Permission_Id = PermissionId;
            rolePermission.Role_Id = RoleId;

            bool result = false;
            string error = "";


            try
            {
                result = rolePermissionRepo.Insert(rolePermission);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (result)
            {
                var result1 = rolePermissionRepo.GetByUserIdAndIdWithJoinPermission(jwt.User_Id, RoleId, rolePermission.Id);

                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, result1, null);
            }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.BadRequest, false, null, FluentValidationHelper.GenerateErrorList(error));
        }


        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public CommonApiResponse Delete(string Id)
        {
            jwt = ViewBag.Jwt;
            bool result = rolePermissionRepo.Delete(jwt.User_Id, Id);

            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }
    }
}
