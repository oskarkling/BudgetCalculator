using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetCalculator
{
    public static class Mockdata
    {
        public static bool MockDatabase()
        {
            try
            {
                using (var db = new BudgetCalcDbContext())
                {
                    db.Accounts.Add(new Account
                    {
                        Username = "Steven",
                        Password = "password",
                        Expenses = new List<Expense>()
                    { new Expense
                        {
                        Name = "hyra",
                        Amount = 6006,
                        Recurring = true,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        }
                    },
                        Incomes = new List<Income>()
                    { new Income
                        {
                        Name = "lön",
                        Amount = 120000,
                        Recurring = true,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        }
                    },
                        Savings = new List<Saving>()
                    { new Saving
                        {
                        Name = "pension",
                        Amount = 99,
                        Recurring = true,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        }
                    },
                        Goals = new List<Goal>()
                    { new Goal
                        {
                        Name = "HELIKOPTER - rosa",
                        Amount = 90000,
                        CreationTime = DateTime.Now,
                        Interval = 1,
                        AmountSavedSoFar = 0,
                        SaveToDate = false,
                        }
                    },
                    });


                    db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
