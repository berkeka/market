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

                context.Users.Add(User1);

                // Trigger that deletes products sales related to sale
                context.Database.ExecuteSqlCommand(@"CREATE PROCEDURE CalculateStockAfterSaleDeletion @SaleID INT
                                                    AS
                                                    UPDATE Stocks 
                                                    SET 
                                                        Amount = Amount + (select a.Amount
						                                                    from (select * 
							                                                    from ProductSales
							                                                    where SaleID = @SaleID) a)
                                                    WHERE Stocks.Barcode = (select Products.Barcode
						                                                    from (select * 
							                                                    from ProductSales
							                                                    where SaleID = @SaleID) a
						                                                    join Products
						                                                    on Products.ID = a.ProductID);");

                context.Database.ExecuteSqlCommand(@"CREATE PROCEDURE CalculateNewDebtAmount @SaleID INT, @CustomerIDNumber BIGINT
                                                    AS
                                                    UPDATE CustomerDebts
                                                    SET
                                                    DebtAmount = DebtAmount - (select Products.Price* a.Amount as sum_amount
                                                                                from(select ProductID, SUM(Amount) Amount
                                                                                        from Sales
                                                                                        join ProductSales
                                                                                        on Sales.ID = ProductSales.SaleID
                                                                                        where Sales.ID = 1
                                                                                        group by ProductID) a
                                                                                join Products
                                                                                on Products.ID = a.ProductID)
                                                    WHERE CustomerIDNumber = @CustomerIDNumber;");


                context.Database.ExecuteSqlCommand(@"CREATE OR ALTER TRIGGER trig_delete_sale
                                                    On Sales
                                                    AFTER DELETE
                                                    AS 
                                                    BEGIN
	                                                    DECLARE @SaleID INT
                                                        SELECT TOP 1 @SaleID = ID
                                                        FROM    DELETED

                                                        DECLARE @CustomerIDNumber BIGINT
                                                        SELECT TOP 1 @CustomerIDNumber = CustomerIDNumber
                                                        FROM DELETED
                                                        
	                                                    EXEC CalculateStockAfterSaleDeletion @SaleID = @SaleID
	                                                    DELETE ProductSales FROM ProductSales INNER JOIN deleted ON deleted.ID = SaleID;
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
