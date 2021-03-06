﻿using IdentityModels;
using IdentityModels.RolePermissions;
using IdentityRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class RolePermissionTest
    {
        RolePermissionRepo permissionRepo = new RolePermissionRepo();
        RolePermission permission = new RolePermission();

        public RolePermissionTest()
        {
            permission = new RolePermission();
            permission._id = "11127a7e-eb62-442b-b0dd-05cc0102ebc1";
            //permission.OwnerId = "21127a7e-eb62-442b-b0dd-05cc0102ebc1"; //OwnerId sahip kullanıcı yani AppAdmin rolüne sahip olan kullanıcıdır.
            permission.UserId = "31127a7e-eb62-442b-b0dd-05cc0102ebc1";
            permission.PermissionId = "41127a7e-eb62-442b-b0dd-05cc0102ebc1";
            permission.RoleId = "51127a7e-eb62-442b-b0dd-05cc0102ebc1";
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
            bool result = permissionRepo.Delete(permission.UserId, permission._id);
            Assert.AreEqual(result, true);
        }
    }
}
