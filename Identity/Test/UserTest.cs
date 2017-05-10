﻿using Identity.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
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
            bool result = userRepo.Add("ee@tt.com", "1111", "isicam", "huseyin", "altunbas", status.Id, new List<Role>());
        }

        [TestMethod]
        public void LoginByEmail()
        {
            User user = userRepo.LoginByEmail("ee@tt.com", "1111");

        }

        [TestMethod]
        public void UserLifeCycle()
        {
            List<Role> roles = new List<Role>();
            roles.Add(roleRepo.Get("AppUser"));
            roles.Add(roleRepo.Get("AppAdmin"));

            Status status = statusRepo.GetByName("Active");

            string email = "test@" + Guid.NewGuid() + ".com";
            bool result = userRepo.Add(email, "1111", "isicam", "huseyin", "altunbas", status.Id, roles);
            User user = userRepo.GetByEmail(email);
            bool jwtResult = jwtRepo.Add(user.Id.ToString(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(3));
            Jwt jwt = jwtRepo.GetByUserId(user.Id.ToString());
            Jwt jwtCheckToken = jwtRepo.CheckToken(jwt.Token);
        }

        [TestMethod]
        public void AddSystemUser()
        {
            List<Role> roles = new List<Role>();
            roles.Add(roleRepo.Get("SystemAdmin"));

            Status status = statusRepo.GetByName("Active");

            string email = "altunbas.huseyin@gmail.com";
            bool result = userRepo.Add(email, "Web+webmercek", "System", "huseyin", "altunbas", status.Id, roles);
           
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = userRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void ff()
        {
            UsersController controllerUnderTest = new UsersController();
            var result = controllerUnderTest.Index("altunbas.huseyin@gmail.com", "Web+webmercek");
            //Assert.AreEqual("fooview", result.ViewName);
        }


    }
}
