using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using YazılımSınamaProje.Models;
using Microsoft.EntityFrameworkCore;

namespace YazılımSınamaProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly Context _context;

        public AccountController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var userInfo = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (userInfo != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };
                var userIdentity = new ClaimsIdentity(claims, "a");
                var principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index2", "Home");
            }

            ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
