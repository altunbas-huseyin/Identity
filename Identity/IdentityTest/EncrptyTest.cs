using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class EncrptyTest
    {

        [TestMethod]
        public void EncriptyDencripty()
        {
            var content = "huseyin";
          

            var encrypted = IdentityHelper.Encripty.EncryptString(content);
            var decrypted = IdentityHelper.Encripty.DecryptString(encrypted);

            Assert.AreEqual(decrypted, content);

        }

        
    }
}
