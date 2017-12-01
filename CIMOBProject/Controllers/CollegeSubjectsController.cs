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
    public class CollegeSubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public void GetCollegeSubjects(int index)
        {
            ViewData["CollegeSubjectId"] = new SelectList(_context.CollegeSubjects.Where(s => s.Id == index), "Id", "SubjectName");
        }

        public CollegeSubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollegeSubjects
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollegeSubjects.ToListAsync());
        }

        // GET: CollegeSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeSubject = await _context.CollegeSubjects
                .SingleOrDefaultAsync(m => m.Id == id);
            if (collegeSubject == null)
            {
                return NotFound();
            }

            return View(collegeSubject);
        }

        // GET: CollegeSubjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollegeSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectName,Credits")] CollegeSubject collegeSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collegeSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collegeSubject);
        }

        // GET: CollegeSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeSubject = await _context.CollegeSubjects.SingleOrDefaultAsync(m => m.Id == id);
            if (collegeSubject == null)
            {
                return NotFound();
            }
            return View(collegeSubject);
        }

        // POST: CollegeSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectName,Credits")] CollegeSubject collegeSubject)
        {
            if (id != collegeSubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collegeSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollegeSubjectExists(collegeSubject.Id))
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
            return View(collegeSubject);
        }

        // GET: CollegeSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collegeSubject = await _context.CollegeSubjects
                .SingleOrDefaultAsync(m => m.Id == id);
            if (collegeSubject == null)
            {
                return NotFound();
            }

            return View(collegeSubject);
        }

        // POST: CollegeSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collegeSubject = await _context.CollegeSubjects.SingleOrDefaultAsync(m => m.Id == id);
            _context.CollegeSubjects.Remove(collegeSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollegeSubjectExists(int id)
        {
            return _context.CollegeSubjects.Any(e => e.Id == id);
        }
    }
}
