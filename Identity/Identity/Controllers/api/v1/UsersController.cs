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

    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        private UserRepo userRepo = new UserRepo();
        private StatusRepo statusRepo = new StatusRepo();
        private RoleRepo roleRepo = new RoleRepo();
        private string error = "";
        private bool status = false;

        [HttpPost]
        public CommonApiResponse Login(string Email, string Password)
        {
            UserView _user = userRepo.LoginByEmail(Email, Password);
            if (_user == null)
            {
                error = "Kullanıcı bilgileri geçersiz.";
                status = false;
            }
            else
            {
                status = true;
            }

            return CommonApiResponse.Create(System.Net.HttpStatusCode.OK, status, _user, error);
        }


        [ValidateModel("SystemAdmin, AppAdmin")]
        [HttpPost]
        public CommonApiResponse Add(UserRegisterView userView)
        {
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
        [HttpGet("{Id}")]
        public string Get(int Id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
