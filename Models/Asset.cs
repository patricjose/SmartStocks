using System;
using System.ComponentModel.DataAnnotations;

namespace SmartStocks.Models
{
    public class Asset 
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public decimal WalletId {get; set; }
    }
}