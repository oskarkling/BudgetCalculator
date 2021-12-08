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
    /// Interaction logic for AddExpense.xaml
    /// </summary>
    public partial class AddExpense : Window
    {
        public AddExpense()
        {
            InitializeComponent();
        }
        private void expenseBtn_Click(object sender, RoutedEventArgs e)
        {

             inputTest.Text = BackendManager.accountController.CurrentAccount.Password;


        }
    }
}
