using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class UserTypeTest
    {

        UserTypeRepo userTypeRepo = new UserTypeRepo();

        [TestMethod]
        public void InsertTypeList()
        {
            userTypeRepo.Add("Firm");
            userTypeRepo.Add("StandartUser");
        }


        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = userTypeRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }





    }
}
