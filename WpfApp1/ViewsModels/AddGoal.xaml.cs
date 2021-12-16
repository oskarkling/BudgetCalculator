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
    /// Interaction logic for AddGoal.xaml
    /// </summary>
    public partial class AddGoal : Window
    {
        public AddGoal()
        {
            InitializeComponent();
            UpdateUI();
        }
        private void UpdateUI()
        {
            var query = from g in BackendManager.accountController.CurrentAccount.Goals
                        orderby g.SaveToDate
                        select g;

            if (BackendManager.accountController.CurrentAccount.Goals != null)
            {
                foreach (var item in query)
                {
                    if (item.SaveToDate)
                    {
                        goalListbox.Items.Add($"{item.Name} | {item.Amount} | {item.SaveToDate}");
                    }
                }
            }

        }
        private void SaveEachMonth_Click(object sender, RoutedEventArgs e)
        {
            SaveEachMonthGoal monthlyGoal = new SaveEachMonthGoal();
            monthlyGoal.Show();
            this.Close();
        }
        private void SaveToDate_Click(object sender, RoutedEventArgs e)
        {
            SaveToEndDateGoal endDateGoal = new SaveToEndDateGoal();
            endDateGoal.Show();
            this.Close();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var goalIndex = goalListbox.SelectedIndex;
            var selectedGoal = BackendManager.accountController.CurrentAccount.Goals[goalIndex];
            if (selectedGoal != null)
            {
               if(selectedGoal.SaveToDate)
                {
                    // UPDATE SAVETODATE VIEW
                    UpdateSaveToDateGoal update = new UpdateSaveToDateGoal(selectedGoal);
                    update.Show();
                    this.Close();
                }
               else
                {
                    // UPDATE SAVEMONTHLY VIEW
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var goalIndex = goalListbox.SelectedIndex;
            var selectedGoal = BackendManager.accountController.CurrentAccount.Goals[goalIndex];
            if (selectedGoal != null)
            {
                DeleteEconomicObject delete = new DeleteEconomicObject(selectedGoal);
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
