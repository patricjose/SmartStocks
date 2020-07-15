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
    public class FundController : ControllerBase, IFundController
    {
        private readonly FundContext _context;
        private readonly IWalletController _walletController;

        public FundController(FundContext context, IWalletController walletController)
        {
            _context = context;
            _walletController = walletController;
        }

        // GET: api/Fund
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fund>>> GetFund()
        {
            var getWallets = await _walletController.GetWallets();
            var wallets = getWallets.Value;
            var funds = await _context.Funds.ToListAsync();

            foreach (Fund f in funds)
            {
                if (wallets.Count() != 0)
                    f.Wallet = wallets.First(w => w.FundName == f.FundName);
            }

            return funds;
        }

        // GET: api/Fund/forpus-acoes-master-fia
        [HttpGet("{id}")]
        public async Task<ActionResult<Fund>> GetFund(string id)
        {
            var fund = await _context.Funds.FindAsync(id);

            if (fund == null)
                return NotFound();

            var getWallets = await _walletController.GetWallets();
            var wallets = getWallets.Value;

            if (wallets.Count() != 0)
                fund.Wallet = wallets.First(w => w.FundName == fund.FundName);

            return fund;
        }

        // PUT: api/Fund/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFund(string id, Fund fund)
        {
            if (id != fund.FundName)
            {
                return BadRequest();
            }

            _context.Entry(fund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundExists(id))
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

        // POST: api/Fund
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Fund>> PostFund(Fund fund)
        {
            _context.Funds.Add(fund);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FundExists(fund.FundName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFund", new { id = fund.FundName }, fund);
        }

        // DELETE: api/Fund/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fund>> DeleteFund(string id)
        {
            var fund = await _context.Funds.FindAsync(id);
            if (fund == null)
            {
                return NotFound();
            }

            _context.Funds.Remove(fund);
            await _context.SaveChangesAsync();

            return fund;
        }

        private bool FundExists(string id)
        {
            return _context.Funds.Any(e => e.FundName == id);
        }
    }
}
