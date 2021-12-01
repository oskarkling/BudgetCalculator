using System;
namespace BudgetCalculator
{
    public class Account
    {
        public EconomicController ecoController { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}