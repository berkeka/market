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
using System.Security.Cryptography;
using Market.Entities;
using Market.Pages;

namespace Market
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            // Get inputs from UsernameText PasswordText text boxes
            string InputUsername = UsernameText.Text;
            string InputPassword = PasswordText.Password;

            // Check if credientials are correct

            // hash input password here 

            var query = context.Users.Where(s => s.Username == InputUsername && 
                                            s.PasswordHash == InputPassword);

            if(query.Count() != 0)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya parola hatalı!");
                this.DialogResult = false;
            }
        }
    }
}
