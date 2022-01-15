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
    public class NewsController : Controller
    {
        private readonly SINContext _context;
        private readonly ImNewService _newService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(SINContext context, ImNewService newService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _newService = newService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View(await _context.mNew.ToListAsync());
        }

        // GET: News/Details/5
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

            var mNew = await _context.mNew
                .FirstOrDefaultAsync(m => m.id == id);
            if (mNew == null)
            {
                return NotFound();
            }

            return View(mNew);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,discription")] mNew mNew, IFormFile photo)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (ModelState.IsValid)
            {
                mNew.photo = fileSave(photo, null).Result;
                _newService.Insert(mNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mNew);
        }

        // GET: News/Edit/5
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

            var mNew = await _context.mNew.FindAsync(id);
            if (mNew == null)
            {
                return NotFound();
            }
            return View(mNew);
        }
        private async Task<string> fileSave(IFormFile formFile, string oldFileName)
        {
            var filePATH = "";
            if (oldFileName != null || oldFileName != "")
            {
                string spath = _hostEnvironment.ContentRootPath + @"\wwwroot\" + oldFileName;
                FileInfo fileInfo = new FileInfo(spath);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
            }
            if (formFile != null && formFile.Length > 0)
            {
                var guid = Guid.NewGuid().ToString();
                string spath = _hostEnvironment.ContentRootPath + @"\wwwroot\files\";
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
        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,discription")] mNew mNew, IFormFile photo)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id != mNew.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo != null && photo.Length > 0)
                    {
                        mNew.photo = fileSave(photo, _context.mNew.Find(id).photo).Result;
                    }
                    _newService.Update(mNew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mNewExists(mNew.id))
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
            return View(mNew);
        }

        // GET: News/Delete/5
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

            var mNew = await _context.mNew
                .FirstOrDefaultAsync(m => m.id == id);
            if (mNew == null)
            {
                return NotFound();
            }

            return View(mNew);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var mNew = await _context.mNew.FindAsync(id);
            fileSave(null, mNew.photo);
            _context.mNew.Remove(mNew);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mNewExists(int id)
        {
            return _context.mNew.Any(e => e.id == id);
        }
    }
}
