using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void GetGoalsOfUserIdTest()
        {
            var user = GetUser();
            var goals = dc.GetGoalsOfUserId(user.Id);

            Assert.IsNotNull(goals);
        }

        [TestMethod()]
        public void UpdateUsernameTest()
        {
            var user = GetUser();
            var result = dc.UpdateUsername(user.Id, "TestTest");
            user = dc.GetAccountByUsernameAndPassword("TestTest", user.Password);

            Assert.IsTrue(result);
            Assert.AreEqual(user.Username, "TestTest");
        }

        [TestMethod()]
        public void UpdatePasswordTest()
        {
            var user = GetUser();
            var result = dc.UpdatePassword(user.Id, "NewPassword");
            user = dc.GetAccountByUsernameAndPassword(user.Username, "NewPassword");

            Assert.IsTrue(result);
            Assert.AreEqual(user.Password, "NewPassword");
        }

        [TestMethod()]
        public void UpdateIncomeNameTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();
            var result = dc.UpdateIncomeName(income.Id, user.Id, "UpdatedIncomeName5");

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeIntervalTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateIncomeInterval(income.Id, user.Id, 12);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeAmountTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateIncomeAmount(income.Id, user.Id, 5000000);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeRecurringTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateIncomeRecurring(income.Id, user.Id, !income.Recurring);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseNameTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            var result = dc.UpdateExpenseName(expense.Id, user.Id, "UpdatedExpenseName");
            expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();

            Assert.IsTrue(result);
            Assert.AreEqual(expense.Name, "UpdatedExpenseName");
        }

        [TestMethod()]
        public void UpdateExpenseIntervalTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateExpenseInterval(expense.Id, user.Id, 12);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseAmountTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateExpenseAmount(expense.Id, user.Id, 123123123);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseRecurringTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateExpenseRecurring(expense.Id, user.Id, true);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingNameTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateSavingName(savings.Id, user.Id, "UpdatedSavingsName2");
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingIntervalTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateSavingInterval(savings.Id, user.Id, 7);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingAmountTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateSavingAmount(savings.Id, user.Id, 123123);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingRecurringTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateSavingRecurring(savings.Id, user.Id, false);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalNameTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalName(goal.Id, user.Id, "UpdatedGoalName");
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalIntervalTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            var result = dc.UpdateGoalInterval(goal.Id, user.Id, 12);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalGoalAmountTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalGoalAmount(goal.Id, user.Id, 333333);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalAmountTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalAmount(goal.Id, user.Id, 3232);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalMonthsToGoalTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalMonthsToGoal(goal.Id, user.Id, 122);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalCurrentTimeTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalCurrentTime(goal.Id, user.Id, DateTime.Now);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSaveToDateBoolTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalSaveToDateBool(goal.Id, user.Id, false);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSaveEachMonthTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalSaveEachMonth(goal.Id, user.Id, 765);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSavedSoFarTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();

            var result = dc.UpdateGoalSavedSoFar(goal.Id, user.Id, 1337);
            Assert.IsTrue(result);
        }

        //[TestMethod()]
        //public void DeleteAccountByIdTest()
        //{
        //    dc.RegisterAccount("PimpMyDragon", "BigDaddy");
        //    var user = dc.GetAccountByUsernameAndPassword("PimpMyDragon", "BigDaddy");
        //    var result = dc.DeleteAccountById(user.Id);
        //    user = dc.GetAccountById(user.Id);

        //    Assert.IsTrue(result);
        //    Assert.IsNull(user);
        //}

        //[TestMethod()]
        //public void DeleteAllIncomeTest()
        //{
        //    dc.RegisterAccount("Maximus", "Pässwörd");
        //    //dc.AddIncomes();
        //    var user = dc.GetAccountByUsernameAndPassword("PimpMyDragon", "BigDaddy");
        //    var user = GetUser();
        //    var incomes = dc.GetIncomesOfUserId(user.Id);
        //    var result = dc.DeleteAllIncome(user.Id);

        //    Assert.IsNull(incomes);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteIncomeByIdTest()
        //{
        //    var user = GetUser();
        //    var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();
        //    var result = dc.DeleteIncomeById(user.Id, income.Id);

        //    Assert.IsNull(income);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteAllExpensesTest()
        //{
        //    var user = GetUser();
        //    var expenses = dc.GetExpensesOfUserId(user.Id);
        //    var result = dc.DeleteAllExpenses(user.Id);

        //    Assert.IsNull(expenses);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteExpenseByIdTest()
        //{
        //    var user = GetUser();
        //    var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
        //    var result = dc.DeleteExpenseById(user.Id, expense.Id);

        //    Assert.IsNull(expense);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteAllSavingsTest()
        //{
        //    var user = GetUser();
        //    var savings = dc.GetSavingsOfUserId(user.Id);
        //    var result = dc.DeleteAllSavings(user.Id);

        //    Assert.IsNull(savings);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteSavingByIdTest()
        //{
        //    var user = GetUser();
        //    var saving = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();
        //    var result = dc.DeleteSavingById(user.Id, saving.Id);

        //    Assert.IsNull(saving);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteAllGoalsTest()
        //{
        //    var user = GetUser();
        //    var goals = dc.GetGoalsOfUserId(user.Id);
        //    var result = dc.DeleteAllGoals(user.Id);

        //    Assert.IsNull(goals);
        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void DeleteGoalByIdTest()
        //{
        //    var user = GetUser();
        //    var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
        //    var result = dc.DeleteGoalById(user.Id, goal.Id);

        //    Assert.IsNull(goal);
        //    Assert.IsTrue(result);
        //}

        [TestMethod()]
        public void RegisterAccountTest()
        {
            var accountExists = dc.GetAccountByUsernameAndPassword("CoolDragon", "CantCrackThisNaNaNaNa");
            var refAcc = new Account();
            if (accountExists == null)
            {
                var success = dc.RegisterAccount("CoolDragon", "CantCrackThisNaNaNaNa");
                Assert.IsTrue(success);
            }

            Assert.ReferenceEquals(accountExists, refAcc);

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

            if (user == null)
            {
                var success = dc.RegisterAccount("DragonSlayer1337", "TopSecretPassword123");
                user = dc.GetAccountByUsernameAndPassword("DragonSlayer1337", "TopSecretPassword123");
            }

            return user;
        }

        private Account GetUserAndEcos()
        {
            var user = dc.GetAccountByUsernameAndPassword("DragonSlayer1337", "TopSecretPassword123");

            if (user == null)
            {
                var success = dc.RegisterAccount("DragonSlayer1337", "TopSecretPassword123");
                user = dc.GetAccountByUsernameAndPassword("DragonSlayer1337", "TopSecretPassword123");
            }
            if (user.Incomes == null || user.Expenses == null || user.Savings == null || user.Goals == null || user.Incomes.Count == 0 || user.Expenses.Count == 0 || user.Savings.Count == 0 || user.Goals.Count == 0)
            {
                using (var db = new BudgetCalcDbContext())
                {
                    var incomes = new List<Income>()
                    {
                        new Income
                        {
                            Name = "lön",
                            Amount = 120000,
                            Recurring = true,
                            CreationTime = DateTime.Now,
                            Interval = 1
                        }
                    };

                    var expenses = new List<Expense>()
                    {
                        new Expense
                        {
                            Name = "hyra",
                            Amount = 6006,
                            Recurring = true,
                            CreationTime = DateTime.Now,
                            Interval = 1
                        }
                    };

                    var savings = new List<Saving>()
                    {
                        new Saving
                        {
                            Name = "pension",
                            Amount = 99,
                            Recurring = true,
                            CreationTime = DateTime.Now,
                            Interval = 1,
                        }
                    };

                    var goals = new List<Goal>()
                    {
                        new Goal
                        {
                        Name = "HELIKOPTER - rosa",
                        Amount = 900,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        GoalAmount = 999999,
                        AmountSavedSoFar = 0,
                        SaveToDate = false,
                        }
                    };

                    user.Incomes = incomes;
                    user.Expenses = expenses;
                    user.Savings = savings;
                    user.Goals = goals;

                    db.Accounts.Update(user);
                    db.SaveChanges();
                }
            }

            return user;
        }
    }
}