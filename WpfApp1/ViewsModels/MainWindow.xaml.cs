using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.ApplicationModel.VoiceCommands;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GreetUser();
            PrintRemainingBalance();
            FillIncomeListbox();
            FillEExpenseListbox();
            FillSavingListbox();
            FillGoalListbox();

        }
        /// <summary>
        /// opens up the addExpense window and closes this current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            AddExpense expenseView = new AddExpense();
            expenseView.Show();
            this.Close();
        }
        /// <summary>
        /// opens up the addIncome window and closes this current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Incomes_Click(object sender, RoutedEventArgs e)
        {
            AddIncome incomeView = new AddIncome();
            incomeView.Show();
            this.Close();
        }
        /// <summary>
        /// opens up the saving window and closes this current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Savings_Click(object sender, RoutedEventArgs e)
        {
            AddSaving savingView = new AddSaving();
            savingView.Show();
            this.Close();
        }
        /// <summary>
        ///  opens up the add goal window and closes this current window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Goals_Click(object sender, RoutedEventArgs e)
        {
            AddGoal addGoal = new AddGoal();
            addGoal.Show();
            this.Close();
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            BackendManager.accountController = new();
            LoggInWindow login = new LoggInWindow();
            login.Show();
            this.Close();

        }
        private void GreetUser()
        {
            greetUser.Text = $"Welcome {BackendManager.accountController.CurrentAccount.Username}";
        }
        private void PrintRemainingBalance()
        {
            remainingBalance.Text = BackendManager.accountController.GetBalance().ToString();
        }
        private void FillIncomeListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Incomes)
            {
                listboxIncomes.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void FillEExpenseListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Expenses)
            {
                listboxExpenses.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void FillSavingListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Savings)
            {
                listboxSavings.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void FillGoalListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Goals)
            {
                listboxGoals.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
    }
}
