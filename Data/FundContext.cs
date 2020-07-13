using Microsoft.EntityFrameworkCore;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Data
{
    public class FundContext : DbContext
    {
        public FundContext (DbContextOptions<FundContext> options) : base(options)
        {
        }

        public DbSet<Fund> Fund { get; set; }
    }
}
