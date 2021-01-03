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

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for TrendChartPage.xaml
    /// </summary>
    public partial class TrendChartPage : Page
    {
        public TrendChartPage()
        {
            var context = new MarketDBContext();
            InitializeComponent();
            ProductList.ItemsSource = context.Products.ToList();
        }

        private void ShowButtonClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ProductList.SelectedItems.Count);
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MainWindow new_main = new MainWindow();

            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
