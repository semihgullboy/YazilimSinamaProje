using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Context _context;

        public ProjectController(Context context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProjectSearch(string p)
        {
            var projects = _context.Projects.Include(p => p.User).ToList();

            if (!string.IsNullOrEmpty(p))
            {
                projects = projects.Where(m => m.ProjectTitle.Contains(p)).ToList();
            }

            return View(projects);
        }

        [HttpGet]
        public IActionResult ProjectAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProjectAdd(Project project)
        {
            var usermail = User.Identity.Name;
            var userId = _context.Users.Where(x => x.Email == usermail).Select(y => y.UserID).FirstOrDefault();
            project.UserID = userId;
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList", "Project");
        }

        [HttpGet]
        public IActionResult ProjectList()
        {
            var projects = _context.Projects.Include(p => p.User).ToList();
            return View(projects);
        }
    }
}
