﻿using System;
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
using Market.Entities;

namespace Market
{
    /// <summary>
    /// Interaction logic for SaleWindow.xaml
    /// </summary>
    public partial class SalePage : Page
    {
        public SalePage()
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
                    string color = "";
                    var product = queryPrd.Find(i);
                    var productStock = queryStock.Find(product.Barcode);
                    int amountPercent;
                    if (product.WarningLimit != 0)
                    {
                        amountPercent = (int)((100 * productStock.Amount) / product.WarningLimit);
                        // We have less products than the warning limit
                        if ((product.WarningLimit / 2) > productStock.Amount)
                        {
                            // Less than %50 of the wanted amount
                            
                            color = "Crimson"; 
                        }
                        else if (amountPercent < 80)
                        {
                            // Less than %50 of the wanted amount
                            color = "Yellow";
                        }
                        else
                        {
                            // Equal or More than the wanted amount
                            color = "Green";
                        }
                        items.Add(new StockItem() { Title = product.Name, Completion = amountPercent, Color = color });
                    } 
                }
                StockList.ItemsSource = items.OrderBy(i => i.Completion);                
            }
        }
        private void PesinButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SaleCartPage());
        }

        private void StockButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new StockPage());
        }

        private void MusteriButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new CustomerPage());
        }

        private void UrunButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new ProductPage());
        }

        private void UrunGirisButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new ProductIntakePage());
        }

        private void SaleRecordButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SaleRecordPage());
        }

        private void UserButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new AddUserPage());
        }
        private void SupplierButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateTo(new SupplierPage());
        }

        private void BorcButtonClicked(object sender, RoutedEventArgs e)
        {
            CustomerSelectionWindow dw = new CustomerSelectionWindow();

            bool returnValue = (bool)dw.ShowDialog();

            if (returnValue == true)
            {
                long sc = dw.selectedCustomerIDNumber;

                CustomerDebtPaymentPage NewWindow = new CustomerDebtPaymentPage();
                NewWindow.SelectedCustomerIDNumber = sc;

                App.NavigateTo(NewWindow);
            }
        }
        private void CariButtonClicked(object sender, RoutedEventArgs e)
        {
            CustomerSelectionWindow csw = new CustomerSelectionWindow();

            bool returnValue = (bool)csw.ShowDialog();

            if (returnValue == true)
            {
                long sc = csw.selectedCustomerIDNumber;

                SaleCartPage NewWindow = new SaleCartPage();
                NewWindow.SelectedCustomerIDNumber = sc;

                App.NavigateTo(NewWindow);
            }
        }

        private void TedarikBorcButtonClicked(object sender, RoutedEventArgs e)
        {
            SupplierSelectionWindow dw = new SupplierSelectionWindow();

            bool returnValue = (bool)dw.ShowDialog();

            if (returnValue == true)
            {
                int ss = dw.selectedSupplierID;

                SupplierDebtPaymentPage NewWindow = new SupplierDebtPaymentPage();
                NewWindow.SelectedSupplierID = ss;

                App.NavigateTo(NewWindow);
            }
        }
        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            App.NavigateToMain();
        }
        private void CikisButtonClicked(object sender, RoutedEventArgs e)
        {
            //Log out
            App.DestroySession();

            App.NavigateToMain();
        }
        public class StockItem
        {
            public string Title { get; set; }
            public int Completion { get; set; }
            public string Color { get; set; }
        }
    }
}
