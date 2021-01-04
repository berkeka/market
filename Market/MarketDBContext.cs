using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Market.Entities;

namespace Market
{
    class MarketDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDebt> CustomerDebts { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
    }
}
