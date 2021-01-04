using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Market.Entities;

namespace Market
{
    class MarketDBInitializer
    {
        public static void initDB(MarketDBContext context)
        {
            if (!context.Database.Exists())
            {
                User User1 = new User("Admin", "12345", "admin", "admin");
                Product Product1 = new Product("1", "a", 1);
                Customer Customer1 = new Customer(1234567890, "Berke", "Kalkan");
                CustomerDebt CustomerDebt1 = new CustomerDebt(1234567890, 40);

                context.Users.Add(User1);
                context.Products.Add(Product1);
                context.Customers.Add(Customer1);
                context.CustomerDebts.Add(CustomerDebt1);
                context.SaveChanges();
            }
        }
    }
}
