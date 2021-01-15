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
    /// Interaction logic for SaleCountReportPage.xaml
    /// </summary>
    public partial class SaleCountReportPage : Page
    {
        public SaleCountReportPage()
        {
            var context = new MarketDBContext();

            var result = context.Database.SqlQuery<ProductItem>(@"select 1 as ID, cast(1 as float) as Price, cast(1 as float) as WarningLimit, Products.Name as ""Name"", Products.Barcode as Barcode, SUM(ProductSales.Amount) as Amount
                                                                from ProductSales
                                                                join Products
                                                                on ProductSales.ProductID = Products.ID
                                                                group by ""Name"", Barcode
                                                                order by Amount DESC;");

            InitializeComponent();

            if (result.ToList().Count > 0)
            {
                List.ItemsSource = result.ToList();
            }
            
        }
        private void ColumnClicked(object sender, RoutedEventArgs e)
        {
            if(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Satılma miktarı")
            {
                var items = List.Items;
                List<ProductItem> reversed_items = new List<ProductItem>();
                for (int i = (items.Count) - 1; i >= 0; i--)
                {
                    reversed_items.Add((ProductItem)items.GetItemAt(i));
                }

                List.ItemsSource = reversed_items;
            }
        }
        //Go back to report main page
        private void GoBackButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new ReportMainPage());
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }
    }
}
