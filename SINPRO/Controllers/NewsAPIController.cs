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
    public class NewsAPIController : ControllerBase
    {
        private readonly SINContext _context;
        private readonly ImNewService _newsService;
        public NewsAPIController(SINContext context, ImNewService newsService)
        {
            _context = context;
            _newsService = newsService;
        }

        // GET: api/News
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mNew>>> GetmNew()
        {
            return await _context.mNew.ToListAsync();
        }

        // GET: api/News/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<mNew>> GetmNew(int id)
        {
            var mNew = await _context.mNew.FindAsync(id);

            if (mNew == null)
            {
                return NotFound();
            }

            return mNew;
        }

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmNew(int id, mNew mNew)
        {
            if (id != mNew.id)
            {
                return BadRequest();
            }
            var res = _newsService.GetByID(id);
            mNew.inserted = res.inserted;
            mNew.updated = DateTime.Now;
            _context.Entry(mNew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mNewExists(id))
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

        // POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<mNew>> PostmNew(mNew mNew)
        {
            mNew.inserted = DateTime.Now;
            _context.mNew.Add(mNew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmNew", new { id = mNew.id }, mNew);
        }

        // DELETE: api/News/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemNew(int id)
        {
            var mNew = await _context.mNew.FindAsync(id);
            if (mNew == null)
            {
                return NotFound();
            }

            _context.mNew.Remove(mNew);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [Authorize]
        private bool mNewExists(int id)
        {
            return _context.mNew.Any(e => e.id == id);
        }
    }
}
