using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SINPRO.Entity;
using SINPRO.Entity.DataModels;
using SINPRO.Services;

namespace SINPRO.Controllers
{
    public class BannersController : Controller
    {
        private readonly SINContext _context;
        private readonly ImBannerService bannerService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BannersController(SINContext context, ImBannerService bannerService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.bannerService = bannerService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View(await _context.mBanner.ToListAsync());
        }

        // GET: Banners/Details/5
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

            var mBanner = await _context.mBanner
                .FirstOrDefaultAsync(m => m.id == id);
            if (mBanner == null)
            {
                return NotFound();
            }

            return View(mBanner);
        }

        // GET: Banners/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,link")] mBanner mBanner, IFormFile photo)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (ModelState.IsValid)
            {
                mBanner.photo = fileSave(photo, null).Result;
                _context.Add(mBanner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mBanner);
        }
        private async Task<string> fileSave(IFormFile formFile, string oldFileName)
        {
            var filePATH = "";
            if (oldFileName != null || oldFileName != "")
            {
                string spath = _hostEnvironment.ContentRootPath + @"/wwwroot/" + oldFileName;
                FileInfo fileInfo = new FileInfo(spath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }
            if (formFile != null && formFile.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                string spath = _hostEnvironment.ContentRootPath + @"/wwwroot/files/";
                //string spFiles = spath.Substring(0, spath.IndexOf("\\"));
                //spFiles = Path.Combine(spFiles, "");
                var filename = guid + "." + fileType(formFile.ContentType);
                //var drpath = Directory.GetDirectoryRoot("/wwwroot/files/");
                var filePuth = Path.Combine(spath, filename);
                filePATH = "/files/" + filename;
                using (var stream = new FileStream(filePuth, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return filePATH;
        }
        private string fileType(string type)
        {
            switch (type)
            {
                case "image/jpeg": return "jpg";
                case "image/jpg": return "jpg";
                case "image/png": return "png";
                case "image/gif": return "gif";
                case "image/bmp": return "bmp";
                default:
                    return "jpg";
            }
        }
        // GET: Banners/Edit/5
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

            var mBanner = await _context.mBanner.FindAsync(id);
            if (mBanner == null)
            {
                return NotFound();
            }
            return View(mBanner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,link")] mBanner mBanner, IFormFile photo)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id != mBanner.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo != null && photo.Length > 0)
                    {
                        mBanner.photo = fileSave(photo, _context.mBanner.Find(id).photo).Result;
                    }
                    bannerService.Update(mBanner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mBannerExists(mBanner.id))
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
            return View(mBanner);
        }

        // GET: Banners/Delete/5
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

            var mBanner = await _context.mBanner
                .FirstOrDefaultAsync(m => m.id == id);
            if (mBanner == null)
            {
                return NotFound();
            }

            return View(mBanner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var mBanner = await _context.mBanner.FindAsync(id);
            _context.mBanner.Remove(mBanner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mBannerExists(int id)
        {
            return _context.mBanner.Any(e => e.id == id);
        }
    }
}
