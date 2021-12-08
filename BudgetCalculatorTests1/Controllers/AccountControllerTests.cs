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
    public class AccountControllerTests
    {
        AccountController Ac = new AccountController();

        [TestMethod()]
        public void LoginTestInvalidUserReturnFalse()
        {
            var user = new Account();
            var actual = Ac.Login(user.Username, user.Password);
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void LoginTestEmptyUserReturnFalse()
        {
            var actual = Ac.Login("", "");
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void RegisterTestNullUserReturnFalse()
        {
            var user = new Account();
            var actual = Ac.Register(user.Username, user.Password);
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void RegisterTestEmptyUserReturnFalse()
        {
            var actual = Ac.Register("","");
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void RegisterTestWhitespacesUserReturnFalse()
        {
            var actual = Ac.Register("          ", "  ");
            Assert.IsFalse(actual);
        }
        [TestMethod()]
        public void RegisterTestLetterAndWhitespacesUserReturnFalse()
        {
            var actual = Ac.Register("  b  ", "c    ");
            Assert.IsFalse(actual);
        }
    }
}