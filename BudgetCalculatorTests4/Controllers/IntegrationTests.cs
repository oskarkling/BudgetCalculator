﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetCalculator
{
    [TestClass()]
    public class IntegrationTests
    {
        /// <summary>
        /// Registers a user
        /// Logs in
        /// Creates a bunch of economic objects
        /// Updates them
        /// Deletes them
        /// </summary>
        [TestMethod()]
        public void IntegrationTest()
        {
            AccountController ac = new();
            List<bool> results = new();
            bool success = true;

            ac.Register("Nicklas", "Password");
            results.Add(ac.Login("Nicklas", "Password"));
            var acc = ac.CurrentAccount;

            AddEcosForIntegrationTest(ac, results, acc);

            foreach (var result in results)
            {
                if (!result) success = false;
            }

            Assert.IsTrue(success);

            var incomes = ac.CurrentAccount.Incomes;
            Assert.IsNotNull(incomes);
            var expenses = ac.CurrentAccount.Expenses;
            Assert.IsNotNull(expenses);
            var savings = ac.CurrentAccount.Savings;
            Assert.IsNotNull(savings);
            var goals = ac.CurrentAccount.Goals;
            Assert.IsNotNull(goals);

            UpdateIncome(ac, incomes);
            UpdateExpense(ac, expenses);
            UpdateSaving(ac, savings);
            UpdateGoal(ac, goals);

            DeleteIncome(ac, incomes);
            DeleteExpense(ac, expenses);
            DeleteSaving(ac, savings);
            DeleteGoal(ac, goals);
        }

        private static void DeleteIncome(AccountController ac, List<Income> incomes)
        {
            var selectedIncome = incomes.Last();
            Assert.IsTrue(ac.DeleteObject(selectedIncome));
        }

        private static void DeleteExpense(AccountController ac, List<Expense> expenses)
        {
            var selectedExpense = expenses.Last();
            Assert.IsTrue(ac.DeleteObject(selectedExpense));
        }

        private static void DeleteSaving(AccountController ac, List<Saving> savings)
        {
            var selectedSaving = savings.Last();
            Assert.IsTrue(ac.DeleteObject(selectedSaving));
        }

        private static void DeleteGoal(AccountController ac, List<Goal> goals)
        {
            var selectedGoal = goals.Last();
            Assert.IsTrue(ac.DeleteObject(selectedGoal));
        }

        private static void UpdateGoal(AccountController ac, List<Goal> goals)
        {
            var selectedGoal = goals.First();
            selectedGoal.Name = "UpdatedGoalName";
            selectedGoal.Amount = 1337;
            Assert.IsTrue(ac.UpdateObject(selectedGoal));
        }

        private static void UpdateSaving(AccountController ac, List<Saving> savings)
        {
            var selectedSaving = savings.First();
            selectedSaving.Name = "UpdatedSavingName";
            selectedSaving.Amount = 1337;
            Assert.IsTrue(ac.UpdateObject(selectedSaving));
        }

        private static void UpdateExpense(AccountController ac, List<Expense> expenses)
        {
            var selectedExpense = expenses.First();
            selectedExpense.Name = "UpdatedExpenseName";
            selectedExpense.Amount = 1337;
            Assert.IsTrue(ac.UpdateObject(selectedExpense));
        }

        private static void UpdateIncome(AccountController ac, List<Income> incomes)
        {
            var selectedIncome = incomes.First();
            selectedIncome.Name = "UpdatedIncomeName";
            selectedIncome.Amount = 1337;
            Assert.IsTrue(ac.UpdateObject(selectedIncome));
        }

        private static void AddEcosForIntegrationTest(AccountController ac, List<bool> results, Account acc)
        {
            results.Add(ac.CreateAnEconomicObject(new Income { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Lön", Interval = 1, Recurring = true, Amount = 30000 }));
            results.Add(ac.CreateAnEconomicObject(new Income { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Lotto", Interval = 0, Recurring = false, Amount = 500 }));
            results.Add(ac.CreateAnEconomicObject(new Income { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Bonus", Interval = 12, Recurring = true, Amount = 5000 }));

            results.Add(ac.CreateAnEconomicObject(new Expense { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Hyra", Interval = 1, Recurring = true, Amount = 7000 }));
            results.Add(ac.CreateAnEconomicObject(new Expense { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Mat", Interval = 1, Recurring = true, Amount = 3000 }));
            results.Add(ac.CreateAnEconomicObject(new Expense { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "ElRäkning", Interval = 3, Recurring = true, Amount = 2000 }));
            results.Add(ac.CreateAnEconomicObject(new Expense { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Cykel", Interval = 0, Recurring = false, Amount = 8000 }));

            results.Add(ac.CreateAnEconomicObject(new Saving { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Pensionsspar", Interval = 1, Recurring = true, Amount = 400 }));
            results.Add(ac.CreateAnEconomicObject(new Saving { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Spar", Interval = 1, Recurring = true, Amount = 2000 }));
            results.Add(ac.CreateAnEconomicObject(new Saving { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Bonus Spar", Interval = 0, Recurring = false, Amount = 1000 }));
            results.Add(ac.CreateAnEconomicObject(new Saving { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Spar av bonus", Interval = 12, Recurring = true, Amount = 5000 }));

            results.Add(ac.CreateAnEconomicObject(new Goal { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Rosa helikopter", Interval = 1, SaveEachMonth = 500, Amount = 1000000, SaveToDate = false, AmountSavedSoFar = 0 }));
            results.Add(ac.CreateAnEconomicObject(new Goal { AccountId = acc.Id, CreationTime = DateTime.Now, Name = "Bil", Interval = 1, MonthsToGoal = 24, Amount = 700000, SaveToDate = true, AmountSavedSoFar = 50000 }));
        }
    }
}