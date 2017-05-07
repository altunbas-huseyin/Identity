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
            var key = "E546C8DF278CD5931069B522E695D4F2";

            var encrypted = IdentityHelper.Encripty.EncryptString(content, key);
            var decrypted = IdentityHelper.Encripty.DecryptString(encrypted, key);

            Assert.AreEqual(decrypted, content);

        }
    }
}
