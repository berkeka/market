using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Market.Entities;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for ProductIntakePage.xaml
    /// </summary>
    public partial class ProductIntakePage : Page
    {
        public ProductIntakePage()
        {
            InitializeComponent();
        }

        private void DosyaButtonClicked(object sender, RoutedEventArgs e)
        {

            // Input Format
            // 1- DispatchID
            // 2- Barcode, Price, Amount
            // 3- Barcode, Price, Amount

            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //fd.InitialDirectory = "";

            if (fd.ShowDialog() == DialogResult.OK) 
            {
                var context = new MarketDBContext();
                // Get text from selected File
                var InputText = File.ReadAllText(fd.FileName);
                // Split the text into rows
                var Rows = InputText.Split('\n');
                // If there are more than one row 
                if(Rows.Length > 1)
                {
                    DispatchNoLabel.Content = "";
                    ProductList.Items.Clear();
                    try
                    {
                        // Get the Dispatch id from the file
                        int InputDispatchID = int.Parse(Rows[0]);
                        // Set the dispatch id value to the corresponding label
                        DispatchNoLabel.Content = Rows[0];
                        
                        // Loop through rows
                        for (int i = 1; i < Rows.Length; i++)
                        {
                            // Split the row into values
                            var Values = Rows.ElementAt(i).Split(',');
                            // Parse values
                            string Barcode = Values.ElementAt(0);
                            double Price = double.Parse(Values.ElementAt(1));
                            double Amount = double.Parse(Values.ElementAt(2));
                            // Find name of the product using given barcode
                            string Name = context.Products.Where(s => s.Barcode == Barcode).First().Name;

                            ProductItem pi = new ProductItem();

                            pi.Name = Name;
                            pi.Barcode = Barcode;
                            pi.Price = (float)Price;
                            pi.Amount = (int)Amount;

                            ProductList.Items.Add(pi);
                        }
                    }
                    catch (Exception ex)
                    {
                        // If a problem occurs while parsing the file give a warning
                        // and clear the list
                        Console.WriteLine(ex.Message);
                        ProductList.Items.Clear();
                        DispatchNoLabel.Content = "";
                        System.Windows.MessageBox.Show("Flawed Input file");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Flawed Input file");
                }
            }
                
        }

        private void OnaylaButtonClicked(object sender, RoutedEventArgs e)
        {
            var context = new MarketDBContext();
            int DispatchID = int.Parse(DispatchNoLabel.Content.ToString());

            // If there are products on the list and dispatchID is new
            if(context.Storages.Where(x => x.DispatchNoteID == DispatchID).Any()) { System.Windows.MessageBox.Show("Dispatch with the given ID exists."); return; }
            if(ProductList.Items.Count > 0 )
            {
                for(int i = 0; i < ProductList.Items.Count; i++)
                {
                    ProductItem item = (ProductItem)ProductList.Items.GetItemAt(i);
                    Storage s = new Storage(DispatchID, item.Barcode, item.Price, item.Amount);
                    context.Storages.Add(s);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("File not selected");
            }
            ProductList.Items.Clear();
            context.SaveChanges();
        }
    }
}
