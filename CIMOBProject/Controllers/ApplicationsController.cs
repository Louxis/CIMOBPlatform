using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CIMOBProject.Data;
using CIMOBProject.Models;

namespace CIMOBProject.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applications.Include(a => a.ApplicationStat).Include(a => a.Employee).Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationStat)
                .Include(a => a.Employee)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ApplicationStatId"] = new SelectList(_context.ApplicationStats, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationId,StudentId,ApplicationStatId,EmployeeId,ArithmeticMean,ECTS,MotivationLetter,Enterview,FinalGrade")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationStatId"] = new SelectList(_context.ApplicationStats, "Id", "Name", application.ApplicationStatId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", application.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", application.StudentId);
            return View(application);
        }

        public async Task<IActionResult> Seriation()
        {
            var applicationDbContext = _context.Applications.Include(a => a.ApplicationStat).Include(a => a.Student).Include(a => a.Student.CollegeSubject).Include(a => a.Student.CollegeSubject.College).OrderByDescending(m => m.FinalGrade);
            /*OrderBy(m => m.Student.CollegeSubject.College.CollegeName).ThenBy(m => m.Student.CollegeSubject.SubjectName).*/
            //var query1 = from item in _context.Applications orderby item.ArithmeticMean descending orderby item.ApplicationStatId ascending group item by item.ApplicationStatId into g select new { Name = g.Key, Order = g.ToList() };
            var query1 = from item in _context.Applications select item;


            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["ApplicationStatId"] = new SelectList(_context.ApplicationStats, "Id", "Name", application.ApplicationStatId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", application.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", application.StudentId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,StudentId,ApplicationStatId,EmployeeId,ArithmeticMean,ECTS,MotivationLetter,Enterview,FinalGrade")] Application application)
        {
            if (id != application.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationId))
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
            ViewData["ApplicationStatId"] = new SelectList(_context.ApplicationStats, "Id", "Name", application.ApplicationStatId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", application.EmployeeId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", application.StudentId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationStat)
                .Include(a => a.Employee)
                .Include(a => a.Student)
                .SingleOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.SingleOrDefaultAsync(m => m.ApplicationId == id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
