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
    [Authorize(Policy = "admin")]
    public class UsersAPIController : ControllerBase
    {
        private readonly SINContext _context;
        private readonly ImUserService _usersService;

        public UsersAPIController(SINContext context, ImUserService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mUser>>> GetmUser()
        {
            return await _context.mUser.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mUser>> GetmUser(int id)
        {
            var mUser = await _context.mUser.FindAsync(id);

            if (mUser == null)
            {
                return NotFound();
            }

            return mUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmUser(int id, mUser mUser)
        {
            if (id != mUser.id)
            {
                return BadRequest();
            }
            var res = _usersService.GetByID(id);
            mUser.inserted = res.inserted;
            mUser.updated = DateTime.Now;
            _context.Entry(mUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mUserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mUser>> PostmUser(mUser mUser)
        {
            mUser.inserted = DateTime.Now;
            _context.mUser.Add(mUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmUser", new { id = mUser.id }, mUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemUser(int id)
        {
            var mUser = await _context.mUser.FindAsync(id);
            if (mUser == null)
            {
                return NotFound();
            }

            _context.mUser.Remove(mUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mUserExists(int id)
        {
            return _context.mUser.Any(e => e.id == id);
        }
    }
}