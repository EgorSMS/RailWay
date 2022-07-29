using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebRailwayApp.Models;

namespace WebRailwayApp.Controllers
{
    public class AuthController : Controller
    {
        private RailwayDBContext db;

        public AuthController(RailwayDBContext context)
        {
            this.db = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Authorization()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Authorization(LoginModel user)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await db.User.FirstOrDefaultAsync(u => u.Login == user.Login && u.Password == user.Password);
                if (currentUser != null)
                {
                    await Authenticate(user.Login);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
