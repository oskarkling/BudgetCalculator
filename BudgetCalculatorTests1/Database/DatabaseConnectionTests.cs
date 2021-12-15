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
            income.Name = "UpdatedIncomeName5";
            var result = dc.UpdateEco(income);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeIntervalTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();
            income.Interval = 12;

            var result = dc.UpdateEco(income);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeAmountTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();
            income.Amount = 5000000;

            var result = dc.UpdateEco(income);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateIncomeRecurringTest()
        {
            var user = GetUserAndEcos();
            var income = dc.GetIncomesOfUserId(user.Id).FirstOrDefault();
            income.Recurring = !income.Recurring;

            var result = dc.UpdateEco(income);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseNameTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            expense.Name = "UpdatedExpenseName";

            var result = dc.UpdateEco(expense);
            Assert.IsTrue(result);


            expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            Assert.AreEqual(expense.Name, "UpdatedExpenseName");
        }

        [TestMethod()]
        public void UpdateExpenseIntervalTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            expense.Interval = 12;

            var result = dc.UpdateEco(expense);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseAmountTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            expense.Amount = 123123123;

            var result = dc.UpdateEco(expense);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateExpenseRecurringTest()
        {
            var user = GetUserAndEcos();
            var expense = dc.GetExpensesOfUserId(user.Id).FirstOrDefault();
            expense.Recurring = !expense.Recurring;

            var result = dc.UpdateEco(expense);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingNameTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();
            savings.Name = "UpdatedSavingsName2";

            var result = dc.UpdateEco(savings);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingIntervalTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();
            savings.Interval = 7;

            var result = dc.UpdateEco(savings);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingAmountTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();
            savings.Amount = 123123;

            var result = dc.UpdateEco(savings);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateSavingRecurringTest()
        {
            var user = GetUserAndEcos();
            var savings = dc.GetSavingsOfUserId(user.Id).FirstOrDefault();
            savings.Recurring = !savings.Recurring;

            var result = dc.UpdateEco(savings);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalNameTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.Name = "UpdatedGoalName";

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalIntervalTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.Interval = 12;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalAmountTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.Amount = 32322;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalMonthsToGoalTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.MonthsToGoal = 122;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalCurrentTimeTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.CreationTime = DateTime.Now;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSaveToDateBoolTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.SaveToDate = !goal.SaveToDate;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSaveEachMonthTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.SaveEachMonth = 765;

            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateGoalSavedSoFarTest()
        {
            var user = GetUserAndEcos();
            var goal = dc.GetGoalsOfUserId(user.Id).FirstOrDefault();
            goal.AmountSavedSoFar = 1337;
            var result = dc.UpdateEco(goal);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteAccountByIdTest()
        {
            dc.RegisterAccount("PimpMyDragon", "BigDaddy");
            var user = dc.GetAccountByUsernameAndPassword("PimpMyDragon", "BigDaddy");
            var result = dc.DeleteAccountById(user.Id);
            user = dc.GetAccountById(user.Id);

            Assert.IsTrue(result);
            Assert.IsNull(user);
        }

        [TestMethod()]
        public void DeleteAllIncomeTest()
        {
            dc.RegisterAccount("Maximus", "Pässwörd");
            var user = dc.GetAccountByUsernameAndPassword("Maximus", "Pässwörd");
            var objs = CreateTestEco(user, "income", 2, "CSN", true, 1, 10000);
            var success1 = dc.CreateEco(objs.First());
            var success2 = dc.CreateEco(objs.Last());
            var incomes = dc.GetIncomesOfUserId(user.Id);
            var result = dc.DeleteAllIncome(user.Id);

            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
            Assert.IsNotNull(incomes);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteIncomeByIdTest()
        {
            dc.RegisterAccount("Picca", "ChOOOO");
            var user = dc.GetAccountByUsernameAndPassword("Picca", "ChOOOO");
            var obj = CreateTestEco(user, "income", 1, "Veckopeng", true, 1, 9999).First();
            var success1 = dc.CreateEco(obj);
            var income = dc.GetIncomesOfUserId(user.Id);
            var result = dc.DeleteIncomeById(user.Id, obj.Id);

            Assert.IsTrue(success1);
            Assert.IsNotNull(income);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteAllExpensesTest()
        {
            dc.RegisterAccount("Sture", "Banan");
            var user = dc.GetAccountByUsernameAndPassword("Sture", "Banan");
            var objs = CreateTestEco(user, "expense", 2, "Rent", true, 1, 5000);
            var success1 = dc.CreateEco(objs.First());
            var success2 = dc.CreateEco(objs.Last());
            var expenses = dc.GetExpensesOfUserId(user.Id);
            var result = dc.DeleteAllExpenses(user.Id);

            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
            Assert.IsNotNull(expenses);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteExpenseByIdTest()
        {
            dc.RegisterAccount("Pinoccio", "CooltPassword");
            var user = dc.GetAccountByUsernameAndPassword("Pinoccio", "CooltPassword");
            var obj = CreateTestEco(user, "expense", 1, "TestExpense", true, 1, 1234).First();
            var success1 = dc.CreateEco(obj);
            var expense = dc.GetExpensesOfUserId(user.Id);
            var result = dc.DeleteExpenseById(user.Id, obj.Id);

            Assert.IsTrue(success1);
            Assert.IsNotNull(expense);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteAllSavingsTest()
        {
            dc.RegisterAccount("Grizzlowe", "Shipmaster");
            var user = dc.GetAccountByUsernameAndPassword("Grizzlowe", "Shipmaster");
            var objs = CreateTestEco(user, "saving", 2, "Boat", false, 0, 20000000);
            var success1 = dc.CreateEco(objs.First());
            var success2 = dc.CreateEco(objs.Last());
            var savings = dc.GetSavingsOfUserId(user.Id);
            var result = dc.DeleteAllSavings(user.Id);

            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
            Assert.IsNotNull(savings);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteSavingByIdTest()
        {
            dc.RegisterAccount("Tervosh", "TheArchmage");
            var user = dc.GetAccountByUsernameAndPassword("Tervosh", "TheArchmage");
            var obj = CreateTestEco(user, "saving", 1, "Whiskey", true, 1, 500).First();
            var success1 = dc.CreateEco(obj);
            var saving = dc.GetSavingsOfUserId(user.Id);
            var result = dc.DeleteSavingById(user.Id, obj.Id);

            Assert.IsTrue(success1);
            Assert.IsNotNull(saving);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteAllGoalsTest()
        {
            dc.RegisterAccount("Proudmoore", "Lady1337");
            var user = dc.GetAccountByUsernameAndPassword("Proudmoore", "Lady1337");
            var objs = CreateTestEco(user, "goal", 2, "Tower", true, 1, 9999999, 12400, 0, false, 5000);
            var success1 = dc.CreateEco(objs.First());
            var success2 = dc.CreateEco(objs.Last());
            var goals = dc.GetGoalsOfUserId(user.Id);
            var result = dc.DeleteAllGoals(user.Id);

            Assert.IsTrue(success1);
            Assert.IsTrue(success2);
            Assert.IsNotNull(goals);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeleteGoalByIdTest()
        {
            dc.RegisterAccount("Grimble", "CooltPassword");
            var user = dc.GetAccountByUsernameAndPassword("Grimble", "CooltPassword");
            var obj = CreateTestEco(user, "goal", 2, "Tower", true, 1, 9999999, 12400, 0, false, 5000).First();
            var success1 = dc.CreateEco(obj);
            var goal = dc.GetGoalsOfUserId(user.Id);
            var result = dc.DeleteGoalById(user.Id, obj.Id);

            Assert.IsTrue(success1);
            Assert.IsNotNull(goal);
            Assert.IsTrue(result);
        }


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

        [TestMethod()]
        public void CreateIncomeTest()
        {
            var user = GetUser();
            var obj = CreateTestEco(user, "income", 1, "TestIncome3", true, 1, 300).First();
            var results = dc.CreateEco(obj);

            Assert.IsTrue(results);
        }

        [TestMethod()]
        public void CreateExpenseTest()
        {
            var user = GetUser();
            var obj = CreateTestEco(user, "expense", 1, "TestExpense2", true, 1, 300).First();
            var results = dc.CreateEco(obj);

            Assert.IsTrue(results);
        }

        [TestMethod()]
        public void CreateSavingTest()
        {
            var user = GetUser();
            var income = CreateTestEco(user, "saving", 1, "TestSaving2", true, 1, 300).First();
            var results = dc.CreateEco(income);

            Assert.IsTrue(results);
        }

        [TestMethod()]
        public void CreateGoalTest()
        {
            var user = GetUser();
            var income = CreateTestEco(user, "saving", 1, "TestGoal2", true, 1, 9999999, 5, 20, false, 20).First();
            var results = dc.CreateEco(income);

            Assert.IsTrue(results);
        }

        private List<EconomicObject> CreateTestEco(Account acc, string typeOfObj, int amountOfObjToCreate, string name, bool rec, int interval, int amount, decimal amountSavedSoFar = 0, int monthsToGoal = 0, bool saveToDate = false, decimal saveEachMonth = 0)
        {
            List<EconomicObject> ecos = new();

            if (typeOfObj.ToLower() == "income")
            {
                if (amountOfObjToCreate == 1)
                {
                    ecos.Add(
                        new Income
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
                else
                {
                    ecos.Add(
                        new Income
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                    ecos.Add(
                        new Income
                        {
                            
                            AccountId = acc.Id,
                            Name = name + "1",
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
            }
            else if (typeOfObj.ToLower() == "expense")
            {
                if (amountOfObjToCreate == 1)
                {
                    ecos.Add(
                        new Expense
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
                else
                {
                    ecos.Add(
                        new Expense
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                    ecos.Add(
                        new Expense
                        {
                            
                            AccountId = acc.Id,
                            Name = name + "1",
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
            }
            else if (typeOfObj.ToLower() == "saving")
            {
                if (amountOfObjToCreate == 1)
                {
                    ecos.Add(
                        new Saving
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
                else
                {
                    ecos.Add(
                        new Saving
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                    ecos.Add(
                        new Saving
                        {
                            
                            AccountId = acc.Id,
                            Name = name + "1",
                            Recurring = rec,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval
                        });
                }
            }
            else if (typeOfObj.ToLower() == "goal")
            {
                if (amountOfObjToCreate == 1)
                {
                    ecos.Add(
                        new Goal
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval,
                            AmountSavedSoFar = amountSavedSoFar,
                            MonthsToGoal = monthsToGoal,
                            SaveToDate = saveToDate,
                            CurrentTime = DateTime.Now,
                            SaveEachMonth = saveEachMonth
                        });
                }
                else
                {
                    ecos.Add(
                        new Goal
                        {
                            
                            AccountId = acc.Id,
                            Name = name,
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval,
                            AmountSavedSoFar = amountSavedSoFar,
                            MonthsToGoal = monthsToGoal,
                            SaveToDate = saveToDate,
                            CurrentTime = DateTime.Now,
                            SaveEachMonth = saveEachMonth
                        });
                    ecos.Add(
                        new Goal
                        {
                            
                            AccountId = acc.Id,
                            Name = name+"1",
                            Amount = amount,
                            CreationTime = DateTime.Now,
                            Interval = interval,
                            AmountSavedSoFar = amountSavedSoFar,
                            MonthsToGoal = monthsToGoal,
                            SaveToDate = saveToDate,
                            CurrentTime = DateTime.Now,
                            SaveEachMonth = saveEachMonth
                        });
                }
            }
            return ecos;
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
                        Amount = 900900,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        AmountSavedSoFar = 0,
                        SaveToDate = false
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