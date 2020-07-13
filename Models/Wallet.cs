using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartStocksAPI.Models
{
    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }
        public string Fund { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public decimal Total { get; set; }
    }
}