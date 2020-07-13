using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartStocksAPI.Data;
using SmartStocksAPI.Models;

namespace SmartStocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly FundContext _context;

        public FundController(FundContext context)
        {
            _context = context;
        }

        // GET: api/Fund
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fund>>> GetFund()
        {
            return await _context.Fund.ToListAsync();
        }

        // GET: api/Fund/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fund>> GetFund(Guid id)
        {
            var fund = await _context.Fund.FindAsync(id);

            if (fund == null)
            {
                return NotFound();
            }

            return fund;
        }

        // PUT: api/Fund/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFund(Guid id, Fund fund)
        {
            if (id != fund.Id)
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
            _context.Fund.Add(fund);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFund", new { id = fund.Id }, fund);
        }

        // DELETE: api/Fund/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fund>> DeleteFund(Guid id)
        {
            var fund = await _context.Fund.FindAsync(id);
            if (fund == null)
            {
                return NotFound();
            }

            _context.Fund.Remove(fund);
            await _context.SaveChangesAsync();

            return fund;
        }

        private bool FundExists(Guid id)
        {
            return _context.Fund.Any(e => e.Id == id);
        }
    }
}
