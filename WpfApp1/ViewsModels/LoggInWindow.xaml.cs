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
        /// <summary>
        /// makes it able to drag the window while pressing the left mouse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        /// <summary>
        /// gets input from inputfields and calls accountcontrollers login method, if it returns true the user get sent to the main view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var username = usernameInput.Text;
            var password = passwordInput.Password;

            ClientConnection client = new ClientConnection();
            ConnectionStringHolder.ConnectionString = client.GetSqlConString(username, password);

            if(BackendManager.accountController.Login(username, password))
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password is invalid");
            }
        }
        /// <summary>
        /// gets input from inputfields and calls accountcontrollers register method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Password;

            ClientConnection client = new ClientConnection();
            ConnectionStringHolder.ConnectionString = client.GetSqlConString("Nicklas", "Password");

            if (BackendManager.accountController.Register(username, password))
            {
                MessageBox.Show("Account is registered");
            }
            else
            {
                MessageBox.Show("Could not register account");
            }

        }

    }
}
