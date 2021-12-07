using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetCalculator.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator.Database.Tests
{
    [TestClass()]
    public class DatabaseConnectionTests
    {
        //[TestMethod()]
        //public void GetIncomeByIdTest()
        //{
        //    var x = new DatabaseConnection();
        //    var res = x.RegisterAccount("username123asd", "password123");
            
        //    Assert.IsTrue(res);
        //}

        [TestMethod()]
        public void CheckIfUserExists()
        {
            var dc = new DatabaseConnection();
            var res = dc.CheckIfUserExist(5);

            var acc = new Account();

            Assert.ReferenceEquals(acc, res);
        }

        [TestMethod()]
        public void CheckIfIncomeExists()
        {
            var dc = new DatabaseConnection();
            var res = dc.GetIncomeById(5);

            var i = new Income();

            Assert.ReferenceEquals(i, res);
        }
        

    }
}