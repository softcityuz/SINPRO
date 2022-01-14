using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SINPRO.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SINPRO.Controllers
{
    public class AccountController : Controller
    {
        private readonly ImUserService _userService;

        public AccountController(ImUserService userService)
        {
            _userService = userService;
        }

        // GET: AccountController
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var _user = _userService.GetBySignIn(email, password);
            if (_user != null)
            {
                HttpContext.Session.SetString("userName", _user.email);
                HttpContext.Session.SetString("userRole", _user.roleId.ToString());
                HttpContext.Session.SetString("userId", _user.id.ToString());
                return RedirectToAction("Index","Home");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account", new { msg = 2 });
        }
        
        //public ActionResult Register()
        //{

        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register()
        //{

        //    return View();
        //}
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account", new { msg = 2 });
        }

    }
}
