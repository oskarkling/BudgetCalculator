using BudgetCalculator;

namespace WpfApp1
{
    public static class BackendManager
    {
        static BackendManager()
        {
            accountController = new AccountController();
        }
        public static AccountController accountController;
    }
}