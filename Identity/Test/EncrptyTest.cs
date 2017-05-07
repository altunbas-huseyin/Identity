using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestClass]
    public class EncrptyTest
    {

        [TestMethod]
        public void EncDenc()
        {
            var content = "huseyin";
          

            var encrypted = IdentityHelper.Encripty.EncryptString(content);
            var decrypted = IdentityHelper.Encripty.DecryptString(encrypted);

            Assert.AreEqual(decrypted, content);

        }

        [TestMethod]
        public void EncDenc1()
        {
            var content = "huseyin";


            var encrypted = IdentityHelper.Encripty.EncryptString(content);
            var decrypted = IdentityHelper.Encripty.DecryptString(encrypted);

            Assert.AreEqual(decrypted, content);

        }
    }
}
