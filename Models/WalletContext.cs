using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartStocks.Models
{
    public class WalletContext : DbContext
    {
        public WalletContext(DbContextOptions<WalletContext> options) : base(options)
        {
        }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }
}