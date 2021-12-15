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
            
        }
        private void openExpenseViewBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpense expenseView = new AddExpense();
            expenseView.Show();
            this.Close();
        }
        private void openIncomeViewBtn_Click(object sender, RoutedEventArgs e)
        {
            AddIncome incomeView = new AddIncome();
            incomeView.Show();
            this.Close();
        }
        private void openSavingViewBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSaving savingView = new AddSaving();
            savingView.Show();
            this.Close();
        }
        private void openAddGoalViewBtn_Click(object sender, RoutedEventArgs e)
        {
            AddGoal addGoal = new AddGoal();
            addGoal.Show();
            this.Close();
        }
        private void Incomes_Click(object sender, RoutedEventArgs e)
        {
            AddIncome income = new AddIncome();
            income.Show();
            this.Close();
        }
        private void Expenses_Click(object sender, RoutedEventArgs e)
        {
            AddExpense expense = new AddExpense();
            expense.Show();
            this.Close();
        }
        private void Savings_Click(object sender, RoutedEventArgs e)
        {
            AddSaving saving = new AddSaving();
            saving.Show();
            this.Close();
        }
        private void Goals_Click(object sender, RoutedEventArgs e)
        {
            AddGoal goal = new AddGoal();
            goal.Show();
            this.Close();
        }
        private void GreetUser()
        {
            greetUser.Text = $"Welcome {BackendManager.accountController.CurrentAccount.Username}";
        }
    }
}
