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
            string type = "expense";
            var expenseNameInput = expenseName.Text;
            var timespanInput = timespan.SelectedIndex; // switch/ if
            var parseSuccessfull = decimal.TryParse(expenseAmount.Text, out decimal amount); // try parse
            bool recurring = true;
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if(!parseSuccessfull)
            {
                MessageBox.Show("Must write number!!");
            }






            //Expense expense = new Expense();
            //expense.Name = expenseNameInput;
            //expense.Interval = timespanInput;
            //expense.Amount = int.Parse(expenseAmountInput);
            //expense.Recurring = recurring;



            MessageBox.Show($"{expenseNameInput} ||| {timespanInput} ||| {amount}");

        }

        private static void ConvertTimeSpan(ref int timespanInput, ref bool recurring)
        {
            switch (timespanInput)
            {
                case 0:
                    timespanInput = 1;
                    break;
                case 1:
                    timespanInput = 0; // kolla upp
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
                    MessageBox.Show("Wrong input, choose timespan");
                    break;
            }
        }
    }

}
