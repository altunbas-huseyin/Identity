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
using FluentValidation.Results;
using IdentityModels.Roles;
using Microsoft.Extensions.Configuration;

namespace Identity.Controllers1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ValidateModel("SystemAdmin,AppAdmin")]
    public class UsersController : Controller
    {
        private UserRepo userRepo;
        private StatusRepo statusRepo;
        private RoleRepo roleRepo;
        private UserConvertRepo _userConvertRepo = new UserConvertRepo();
        Jwt jwt = new Jwt();

        public UsersController(IConfiguration configuration)
        {
            userRepo = new UserRepo(configuration);
            statusRepo = new StatusRepo(configuration);
            roleRepo = new RoleRepo(configuration);
        }

        // GET api/values
        [HttpGet]
        public CommonApiResponse Get()
        {
            jwt = ViewBag.Jwt;
            List<User> userList = userRepo.GetByParentId(jwt.User_Id);

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, userList, null);
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
        public CommonApiResponse Get(long Id)
        {
            jwt = ViewBag.Jwt;
            User user = userRepo.GetById(jwt.User_Id, Id);
            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
        }

        // POST api/values
        [ValidateModel("SystemAdmin,AppAdmin")]
        [HttpPost]
        public CommonApiResponse Post(UserRegisterView userView)
        {
            try
            {
                jwt = ViewBag.Jwt;
                if (userRepo.GetByEmail(userView.Email) != null)
                {
                    return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, false, null, "Bu mail adresi sistemimize kayıtlıdır.");
                }
                User tempUser = userRepo.GetById(jwt.User_Id);
                User user = new User();
                user.Parent_Id = jwt.User_Id;
                user.Email = userView.Email;
                user.Password = IdentityHelper.Encripty.EncryptString(userView.Password);
                user.Name = userView.Name;
                user.SurName = userView.SurName;
                user.Status_Id = statusRepo.GetByName("WaitingForApproval").Id;
                //user.Role = new List<Role>();

                List<ValidationFailure> list = UserValidator.FieldValidate(user).ToList();
                if (list.Count > 0)
                {
                   return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.NotFound, false, null, list);
                }

                int result = userRepo.Add(user);

                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
            }
            catch (Exception ex)
            {
                return CommonApiResponse.Create(System.Net.HttpStatusCode.NoContent, false, null, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public CommonApiResponse Put(UserUpdateView userUpdateView)
        {
            jwt = ViewBag.Jwt;
            User user = userRepo.GetById(jwt.User_Id, userUpdateView.Id);

            if (user == null)
            {
                return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, false, null, "Üye bulunamadı");
            }

            user.Email = userUpdateView.Email;
            user.Name = userUpdateView.Name;
            user.SurName = userUpdateView.SurName;
            user.Extra1 = userUpdateView.Extra1;
            user.Extra2 = userUpdateView.Extra2;

            bool result = userRepo.Update(user);
            if (!result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, false, null, FluentValidationHelper.GenerateErrorList("güncelleme esnasında hata oluştu."));
            }

            return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, user, null);
        }

        // DELETE api/values/5
        [HttpDelete]
        public CommonApiResponse Delete(UserUpdateView userUpdateView)
        {
            jwt = ViewBag.Jwt;
            User user = userRepo.GetById(jwt.User_Id, userUpdateView.Id);
            if (user == null)
            {
                return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, false, null, "Üye bulunamadı");
            }

            bool result = userRepo.Delete(user.Id);
            if (result)
            {
                return CommonApiResponse.Create(Response, System.Net.HttpStatusCode.OK, true, "işlem başarılı", null);
            }
            else
            {
                return CommonApiResponse.Create(System.Net.HttpStatusCode.Conflict, false, null, "İşlem başarısız.");
            }
        }
    }
}
