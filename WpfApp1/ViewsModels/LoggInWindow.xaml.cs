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
        //AccountController ac = new AccountController();
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

            if(BackendManager.accountController.Login(username, password))
            {

                //MessageBox.Show("Success");
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
                // SLUSSA VIDARE TILL HEM VY
            }
            else
            {
                MessageBox.Show("Username or Password is invalid");
            }
        }
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Password;
            if(BackendManager.accountController.Register(username, password))
            {

            }

        }

    }
}
