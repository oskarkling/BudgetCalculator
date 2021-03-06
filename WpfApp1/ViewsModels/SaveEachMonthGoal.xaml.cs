using BudgetCalculator;
using System;
using System.Windows;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for SaveEachMonthGoal.xaml
    /// </summary>
    public partial class SaveEachMonthGoal : Window
    {
        Goal goal = new();
        public SaveEachMonthGoal()
        {
            InitializeComponent();
            UpdateUI();
            addGoalBtn.IsEnabled = false;
        }

        /// <summary>
        /// Loops through and print all goals that an account has.
        /// </summary>
        private void UpdateUI()
        {
            foreach (var item in BackendManager.accountController.CurrentAccount.Goals)
            {
                listboxGoals.Items.Add($"{item.Name} | {item.Amount}");
            }
        }

        /// <summary>
        /// Adds economic object to ListBox
        /// </summary>
        /// <param name="ecoObject"></param>
        private void AddItemToListBox(EconomicObject ecoObject) => listboxGoals.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");

        /// <summary>
        /// Takes the input from fields and creates and fills goal object with info. Prints info about the goal in the result listbox.
        /// Also sets the add button to enable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
            var goalName = nameOfGoal.Text;
            var totAmountToSaveTxt = totalAmountToSave.Text;
            var amountPerMonthTxt = amountToSaveEachMonth.Text;
            var totAmountParseSuccessfull = decimal.TryParse(totAmountToSaveTxt, out decimal totAmountToSave);
            var amountPerMonthParseSuccessfull = decimal.TryParse(amountPerMonthTxt, out decimal amountPerMonth);

            if (!totAmountParseSuccessfull || !amountPerMonthParseSuccessfull || string.IsNullOrEmpty(goalName) || amountPerMonth < 0 || totAmountToSave < 0)
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
                    resultListbox.Items.Add($"you will afford {goal.Name} at\n{dateToAccomplishGoal.ToShortDateString()}\nwhen saving {goal.SaveEachMonth} per month");
                    addGoalBtn.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// calls create an economic object method in the accountcontroller with the goal object. If it returns true it adds the goal to the listbox,
        /// sets the addgoalbtn back to false and clears the result listbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (BackendManager.accountController.CreateAnEconomicObject(goal))
            {
                MessageBox.Show("GOAL ADDED");
                AddItemToListBox(goal);
                addGoalBtn.IsEnabled = false;
                resultListbox.Items.Clear();
            }
            else
            {
                MessageBox.Show("Could not add ");
            }
        }

        /// <summary>
        /// Closes this window and opens up mainWindow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AddGoal addGoal = new();
            addGoal.Show();
            this.Close();
        }
    }
}
