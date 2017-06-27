using Microsoft.VisualStudio.TestTools.UnitTesting;
using IdentityModels;
using IdentityRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityTest
{
    [TestClass]
    public class TableClassTest
    {
       
        [TestMethod]
        public void GetTableSchema()
        {
            Status r = new Status();
            TableClass TableClass = new TableClass(r.GetType());
            string schema = TableClass.CreateTableScript();
        }
    }
}
