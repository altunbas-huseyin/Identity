using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class UserTypeTest
    {

        UserTypeRepo userTypeRepo = new UserTypeRepo();

        [TestMethod]
        public void InsertTypeList()
        {
            if (userTypeRepo.GetByName("Firm") == null)
                userTypeRepo.Add("Firm");

            if (userTypeRepo.GetByName("StandartUser") == null)
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
