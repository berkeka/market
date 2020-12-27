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

namespace Market
{
    /// <summary>
    /// Interaction logic for SaleCartWindow.xaml
    /// </summary>
    public partial class SaleCartWindow : Page
    {
        public SaleCartWindow()
        {
            InitializeComponent();
        }

        private void EkleButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            var InputBarcode = BarcodeText.Text;
            int Amount;

            if(AmountText.Text != "")
            {
                // Parse amount value
                Amount = int.Parse(AmountText.Text);

                var query = context.Products.Where(s => s.Barcode == InputBarcode);

                if (query.Any() && Amount > 0)
                {
                    // Create object for new item
                    Product p = query.First();
                    ProductItem pi = new ProductItem(p, Amount);

                    // Check if the added item was already in the list
                    bool productInList = false;

                    for (int i = 0; i < ItemList.Items.Count; i++)
                    {
                        ProductItem piFromWindow = (ProductItem)ItemList.Items.GetItemAt(i);
                          
                        // If product was already in the list
                        if (piFromWindow.Barcode == pi.Barcode)
                        {
                            productInList = true;
                            piFromWindow.Amount += pi.Amount;
                            ItemList.Items.Remove(piFromWindow);
                            ItemList.Items.Add(piFromWindow);
                            
                        }

                    }
                    
                    // If product wasn't already in the list 
                    if(!productInList)
                    {
                        ItemList.Items.Add(pi);
                    }

                    // Calculate new sum
                    RefreshSum();
                }
                else
                {
                    MessageBox.Show("Couldn't find Product");
                }
            }
            else
            {
                MessageBox.Show("Wrong Amount!");
            }

        }

        private void CikarButtonClicked(object sender, RoutedEventArgs e)
        {
            // Get selected items
            var List = ItemList.SelectedItems;

            // Loop through items
            for (int i = 0; i < List.Count; i++)
            {
                // Remove each one of them
                ItemList.Items.Remove(List[i]);
                // Calculate sum after each removal
                RefreshSum();
            }

            // Remove the selected item from the list 
            // Calculate sum value and change the label

        }
        private void TamamlaButtonClicked(object sender, RoutedEventArgs e)
        {
            // Check if no problems exists
        }

        private void RefreshSum()
        {
            double sum = 0.0;
            for (int i = 0; i < ItemList.Items.Count; i++)
            {
                ProductItem pi = (ProductItem)ItemList.Items.GetItemAt(i);

                sum += (pi.Amount * pi.Price);
            }

            SumLabel.Content = sum.ToString();
        }


    }

    class ProductItem : Product
    {
        public int Amount { get; set; }

        public ProductItem() { }
        public ProductItem(Product p, int Amount)
        {
            this.Barcode = p.Barcode;
            this.ID = p.ID;
            this.Name = p.Name;
            this.Price = p.Price;
            this.Amount = Amount;
        }

    }
}
