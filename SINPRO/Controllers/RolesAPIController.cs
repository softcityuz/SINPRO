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
    [Authorize(Policy ="roles")]
    public class RolesAPIController : ControllerBase
    {
        private readonly SINContext _context;
        private readonly ImRoleService _rolesService;

        public RolesAPIController(SINContext context, ImRoleService rolesService)
        {
            _context = context;
            _rolesService = rolesService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<mRole>>> GetmRole()
        {
            return await _context.mRole.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<mRole>> GetmRole(int id)
        {
            var mRole = await _context.mRole.FindAsync(id);

            if (mRole == null)
            {
                return NotFound();
            }

            return mRole;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutmRole(int id, mRole mRole)
        {
            if (id != mRole.id)
            {
                return BadRequest();
            }
            _rolesService.Update(mRole);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!mRoleExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<mRole>> PostmRole(mRole mRole)
        {
            mRole.inserted = DateTime.Now;
            _context.mRole.Add(mRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetmRole", new { id = mRole.id }, mRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletemRole(int id)
        {
            var mRole = await _context.mRole.FindAsync(id);
            if (mRole == null)
            {
                return NotFound();
            }

            _context.mRole.Remove(mRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool mRoleExists(int id)
        {
            return _context.mRole.Any(e => e.id == id);
        }
    }
}
