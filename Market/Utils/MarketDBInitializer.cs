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
                User User1 = new User("admin", "12345", "admin", "admin");
                Product Product1 = new Product("1", "Çikolata", 3, 100);
                Product Product2 = new Product("2", "Elma", 4, 50);
                Customer Customer1 = new Customer(1234567890, "Berke", "Kalkan");

                context.Users.Add(User1);
                context.Products.Add(Product1);
                context.Products.Add(Product2);
                context.Customers.Add(Customer1);

                // Trigger that deletes products sales related to sale
                context.Database.ExecuteSqlCommand(@"CREATE OR ALTER TRIGGER trig_delete_sale
                                                    On Sales
                                                    AFTER DELETE
                                                    AS 
                                                    BEGIN
                                                    DELETE ProductSales FROM ProductSales INNER JOIN deleted ON deleted.ID = SaleID
                                                    END;");

                context.Database.ExecuteSqlCommand(@"CREATE PROCEDURE CustomerSales @InputID BIGINT
                                                    AS
                                                    select Products.Price as Price, Products.Name, a.Amount
                                                    from (select ProductID, SUM(Amount) Amount
	                                                    from Sales
	                                                    join ProductSales
	                                                    on Sales.ID = ProductSales.SaleID
	                                                    where Sales.CustomerIDNumber = @InputID
	                                                    group by ProductID) a
                                                    join Products
                                                    on Products.ID = a.ProductID;");

                context.SaveChanges();
            }
        }
    }
}
