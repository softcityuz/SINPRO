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
    public class UsersController : Controller
    {
        private readonly SINContext _context;
        private readonly ImUserService _userService;

        public UsersController(SINContext context, ImUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var sINContext = _context.mUser.Include(m => m.mRole);
            return View(await sINContext.ToListAsync());
        }

        // GET: Users/Details/5
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

            var mUser = await _context.mUser
                .Include(m => m.mRole)
                .FirstOrDefaultAsync(m => m.id == id);
            if (mUser == null)
            {
                return NotFound();
            }

            return View(mUser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            ViewData["roleId"] = new SelectList(_context.mRole, "id", "id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fName,sName,phone,email,password,status,statusDate")] mUser mUser)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (ModelState.IsValid)
            {
                _userService.Insert(mUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mUser);
        }
        [HttpPost]
        public IActionResult findUser(string login)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return Json(_userService.findEmail(login));
        }
        // GET: Users/Edit/5
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

            var mUser = await _context.mUser.FindAsync(id);
            if (mUser == null)
            {
                return NotFound();
            }
            mUser.password = "";
            //ViewData["roleId"] = new SelectList(_context.mRole, "id", "id", mUser.roleId);
            return View(mUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fName,sName,phone,email,password,status,statusDate")] mUser mUser)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id != mUser.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Update(mUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mUserExists(mUser.id))
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
            ViewData["roleId"] = new SelectList(_context.mRole, "id", "id", mUser.roleId);
            return View(mUser);
        }

        // GET: Users/Delete/5
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

            var mUser = await _context.mUser
                .Include(m => m.mRole)
                .FirstOrDefaultAsync(m => m.id == id);
            if (mUser == null)
            {
                return NotFound();
            }

            return View(mUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var mUser = await _context.mUser.FindAsync(id);
            _context.mUser.Remove(mUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mUserExists(int id)
        {
            return _context.mUser.Any(e => e.id == id);
        }
    }
}
