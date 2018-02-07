using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using CIMOBProject.Services;
using System.Text.Encodings.Web;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for a bonus feature, quizzes.
    /// </summary>
    public class QuizzsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int FINAL_STAT_ID = 6;

        /// <summary>
        /// Initializes controller with the pretended context.
        /// </summary>
        /// <param name="context"></param>
        public QuizzsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzs.ToListAsync());
        }        

        /// <summary>
        /// Publishes a quizz, notifying the users of such publication and publishing the url via email.
        /// There will also be created a news warning that a quizz has been published. 
        /// The quizz is supposed to only be filled by students who finished the mobility process.
        /// </summary>
        /// <param name="id">Quizz id.</param>
        /// <param name="employeeId">Logged employee id.</param>
        /// <returns></returns>
        public IActionResult Publish(int id, string employeeId)
        {
            Quizz quizz = _context.Quizzs.Where(q => q.Id == id).FirstOrDefault();
            if (quizz != null)
            {
                string title = "Questionário " + quizz.Year + " " + quizz.Semester + "º semestre";
                string content = "Encontra-se no email dos alunos que terminaram a mobilização uma " +
                    "ligação para a realização do questionário" +
                    " sobre o que acharam da experiência, este é de realização opcional mas irá ajudar na" +
                    " melhoria do nosso serviço. Obrigado, CIMOB.";
                News news = new News()
                {
                    EmployeeId = employeeId,
                    IsPublished = true,
                    Title = title,
                    TextContent = content
                };
                EmailSender sender = new EmailSender();
                sender.SendMultipleEmail(filterFinalStudents(), title, $"Aqui está o seu questionário:" +
                    $" <a href='{HtmlEncoder.Default.Encode(quizz.QuizzUrl)}'>link</a>.");
                quizz.IsPublished = true;
                _context.Update(quizz);
                _context.Add(news);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "News");
        }

        /// <summary>
        /// Decides which students are supposed to take the quizz.
        /// </summary>
        /// <returns>List of students emails.</returns>
        private List<string> filterFinalStudents()
        {
            List<Edital> latestEditals = _context.Editals.OrderByDescending(e => e.Id)
                .Take(2).ToList(); //get last 2 editals
            List<string> emails = new List<string>();
            List<Student> students = _context.Students.Include(s => s.Applications)
                .ThenInclude(a => a.ApplicationStat).ToList();
            Application latestApplication = null;
            foreach (Student student in students.ToList())
            {
                latestApplication = student.Applications.OrderBy(a => a.ApplicationId).LastOrDefault();
                if (latestApplication == null)
                {
                    students.Remove(student);
                }
                else if (latestApplication.ApplicationStatId != FINAL_STAT_ID)
                {
                    students.Remove(student);
                }
                else if (latestEditals.Count > 0)
                {
                    if (latestEditals[0] != null && latestEditals.Count > 1)
                    {
                        if (latestEditals[1] != null)
                        {
                            if (!(latestApplication.CreationDate.Ticks > latestEditals[0].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[0].CloseDate.Ticks) ||
                            !(latestApplication.CreationDate.Ticks > latestEditals[1].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[1].CloseDate.Ticks))
                            {
                                students.Remove(student);
                            }
                        }
                        else
                        {
                            if (!(latestApplication.CreationDate.Ticks > latestEditals[0].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[0].CloseDate.Ticks))
                            {
                                students.Remove(student);
                            }
                        }
                    }
                }
            }
            emails = students.Select(s => s.Email).ToList();
            return emails;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        public IActionResult Create()
        {
            loadHelp();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Semester,QuizzUrl")] Quizz quizz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quizz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            loadHelp();
            return View(quizz);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzs.SingleOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }
            return View(quizz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Semester,QuizzUrl")] Quizz quizz)
        {
            if (id != quizz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzExists(quizz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quizz);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizz = await _context.Quizzs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (quizz == null)
            {
                return NotFound();
            }

            return View(quizz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quizz = await _context.Quizzs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Quizzs.Remove(quizz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void loadHelp()
        {
            ViewData["YearTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Year") as Help).HelpDescription;
            ViewData["SemesterTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "Semester") as Help).HelpDescription;
            ViewData["QuizURLTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "QuizURL") as Help).HelpDescription;
        }

        private bool QuizzExists(int id)
        {
            return _context.Quizzs.Any(e => e.Id == id);
        }
    }
}
