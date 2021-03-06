using Microsoft.EntityFrameworkCore;
using System;

namespace BudgetCalculator
{
    public static class DbExtensions
    {
        /// <summary>
        /// Removes the table content if needed.
        /// </summary>
        /// <typeparam name="T">Set name.</typeparam>
        /// <param name="dbSet">DB-set.</param>
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

        public static bool EmptyAllTables()
        {
            try
            {
                using BudgetCalcDbContext db = new();

                db.Accounts.Clear();
                db.Expenses.Clear();
                db.Goals.Clear();
                db.Incomes.Clear();
                db.Savings.Clear();
                db.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                ErrorLogger.Add(ex.Message);
                return false;
            }
        }

        public static bool DeleteTables()
        {
            try
            {
                using BudgetCalcDbContext db = new();
                db.Accounts.FromSqlRaw("DROP TABLE [dbo].[Accounts]");
                db.Incomes.FromSqlRaw("DROP TABLE [dbo].[Incomes]");
                db.Expenses.FromSqlRaw("DROP TABLE [dbo].[Expenses]");
                db.Savings.FromSqlRaw("DROP TABLE [dbo].[Savings]");
                db.Goals.FromSqlRaw("DROP TABLE [dbo].[Goals]");
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.Add(ex.Message);
                return false;
            }
        }
    }
}
