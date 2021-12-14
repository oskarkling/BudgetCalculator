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
    /// 

    public partial class AddExpense : Window
    {
    
        public AddExpense()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Expenses)
            {
                expenseListbox.Items.Add($"{item.Name} | {item.Amount}");
            }
        }

        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {

            var expenseNameInput = expenseName.Text;
            var timespanInput = expenseTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(expenseAmount.Text, out decimal amount);
            bool recurring = true;
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if (!parseSuccessfull && !Validator.GetAddExpenseValidation())
            {
                MessageBox.Show("PLease fill all forms!!");
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
                    AccountId = loggedInAccount.Id
                };
                
                if(BackendManager.accountController.CreateAnEconomicObject(expense))
                {
                    MessageBox.Show("EXPENSE ADDED");
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Could not add ");
                }



            }

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
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
                    Validator.SetAddExpenseValidation(false);
                    break;
            }
        }
    }

}
