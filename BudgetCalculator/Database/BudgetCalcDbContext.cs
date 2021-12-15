using Microsoft.EntityFrameworkCore;


namespace BudgetCalculator
{
    public class BudgetCalcDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Saving> Savings { get; set; }

        public static string ConnectionString()
        {
            return "workstation id=budgetdb.mssql.somee.com;packet size=4096;user id=budgetadmin;pwd=budget123;data source=budgetdb.mssql.somee.com;persist security info=False;initial catalog=budgetdb";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(BudgetCalcDbContext.ConnectionString());
        }
    }
}
