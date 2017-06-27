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
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class RolesController : Controller
    {
        private RoleRepo roleRepo;
        private StatusRepo statusRepo;
        Jwt jwt = new Jwt();

        public RolesController(IConfiguration Config)
        {
            statusRepo = new StatusRepo(Config);
            roleRepo = new RoleRepo(Config);
        }

        // GET: api/values
        [HttpGet]
        public CommonApiResponse Get()
        {
            jwt = ViewBag.Jwt;
            List<Role> roleList = roleRepo.GetByUserId(jwt.User_Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, roleList, null);
        }

        // GET api/values/5
        [HttpGet("{Id}")]
        public CommonApiResponse Get(string Id)
        {
            jwt = ViewBag.Jwt;
            Role role = roleRepo.GetById(jwt.User_Id, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
        }

        // POST api/values
        [HttpPost]
        public CommonApiResponse Post(RoleRegisterView roleRegisterView)
        {
            jwt = ViewBag.Jwt;
            Role role = new Role();
            role.Name = roleRegisterView.Name;
            role.User_Id = jwt.User_Id;
            role.Description = roleRegisterView.Description;
            role.Status_Id = statusRepo.GetByName("Active").Id;

            bool result = roleRepo.Insert(role);
            if (result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
            }
            else
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, "", FluentValidationHelper.GenerateErrorList("Kayıt Başarısız."));
            }
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse Put(RoleUpdateView roleUpdateView)
        {
            jwt = ViewBag.Jwt;
            Role role = roleRepo.GetById(jwt.User_Id, roleUpdateView.Id);
            if (role == null)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Rol bulunamadı."));
            }

            role.Name = roleUpdateView.Name;
            role.Description = roleUpdateView.Description;
            bool result = roleRepo.Update(role);

            if (result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, role, null);
            }
            else
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Kayıt başarısız."));
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse Delete(string UserId, String Id)
        {
            jwt = ViewBag.Jwt;
            bool result = roleRepo.Delete(UserId, Id);
            if (result)
            { return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "İşlem başaılı", null); }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("Hata oluştu"));
        }
    }
}
