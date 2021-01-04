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
using LiveCharts;
using LiveCharts.Wpf;

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
            // Clear the chart if it has values already
            if (Chart.Series != null) { Chart.Series.Clear(); }

            // Exception control
            // If date fields are empty
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null) { MessageBox.Show("Please select dates"); return; }
            // If selected dates aren't possible
            if (StartDatePicker.SelectedDate >= EndDatePicker.SelectedDate) { MessageBox.Show("Start date should be before the end date"); return; }

            DateTime Start = (DateTime)StartDatePicker.SelectedDate;
            DateTime End = (DateTime)EndDatePicker.SelectedDate;

            string dateFormat;
            if(End.Year != Start.Year) { dateFormat = "dd/MM/yyyy"; }
            else{ dateFormat = "dd/MM"; }

            Chart.Series = new SeriesCollection{};

            // Calculate total days between selected dates
            int TotalDays = (int)(End - Start).TotalDays + 1;

            // Get selected Products
            var selectedItems = ProductList.SelectedItems;
            if (selectedItems.Count == 0) { MessageBox.Show("Please select a product"); return; }

            ChartValues<string> DateLabels = new ChartValues<string>();

            // Loop through selected items and draw a line for every item
            foreach (Product product in selectedItems)
            {
                // Create arrays for x and y values of the graph
                DateTime[] dates = new DateTime[TotalDays];
                ChartValues<double> AmountOfProductsSold = new ChartValues<double>();

                var context = new MarketDBContext();

                // Loop for total days
                for (int i = 0; i < TotalDays; i++)
                {
                    DateTime indexDate = Start.AddDays(i);
                    DateTime queryEndDate = indexDate.AddDays(1);

                    dates[i] = indexDate;

                    // List of sales that are between start and end date
                    List<Sale> saleList = context.Sales.Where(s => s.Date >= indexDate && s.Date < queryEndDate).ToList();

                    if (saleList.Count() > 0)
                    {
                        double amountSum = 0;
                        // Loop through each sale we found
                        foreach (Sale saleItem in saleList)
                        {
                            // Get each product that is related to the sale we are currently looping
                            // Then check if we have the selected product in those related products
                            List<ProductSale> psList = context.ProductSales.Where(s => s.SaleID == saleItem.ID)
                                                                            .Where(ps => ps.ProductID == product.ID).ToList();

                            if (psList.Count > 0)
                            {
                                amountSum += psList.First().Amount;
                            }
                        }
                        // Insert the sum to the corresponding chartValue
                        AmountOfProductsSold.Insert(i, amountSum);
                    }
                    else
                    {
                        AmountOfProductsSold.Insert(i, 0);
                    }
                }

                foreach (DateTime date in dates)
                {
                    DateLabels.Add(date.ToString(dateFormat));
                }

                // Input data to the chart
                Chart.Series.Add(new LineSeries
                {
                    LineSmoothness = 0,
                    Title = product.Name,
                    Values = AmountOfProductsSold
                });
                
            }
            
            // Set date strings to labels on the X axis
            Chart.AxisX.First().Labels = DateLabels;
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
