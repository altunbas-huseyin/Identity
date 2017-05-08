using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
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
            statusRepo.InsertStatusCodeList();
        }

        

    }
}
