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
            //Mockdata.MockDatabase();
            DbExtensions.EmptyAllTables();


            Assert.Fail();
        }
    }
}