using Microsoft.EntityFrameworkCore;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Data
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