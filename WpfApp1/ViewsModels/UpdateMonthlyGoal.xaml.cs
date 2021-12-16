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
    /// Interaction logic for UpdateMonthlyGoal.xaml
    /// </summary>
    public partial class UpdateMonthlyGoal : Window
    {
        Goal SelectedObject;
        public UpdateMonthlyGoal(Goal selectedObject)
        {
            InitializeComponent();
            SelectedObject = selectedObject;
            FillInfo(selectedObject);
        }
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var updatedName = nameOfGoal.Text;
            var amountToSaveTxt = totalAmountToSave.Text;
            var amountToSavePerMonthtxt = amountToSavePerMonth.Text;

            var amountParse = decimal.TryParse(amountToSaveTxt, out decimal totAmountToSave);
            var monthsParse = int.TryParse(amountToSavePerMonthtxt, out int amountPerMonth);

            if (!amountParse || !monthsParse)
            {
                MessageBox.Show("PLease fill all forms!!");
            }
            else
            {
                SelectedObject.Name = updatedName;
                SelectedObject.Amount = totAmountToSave;
                SelectedObject.MonthsToGoal = amountPerMonth;
                if (BackendManager.accountController.UpdateObject(SelectedObject))
                {
                    MessageBox.Show("Update Complete!");
                    AddGoal addGoal = new AddGoal();
                    addGoal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error!");
                }

            }

        }
        private void FillInfo(Goal goal)
        {
            nameOfGoal.Text = goal.Name;
            totalAmountToSave.Text = goal.Amount.ToString();
            amountToSavePerMonth.Text = goal.MonthsToGoal.ToString();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AddGoal addGoal = new AddGoal();
            addGoal.Show();
            this.Close();
        }
    }
}
