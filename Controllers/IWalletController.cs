using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Controllers
{
    public interface IWalletController
    {
        Task<ActionResult<Wallet>> DeleteWallet(Guid id);
        Task<ActionResult<Wallet>> GetWallet(Guid id);
        Task<ActionResult<IEnumerable<Wallet>>> GetWallets();
        Task<ActionResult<Wallet>> PostWallet(Wallet wallet);
        Task<IActionResult> PutWallet(Guid id, Wallet wallet);
    }
}