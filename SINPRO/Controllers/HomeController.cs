using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SINPRO.Logic;
using SINPRO.Models;
using SINPRO.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace SINPRO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ImNewService _newService;
        private readonly ImBannerService _bannerService;
        private readonly ImMatchService _matchService;
        private readonly ImUserService _userService;
        private readonly ImRoleService _roleService;
        private readonly IAuthLogic _authLogic;
        public HomeController(
        ImNewService newService, ImBannerService bannerService, ImMatchService matchService, ImUserService userService, ImRoleService roleService, IAuthLogic authLogic)
        {
            _newService = newService;
            _bannerService = bannerService;
            _matchService = matchService;
            _userService = userService;
            _roleService = roleService;
            _authLogic = authLogic;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            ViewData["userName"] = HttpContext.Session.GetString("userName");
            return View();
        }

        public IActionResult Users()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }
        public IActionResult News()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }
        public IActionResult Banners()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (HttpContext.Session.GetString("userName") == null)
            {
                return RedirectToAction("Login", "Account", new { msg = 2 });
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
