using System;
using System.Collections.Generic;
using System.Linq;
namespace BudgetCalculator.Database
{
    public class DatabaseConnection
    {
        private static readonly BudgetCalcDbContext db = new();

        #region Get

        #region Get Account
        public IEnumerable<Account> GetAllAccounts(int adminId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(a => a.Id == adminId);

                if (user.IsAdmin) return db.Accounts.ToList();

                return null;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Account GetAccountById(int id)
        {
            try
            {
                return db.Accounts.Where(a => a.Id == id) as Account;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Account GetAccountByUsernameAndPassword(string username, string password)
        {
            try
            {
                return db.Accounts.Where(a => a.Username == username && a.Password == password) as Account;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        #endregion Get Account

        #region Get Income
        public IEnumerable<Income> GetAllIncomes(int adminId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(a => a.Id == adminId);
                if (user.IsAdmin) return db.Incomes.ToList();

                return null;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Income GetIncomeById(int id)
        {
            try
            {
                return db.Incomes.Where(a => a.Id == id) as Income;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        #endregion Get Income

        #region Get Expense
        public IEnumerable<Expense> GetAllExpenses(int adminId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(a => a.Id == adminId);
                if (user.IsAdmin) return db.Expenses.ToList();

                return null;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Expense GetExpenseById(int id)
        {
            try
            {
                return db.Expenses.Where(a => a.Id == id) as Expense;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        #endregion Get Expense

        #region Get Saving
        public IEnumerable<Saving> GetAllSavings(int userId)
        {
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;

                var results = db.Savings.Where(s => s.AccountId == userId);

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Saving GetSavingById(int id)
        {
            try
            {
                return db.Savings.Where(a => a.Id == id) as Saving;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        #endregion Get Saving

        #region Get Goal
        public IEnumerable<Goal> GetAllGoals(int adminId)
        {
            try
            {
                if (CheckIfUserExist(adminId).IsAdmin) return db.Goals.ToList();

                return null;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public Goal GetGoalById(int id)
        {
            try
            {
                return db.Goals.Where(a => a.Id == id) as Goal;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        #endregion Get Goal

        #endregion Get

        #region Update

        #region Update Account
        #endregion Update Account

        #region Update Income
        #endregion Update Income

        #region Update Expense
        #endregion Update Expense

        #region Update Saving
        #endregion Update Saving

        #region Update Goal
        #endregion Update Goal

        #endregion Update

        #region Delete

        #region Delete Account
        public bool DeleteAccountById(int accountToBeDeletedId)
        {
            try
            {
                var targetAccount = db.Accounts.FirstOrDefault(a => a.Id == accountToBeDeletedId) as Account;
                if (targetAccount == null) return false;

                db.Accounts.Remove(targetAccount);

                db.Update(db.Incomes);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }
        #endregion Delete Account

        #region Delete Income
        public bool DeleteAllIncome(int userId)
        {
            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                db.Incomes.RemoveRange(db.Incomes.Where(I => I.AccountId == userId));
                db.Update(db.Incomes);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteIncomeById(int userId, int incomeId)
        {
            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                var targetIncome = db.Incomes.FirstOrDefault(I => I.AccountId == userId && I.Id == incomeId) as Income;
                if (targetIncome == null) return false;

                var results = db.Incomes.Remove(targetIncome);

                if (results == null) return false;

                db.Update(db.Incomes);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }
        #endregion Delete Income

        #region Delete Expense
        public bool DeleteAllExpenses(int userId)
        {
            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                db.Expenses.RemoveRange(db.Expenses.Where(I => I.AccountId == userId));
                db.Update(db.Expenses);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteExpenseById(int userId, int ExpenseId)
        {
            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                var targetExpense = db.Expenses.FirstOrDefault(I => I.AccountId == userId && I.Id == ExpenseId) as Expense;
                if (targetExpense == null) return false;

                var results = db.Expenses.Remove(targetExpense);

                if (results == null) return false;

                db.Update(db.Expenses);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }
        #endregion Delete Expense

        #region Delete Saving
        public bool DeleteAllSavings(int userId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetSaving = db.Savings.Where(I => I.AccountId == userId);
                if (targetSaving == null) return false;

                db.Savings.RemoveRange(db.Savings.Where(I => I.AccountId == user.Id));
                db.Update(db.Savings);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteSavingById(int userId, int savingId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetSaving = db.Savings.FirstOrDefault(I => I.AccountId == userId && I.Id == savingId) as Saving;
                if (targetSaving == null) return false;

                var results = db.Savings.Remove(targetSaving);

                if (results == null) return false;

                db.Update(db.Savings);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }
        #endregion Delete Saving

        #region Delete Goal
        public bool DeleteAllGoals(int userId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetGoal = db.Goals.Where(I => I.AccountId == userId);
                if (targetGoal == null) return false;

                db.Goals.RemoveRange(db.Goals.Where(I => I.AccountId == user.Id));
                db.Update(db.Goals);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteGoalById(int userId, int goalId)
        {
            try
            {
                var user = db.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetGoal = db.Goals.FirstOrDefault(I => I.AccountId == userId && I.Id == goalId) as Goal;
                if (targetGoal == null) return false;

                var results = db.Goals.Remove(targetGoal);

                if (results == null) return false;

                db.Update(db.Goals);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }
        #endregion Delete Goal

        #endregion Delete

        #region Create
        public bool RegisterAccount(string username, string password)
        {
            try
            {
                var accountExists = GetAccountByUsernameAndPassword(username, password);

                if (accountExists != null) return false;

                var newAccount = new Account()
                {
                    Username = username,
                    Password = password
                };

                db.Add(newAccount);
                db.Update(newAccount);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return LogB(e);
            }

            return false;
        }
        #endregion Create

        #region Refactored code
        private static Account CheckIfUserExist(int userId)
        {
            return db.Accounts.FirstOrDefault(u => u.Id == userId);
        }

        private static bool LogB(Exception e)
        {
            ErrorLogger.Add(e.Message);
            return false;
        }

        #endregion Refactored code
    }
}
