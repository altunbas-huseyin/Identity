using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels.Roles;

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
            Role roleSystemAdmin = new Role { _id = "1c823a7d-7475-4c09-ad13-3b94a53ca943", UserId = "1c823a7d-7475-4c09-ad13-3b94a53ca943", Name = "SystemAdmin", Description = "Tüm sistemi kullanıcıları yönetebilen kullanıcı." };
            Role roleAppAdmin = new Role { _id = "57daa98a-3c56-4f0e-9247-3a07ac1b4c08", UserId = "1c823a7d-7475-4c09-ad13-3b94a53ca943", Name = "AppAdmin", Description = "X isimli bir proje üyelerinin yönetilebileceği bir hesap." };
            Role roleAppUser = new Role { _id = "44211fbb-ed8a-405d-a639-9919f5fbbb3e", UserId = "1c823a7d-7475-4c09-ad13-3b94a53ca943", Name = "AppUser", Description = "Herhangi bir AppAdmin kullanıcısının oluşturduğu kullanıcılar." };

            if (roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", "SystemAdmin") == null)
                roleRepo.Insert(roleSystemAdmin);

            if (roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", "AppAdmin") == null)
                roleRepo.Insert(roleAppAdmin);

            if (roleRepo.GetByName("1c823a7d-7475-4c09-ad13-3b94a53ca943", "AppUser") == null)
                roleRepo.Insert(roleAppUser);

        }
    }
}
