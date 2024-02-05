
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class WorkController : Controller
    {

        [HttpPost]
        public IActionResult OfferConfirmation(int projectId, int teklifverenID, int teklifalanID, int newbudget)
        {
            using (var c = new Context())
            {
                // Daha önce bu projeyi onaylamış bir iş var mı kontrol et
                var existingWork = c.works.FirstOrDefault(w => w.projectID == projectId && w.employerID == teklifalanID);

                if (existingWork != null)
                {
                    // Daha önce onaylanmış bir iş varsa, hata mesajı gönder
                    TempData["ErrorMessage"] = "Bu projeyi zaten onayladınız!";
                    return RedirectToAction("OfferList", "Offer");
                }

                // Yeni işi oluştur ve veritabanına ekle
                var work = new Work
                {
                    projectID = projectId,
                    workBusinessID = teklifverenID,
                    employerID = teklifalanID,
                    newBudget = newbudget,
                    // Diğer teklif özelliklerini doldurabilirsiniz
                };

                c.works.Add(work);
                c.SaveChanges();

                // Başarı mesajını TempData ile sakla
                TempData["SuccessMessage"] = "Teklif başarıyla gönderildi.";

                return RedirectToAction("OfferList", "Offer");
            }
        }


        [HttpGet]
        public IActionResult MyWorks()
        {
            using var c = new Context();
            var usermail = User.Identity.Name;
            var girisyapan = c.users.Where(x => x.email == usermail).Select(y => y.userID).FirstOrDefault();
            var degerler = c.works.Where(x => x.workBusinessID== girisyapan).ToList();
            c.works.Include(p => p.project).ToList();
            return View(degerler);
        }

        [HttpPost]
        public IActionResult JobDelivery(int workId, string aciklama)
        {
            using (var c = new Context())
            {
                // İş ID'sine göre ilgili işi bul
                var work = c.works.FirstOrDefault(w => w.workID == workId);

                if (work != null)
                {
                    if (!string.IsNullOrEmpty(work.explanation))
                    {
                        TempData["ErrorMessage"] = "İş zaten teslim edildi. Başka bir açıklama ekleyemezsiniz.";
                        return RedirectToAction("MyWorks", "Work");
                    }
                    // Açıklamayı ilgili işe ekle
                    work.explanation = aciklama;
                    c.SaveChanges();
                }
                // Başarıyla teslim edildiğine dair mesaj
                TempData["SuccessMessage"] = "İş başarıyla teslim edildi. Onay bekleniyor.";

                // Geri dönmek istediğiniz sayfaya yönlendirin
                return RedirectToAction("MyWorks", "Work");
            }
        }

        [HttpGet]
        public IActionResult JobDeliveryList()
        {
            using var c = new Context();
            var usermail = User.Identity.Name;
            var girisyapan = c.users.Where(x => x.email == usermail).Select(y => y.userID).FirstOrDefault();
            var degerler = c.works.Where(x => x.employerID == girisyapan).ToList();
            c.works.Include(p => p.project).ToList();
            return View(degerler);
        }

        [HttpPost]
        public IActionResult ApprovedJob(int workId)
        {
            using (var c = new Context())
            {
                var work = c.works.FirstOrDefault(w => w.workID == workId);

                if (work != null)
                {
                    // İşçinin parasını arttır
                    var worker = c.users.FirstOrDefault(u => u.userID == work.workBusinessID);
                    if (worker != null)
                    {
                        // Eğer worker'ın parası null ise, bir başlangıç değeri atayın
                        if (worker.money == null)
                        {
                            worker.money = 0; // veya başka bir başlangıç değeri
                        }

                        worker.money += work.newBudget;
                    }

                    // İşverenin parasını azalt
                    var employer = c.users.FirstOrDefault(u => u.userID == work.employerID);
                    if (employer != null)
                    {
                        // Eğer employer'ın parası null ise, bir başlangıç değeri atayın
                        if (employer.money == null)
                        {
                            employer.money = 0; // veya başka bir başlangıç değeri
                        }

                        employer.money -= work.newBudget;
                    }

                    // İş ve projeyi sil
                    c.works.Remove(work);
                    var project = c.projects.FirstOrDefault(p => p.projectID == work.projectID);
                    if (project != null)
                    {
                        c.projects.Remove(project);
                    }

                    // Değişiklikleri kaydet
                    c.SaveChanges();
                }

                // Başka bir sayfaya yönlendirme
                return RedirectToAction("Index2", "Home");
            }
        }
    }
}
