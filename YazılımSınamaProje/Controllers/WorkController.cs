using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class WorkController : Controller
    {
        private readonly Context _context;

        public WorkController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult OfferConfirmation(int projectId, int teklifverenID, int teklifalanID, int newbudget)
        {
            var existingWork = _context.Works.FirstOrDefault(w => w.ProjectID == projectId && w.EmployerID == teklifalanID);

            if (existingWork != null)
            {
                TempData["ErrorMessage"] = "Bu projeyi zaten onayladınız!";
                return RedirectToAction("OfferList", "Offer");
            }

            var work = new Work
            {
                ProjectID = projectId,
                WorkBusinessID = teklifverenID,
                EmployerID = teklifalanID,
                NewBudget = newbudget
            };

            _context.Works.Add(work);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Teklif başarıyla gönderildi.";

            return RedirectToAction("OfferList", "Offer");
        }


        [HttpGet]
        public IActionResult MyWorks()
        {
            var usermail = User.Identity.Name;
            var girisyapan = _context.Users.Where(x => x.Email == usermail).Select(y => y.UserID).FirstOrDefault();
            var degerler = _context.Works.Where(x => x.WorkBusinessID == girisyapan).Include(w => w.Project).ToList();
            return View(degerler);
        }

        [HttpPost]
        public IActionResult JobDelivery(int workId, string aciklama)
        {
            var work = _context.Works.FirstOrDefault(w => w.WorkID == workId);

            if (work != null)
            {
                if (!string.IsNullOrEmpty(work.Explanation))
                {
                    TempData["ErrorMessage"] = "İş zaten teslim edildi. Başka bir açıklama ekleyemezsiniz.";
                    return RedirectToAction("MyWorks", "Work");
                }

                work.Explanation = aciklama;
                _context.SaveChanges();
            }

            TempData["SuccessMessage"] = "İş başarıyla teslim edildi. Onay bekleniyor.";

            return RedirectToAction("MyWorks", "Work");
        }

        [HttpGet]
        public IActionResult JobDeliveryList()
        {
            var usermail = User.Identity.Name;
            var girisyapan = _context.Users.Where(x => x.Email == usermail).Select(y => y.UserID).FirstOrDefault();
            var degerler = _context.Works.Where(x => x.EmployerID == girisyapan).Include(w => w.Project).ToList();
            return View(degerler);
        }

        [HttpPost]
        public IActionResult ApprovedJob(int workId)
        {
            var work = _context.Works.FirstOrDefault(w => w.WorkID == workId);

            if (work != null)
            {
                var worker = _context.Users.FirstOrDefault(u => u.UserID == work.WorkBusinessID);
                if (worker != null)
                {
                    if (worker.Money == null)
                    {
                        worker.Money = 0;
                    }

                    worker.Money += work.NewBudget;
                }

                var employer = _context.Users.FirstOrDefault(u => u.UserID == work.EmployerID);
                if (employer != null)
                {
                    if (employer.Money == null)
                    {
                        employer.Money = 0;
                    }

                    employer.Money -= work.NewBudget;
                }

                _context.Works.Remove(work);
                var project = _context.Projects.FirstOrDefault(p => p.ProjectID == work.ProjectID);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index2", "Home");
        }
    }
}
