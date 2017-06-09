using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity.Filters;
using IdentityRepository;
using IdentityModels;
using IdentityModels.Roles;
using Identity.Middleware;
using FluentValidation.Results;
using IdentityHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class RolesController : Controller
    {
        // GET: api/values
        private RoleRepo roleRepo = new RoleRepo();
        private StatusRepo statusRepo = new StatusRepo();
        Jwt jwt = new Jwt();

        [HttpGet]
        public CommonApiResponse Get()
        {
            jwt = ViewBag.Jwt;
            List<Role> roleList = roleRepo.GetByUserId(jwt.UserId);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, roleList, null);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            Role role = roleRepo.GetById(jwt.UserId, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post([FromBody]RoleRegisterView roleRegisterView)
        {
            jwt = ViewBag.Jwt;
            Role role = new Role();
            role.Name = roleRegisterView.Name;
            role.UserId = jwt.UserId;
            role.Description = roleRegisterView.Description;
            role.Status = statusRepo.GetByName("Active");

            bool result = roleRepo.Insert(role);
            if (result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "Kayıt başarılı", null);
            }
            else
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, "", FluentValidationHelper.GenerateErrorList("Kayıt Başarısız."));
            }
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse Put([FromBody]RoleUpdateView roleUpdateView)
        {
            jwt = ViewBag.Jwt;
            Role role = roleRepo.GetById(jwt.UserId, roleUpdateView.Id);
            if (role == null)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Rol bulunamadı."));
            }

            role.Name = roleUpdateView.Name;
            role.Description = roleUpdateView.Description;
            bool result = roleRepo.Update(role);

            if (result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "Kayıt başarılı", null);
            }
            else
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Kayıt başarısız."));
            }
        }

        // DELETE api/values/5
        [HttpDelete("{Id}")]
        public CommonApiResponse Delete(string Id)
        {
            jwt = ViewBag.Jwt;
            Role role = roleRepo.GetById(jwt.UserId, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
        }
    }
}
