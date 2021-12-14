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
    /// Interaction logic for AddSaving.xaml
    /// </summary>
    public partial class AddSaving : Window
    {
        public AddSaving()
        {
            InitializeComponent();
            UpdateUI();
            
        }
        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Savings)
            {
                savingListbox.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void AddSavingBtn_Click(object sender, RoutedEventArgs e)
        {

            var savingNameInput = savingName.Text;
            var timespanSaving = savingTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(savingAmount.Text, out decimal amount);
            bool recurring = true;
            GetCurrentUser(out Account loggedInAccount);
            ConvertTimeSpan(ref timespanSaving, ref recurring);
            if (!parseSuccessfull && !Validator.AddSavingValidator)
            {
                MessageBox.Show("PLease fill all forms!!");
            }
            else
            {

                Saving saving = new Saving()
                {
                    Name = savingNameInput,
                    Interval = timespanSaving,
                    Amount = amount,
                    Recurring = recurring,
                    CreationTime = DateTime.Now,
                    AccountId = loggedInAccount.Id
                };

                if (BackendManager.accountController.CreateAnEconomicObject(saving))
                {
                    MessageBox.Show("SAVING ADDED");
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
                    Validator.AddSavingValidator = false;
                    break;
            }
        }
    }
}
