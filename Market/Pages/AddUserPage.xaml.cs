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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Market.Entities;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
        }
        private void CompleteButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            string InputName = NameText.Text;
            string InputLastName = LastNameText.Text;
            string InputUserName = UserNameText.Text;
            string InputPassword = PasswordText.Password;
            string InputConfirmPassword = ConfirmPasswordText.Password;

            var query = context.Users.Where(s => s.Username == InputUserName);

            //Check username if its taken return
            if (query.Any()) { MessageBox.Show("Kullanıcı adı başkası tarafından alınmış!"); UserNameText.Text = String.Empty; return; }

            if(InputPassword != InputConfirmPassword) { MessageBox.Show("Parolalar aynı olmalı!"); PasswordText.Password = String.Empty; ConfirmPasswordText.Password = String.Empty; return; }

            User user = new User(InputUserName, InputPassword, Name, InputLastName);

            context.Users.Add(user);
            context.SaveChanges();

            NameText.Text = String.Empty;
            LastNameText.Text = String.Empty;
            UserNameText.Text = String.Empty;
            PasswordText.Password = String.Empty;
            ConfirmPasswordText.Password = String.Empty;

            MessageBox.Show(InputUserName + "kullanıcı adlı satıcı oluşturuldu");
        }
        //Go back to sale page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SalePage newPage = new SalePage();

            main.Title = newPage.Title;
            main.Content = newPage.Content;
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            // Get the instance of the MainWindow
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            // Initialize new MainWindow
            MainWindow new_main = new MainWindow();

            // Rebuild mainWindow
            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
