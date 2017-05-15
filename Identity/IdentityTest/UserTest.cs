using Identity.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Users;

namespace IdentityTest
{
    [TestClass]
    public class UserTest
    {

        UserRepo userRepo = new UserRepo();
        RoleRepo roleRepo = new RoleRepo();
        JwtRepo jwtRepo = new JwtRepo();
        StatusRepo statusRepo = new StatusRepo();

        [TestMethod]
        public void AddUser()
        {
            Status status = statusRepo.GetByName("Active");
            Role role = roleRepo.GetByName("AppUser");
            Assert.AreNotEqual(role, null);

            if (userRepo.GetByEmail("ee@tt.com") == null)
            {
                User user = new User();
                user.Email = "ee@tt.com";
                user.Password = "1111";
                user.Name = "Hüseyin";
                user.SurName = "Altunbaş";
                user.StatusId = status.Id;
                user.Role = new List<Role>();
                user.Role.Add(role);

                userRepo.Add(user);
            }



        }

        [TestMethod]
        public void LoginByEmail()
        {
            User user = userRepo.LoginByEmail("ee@tt.com", "1111");

        }

        [TestMethod]
        public void UserLifeCycle()
        {
            Status status = statusRepo.GetByName("Active");
            Role role = roleRepo.GetByName("AppUser");

            Assert.AreNotEqual(status, null);
            Assert.AreNotEqual(role, null);


            string email = "test@" + Guid.NewGuid() + ".com";

            User _user = new User();
            _user.Email = email;
            _user.Password = "1111";
            _user.Name = "Hüseyin";
            _user.SurName = "Altunbaş";
            _user.StatusId = status.Id;
            _user.Role = new List<Role>();
            _user.Role.Add(role);

            userRepo.Add(_user);

            User user = userRepo.GetByEmail(email);
            jwtRepo.Add(user.Id.ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(3));
            Jwt jwt = jwtRepo.GetByUserId(user.Id.ToString());
            Jwt jwtCheckToken = jwtRepo.CheckToken(jwt.Token);
        }

        [TestMethod]
        public void AddSystemUser()
        {

            Role role = roleRepo.GetByName("SystemAdmin");
            Assert.AreNotEqual(role, null);

            Status status = statusRepo.GetByName("Active");
            if (userRepo.GetByEmail("altunbas.huseyin@gmail.com") == null)
            {
                User user = new User();
                user.Email = "altunbas.huseyin@gmail.com";
                user.Password = "1111";
                user.Name = "Hüseyin";
                user.SurName = "Altunbaş";
                user.StatusId = status.Id;
                user.Role = new List<Role>();
                user.Role.Add(role);

                userRepo.Add(user);
            }


        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = userRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Login()
        {
            UsersController controllerUnderTest = new UsersController();
            var result = controllerUnderTest.Login("altunbas.huseyin@gmail.com", "Web+webmercek");
            //Assert.AreEqual("fooview", result.ViewName);
        }


    }
}
