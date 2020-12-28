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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage()
        {
            
            InitializeComponent();
            RefreshList(ProductList);
            
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();

            string InputBarcode = BarcodeText.Text;
            string InputName = NameText.Text;
            int InputPrice = int.Parse(PriceText.Text);

            // Check inputs for exceptions

            Product p = new Product(InputBarcode, InputName, InputPrice);

            context.Products.Add(p);
            context.SaveChanges();
            RefreshList(ProductList);
        }

        public void RefreshList(ListView List)
        {
            var context = new MarketDBContext();

            List.ItemsSource = context.Products.ToList<Product>();
        }
    }
}
