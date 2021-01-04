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
using Market.Entities;
using Market.Pages;

namespace Market.Pages
{
    /// <summary>
    /// Interaction logic for CustomerDebtPaymentPage.xaml
    /// </summary>
    public partial class CustomerDebtPaymentPage : Page
    {
        public long SelectedCustomerIDNumber
        {
            get
            {
                return _SelectedCustomerIDNumber;
            }
            set
            {
                _SelectedCustomerIDNumber = value;
                if (_SelectedCustomerIDNumber != 0)
                {
                    var context = new MarketDBContext();
                    Customer c = context.Customers.Find(SelectedCustomerIDNumber);
                    CustomerLabel.Content = "Seçilmiş Müşteri: " + c.Name + " " + c.LastName;

                    CustomerDebt cd = context.CustomerDebts.Find(SelectedCustomerIDNumber);
                    double sum = cd.DebtAmount;
                    // Set content of the label to sum
                    SumLabel.Content = sum.ToString();
                }
            }
        }
        private long _SelectedCustomerIDNumber;
        public CustomerDebtPaymentPage()
        {
            InitializeComponent();
        }
        private void OdeButtonClicked(object sender, EventArgs e)
        {

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
