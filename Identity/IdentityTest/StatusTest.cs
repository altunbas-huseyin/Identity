using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityModels;
using Microsoft.Extensions.Configuration;

namespace IdentityTest
{
    [TestClass]
    public class StatusTest
    {
        StatusRepo statusRepo;

        public StatusTest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json.config", optional: true)
               .Build();

            statusRepo = new StatusRepo(configuration);
        }

        [TestMethod]
        public void AddUniqIndex()
        {
            statusRepo.AddUniqIndex();
        }

        [TestMethod]
        public void InsertStatusCodeList()
        {
            Status statusWaitingForApproval = new Status { Id = 1, Name = "WaitingForApproval" };
            Status statusActive = new Status { Id = 2, Name = "Active" };
            Status statusPassive = new Status { Id = 3, Name = "Passive" };
            Status statusWaiting = new Status { Id = 4, Name = "Waiting" };
            Status statusDeleted = new Status { Id = 5, Name = "Deleted" };

            if (statusRepo.GetByName("WaitingForApproval") == null)
                statusRepo.Insert(statusWaitingForApproval);

            if (statusRepo.GetByName("Active") == null)
                statusRepo.Insert(statusActive);

            if (statusRepo.GetByName("Passive") == null)
                statusRepo.Insert(statusPassive);

            if (statusRepo.GetByName("Waiting") == null)
                statusRepo.Insert(statusWaiting);

            if (statusRepo.GetByName("Deleted") == null)
                statusRepo.Insert(statusDeleted);

        }



    }
}
