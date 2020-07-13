using System;
using System.ComponentModel.DataAnnotations;

namespace SmartStocksAPI.Models
{
    public class Asset 
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Size { get; set; }
        public Guid WalletId {get; set; }
    }
}