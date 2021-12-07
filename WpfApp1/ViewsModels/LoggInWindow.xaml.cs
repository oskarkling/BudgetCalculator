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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoggInWindow.xaml
    /// </summary>
    public partial class LoggInWindow : Window
    {
        AccountController ac = new AccountController();
        public LoggInWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var username = usernameInput.Text;
            var password = passwordInput.Password;

            if(ac.Login(username, password))
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Failed to login");
            }
            //MessageBox.Show($"{username} | {password}");
        }
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Password;
            if(ac.Register(username, password))
            {
                MessageBox.Show("Success");
            }
            //MessageBox.Show("register button clicked!");
        }

    }
}
