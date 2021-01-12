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
    /// Interaction logic for StockPage.xaml
    /// </summary>
    public partial class StockPage : Page
    {
        public StockPage()
        {
            InitializeComponent();

            var context = new MarketDBContext();

            var queryPrd = context.Products;
            var queryStock = context.Stocks;
            if (queryStock.Any())
            {
                List<StockItem> items = new List<StockItem>();
                for (int i = 1; i <= queryStock.Count(); i++)
                {
                    var product = queryPrd.Find(i);
                    var productStock = queryStock.Find(product.Barcode);
                    items.Add(new StockItem() { Barcode = productStock.Barcode, Name = product.Name, Amount = productStock.Amount });
                }
                StockList.ItemsSource = items.OrderBy(i => i.Amount);
            }
        }
        public class StockItem
        {
            public string Barcode { get; set; }
            public string Name { get; set; }
            public double Amount { get; set; }
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
            MainWindow main = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            MainWindow new_main = new MainWindow();

            main.Title = new_main.Title;
            main.Content = new_main.Content;
            // Close the newly initialized window
            new_main.Close();
        }
    }
}
