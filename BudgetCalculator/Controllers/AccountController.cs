using System;
namespace BudgetCalculator
{
    public class AccountController
    {
        public Account CurrentAccount { get; set; } 
        public bool AccountLoggedIn {get; set;}
        public EconomicController ecoController { get; set; }

        
    }
}