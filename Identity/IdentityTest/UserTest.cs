
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Users;
using IdentityHelper;
using IdentityModels.Roles;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace IdentityTest
{
    [TestClass]
    public class UserTest
    {
        UserRepo userRepo;
        RoleRepo roleRepo;
        JwtRepo jwtRepo;
        StatusRepo statusRepo;

        private User user = null;

        public UserTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json.config", optional: true)
                .Build();

            userRepo = new UserRepo(configuration);
            roleRepo = new RoleRepo(configuration);
            jwtRepo = new JwtRepo(configuration);
            statusRepo = new StatusRepo(configuration);

            Status status = statusRepo.GetByName("Active");
            Role role = roleRepo.GetByName(1, "AppAdmin");

            user = new User();
            user.Email = "test@test.com";
            user.Password = IdentityHelper.Encripty.EncryptString("1111");
            user.Name = "Hüseyin";
            user.SurName = "Altunbaş";
            user.Status_Id = 1;//"4b36afc8-5205-49c1-af16-4dc6f96db984"; //status.Status_Id;
            user.Parent_Id = 0; //"4b36afc8-5205-49c1-af16-4dc6f96db984";
            //user.Role = new List<Role>();
            //user.Role.Add(role);
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = userRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void nativeQuery()
        {
            object y = userRepo.nativequery();
        }
        [TestMethod]
        public void AddUser()
        {

            int result = userRepo.Add(user);
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoginByEmail()
        {
            UserView userView = userRepo.LoginByEmail(user.Email, user.Password);
            Assert.AreNotEqual(userView, null);
        }

        [TestMethod]
        public void UserAddJwt()
        {
            Jwt jwt = (Jwt)jwtRepo.Add(user.Id, Guid.NewGuid().ToString(), DateTime.Now.AddDays(5)).Data;
        }

        [TestMethod]
        public void UserJwtTest()
        {
            Jwt jwt = (Jwt)jwtRepo.GetByUserId(user.Id).Data;
            Jwt jwtCheckToken = (Jwt)jwtRepo.CheckToken(jwt.Token).Data;
        }

        [TestMethod]
        public void AddSystemUser()
        {
            Role role = roleRepo.GetByName(1, "SystemAdmin");
            Assert.AreNotEqual(role, null);

            Status status = statusRepo.GetByName("Active");
            if (userRepo.GetByEmail("altunbas.huseyin@gmail.com") == null)
            {
                User user = new User();
                user.Id = 1;  //1c823a7d-7475-4c09-ad13-3b94a53ca943";
                user.Email = "altunbas.huseyin@gmail.com";
                user.Parent_Id = 0;//"00000000-0000-0000-0000-000000000000";
                user.Password = IdentityHelper.Encripty.EncryptString("1111");
                user.Name = "Hüseyin";
                user.SurName = "Altunbaş";
                user.Status_Id = status.Status_Id;
                //user.Role = new List<Role>();
                //user.Role.Add(role);

                userRepo.Add(user);
            }

            User _user = userRepo.GetByParentId(0, 1);

        }

        [TestMethod]
        public void Login()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json.config", optional: true)
                 .Build();
            Jwt jwt = (Jwt)jwtRepo.GetByUserId(user.Id).Data;
            Identity.Controllers1.LoginController userController = new Identity.Controllers1.LoginController(configuration);
            UserView userView = userRepo.LoginByEmail(user.Email, user.Password);

            Assert.AreNotEqual(userView, null);

        }

        //[TestMethod]
        // public void Register()
        // {
        //     UsersController userController = new UsersController();
        //     UserRegisterView _user = new UserRegisterView();
        //     _user.Email = "Huseyin";
        //     _user.Password = Encripty.EncryptString("1111");
        //     _user.Name = "Hüseyin";
        //     _user.SurName = "Altunbaş";
        //
        //     var result = userController.Post(_user);
        // }


        [TestMethod]
        public void DeleteUser()
        {
            bool result = userRepo.Delete(user.Id);
            Assert.AreEqual(result, true);

        }

    }
}
