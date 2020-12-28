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

namespace Market
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            InitializeComponent();
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            //Add customer to the customers list if its already exists don't.
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
