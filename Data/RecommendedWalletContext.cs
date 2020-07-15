using Microsoft.EntityFrameworkCore;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Data
{
    public class RecommendedWalletContext : DbContext
    {
        public RecommendedWalletContext(DbContextOptions<RecommendedWalletContext> options) : base(options)
        {
        }

        public DbSet<RecommendedWallet> RecommendedWallets { get; set; }
        public DbSet<Asset> Assets { get; set; }
    }
}