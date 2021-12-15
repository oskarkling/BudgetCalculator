using BudgetCalculator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddIncome : Window
    {
        public AddIncome()
        {
            InitializeComponent();
            UpdateUI();
        }
        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Incomes)
            {
                incomeListbox.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void AddItemToListBox(EconomicObject ecoObject)
        {
            incomeListbox.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");
        }

        private void AddIncomeBtn_Click(object sender, RoutedEventArgs e)
        {

            var incomeNameInput = incomeName.Text;
            var timespanInput = incomeTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(incomeAmount.Text, out decimal amount);
            bool recurring = true;
            GetCurrentUser(out Account loggedInAccount);
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if (!parseSuccessfull && !Validator.AddIncomeValidator)
            {
                MessageBox.Show("PLease fill all forms!!");
            }
            else
            {

                Income income = new Income()
                {
                    Name = incomeNameInput,
                    Interval = timespanInput,
                    Amount = amount,
                    Recurring = recurring,
                    CreationTime = DateTime.Now,
                    AccountId = loggedInAccount.Id
                };



                if (BackendManager.accountController.CreateAnEconomicObject(income))
                {
                    MessageBox.Show("INCOME ADDED");
                    AddItemToListBox(income);
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
        private void UpdateIncome_Click(object sender, RoutedEventArgs e)
        {
            var incomeId = incomeListbox.SelectedIndex;
            var selectedIncome = BackendManager.accountController.CurrentAccount.Incomes.FirstOrDefault(i => i.Id == incomeId);
            if(selectedIncome != null)
            {

            }
        }

        private void GetCurrentUser(out Account loggedInAccount)
        {
            loggedInAccount = BackendManager.accountController.CurrentAccount;
        }

        private static void ConvertTimeSpan(ref int timespanInput, ref bool recurring)
        {
            // släng in i helper class
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
                    Validator.AddIncomeValidator = false;
                    break;
            }
        }
    }

}
