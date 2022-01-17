
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
    [Authorize(Policy ="user")]
    public class BannersAPIController : ControllerBase
    {
        private readonly SINContext _context;
        private readonly ImBannerService _bannerService;
        public BannersAPIController(SINContext context, ImBannerService bannerService)
        {
            _context = context;
            _bannerService = bannerService;
        }
        // GET: api/Banners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mBanner>>> GetmBanner()
        {
            return await _context.mBanner.ToListAsync();
        }
        [Authorize]
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
        [Authorize(Policy = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmBanner(int id, mBanner mBanner)
        {
            if (id != mBanner.id)
            {
                return BadRequest();
            }
            var result= _bannerService.GetByID(id);
            mBanner.inserted = result.inserted;
            mBanner.updated = result.updated;
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
        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<ActionResult<mBanner>> PostmBanner(mBanner mBanner)
        {
            mBanner.inserted = DateTime.Now;
            _context.mBanner.Add(mBanner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmBanner", new { id = mBanner.id }, mBanner);
        }

        [Authorize(Policy = "admin")]
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
