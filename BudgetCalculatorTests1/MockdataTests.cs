using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Tests
{
    [TestClass()]
    public class MockdataTests
    {
        [TestMethod()]
        public void MockDatabaseTest()
        {
            var res = Mockdata.MockDatabase();
            Assert.IsTrue(res);
            //DbExtensions.EmptyAllTables();
        }

        [TestMethod()]
        public void ClearTables()
        {
            var x = DbExtensions.EmptyAllTables();
            Assert.IsTrue(x);
        }
    }
}