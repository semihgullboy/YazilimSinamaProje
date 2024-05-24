using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YazılımSınamaProje.Models;
using System.Linq;

namespace YazılımSınamaProje.Controllers
{
    public class OfferController : Controller
    {
        private readonly Context _context;

        public OfferController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GiveOffer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GiveOffer(int projectId, int teklifMiktari)
        {
            var usermail = User.Identity.Name;
            var userID = _context.Users.Where(x => x.Email == usermail).Select(y => y.UserID).FirstOrDefault();
            var teklifAlanID = _context.Projects.Where(p => p.ProjectID == projectId).Select(p => p.UserID).FirstOrDefault();

            var existingOffer = _context.Offers.FirstOrDefault(o => o.ProjectID == projectId && o.BidderID == userID);
            if (existingOffer != null)
            {
                TempData["ErrorMessage"] = "Bu projeye zaten teklif verdiniz!";
                return RedirectToAction("ProjectList", "Project");
            }

            if (userID == teklifAlanID)
            {
                TempData["ErrorMessage"] = "Kendi projenize teklif veremezsiniz!";
                return RedirectToAction("ProjectList", "Project");
            }

            var offer = new Offer
            {
                ProjectID = projectId,
                BidAmount = teklifMiktari,
                OfferRecipientID = teklifAlanID,
                BidderID = userID
            };

            _context.Offers.Add(offer);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Teklifiniz başarıyla gönderildi!";
            return RedirectToAction("ProjectList", "Project");
        }

        [HttpGet]
        public IActionResult OfferList()
        {
            var usermail = User.Identity.Name;
            var girisyapan = _context.Users.Where(x => x.Email == usermail).Select(y => y.UserID).FirstOrDefault();
            var degerler = _context.Offers
                .Include(o => o.Project)
                .Where(o => o.OfferRecipientID == girisyapan)
                .ToList();

            if (degerler.Count == 0)
            {
                ViewBag.TeklifBulunamadi = "Henüz bir teklif bulunmamaktadır.";
            }

            return View(degerler);
        }
    }
}
