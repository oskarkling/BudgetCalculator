using System;
using System.Collections.Generic;
using BudgetCalculator;

namespace ConsoleAdminPanel
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var db = new DatabaseConnection();
                var ac = new AccountController();
                ac.Register("Admin", "Admin123");
                ac.Login("Admin", "Admin123");
                var admin = db.GetAccountByUsernameAndPassword("Admin", "Admin123");

                var accounts = db.GetAllAccountsAsAdmin(admin.Id);
                var incomes = db.GetAllIncomesAsAdmin(admin.Id);
                var expenses = db.GetAllExpensesAsAdmin(admin.Id);
                var savings = db.GetAllSavingsAsAdmin(admin.Id);
                var goals = db.GetAllGoalsAsAdmin(admin.Id);

                Console.WriteLine("---- Accounts ----");
                foreach (var item in accounts)
                {
                    Console.WriteLine("Username: " + item.Username + ". ID: " + item.Id);
                }
                Console.WriteLine();

                Console.WriteLine("---- Incomes ----");
                foreach (var item in incomes)
                {
                    Console.WriteLine("Name: " + item.Name + ". ID: " + item.Id + ". AccountID: " + item.AccountId);
                }
                Console.WriteLine();

                Console.WriteLine("---- Expenses ----");
                foreach (var item in expenses)
                {
                    Console.WriteLine("Name: " + item.Name + ". ID: " + item.Id + ". AccountID: " + item.AccountId);
                }
                Console.WriteLine();

                Console.WriteLine("---- Savings ----");
                foreach (var item in savings)
                {
                    Console.WriteLine("Name: " + item.Name + ". ID: " + item.Id + ". AccountID: " + item.AccountId);
                }
                Console.WriteLine();

                Console.WriteLine("---- Goals ----");
                foreach (var item in goals)
                {
                    Console.WriteLine("Name: " + item.Name + ". ID: " + item.Id + ". AccountID: " + item.AccountId);
                }
                Console.WriteLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);
            }
        }
    }
}
