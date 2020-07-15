using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStocksAPI.Data;
using SmartStocksAPI.Models;

namespace SmartStocksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendedWalletController : ControllerBase, IRecommendedWalletController
    {
        private readonly RecommendedWalletContext _context;
        private readonly IWalletController _walletController;
        private readonly IFundController _fundController;

        public RecommendedWalletController(RecommendedWalletContext context, IWalletController walletController, IFundController fundController)
        {
            _context = context;
            _walletController = walletController;
            _fundController = fundController;
        }

        // GET: api/RecommendedWallet/{fundNames}
        [HttpGet]
        public async Task<ActionResult<RecommendedWallet>> GetRecommendedWallet(string fundName)
        {
            var RecommendedWallet = new RecommendedWallet();

            var fund = await _fundController.GetFund(fundName);
            var wallet = await _walletController.GetWallet(fund.Value.Wallet.Id);
            
            RecommendedWallet.Id = new Guid();
            RecommendedWallet.Assets = wallet.Value.Assets.OrderByDescending(s => s.Size).Take(5);
            RecommendedWallet.FundNames = new List<string>();
            RecommendedWallet.FundNames.Add(fundName);
            RecommendedWallet.Performance12Months = 0;
            RecommendedWallet.Performance6Months = 0;
            RecommendedWallet.Performance1Month = 0;
            RecommendedWallet.Total = RecommendedWallet.Assets.Select(s => s.Size).Sum();

            return RecommendedWallet;
        }

        // GET: api/RecommendedWallet/52a54125-b175-4ceb-8547-0275a3a66c45
        [HttpGet("{id}")]
        public async Task<ActionResult<RecommendedWallet>> GetRecommendedWallet(Guid id)
        {
            var recommendedWallet = await _context.RecommendedWallets.FindAsync(id);

            if (recommendedWallet == null)
                return NotFound();

            var assetList = await _context.Assets.ToListAsync();

            //recommendedWallet.Assets = assetList.Where(a => a.FundName == recommendedWallet.Fund);

            return recommendedWallet;
        }

        // PUT: api/RecommendedWallet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecommendedWallet(Guid id, RecommendedWallet recommendedWallet)
        {
            if (id != recommendedWallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(recommendedWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecommendedWalletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecommendedWallet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RecommendedWallet>> PostRecommendedWallet(RecommendedWallet recommendedWallet)
        {
            _context.RecommendedWallets.Add(recommendedWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecommendedWallet", new { id = recommendedWallet.Id }, recommendedWallet);
        }

        // DELETE: api/RecommendedWallet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecommendedWallet>> DeleteRecommendedWallet(Guid id)
        {
            var recommendedWallet = await _context.RecommendedWallets.FindAsync(id);
            if (recommendedWallet == null)
            {
                return NotFound();
            }

            _context.RecommendedWallets.Remove(recommendedWallet);
            await _context.SaveChangesAsync();

            return recommendedWallet;
        }

        private bool RecommendedWalletExists(Guid id)
        {
            return _context.RecommendedWallets.Any(e => e.Id == id);
        }
    }
}
