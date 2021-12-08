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
    public class DatabaseConnectionTests
    {
        private readonly DatabaseConnection dc = new();

        [TestMethod()]
        public void GetAccountByIdTest()
        {
            var user = GetUser();
            var resultAcc = dc.GetAccountById(user.Id);
            var accRef = new Account();

            Assert.ReferenceEquals(resultAcc, accRef);
        }

        [TestMethod()]
        public void GetAccountByUsernameAndPasswordTest()
        {
            var user = GetUser();
            var accRef = new Account();

            Assert.ReferenceEquals(user, accRef);
        }

        [TestMethod()]
        public void GetIncomesOfUserIdTest()
        {
            var user = GetUser();
            var result = dc.GetIncomesOfUserId(user.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetAllExpensesTest()
        {
            var user = GetUser();
            var results = dc.GetAllExpenses(user.Id);

            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void GetExpensesOfUserIdTest()
        {
            var user = GetUser();
            var expenses = dc.GetExpensesOfUserId(user.Id);

            Assert.IsNotNull(expenses);
        }

        [TestMethod()]
        public void GetAllSavingsTest()
        {
            var user = GetUser();
            var savings = dc.GetAllSavings(user.Id);

            Assert.IsNotNull(savings);
        }

        [TestMethod()]
        public void GetSavingsOfUserIdTest()
        {
            var user = GetUser();
            var results = dc.GetSavingsOfUserId(user.Id);

            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void GetAllGoalsTest()
        {
            var user = GetUser();
            var results = dc.GetAllGoals(user.Id);

            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void GetGoalsOfUserIdTest()
        {
            var user = GetUser();
            var goals = dc.GetGoalsOfUserId(user.Id);

            Assert.IsNotNull(goals);
        }

        //[TestMethod()]
        //public void UpdateUsernameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdatePasswordTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateIncomeNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateIncomeIntervalTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateIncomeAmountTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateIncomeRecurringTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateExpenseNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateExpenseIntervalTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateExpenseAmountTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateExpenseRecurringTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateSavingNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateSavingIntervalTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateSavingAmountTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateSavingRecurringTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalNameTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalIntervalTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalGoalAmountTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalAmountTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalMonthsToGoalTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalCurrentTimeTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalSaveToDateBoolTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalSaveEachMonthTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void UpdateGoalSavedSoFarTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAccountByIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAllIncomeTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteIncomeByIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAllExpensesTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteExpenseByIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAllSavingsTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteSavingByIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAllGoalsTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteGoalByIdTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void RegisterAccountTest()
        {
            var success = dc.RegisterAccount("CoolDragon", "CantCrackThisNaNaNaNa");

            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void CheckIfUserExistTest()
        {
            var user = GetUser();
            var results = dc.CheckIfUserExist(user.Id);
            var referenceAcc = new Account();

            Assert.ReferenceEquals(results, referenceAcc);
        }

        [TestMethod()]
        public void CheckIfUserExistsTest()
        {
            var user = GetUser();
            var results = dc.CheckIfUserExists(user.Id);

            Assert.IsTrue(results);
        }

        private Account GetUser()
        {
            var user = dc.GetAccountByUsernameAndPassword("DragonSlayer1337", "TopSecretPassword123");

           if(user == null)
            {
                var success = dc.RegisterAccount("DragonSlayer1337", "TopSecretPassword123");
                user = dc.GetAccountByUsernameAndPassword("DragonSlayer1337", "TopSecretPassword123");
            }

            return user;
        }
    }
}