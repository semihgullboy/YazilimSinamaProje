﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            var usermail = User.Identity.Name;
            Context c = new Context();
            var user = c.Users.FirstOrDefault(x => x.Email == usermail);

            if (user != null)
            {
                var UserName = user.FirstName + " " + user.LastName;

                // Kullanıcının mevcut bakiyesini al
                var Money = user.Money;

                // Eğer bakiye null ise (veya belirli bir durumu temsil ediyorsa), 200 TL ekle
                if (Money == null)
                {
                    Money = 300;
                    user.Money = Money;
                    c.SaveChanges(); // Veritabanındaki değişiklikleri kaydet
                }

                // ViewBag'e değerleri ata
                ViewBag.UserName = UserName;
                ViewBag.Money = Money;

                return View();
            }
            return View();

        }

        public IActionResult Privacy()
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