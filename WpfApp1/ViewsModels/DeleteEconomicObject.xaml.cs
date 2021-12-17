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
    /// Interaction logic for DeleteEconomicObject.xaml
    /// </summary>
    public partial class DeleteEconomicObject : Window
    {
        EconomicObject SelectedObject;
        public DeleteEconomicObject(EconomicObject selectedObject)
        {
            InitializeComponent();
            SelectedObject = selectedObject;
            FillInfo();
        }
        /// <summary>
        /// sends in the selected object to the DeleteObject method in accountController. If it returns true the user will get sent back to main view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BackendManager.accountController.DeleteObject(SelectedObject))
            {
                MessageBox.Show("Deleted Successfully!");
                MainWindow main = new MainWindow();
                main.Show();
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
        /// Fills the input fields with the selected objects info.
        /// </summary>
        private void FillInfo()
        {
            deleteName.Text = SelectedObject.Name;
            deleteAmount.Text = SelectedObject.Amount.ToString();
        }
    }
}
