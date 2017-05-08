using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class JwtTest
    {
        JwtRepo jwtRepo = new JwtRepo();

        [TestMethod]
        public void AddUniqIndex()
        {
            bool result = jwtRepo.AddUniqIndex();
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void AddToken()
        {
            bool result = jwtRepo.Add("fcbe54b8-8798-4d30-b695-8ffb6539911c", Guid.NewGuid().ToString(), DateTime.Now.AddDays(5));
            Assert.AreEqual(result, true);
        }
    }
}
