using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class RoleTest
    {
        RoleRepo roleRepo = new RoleRepo();

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = roleRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void InsertRoleList()
        {
            Role roleSystemAdmin = new Role { Id = Guid.Parse("1c823a7d-7475-4c09-ad13-3b94a53ca943"), Name = "SystemAdmin", Description = "Tüm sistemi kullanıcıları yönetebilen kullanıcı." };
            Role roleAppAdmin = new Role { Id = Guid.Parse("57daa98a-3c56-4f0e-9247-3a07ac1b4c08"), Name = "AppAdmin", Description = "X isimli bir proje üyelerinin yönetilebileceği bir hesap." };
            Role roleAppUser = new Role { Id = Guid.Parse("44211fbb-ed8a-405d-a639-9919f5fbbb3e"), Name = "AppUser", Description = "Herhangi bir AppAdmin kullanıcısının oluşturduğu kullanıcılar." };

            bool result = roleRepo.InsertRoleList(roleSystemAdmin);
            Assert.AreEqual(result, true);

            result = roleRepo.InsertRoleList(roleAppAdmin);
            Assert.AreEqual(result, true);

            result = roleRepo.InsertRoleList(roleAppUser);
            Assert.AreEqual(result, true);
        }
    }
}
