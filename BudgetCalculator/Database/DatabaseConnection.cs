using System;
using System.Collections.Generic;
using System.Linq;
namespace BudgetCalculator
{
    public class DatabaseConnection
    {
        private static BudgetCalcDbContext db = new();

        #region Get

        #region Get Account
    
        public Account GetAccountById(int id)
        {
            try
            {
                return db.Accounts.Where(a => a.Id == id).FirstOrDefault();
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
            try
            {
                return db.Accounts.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
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
            try
            {

                var list = db.Incomes.Where(a => a.AccountId == userId).ToList();
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
        public List<Saving> GetSavingsOfUserId(int userId)
        {
            try
            {

                var list = db.Savings.Where(a => a.AccountId == userId).ToList();
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

        #endregion Get

        #region Update

        #region Update Account
        public bool UpdateUsername(int userId, string username)
        {
            try
            {
                var acc = db.Accounts.FirstOrDefault(a => a.Id == userId);

                if (acc != null)
                {
                    acc.Username = username;

                    db.Accounts.Update(acc);
                    db.SaveChanges();
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
            try
            {
                var acc = db.Accounts.FirstOrDefault(a => a.Id == userId);

                if (acc != null)
                {
                    acc.Password = password;

                    db.Accounts.Update(acc);
                    db.SaveChanges();
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

        #region Update Income
        public bool UpdateIncomeName(int incomeId, int userId, string name)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var income = dbnew.Incomes.Where(i => i.Id == incomeId && i.AccountId == userId).FirstOrDefault();

                if (income != null)
                {
                    income.Name = name;

                    dbnew.Incomes.Update(income);
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

        public bool UpdateIncomeInterval(int incomeId, int userId, int interval)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var income = dbnew.Incomes.Where(i => i.Id == incomeId && i.AccountId == userId).FirstOrDefault();

                if (income != null)
                {
                    income.Interval = interval;

                    dbnew.Incomes.Update(income);
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

        public bool UpdateIncomeAmount(int incomeId, int userId, decimal Amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var income = dbnew.Incomes.Where(i => i.Id == incomeId && i.AccountId == userId).FirstOrDefault();

                if (income != null)
                {
                    income.Amount = Amount;

                    dbnew.Incomes.Update(income);
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

        public bool UpdateIncomeRecurring(int incomeId, int userId, bool recurring)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var income = dbnew.Incomes.Where(i => i.Id == incomeId && i.AccountId == userId).FirstOrDefault();
                if (income != null)
                {
                    income.Recurring = recurring;

                    dbnew.Incomes.Update(income);
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

        #endregion Update Income

        #region Update Expense
        public bool UpdateExpenseName(int expenseId, int userId, string name)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var expense = dbnew.Expenses.Where(i => i.Id == expenseId && i.AccountId == userId).FirstOrDefault();

                if (expense != null)
                {
                    expense.Name = name;

                    dbnew.Expenses.Update(expense);
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

        public bool UpdateExpenseInterval(int expenseId, int userId, int interval)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var expense = dbnew.Expenses.Where(i => i.Id == expenseId && i.AccountId == userId).FirstOrDefault();

                if (expense != null)
                {
                    expense.Interval = interval;

                    dbnew.Expenses.Update(expense);
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

        public bool UpdateExpenseAmount(int expenseId, int userId, decimal Amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var expense = dbnew.Expenses.Where(i => i.Id == expenseId && i.AccountId == userId).FirstOrDefault();

                if (expense != null)
                {
                    expense.Amount = Amount;

                    dbnew.Expenses.Update(expense);
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

        public bool UpdateExpenseRecurring(int expenseId, int userId, bool recurring)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var expense = dbnew.Expenses.Where(i => i.Id == expenseId && i.AccountId == userId).FirstOrDefault();
                if (expense != null)
                {
                    expense.Recurring = recurring;

                    dbnew.Expenses.Update(expense);
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

        #endregion Update Expense

        #region Update Saving
        public bool UpdateSavingName(int savingId, int userId, string name)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var saving = dbnew.Savings.Where(i => i.Id == savingId && i.AccountId == userId).FirstOrDefault();

                if (saving != null)
                {
                    saving.Name = name;

                    dbnew.Savings.Update(saving);
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

        public bool UpdateSavingInterval(int savingId, int userId, int interval)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var saving = dbnew.Savings.Where(i => i.Id == savingId && i.AccountId == userId).FirstOrDefault();

                if (saving != null)
                {
                    saving.Interval = interval;

                    dbnew.Savings.Update(saving);
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

        public bool UpdateSavingAmount(int savingId, int userId, decimal Amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var saving = dbnew.Savings.Where(i => i.Id == savingId && i.AccountId == userId).FirstOrDefault();

                if (saving != null)
                {
                    saving.Amount = Amount;

                    dbnew.Savings.Update(saving);
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

        public bool UpdateSavingRecurring(int savingId, int userId, bool recurring)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var saving = dbnew.Savings.Where(i => i.Id == savingId && i.AccountId == userId).FirstOrDefault();
                if (saving != null)
                {
                    saving.Recurring = recurring;

                    dbnew.Savings.Update(saving);
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
        #endregion Update Saving

        #region Update Goal
        public bool UpdateGoalName(int goalId, int userId, string name)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.Name = name;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalInterval(int goalId, int userId, int interval)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(g => g.Id == goalId && g.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.Interval = interval;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalGoalAmount(int goalId, int userId, decimal goalMount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.GoalAmount = goalMount;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalAmount(int goalId, int userId, decimal amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.Amount = amount;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalMonthsToGoal(int goalId, int userId, int months)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.MonthsToGoal = months;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalCurrentTime(int goalId, int userId, DateTime time)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.CurrentTime = time;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalSaveToDateBool(int goalId, int userId, bool saveToDate)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.SaveToDate = saveToDate;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalSaveEachMonth(int goalId, int userId, decimal amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();
                if (goal != null)
                {
                    goal.SaveEachMonth = amount;

                    dbnew.Goals.Update(goal);
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

        public bool UpdateGoalSavedSoFar(int goalId, int userId, decimal amount)
        {
            BudgetCalcDbContext dbnew = new BudgetCalcDbContext();
            try
            {
                var goal = dbnew.Goals.Where(i => i.Id == goalId && i.AccountId == userId).FirstOrDefault();

                if (goal != null)
                {
                    goal.AmountSavedSoFar = amount;

                    dbnew.Goals.Update(goal);
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

        //TODO create lists of economic objects

        #endregion Create

        #region Refactored code
        public Account CheckIfUserExist(int userId)
        {
            return db.Accounts.FirstOrDefault(u => u.Id == userId);
        }

        private static bool LogB(Exception e)
        {
            ErrorLogger.Add(e.Message);
            return false;
        }

        public bool CheckIfUserExists(int userId)
        {
            try
            {
                var user = db.Accounts.Where(u => u.Id.Equals(userId)).FirstOrDefault();
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