using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;
using CIMOBProject.Services;

namespace CIMOBProject.Controllers
{
    /// <summary>
    /// This controller is responsible for all the actions that involve directly Interview class.
    /// </summary>
    public class InterviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private EmailSender emailSender;

        public InterviewsController(ApplicationDbContext context)
        {
            _context = context;
            emailSender = new EmailSender();
        }

        // GET: Interviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Interviews.Include(i => i.Application).ThenInclude(a => a.Student).Include(i => i.Employee).Where(i => i.InterviewDate >= DateTime.Now).OrderByDescending(i => i.InterviewDate);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Interviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews
                .Include(i => i.Application)
                .Include(i => i.Employee)
                .SingleOrDefaultAsync(m => m.InterviewId == id);
            if (interview == null)
            {
                return NotFound();
            }

            return View(interview);
        }
        /// <summary>
        /// Action that leads to Interview creation view.
        /// Receives employee's ID and application's ID so it can create an Interview.
        /// </summary>
        /// <param name="employeeId">Employee's ID in the system</param>
        /// <param name="applicationId">Application's ID in the system</param>
        /// <returns>The view that will lead to the creation of an Interview</returns>
        // GET: Interviews/Create
        public IActionResult Create(string employeeId, int applicationId)
        {
            ViewData["ApplicationId"] = applicationId;
            ViewData["EmployeeId"] = employeeId;

            loadHelp();

            return View();
        }

        /// <summary>
        /// This method will create an object of type Interview.
        /// </summary>
        /// <param name="interview">The object to add to database</param>
        /// <returns>Index View if creation is valid, otherwise returns the same where this method was called.</returns>
        // POST: Interviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterviewId,EmployeeId,ApplicationId,InterviewDate")] Interview interview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interview);
                await _context.SaveChangesAsync();
                Application app = _context.Applications.Where(a => a.ApplicationId == interview.ApplicationId).SingleOrDefault();
                Student interviewStudent = _context.Applications.Include(a => a.Student).Where(a => a.StudentId.Equals(app.StudentId)).SingleOrDefault().Student;
                await emailSender.Execute("Entrevista Agendada", "Saudações, " +
                    interview.Application.Student.UserFullname + " uma entrevista consigo foi agendada para o dia " + interview.InterviewDate +
                    " no nosso gabinete. Entre em contacto conosco se não for possivel comparecer a esta entrevista." +
                    " Uma falta sem justificação irá resulta numa avaliação de 0.", interview.Application.Student.Email);
                return RedirectToAction("Index", "Applications", new { employeeId = interview.EmployeeId });
            }
            ViewData["ApplicationId"] = interview.ApplicationId;
            ViewData["EmployeeId"] = interview.EmployeeId;
            return View(interview);
        }

        // GET: Interviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interview = await _context.Interviews.SingleOrDefaultAsync(m => m.InterviewId == id);
            if (interview == null)
            {
                return NotFound();
            }
            ViewData["ApplicationId"] = interview.ApplicationId;
            ViewData["EmployeeId"] = interview.EmployeeId;

            loadHelp();

            return View(interview);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InterviewId,EmployeeId,ApplicationId,InterviewDate")] Interview interview)
        {
            if (id != interview.InterviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewExists(interview.InterviewId))
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
            ViewData["ApplicationId"] = interview.ApplicationId;
            ViewData["EmployeeId"] =  interview.EmployeeId;
            return View(interview);
        }

        //Loads help tips
        private void loadHelp()
        {
            ViewData["InterviewDateTip"] = (_context.Helps.FirstOrDefault(h => h.HelpName == "InterviewDate") as Help).HelpDescription;
        }

        private bool InterviewExists(int id)
        {
            return _context.Interviews.Any(e => e.InterviewId == id);
        }
    }
}
