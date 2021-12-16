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
                    return FetchEconomicObjectesbyID(DateTime.Now);
                }
            }

            return false;
        }

        /// <summary>
        /// Fetches the economicObjects lists from database depending on Account id
        /// </summary>
        /// <returns>true if successful, false if not</returns>
        private bool FetchEconomicObjectesbyID(DateTime currentTime)
        {
            try
            {
                if (ClearAllAccountLists())
                {
                    CurrentAccount.Savings = new List<Saving>();
                    var listOfSavings = dbConnect.GetAllSavings(CurrentAccount.Id);
                    if (listOfSavings != null)
                    {
                        foreach (var item in listOfSavings)
                        {
                            if (item.Recurring)
                            {
                                if (eco.ReoccuringPayment(item, currentTime))
                                {
                                    CurrentAccount.Savings.Add(item);
                                }
                            }
                            else if (!item.Recurring && item.CreationTime.Month == currentTime.Month)
                            {
                                CurrentAccount.Savings.Add(item);
                            }
                        }
                    }

                    CurrentAccount.Incomes = new List<Income>();
                    var listOfIncomes = dbConnect.GetIncomesOfUserId(CurrentAccount.Id);
                    if (listOfIncomes != null)
                    {
                        foreach (var item in listOfIncomes)
                        {
                            if (item.Recurring)
                            {
                                if (eco.ReoccuringPayment(item, currentTime))
                                {
                                    CurrentAccount.Incomes.Add(item);
                                }
                            }
                            else if (!item.Recurring && item.CreationTime.Month == currentTime.Month)
                            {
                                CurrentAccount.Incomes.Add(item);
                            }
                        }
                    }

                    CurrentAccount.Expenses = new List<Expense>();
                    List<Expense> listOfExpenses = dbConnect.GetExpensesOfUserId(CurrentAccount.Id);
                    if (listOfExpenses != null)
                    {
                        foreach (var item in listOfExpenses)
                        {
                            if (item.Recurring)
                            {
                                if (eco.ReoccuringPayment(item, currentTime))
                                {
                                    CurrentAccount.Expenses.Add(item);
                                }
                            }
                            else if (!item.Recurring && item.CreationTime.Month == currentTime.Month)
                            {
                                CurrentAccount.Expenses.Add(item);
                            }
                        }
                    }

                    CurrentAccount.Goals = new List<Goal>();
                    List<Goal> listOfGoals = dbConnect.GetGoalsOfUserId(CurrentAccount.Id);
                    if (listOfGoals != null)
                    {
                        foreach (var item in listOfGoals)
                        {
                            if (item.SaveEachMonth != 0)
                            {
                                DateTime endDate = GetGoalEndDate(item);
                                if (endDate.Month <= currentTime.Month)
                                {
                                    CurrentAccount.Goals.Add(item);
                                }
                            }
                            else if (item.SaveToDate && item.MonthsToGoal != 0 && item.CreationTime.Month == currentTime.Month)
                            {
                                CurrentAccount.Goals.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Add(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Method to clear all lists connected to the current account logged in
        /// </summary>
        /// <returns>bool true if successful, false if not</returns>
        public bool ClearAllAccountLists()
        {
            try
            {
                if (CurrentAccount.Incomes != null)
                {
                    CurrentAccount.Incomes.Clear();
                }
                if (CurrentAccount.Expenses != null)
                {
                    CurrentAccount.Expenses.Clear();
                }
                if (CurrentAccount.Savings != null)
                {
                    CurrentAccount.Savings.Clear();
                }
                if (CurrentAccount.Goals != null)
                {
                    CurrentAccount.Goals.Clear();
                }
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
        public bool Logout()
        {
            if (AccountLoggedIn)
            {
                AccountLoggedIn = false;
                return ClearAllAccountLists();
            }

            return false;
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
            if (password.Any(char.IsLetter) && !password.Any(char.IsWhiteSpace) || password.Any(char.IsDigit) && !password.Any(char.IsWhiteSpace))
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
                if (obj != null && obj.AccountId != 0)
                {
                    if (CurrentAccount.Id == obj.AccountId)
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
            if (obj != null && obj.AccountId == CurrentAccount.Id)
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

        /// <summary>
        /// Updates an economic object
        /// </summary>
        /// <param name="ecoObj"></param>
        /// <returns>true if successful, false if not</returns>
        public bool UpdateObject(EconomicObject ecoObj)
        {
            if (AccountLoggedIn && CurrentAccount != null)
            {
                if (ecoObj != null)
                {
                    if (ecoObj is Expense)
                    {
                        if (dbConnect.UpdateEco(ecoObj))
                        {
                            var obj = CurrentAccount.Expenses.FirstOrDefault(e => e.Id == ecoObj.Id);
                            int index = CurrentAccount.Expenses.FindIndex(i => i.Id == ecoObj.Id);
                            if (obj != null && index >= 0)
                            {
                                CurrentAccount.Expenses[index] = (Expense)ecoObj;
                                return true;
                            }
                        }
                    }
                    if (ecoObj is Income)
                    {
                        if (dbConnect.UpdateEco(ecoObj))
                        {
                            var obj = CurrentAccount.Incomes.FirstOrDefault(e => e.Id == ecoObj.Id);
                            int index = CurrentAccount.Incomes.FindIndex(i => i.Id == ecoObj.Id);
                            if (obj != null && index >= 0)
                            {
                                CurrentAccount.Incomes[index] = (Income)ecoObj;
                                return true;
                            }
                        }
                    }
                    if (ecoObj is Goal)
                    {
                        if (dbConnect.UpdateEco(ecoObj))
                        {
                            var obj = CurrentAccount.Goals.FirstOrDefault(e => e.Id == ecoObj.Id);
                            int index = CurrentAccount.Goals.FindIndex(i => i.Id == ecoObj.Id);
                            if (obj != null && index >= 0)
                            {
                                CurrentAccount.Goals[index] = (Goal)ecoObj;
                                return true;
                            }
                        }
                    }
                    if (ecoObj is Saving)
                    {
                        if (dbConnect.UpdateEco(ecoObj))
                        {
                            var obj = CurrentAccount.Savings.FirstOrDefault(e => e.Id == ecoObj.Id);
                            int index = CurrentAccount.Savings.FindIndex(i => i.Id == ecoObj.Id);
                            if (obj != null && index >= 0)
                            {
                                CurrentAccount.Savings[index] = (Saving)ecoObj;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }


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

        /// <summary>
        /// Get balance remaining for the user
        /// </summary>
        /// <returns>decimal</returns>
        public decimal GetBalance()
        {
            return eco.CalculateBalance(GetTotalIncome(), GetTotalSavings(), GetTotalExpense(), GetTotalGoals());
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

        /// <summary>
        /// Returns decimal value of all savings in account savings list
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalSavings()
        {
            var savings = CurrentAccount.Savings.Where(i => i.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalSavings(savings);
        }

        /// <summary>
        /// Returns decial value of all expenses in account expenses list
        /// </summary>
        /// <returns>decimal</returns>
        public decimal GetTotalExpense()
        {
            var expenses = CurrentAccount.Expenses.Where(i => i.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalExpenses(expenses);
        }

        /// <summary>
        /// Gets the calculated end date if prop SaveEachMonth is prefilled
        /// </summary>
        /// <param name="goal"></param>
        /// <returns>the end date for reaching goal</returns>
        public DateTime GetGoalEndDate(Goal goal)
        {
            if (goal.SaveEachMonth != 0)
            {
                return eco.CalculateEndDate(goal);
            }
            return new DateTime();
        }

        /// <summary>
        /// Gets the calculated amount needed to save each month to reach goal
        /// </summary>
        /// <param name="goal"></param>
        /// <returns>decimal, the amound needed per month</returns>
        public decimal GetGoalAmountNeededEachMonth(Goal goal)
        {
            if (goal.SaveToDate && goal.MonthsToGoal != 0)
            {
                return eco.CalculateAmountToSave(goal);
            }

            return 0;
        }

        /// <summary>
        /// Returns decimal value of all goals in account goals list
        /// </summary>
        /// <returns>decimal</returns>
        public decimal GetTotalGoals()
        {
            var goals = CurrentAccount.Goals.Where(g => g.AccountId == CurrentAccount.Id).ToList();
            return eco.CalculateTotalGoals(goals);
        }
        public List<EconomicObject> MoveBackward(DateTime current)
        {
            List<EconomicObject> listOfEconomicObjects = new List<EconomicObject>();

            List<Income> listOfIncomes = dbConnect.GetIncomesOfUserId(CurrentAccount.Id);
            listOfEconomicObjects.AddRange(listOfIncomes);
            List<Saving> listOfSavings = dbConnect.GetSavingsOfUserId(CurrentAccount.Id);
            listOfEconomicObjects.AddRange(listOfSavings);
            List<Expense> listOfExpenses = dbConnect.GetExpensesOfUserId(CurrentAccount.Id);
            listOfEconomicObjects.AddRange(listOfExpenses);
            List<Goal> listOfGoals = dbConnect.GetGoalsOfUserId(CurrentAccount.Id);
            listOfEconomicObjects.AddRange(listOfGoals);

            if (listOfEconomicObjects.Count != 0)
            {
                foreach (var item in listOfEconomicObjects)
                {
                    if (item.CreationTime.Month != current.Month && item.CreationTime.Year != current.Year)
                    {
                        listOfEconomicObjects.Remove(item);
                    }
                }
            }

            return listOfEconomicObjects;
        }
        public List<EconomicObject> MoveForward(DateTime current)
        {
            List<EconomicObject> listOfEconomicObjects = new List<EconomicObject>();

            List<Income> listOfIncomes = dbConnect.GetIncomesOfUserId(CurrentAccount.Id);
            List<Saving> listOfSavings = dbConnect.GetSavingsOfUserId(CurrentAccount.Id);
            List<Expense> listOfExpenses = dbConnect.GetExpensesOfUserId(CurrentAccount.Id);
            List<Goal> listOfGoals = dbConnect.GetGoalsOfUserId(CurrentAccount.Id);

            if (listOfIncomes.Count != 0)
            {
                foreach (var item in listOfIncomes)
                {
                    if (item.CreationTime.Month == current.Month)
                    {
                        listOfEconomicObjects.Add(item);
                    }
                    if (item.Recurring && eco.ReoccuringPayment(item, current))
                    {
                        listOfEconomicObjects.Add(item);
                    }
                }
            }
            if (listOfSavings.Count != 0)
            {
                foreach (var item in listOfSavings)
                {
                    if (item.CreationTime.Month == current.Month)
                    {
                        listOfEconomicObjects.Add(item);
                    }
                    if (item.Recurring && eco.ReoccuringPayment(item, current))
                    {
                        listOfEconomicObjects.Add(item);
                    }
                }
            }
            if (listOfExpenses.Count != 0)
            {
                foreach (var item in listOfExpenses)
                {
                    if (item.CreationTime.Month == current.Month)
                    {
                        listOfEconomicObjects.Add(item);
                    }
                    if (item.Recurring && eco.ReoccuringPayment(item, current))
                    {
                        listOfEconomicObjects.Add(item);
                    }
                }
            }
            if (listOfGoals.Count != 0)
            {

                foreach (var item in listOfGoals)
                {
                    if (item.SaveEachMonth != 0)
                    {
                        DateTime endDate = GetGoalEndDate(item);
                        if (endDate.Month <= current.Month)
                        {
                            listOfEconomicObjects.Add(item);
                        }
                    }
                    else if (item.SaveToDate && item.MonthsToGoal != 0 && item.CreationTime.Month == current.Month)
                    {
                        listOfEconomicObjects.Add(item);
                    }
                }

            }

            return listOfEconomicObjects;
        }
    }
}