using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPDTrackerWebApp;
using IPDTrackerWebApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace IPDTrackerWebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/BillingEntries")]
    [Authorize]
    public class BillingEntriesController : Controller
    {
        private readonly IPDContext _context;

        public BillingEntriesController(IPDContext context)
        {
            _context = context;
        }

        // GET: api/BillingEntries
        [HttpGet]
        public IEnumerable<BillingEntry> GetBillingEntries()
        {

            return _context.BillingEntries; //.Where(b => b.UserId == User.Identity.Name);
        }
        
        // GET: api/BillingEntries/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillingEntry([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billingEntry = await _context.BillingEntries.SingleOrDefaultAsync(m => m.Id == id);

            if (billingEntry == null)
            {
                return NotFound();
            }

            return Ok(billingEntry);
        }

        // PUT: api/BillingEntries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillingEntry([FromRoute] Guid id, [FromBody] BillingEntry billingEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billingEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(billingEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingEntryExists(id))
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

        // POST: api/BillingEntries
        [HttpPost]
        public async Task<IActionResult> PostBillingEntry([FromBody] BillingEntry billingEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BillingEntries.Add(billingEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillingEntry", new { id = billingEntry.Id }, billingEntry);
        }

        // DELETE: api/BillingEntries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillingEntry([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billingEntry = await _context.BillingEntries.SingleOrDefaultAsync(m => m.Id == id);
            if (billingEntry == null)
            {
                return NotFound();
            }

            _context.BillingEntries.Remove(billingEntry);
            await _context.SaveChangesAsync();

            return Ok(billingEntry);
        }

        private bool BillingEntryExists(Guid id)
        {
            return _context.BillingEntries.Any(e => e.Id == id);
        }
    }
}