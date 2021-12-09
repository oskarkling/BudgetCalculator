using BudgetCalculator;
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
using System.Windows.Shapes;
using WpfApp1.Utility.FrontendValidation;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AddExpense.xaml
    /// </summary>
    public partial class AddExpense : Window
    {
        public AddExpense()
        {
            InitializeComponent();
        }
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {

            var expenseNameInput = expenseName.Text;
            var timespanInput = timespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(expenseAmount.Text, out decimal amount);
            bool recurring = true;
            GetCurrentUser(out Account loggedInAccount);
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if (!parseSuccessfull && !Validator.GetAddExpenseValidaton())
            {
                MessageBox.Show("Must write number!!");
            }
            else
            {

                Expense expense = new Expense()
                {
                    Name = expenseNameInput,
                    Interval = timespanInput,
                    Amount = amount,
                    Recurring = recurring,
                    CreationTime = DateTime.Now,
                    Account = loggedInAccount,
                    AccountId = loggedInAccount.Id
                };
                MessageBox.Show($"{expense.Name} | {expense.Interval} | {expense.Amount} | {expense.Recurring} | {expense.CreationTime} | {expense.Account} | {expense.AccountId}");
            }

        }

        private void GetCurrentUser(out Account loggedInAccount)
        {
            loggedInAccount = BackendManager.accountController.CurrentAccount;
        }

        private static void ConvertTimeSpan(ref int timespanInput, ref bool recurring)
        {
            switch (timespanInput)
            {
                case 0:
                    timespanInput = 1;
                    break;
                case 1:
                    timespanInput = 0;
                    recurring = false;
                    break;
                case 2:
                    timespanInput = 2;
                    break;
                case 3:
                    timespanInput = 3;
                    break;
                case 4:
                    timespanInput = 6;
                    break;
                case 5:
                    timespanInput = 12;
                    break;

                default:
                    Validator.SetAddExpenseValidaton(false);
                    MessageBox.Show("Wrong input, choose timespan");
                    break;
            }
        }
    }

}
