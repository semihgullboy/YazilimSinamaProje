using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class AccountController : Controller
    {
       
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User p1)
        {
            using var c = new Context();
            c.users.Add(p1);
            c.SaveChanges();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User p)
        {
            using var c = new Context();
            var bilgiler = c.users.FirstOrDefault(c => c.email == p.email && c.password == p.password);

            if (bilgiler != null)
            {
                //HttpContext.Session.SetString("UserID", p.Email);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.email)
                };
                var useridentity = new ClaimsIdentity(claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index2", "Home");
            }

            // Eğer bilgiler null ise, yani eşleşen kullanıcı yoksa, ModelState hatası ekleyin.
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

