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
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Banners()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
