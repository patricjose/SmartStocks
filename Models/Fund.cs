using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartStocksAPI.Models
{
    public class Fund
    {
        [Key]
        public Guid Id { get; set; }
        public string FundName { get; set; }
        public Wallet Wallet { get; set; }
        public string Type { get; set; }
        public string Class { get; set; }
        public decimal Variation6Months { get; set; } 
    }
}