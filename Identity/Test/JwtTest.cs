using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
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
            bool result = jwtRepo.Add("fcbe54b8-8798-4d30-b695-8ffb6539911c", "fcbe54b8-8798-4d30-b695-8ffb6539911c", DateTime.Now.AddDays(5));
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void GetToken()
        {
            Jwt result = jwtRepo.Get("fcbe54b8-8798-4d30-b695-8ffb6539911c");
            Assert.AreNotSame(result, null);
        }
    }
}
