using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class OfferController : Controller
    {
       
        [HttpGet]
        public IActionResult GiveOffer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GiveOffer(int projectId, int teklifMiktari)
        {
            using (var c = new Context())
            {
                var usermail = User.Identity.Name;
                var userID = c.users.Where(x => x.email == usermail).Select(y => y.userID).FirstOrDefault();
                var teklifAlanID = c.projects.Where(p => p.projectID == projectId).Select(p => p.userId).FirstOrDefault();

                // Kullanıcının daha önce aynı projeye teklif verip vermediğini kontrol et
                var existingOffer = c.offers.Where(o => o.projectID == projectId && o.bidderID == userID).FirstOrDefault();

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
                    projectID = projectId,
                    bidAmount = teklifMiktari,
                    offerRecipientID = teklifAlanID,
                    bidderID = userID
                };

                c.offers.Add(offer);
                c.SaveChanges();

                TempData["SuccessMessage"] = "Teklifiniz başarıyla gönderildi!";
                return RedirectToAction("ProjectList", "Project");
            }
        }



        [HttpGet]
        public IActionResult OfferList(Offer offer)
        {
            using (var c = new Context())
            {
                var usermail = User.Identity.Name;
                var girisyapan = c.users.Where(x => x.email == usermail).Select(y => y.userID).FirstOrDefault();
                var degerler = c.offers
                    .Where(x => x.offerRecipientID == girisyapan)
                    .ToList();

                c.offers.Include(p => p.Project).ToList();

                if (degerler.Count == 0)
                {
                    ViewBag.TeklifBulunamadi = "Henüz bir teklif bulunmamaktadır.";
                }

                return View(degerler);
            }
        }

    }
}
