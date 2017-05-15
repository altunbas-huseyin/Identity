using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels;

namespace IdentityTest
{
    [TestClass]
    public class StatusTest
    {
        StatusRepo statusRepo = new StatusRepo();

        [TestMethod]
        public void AddUniqIndex()
        {
            statusRepo.AddUniqIndex();
        }

        [TestMethod]
        public void InsertStatusCodeList()
        {
            Status statusWaitingForApproval = new Status {Id=Guid.Parse("221fb324-1601-43cc-9969-05e13f2ad094"), Name = "WaitingForApproval" };
            Status statusActive = new Status  { Id = Guid.Parse("daa69d17-38e1-41a8-b9a4-04c044311da0"), Name = "Active" };
            Status statusPassive = new Status { Id = Guid.Parse("e3c573b0-1a2c-4da8-a6ae-221ab95e5248"), Name = "Passive" };
            Status statusWaiting = new Status { Id = Guid.Parse("34097424-3961-463b-b68b-ddd0ca855715"), Name = "Waiting" };
            Status statusDeleted = new Status { Id = Guid.Parse("494d8cf4-a5f1-4ef7-929a-1a4f3808031f"), Name = "Deleted" };

            bool result = statusRepo.InsertStatusCodeList(statusWaitingForApproval);
            Assert.AreEqual(result, true);

            result = statusRepo.InsertStatusCodeList(statusActive);
            Assert.AreEqual(result, true);

            result = statusRepo.InsertStatusCodeList(statusPassive);
            Assert.AreEqual(result, true);

            result = statusRepo.InsertStatusCodeList(statusWaiting);
            Assert.AreEqual(result, true);

            result = statusRepo.InsertStatusCodeList(statusDeleted);
            Assert.AreEqual(result, true);
        }



    }
}
