using System;
using BudgetCalculator;
using BudgetCalculatorTests1.Seeder;
using BudgetCalculator.Controllers;
using BudgetCalculator.Helpers;
using BudgetCalculator.Tests;

namespace TestConsoleEnviorment
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Account acc = new Account();
            System.Console.WriteLine("hello");

            AccountController accountController = new AccountController();

            var dbcon = new DatabaseConnection();
            acc = dbcon.GetAccountByUsernameAndPassword("Steven", "password");
            System.Console.WriteLine(acc.Id);

            if(dbcon.Login("Steven", "password", out acc))
            {
                System.Console.WriteLine("acc logged in");
            }
            else{
                System.Console.WriteLine("NOT LOGGED IN");
            }




            
        }
    }
}
