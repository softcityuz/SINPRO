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
    public class MatchesController : Controller
    {
        private readonly SINContext _context;
        private readonly ImMatchService _matchService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MatchesController(SINContext context, ImMatchService matchService, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _matchService = matchService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View(await _context.mMatch.ToListAsync());
        }

        // GET: Matches/Details/5
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

            var mMatch = await _context.mMatch
                .FirstOrDefaultAsync(m => m.id == id);
            if (mMatch == null)
            {
                return NotFound();
            }

            return View(mMatch);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,firstTimeName,secondTimeName,matchTime,matchDate,textDescription")] mMatch mMatch,IFormFile firstTimePhoto,IFormFile secondTimePhoto)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (ModelState.IsValid)
            {
                string firstPhoto = fileSave(firstTimePhoto,null).Result;
                string secondPhoto = fileSave(secondTimePhoto,null).Result;
                mMatch.firstTimePhoto = firstPhoto;
                mMatch.secondTimePhoto = secondPhoto;
                mMatch.inserted = DateTime.Now;
                _context.Add(mMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mMatch);
        }
        private async Task<string> fileSave(IFormFile formFile,string oldFileName)
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
                string spath = _hostEnvironment.ContentRootPath+ @"/wwwroot/files/";
                //string spFiles = spath.Substring(0, spath.IndexOf("\\"));
                //spFiles = Path.Combine(spFiles, "");
                var filename = guid+ "." + fileType(formFile.ContentType);
                //var drpath = Directory.GetDirectoryRoot("/wwwroot/files/");
                var filePuth = Path.Combine(spath, filename);
                filePATH = "/files/"+filename;
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
        // GET: Matches/Edit/5
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

            var mMatch = await _context.mMatch.FindAsync(id);
            if (mMatch == null)
            {
                return NotFound();
            }
            return View(mMatch);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,firstTimeName,secondTimeName,matchTime,matchDate,textDescription")] mMatch mMatch, IFormFile firstTimePhoto, IFormFile secondTimePhoto)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            if (id != mMatch.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (firstTimePhoto!=null&&firstTimePhoto.Length>0)
                    {
                        mMatch.firstTimePhoto = fileSave(firstTimePhoto, _context.mMatch.Find(id).firstTimePhoto).Result;
                    }
                    if (secondTimePhoto!=null&&secondTimePhoto.Length>0)
                    {
                        mMatch.secondTimePhoto = fileSave(secondTimePhoto, _context.mMatch.Find(id).secondTimePhoto).Result;
                    }
                    _matchService.Update(mMatch);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mMatchExists(mMatch.id))
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
            return View(mMatch);
        }

        // GET: Matches/Delete/5
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

            var mMatch = await _context.mMatch
                .FirstOrDefaultAsync(m => m.id == id);
            if (mMatch == null)
            {
                return NotFound();
            }

            return View(mMatch);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            var mMatch = await _context.mMatch.FindAsync(id);
            fileSave(null, mMatch.firstTimePhoto);
            fileSave(null, mMatch.secondTimePhoto);
            _context.mMatch.Remove(mMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mMatchExists(int id)
        {
            return _context.mMatch.Any(e => e.id == id);
        }
    }
}
