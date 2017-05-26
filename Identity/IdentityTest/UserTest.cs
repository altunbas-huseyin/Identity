
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Users;
using IdentityHelper;
using IdentityModels.Roles;

namespace IdentityTest
{
    [TestClass]
    public class UserTest
    {
        UserRepo userRepo = new UserRepo();
        RoleRepo roleRepo = new RoleRepo();
        JwtRepo jwtRepo = new JwtRepo();
        StatusRepo statusRepo = new StatusRepo();

        private User user = null;

        public UserTest()
        {
            Status status = statusRepo.GetByName("Active");
            Role role = roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", "AppAdmin");

            user = new User();
            user._id = "bra6b053-f21b-4304-8844-93f073465630";
            user.Email = "test@test.com";
            user.Password = "1111";
            user.Name = "Hüseyin";
            user.SurName = "Altunbaş";
            user.Status = status.Status;
            user.Role = new List<Role>();
            user.Role.Add(role);
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
            bool result = userRepo.Add(user);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void LoginByEmail()
        {
            UserView userView = userRepo.LoginByEmail(user.Email, user.Password);
            Assert.AreNotEqual(userView, null);
        }

        [TestMethod]
        public void DeleteUser()
        {
            bool result = userRepo.Delete(user._id);
            Assert.AreEqual(result, true);

        }

        [TestMethod]
        public void UserJwtTest()
        {
            Jwt jwt = jwtRepo.GetByUserId(user._id.ToString());
            Jwt jwtCheckToken = jwtRepo.CheckToken(jwt.Token);
        }

        [TestMethod]
        public void AddSystemUser()
        {
            Role role = roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", "SystemAdmin");
            Assert.AreNotEqual(role, null);

            Status status = statusRepo.GetByName("Active");
            if (userRepo.GetByEmail("altunbas.huseyin@gmail.com") == null)
            {
                User user = new User();
                user._id = "1c823a7d-7475-4c09-ad13-3b94a53ca943";
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
