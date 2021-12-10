
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

        public List<Expense> listOfExpenses = new();
        public List<Income> listOfIncomes = new();
        public List<Saving> listOfSavings = new();
        public List<Goal> listOfGoals = new();

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
                    return FetchEcomicObjectesbyID();
                }
            }

            return false;
        }

        private bool FetchEcomicObjectesbyID()
        {
            try
            {
                CurrentAccount.Savings = (List<Saving>)dbConnect.GetAllSavings(CurrentAccount.Id);
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

        private bool DeleteAccount()
        {
            if (CurrentAccount != null)
            {
                return dbConnect.DeleteAccountById(CurrentAccount.Id);
            }

            return false;
        }

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
                if(obj != null)
                {
                    eco.AddEconomicObject(obj);
                    return true;
                }
            }
            return false;
        }

        public void UpdateObject(EconomicObject ecoObj, bool loggedIn, string expenseName = "", decimal amount = 0, int interval = 0, bool recurring = false, decimal goalAmount = 0, decimal amountSavedSoFar = 0, int monthsToGoal = 0, bool savedToDate = false, decimal saveEachMonth = 0)
        {
            if (loggedIn && CurrentAccount != null)
            {
                if (ecoObj != null)
                {
                    if (ecoObj is Expense)
                    {
                        ecoObj.Name = expenseName;
                        ecoObj.Amount = amount;
                        ecoObj.Interval = interval;
                        ((Expense)ecoObj).Recurring = recurring;
                    }
                    if (ecoObj is Income)
                    {
                        ecoObj.Name = expenseName;
                        ecoObj.Amount = amount;
                        ecoObj.Interval = interval;
                        ((Income)ecoObj).Recurring = recurring;
                    }
                    if (ecoObj is Goal)
                    {
                        ecoObj.Name = expenseName;
                        ecoObj.Amount = amount;
                        ecoObj.Interval = interval;
                        ((Goal)ecoObj).GoalAmount = goalAmount;
                        ((Goal)ecoObj).AmountSavedSoFar = amountSavedSoFar;
                        ((Goal)ecoObj).MonthsToGoal = monthsToGoal;
                        //((Goal)ecoObj).CurrentTime = n�t? annars nej.
                        ((Goal)ecoObj).SaveToDate = savedToDate;
                        ((Goal)ecoObj).SaveEachMonth = saveEachMonth;
                    }
                    if (ecoObj is Saving)
                    {
                        ecoObj.Name = expenseName;
                        ecoObj.Amount = amount;
                        ecoObj.Interval = interval;
                        ((Expense)ecoObj).Recurring = recurring;
                    }

                    //Prata med crud f�r uppdatering av expense objekt
                }
            }

        }


        public bool DeleteObject(EconomicObject ecoObj, bool loggedIn)
        {
            if (loggedIn && CurrentAccount != null)
            {
                //prata med crud on expense.Id existerar
                if (ecoObj != null)
                {
                    if (ecoObj is Expense)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfExpenses.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfExpenses.Remove(obj);
                        return dbConnect.DeleteExpenseById(CurrentAccount.Id, tempId);
                        //pratar med crud f�r att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Income)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfIncomes.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfIncomes.Remove(obj);
                        return dbConnect.DeleteIncomeById(CurrentAccount.Id, tempId);
                        //pratar med crud f�r att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Goal)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfGoals.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfGoals.Remove(obj);
                        return dbConnect.DeleteGoalById(CurrentAccount.Id, tempId);
                        //pratar med crud f�r att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Saving)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfSavings.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfSavings.Remove(obj);
                        return dbConnect.DeleteSavingById(CurrentAccount.Id, tempId);
                        //pratar med crud f�r att radera ur databas, skicka in tempId
                    }
                }
            }
            return false;
        }
    }
}