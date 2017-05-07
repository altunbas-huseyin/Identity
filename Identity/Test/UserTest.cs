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
        [TestMethod]
        public void AddUser()
        {
            User user = new User();
            user.Email = "test@test.com";
            bool result = userRepo.Add("ee@tt.com", "1111", "isicam", "huseyin", "altunbas", User.UserType.Firm);
        }

        [TestMethod]
        public void LoginByEmail()
        {
            User user = userRepo.LoginByEmail("isicam", "ee@tt.com", "1111");
            
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = userRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }





    }
}
