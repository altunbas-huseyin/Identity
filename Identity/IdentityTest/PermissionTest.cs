using IdentityModels;
using IdentityModels.Permissions;
using IdentityRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class PermissionTest
    {
        PermissionRepo permissionRepo;
        Permission permission = new Permission();

        public PermissionTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json.config", optional: true)
                 .Build();
            permissionRepo = new PermissionRepo(configuration);
            permission = new Permission { User_Id = 1, Id= 1, Name = "Test", Description = "Test" };
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = permissionRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddPermission()
        {
            bool result = permissionRepo.Insert(permission);
            Assert.AreEqual(result, true);
        }


        [TestMethod]
        public void UpdatePermission()
        {
            bool result = permissionRepo.Update(permission);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void DeletePermission()
        {
            bool result = permissionRepo.Delete(permission.User_Id, permission.Id);
            Assert.AreEqual(result, true);
        }
    }
}
