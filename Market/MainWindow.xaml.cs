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
using Market.Pages;

namespace Market
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // If this is first run of the app 
            // Initialize session values
            if (App.LastLogin == null) { App.DestroySession(); }

            MarketDBInitializer.initDB(new MarketDBContext());
            InitializeComponent();
        }
        
       
        private void SatisButtonClicked(object sender, RoutedEventArgs e)
        {
            var ReturnValue = false;

            // If last login was in the last 5 minutes
            if ((DateTime.Now - App.LastLogin).TotalMinutes < 5)
            {
                // No need for login
                ReturnValue = true;
            }
            else
            {
                // Show loginwindow
                LogInWindow Login = new LogInWindow();
                ReturnValue = (bool)Login.ShowDialog();
            }

            // After login is successful

            if (ReturnValue == true)
            {
                // Login is succesful
                // Set Session
                App.CreateSession();
                // Change page
                MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                SalePage NewWindow = new SalePage();

                main.Title = NewWindow.Title;
                main.Content = NewWindow;
            }
        }

        private void RaporButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
