using BudgetCalculator;
using System.Windows;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for UpdateSaveToDateGoal.xaml
    /// </summary>
    public partial class UpdateSaveToDateGoal : Window
    {
        Goal SelectedObject;
        public UpdateSaveToDateGoal(Goal selectedObject)
        {
            InitializeComponent();
            SelectedObject = selectedObject;
            FillInfo(selectedObject);
        }

        /// <summary>
        /// gets input from fields and adds to selected object, goes back to addGoal view if update is succesfull.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var updatedName = nameOfGoal.Text;
            var amountToSaveTxt = totalAmountToSave.Text;
            var monthsTxt = numberOfMonths.Text;
            var amountParse = decimal.TryParse(amountToSaveTxt, out decimal amountToSave);
            var monthsParse = int.TryParse(monthsTxt, out int months);

            if (!amountParse || !monthsParse || amountToSave < 0)
            {
                MessageBox.Show("Wrong input");
            }
            else
            {
                SelectedObject.Name = updatedName;
                SelectedObject.Amount = amountToSave;
                SelectedObject.MonthsToGoal = months;
                if (BackendManager.accountController.UpdateObject(SelectedObject))
                {
                    MessageBox.Show("Update Complete!");
                    AddGoal addGoal = new();
                    addGoal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error!");
                }
            }
        }

        /// <summary>
        /// Fills the input fields with the selected goals info.
        /// </summary>
        /// <param name="goal"></param>
        private void FillInfo(Goal goal)
        {
            nameOfGoal.Text = goal.Name;
            totalAmountToSave.Text = goal.Amount.ToString();
            numberOfMonths.Text = goal.MonthsToGoal.ToString();
        }

        /// <summary>
        /// goes back to addGoal view
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
