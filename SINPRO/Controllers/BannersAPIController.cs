using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;

namespace SINPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersAPIController : ControllerBase
    {
        private readonly SINContext _context;

        public BannersAPIController(SINContext context)
        {
            _context = context;
        }

        // GET: api/Banners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mBanner>>> GetmBanner()
        {
            return await _context.mBanner.ToListAsync();
        }

        // GET: api/Banners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mBanner>> GetmBanner(int id)
        {
            var mBanner = await _context.mBanner.FindAsync(id);

            if (mBanner == null)
            {
                return NotFound();
            }

            return mBanner;
        }

        // PUT: api/Banners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmBanner(int id, mBanner mBanner)
        {
            if (id != mBanner.id)
            {
                return BadRequest();
            }

            _context.Entry(mBanner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mBannerExists(id))
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

        // POST: api/Banners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mBanner>> PostmBanner(mBanner mBanner)
        {
            _context.mBanner.Add(mBanner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmBanner", new { id = mBanner.id }, mBanner);
        }

        // DELETE: api/Banners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemBanner(int id)
        {
            var mBanner = await _context.mBanner.FindAsync(id);
            if (mBanner == null)
            {
                return NotFound();
            }

            _context.mBanner.Remove(mBanner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mBannerExists(int id)
        {
            return _context.mBanner.Any(e => e.id == id);
        }
    }
}
