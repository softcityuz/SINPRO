using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using SINPRO.Services;

namespace SINPRO.Controllers
{
    public class RolesController : Controller
    {
        private readonly SINContext _context;
        private readonly ImRoleService _roleServise;
        public RolesController(SINContext context, ImRoleService roleServise)
        {
            _context = context;
            _roleServise = roleServise;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View(await _context.mRole.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id == null)
            {
                return NotFound();
            }

            var mRole = await _context.mRole
                .FirstOrDefaultAsync(m => m.id == id);
            if (mRole == null)
            {
                return NotFound();
            }

            return View(mRole);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,parentId,resourceName,status")] mRole mRole)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (ModelState.IsValid)
            {
                _roleServise.Insert(mRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mRole);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id == null)
            {
                return NotFound();
            }

            var mRole = await _context.mRole.FindAsync(id);
            if (mRole == null)
            {
                return NotFound();
            }
            return View(mRole);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,parentId,resourceName,status")] mRole mRole)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id != mRole.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _roleServise.Update(mRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mRoleExists(mRole.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mRole);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id == null)
            {
                return NotFound();
            }

            var mRole = await _context.mRole
                .FirstOrDefaultAsync(m => m.id == id);
            if (mRole == null)
            {
                return NotFound();
            }

            return View(mRole);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var mRole = await _context.mRole.FindAsync(id);
            _context.mRole.Remove(mRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mRoleExists(int id)
        {
            return _context.mRole.Any(e => e.id == id);
        }
    }
}
