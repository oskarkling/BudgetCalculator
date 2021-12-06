using BudgetCalculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;


namespace BudgetCalculator
{
    public class BudgetCalcDbContext : DbContext
    {

        //public readonly string dbConnStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Saving> Savings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("workstation id=budgetdb.mssql.somee.com;packet size=4096;user id=budgetadmin;pwd=budget123;data source=budgetdb.mssql.somee.com;persist security info=False;initial catalog=budgetdb");
        }
    }
}
