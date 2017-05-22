
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Users;
using IdentityHelper;

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
        public void nativeQuery()
        {
          object y=  userRepo.nativequery();
        }
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
                user.Status = status.Status;
                user.Role = new List<Role>();
                user.Role.Add(role);

                userRepo.Add(user);
            }

        }

        [TestMethod]
        public void LoginByEmail()
        {
            UserView user = userRepo.LoginByEmail("ee@tt.com", "1111");

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
            //_user.ParentId = _user._id;
            _user.Email = email;
            _user.Password = "1111";
            _user.Name = "Hüseyin";
            _user.SurName = "Altunbaş";
            _user.Status = status;
            _user.Role = new List<Role>();
            _user.Role.Add(role);
           
            userRepo.Add(_user);

            UserView user = userRepo.LoginByEmail(email,"1111");
            Assert.AreNotEqual(user, null);

            Jwt jwt = jwtRepo.GetByUserId(_user._id.ToString());
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
                user.ParentId = "00000000-0000-0000-0000-000000000000";
                user.Password = "1111";
                user.Name = "Hüseyin";
                user.SurName = "Altunbaş";
                user.Status = status.Status;
                user.Role = new List<Role>();
                user.Role.Add(role);

                userRepo.Add(user);
            }

            User _user = userRepo.GetByParentId("00000000-0000-0000-0000-000000000000");

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
           // Identity.Controllers1.lo userController = new Identity.Controllers1.LoginController();
           // var result = userController.Login("altunbas.huseyin@gmail.com", "Web+webmercek");
            //Assert.AreEqual("fooview", result.ViewName);
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

    }
}
