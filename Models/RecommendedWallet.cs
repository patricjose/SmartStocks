using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartStocksAPI.Models
{
    public class RecommendedWallet
    {
        [Key]
        public Guid Id { get; set; }
        public string[] FundNames { get; set; }
        public List<Asset> Assets { get; set; }
        public decimal Total { get; set; }
        public decimal Performance1Month { get; set; }
        public decimal Performance6Months { get; set; }
        public decimal Performance12Months { get; set; }
    }
}