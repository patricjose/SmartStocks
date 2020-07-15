using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Controllers
{
    public interface IRecommendedWalletController
    {
        Task<ActionResult<RecommendedWallet>> DeleteRecommendedWallet(Guid id);
        Task<ActionResult<RecommendedWallet>> GetRecommendedWallet(Guid id);
        Task<ActionResult<RecommendedWallet>> GetRecommendedWallet(string fundName);
        Task<ActionResult<RecommendedWallet>> PostRecommendedWallet(RecommendedWallet recommendedWallet);
        Task<IActionResult> PutRecommendedWallet(Guid id, RecommendedWallet recommendedWallet);
    }
}