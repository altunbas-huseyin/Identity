using IdentityModels;
using IdentityModels.RolePermissions;
using IdentityRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class RolePermissionTest
    {
        RolePermissionRepo permissionRepo;
        Role_Permission permission = new Role_Permission();

        public RolePermissionTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json.config", optional: true)
               .Build();

            permissionRepo = new RolePermissionRepo(configuration);

            permission = new Role_Permission();
            permission.Id = "11127a7e-eb62-442b-b0dd-05cc0102ebc1";
            //permission.OwnerId = "21127a7e-eb62-442b-b0dd-05cc0102ebc1"; //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            permission.User_Id = "31127a7e-eb62-442b-b0dd-05cc0102ebc1";
            permission.Permission_Id = "41127a7e-eb62-442b-b0dd-05cc0102ebc1";
            permission.Role_Id = "51127a7e-eb62-442b-b0dd-05cc0102ebc1";
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
