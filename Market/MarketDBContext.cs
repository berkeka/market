using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market
{
    class MarketDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
