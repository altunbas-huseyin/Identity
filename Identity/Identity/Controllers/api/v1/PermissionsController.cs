using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Filters;
using IdentityRepository;
using Identity.Middleware;
using IdentityModels;
using IdentityModels.Permissions;
using IdentityHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class PermissionsController : Controller
    {
        PermissionRepo permissionRepo = new PermissionRepo();
        private StatusRepo statusRepo = new StatusRepo();
        Jwt jwt = new Jwt();

        // GET: api/values
        [HttpGet]
        public CommonApiResponse Get()
        {
            jwt = ViewBag.Jwt;
            List<Permission> list = permissionRepo.GetByUserId(jwt.User_Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, list, null);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            Permission permission = permissionRepo.GetById(jwt.User_Id, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, permission, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            Permission permission = new Permission();
            permission.User_Id = jwt.User_Id;
            permission.Name = permissionView.Name;
            permission.Description = permissionView.Description;

            bool result = permissionRepo.Insert(permission);
            if (result)
            { return CommonApiResponse.Create( Response, System.Net.HttpStatusCode.OK, true, permission, null); }

            return CommonApiResponse.Create( Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse Put(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            Permission permission = new Permission();
            permission._id = permissionView._id;
            permission.User_Id = jwt.User_Id;
            permission.Name = permissionView.Name;
            permission.Description = permissionView.Description;

            bool result = permissionRepo.Update(permission);
            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, permission, null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            bool result = permissionRepo.Delete(jwt.User_Id, permissionView._id);
            //if (result)
            //{ return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }
            //
            //return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, "Hata oluştu");
        }


    }
}
