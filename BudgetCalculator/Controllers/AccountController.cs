using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetCalculator
{
    public class AccountController
    {
        public Account CurrentAccount { get; private set; }
        public bool AccountLoggedIn { get; set; }
        public EconomicController ecoController { get; set; }

        

        private DatabaseConnection dbConnect = new DatabaseConnection();
        private EconomicController eco = new EconomicController();


        #region Get

        #region Get Account

        /// <summary>
        /// NullorEmpty validation of username and password variables.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public bool Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (CheckUsernameAndPassword(username, password))
                {
                    return FetchEconomicObjectesbyID();
                }
            }

            return false;
        }

        private bool FetchEconomicObjectesbyID()
        {
            try
            {
                CurrentAccount.Savings = dbConnect.GetAllSavings(CurrentAccount.Id);
                CurrentAccount.Incomes = dbConnect.GetIncomesOfUserId(CurrentAccount.Id);
                CurrentAccount.Expenses = dbConnect.GetExpensesOfUserId(CurrentAccount.Id);
                CurrentAccount.Goals = dbConnect.GetGoalsOfUserId(CurrentAccount.Id);
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// NullorEmpty validation of username and password variables.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public bool Register(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (CheckUser(username) && CheckPassword(password))
                {

                    return CheckRegister(username, password);
                }
            }
            return false;
        }

        /// <summary>
        /// Logs out user if account is currently logged in when called.
        /// </summary>
        public void Logout()
        {
            if (AccountLoggedIn)
            {
                AccountLoggedIn = false;
            }
        }

        //private bool DeleteAccount()
        //{
        //    if (CurrentAccount != null)
        //    {
        //        return dbConnect.DeleteAccountById(CurrentAccount.Id);
        //    }

        //    return false;
        //}

        /// <summary>
        /// Checks if username meets requirements by containing a letter and no whitespace.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool</returns>
        private bool CheckUser(string username)
        {
            if (username.Any(char.IsLetter) && !username.Any(char.IsWhiteSpace))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if password meets requirements by containing a letter or digit with no whitespace.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        private bool CheckPassword(string password)
        {
            if(password.Any(char.IsLetter) && !password.Any(char.IsWhiteSpace) || password.Any(char.IsDigit) && !password.Any(char.IsWhiteSpace))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Calling databasefunction for registration success or fail.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        private bool CheckRegister(string username, string password)
        {
            return dbConnect.RegisterAccount(username, password);
        }

        /// <summary>
        /// Calling databasefunction to check if account already exists
        /// if so account gets returned from database
        /// if so logged in status is updated
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        private bool CheckUsernameAndPassword(string username, string password)
        {
            Account account = dbConnect.GetAccountByUsernameAndPassword(username, password);
            if (account != null)
            {
                CurrentAccount = account;
                AccountLoggedIn = true;
                return true;
            }

            return false;
        }
        #endregion Get Account

        #region Get Income

        #region Get Expense
        #region Get Saving
        #region Get Goal

        #endregion Get Goal
        #endregion Get Saving
        #endregion Get Expense
        #endregion Get Income

        #endregion Get

        /// <summary>
        /// Creates an economic object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if successful, false if not</returns>
        public bool CreateAnEconomicObject(EconomicObject obj)
        {
            if (AccountLoggedIn && CurrentAccount != null)
            {
                if(obj != null && obj.AccountId != 0)
                {
                    if(CurrentAccount.Id == obj.AccountId)
                    {
                        var success = eco.AddEconomicObject(obj);
                        if (success)
                        {
                            return AddEcoObjectToAccountList(obj);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Adds the economic object to the correct Account list
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true if successful, false if not</returns>
        public bool AddEcoObjectToAccountList(EconomicObject obj)
        {
            if(obj != null && obj.AccountId == CurrentAccount.Id)
            {
                if (obj is Income)
                {
                    CurrentAccount.Incomes.Add((Income)obj);
                }
                if (obj is Expense)
                {
                    CurrentAccount.Expenses.Add((Expense)obj);
                }
                if (obj is Saving)
                {
                    CurrentAccount.Savings.Add((Saving)obj);
                }
                if (obj is Goal)
                {
                    CurrentAccount.Goals.Add((Goal)obj);
                }

                return true;
            }
            return false;
        }

        //public bool UpdateObject(EconomicObject ecoObj)
        //{
        //    if (AccountLoggedIn && CurrentAccount != null)
        //    {
        //        if (ecoObj != null)
        //        {
        //            if (ecoObj is Expense)
        //            {
                        
        //            }
        //            if (ecoObj is Income)
        //            {
                        
        //            }
        //            if (ecoObj is Goal)
        //            {
                        
        //            }
        //            if (ecoObj is Saving)
        //            {
                        
        //            }
        //        }
        //    }
        //    return false;
        //}


        /// <summary>
        /// Deletes an EconomicObject from the Account list
        /// </summary>
        /// <param name="ecoObj"></param>
        /// <returns>true if successful, false if not</returns>
        public bool DeleteObject(EconomicObject ecoObj)
        {
            if (AccountLoggedIn && CurrentAccount != null)
            {
                if (ecoObj != null)
                {
                    if (ecoObj is Expense)
                    {
                        int tempId = ecoObj.Id;
                        var obj = CurrentAccount.Expenses.FirstOrDefault(e => e.Id == ecoObj.Id);
                        CurrentAccount.Expenses.Remove(obj);
                        return dbConnect.DeleteExpenseById(CurrentAccount.Id, tempId);
                    }
                    if (ecoObj is Income)
                    {
                        int tempId = ecoObj.Id;
                        var obj = CurrentAccount.Incomes.FirstOrDefault(e => e.Id == ecoObj.Id);
                        CurrentAccount.Incomes.Remove(obj);
                        return dbConnect.DeleteIncomeById(CurrentAccount.Id, tempId);
                    }
                    if (ecoObj is Goal)
                    {
                        int tempId = ecoObj.Id;
                        var obj = CurrentAccount.Goals.FirstOrDefault(e => e.Id == ecoObj.Id);
                        CurrentAccount.Goals.Remove(obj);
                        return dbConnect.DeleteGoalById(CurrentAccount.Id, tempId);
                    }
                    if (ecoObj is Saving)
                    {
                        int tempId = ecoObj.Id;
                        var obj = CurrentAccount.Savings.FirstOrDefault(e => e.Id == ecoObj.Id);
                        CurrentAccount.Savings.Remove(obj);
                        return dbConnect.DeleteSavingById(CurrentAccount.Id, tempId);
                    }
                }
            }
            return false;
        }

        public void GetBalance()
        {
            var res = eco.CalculateBalance(GetTotalIncome(), GetTotalSavings(), GetTotalExpense(), GetGoals());
        }
        /// <summary>
        /// Returns decimal value of all incomes in account income list
        /// </summary>
        /// <returns>Decimal</returns>
        public decimal GetTotalIncome()
        {
            var income = CurrentAccount.Incomes.Where(i => i.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalIncome(income);
        }
        public decimal GetTotalSavings()
        {
            var savings = CurrentAccount.Savings.Where(i => i.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalSavings(savings);
        }
        public decimal GetTotalExpense()
        {
            var expenses = CurrentAccount.Expenses.Where(i => i.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalExpenses(expenses);
        }

        //goals section
        public void GetGoal()
        {

        }
        public decimal GetGoals()
        {
            var goals = CurrentAccount.Goals.Where(g => g.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalGoals(goals);
        }
    }
}