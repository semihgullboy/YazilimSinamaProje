using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Linq;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class ProjectController : Controller
    {

        [Authorize]
        public IActionResult Index() {
            
            return View(); 
        }

        [HttpGet]
        public IActionResult ProjectSearch(string p)
        {
            using var c = new Context();
            var degerler = from d in c.projects select d;
            c.projects.Include(p => p.User).ToList();
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.projectTitle.Contains(p));
            }
            return View(degerler.ToList());
        }

        [HttpGet]
        public IActionResult ProjectAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProjectAdd(Project project)
        {
            using var c = new Context();
            var usermail = User.Identity.Name;
            var UserId = c.users.Where(x => x.email == usermail).Select(y => y.userID).FirstOrDefault();
            project.userId = UserId;
            c.projects.Add(project);
            c.SaveChanges();
            return RedirectToAction("ProjectList", "Project");
        }

        [HttpGet]
        public IActionResult ProjectList()
        {
            using var c = new Context();
            var degerler = c.projects.ToList();
            c.projects.Include(p => p.User).ToList();
            return View( degerler);
        }
    }
}
