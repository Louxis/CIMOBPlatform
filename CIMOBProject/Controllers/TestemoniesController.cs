using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions that involve directly Testemony class.
    /// </summary>
    public class TestemoniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes controller with the pretended context.
        /// </summary>
        /// <param name="context"></param>
        public TestemoniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string userId)
        {

            var currentUser = _context.ApplicationUsers.Where(m => m.Id.Equals(userId)).SingleOrDefault();
            ViewData["currentStudentApplication"] = "";
            var applicationDbContext = _context.Testemonies.Include(t => t.Student).OrderByDescending(t => t.CreationDate);

            if(currentUser != null)
            {
                
                var currentStudentApplication = _context.Applications.Include(s => s.ApplicationStat)
                            .Where(a => a.StudentId.Equals(currentUser.Id)).OrderBy(a => a.ApplicationId).LastOrDefault();
                ViewData["currentStudentApplication"] = "";
                ViewData["testemony"] = "";
                if (currentStudentApplication != null)
                {

                    var latestEdital = _context.Editals.Where(e => e.OpenDate <= currentStudentApplication.CreationDate && e.CloseDate >= currentStudentApplication.CreationDate).LastOrDefault();
                    var userTestemony = _context.Testemonies.Include(t => t.Student).Where(t => t.Student.Id.Equals(currentUser.Id)
                                                        && ((latestEdital.OpenDate <= t.CreationDate)
                                                        || (latestEdital.CloseDate <= t.CreationDate))).SingleOrDefault();
                    
                    if (userTestemony != null)
                    {
                        ViewData["testemony"] = userTestemony;
                    }
                    
                    ViewData["currentStudentApplication"] = currentStudentApplication;
                }
            }


            return View(await applicationDbContext.ToListAsync());

        }

        /// <summary>
        /// This method changes Valid attribute in class Testemony to True.
        /// </summary>
        /// <param name="testemonyId">ID of the testemony to change Valid attibute</param>
        /// <returns>The View Index</returns>
        public async Task<IActionResult> ValidateTestemony(string userId, int testemonyId)
        {
            var testemony = _context.Testemonies.Where(t => t.TestemonyId == testemonyId).SingleOrDefault();
            testemony.Valid = true;
            _context.Update(testemony);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Testemonies", new { userId = userId });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testemony = await _context.Testemonies
                .Include(t => t.Student)
                .SingleOrDefaultAsync(m => m.TestemonyId == id);
            if (testemony == null)
            {
                return NotFound();
            }

            return View(testemony);
        }

        public IActionResult Create(string userId)
        {
            ViewData["StudentId"] = userId;
            ViewData["CreationDate"] = DateTime.Now;

            loadHelp();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestemonyId,Title,Content,StudentId,Valid,CreationDate")] Testemony testemony)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testemony);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Testemonies", new { userId = testemony.StudentId });
            }
            ViewData["StudentId"] = testemony.StudentId;
            ViewData["CreationDate"] = testemony.CreationDate;
            return View(testemony);
        }

        public async Task<IActionResult> Delete(int? id, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testemony = await _context.Testemonies
                .Include(t => t.Student)
                .SingleOrDefaultAsync(m => m.TestemonyId == id);
            if (testemony == null)
            {
                return NotFound();
            }
            ViewData["userId"] = userId;
            return View(testemony);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string userId)
        {
            var testemony = await _context.Testemonies.SingleOrDefaultAsync(m => m.TestemonyId == id);

            _context.Testemonies.Remove(testemony);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Testemonies", new { userId = userId });
        }

        private void loadHelp()
        {
            ViewData["TitleTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Title") as Help).HelpDescription;
            ViewData["TextContentTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "TextContent") as Help).HelpDescription;
        }

        private bool TestemonyExists(int id)
        {
            return _context.Testemonies.Any(e => e.TestemonyId == id);
        }
    }
}
