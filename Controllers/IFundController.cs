using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Controllers
{
    public interface IFundController
    {
        Task<ActionResult<Fund>> DeleteFund(string id);
        Task<ActionResult<IEnumerable<Fund>>> GetFund();
        Task<ActionResult<Fund>> GetFund(string id);
        Task<ActionResult<Fund>> PostFund(Fund fund);
        Task<IActionResult> PutFund(string id, Fund fund);
    }
}