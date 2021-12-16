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
    /// Interaction logic for UpdateEconomicObject.xaml
    /// </summary>
    public partial class UpdateEconomicObject : Window
    {
        EconomicObject SelectedObject;
        public UpdateEconomicObject(EconomicObject selectedObject)
        {
            InitializeComponent();
            FillInfo(selectedObject);
            SelectedObject = selectedObject;
        }
        /// <summary>
        /// gets input from fields and adds to selected object, goes back to main if update is succesfull.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var updatedName = updateName.Text;
            var timespanInput = updateTimespan.SelectedIndex;
            var parseSuccessfull = decimal.TryParse(updateAmount.Text, out decimal amount);
            bool recurring = true;
            ConvertTimeSpan(ref timespanInput, ref recurring);
            if(!parseSuccessfull)
            {
                MessageBox.Show("PLease fill all forms!!");
            }
            else
            {
                SelectedObject.Name = updatedName;
                SelectedObject.Interval = timespanInput;
                SelectedObject.Amount = amount;
                if(BackendManager.accountController.UpdateObject(SelectedObject))
                {
                    MessageBox.Show("Update Complete!");
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error!");
                }
                
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
            }
        }
        /// <summary>
        /// Fills the input fields with the selected objects info.
        /// </summary>
        /// <param name="economicObject"></param>
        private void FillInfo(EconomicObject economicObject)
        {
            updateName.Text = economicObject.Name;
            updateAmount.Text = economicObject.Amount.ToString();
        }
    }
}
