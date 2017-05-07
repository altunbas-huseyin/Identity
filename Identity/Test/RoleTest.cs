using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
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
            bool result = roleRepo.InsertRoleList();
            Assert.AreEqual(result, true);
        }
    }
}
