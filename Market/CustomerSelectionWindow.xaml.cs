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
using System.Windows.Shapes;
using Market.Entities;

namespace Market
{
    /// <summary>
    /// Interaction logic for CustomerSelectionWindow.xaml
    /// </summary>
    public partial class CustomerSelectionWindow : Window
    {
        public int selectedCustomerID { get; set; }
        public CustomerSelectionWindow()
        {
            var context = new MarketDBContext();
            List<Customer> ls = context.Customers.ToList();

            InitializeComponent();
            CustomerList.ItemsSource = ls;
        }

        private void SecButtonClicked(object sender, RoutedEventArgs e)
        {
            var selection = CustomerList.SelectedItem;

            if(selection != null)
            {
                var customer = (Customer)selection;
                this.selectedCustomerID = customer.ID;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Selection");
            }
            
        }
    }
}
