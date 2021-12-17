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
            GetTotal();

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
        /// <summary>
        /// Goes to login view and reset accountController.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            BackendManager.accountController = new();
            LoggInWindow login = new LoggInWindow();
            login.Show();
            this.Close();

        }
        /// <summary>
        /// prints out the logged in users name
        /// </summary>
        private void GreetUser()
        {
            greetUser.Text = $"Welcome {BackendManager.accountController.CurrentAccount.Username}";
        }
        /// <summary>
        /// Prints out the remaining balance, text in red if negative value, green if not.
        /// </summary>
        private void PrintRemainingBalance()
        {
            var balance = BackendManager.accountController.GetBalance();
            if (balance < 0)
            {
                remainingBalance.Foreground = Brushes.Red;
            }
            remainingBalance.Text = balance.ToString();
        }
        /// <summary>
        /// Fills income list box with all incomes
        /// </summary>
        private void FillIncomeListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Incomes)
            {
                listboxIncomes.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        /// <summary>
        /// Fills expense list box with all expenses
        /// </summary>
        private void FillEExpenseListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Expenses)
            {
                listboxExpenses.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        /// <summary>
        /// Fills saving list box with all savings
        /// </summary>
        private void FillSavingListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Savings)
            {
                listboxSavings.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        /// <summary>
        /// Fills goal list box with all goals
        /// </summary>
        private void FillGoalListbox()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Goals)
            {
                listboxGoals.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        /// <summary>
        /// Prints total amount of each economic object to corresponding textblocks.
        /// </summary>
        private void GetTotal()
        {
            totalGoal.Text = $"Total Goal: {BackendManager.accountController.GetTotalGoals()}";
            totalSaving.Text = $"Total Saving: {BackendManager.accountController.GetTotalSavings()}";
            totalIncome.Text = $"Total Income: {BackendManager.accountController.GetTotalIncome()}";
            totalExpense.Text = $"Total Expense: {BackendManager.accountController.GetTotalExpense()}";
        }
    }
}
