using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using Kendo.DynamicLinqCore;
using IdentityModels;
using Identity.Middleware;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using IdentityRepository;
using Identity.Filters;
using IdentityModels.Users;
using IdentityHelper;

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private UserRepo userRepo = new UserRepo();
        private StatusRepo statusRepo = new StatusRepo();
        private RoleRepo roleRepo = new RoleRepo();
        private string error = "";
        private bool status = false; 
        Jwt jwt = new Jwt();
        public UsersController()
        {
            jwt = this.ViewBag.Jwt;
        }

 
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            HttpStatusCode s = new HttpStatusCode();
            //CommonApiResponse c = new CommonApiResponse(s);
            return new string[] { "value1", "value2" };
        }

        public IEnumerable<User> Get(int take, int skip, IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates, IEnumerable<Sort> group)
        {
            //  northwind.Products
            //   .OrderBy(p => p.ProductID) // EF requires ordering for paging                    
            //   .Select(p => new ProductViewModel // Use a view model to avoid serializing internal Entity Framework properties as JSON
            //   {
            //       ProductID = p.ProductID,
            //       ProductName = p.ProductName,
            //       UnitPrice = p.UnitPrice,
            //       UnitsInStock = p.UnitsInStock,
            //       Discontinued = p.Discontinued
            //   })
            //.ToDataSourceResult(take, skip, sort, filter, aggregates, group);

            return new List<User>();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public CommonApiResponse Get(string id)
        {
            User user = userRepo.GetById(jwt.UserId, id);
            status = true;
            return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, status, user, error);

        }

        // POST api/values
        [ValidateModel("SystemAdmin, AppAdmin")]
        [HttpPost]
        public CommonApiResponse Post([FromBody]UserRegisterView userView)
        {
            userView.ParentId = jwt.UserId;
            if (userRepo.GetByEmail(userView.Email) != null)
            {
                status = false;
                error = "Bu mail adresi sistemimize kayıtlıdır.";
                return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, status, "", error);
            }

            User user = new User();
            user.Email = userView.Email;
            user.Password = Encripty.EncryptString(userView.Password);
            user.Name = userView.Name;
            user.SurName = userView.SurName;
            user.FirmCode = userView.FirmCode;
            user.FirmLogo = userView.FirmLogo;
            user.Status = statusRepo.GetByName("WaitingForApproval");
            user.Role = new List<Role>();
            user.Role.Add(roleRepo.GetByName("AppUser"));

            userRepo.Add(user);
            status = true;
            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, status, user, error);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
