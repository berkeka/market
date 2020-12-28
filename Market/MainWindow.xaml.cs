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
            MarketDBInitializer.initDB(new MarketDBContext());
            InitializeComponent();
        }
        private void SatisButtonClicked(object sender, RoutedEventArgs e)
        {
            // Show loginwindow
            LogInWindow Login = new LogInWindow();
            var ReturnValue=  Login.ShowDialog();
            // After login is successful

            if (ReturnValue == true)
            {
                // Login is succesful
                SalePage NewWindow = new SalePage();

                this.Title = NewWindow.Title;
                this.Content = NewWindow;
            }
            else
            {
                // Login is unsuccesful
            }
            

        }

        private void RaporButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
