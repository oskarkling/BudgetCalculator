using System;
using System.Collections.Generic;

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