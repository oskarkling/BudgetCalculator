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

        public void Login(string username, string password)
        {
            if (username != string.Empty && password != string.Empty)
            {
                if(checkUsernameAndPassword(username, password))
                {
                    //print successmessage to UI
                }
                else
                {
                    //print errormessage to UI
                }
            }
        }

        public void Logout(Account account)
        {
            if (AccountLoggedIn)
            {
                AccountLoggedIn = false;
            }
        }

        public void CreateAnExpense(Account account, bool loggedIn, string expenseName, decimal amount, int interval, bool recurring)
        {
            if (loggedIn && account != null)
            {
                if(expenseName != string.Empty && amount != 0)
                {
                    listOfExpenses = new()
                    {
                        new Expense {Name = expenseName, Amount = amount, Recurring = recurring, Interval = interval, Account = account, AccountId = account.Id, CreationTime = DateTime.Now}
                    };
                    //Skicka vidare expense till Crud
                    //Få respons och printa success message til UI
                }
            }
            else
            {
                //Print error message to UI
            }
        }

        public void UpdateExpense(Expense expense, Account account, bool loggedIn, string expenseName, decimal amount, int interval, bool recurring)
        {
            if (loggedIn && account != null)
            {
                if (expense != null)
                {
                    expense.Name = expenseName;
                    expense.Amount = amount;
                    expense.Interval = interval;
                    expense.Recurring = recurring;

                    //Prata med crud för uppdatering av expense objekt
                }
            }
                
        }

        public void DeleteExpense(Expense expense, Account account, bool loggedIn)
        {
            if(loggedIn && account != null)
            {
                //prata med crud on expense.Id existerar
                if(expense != null)
                {
                    int tempId = expense.Id;
                    var obj = listOfExpenses.FirstOrDefault(e => e.Id == expense.Id);
                    listOfExpenses.Remove(obj);
                    //pratar med crud för att radera ur databas, skicka in tempId
                }
            }
        }


        private bool checkUsernameAndPassword(string username, string password)
        {
            //try catch
            //skicka vidare information till CRUD
            //Får svar kontrollerar nullcheck och sätter respons till CurrentAccount,
            //sätt AccountLoggedIn = true, retunera AccountLoggedIn
            return false;
        }


    }
}