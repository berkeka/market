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
    /// Interaction logic for SaleWindow.xaml
    /// </summary>
    public partial class SaleWindow : Page
    {
        public SaleWindow()
        {
            InitializeComponent();
        }
        private void PesinButtonClicked(object sender, RoutedEventArgs e)
        {
            // Gets the instance of MainWindow
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SaleCartWindow NewWindow = new SaleCartWindow();

            main.Title = NewWindow.Title;
            main.Content = NewWindow;

        }
        private void CariButtonClicked(object sender, RoutedEventArgs e)
        {
            // First select a customer 
            // Then proceed to the salecart window
            // Need to pass CustomerID to the new page

            /*
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            SaleCartWindow NewWindow = new SaleCartWindow();

            main.Title = NewWindow.Title;
            main.Content = NewWindow;
            */
        }

        private void MusteriButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            CustomerWindow NewWindow = new CustomerWindow();

            main.Title = NewWindow.Title;
            main.Content = NewWindow;
        }

        private void UrunButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            ProductPage NewPage = new ProductPage();

            main.Title = NewPage.Title;
            main.Content = NewPage;
        }
    }
}
