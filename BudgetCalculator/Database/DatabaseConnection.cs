using System;
using System.Collections.Generic;
using System.Linq;
namespace BudgetCalculator
{
    public class DatabaseConnection
    {
        #region Get

        #region Get Account

        public Account GetAccountById(int id)
        {
            var dbnew = new BudgetCalcDbContext();
            try
            {
                return dbnew.Accounts.Where(a => a.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        /// <summary>
        /// Used for Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account GetAccountByUsernameAndPassword(string username, string password)
        {
            var dbnew = new BudgetCalcDbContext();
            try
            {
                return dbnew.Accounts.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }
        #endregion Get Account

        #region Get Income

        public List<Income> GetIncomesOfUserId(int userId)
        {
            var dbnew = new BudgetCalcDbContext();
            try
            {

                var list = dbnew.Incomes.Where(a => a.AccountId == userId).ToList();
                if (list != null) return list;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }

            return null;
        }

        #endregion Get Income

        #region Get Expense

        public List<Expense> GetExpensesOfUserId(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {

                var list = dbnew.Expenses.Where(a => a.AccountId == userId).ToList();
                if (list != null) return list;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }

            return null;
        }
        #endregion Get Expense

        #region Get Saving
        public List<Saving> GetAllSavings(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;

                var results = dbnew.Savings.Where(s => s.AccountId == userId).ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public List<Saving> GetSavingsOfUserId(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {

                var list = dbnew.Savings.Where(a => a.AccountId == userId).ToList();
                if (list != null) return list;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
            return null;
        }
        #endregion Get Saving

        #region Get Goal

        public List<Goal> GetGoalsOfUserId(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var list = dbnew.Goals.Where(a => a.AccountId == userId).ToList();
                if (list != null) return list;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
            return null;
        }
        #endregion Get Goal

        public List<Account> GetAllAccountsAsAdmin(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;
                var results = dbnew.Accounts.ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public List<Income> GetAllIncomesAsAdmin(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;
                var results = dbnew.Incomes.ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public List<Expense> GetAllExpensesAsAdmin(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;
                var results = dbnew.Expenses.ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public List<Saving> GetAllSavingsAsAdmin(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;
                var results = dbnew.Savings.ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        public List<Goal> GetAllGoalsAsAdmin(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var user = CheckIfUserExist(userId);
                if (user == null) return null;
                var results = dbnew.Goals.ToList();

                return results;
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return null;
            }
        }

        #endregion Get

        #region Update Account
        public bool UpdateUsername(int userId, string username)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var acc = dbnew.Accounts.FirstOrDefault(a => a.Id == userId);

                if (acc != null)
                {
                    acc.Username = username;

                    dbnew.Accounts.Update(acc);
                    dbnew.SaveChanges();
                }
                else return false;
            }
            catch (Exception e)
            {
                return LogB(e);
            }

            return true;
        }

        public bool UpdatePassword(int userId, string password)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var acc = dbnew.Accounts.FirstOrDefault(a => a.Id == userId);

                if (acc != null)
                {
                    acc.Password = password;

                    dbnew.Accounts.Update(acc);
                    dbnew.SaveChanges();
                }
                else return false;
            }
            catch (Exception e)
            {
                return LogB(e);
            }

            return true;
        }
        #endregion Update Account

        #region Delete

        #region Delete Account
        public bool DeleteAccountById(int accountToBeDeletedId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var targetAccount = dbnew.Accounts.FirstOrDefault(a => a.Id == accountToBeDeletedId) as Account;
                if (targetAccount == null) return false;

                dbnew.Accounts.Remove(targetAccount);
                dbnew.SaveChanges();
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
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                if (CheckIfUserExist(userId) == null) return false;
                var incomes = dbnew.Incomes.Where(I => I.AccountId == userId).ToList();
                dbnew.Incomes.RemoveRange(incomes);
                //dbnew.Update(dbnew.Incomes);
                dbnew.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteIncomeById(int userId, int incomeId)
        {
            BudgetCalcDbContext dbnew = new();

            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                var targetIncome = dbnew.Incomes.FirstOrDefault(I => I.AccountId == userId && I.Id == incomeId) as Income;
                if (targetIncome == null) return false;

                dbnew.Incomes.Remove(targetIncome);
                dbnew.SaveChanges();
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
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                dbnew.Expenses.RemoveRange(dbnew.Expenses.Where(I => I.AccountId == userId).ToList());
                dbnew.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteExpenseById(int userId, int ExpenseId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                if (CheckIfUserExist(userId) == null) return false;

                var targetExpense = dbnew.Expenses.FirstOrDefault(I => I.AccountId == userId && I.Id == ExpenseId) as Expense;
                if (targetExpense == null) return false;

                dbnew.Expenses.Remove(targetExpense);
                dbnew.SaveChanges();
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
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = dbnew.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetSaving = dbnew.Savings.Where(I => I.AccountId == userId).ToList();
                if (targetSaving == null) return false;

                dbnew.Savings.RemoveRange(dbnew.Savings.Where(I => I.AccountId == user.Id));
                dbnew.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteSavingById(int userId, int savingId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = dbnew.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetSaving = dbnew.Savings.FirstOrDefault(I => I.AccountId == userId && I.Id == savingId) as Saving;
                if (targetSaving == null) return false;

                dbnew.Savings.Remove(targetSaving);
                dbnew.SaveChanges();
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
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = dbnew.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetGoal = dbnew.Goals.Where(I => I.AccountId == userId);
                if (targetGoal == null) return false;

                dbnew.Goals.RemoveRange(dbnew.Goals.Where(I => I.AccountId == user.Id).ToList());
                dbnew.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return LogB(e);
            }
        }

        public bool DeleteGoalById(int userId, int goalId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = dbnew.Accounts.FirstOrDefault(u => u.Id == userId);
                if (user == null) return false;

                var targetGoal = dbnew.Goals.FirstOrDefault(I => I.AccountId == userId && I.Id == goalId) as Goal;
                if (targetGoal == null) return false;

                dbnew.Goals.Remove(targetGoal);
                dbnew.SaveChanges();
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
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var accountExists = GetAccountByUsernameAndPassword(username, password);

                if (accountExists != null) return false;

                var newAccount = new Account()
                {
                    Username = username,
                    Password = password
                };

                dbnew.Accounts.Add(newAccount);
                dbnew.SaveChanges();
            }
            catch (Exception e)
            {
                return LogB(e);
            }

            return true;
        }

        public bool CreateEco(EconomicObject obj)
        {
            var dbnew = new BudgetCalcDbContext();

            try
            {
                if (obj == null) return false;

                if (obj is Income) dbnew.Incomes.Add(obj as Income);
                else if (obj is Expense) dbnew.Expenses.Add(obj as Expense);
                else if (obj is Saving) dbnew.Savings.Add(obj as Saving);
                else if (obj is Goal) dbnew.Goals.Add(obj as Goal);


                dbnew.SaveChanges();
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return false;
            }

            return true;
        }

        public bool UpdateEco(EconomicObject obj)
        {
            var dbnew = new BudgetCalcDbContext();

            try
            {
                if (obj == null) return false;

                if (obj is Income) dbnew.Incomes.Update(obj as Income);
                else if (obj is Expense) dbnew.Expenses.Update(obj as Expense);
                else if (obj is Saving) dbnew.Savings.Update(obj as Saving);
                else if (obj is Goal) dbnew.Goals.Update(obj as Goal);


                dbnew.SaveChanges();
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return false;
            }

            return true;
        }

        #endregion Create

        #region Refactored code
        public Account CheckIfUserExist(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            return dbnew.Accounts.FirstOrDefault(u => u.Id == userId);
        }

        private static bool LogB(Exception e)
        {
            ErrorLogger.Add(e.Message);
            return false;
        }

        public bool CheckIfUserExists(int userId)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();

            try
            {
                var user = dbnew.Accounts.Where(u => u.Id.Equals(userId)).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Add(ex.Message);
                return false;
            }
        }

        #endregion Refactored code
    }
}