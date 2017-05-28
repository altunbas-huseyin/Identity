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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class PermissionController : Controller
    {
        PermissionRepo permissionRepo = new PermissionRepo();
        private StatusRepo statusRepo = new StatusRepo();
        Jwt jwt = new Jwt();

        // GET: api/values
        [HttpGet]
        public CommonApiResponse Get()
        {
            jwt = ViewBag.Jwt;
            List<Permission> list = permissionRepo.GetByUserId(jwt.UserId);
            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, true, list, null);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            Permission permission = permissionRepo.GetById(jwt.UserId, Id);
            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, true, permission, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]PermissionCrudView permissionView)
        {
            jwt = ViewBag.Jwt;
            Permission permission = new Permission();
            permission.UserId = jwt.UserId;
            permission.Name = permissionView.Name;
            permission.Description = permissionView.Description;

            bool result = permissionRepo.Insert(permission);
            if (result)
            { return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, true, permission, null); }

            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Hata oluştu");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public CommonApiResponse Delete(string Id)
        {
            jwt = ViewBag.Jwt;
            bool result = permissionRepo.Delete(jwt.UserId, Id);
            if (result)
            { return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }

            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, false, null, "Hata oluştu");
        }
    }
}
