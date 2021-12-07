using BudgetCalculator.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetCalculator
{
    public class AccountController
    {
        public Account CurrentAccount { get; set; }
        public bool AccountLoggedIn { get; set; }
        public EconomicController ecoController { get; set; }

        public List<Expense> listOfExpenses = new();
        public List<Income> listOfIncomes = new();
        public List<Saving> listOfSavings = new();
        public List<Goal> listOfGoals = new();

        private DatabaseConnection dbConnect = new DatabaseConnection();

        public bool Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (CheckUsernameAndPassword(username, password))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Register(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
               return dbConnect.RegisterAccount(username, password);
            }
            return false;
        }

        public void Logout(Account account)
        {
            if (AccountLoggedIn)
            {
                AccountLoggedIn = false;
            }
        }

        public void CreateAnObject(bool loggedIn, string type, string expenseName = "", decimal amount = 0, int interval = 0, bool recurring = false, decimal goalAmount = 0, decimal amountSavedSoFar = 0, int monthsToGoal = 0, bool savedToDate = false, decimal saveEachMonth = 0)
        {
            if (loggedIn && CurrentAccount != null)
            {
                if (expenseName != string.Empty && amount != 0)
                {
                    if (type == "Expense")
                    {
                        listOfExpenses = new()
                        {
                            new Expense { Name = expenseName, Amount = amount, Recurring = recurring, Interval = interval, Account = CurrentAccount, AccountId = CurrentAccount.Id, CreationTime = DateTime.Now }
                        };
                    }
                    if (type == "Income")
                    {
                        listOfIncomes = new()
                        {
                            new Income { Name = expenseName, Amount = amount, Recurring = recurring, Interval = interval, Account = CurrentAccount, AccountId = CurrentAccount.Id, CreationTime = DateTime.Now }
                        };
                    }
                    if (type == "Goal")
                    {
                        listOfGoals = new()
                        {
                            new Goal { Name = expenseName, Amount = amount, Interval = interval, Account = CurrentAccount, AccountId = CurrentAccount.Id, CreationTime = DateTime.Now, GoalAmount = goalAmount, AmountSavedSoFar = amountSavedSoFar, MonthsToGoal = monthsToGoal, SaveToDate = savedToDate, SaveEachMonth = saveEachMonth, CurrentTime = DateTime.Now }
                        };
                    }
                    if (type == "Savings")
                    {
                        listOfSavings = new()
                        {
                            new Saving { Name = expenseName, Amount = amount, Recurring = recurring, Interval = interval, Account = CurrentAccount, AccountId = CurrentAccount.Id, CreationTime = DateTime.Now }
                        };
                    }
                    //Skicka vidare expense till Crud
                    //Få respons och printa success message til UI
                }
            }
            else
            {
                //Print error message to UI
            }
        }

        public void UpdateObject(EconomicObject ecoObj, Account account, bool loggedIn, string expenseName = "", decimal amount = 0, int interval = 0, bool recurring = false, decimal goalAmount = 0, decimal amountSavedSoFar = 0, int monthsToGoal = 0, bool savedToDate = false, decimal saveEachMonth = 0)
        {
            if (loggedIn && account != null)
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
                        ((Expense)ecoObj).Recurring = recurring;
                    }
                    if (ecoObj is Goal)
                    {
                        ecoObj.Name = expenseName;
                        ecoObj.Amount = amount;
                        ecoObj.Interval = interval;
                        ((Goal)ecoObj).GoalAmount = goalAmount;
                        ((Goal)ecoObj).AmountSavedSoFar = amountSavedSoFar;
                        ((Goal)ecoObj).MonthsToGoal = monthsToGoal;
                        //((Goal)ecoObj).CurrentTime = nåt? annars nej.
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

                    //Prata med crud för uppdatering av expense objekt
                }
            }

        }


        public void DeleteObject(EconomicObject ecoObj, bool loggedIn)
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
                        //pratar med crud för att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Income)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfIncomes.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfIncomes.Remove(obj);
                        //pratar med crud för att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Goal)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfGoals.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfGoals.Remove(obj);
                        //pratar med crud för att radera ur databas, skicka in tempId
                    }
                    if (ecoObj is Saving)
                    {
                        int tempId = ecoObj.Id;
                        var obj = listOfSavings.FirstOrDefault(e => e.Id == ecoObj.Id);
                        listOfSavings.Remove(obj);
                        //pratar med crud för att radera ur databas, skicka in tempId
                    }
                }
            }
        }


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
        #region Get

        #region Get Account
        #region Get Income
        #region Get Expense
        #region Get Saving
        #region Get Goal

        #endregion Get Goal
        #endregion Get Saving
        #endregion Get Expense
        #endregion Get Income
        #endregion Get Account

        #endregion Get
    }
}