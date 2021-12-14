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
        }
        private void SaveEachMonth_Click(object sender, RoutedEventArgs e)
        {
            SaveEachMonthGoal monthlyGoal = new SaveEachMonthGoal();
            monthlyGoal.Show();
            this.Close();
        }
        private void SaveToDate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("To Date");
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
