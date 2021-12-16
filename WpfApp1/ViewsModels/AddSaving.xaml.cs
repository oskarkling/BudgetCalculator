﻿using BudgetCalculator;
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
        /// <summary>
        /// loops through and prints all savings that the current account has.
        /// </summary>
        private void UpdateUI()
        {
            if(BackendManager.accountController.CurrentAccount.Savings != null)
            {
                foreach (var item in BackendManager.accountController.CurrentAccount.Savings)
                {
                    savingListbox.Items.Add($"{item.Name} | {item.Amount}");
                }
            }
        }
        /// <summary>
        /// Adds economic object to ListBox
        /// </summary>
        /// <param name="ecoObject"></param>
        private void AddItemToListBox(EconomicObject ecoObject)
        {
            savingListbox.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");
        }
        /// <summary>
        /// Takes the input from fields and creates a saving object if valid input. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSavingBtn_Click(object sender, RoutedEventArgs e)
        {

            var savingNameInput = savingName.Text;
            var timespanSaving = savingTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(savingAmount.Text, out decimal amount);
            bool recurring = true;
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
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
                    AddItemToListBox(saving);
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
        private void UpdateSaving_Click(object sender, RoutedEventArgs e)
        {
            var savingIndex = savingListbox.SelectedIndex;
            var selectedSaving = BackendManager.accountController.CurrentAccount.Savings[savingIndex];
            if (selectedSaving != null)
            {
                UpdateEconomicObject update = new UpdateEconomicObject(selectedSaving);
                update.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }
        /// <summary>
        /// gets the chosen object via SelectedIndex from the listbox, finds it in the list and sends it to DeleteEconomicObject view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSaving_Click(object sender, RoutedEventArgs e)
        {
            var savingIndex = savingListbox.SelectedIndex;
            var selectedSaving = BackendManager.accountController.CurrentAccount.Savings[savingIndex];
            if (selectedSaving != null)
            {
                DeleteEconomicObject update = new DeleteEconomicObject(selectedSaving);
                update.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }
        /// <summary>
        /// Closes this window and opens up mainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
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
