using WebDev.Models;
using WebDev.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebDev.Controllers
{
    public class AccountsController : Controller
    {
        private readonly DBContext context;
        public AccountsController(DBContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(string userName, string password)
        //{
        //    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    if (userName == "Admin" && password == "password")
        //    {
        //        var identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name, userName),
        //            new Claim(ClaimTypes.Role, "Admin")
        //            }, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var principal = new ClaimsPrincipal(identity);
        //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        return RedirectToAction("Index", "Home");
        //    }

        //    if (userName == "User" && password == "password")
        //    {
        //        var identity = new ClaimsIdentity(new[] {
        //            new Claim(ClaimTypes.Name, userName),
        //            new Claim(ClaimTypes.Role, "User")
        //            }, CookieAuthenticationDefaults.AuthenticationScheme);

        //        var principal = new ClaimsPrincipal(identity);
        //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //        return RedirectToAction("Index", "Home");
        //    }

        //    return RedirectToAction("Login");
        //}
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            User user = await context.Users.Include(a => a.Role).FirstOrDefaultAsync(a => a.UserName == userName);

            if (user == null || user.Password != password)
            {
                return RedirectToAction("Login");
            }

            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                    new Claim(ClaimTypes.Email, user.Email)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
