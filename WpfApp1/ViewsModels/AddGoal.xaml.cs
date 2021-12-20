using System.Linq;
using System.Windows;

namespace WpfApp1.Views
{
    /// <summary>
    /// Interaction logic for AddGoal.xaml
    /// </summary>
    public partial class AddGoal : Window
    {
        public AddGoal()
        {
            InitializeComponent();
            UpdateUI();
        }

        /// <summary>
        /// loops through and prints all goals that the current account has. Sorts them in order of Monthly goals -> to date goals.
        /// </summary>
        private void UpdateUI()
        {
            var query = from g in BackendManager.accountController.CurrentAccount.Goals
                        orderby g.SaveToDate
                        select g;

            if (query != null)
            {
                foreach (var item in query)
                {
                    goalListbox.Items.Add($"{item.Name} | {item.Amount} | {item.SaveToDate}");
                }
            }
        }

        /// <summary>
        /// goes to view for save monthly goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEachMonth_Click(object sender, RoutedEventArgs e)
        {
            SaveEachMonthGoal monthlyGoal = new();
            monthlyGoal.Show();
            this.Close();
        }

        /// <summary>
        /// goes to view for save to end date goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToDate_Click(object sender, RoutedEventArgs e)
        {
            SaveToEndDateGoal endDateGoal = new();
            endDateGoal.Show();
            this.Close();
        }

        /// <summary>
        /// Returns to main view
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
        /// gets the chosen object via SelectedIndex from the listbox, and sends it to selectedGoal view if SaveToDate is true,
        /// otherwise sends it to UpdateMonthlyGoal view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var goalIndex = goalListbox.SelectedIndex;

            if (goalIndex == -1)
            {
                MessageBox.Show("Choose an item");
            }
            else
            {
                var selectedGoal = BackendManager.accountController.CurrentAccount.Goals[goalIndex];
                if (selectedGoal != null)
                {
                    if (selectedGoal.SaveToDate)
                    {
                        UpdateSaveToDateGoal update = new(selectedGoal);
                        update.Show();
                        this.Close();
                    }
                    else
                    {
                        UpdateMonthlyGoal update = new(selectedGoal);
                        update.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }

        /// <summary>
        /// gets the chosen object via SelectedIndex from the listbox, and sends it to Delete object view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var goalIndex = goalListbox.SelectedIndex;

            if (goalIndex == -1)
            {
                MessageBox.Show("Select an item");
            }
            else
            {
                var selectedGoal = BackendManager.accountController.CurrentAccount.Goals[goalIndex];
                if (selectedGoal != null)
                {
                    DeleteEconomicObject delete = new(selectedGoal);
                    delete.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
            }
        }
    }
}
