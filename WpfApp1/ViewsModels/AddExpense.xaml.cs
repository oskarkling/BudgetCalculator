using BudgetCalculator;
using System;
using System.Windows;
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
            UpdateUI();
        }
        /// <summary>
        /// Loops through and print all expenses that an account has.
        /// </summary>
        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Expenses)
            {
                expenseListbox.Items.Add($"{item.Name} | {item.Amount}");
            }
        }

        /// <summary>
        /// Takes the input from fields and creates a expense object if input is valid. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            var expenseNameInput = expenseName.Text;
            var timespanInput = expenseTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(expenseAmount.Text, out decimal amount);

            bool recurring = true;
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if (!parseSuccessfull && !Validator.GetAddExpenseValidation() || amount < 0)
            {
                MessageBox.Show("Wrong input");
            }
            else
            {
                Expense expense = new()
                {
                    Name = expenseNameInput,
                    Interval = timespanInput,
                    Amount = amount,
                    Recurring = recurring,
                    CreationTime = DateTime.Now,
                    AccountId = loggedInAccount.Id
                };

                if (BackendManager.accountController.CreateAnEconomicObject(expense))
                {
                    MessageBox.Show("EXPENSE ADDED");
                    AddItemToListBox(expense);
                }
                else
                {
                    MessageBox.Show("Could not add ");
                }
            }
        }

        /// <summary>
        /// gets the chosen object via SelectedIndex from the listbox, and sends it to UpdateEconomicObject view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateExpense_Click(object sender, RoutedEventArgs e)
        {
            var expenseIndex = expenseListbox.SelectedIndex;
            if (expenseIndex == -1)
            {
                MessageBox.Show("Choose an item");
            }
            else
            {
                var selectedExpense = BackendManager.accountController.CurrentAccount.Expenses[expenseIndex];

                if (selectedExpense != null)
                {
                    UpdateEconomicObject update = new UpdateEconomicObject(selectedExpense);
                    update.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }

        /// <summary>
        /// gets the chosen object via SelectedIndex from the listbox, and sends it to DeleteEconomicObject view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            var expenseIndex = expenseListbox.SelectedIndex;

            if (expenseIndex == -1)
            {
                MessageBox.Show("Select an item");
            }
            else
            {
                var selectedExpense = BackendManager.accountController.CurrentAccount.Expenses[expenseIndex];
                if (selectedExpense != null)
                {
                    DeleteEconomicObject delete = new(selectedExpense);
                    delete.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }

        /// <summary>
        /// Adds economic object to ListBox
        /// </summary>
        /// <param name="ecoObject"></param>
        private void AddItemToListBox(EconomicObject ecoObject)
        {
            expenseListbox.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");
        }

        /// <summary>
        /// Closes this window and opens up mainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }

        /// <summary>
        /// Convert chosen index from timespan and converts it to valid numbers. Also sets reccuring to false if not reccuring payment.
        /// </summary>
        /// <param name="timespanInput"></param>
        /// <param name="recurring"></param>
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
