using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using System.Security.Claims;
using CIMOBProject.Services;
using System.Text.Encodings.Web;

namespace CIMOBProject.Controllers
{
    public class QuizzsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuizzsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quizzs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzs.ToListAsync());
        }

        private const int FINAL_STAT_ID = 6;

        public IActionResult Publish(int id , string employeeId) {
            Quizz quizz = _context.Quizzs.Where(q => q.Id == id).FirstOrDefault();
            if (quizz != null) {
                string title = "Questionário " + quizz.Year + " " + quizz.Semester + "º semestre";
                string content = "Encontra-se no email dos alunos que terminaram a mobilização uma " +
                    "ligação para a realização do questionário" +
                    " sobre o que acharam da experiência, este é de realização opcional mas irá ajudar na" +
                    " melhoria do nosso serviço. Obrigado, CIMOB.";
                News news = new News() {
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

        private List<string> filterFinalStudents() {
            List<Edital> latestEditals = _context.Editals.OrderByDescending(e => e.Id).Take(2).ToList(); //get last 2 editals
            List<string> emails = new List<string>();
            List<Student> students = _context.Students.Include(s => s.Applications).ThenInclude(a => a.ApplicationStat).ToList();
            Application latestApplication = null;
            foreach (Student student in students.ToList()) {
                latestApplication = student.Applications.OrderBy(a => a.ApplicationId).LastOrDefault();
                if (latestApplication == null) {
                    students.Remove(student);
                }
                else if (latestApplication.ApplicationStatId != FINAL_STAT_ID) {
                    students.Remove(student);
                }
                else if (latestEditals.Count > 0) {
                    if (latestEditals[0] != null && latestEditals.Count > 1) {
                        if (latestEditals[1] != null) {
                            if (!(latestApplication.CreationDate.Ticks > latestEditals[0].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[0].CloseDate.Ticks) ||
                            (latestApplication.CreationDate.Ticks > latestEditals[1].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[1].CloseDate.Ticks)) {
                                students.Remove(student);
                            }
                        }
                        else {
                            if (!(latestApplication.CreationDate.Ticks > latestEditals[0].OpenDate.Ticks &&
                            latestApplication.CreationDate.Ticks < latestEditals[0].CloseDate.Ticks)) {
                                students.Remove(student);
                            }
                        }
                    }
                }
            }
            emails = students.Select(s => s.Email).ToList();
            return emails;
        }

        // GET: Quizzs/Details/5
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

        // GET: Quizzs/Create
        public IActionResult Create()
        {
            loadHelp();
            return View();
        }

        // POST: Quizzs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Quizzs/Edit/5
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

        // POST: Quizzs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Quizzs/Delete/5
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

        // POST: Quizzs/Delete/5
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
