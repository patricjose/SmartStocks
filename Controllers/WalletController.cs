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
    public class WalletController : ControllerBase, IWalletController
    {
        private readonly WalletContext _context;

        public WalletController(WalletContext context)
        {
            _context = context;
        }

        // GET: api/Wallet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            var walletList = await _context.Wallets.ToListAsync();
            var assetList = await _context.Assets.ToListAsync();

            foreach (Wallet w in walletList)
                w.Assets = assetList.Where(a => a.WalletId == w.Id);

            return walletList;
        }

        // GET: api/Wallet/52a54125-b175-4ceb-8547-0275a3a66c45
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
                return NotFound();

            var assetList = await _context.Assets.ToListAsync();

            wallet.Assets = assetList.Where(a => a.WalletId == wallet.Id);

            return wallet;
        }

        // PUT: api/Wallet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWallet(Guid id, Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
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

        // POST: api/Wallet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        // DELETE: api/Wallet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wallet>> DeleteWallet(Guid id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return wallet;
        }

        private bool WalletExists(Guid id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }
    }
}
