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
    /// Interaction logic for SaveEachMonthGoal.xaml
    /// </summary>
    public partial class SaveEachMonthGoal : Window
    {
        Goal goal = new Goal();
        public SaveEachMonthGoal()
        {
            InitializeComponent();
            UpdateUI();
            addGoalBtn.IsEnabled = false;

        }
        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Goals)
            {
                listboxGoals.Items.Add($"{item.Name} | {item.Amount}");
            }
        }
        private void AddItemToListBox(EconomicObject ecoObject)
        {
            listboxGoals.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");
        }
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
            var goalName = nameOfGoal.Text;
            var totAmountToSaveTxt = totalAmountToSave.Text;
            var amountPerMonthTxt = amountToSaveEachMonth.Text;

            var totAmountParseSuccessfull = decimal.TryParse(totAmountToSaveTxt, out decimal totAmountToSave);
            var amountPerMonthParseSuccessfull = decimal.TryParse(amountPerMonthTxt, out decimal amountPerMonth);

            if (!totAmountParseSuccessfull || !amountPerMonthParseSuccessfull || string.IsNullOrEmpty(goalName))
            {
                MessageBox.Show("Not valid input! Try again");
            }
            else
            {
                goal.Name = goalName;
                goal.Amount = totAmountToSave;
                goal.SaveEachMonth = amountPerMonth;
                goal.CreationTime = DateTime.Now;
                goal.AccountId = loggedInAccount.Id;

                var dateToAccomplishGoal = BackendManager.accountController.GetGoalEndDate(goal);
                if (dateToAccomplishGoal < goal.CreationTime)
                {
                    MessageBox.Show("Something went wrong! Try again");
                }
                else
                {
                    resultListbox.Items.Clear();
                    resultListbox.Items.Add($"you will afford {goal.Name}\nat {dateToAccomplishGoal.ToShortDateString()}\nwhen saving {goal.SaveEachMonth} per month");
                    addGoalBtn.IsEnabled = true;
                }
            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BackendManager.accountController.CreateAnEconomicObject(goal))
            {
                MessageBox.Show("GOAL ADDED");
                MessageBox.Show($"{goal.Name} | {goal.Amount} | {goal.SaveEachMonth}");
                AddItemToListBox(goal);
                addGoalBtn.IsEnabled = false;
                resultListbox.Items.Clear();
            }
            else
            {
                MessageBox.Show("Could not add ");

            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AddGoal addGoal = new AddGoal();
            addGoal.Show();
            this.Close();
        }

    }
}
