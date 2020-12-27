﻿using System;
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
                User User1 = new User(1, "Admin", "12345", "admin", "admin");
                Product Product1 = new Product("1", 1, "a", 1);

                context.Users.Add(User1);
                context.Products.Add(Product1);
                context.SaveChanges();
            }
        }
    }
}