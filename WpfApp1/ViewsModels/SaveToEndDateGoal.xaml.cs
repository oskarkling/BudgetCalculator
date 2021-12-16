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
    /// Interaction logic for SaveToEndDateGoal.xaml
    /// </summary>
    public partial class SaveToEndDateGoal : Window
    {
        Goal goal = new Goal();
        public SaveToEndDateGoal()
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
        private void AddItemToListBox(EconomicObject ecoObject)
        {
            listboxGoals.Items.Add($"{ecoObject.Name} | {ecoObject.Amount}");
        }
        /// <summary>
        /// Takes the input from fields and creates and fills goal object with info. Prints info about the goal in the result listbox.
        /// Also sets the addGoalbtn to enable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            var loggedInAccount = BackendManager.accountController.CurrentAccount;
            var goalName = nameOfGoal.Text;
            var totAmountToSaveTxt = totalAmountToSave.Text;
            var numberOfMonthsTxt = numberOfMonths.Text;

            var totAmountParseSuccessfull = decimal.TryParse(totAmountToSaveTxt, out decimal totAmountToSave);
            var numberOfMonthsParseSuccessfull = int.TryParse(numberOfMonthsTxt, out int numberOfMonthsToSave);

            if (!totAmountParseSuccessfull || !numberOfMonthsParseSuccessfull || string.IsNullOrEmpty(goalName))
            {
                MessageBox.Show("Not valid input! Try again");
            }
            else
            {
                goal.Name = goalName;
                goal.Amount = totAmountToSave;
                goal.MonthsToGoal = numberOfMonthsToSave;
                goal.SaveToDate = true;
                goal.CreationTime = DateTime.Now;
                goal.AccountId = loggedInAccount.Id;

                var goalAmountNeededEachMonth = BackendManager.accountController.GetGoalAmountNeededEachMonth(goal);

                resultListbox.Items.Clear();
                // display max 2 decimals if needed?
                resultListbox.Items.Add($"you will have to save {string.Format(goalAmountNeededEachMonth % 1 == 0 ? "{0:0}" : "{0:0.00}", goalAmountNeededEachMonth)} each month\nto afford {goal.Name} in {goal.MonthsToGoal} months");
                addGoalBtn.IsEnabled = true;
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
            AddGoal addGoal = new AddGoal();
            addGoal.Show();
            this.Close();
        }
    }
}
