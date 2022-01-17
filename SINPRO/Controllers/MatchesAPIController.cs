using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using SINPRO.Services;

namespace SINPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "user")]
    public class MatchesAPIController : ControllerBase
    {
        private readonly SINContext _context;
        private readonly ImMatchService _matchService;

        public MatchesAPIController(SINContext context, ImMatchService matchService)
        {
            _context = context;
            _matchService = matchService;
        }

        // GET: api/mMatches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mMatch>>> GetmMatch()
        {
            return await _context.mMatch.ToListAsync();
        }

        // GET: api/mMatches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mMatch>> GetmMatch(int id)
        {
            var mMatch = await _context.mMatch.FindAsync(id);

            if (mMatch == null)
            {
                return NotFound();
            }

            return mMatch;
        }
        [Authorize(Policy = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmMatch(int id, mMatch mMatch)
        {
            if (id != mMatch.id)
            {
                return BadRequest();
            }
            var res = _matchService.GetByID(id);
            mMatch.inserted = res.inserted;
            mMatch.updated = DateTime.Now;
            _context.Entry(mMatch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mMatchExists(id))
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

        // POST: api/mMatches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<ActionResult<mMatch>> PostmMatch(mMatch mMatch)
        {
            mMatch.inserted = DateTime.Now;
            _context.mMatch.Add(mMatch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmMatch", new { id = mMatch.id }, mMatch);
        }

        // DELETE: api/mMatches/5
        [Authorize(Policy = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemMatch(int id)
        {
            var mMatch = await _context.mMatch.FindAsync(id);
            if (mMatch == null)
            {
                return NotFound();
            }

            _context.mMatch.Remove(mMatch);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool mMatchExists(int id)
        {
            return _context.mMatch.Any(e => e.id == id);
        }
    }
}