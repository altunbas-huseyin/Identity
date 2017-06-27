using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Roles;
using Microsoft.Extensions.Configuration;

namespace IdentityTest
{
    [TestClass]
    public class RoleTest
    {
        RoleRepo roleRepo;

        public RoleTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json.config", optional: true)
               .Build();
            roleRepo = new RoleRepo(configuration);
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = roleRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void InsertRoleList()
        {
            Role roleSystemAdmin = new Role { Id =1, User_Id = 1, Name = "SystemAdmin", Description = "Tüm sistemi kullanıcıları yönetebilen kullanıcı." };
            Role roleAppAdmin = new Role { Id = 2, User_Id = 1, Name = "AppAdmin", Description = "X isimli bir proje üyelerinin yönetilebileceği bir hesap." };
            Role roleAppUser = new Role { Id = 3, User_Id = 1, Name = "AppUser", Description = "Herhangi bir AppAdmin kullanıcısının oluşturduğu kullanıcılar." };

            if (roleRepo.GetByName(1, "SystemAdmin") == null)
                roleRepo.Insert(roleSystemAdmin);

            if (roleRepo.GetByName(2, "AppAdmin") == null)
                roleRepo.Insert(roleAppAdmin);

            if (roleRepo.GetByName(3, "AppUser") == null)
                roleRepo.Insert(roleAppUser);

        }
    }
}
